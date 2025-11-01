using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;

namespace HandDetectionApp;

public partial class Form1 : Form
{
    private VideoCapture? _capture;
    private Mat? _frame;
    private Thread? _cameraThread;
    private bool _isRunning = false;

    public Form1()
    {
        InitializeComponent();
    }

    private void btnBaslat_Click(object? sender, EventArgs e)
    {
        try
        {
            _capture = new VideoCapture(0); // 0 = varsayılan kamera
            
            if (!_capture.IsOpened())
            {
                MessageBox.Show("Kamera açılamadı! Lütfen kamera bağlantınızı kontrol edin.", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _isRunning = true;
            _cameraThread = new Thread(CaptureCamera);
            _cameraThread.Start();

            btnBaslat.Enabled = false;
            btnDurdur.Enabled = true;
            lblDurum.Text = "Durum: Çalışıyor";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Hata: {ex.Message}", "Hata", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDurdur_Click(object? sender, EventArgs e)
    {
        DurdurKamera();
    }

    private void DurdurKamera()
    {
        _isRunning = false;
        
        if (_cameraThread != null && _cameraThread.IsAlive)
        {
            _cameraThread.Join(1000); // 1 saniye bekle
        }

        _capture?.Release();
        _capture?.Dispose();
        _frame?.Dispose();

        btnBaslat.Enabled = true;
        btnDurdur.Enabled = false;
        lblDurum.Text = "Durum: Durduruldu";
        
        pictureBoxCamera.Image = null;
    }

    private void CaptureCamera()
    {
        _frame = new Mat();
        Stopwatch stopwatch = new Stopwatch();

        while (_isRunning && _capture != null && _capture.IsOpened())
        {
            try
            {
                stopwatch.Restart();
                
                _capture.Read(_frame);
                
                if (_frame.Empty())
                    continue;

                Mat processedFrame = _frame.Clone();

                // El algılama aktifse işle
                if (checkBoxElAlgilama.Checked)
                {
                    DetectHand(processedFrame);
                }

                // FPS hesapla ve ekrana yaz
                stopwatch.Stop();
                double fps = 1000.0 / stopwatch.ElapsedMilliseconds;
                Cv2.PutText(processedFrame, $"FPS: {fps:F1}", 
                    new OpenCvSharp.Point(10, 30), 
                    HersheyFonts.HersheySimplex, 0.7, 
                    Scalar.Green, 2);

                // Görüntüyü PictureBox'a aktar
                if (pictureBoxCamera.InvokeRequired)
                {
                    pictureBoxCamera.Invoke(new Action(() =>
                    {
                        var oldImage = pictureBoxCamera.Image;
                        pictureBoxCamera.Image = BitmapConverter.ToBitmap(processedFrame);
                        oldImage?.Dispose();
                    }));
                }
                else
                {
                    var oldImage = pictureBoxCamera.Image;
                    pictureBoxCamera.Image = BitmapConverter.ToBitmap(processedFrame);
                    oldImage?.Dispose();
                }

                processedFrame.Dispose();
                Thread.Sleep(10); // CPU kullanımını düşürmek için
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Kamera okuma hatası: {ex.Message}");
            }
        }
    }

    private void DetectHand(Mat frame)
    {
        try
        {
            // BGR'den HSV'ye dönüştür
            Mat hsvFrame = new Mat();
            Cv2.CvtColor(frame, hsvFrame, ColorConversionCodes.BGR2HSV);

            // Cilt rengi için HSV aralığı
            // Ten rengi tespiti için geniş bir aralık kullanıyoruz
            Scalar lowerSkin = new Scalar(0, 20, 70);
            Scalar upperSkin = new Scalar(20, 255, 255);
            
            Mat skinMask = new Mat();
            Cv2.InRange(hsvFrame, lowerSkin, upperSkin, skinMask);

            // Gürültüyü azaltmak için morfolojik işlemler
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(11, 11));
            Cv2.Erode(skinMask, skinMask, kernel, iterations: 2);
            Cv2.Dilate(skinMask, skinMask, kernel, iterations: 2);
            
            // Gaussian blur ile yumuşatma
            Cv2.GaussianBlur(skinMask, skinMask, new OpenCvSharp.Size(3, 3), 0);

            // Konturları bul
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(skinMask, out contours, out hierarchy, 
                RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            if (contours.Length > 0)
            {
                // En büyük konturu bul (muhtemelen el)
                int maxContourIndex = 0;
                double maxArea = 0;
                
                for (int i = 0; i < contours.Length; i++)
                {
                    double area = Cv2.ContourArea(contours[i]);
                    if (area > maxArea)
                    {
                        maxArea = area;
                        maxContourIndex = i;
                    }
                }

                // Minimum alan kontrolü
                if (maxArea > 5000)
                {
                    var maxContour = contours[maxContourIndex];
                    
                    // Konveks gövde (convex hull) bul
                    OpenCvSharp.Point[] hull = Cv2.ConvexHull(maxContour);
                    
                    // El konturunu çiz
                    Cv2.DrawContours(frame, new OpenCvSharp.Point[][] { hull }, -1, 
                        Scalar.Green, 2);

                    // Bounding rectangle çiz
                    var rect = Cv2.BoundingRect(maxContour);
                    Cv2.Rectangle(frame, rect, Scalar.Blue, 2);
                    
                    Cv2.PutText(frame, "El Algilandi!", 
                        new OpenCvSharp.Point(10, 60),
                        HersheyFonts.HersheySimplex, 0.7, Scalar.Green, 2);
                }
            }

            hsvFrame.Dispose();
            skinMask.Dispose();
            kernel.Dispose();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"El algılama hatası: {ex.Message}");
        }
    }

    private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        DurdurKamera();
    }
}
