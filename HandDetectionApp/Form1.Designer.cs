namespace HandDetectionApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.pictureBoxCamera = new System.Windows.Forms.PictureBox();
        this.btnBaslat = new System.Windows.Forms.Button();
        this.btnDurdur = new System.Windows.Forms.Button();
        this.lblDurum = new System.Windows.Forms.Label();
        this.checkBoxElAlgilama = new System.Windows.Forms.CheckBox();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).BeginInit();
        this.SuspendLayout();
        // 
        // pictureBoxCamera
        // 
        this.pictureBoxCamera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBoxCamera.Location = new System.Drawing.Point(12, 12);
        this.pictureBoxCamera.Name = "pictureBoxCamera";
        this.pictureBoxCamera.Size = new System.Drawing.Size(640, 480);
        this.pictureBoxCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.pictureBoxCamera.TabIndex = 0;
        this.pictureBoxCamera.TabStop = false;
        // 
        // btnBaslat
        // 
        this.btnBaslat.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.btnBaslat.Location = new System.Drawing.Point(670, 12);
        this.btnBaslat.Name = "btnBaslat";
        this.btnBaslat.Size = new System.Drawing.Size(120, 50);
        this.btnBaslat.TabIndex = 1;
        this.btnBaslat.Text = "Başlat";
        this.btnBaslat.UseVisualStyleBackColor = true;
        this.btnBaslat.Click += new System.EventHandler(this.btnBaslat_Click);
        // 
        // btnDurdur
        // 
        this.btnDurdur.Enabled = false;
        this.btnDurdur.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.btnDurdur.Location = new System.Drawing.Point(670, 68);
        this.btnDurdur.Name = "btnDurdur";
        this.btnDurdur.Size = new System.Drawing.Size(120, 50);
        this.btnDurdur.TabIndex = 2;
        this.btnDurdur.Text = "Durdur";
        this.btnDurdur.UseVisualStyleBackColor = true;
        this.btnDurdur.Click += new System.EventHandler(this.btnDurdur_Click);
        // 
        // lblDurum
        // 
        this.lblDurum.AutoSize = true;
        this.lblDurum.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.lblDurum.Location = new System.Drawing.Point(670, 150);
        this.lblDurum.Name = "lblDurum";
        this.lblDurum.Size = new System.Drawing.Size(120, 19);
        this.lblDurum.TabIndex = 3;
        this.lblDurum.Text = "Durum: Hazır";
        // 
        // checkBoxElAlgilama
        // 
        this.checkBoxElAlgilama.AutoSize = true;
        this.checkBoxElAlgilama.Checked = true;
        this.checkBoxElAlgilama.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkBoxElAlgilama.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.checkBoxElAlgilama.Location = new System.Drawing.Point(670, 190);
        this.checkBoxElAlgilama.Name = "checkBoxElAlgilama";
        this.checkBoxElAlgilama.Size = new System.Drawing.Size(122, 23);
        this.checkBoxElAlgilama.TabIndex = 4;
        this.checkBoxElAlgilama.Text = "El Algılamayı Aç";
        this.checkBoxElAlgilama.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(810, 510);
        this.Controls.Add(this.checkBoxElAlgilama);
        this.Controls.Add(this.lblDurum);
        this.Controls.Add(this.btnDurdur);
        this.Controls.Add(this.btnBaslat);
        this.Controls.Add(this.pictureBoxCamera);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "El Algılama Uygulaması - OpenCV";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBoxCamera;
    private System.Windows.Forms.Button btnBaslat;
    private System.Windows.Forms.Button btnDurdur;
    private System.Windows.Forms.Label lblDurum;
    private System.Windows.Forms.CheckBox checkBoxElAlgilama;
}
