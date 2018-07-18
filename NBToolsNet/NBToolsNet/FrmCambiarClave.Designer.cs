namespace NBToolsNet
{
    partial class FrmCambiarClave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCambiarClave));
            this.PanelPpal = new System.Windows.Forms.Panel();
            this.LblNombreCia = new System.Windows.Forms.Label();
            this.LblModuloVersion = new System.Windows.Forms.Label();
            this.LblMensaje = new System.Windows.Forms.Label();
            this.CmdCancelar = new System.Windows.Forms.Button();
            this.CmdAceptar = new System.Windows.Forms.Button();
            this.GrpClave2 = new System.Windows.Forms.GroupBox();
            this.TxtClave2 = new System.Windows.Forms.TextBox();
            this.GrpClave1 = new System.Windows.Forms.GroupBox();
            this.TxtClave1 = new System.Windows.Forms.TextBox();
            this.GrpClave = new System.Windows.Forms.GroupBox();
            this.TxtClave = new System.Windows.Forms.TextBox();
            this.PanelPpal.SuspendLayout();
            this.GrpClave2.SuspendLayout();
            this.GrpClave1.SuspendLayout();
            this.GrpClave.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelPpal
            // 
            this.PanelPpal.BackColor = System.Drawing.Color.Brown;
            this.PanelPpal.Controls.Add(this.LblNombreCia);
            this.PanelPpal.Controls.Add(this.LblModuloVersion);
            this.PanelPpal.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelPpal.Location = new System.Drawing.Point(0, 0);
            this.PanelPpal.Name = "PanelPpal";
            this.PanelPpal.Size = new System.Drawing.Size(570, 40);
            this.PanelPpal.TabIndex = 1;
            // 
            // LblNombreCia
            // 
            this.LblNombreCia.AutoSize = true;
            this.LblNombreCia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNombreCia.ForeColor = System.Drawing.Color.White;
            this.LblNombreCia.Location = new System.Drawing.Point(3, 22);
            this.LblNombreCia.Name = "LblNombreCia";
            this.LblNombreCia.Size = new System.Drawing.Size(125, 18);
            this.LblNombreCia.TabIndex = 6;
            this.LblNombreCia.Text = "Nombre cia11111";
            // 
            // LblModuloVersion
            // 
            this.LblModuloVersion.AutoSize = true;
            this.LblModuloVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblModuloVersion.ForeColor = System.Drawing.Color.White;
            this.LblModuloVersion.Location = new System.Drawing.Point(3, 0);
            this.LblModuloVersion.Name = "LblModuloVersion";
            this.LblModuloVersion.Size = new System.Drawing.Size(169, 18);
            this.LblModuloVersion.TabIndex = 5;
            this.LblModuloVersion.Text = "modulo y version 1.0.0.0";
            // 
            // LblMensaje
            // 
            this.LblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.Color.Red;
            this.LblMensaje.Location = new System.Drawing.Point(3, 182);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(567, 77);
            this.LblMensaje.TabIndex = 32;
            this.LblMensaje.Text = "mensaje de informacion1 ccccccccccccccccccccccccccccccccccccccccccccccccccc111";
            // 
            // CmdCancelar
            // 
            this.CmdCancelar.BackColor = System.Drawing.Color.IndianRed;
            this.CmdCancelar.Location = new System.Drawing.Point(296, 142);
            this.CmdCancelar.Name = "CmdCancelar";
            this.CmdCancelar.Size = new System.Drawing.Size(110, 28);
            this.CmdCancelar.TabIndex = 4;
            this.CmdCancelar.Text = "&Cancelar";
            this.CmdCancelar.UseVisualStyleBackColor = false;
            this.CmdCancelar.Click += new System.EventHandler(this.CmdCancelar_Click);
            // 
            // CmdAceptar
            // 
            this.CmdAceptar.BackColor = System.Drawing.Color.IndianRed;
            this.CmdAceptar.Location = new System.Drawing.Point(167, 142);
            this.CmdAceptar.Name = "CmdAceptar";
            this.CmdAceptar.Size = new System.Drawing.Size(110, 28);
            this.CmdAceptar.TabIndex = 3;
            this.CmdAceptar.Text = "&Aceptar";
            this.CmdAceptar.UseVisualStyleBackColor = false;
            this.CmdAceptar.Click += new System.EventHandler(this.CmdAceptar_Click);
            // 
            // GrpClave2
            // 
            this.GrpClave2.Controls.Add(this.TxtClave2);
            this.GrpClave2.Location = new System.Drawing.Point(310, 96);
            this.GrpClave2.Name = "GrpClave2";
            this.GrpClave2.Size = new System.Drawing.Size(255, 38);
            this.GrpClave2.TabIndex = 31;
            this.GrpClave2.TabStop = false;
            this.GrpClave2.Text = "Confirma Contraseña";
            // 
            // TxtClave2
            // 
            this.TxtClave2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtClave2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtClave2.ForeColor = System.Drawing.Color.Gray;
            this.TxtClave2.Location = new System.Drawing.Point(3, 16);
            this.TxtClave2.Name = "TxtClave2";
            this.TxtClave2.PasswordChar = '*';
            this.TxtClave2.Size = new System.Drawing.Size(249, 13);
            this.TxtClave2.TabIndex = 2;
            this.TxtClave2.Text = "Clave o Contraseña";
            this.TxtClave2.TextChanged += new System.EventHandler(this.TxtClave2_TextChanged);
            // 
            // GrpClave1
            // 
            this.GrpClave1.Controls.Add(this.TxtClave1);
            this.GrpClave1.Location = new System.Drawing.Point(15, 96);
            this.GrpClave1.Name = "GrpClave1";
            this.GrpClave1.Size = new System.Drawing.Size(255, 38);
            this.GrpClave1.TabIndex = 30;
            this.GrpClave1.TabStop = false;
            this.GrpClave1.Text = "Nueva Contraseña";
            // 
            // TxtClave1
            // 
            this.TxtClave1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtClave1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtClave1.ForeColor = System.Drawing.Color.Gray;
            this.TxtClave1.Location = new System.Drawing.Point(3, 16);
            this.TxtClave1.Name = "TxtClave1";
            this.TxtClave1.PasswordChar = '*';
            this.TxtClave1.Size = new System.Drawing.Size(249, 13);
            this.TxtClave1.TabIndex = 1;
            this.TxtClave1.Text = "Clave o Contraseña";
            this.TxtClave1.TextChanged += new System.EventHandler(this.TxtClave1_TextChanged);
            // 
            // GrpClave
            // 
            this.GrpClave.Controls.Add(this.TxtClave);
            this.GrpClave.Location = new System.Drawing.Point(12, 52);
            this.GrpClave.Name = "GrpClave";
            this.GrpClave.Size = new System.Drawing.Size(255, 38);
            this.GrpClave.TabIndex = 29;
            this.GrpClave.TabStop = false;
            this.GrpClave.Text = "Contraseña";
            // 
            // TxtClave
            // 
            this.TxtClave.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtClave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtClave.ForeColor = System.Drawing.Color.Gray;
            this.TxtClave.Location = new System.Drawing.Point(3, 16);
            this.TxtClave.Name = "TxtClave";
            this.TxtClave.PasswordChar = '*';
            this.TxtClave.Size = new System.Drawing.Size(249, 13);
            this.TxtClave.TabIndex = 0;
            this.TxtClave.Text = "Clave o Contraseña";
            this.TxtClave.TextChanged += new System.EventHandler(this.TxtClave_TextChanged);
            // 
            // FrmCambiarClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 264);
            this.ControlBox = false;
            this.Controls.Add(this.LblMensaje);
            this.Controls.Add(this.CmdCancelar);
            this.Controls.Add(this.CmdAceptar);
            this.Controls.Add(this.GrpClave2);
            this.Controls.Add(this.GrpClave1);
            this.Controls.Add(this.GrpClave);
            this.Controls.Add(this.PanelPpal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCambiarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCambiarClave";
            this.PanelPpal.ResumeLayout(false);
            this.PanelPpal.PerformLayout();
            this.GrpClave2.ResumeLayout(false);
            this.GrpClave2.PerformLayout();
            this.GrpClave1.ResumeLayout(false);
            this.GrpClave1.PerformLayout();
            this.GrpClave.ResumeLayout(false);
            this.GrpClave.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelPpal;
        private System.Windows.Forms.Label LblModuloVersion;
        private System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.Button CmdCancelar;
        private System.Windows.Forms.Button CmdAceptar;
        private System.Windows.Forms.GroupBox GrpClave2;
        private System.Windows.Forms.TextBox TxtClave2;
        private System.Windows.Forms.GroupBox GrpClave1;
        private System.Windows.Forms.TextBox TxtClave1;
        private System.Windows.Forms.GroupBox GrpClave;
        private System.Windows.Forms.TextBox TxtClave;
        private System.Windows.Forms.Label LblNombreCia;
    }
}