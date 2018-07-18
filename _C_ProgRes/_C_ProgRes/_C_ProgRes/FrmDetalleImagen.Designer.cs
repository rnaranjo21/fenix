namespace Fenix
{
    partial class FrmDetalleImagen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetalleImagen));
            this.btnOpen = new System.Windows.Forms.Button();
            this.picZoom = new System.Windows.Forms.PictureBox();
            this.tZoom = new System.Windows.Forms.TrackBar();
            this.LblZoom = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.picSmall = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSmall)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(719, 130);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Visible = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click_1);
            // 
            // picZoom
            // 
            this.picZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picZoom.Location = new System.Drawing.Point(268, 12);
            this.picZoom.Name = "picZoom";
            this.picZoom.Size = new System.Drawing.Size(500, 500);
            this.picZoom.TabIndex = 6;
            this.picZoom.TabStop = false;
            this.picZoom.Click += new System.EventHandler(this.picZoom_Click);
            // 
            // tZoom
            // 
            this.tZoom.LargeChange = 1;
            this.tZoom.Location = new System.Drawing.Point(12, 263);
            this.tZoom.Maximum = 4;
            this.tZoom.Minimum = 1;
            this.tZoom.Name = "tZoom";
            this.tZoom.Size = new System.Drawing.Size(202, 45);
            this.tZoom.TabIndex = 7;
            this.tZoom.Value = 1;
            this.tZoom.Scroll += new System.EventHandler(this.tZoom_Scroll);
            // 
            // LblZoom
            // 
            this.LblZoom.AutoSize = true;
            this.LblZoom.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblZoom.Location = new System.Drawing.Point(8, 240);
            this.LblZoom.Name = "LblZoom";
            this.LblZoom.Size = new System.Drawing.Size(50, 20);
            this.LblZoom.TabIndex = 8;
            this.LblZoom.Text = "Zoom";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFile1";
            // 
            // picSmall
            // 
            this.picSmall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSmall.Location = new System.Drawing.Point(12, 12);
            this.picSmall.Name = "picSmall";
            this.picSmall.Size = new System.Drawing.Size(240, 210);
            this.picSmall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSmall.TabIndex = 9;
            this.picSmall.TabStop = false;
            this.picSmall.Click += new System.EventHandler(this.picSmall_Click);
            this.picSmall.Paint += new System.Windows.Forms.PaintEventHandler(this.picSmall_Paint);
            // 
            // FrmDetalleImagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 516);
            this.Controls.Add(this.tZoom);
            this.Controls.Add(this.LblZoom);
            this.Controls.Add(this.picSmall);
            this.Controls.Add(this.picZoom);
            this.Controls.Add(this.btnOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDetalleImagen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDetalleImagen";
            this.TopMost = true;
            this.Resize += new System.EventHandler(this.FrmDetalleImagen_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSmall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.PictureBox picZoom;
        private System.Windows.Forms.TrackBar tZoom;
        private System.Windows.Forms.Label LblZoom;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox picSmall;
    }
}