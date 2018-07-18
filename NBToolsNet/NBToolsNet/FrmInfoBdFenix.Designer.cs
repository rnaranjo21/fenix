namespace StrailSAS_C_ProgRes
{
    partial class FrmInfoBdFenix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInfoBdFenix));
            this.PanelPpal = new System.Windows.Forms.Panel();
            this.LblNombreCia = new System.Windows.Forms.Label();
            this.LblModuloVersion = new System.Windows.Forms.Label();
            this.GrpClave = new System.Windows.Forms.GroupBox();
            this.TxtClave = new System.Windows.Forms.TextBox();
            this.GrpUsuario = new System.Windows.Forms.GroupBox();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.GrpServidor = new System.Windows.Forms.GroupBox();
            this.cmbServidores = new System.Windows.Forms.ComboBox();
            this.GrpBaseDatos = new System.Windows.Forms.GroupBox();
            this.TxtBD = new System.Windows.Forms.TextBox();
            this.LblMensaje = new System.Windows.Forms.Label();
            this.CmdCancelar = new System.Windows.Forms.Button();
            this.CmdProbar = new System.Windows.Forms.Button();
            this.CmdAceptar = new System.Windows.Forms.Button();
            this.PanelPpal.SuspendLayout();
            this.GrpClave.SuspendLayout();
            this.GrpUsuario.SuspendLayout();
            this.GrpServidor.SuspendLayout();
            this.GrpBaseDatos.SuspendLayout();
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
            this.PanelPpal.Size = new System.Drawing.Size(562, 40);
            this.PanelPpal.TabIndex = 1;
            // 
            // LblNombreCia
            // 
            this.LblNombreCia.AutoSize = true;
            this.LblNombreCia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNombreCia.ForeColor = System.Drawing.Color.White;
            this.LblNombreCia.Location = new System.Drawing.Point(3, 20);
            this.LblNombreCia.Name = "LblNombreCia";
            this.LblNombreCia.Size = new System.Drawing.Size(125, 18);
            this.LblNombreCia.TabIndex = 7;
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
            this.LblModuloVersion.TabIndex = 6;
            this.LblModuloVersion.Text = "modulo y version 1.0.0.0";
            // 
            // GrpClave
            // 
            this.GrpClave.Controls.Add(this.TxtClave);
            this.GrpClave.Location = new System.Drawing.Point(295, 94);
            this.GrpClave.Name = "GrpClave";
            this.GrpClave.Size = new System.Drawing.Size(255, 38);
            this.GrpClave.TabIndex = 20;
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
            this.TxtClave.TabIndex = 3;
            this.TxtClave.Text = "Clave o Contraseña";
            this.TxtClave.TextChanged += new System.EventHandler(this.TxtClave_TextChanged);
            // 
            // GrpUsuario
            // 
            this.GrpUsuario.Controls.Add(this.TxtUsuario);
            this.GrpUsuario.Location = new System.Drawing.Point(12, 94);
            this.GrpUsuario.Name = "GrpUsuario";
            this.GrpUsuario.Size = new System.Drawing.Size(255, 38);
            this.GrpUsuario.TabIndex = 19;
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
            this.TxtUsuario.TabIndex = 2;
            this.TxtUsuario.Text = "Código del Usuario";
            this.TxtUsuario.TextChanged += new System.EventHandler(this.TxtUsuario_TextChanged);
            // 
            // GrpServidor
            // 
            this.GrpServidor.Controls.Add(this.cmbServidores);
            this.GrpServidor.Location = new System.Drawing.Point(12, 50);
            this.GrpServidor.Name = "GrpServidor";
            this.GrpServidor.Size = new System.Drawing.Size(258, 38);
            this.GrpServidor.TabIndex = 17;
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
            this.cmbServidores.SelectedIndexChanged += new System.EventHandler(this.cmbServidores_SelectedIndexChanged);
            // 
            // GrpBaseDatos
            // 
            this.GrpBaseDatos.Controls.Add(this.TxtBD);
            this.GrpBaseDatos.Location = new System.Drawing.Point(295, 50);
            this.GrpBaseDatos.Name = "GrpBaseDatos";
            this.GrpBaseDatos.Size = new System.Drawing.Size(255, 38);
            this.GrpBaseDatos.TabIndex = 18;
            this.GrpBaseDatos.TabStop = false;
            this.GrpBaseDatos.Text = "Base de Datos";
            // 
            // TxtBD
            // 
            this.TxtBD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtBD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtBD.ForeColor = System.Drawing.Color.Gray;
            this.TxtBD.Location = new System.Drawing.Point(3, 16);
            this.TxtBD.Name = "TxtBD";
            this.TxtBD.Size = new System.Drawing.Size(249, 13);
            this.TxtBD.TabIndex = 1;
            this.TxtBD.Text = "Base de Datos";
            this.TxtBD.TextChanged += new System.EventHandler(this.TxtBD_TextChanged);
            // 
            // LblMensaje
            // 
            this.LblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.Color.Red;
            this.LblMensaje.Location = new System.Drawing.Point(3, 173);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(559, 58);
            this.LblMensaje.TabIndex = 27;
            this.LblMensaje.Text = "mensaje de informacion1111";
            // 
            // CmdCancelar
            // 
            this.CmdCancelar.BackColor = System.Drawing.Color.IndianRed;
            this.CmdCancelar.Location = new System.Drawing.Point(368, 138);
            this.CmdCancelar.Name = "CmdCancelar";
            this.CmdCancelar.Size = new System.Drawing.Size(110, 28);
            this.CmdCancelar.TabIndex = 6;
            this.CmdCancelar.Text = "&Cancelar";
            this.CmdCancelar.UseVisualStyleBackColor = false;
            this.CmdCancelar.Click += new System.EventHandler(this.CmdCancelar_Click);
            // 
            // CmdProbar
            // 
            this.CmdProbar.BackColor = System.Drawing.Color.DarkSalmon;
            this.CmdProbar.Location = new System.Drawing.Point(136, 138);
            this.CmdProbar.Name = "CmdProbar";
            this.CmdProbar.Size = new System.Drawing.Size(110, 28);
            this.CmdProbar.TabIndex = 4;
            this.CmdProbar.Text = "Probar Cone&xion";
            this.CmdProbar.UseVisualStyleBackColor = false;
            this.CmdProbar.Click += new System.EventHandler(this.CmdProbar_Click);
            // 
            // CmdAceptar
            // 
            this.CmdAceptar.BackColor = System.Drawing.Color.IndianRed;
            this.CmdAceptar.Location = new System.Drawing.Point(252, 138);
            this.CmdAceptar.Name = "CmdAceptar";
            this.CmdAceptar.Size = new System.Drawing.Size(110, 28);
            this.CmdAceptar.TabIndex = 5;
            this.CmdAceptar.Text = "&Aceptar";
            this.CmdAceptar.UseVisualStyleBackColor = false;
            this.CmdAceptar.Click += new System.EventHandler(this.CmdAceptar_Click);
            // 
            // FrmInfoBdFenix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 240);
            this.ControlBox = false;
            this.Controls.Add(this.LblMensaje);
            this.Controls.Add(this.CmdCancelar);
            this.Controls.Add(this.CmdProbar);
            this.Controls.Add(this.CmdAceptar);
            this.Controls.Add(this.GrpBaseDatos);
            this.Controls.Add(this.GrpClave);
            this.Controls.Add(this.GrpUsuario);
            this.Controls.Add(this.GrpServidor);
            this.Controls.Add(this.PanelPpal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInfoBdFenix";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmInfoBdFenix";
            this.Shown += new System.EventHandler(this.FrmInfoBdFenix_Shown);
            this.PanelPpal.ResumeLayout(false);
            this.PanelPpal.PerformLayout();
            this.GrpClave.ResumeLayout(false);
            this.GrpClave.PerformLayout();
            this.GrpUsuario.ResumeLayout(false);
            this.GrpUsuario.PerformLayout();
            this.GrpServidor.ResumeLayout(false);
            this.GrpBaseDatos.ResumeLayout(false);
            this.GrpBaseDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelPpal;
        private System.Windows.Forms.GroupBox GrpClave;
        private System.Windows.Forms.TextBox TxtClave;
        private System.Windows.Forms.GroupBox GrpUsuario;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.GroupBox GrpServidor;
        private System.Windows.Forms.ComboBox cmbServidores;
        private System.Windows.Forms.GroupBox GrpBaseDatos;
        private System.Windows.Forms.TextBox TxtBD;
        private System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.Button CmdCancelar;
        private System.Windows.Forms.Button CmdProbar;
        private System.Windows.Forms.Button CmdAceptar;
        private System.Windows.Forms.Label LblModuloVersion;
        private System.Windows.Forms.Label LblNombreCia;
    }
}