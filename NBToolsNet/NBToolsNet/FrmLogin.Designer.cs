namespace NBToolsNet
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.GrpServidor = new System.Windows.Forms.GroupBox();
            this.cmbServidores = new System.Windows.Forms.ComboBox();
            this.GrpUsuario = new System.Windows.Forms.GroupBox();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.GrpClave = new System.Windows.Forms.GroupBox();
            this.TxtClave = new System.Windows.Forms.TextBox();
            this.PanelPpal = new System.Windows.Forms.Panel();
            this.LblNombreCia = new System.Windows.Forms.Label();
            this.LblModuloVersion = new System.Windows.Forms.Label();
            this.CmdAceptar = new System.Windows.Forms.Button();
            this.CmdCambiarClave = new System.Windows.Forms.Button();
            this.CmdCancelar = new System.Windows.Forms.Button();
            this.LblMensaje = new System.Windows.Forms.Label();
            this.GrpServidor.SuspendLayout();
            this.GrpUsuario.SuspendLayout();
            this.GrpClave.SuspendLayout();
            this.PanelPpal.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpServidor
            // 
            this.GrpServidor.Controls.Add(this.cmbServidores);
            this.GrpServidor.Location = new System.Drawing.Point(12, 45);
            this.GrpServidor.Name = "GrpServidor";
            this.GrpServidor.Size = new System.Drawing.Size(258, 38);
            this.GrpServidor.TabIndex = 14;
            this.GrpServidor.TabStop = false;
            this.GrpServidor.Text = "Servidor";
            // 
            // cmbServidores
            // 
            this.cmbServidores.FormattingEnabled = true;
            this.cmbServidores.Items.AddRange(new object[] {
            "Nombre de Servidor de Base de Datos"});
            this.cmbServidores.Location = new System.Drawing.Point(6, 11);
            this.cmbServidores.Name = "cmbServidores";
            this.cmbServidores.Size = new System.Drawing.Size(246, 21);
            this.cmbServidores.TabIndex = 0;
            // 
            // GrpUsuario
            // 
            this.GrpUsuario.Controls.Add(this.TxtUsuario);
            this.GrpUsuario.Location = new System.Drawing.Point(12, 89);
            this.GrpUsuario.Name = "GrpUsuario";
            this.GrpUsuario.Size = new System.Drawing.Size(255, 38);
            this.GrpUsuario.TabIndex = 15;
            this.GrpUsuario.TabStop = false;
            this.GrpUsuario.Text = "Usuario";
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtUsuario.ForeColor = System.Drawing.Color.Gray;
            this.TxtUsuario.Location = new System.Drawing.Point(3, 16);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(249, 13);
            this.TxtUsuario.TabIndex = 1;
            this.TxtUsuario.Text = "Código del Usuario";
            this.TxtUsuario.TextChanged += new System.EventHandler(this.TxtUsuario_TextChanged);
            // 
            // GrpClave
            // 
            this.GrpClave.Controls.Add(this.TxtClave);
            this.GrpClave.Location = new System.Drawing.Point(283, 89);
            this.GrpClave.Name = "GrpClave";
            this.GrpClave.Size = new System.Drawing.Size(255, 38);
            this.GrpClave.TabIndex = 16;
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
            this.TxtClave.TabIndex = 2;
            this.TxtClave.Text = "Clave o Contraseña";
            this.TxtClave.TextChanged += new System.EventHandler(this.TxtClave_TextChanged);
            // 
            // PanelPpal
            // 
            this.PanelPpal.BackColor = System.Drawing.Color.Brown;
            this.PanelPpal.Controls.Add(this.LblNombreCia);
            this.PanelPpal.Controls.Add(this.LblModuloVersion);
            this.PanelPpal.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelPpal.Location = new System.Drawing.Point(0, 0);
            this.PanelPpal.Name = "PanelPpal";
            this.PanelPpal.Size = new System.Drawing.Size(547, 40);
            this.PanelPpal.TabIndex = 17;
            // 
            // LblNombreCia
            // 
            this.LblNombreCia.AutoSize = true;
            this.LblNombreCia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNombreCia.ForeColor = System.Drawing.Color.White;
            this.LblNombreCia.Location = new System.Drawing.Point(12, 20);
            this.LblNombreCia.Name = "LblNombreCia";
            this.LblNombreCia.Size = new System.Drawing.Size(125, 18);
            this.LblNombreCia.TabIndex = 1;
            this.LblNombreCia.Text = "Nombre cia11111";
            // 
            // LblModuloVersion
            // 
            this.LblModuloVersion.AutoSize = true;
            this.LblModuloVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblModuloVersion.ForeColor = System.Drawing.Color.White;
            this.LblModuloVersion.Location = new System.Drawing.Point(12, 0);
            this.LblModuloVersion.Name = "LblModuloVersion";
            this.LblModuloVersion.Size = new System.Drawing.Size(169, 18);
            this.LblModuloVersion.TabIndex = 0;
            this.LblModuloVersion.Text = "modulo y version 1.0.0.0";
            // 
            // CmdAceptar
            // 
            this.CmdAceptar.BackColor = System.Drawing.Color.IndianRed;
            this.CmdAceptar.Location = new System.Drawing.Point(110, 132);
            this.CmdAceptar.Name = "CmdAceptar";
            this.CmdAceptar.Size = new System.Drawing.Size(110, 28);
            this.CmdAceptar.TabIndex = 3;
            this.CmdAceptar.Text = "&Ingresar";
            this.CmdAceptar.UseVisualStyleBackColor = false;
            this.CmdAceptar.Click += new System.EventHandler(this.CmdAceptar_Click);
            // 
            // CmdCambiarClave
            // 
            this.CmdCambiarClave.BackColor = System.Drawing.Color.DarkSalmon;
            this.CmdCambiarClave.Location = new System.Drawing.Point(226, 132);
            this.CmdCambiarClave.Name = "CmdCambiarClave";
            this.CmdCambiarClave.Size = new System.Drawing.Size(110, 28);
            this.CmdCambiarClave.TabIndex = 4;
            this.CmdCambiarClave.Text = "Cambiar Cla&ve";
            this.CmdCambiarClave.UseVisualStyleBackColor = false;
            this.CmdCambiarClave.Click += new System.EventHandler(this.CmdCambiarClave_Click);
            // 
            // CmdCancelar
            // 
            this.CmdCancelar.BackColor = System.Drawing.Color.IndianRed;
            this.CmdCancelar.Location = new System.Drawing.Point(342, 132);
            this.CmdCancelar.Name = "CmdCancelar";
            this.CmdCancelar.Size = new System.Drawing.Size(110, 28);
            this.CmdCancelar.TabIndex = 5;
            this.CmdCancelar.Text = "&Cancelar";
            this.CmdCancelar.UseVisualStyleBackColor = false;
            this.CmdCancelar.Click += new System.EventHandler(this.CmdCancelar_Click);
            // 
            // LblMensaje
            // 
            this.LblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.Color.Red;
            this.LblMensaje.Location = new System.Drawing.Point(0, 166);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(547, 73);
            this.LblMensaje.TabIndex = 23;
            this.LblMensaje.Text = "mensaje de informacion1 ccccccccccccccccccccccccccccccccccccccccccccccccccc111";
            this.LblMensaje.Click += new System.EventHandler(this.LblMensaje_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 239);
            this.ControlBox = false;
            this.Controls.Add(this.LblMensaje);
            this.Controls.Add(this.CmdCancelar);
            this.Controls.Add(this.CmdCambiarClave);
            this.Controls.Add(this.CmdAceptar);
            this.Controls.Add(this.PanelPpal);
            this.Controls.Add(this.GrpClave);
            this.Controls.Add(this.GrpUsuario);
            this.Controls.Add(this.GrpServidor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLogin";
            this.Shown += new System.EventHandler(this.FrmLogin_Shown);
            this.GrpServidor.ResumeLayout(false);
            this.GrpUsuario.ResumeLayout(false);
            this.GrpUsuario.PerformLayout();
            this.GrpClave.ResumeLayout(false);
            this.GrpClave.PerformLayout();
            this.PanelPpal.ResumeLayout(false);
            this.PanelPpal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpServidor;
        private System.Windows.Forms.GroupBox GrpUsuario;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.GroupBox GrpClave;
        private System.Windows.Forms.TextBox TxtClave;
        private System.Windows.Forms.Panel PanelPpal;
        private System.Windows.Forms.Label LblModuloVersion;
        private System.Windows.Forms.Button CmdAceptar;
        private System.Windows.Forms.Button CmdCambiarClave;
        private System.Windows.Forms.Button CmdCancelar;
        private System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.ComboBox cmbServidores;
        private System.Windows.Forms.Label LblNombreCia;
    }
}