namespace FENIX_KIOSCO
{
    partial class MyMessageBox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyMessageBox));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblmensaje = new System.Windows.Forms.Label();
            this.cmd_boton1 = new System.Windows.Forms.Button();
            this.cmd_boton2 = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FENIX_KIOSCO.Properties.Resources.Encabezado2;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(432, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lblmensaje
            // 
            this.lblmensaje.BackColor = System.Drawing.Color.Transparent;
            this.lblmensaje.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblmensaje.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmensaje.ForeColor = System.Drawing.Color.Black;
            this.lblmensaje.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblmensaje.Location = new System.Drawing.Point(29, 26);
            this.lblmensaje.Name = "lblmensaje";
            this.lblmensaje.Size = new System.Drawing.Size(350, 70);
            this.lblmensaje.TabIndex = 12;
            this.lblmensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmd_boton1
            // 
            this.cmd_boton1.Location = new System.Drawing.Point(42, 102);
            this.cmd_boton1.Name = "cmd_boton1";
            this.cmd_boton1.Size = new System.Drawing.Size(93, 46);
            this.cmd_boton1.TabIndex = 11;
            this.cmd_boton1.UseVisualStyleBackColor = true;
            this.cmd_boton1.Click += new System.EventHandler(this.cmd_OK_Click);
            // 
            // cmd_boton2
            // 
            this.cmd_boton2.Location = new System.Drawing.Point(273, 102);
            this.cmd_boton2.Name = "cmd_boton2";
            this.cmd_boton2.Size = new System.Drawing.Size(90, 46);
            this.cmd_boton2.TabIndex = 10;
            this.cmd_boton2.UseVisualStyleBackColor = true;
            this.cmd_boton2.Click += new System.EventHandler(this.cmd_cancelar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(0, 13);
            this.lblTitulo.TabIndex = 14;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.BackColor = System.Drawing.Color.Transparent;
            this.lblTimer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTimer.Location = new System.Drawing.Point(191, 102);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(29, 16);
            this.lblTimer.TabIndex = 20;
            this.lblTimer.Text = "kkk";
            this.lblTimer.Visible = false;
            // 
            // MyMessageBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::FENIX_KIOSCO.Properties.Resources._4f3FondoAzul1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(414, 160);
            this.ControlBox = false;
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblmensaje);
            this.Controls.Add(this.cmd_boton1);
            this.Controls.Add(this.cmd_boton2);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyMessageBox";
            this.Opacity = 0.9D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Messagebox";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MyMessageBox_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyMessageBox_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblmensaje;
        private System.Windows.Forms.Button cmd_boton1;
        private System.Windows.Forms.Button cmd_boton2;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblTimer;

    }
}