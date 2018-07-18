using System.Drawing;
namespace FENIX_KIOSCO
{
    partial class Frm_Principal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Principal));
            this.CMD_TurnosRadicacion = new System.Windows.Forms.Button();
            this.CMD_Turnos = new System.Windows.Forms.Button();
            this.cmd_consulta = new System.Windows.Forms.Button();
            this.pnl_infoAfiliado = new System.Windows.Forms.Panel();
            this.lbl_Cedula = new System.Windows.Forms.Label();
            this.lbl_Nombres = new System.Windows.Forms.Label();
            this.ptb_Fotografia = new System.Windows.Forms.PictureBox();
            this.pnl_huella = new System.Windows.Forms.Panel();
            this.pnl_vistaHuella = new System.Windows.Forms.Panel();
            this.ptb_HuellaEstatica = new System.Windows.Forms.PictureBox();
            this.ptb_HuelaPrelim = new System.Windows.Forms.PictureBox();
            this.lbl_respuesta = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.CMD_RENTA = new System.Windows.Forms.Button();
            this.CMD_Certificado = new System.Windows.Forms.Button();
            this.CMD_EstadoTramite = new System.Windows.Forms.Button();
            this.CMD_EstadCuenta = new System.Windows.Forms.Button();
            this.pnl_EstadoCuenta = new System.Windows.Forms.Panel();
            this.pnl_EstadoCuenta1 = new System.Windows.Forms.Panel();
            this.webBrowser3 = new System.Windows.Forms.WebBrowser();
            this.lbl_esta_cuentas_1 = new System.Windows.Forms.Label();
            this.CMD_ImprimirEstado = new System.Windows.Forms.Button();
            this.pnl_TituloCuentas = new System.Windows.Forms.Panel();
            this.lbl_EstadoCuentas = new System.Windows.Forms.Label();
            this.pnl_Turno = new System.Windows.Forms.Panel();
            this.lbl_ceduNAfi = new System.Windows.Forms.Label();
            this.lbl_NombreNoAfi = new System.Windows.Forms.Label();
            this.lbl_Alertas = new System.Windows.Forms.Label();
            this.lbl_HoraTurno = new System.Windows.Forms.Label();
            this.lbl_tipoServicio = new System.Windows.Forms.Label();
            this.lbl_ceduAfi = new System.Windows.Forms.Label();
            this.lbl_NombreAfi = new System.Windows.Forms.Label();
            this.lbl_Turno = new System.Windows.Forms.Label();
            this.ptb_Logo = new System.Windows.Forms.PictureBox();
            this.lbl_TipoTurno = new System.Windows.Forms.Label();
            this.pnl_teclado = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_Identificacion = new System.Windows.Forms.TextBox();
            this.cmd_3 = new System.Windows.Forms.Button();
            this.cmd_2 = new System.Windows.Forms.Button();
            this.cmd_1 = new System.Windows.Forms.Button();
            this.cmd_6 = new System.Windows.Forms.Button();
            this.cmd_5 = new System.Windows.Forms.Button();
            this.cmd_4 = new System.Windows.Forms.Button();
            this.cmd_9 = new System.Windows.Forms.Button();
            this.cmd_8 = new System.Windows.Forms.Button();
            this.cmd_7 = new System.Windows.Forms.Button();
            this.cmd_Accept = new System.Windows.Forms.Button();
            this.cmd_0 = new System.Windows.Forms.Button();
            this.cmd_Cancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_Identificacion = new System.Windows.Forms.Label();
            this.pnl_certPago = new System.Windows.Forms.Panel();
            this.CMD_ImprimirCerti = new System.Windows.Forms.Button();
            this.pnl_certPagContain = new System.Windows.Forms.Panel();
            this.rTxt_Certificacion = new System.Windows.Forms.RichTextBox();
            this.pnl_certPag_title = new System.Windows.Forms.Panel();
            this.lbl_certPag_title = new System.Windows.Forms.Label();
            this.CMD_Prioritario = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.ObjPr_timer = new System.Windows.Forms.Timer(this.components);
            this.ObjPr_timerHuella = new System.Windows.Forms.Timer(this.components);
            this.tmCierraNiegaTurno = new System.Windows.Forms.Timer(this.components);
            this.tmGeneral = new System.Windows.Forms.Timer(this.components);
            this.pnl_Sombras = new System.Windows.Forms.Panel();
            this.pnlSapo = new System.Windows.Forms.Panel();
            this.LBL_MODO = new System.Windows.Forms.Label();
            this.LblPr_version = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblMsjs = new System.Windows.Forms.Label();
            this.cbAutoupdate = new System.Windows.Forms.CheckBox();
            this.LblVersion = new System.Windows.Forms.Label();
            this.cbAutoplug = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.formatsComboBox = new System.Windows.Forms.ComboBox();
            this.biometricDevicePositionComboBox = new System.Windows.Forms.ComboBox();
            this.cbAutomatic = new System.Windows.Forms.CheckBox();
            this.biometricDeviceImpressionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.deviceTypesGroupBox = new System.Windows.Forms.GroupBox();
            this.captureDeviceCheckBox = new System.Windows.Forms.CheckBox();
            this.microphoneCheckBox = new System.Windows.Forms.CheckBox();
            this.anyCheckBox = new System.Windows.Forms.CheckBox();
            this.fScannerCheckBox = new System.Windows.Forms.CheckBox();
            this.cameraCheckBox = new System.Windows.Forms.CheckBox();
            this.irisScannerCheckBox = new System.Windows.Forms.CheckBox();
            this.fingerScannerCheckBox = new System.Windows.Forms.CheckBox();
            this.biometricDeviceCheckBox = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.deviceTreeView = new System.Windows.Forms.TreeView();
            this.devicePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.CMD_Cancelar = new System.Windows.Forms.Button();
            this.CMD_Regresar = new System.Windows.Forms.Button();
            this.pnl_Inicio = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cmd_NoAfiliado = new System.Windows.Forms.Button();
            this.cmd_Afiliado = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pnl_NoAfiliado = new System.Windows.Forms.Panel();
            this.cmd_Beneficiario = new System.Windows.Forms.Button();
            this.cmd_Apoderado = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.button21 = new System.Windows.Forms.Button();
            this.pnl_turnosTitle = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_TurnoTitle = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CMD_Tramite = new System.Windows.Forms.Button();
            this.CMD_Vivienda14 = new System.Windows.Forms.Button();
            this.CMD_Vivienda8 = new System.Windows.Forms.Button();
            this.CMD_Leasing = new System.Windows.Forms.Button();
            this.CMD_Heroes = new System.Windows.Forms.Button();
            this.CMD_Agenda_cita = new System.Windows.Forms.Button();
            this.CMD_Futuro = new System.Windows.Forms.Button();
            this.CMD_Cuenta = new System.Windows.Forms.Button();
            this.CMD_Atencion_cita = new System.Windows.Forms.Button();
            this.pnl_TurnosInfoAfiliado = new System.Windows.Forms.Panel();
            this.CMD_RAFONDOH = new System.Windows.Forms.Button();
            this.CMD_Turnofuturoafiliado = new System.Windows.Forms.Button();
            this.CMD_Turnopretramiteafiliado = new System.Windows.Forms.Button();
            this.CMD_turnoleasingafiliado = new System.Windows.Forms.Button();
            this.CDM_Turno8afiliado = new System.Windows.Forms.Button();
            this.CMD_Turno14afiliado = new System.Windows.Forms.Button();
            this.CMD_Turnotramiteafiliado = new System.Windows.Forms.Button();
            this.pnl_TurnosNAfiliados = new System.Windows.Forms.Panel();
            this.CMD_ViviendaNFuturo = new System.Windows.Forms.Button();
            this.CMD_ViviendaNFondo = new System.Windows.Forms.Button();
            this.CMD_ViviendaN14 = new System.Windows.Forms.Button();
            this.CMD_ViviendaN8 = new System.Windows.Forms.Button();
            this.CMD_ViviendaNLeasing = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblnoafi = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.CMD_RadiNFuturo = new System.Windows.Forms.Button();
            this.CMD_RadiNFondo = new System.Windows.Forms.Button();
            this.CMD_RadiViviendaN14 = new System.Windows.Forms.Button();
            this.CMD_RadiViviendaN8 = new System.Windows.Forms.Button();
            this.CMD_RadiViviendaNLeasing = new System.Windows.Forms.Button();
            this.pnlTurnosBeneficiarios = new System.Windows.Forms.Panel();
            this.CMD_IBFUTURO = new System.Windows.Forms.Button();
            this.CMD_IBFONDO = new System.Windows.Forms.Button();
            this.CMD_IBCATORCE = new System.Windows.Forms.Button();
            this.CMD_IBOCHO = new System.Windows.Forms.Button();
            this.CMD_IBLEASING = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.CMD_RBFUTURO = new System.Windows.Forms.Button();
            this.CMD_RBFONDO = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.CMD_RBOCHO = new System.Windows.Forms.Button();
            this.CMD_BRLEASING = new System.Windows.Forms.Button();
            this.pnl_decla = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmd_imprimir_dela = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.CMD_imprimirEstTramite = new System.Windows.Forms.Button();
            this.pnl_estadoTramitTitle = new System.Windows.Forms.Panel();
            this.lbl_EstadoTramite = new System.Windows.Forms.Label();
            this.pnl_ESTADO_TRAMITE = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rTxt_Tramites = new System.Windows.Forms.RichTextBox();
            this.pnl_infoAfiliado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_Fotografia)).BeginInit();
            this.pnl_huella.SuspendLayout();
            this.pnl_vistaHuella.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_HuellaEstatica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_HuelaPrelim)).BeginInit();
            this.panel4.SuspendLayout();
            this.pnl_EstadoCuenta.SuspendLayout();
            this.pnl_EstadoCuenta1.SuspendLayout();
            this.pnl_TituloCuentas.SuspendLayout();
            this.pnl_Turno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_Logo)).BeginInit();
            this.pnl_teclado.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_certPago.SuspendLayout();
            this.pnl_certPagContain.SuspendLayout();
            this.pnl_certPag_title.SuspendLayout();
            this.pnlSapo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.deviceTypesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_Inicio.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnl_NoAfiliado.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnl_turnosTitle.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_TurnosInfoAfiliado.SuspendLayout();
            this.pnl_TurnosNAfiliados.SuspendLayout();
            this.pnlTurnosBeneficiarios.SuspendLayout();
            this.pnl_decla.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            this.pnl_estadoTramitTitle.SuspendLayout();
            this.pnl_ESTADO_TRAMITE.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // CMD_TurnosRadicacion
            // 
            this.CMD_TurnosRadicacion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_TurnosRadicacion.BackgroundImage")));
            this.CMD_TurnosRadicacion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_TurnosRadicacion.Enabled = false;
            this.CMD_TurnosRadicacion.FlatAppearance.BorderSize = 0;
            this.CMD_TurnosRadicacion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_TurnosRadicacion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_TurnosRadicacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_TurnosRadicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_TurnosRadicacion.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_TurnosRadicacion.Location = new System.Drawing.Point(629, 67);
            this.CMD_TurnosRadicacion.Name = "CMD_TurnosRadicacion";
            this.CMD_TurnosRadicacion.Size = new System.Drawing.Size(337, 64);
            this.CMD_TurnosRadicacion.TabIndex = 5;
            this.CMD_TurnosRadicacion.Text = "RADICACIÓN";
            this.CMD_TurnosRadicacion.UseVisualStyleBackColor = true;
            this.CMD_TurnosRadicacion.Click += new System.EventHandler(this.CMD_TurnosRadicacion_Click);
            // 
            // CMD_Turnos
            // 
            this.CMD_Turnos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Turnos.BackgroundImage")));
            this.CMD_Turnos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Turnos.Enabled = false;
            this.CMD_Turnos.FlatAppearance.BorderSize = 0;
            this.CMD_Turnos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Turnos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Turnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Turnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Turnos.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Turnos.Location = new System.Drawing.Point(1049, 66);
            this.CMD_Turnos.Name = "CMD_Turnos";
            this.CMD_Turnos.Size = new System.Drawing.Size(310, 64);
            this.CMD_Turnos.TabIndex = 4;
            this.CMD_Turnos.Text = "INFORMACIÓN GENERAL";
            this.CMD_Turnos.UseVisualStyleBackColor = true;
            this.CMD_Turnos.Click += new System.EventHandler(this.CMD_Turnos_Click);
            // 
            // cmd_consulta
            // 
            this.cmd_consulta.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmd_consulta.BackgroundImage")));
            this.cmd_consulta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_consulta.Enabled = false;
            this.cmd_consulta.FlatAppearance.BorderSize = 0;
            this.cmd_consulta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.cmd_consulta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_consulta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_consulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_consulta.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_consulta.Location = new System.Drawing.Point(196, 67);
            this.cmd_consulta.Name = "cmd_consulta";
            this.cmd_consulta.Size = new System.Drawing.Size(338, 64);
            this.cmd_consulta.TabIndex = 3;
            this.cmd_consulta.Text = "CERTIFICACIONES ";
            this.cmd_consulta.UseVisualStyleBackColor = true;
            this.cmd_consulta.Click += new System.EventHandler(this.cmd_consulta_Click);
            // 
            // pnl_infoAfiliado
            // 
            this.pnl_infoAfiliado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(103)))), ((int)(((byte)(103)))));
            this.pnl_infoAfiliado.Controls.Add(this.lbl_Cedula);
            this.pnl_infoAfiliado.Controls.Add(this.lbl_Nombres);
            this.pnl_infoAfiliado.Controls.Add(this.ptb_Fotografia);
            this.pnl_infoAfiliado.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pnl_infoAfiliado.Location = new System.Drawing.Point(11, 567);
            this.pnl_infoAfiliado.Name = "pnl_infoAfiliado";
            this.pnl_infoAfiliado.Size = new System.Drawing.Size(481, 254);
            this.pnl_infoAfiliado.TabIndex = 1;
            // 
            // lbl_Cedula
            // 
            this.lbl_Cedula.AutoSize = true;
            this.lbl_Cedula.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Cedula.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_Cedula.Location = new System.Drawing.Point(12, 221);
            this.lbl_Cedula.Name = "lbl_Cedula";
            this.lbl_Cedula.Size = new System.Drawing.Size(60, 13);
            this.lbl_Cedula.TabIndex = 2;
            this.lbl_Cedula.Text = "CEDULA:";
            this.lbl_Cedula.Visible = false;
            // 
            // lbl_Nombres
            // 
            this.lbl_Nombres.AutoSize = true;
            this.lbl_Nombres.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Nombres.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbl_Nombres.Location = new System.Drawing.Point(94, 221);
            this.lbl_Nombres.Name = "lbl_Nombres";
            this.lbl_Nombres.Size = new System.Drawing.Size(50, 13);
            this.lbl_Nombres.TabIndex = 1;
            this.lbl_Nombres.Text = "Nombre";
            // 
            // ptb_Fotografia
            // 
            this.ptb_Fotografia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ptb_Fotografia.BackColor = System.Drawing.Color.White;
            this.ptb_Fotografia.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.ptb_Fotografia.Location = new System.Drawing.Point(3, 3);
            this.ptb_Fotografia.Name = "ptb_Fotografia";
            this.ptb_Fotografia.Size = new System.Drawing.Size(475, 248);
            this.ptb_Fotografia.TabIndex = 0;
            this.ptb_Fotografia.TabStop = false;
            // 
            // pnl_huella
            // 
            this.pnl_huella.BackColor = System.Drawing.Color.Transparent;
            this.pnl_huella.Controls.Add(this.pnl_vistaHuella);
            this.pnl_huella.Controls.Add(this.lbl_respuesta);
            this.pnl_huella.Controls.Add(this.panel4);
            this.pnl_huella.Location = new System.Drawing.Point(208, 174);
            this.pnl_huella.Name = "pnl_huella";
            this.pnl_huella.Size = new System.Drawing.Size(392, 516);
            this.pnl_huella.TabIndex = 4;
            this.pnl_huella.Visible = false;
            // 
            // pnl_vistaHuella
            // 
            this.pnl_vistaHuella.AutoSize = true;
            this.pnl_vistaHuella.Controls.Add(this.ptb_HuellaEstatica);
            this.pnl_vistaHuella.Controls.Add(this.ptb_HuelaPrelim);
            this.pnl_vistaHuella.Location = new System.Drawing.Point(77, 169);
            this.pnl_vistaHuella.Name = "pnl_vistaHuella";
            this.pnl_vistaHuella.Size = new System.Drawing.Size(280, 324);
            this.pnl_vistaHuella.TabIndex = 1;
            // 
            // ptb_HuellaEstatica
            // 
            this.ptb_HuellaEstatica.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ptb_HuellaEstatica.BackgroundImage")));
            this.ptb_HuellaEstatica.Image = ((System.Drawing.Image)(resources.GetObject("ptb_HuellaEstatica.Image")));
            this.ptb_HuellaEstatica.Location = new System.Drawing.Point(17, 9);
            this.ptb_HuellaEstatica.Name = "ptb_HuellaEstatica";
            this.ptb_HuellaEstatica.Size = new System.Drawing.Size(256, 303);
            this.ptb_HuellaEstatica.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptb_HuellaEstatica.TabIndex = 3;
            this.ptb_HuellaEstatica.TabStop = false;
            this.ptb_HuellaEstatica.Visible = false;
            // 
            // ptb_HuelaPrelim
            // 
            this.ptb_HuelaPrelim.Location = new System.Drawing.Point(17, 9);
            this.ptb_HuelaPrelim.Name = "ptb_HuelaPrelim";
            this.ptb_HuelaPrelim.Size = new System.Drawing.Size(256, 304);
            this.ptb_HuelaPrelim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptb_HuelaPrelim.TabIndex = 0;
            this.ptb_HuelaPrelim.TabStop = false;
            // 
            // lbl_respuesta
            // 
            this.lbl_respuesta.AutoSize = true;
            this.lbl_respuesta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_respuesta.ForeColor = System.Drawing.Color.LightGray;
            this.lbl_respuesta.Location = new System.Drawing.Point(43, 107);
            this.lbl_respuesta.Name = "lbl_respuesta";
            this.lbl_respuesta.Size = new System.Drawing.Size(0, 20);
            this.lbl_respuesta.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(0, -9);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(487, 109);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 32.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 108);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CMD_RENTA
            // 
            this.CMD_RENTA.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RENTA.BackgroundImage")));
            this.CMD_RENTA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RENTA.FlatAppearance.BorderSize = 0;
            this.CMD_RENTA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_RENTA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_RENTA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RENTA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RENTA.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RENTA.Location = new System.Drawing.Point(196, 374);
            this.CMD_RENTA.Name = "CMD_RENTA";
            this.CMD_RENTA.Size = new System.Drawing.Size(338, 63);
            this.CMD_RENTA.TabIndex = 5;
            this.CMD_RENTA.Text = "CERTIFICADO DECLARACIÓN DE RENTA";
            this.CMD_RENTA.UseVisualStyleBackColor = true;
            this.CMD_RENTA.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // CMD_Certificado
            // 
            this.CMD_Certificado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Certificado.BackgroundImage")));
            this.CMD_Certificado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Certificado.FlatAppearance.BorderSize = 0;
            this.CMD_Certificado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Certificado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Certificado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Certificado.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.CMD_Certificado.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Certificado.Location = new System.Drawing.Point(196, 222);
            this.CMD_Certificado.Name = "CMD_Certificado";
            this.CMD_Certificado.Size = new System.Drawing.Size(338, 63);
            this.CMD_Certificado.TabIndex = 4;
            this.CMD_Certificado.Text = "CERTIFICADO PAGO";
            this.CMD_Certificado.UseVisualStyleBackColor = true;
            this.CMD_Certificado.Click += new System.EventHandler(this.CMD_Certificado_Click);
            // 
            // CMD_EstadoTramite
            // 
            this.CMD_EstadoTramite.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_EstadoTramite.BackgroundImage")));
            this.CMD_EstadoTramite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_EstadoTramite.FlatAppearance.BorderSize = 0;
            this.CMD_EstadoTramite.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_EstadoTramite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_EstadoTramite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_EstadoTramite.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.CMD_EstadoTramite.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_EstadoTramite.Location = new System.Drawing.Point(196, 293);
            this.CMD_EstadoTramite.Name = "CMD_EstadoTramite";
            this.CMD_EstadoTramite.Size = new System.Drawing.Size(338, 63);
            this.CMD_EstadoTramite.TabIndex = 3;
            this.CMD_EstadoTramite.Text = "ESTADO TRAMITE";
            this.CMD_EstadoTramite.UseVisualStyleBackColor = true;
            this.CMD_EstadoTramite.Click += new System.EventHandler(this.CMD_EstadoTramite_Click);
            // 
            // CMD_EstadCuenta
            // 
            this.CMD_EstadCuenta.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_EstadCuenta.BackgroundImage")));
            this.CMD_EstadCuenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_EstadCuenta.FlatAppearance.BorderSize = 0;
            this.CMD_EstadCuenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_EstadCuenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_EstadCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_EstadCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_EstadCuenta.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_EstadCuenta.Location = new System.Drawing.Point(196, 144);
            this.CMD_EstadCuenta.Name = "CMD_EstadCuenta";
            this.CMD_EstadCuenta.Size = new System.Drawing.Size(338, 63);
            this.CMD_EstadCuenta.TabIndex = 2;
            this.CMD_EstadCuenta.Text = "ESTADO CUENTA";
            this.CMD_EstadCuenta.UseVisualStyleBackColor = true;
            this.CMD_EstadCuenta.Click += new System.EventHandler(this.CMD_EstadCuenta_Click);
            // 
            // pnl_EstadoCuenta
            // 
            this.pnl_EstadoCuenta.BackColor = System.Drawing.Color.Transparent;
            this.pnl_EstadoCuenta.Controls.Add(this.pnl_EstadoCuenta1);
            this.pnl_EstadoCuenta.Controls.Add(this.CMD_ImprimirEstado);
            this.pnl_EstadoCuenta.Controls.Add(this.pnl_TituloCuentas);
            this.pnl_EstadoCuenta.Location = new System.Drawing.Point(9, 153);
            this.pnl_EstadoCuenta.Name = "pnl_EstadoCuenta";
            this.pnl_EstadoCuenta.Size = new System.Drawing.Size(584, 717);
            this.pnl_EstadoCuenta.TabIndex = 6;
            this.pnl_EstadoCuenta.Visible = false;
            this.pnl_EstadoCuenta.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_EstadoCuenta_Paint);
            // 
            // pnl_EstadoCuenta1
            // 
            this.pnl_EstadoCuenta1.BackColor = System.Drawing.Color.Silver;
            this.pnl_EstadoCuenta1.Controls.Add(this.webBrowser3);
            this.pnl_EstadoCuenta1.Controls.Add(this.lbl_esta_cuentas_1);
            this.pnl_EstadoCuenta1.Location = new System.Drawing.Point(14, 72);
            this.pnl_EstadoCuenta1.Name = "pnl_EstadoCuenta1";
            this.pnl_EstadoCuenta1.Size = new System.Drawing.Size(524, 552);
            this.pnl_EstadoCuenta1.TabIndex = 3;
            // 
            // webBrowser3
            // 
            this.webBrowser3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser3.Location = new System.Drawing.Point(0, 0);
            this.webBrowser3.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser3.Name = "webBrowser3";
            this.webBrowser3.Size = new System.Drawing.Size(524, 552);
            this.webBrowser3.TabIndex = 20;
            // 
            // lbl_esta_cuentas_1
            // 
            this.lbl_esta_cuentas_1.AutoSize = true;
            this.lbl_esta_cuentas_1.Location = new System.Drawing.Point(12, 71);
            this.lbl_esta_cuentas_1.Name = "lbl_esta_cuentas_1";
            this.lbl_esta_cuentas_1.Size = new System.Drawing.Size(0, 13);
            this.lbl_esta_cuentas_1.TabIndex = 3;
            // 
            // CMD_ImprimirEstado
            // 
            this.CMD_ImprimirEstado.BackColor = System.Drawing.Color.Transparent;
            this.CMD_ImprimirEstado.BackgroundImage = global::FENIX_KIOSCO.Properties.Resources.B_Menu;
            this.CMD_ImprimirEstado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_ImprimirEstado.FlatAppearance.BorderSize = 0;
            this.CMD_ImprimirEstado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_ImprimirEstado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_ImprimirEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_ImprimirEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.CMD_ImprimirEstado.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_ImprimirEstado.Location = new System.Drawing.Point(188, 630);
            this.CMD_ImprimirEstado.Name = "CMD_ImprimirEstado";
            this.CMD_ImprimirEstado.Size = new System.Drawing.Size(192, 70);
            this.CMD_ImprimirEstado.TabIndex = 2;
            this.CMD_ImprimirEstado.Text = "IMPRIMIR";
            this.CMD_ImprimirEstado.UseVisualStyleBackColor = false;
            this.CMD_ImprimirEstado.Click += new System.EventHandler(this.CMD_ImprimirEstado_Click);
            // 
            // pnl_TituloCuentas
            // 
            this.pnl_TituloCuentas.BackColor = System.Drawing.Color.Transparent;
            this.pnl_TituloCuentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_TituloCuentas.Controls.Add(this.lbl_EstadoCuentas);
            this.pnl_TituloCuentas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_TituloCuentas.Location = new System.Drawing.Point(0, 0);
            this.pnl_TituloCuentas.Name = "pnl_TituloCuentas";
            this.pnl_TituloCuentas.Size = new System.Drawing.Size(584, 63);
            this.pnl_TituloCuentas.TabIndex = 0;
            // 
            // lbl_EstadoCuentas
            // 
            this.lbl_EstadoCuentas.AutoSize = true;
            this.lbl_EstadoCuentas.Font = new System.Drawing.Font("Modern No. 20", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EstadoCuentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_EstadoCuentas.Location = new System.Drawing.Point(89, 7);
            this.lbl_EstadoCuentas.Name = "lbl_EstadoCuentas";
            this.lbl_EstadoCuentas.Size = new System.Drawing.Size(332, 45);
            this.lbl_EstadoCuentas.TabIndex = 0;
            this.lbl_EstadoCuentas.Text = "Estado de Cuentas";
            // 
            // pnl_Turno
            // 
            this.pnl_Turno.BackColor = System.Drawing.Color.White;
            this.pnl_Turno.Controls.Add(this.lbl_ceduNAfi);
            this.pnl_Turno.Controls.Add(this.lbl_NombreNoAfi);
            this.pnl_Turno.Controls.Add(this.lbl_Alertas);
            this.pnl_Turno.Controls.Add(this.lbl_HoraTurno);
            this.pnl_Turno.Controls.Add(this.lbl_tipoServicio);
            this.pnl_Turno.Controls.Add(this.lbl_ceduAfi);
            this.pnl_Turno.Controls.Add(this.lbl_NombreAfi);
            this.pnl_Turno.Controls.Add(this.lbl_Turno);
            this.pnl_Turno.Controls.Add(this.ptb_Logo);
            this.pnl_Turno.Controls.Add(this.lbl_TipoTurno);
            this.pnl_Turno.Location = new System.Drawing.Point(6, 751);
            this.pnl_Turno.Name = "pnl_Turno";
            this.pnl_Turno.Size = new System.Drawing.Size(430, 315);
            this.pnl_Turno.TabIndex = 0;
            this.pnl_Turno.Visible = false;
            this.pnl_Turno.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Turno_Paint);
            // 
            // lbl_ceduNAfi
            // 
            this.lbl_ceduNAfi.AutoSize = true;
            this.lbl_ceduNAfi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ceduNAfi.Location = new System.Drawing.Point(20, 151);
            this.lbl_ceduNAfi.Name = "lbl_ceduNAfi";
            this.lbl_ceduNAfi.Size = new System.Drawing.Size(60, 13);
            this.lbl_ceduNAfi.TabIndex = 9;
            this.lbl_ceduNAfi.Text = "CEDULA:";
            // 
            // lbl_NombreNoAfi
            // 
            this.lbl_NombreNoAfi.AutoSize = true;
            this.lbl_NombreNoAfi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NombreNoAfi.Location = new System.Drawing.Point(20, 122);
            this.lbl_NombreNoAfi.Name = "lbl_NombreNoAfi";
            this.lbl_NombreNoAfi.Size = new System.Drawing.Size(101, 18);
            this.lbl_NombreNoAfi.TabIndex = 8;
            this.lbl_NombreNoAfi.Text = "No Afiliado(a):";
            // 
            // lbl_Alertas
            // 
            this.lbl_Alertas.AutoSize = true;
            this.lbl_Alertas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Alertas.Location = new System.Drawing.Point(17, 211);
            this.lbl_Alertas.Name = "lbl_Alertas";
            this.lbl_Alertas.Size = new System.Drawing.Size(31, 13);
            this.lbl_Alertas.TabIndex = 7;
            this.lbl_Alertas.Text = "____";
            // 
            // lbl_HoraTurno
            // 
            this.lbl_HoraTurno.AutoSize = true;
            this.lbl_HoraTurno.Location = new System.Drawing.Point(141, 130);
            this.lbl_HoraTurno.Name = "lbl_HoraTurno";
            this.lbl_HoraTurno.Size = new System.Drawing.Size(41, 13);
            this.lbl_HoraTurno.TabIndex = 6;
            this.lbl_HoraTurno.Text = "HORA:";
            // 
            // lbl_tipoServicio
            // 
            this.lbl_tipoServicio.AutoSize = true;
            this.lbl_tipoServicio.BackColor = System.Drawing.Color.Transparent;
            this.lbl_tipoServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tipoServicio.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_tipoServicio.Location = new System.Drawing.Point(19, 44);
            this.lbl_tipoServicio.Name = "lbl_tipoServicio";
            this.lbl_tipoServicio.Size = new System.Drawing.Size(254, 24);
            this.lbl_tipoServicio.TabIndex = 5;
            this.lbl_tipoServicio.Text = "ORIENTACION GENERAL";
            // 
            // lbl_ceduAfi
            // 
            this.lbl_ceduAfi.AutoSize = true;
            this.lbl_ceduAfi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ceduAfi.Location = new System.Drawing.Point(20, 198);
            this.lbl_ceduAfi.Name = "lbl_ceduAfi";
            this.lbl_ceduAfi.Size = new System.Drawing.Size(60, 13);
            this.lbl_ceduAfi.TabIndex = 4;
            this.lbl_ceduAfi.Text = "CEDULA:";
            // 
            // lbl_NombreAfi
            // 
            this.lbl_NombreAfi.AutoSize = true;
            this.lbl_NombreAfi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NombreAfi.Location = new System.Drawing.Point(20, 173);
            this.lbl_NombreAfi.Name = "lbl_NombreAfi";
            this.lbl_NombreAfi.Size = new System.Drawing.Size(77, 18);
            this.lbl_NombreAfi.TabIndex = 3;
            this.lbl_NombreAfi.Text = "Afiliado(a):";
            // 
            // lbl_Turno
            // 
            this.lbl_Turno.AutoSize = true;
            this.lbl_Turno.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Turno.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Turno.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Turno.Location = new System.Drawing.Point(11, 86);
            this.lbl_Turno.Name = "lbl_Turno";
            this.lbl_Turno.Size = new System.Drawing.Size(133, 33);
            this.lbl_Turno.TabIndex = 0;
            this.lbl_Turno.Text = "TURNO:";
            // 
            // ptb_Logo
            // 
            this.ptb_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ptb_Logo.Image = ((System.Drawing.Image)(resources.GetObject("ptb_Logo.Image")));
            this.ptb_Logo.Location = new System.Drawing.Point(11, 231);
            this.ptb_Logo.Name = "ptb_Logo";
            this.ptb_Logo.Size = new System.Drawing.Size(211, 40);
            this.ptb_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptb_Logo.TabIndex = 2;
            this.ptb_Logo.TabStop = false;
            // 
            // lbl_TipoTurno
            // 
            this.lbl_TipoTurno.AutoSize = true;
            this.lbl_TipoTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TipoTurno.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_TipoTurno.Location = new System.Drawing.Point(168, 81);
            this.lbl_TipoTurno.Name = "lbl_TipoTurno";
            this.lbl_TipoTurno.Size = new System.Drawing.Size(74, 42);
            this.lbl_TipoTurno.TabIndex = 0;
            this.lbl_TipoTurno.Text = "IF1";
            // 
            // pnl_teclado
            // 
            this.pnl_teclado.BackColor = System.Drawing.Color.Transparent;
            this.pnl_teclado.Controls.Add(this.button1);
            this.pnl_teclado.Controls.Add(this.txt_Identificacion);
            this.pnl_teclado.Controls.Add(this.cmd_3);
            this.pnl_teclado.Controls.Add(this.cmd_2);
            this.pnl_teclado.Controls.Add(this.cmd_1);
            this.pnl_teclado.Controls.Add(this.cmd_6);
            this.pnl_teclado.Controls.Add(this.cmd_5);
            this.pnl_teclado.Controls.Add(this.cmd_4);
            this.pnl_teclado.Controls.Add(this.cmd_9);
            this.pnl_teclado.Controls.Add(this.cmd_8);
            this.pnl_teclado.Controls.Add(this.cmd_7);
            this.pnl_teclado.Controls.Add(this.cmd_Accept);
            this.pnl_teclado.Controls.Add(this.cmd_0);
            this.pnl_teclado.Controls.Add(this.cmd_Cancel);
            this.pnl_teclado.Controls.Add(this.panel2);
            this.pnl_teclado.Location = new System.Drawing.Point(550, 144);
            this.pnl_teclado.MaximumSize = new System.Drawing.Size(412, 640);
            this.pnl_teclado.Name = "pnl_teclado";
            this.pnl_teclado.Size = new System.Drawing.Size(412, 640);
            this.pnl_teclado.TabIndex = 1;
            this.pnl_teclado.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::FENIX_KIOSCO.Properties.Resources.Boton_Regresar;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(318, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 57);
            this.button1.TabIndex = 15;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txt_Identificacion
            // 
            this.txt_Identificacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Identificacion.BackColor = System.Drawing.Color.White;
            this.txt_Identificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Identificacion.ForeColor = System.Drawing.Color.Black;
            this.txt_Identificacion.Location = new System.Drawing.Point(46, 100);
            this.txt_Identificacion.MaximumSize = new System.Drawing.Size(320, 40);
            this.txt_Identificacion.Name = "txt_Identificacion";
            this.txt_Identificacion.Size = new System.Drawing.Size(278, 40);
            this.txt_Identificacion.TabIndex = 13;
            this.txt_Identificacion.Text = "Cédula...";
            // 
            // cmd_3
            // 
            this.cmd_3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_3.AutoSize = true;
            this.cmd_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_3.FlatAppearance.BorderSize = 0;
            this.cmd_3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmd_3.Image = global::FENIX_KIOSCO.Properties.Resources.B3_UP;
            this.cmd_3.Location = new System.Drawing.Point(272, 164);
            this.cmd_3.Name = "cmd_3";
            this.cmd_3.Size = new System.Drawing.Size(119, 119);
            this.cmd_3.TabIndex = 12;
            this.cmd_3.UseVisualStyleBackColor = true;
            this.cmd_3.Click += new System.EventHandler(this.button10_Click);
            this.cmd_3.MouseEnter += new System.EventHandler(this.cmd_3_MouseEnter);
            this.cmd_3.MouseLeave += new System.EventHandler(this.cmd_3_MouseLeave);
            // 
            // cmd_2
            // 
            this.cmd_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_2.AutoSize = true;
            this.cmd_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_2.FlatAppearance.BorderSize = 0;
            this.cmd_2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmd_2.Image = global::FENIX_KIOSCO.Properties.Resources.B2_UP;
            this.cmd_2.Location = new System.Drawing.Point(153, 164);
            this.cmd_2.Name = "cmd_2";
            this.cmd_2.Size = new System.Drawing.Size(119, 119);
            this.cmd_2.TabIndex = 11;
            this.cmd_2.UseVisualStyleBackColor = true;
            this.cmd_2.Click += new System.EventHandler(this.button11_Click);
            this.cmd_2.MouseEnter += new System.EventHandler(this.cmd_2_MouseEnter);
            this.cmd_2.MouseLeave += new System.EventHandler(this.cmd_2_MouseLeave);
            // 
            // cmd_1
            // 
            this.cmd_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_1.AutoSize = true;
            this.cmd_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_1.FlatAppearance.BorderSize = 0;
            this.cmd_1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmd_1.Image = global::FENIX_KIOSCO.Properties.Resources.B1_UP;
            this.cmd_1.Location = new System.Drawing.Point(34, 164);
            this.cmd_1.Name = "cmd_1";
            this.cmd_1.Size = new System.Drawing.Size(123, 119);
            this.cmd_1.TabIndex = 10;
            this.cmd_1.UseVisualStyleBackColor = true;
            this.cmd_1.Click += new System.EventHandler(this.button12_Click);
            this.cmd_1.MouseEnter += new System.EventHandler(this.cmd_1_MouseEnter);
            this.cmd_1.MouseLeave += new System.EventHandler(this.cmd_1_MouseLeave);
            // 
            // cmd_6
            // 
            this.cmd_6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_6.AutoSize = true;
            this.cmd_6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_6.FlatAppearance.BorderSize = 0;
            this.cmd_6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmd_6.Image = global::FENIX_KIOSCO.Properties.Resources.B6_UP;
            this.cmd_6.Location = new System.Drawing.Point(272, 281);
            this.cmd_6.Name = "cmd_6";
            this.cmd_6.Size = new System.Drawing.Size(119, 119);
            this.cmd_6.TabIndex = 9;
            this.cmd_6.UseVisualStyleBackColor = true;
            this.cmd_6.Click += new System.EventHandler(this.button7_Click);
            this.cmd_6.MouseEnter += new System.EventHandler(this.cmd_6_MouseEnter);
            this.cmd_6.MouseLeave += new System.EventHandler(this.cmd_6_MouseLeave);
            // 
            // cmd_5
            // 
            this.cmd_5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_5.AutoSize = true;
            this.cmd_5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_5.FlatAppearance.BorderSize = 0;
            this.cmd_5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmd_5.Image = global::FENIX_KIOSCO.Properties.Resources.B5_UP;
            this.cmd_5.Location = new System.Drawing.Point(153, 281);
            this.cmd_5.Name = "cmd_5";
            this.cmd_5.Size = new System.Drawing.Size(119, 119);
            this.cmd_5.TabIndex = 8;
            this.cmd_5.UseVisualStyleBackColor = true;
            this.cmd_5.Click += new System.EventHandler(this.button8_Click);
            this.cmd_5.MouseEnter += new System.EventHandler(this.cmd_5_MouseEnter);
            this.cmd_5.MouseLeave += new System.EventHandler(this.cmd_5_MouseLeave);
            // 
            // cmd_4
            // 
            this.cmd_4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_4.AutoSize = true;
            this.cmd_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_4.FlatAppearance.BorderSize = 0;
            this.cmd_4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmd_4.Image = global::FENIX_KIOSCO.Properties.Resources.B4_UP;
            this.cmd_4.Location = new System.Drawing.Point(34, 281);
            this.cmd_4.Name = "cmd_4";
            this.cmd_4.Size = new System.Drawing.Size(119, 119);
            this.cmd_4.TabIndex = 7;
            this.cmd_4.UseVisualStyleBackColor = true;
            this.cmd_4.Click += new System.EventHandler(this.button9_Click);
            this.cmd_4.MouseEnter += new System.EventHandler(this.cmd_4_MouseEnter);
            this.cmd_4.MouseLeave += new System.EventHandler(this.cmd_4_MouseLeave);
            // 
            // cmd_9
            // 
            this.cmd_9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_9.AutoSize = true;
            this.cmd_9.BackColor = System.Drawing.Color.Transparent;
            this.cmd_9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_9.FlatAppearance.BorderSize = 0;
            this.cmd_9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_9.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_9.Image = global::FENIX_KIOSCO.Properties.Resources.B9_UP;
            this.cmd_9.Location = new System.Drawing.Point(272, 398);
            this.cmd_9.Name = "cmd_9";
            this.cmd_9.Size = new System.Drawing.Size(119, 119);
            this.cmd_9.TabIndex = 6;
            this.cmd_9.UseVisualStyleBackColor = false;
            this.cmd_9.Click += new System.EventHandler(this.button4_Click);
            this.cmd_9.MouseEnter += new System.EventHandler(this.cmd_9_MouseEnter);
            this.cmd_9.MouseLeave += new System.EventHandler(this.cmd_9_MouseLeave);
            // 
            // cmd_8
            // 
            this.cmd_8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_8.AutoSize = true;
            this.cmd_8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_8.FlatAppearance.BorderSize = 0;
            this.cmd_8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_8.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_8.Image = global::FENIX_KIOSCO.Properties.Resources.B8_UP;
            this.cmd_8.Location = new System.Drawing.Point(153, 398);
            this.cmd_8.Name = "cmd_8";
            this.cmd_8.Size = new System.Drawing.Size(119, 119);
            this.cmd_8.TabIndex = 5;
            this.cmd_8.UseVisualStyleBackColor = true;
            this.cmd_8.Click += new System.EventHandler(this.button5_Click);
            this.cmd_8.MouseEnter += new System.EventHandler(this.cmd_8_MouseEnter);
            this.cmd_8.MouseLeave += new System.EventHandler(this.cmd_8_MouseLeave);
            // 
            // cmd_7
            // 
            this.cmd_7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_7.AutoSize = true;
            this.cmd_7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_7.FlatAppearance.BorderSize = 0;
            this.cmd_7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_7.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_7.Image = global::FENIX_KIOSCO.Properties.Resources.B7_UP;
            this.cmd_7.Location = new System.Drawing.Point(34, 398);
            this.cmd_7.Name = "cmd_7";
            this.cmd_7.Size = new System.Drawing.Size(119, 119);
            this.cmd_7.TabIndex = 4;
            this.cmd_7.UseVisualStyleBackColor = true;
            this.cmd_7.Click += new System.EventHandler(this.button6_Click);
            this.cmd_7.MouseEnter += new System.EventHandler(this.cmd_7_MouseEnter);
            this.cmd_7.MouseLeave += new System.EventHandler(this.cmd_7_MouseLeave);
            // 
            // cmd_Accept
            // 
            this.cmd_Accept.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_Accept.AutoSize = true;
            this.cmd_Accept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_Accept.Enabled = false;
            this.cmd_Accept.FlatAppearance.BorderSize = 0;
            this.cmd_Accept.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_Accept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_Accept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_Accept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_Accept.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmd_Accept.Image = ((System.Drawing.Image)(resources.GetObject("cmd_Accept.Image")));
            this.cmd_Accept.Location = new System.Drawing.Point(272, 516);
            this.cmd_Accept.Name = "cmd_Accept";
            this.cmd_Accept.Size = new System.Drawing.Size(119, 119);
            this.cmd_Accept.TabIndex = 3;
            this.cmd_Accept.UseVisualStyleBackColor = true;
            this.cmd_Accept.Click += new System.EventHandler(this.button3_Click);
            this.cmd_Accept.MouseEnter += new System.EventHandler(this.cmd_Accept_MouseEnter);
            this.cmd_Accept.MouseLeave += new System.EventHandler(this.cmd_Accept_MouseLeave);
            // 
            // cmd_0
            // 
            this.cmd_0.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_0.AutoSize = true;
            this.cmd_0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_0.FlatAppearance.BorderSize = 0;
            this.cmd_0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_0.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_0.Image = global::FENIX_KIOSCO.Properties.Resources.B0_UP;
            this.cmd_0.Location = new System.Drawing.Point(153, 516);
            this.cmd_0.Name = "cmd_0";
            this.cmd_0.Size = new System.Drawing.Size(119, 119);
            this.cmd_0.TabIndex = 2;
            this.cmd_0.UseVisualStyleBackColor = true;
            this.cmd_0.Click += new System.EventHandler(this.button2_Click);
            this.cmd_0.MouseEnter += new System.EventHandler(this.cmd_0_MouseEnter);
            this.cmd_0.MouseLeave += new System.EventHandler(this.cmd_0_MouseLeave);
            // 
            // cmd_Cancel
            // 
            this.cmd_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmd_Cancel.AutoSize = true;
            this.cmd_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_Cancel.FlatAppearance.BorderSize = 0;
            this.cmd_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmd_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_Cancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cmd_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("cmd_Cancel.Image")));
            this.cmd_Cancel.Location = new System.Drawing.Point(34, 516);
            this.cmd_Cancel.Name = "cmd_Cancel";
            this.cmd_Cancel.Size = new System.Drawing.Size(119, 119);
            this.cmd_Cancel.TabIndex = 1;
            this.cmd_Cancel.UseVisualStyleBackColor = true;
            this.cmd_Cancel.Click += new System.EventHandler(this.button1_Click);
            this.cmd_Cancel.MouseEnter += new System.EventHandler(this.cmd_Cancel_MouseEnter);
            this.cmd_Cancel.MouseLeave += new System.EventHandler(this.cmd_Cancel_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.lbl_Identificacion);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.MaximumSize = new System.Drawing.Size(412, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(412, 93);
            this.panel2.TabIndex = 0;
            // 
            // lbl_Identificacion
            // 
            this.lbl_Identificacion.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Identificacion.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Identificacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_Identificacion.Location = new System.Drawing.Point(3, 3);
            this.lbl_Identificacion.Name = "lbl_Identificacion";
            this.lbl_Identificacion.Size = new System.Drawing.Size(403, 87);
            this.lbl_Identificacion.TabIndex = 0;
            this.lbl_Identificacion.Text = "Identificación";
            this.lbl_Identificacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_certPago
            // 
            this.pnl_certPago.BackColor = System.Drawing.Color.Transparent;
            this.pnl_certPago.Controls.Add(this.CMD_ImprimirCerti);
            this.pnl_certPago.Controls.Add(this.pnl_certPagContain);
            this.pnl_certPago.Controls.Add(this.pnl_certPag_title);
            this.pnl_certPago.Location = new System.Drawing.Point(3, 372);
            this.pnl_certPago.Name = "pnl_certPago";
            this.pnl_certPago.Size = new System.Drawing.Size(245, 284);
            this.pnl_certPago.TabIndex = 11;
            this.pnl_certPago.Visible = false;
            // 
            // CMD_ImprimirCerti
            // 
            this.CMD_ImprimirCerti.BackColor = System.Drawing.Color.Transparent;
            this.CMD_ImprimirCerti.BackgroundImage = global::FENIX_KIOSCO.Properties.Resources.B_Menu;
            this.CMD_ImprimirCerti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_ImprimirCerti.FlatAppearance.BorderSize = 0;
            this.CMD_ImprimirCerti.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_ImprimirCerti.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_ImprimirCerti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_ImprimirCerti.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_ImprimirCerti.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_ImprimirCerti.Location = new System.Drawing.Point(132, 525);
            this.CMD_ImprimirCerti.Name = "CMD_ImprimirCerti";
            this.CMD_ImprimirCerti.Size = new System.Drawing.Size(163, 68);
            this.CMD_ImprimirCerti.TabIndex = 5;
            this.CMD_ImprimirCerti.Text = "IMPRIMIR";
            this.CMD_ImprimirCerti.UseVisualStyleBackColor = false;
            this.CMD_ImprimirCerti.Click += new System.EventHandler(this.CMD_ImprimirCerti_Click);
            // 
            // pnl_certPagContain
            // 
            this.pnl_certPagContain.BackColor = System.Drawing.Color.Silver;
            this.pnl_certPagContain.Controls.Add(this.rTxt_Certificacion);
            this.pnl_certPagContain.Location = new System.Drawing.Point(14, 87);
            this.pnl_certPagContain.Name = "pnl_certPagContain";
            this.pnl_certPagContain.Size = new System.Drawing.Size(402, 432);
            this.pnl_certPagContain.TabIndex = 3;
            // 
            // rTxt_Certificacion
            // 
            this.rTxt_Certificacion.BackColor = System.Drawing.Color.Silver;
            this.rTxt_Certificacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rTxt_Certificacion.Location = new System.Drawing.Point(14, 15);
            this.rTxt_Certificacion.Name = "rTxt_Certificacion";
            this.rTxt_Certificacion.Size = new System.Drawing.Size(375, 399);
            this.rTxt_Certificacion.TabIndex = 5;
            this.rTxt_Certificacion.Text = "";
            // 
            // pnl_certPag_title
            // 
            this.pnl_certPag_title.BackColor = System.Drawing.Color.Transparent;
            this.pnl_certPag_title.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_certPag_title.Controls.Add(this.lbl_certPag_title);
            this.pnl_certPag_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_certPag_title.Location = new System.Drawing.Point(0, 0);
            this.pnl_certPag_title.Name = "pnl_certPag_title";
            this.pnl_certPag_title.Size = new System.Drawing.Size(245, 63);
            this.pnl_certPag_title.TabIndex = 2;
            // 
            // lbl_certPag_title
            // 
            this.lbl_certPag_title.AutoSize = true;
            this.lbl_certPag_title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_certPag_title.Font = new System.Drawing.Font("Modern No. 20", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_certPag_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_certPag_title.Location = new System.Drawing.Point(15, 7);
            this.lbl_certPag_title.Name = "lbl_certPag_title";
            this.lbl_certPag_title.Size = new System.Drawing.Size(383, 45);
            this.lbl_certPag_title.TabIndex = 0;
            this.lbl_certPag_title.Text = "Certificación de pago";
            // 
            // CMD_Prioritario
            // 
            this.CMD_Prioritario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CMD_Prioritario.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CMD_Prioritario.BackColor = System.Drawing.Color.Transparent;
            this.CMD_Prioritario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Prioritario.FlatAppearance.BorderSize = 0;
            this.CMD_Prioritario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CMD_Prioritario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Prioritario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Prioritario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.CMD_Prioritario.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Prioritario.Image = global::FENIX_KIOSCO.Properties.Resources.Boton_Minusválido;
            this.CMD_Prioritario.Location = new System.Drawing.Point(0, 403);
            this.CMD_Prioritario.Name = "CMD_Prioritario";
            this.CMD_Prioritario.Size = new System.Drawing.Size(188, 406);
            this.CMD_Prioritario.TabIndex = 16;
            this.CMD_Prioritario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CMD_Prioritario.UseVisualStyleBackColor = false;
            this.CMD_Prioritario.Click += new System.EventHandler(this.CMD_Prioritario_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage_1);
            // 
            // printDocument2
            // 
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
            // 
            // ObjPr_timer
            // 
            this.ObjPr_timer.Interval = 20000;
            this.ObjPr_timer.Tick += new System.EventHandler(this.ObjPr_timer_Tick_1);
            // 
            // ObjPr_timerHuella
            // 
            this.ObjPr_timerHuella.Tick += new System.EventHandler(this.ObjPr_timerHuella_Tick_1);
            // 
            // tmCierraNiegaTurno
            // 
            this.tmCierraNiegaTurno.Interval = 6000;
            this.tmCierraNiegaTurno.Tick += new System.EventHandler(this.tmCierraNiegaTurno_Tick);
            // 
            // tmGeneral
            // 
            this.tmGeneral.Interval = 1000;
            this.tmGeneral.Tick += new System.EventHandler(this.tmGeneral_Tick);
            // 
            // pnl_Sombras
            // 
            this.pnl_Sombras.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Sombras.Location = new System.Drawing.Point(12, 266);
            this.pnl_Sombras.Name = "pnl_Sombras";
            this.pnl_Sombras.Size = new System.Drawing.Size(62, 99);
            this.pnl_Sombras.TabIndex = 9;
            // 
            // pnlSapo
            // 
            this.pnlSapo.BackColor = System.Drawing.Color.Transparent;
            this.pnlSapo.BackgroundImage = global::FENIX_KIOSCO.Properties.Resources.Encabezado7;
            this.pnlSapo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSapo.Controls.Add(this.LBL_MODO);
            this.pnlSapo.Controls.Add(this.LblPr_version);
            this.pnlSapo.Controls.Add(this.btnAccept);
            this.pnlSapo.Controls.Add(this.lblMsjs);
            this.pnlSapo.Controls.Add(this.cbAutoupdate);
            this.pnlSapo.Controls.Add(this.LblVersion);
            this.pnlSapo.Controls.Add(this.cbAutoplug);
            this.pnlSapo.Controls.Add(this.pictureBox2);
            this.pnlSapo.Controls.Add(this.formatsComboBox);
            this.pnlSapo.Controls.Add(this.biometricDevicePositionComboBox);
            this.pnlSapo.Controls.Add(this.cbAutomatic);
            this.pnlSapo.Controls.Add(this.biometricDeviceImpressionTypeComboBox);
            this.pnlSapo.Controls.Add(this.typeLabel);
            this.pnlSapo.Controls.Add(this.deviceTypesGroupBox);
            this.pnlSapo.Controls.Add(this.pictureBox1);
            this.pnlSapo.Controls.Add(this.deviceTreeView);
            this.pnlSapo.Controls.Add(this.devicePropertyGrid);
            this.pnlSapo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSapo.Location = new System.Drawing.Point(0, 0);
            this.pnlSapo.Name = "pnlSapo";
            this.pnlSapo.Size = new System.Drawing.Size(4576, 144);
            this.pnlSapo.TabIndex = 15;
            // 
            // LBL_MODO
            // 
            this.LBL_MODO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_MODO.AutoSize = true;
            this.LBL_MODO.BackColor = System.Drawing.Color.Transparent;
            this.LBL_MODO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.LBL_MODO.ForeColor = System.Drawing.Color.FloralWhite;
            this.LBL_MODO.Location = new System.Drawing.Point(4226, 75);
            this.LBL_MODO.Name = "LBL_MODO";
            this.LBL_MODO.Size = new System.Drawing.Size(77, 25);
            this.LBL_MODO.TabIndex = 14;
            this.LBL_MODO.Text = "MODO";
            // 
            // LblPr_version
            // 
            this.LblPr_version.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPr_version.AutoSize = true;
            this.LblPr_version.BackColor = System.Drawing.Color.Transparent;
            this.LblPr_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPr_version.ForeColor = System.Drawing.Color.Goldenrod;
            this.LblPr_version.Location = new System.Drawing.Point(4212, 112);
            this.LblPr_version.Name = "LblPr_version";
            this.LblPr_version.Size = new System.Drawing.Size(105, 25);
            this.LblPr_version.TabIndex = 13;
            this.LblPr_version.Text = "VERSION";
            this.LblPr_version.Click += new System.EventHandler(this.LblPr_version_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(4083, 106);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 38;
            this.btnAccept.Text = "&OK";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Visible = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblMsjs
            // 
            this.lblMsjs.AutoSize = true;
            this.lblMsjs.BackColor = System.Drawing.Color.Transparent;
            this.lblMsjs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsjs.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblMsjs.Location = new System.Drawing.Point(881, 116);
            this.lblMsjs.Name = "lblMsjs";
            this.lblMsjs.Size = new System.Drawing.Size(140, 20);
            this.lblMsjs.TabIndex = 15;
            this.lblMsjs.Text = "AGR (NT_Infot).";
            // 
            // cbAutoupdate
            // 
            this.cbAutoupdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAutoupdate.AutoSize = true;
            this.cbAutoupdate.Checked = true;
            this.cbAutoupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoupdate.Location = new System.Drawing.Point(1185, 28);
            this.cbAutoupdate.Name = "cbAutoupdate";
            this.cbAutoupdate.Size = new System.Drawing.Size(84, 17);
            this.cbAutoupdate.TabIndex = 37;
            this.cbAutoupdate.Text = "&Auto update";
            this.cbAutoupdate.UseVisualStyleBackColor = true;
            this.cbAutoupdate.Visible = false;
            // 
            // LblVersion
            // 
            this.LblVersion.AutoSize = true;
            this.LblVersion.BackColor = System.Drawing.Color.Transparent;
            this.LblVersion.ForeColor = System.Drawing.Color.MidnightBlue;
            this.LblVersion.Location = new System.Drawing.Point(1080, 95);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(0, 13);
            this.LblVersion.TabIndex = 3;
            // 
            // cbAutoplug
            // 
            this.cbAutoplug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAutoplug.AutoSize = true;
            this.cbAutoplug.Checked = true;
            this.cbAutoplug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoplug.Location = new System.Drawing.Point(1196, 53);
            this.cbAutoplug.Name = "cbAutoplug";
            this.cbAutoplug.Size = new System.Drawing.Size(71, 17);
            this.cbAutoplug.TabIndex = 36;
            this.cbAutoplug.Text = "&Auto plug";
            this.cbAutoplug.UseVisualStyleBackColor = true;
            this.cbAutoplug.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3986, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(234, 80);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // formatsComboBox
            // 
            this.formatsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatsComboBox.FormattingEnabled = true;
            this.formatsComboBox.Location = new System.Drawing.Point(1072, 72);
            this.formatsComboBox.Name = "formatsComboBox";
            this.formatsComboBox.Size = new System.Drawing.Size(121, 21);
            this.formatsComboBox.TabIndex = 34;
            this.formatsComboBox.Visible = false;
            // 
            // biometricDevicePositionComboBox
            // 
            this.biometricDevicePositionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.biometricDevicePositionComboBox.FormattingEnabled = true;
            this.biometricDevicePositionComboBox.Location = new System.Drawing.Point(1071, 27);
            this.biometricDevicePositionComboBox.Name = "biometricDevicePositionComboBox";
            this.biometricDevicePositionComboBox.Size = new System.Drawing.Size(121, 21);
            this.biometricDevicePositionComboBox.TabIndex = 33;
            this.biometricDevicePositionComboBox.Visible = false;
            // 
            // cbAutomatic
            // 
            this.cbAutomatic.AutoSize = true;
            this.cbAutomatic.Checked = true;
            this.cbAutomatic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutomatic.Location = new System.Drawing.Point(1198, 52);
            this.cbAutomatic.Name = "cbAutomatic";
            this.cbAutomatic.Size = new System.Drawing.Size(73, 17);
            this.cbAutomatic.TabIndex = 32;
            this.cbAutomatic.Text = "&Automatic";
            this.cbAutomatic.UseVisualStyleBackColor = true;
            this.cbAutomatic.Visible = false;
            // 
            // biometricDeviceImpressionTypeComboBox
            // 
            this.biometricDeviceImpressionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.biometricDeviceImpressionTypeComboBox.FormattingEnabled = true;
            this.biometricDeviceImpressionTypeComboBox.Location = new System.Drawing.Point(1071, 49);
            this.biometricDeviceImpressionTypeComboBox.Name = "biometricDeviceImpressionTypeComboBox";
            this.biometricDeviceImpressionTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.biometricDeviceImpressionTypeComboBox.TabIndex = 31;
            this.biometricDeviceImpressionTypeComboBox.Visible = false;
            // 
            // typeLabel
            // 
            this.typeLabel.Location = new System.Drawing.Point(1080, 5);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(99, 17);
            this.typeLabel.TabIndex = 29;
            this.typeLabel.Text = "Type";
            this.typeLabel.Visible = false;
            // 
            // deviceTypesGroupBox
            // 
            this.deviceTypesGroupBox.Controls.Add(this.captureDeviceCheckBox);
            this.deviceTypesGroupBox.Controls.Add(this.microphoneCheckBox);
            this.deviceTypesGroupBox.Controls.Add(this.anyCheckBox);
            this.deviceTypesGroupBox.Controls.Add(this.fScannerCheckBox);
            this.deviceTypesGroupBox.Controls.Add(this.cameraCheckBox);
            this.deviceTypesGroupBox.Controls.Add(this.irisScannerCheckBox);
            this.deviceTypesGroupBox.Controls.Add(this.fingerScannerCheckBox);
            this.deviceTypesGroupBox.Controls.Add(this.biometricDeviceCheckBox);
            this.deviceTypesGroupBox.Location = new System.Drawing.Point(691, 14);
            this.deviceTypesGroupBox.Name = "deviceTypesGroupBox";
            this.deviceTypesGroupBox.Size = new System.Drawing.Size(376, 65);
            this.deviceTypesGroupBox.TabIndex = 35;
            this.deviceTypesGroupBox.TabStop = false;
            this.deviceTypesGroupBox.Text = "Device types";
            this.deviceTypesGroupBox.Visible = false;
            // 
            // captureDeviceCheckBox
            // 
            this.captureDeviceCheckBox.AutoCheck = false;
            this.captureDeviceCheckBox.AutoSize = true;
            this.captureDeviceCheckBox.Location = new System.Drawing.Point(6, 27);
            this.captureDeviceCheckBox.Name = "captureDeviceCheckBox";
            this.captureDeviceCheckBox.Size = new System.Drawing.Size(100, 17);
            this.captureDeviceCheckBox.TabIndex = 12;
            this.captureDeviceCheckBox.Text = "Capture Device";
            this.captureDeviceCheckBox.UseVisualStyleBackColor = true;
            this.captureDeviceCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
            // 
            // microphoneCheckBox
            // 
            this.microphoneCheckBox.AutoCheck = false;
            this.microphoneCheckBox.AutoSize = true;
            this.microphoneCheckBox.Location = new System.Drawing.Point(101, 12);
            this.microphoneCheckBox.Name = "microphoneCheckBox";
            this.microphoneCheckBox.Size = new System.Drawing.Size(82, 17);
            this.microphoneCheckBox.TabIndex = 11;
            this.microphoneCheckBox.Text = "Microphone";
            this.microphoneCheckBox.UseVisualStyleBackColor = true;
            this.microphoneCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
            // 
            // anyCheckBox
            // 
            this.anyCheckBox.AutoCheck = false;
            this.anyCheckBox.AutoSize = true;
            this.anyCheckBox.Location = new System.Drawing.Point(6, 12);
            this.anyCheckBox.Name = "anyCheckBox";
            this.anyCheckBox.Size = new System.Drawing.Size(44, 17);
            this.anyCheckBox.TabIndex = 9;
            this.anyCheckBox.Text = "Any";
            this.anyCheckBox.ThreeState = true;
            this.anyCheckBox.UseVisualStyleBackColor = true;
            this.anyCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
            // 
            // fScannerCheckBox
            // 
            this.fScannerCheckBox.AutoSize = true;
            this.fScannerCheckBox.Location = new System.Drawing.Point(290, 27);
            this.fScannerCheckBox.Name = "fScannerCheckBox";
            this.fScannerCheckBox.Size = new System.Drawing.Size(73, 17);
            this.fScannerCheckBox.TabIndex = 14;
            this.fScannerCheckBox.Text = "F scanner";
            this.fScannerCheckBox.UseVisualStyleBackColor = true;
            this.fScannerCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
            // 
            // cameraCheckBox
            // 
            this.cameraCheckBox.AutoCheck = false;
            this.cameraCheckBox.AutoSize = true;
            this.cameraCheckBox.Location = new System.Drawing.Point(6, 45);
            this.cameraCheckBox.Name = "cameraCheckBox";
            this.cameraCheckBox.Size = new System.Drawing.Size(62, 17);
            this.cameraCheckBox.TabIndex = 10;
            this.cameraCheckBox.Text = "Camera";
            this.cameraCheckBox.UseVisualStyleBackColor = true;
            this.cameraCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
            // 
            // irisScannerCheckBox
            // 
            this.irisScannerCheckBox.AutoCheck = false;
            this.irisScannerCheckBox.AutoSize = true;
            this.irisScannerCheckBox.Location = new System.Drawing.Point(290, 42);
            this.irisScannerCheckBox.Name = "irisScannerCheckBox";
            this.irisScannerCheckBox.Size = new System.Drawing.Size(80, 17);
            this.irisScannerCheckBox.TabIndex = 16;
            this.irisScannerCheckBox.Text = "Iris scanner";
            this.irisScannerCheckBox.UseVisualStyleBackColor = true;
            this.irisScannerCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
            // 
            // fingerScannerCheckBox
            // 
            this.fingerScannerCheckBox.AutoCheck = false;
            this.fingerScannerCheckBox.AutoSize = true;
            this.fingerScannerCheckBox.Location = new System.Drawing.Point(207, 12);
            this.fingerScannerCheckBox.Name = "fingerScannerCheckBox";
            this.fingerScannerCheckBox.Size = new System.Drawing.Size(96, 17);
            this.fingerScannerCheckBox.TabIndex = 15;
            this.fingerScannerCheckBox.Text = "Finger scanner";
            this.fingerScannerCheckBox.UseVisualStyleBackColor = true;
            this.fingerScannerCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
            // 
            // biometricDeviceCheckBox
            // 
            this.biometricDeviceCheckBox.AutoCheck = false;
            this.biometricDeviceCheckBox.AutoSize = true;
            this.biometricDeviceCheckBox.Location = new System.Drawing.Point(184, 35);
            this.biometricDeviceCheckBox.Name = "biometricDeviceCheckBox";
            this.biometricDeviceCheckBox.Size = new System.Drawing.Size(104, 17);
            this.biometricDeviceCheckBox.TabIndex = 13;
            this.biometricDeviceCheckBox.Text = "Biometric device";
            this.biometricDeviceCheckBox.UseVisualStyleBackColor = true;
            this.biometricDeviceCheckBox.Click += new System.EventHandler(this.deviceTypeCheckBox_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::FENIX_KIOSCO.Properties.Resources.logoCaja;
            this.pictureBox1.Location = new System.Drawing.Point(144, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(510, 134);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // deviceTreeView
            // 
            this.deviceTreeView.HideSelection = false;
            this.deviceTreeView.Location = new System.Drawing.Point(3, -3);
            this.deviceTreeView.Name = "deviceTreeView";
            this.deviceTreeView.Size = new System.Drawing.Size(402, 96);
            this.deviceTreeView.TabIndex = 28;
            this.deviceTreeView.Visible = false;
            this.deviceTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.deviceTreeView_AfterSelect);
            // 
            // devicePropertyGrid
            // 
            this.devicePropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.devicePropertyGrid.Location = new System.Drawing.Point(404, 1);
            this.devicePropertyGrid.Name = "devicePropertyGrid";
            this.devicePropertyGrid.Size = new System.Drawing.Size(289, 99);
            this.devicePropertyGrid.TabIndex = 30;
            this.devicePropertyGrid.Visible = false;
            // 
            // CMD_Cancelar
            // 
            this.CMD_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CMD_Cancelar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CMD_Cancelar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_Cancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Cancelar.FlatAppearance.BorderSize = 0;
            this.CMD_Cancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CMD_Cancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Cancelar.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Cancelar.Image = global::FENIX_KIOSCO.Properties.Resources.Boton_terminar;
            this.CMD_Cancelar.Location = new System.Drawing.Point(4426, 403);
            this.CMD_Cancelar.Name = "CMD_Cancelar";
            this.CMD_Cancelar.Size = new System.Drawing.Size(150, 406);
            this.CMD_Cancelar.TabIndex = 8;
            this.CMD_Cancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CMD_Cancelar.UseVisualStyleBackColor = false;
            this.CMD_Cancelar.Visible = false;
            this.CMD_Cancelar.Click += new System.EventHandler(this.CMD_Cancelar_Click);
            // 
            // CMD_Regresar
            // 
            this.CMD_Regresar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CMD_Regresar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CMD_Regresar.BackColor = System.Drawing.Color.Transparent;
            this.CMD_Regresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Regresar.FlatAppearance.BorderSize = 0;
            this.CMD_Regresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Regresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Regresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.CMD_Regresar.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Regresar.Image = global::FENIX_KIOSCO.Properties.Resources.Boton_Regresar;
            this.CMD_Regresar.Location = new System.Drawing.Point(0, 403);
            this.CMD_Regresar.Name = "CMD_Regresar";
            this.CMD_Regresar.Size = new System.Drawing.Size(188, 406);
            this.CMD_Regresar.TabIndex = 7;
            this.CMD_Regresar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CMD_Regresar.UseVisualStyleBackColor = false;
            this.CMD_Regresar.Visible = false;
            this.CMD_Regresar.Click += new System.EventHandler(this.CMD_Regresar_Click);
            // 
            // pnl_Inicio
            // 
            this.pnl_Inicio.AutoSize = true;
            this.pnl_Inicio.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Inicio.Controls.Add(this.panel7);
            this.pnl_Inicio.Controls.Add(this.cmd_NoAfiliado);
            this.pnl_Inicio.Controls.Add(this.cmd_Afiliado);
            this.pnl_Inicio.Controls.Add(this.label5);
            this.pnl_Inicio.Location = new System.Drawing.Point(2417, 154);
            this.pnl_Inicio.Name = "pnl_Inicio";
            this.pnl_Inicio.Size = new System.Drawing.Size(419, 605);
            this.pnl_Inicio.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.label8);
            this.panel7.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.MaximumSize = new System.Drawing.Size(416, 63);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(416, 63);
            this.panel7.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Modern No. 20", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label8.Location = new System.Drawing.Point(140, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 45);
            this.label8.TabIndex = 0;
            this.label8.Text = "Inicio";
            // 
            // cmd_NoAfiliado
            // 
            this.cmd_NoAfiliado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmd_NoAfiliado.BackgroundImage")));
            this.cmd_NoAfiliado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_NoAfiliado.FlatAppearance.BorderSize = 0;
            this.cmd_NoAfiliado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.cmd_NoAfiliado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_NoAfiliado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_NoAfiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.cmd_NoAfiliado.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_NoAfiliado.Location = new System.Drawing.Point(29, 372);
            this.cmd_NoAfiliado.Name = "cmd_NoAfiliado";
            this.cmd_NoAfiliado.Size = new System.Drawing.Size(364, 74);
            this.cmd_NoAfiliado.TabIndex = 4;
            this.cmd_NoAfiliado.Text = "NO AFILIADO";
            this.cmd_NoAfiliado.UseVisualStyleBackColor = true;
            this.cmd_NoAfiliado.Click += new System.EventHandler(this.cmd_NoAfiliado_Click);
            // 
            // cmd_Afiliado
            // 
            this.cmd_Afiliado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmd_Afiliado.BackgroundImage")));
            this.cmd_Afiliado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_Afiliado.FlatAppearance.BorderSize = 0;
            this.cmd_Afiliado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.cmd_Afiliado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_Afiliado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_Afiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold);
            this.cmd_Afiliado.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_Afiliado.Location = new System.Drawing.Point(35, 199);
            this.cmd_Afiliado.Name = "cmd_Afiliado";
            this.cmd_Afiliado.Size = new System.Drawing.Size(364, 74);
            this.cmd_Afiliado.TabIndex = 3;
            this.cmd_Afiliado.Text = "AFILIADO";
            this.cmd_Afiliado.UseVisualStyleBackColor = true;
            this.cmd_Afiliado.Click += new System.EventHandler(this.cmd_Afiliado_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(43, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 20);
            this.label5.TabIndex = 2;
            // 
            // pnl_NoAfiliado
            // 
            this.pnl_NoAfiliado.BackColor = System.Drawing.Color.Transparent;
            this.pnl_NoAfiliado.Controls.Add(this.cmd_Beneficiario);
            this.pnl_NoAfiliado.Controls.Add(this.cmd_Apoderado);
            this.pnl_NoAfiliado.Controls.Add(this.label9);
            this.pnl_NoAfiliado.Controls.Add(this.panel8);
            this.pnl_NoAfiliado.Location = new System.Drawing.Point(4007, 154);
            this.pnl_NoAfiliado.Name = "pnl_NoAfiliado";
            this.pnl_NoAfiliado.Size = new System.Drawing.Size(416, 605);
            this.pnl_NoAfiliado.TabIndex = 17;
            this.pnl_NoAfiliado.Visible = false;
            // 
            // cmd_Beneficiario
            // 
            this.cmd_Beneficiario.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmd_Beneficiario.BackgroundImage")));
            this.cmd_Beneficiario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_Beneficiario.FlatAppearance.BorderSize = 0;
            this.cmd_Beneficiario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.cmd_Beneficiario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_Beneficiario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_Beneficiario.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_Beneficiario.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_Beneficiario.Location = new System.Drawing.Point(23, 285);
            this.cmd_Beneficiario.Name = "cmd_Beneficiario";
            this.cmd_Beneficiario.Size = new System.Drawing.Size(377, 74);
            this.cmd_Beneficiario.TabIndex = 4;
            this.cmd_Beneficiario.Text = "BENEFICIARIO";
            this.cmd_Beneficiario.UseVisualStyleBackColor = true;
            this.cmd_Beneficiario.Click += new System.EventHandler(this.cmd_Beneficiario_Click);
            // 
            // cmd_Apoderado
            // 
            this.cmd_Apoderado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmd_Apoderado.BackgroundImage")));
            this.cmd_Apoderado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_Apoderado.FlatAppearance.BorderSize = 0;
            this.cmd_Apoderado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.cmd_Apoderado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_Apoderado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_Apoderado.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_Apoderado.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_Apoderado.Location = new System.Drawing.Point(23, 162);
            this.cmd_Apoderado.Name = "cmd_Apoderado";
            this.cmd_Apoderado.Size = new System.Drawing.Size(377, 84);
            this.cmd_Apoderado.TabIndex = 3;
            this.cmd_Apoderado.Text = "APODERADO   AUTORIZADO";
            this.cmd_Apoderado.UseVisualStyleBackColor = true;
            this.cmd_Apoderado.Click += new System.EventHandler(this.cmd_Apoderado_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.LightGray;
            this.label9.Location = new System.Drawing.Point(43, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 20);
            this.label9.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel8.Controls.Add(this.label10);
            this.panel8.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.MaximumSize = new System.Drawing.Size(416, 63);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(416, 63);
            this.panel8.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Modern No. 20", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.Location = new System.Drawing.Point(102, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(227, 45);
            this.label10.TabIndex = 0;
            this.label10.Text = "No Afiliado";
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(0, 0);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(75, 23);
            this.button21.TabIndex = 0;
            // 
            // pnl_turnosTitle
            // 
            this.pnl_turnosTitle.BackColor = System.Drawing.Color.Transparent;
            this.pnl_turnosTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_turnosTitle.Controls.Add(this.panel1);
            this.pnl_turnosTitle.Controls.Add(this.lbl_TurnoTitle);
            this.pnl_turnosTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_turnosTitle.Location = new System.Drawing.Point(0, 0);
            this.pnl_turnosTitle.Name = "pnl_turnosTitle";
            this.pnl_turnosTitle.Size = new System.Drawing.Size(1390, 63);
            this.pnl_turnosTitle.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1390, 63);
            this.panel1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Modern No. 20", 32.25F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(3, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1384, 41);
            this.label6.TabIndex = 0;
            this.label6.Text = "Turnos Afiliados";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TurnoTitle
            // 
            this.lbl_TurnoTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_TurnoTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TurnoTitle.Font = new System.Drawing.Font("Modern No. 20", 32.25F);
            this.lbl_TurnoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_TurnoTitle.Location = new System.Drawing.Point(27, 9);
            this.lbl_TurnoTitle.Name = "lbl_TurnoTitle";
            this.lbl_TurnoTitle.Size = new System.Drawing.Size(1332, 41);
            this.lbl_TurnoTitle.TabIndex = 0;
            this.lbl_TurnoTitle.Text = "Turnos Afilidados";
            this.lbl_TurnoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(61, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 2;
            // 
            // CMD_Tramite
            // 
            this.CMD_Tramite.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Tramite.BackgroundImage")));
            this.CMD_Tramite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Tramite.FlatAppearance.BorderSize = 0;
            this.CMD_Tramite.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Tramite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Tramite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Tramite.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Tramite.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Tramite.Location = new System.Drawing.Point(1049, 526);
            this.CMD_Tramite.Name = "CMD_Tramite";
            this.CMD_Tramite.Size = new System.Drawing.Size(310, 63);
            this.CMD_Tramite.TabIndex = 4;
            this.CMD_Tramite.Text = "TRÁMITE EN LÍNEA";
            this.CMD_Tramite.UseVisualStyleBackColor = true;
            this.CMD_Tramite.Click += new System.EventHandler(this.CMD_InfoGral_Click);
            // 
            // CMD_Vivienda14
            // 
            this.CMD_Vivienda14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Vivienda14.BackgroundImage")));
            this.CMD_Vivienda14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Vivienda14.FlatAppearance.BorderSize = 0;
            this.CMD_Vivienda14.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Vivienda14.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Vivienda14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Vivienda14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Vivienda14.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Vivienda14.Location = new System.Drawing.Point(1049, 293);
            this.CMD_Vivienda14.Name = "CMD_Vivienda14";
            this.CMD_Vivienda14.Size = new System.Drawing.Size(310, 63);
            this.CMD_Vivienda14.TabIndex = 12;
            this.CMD_Vivienda14.Text = "VIVIENDA 14";
            this.CMD_Vivienda14.UseVisualStyleBackColor = true;
            this.CMD_Vivienda14.Click += new System.EventHandler(this.CMD_Vivienda14_Click);
            // 
            // CMD_Vivienda8
            // 
            this.CMD_Vivienda8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Vivienda8.BackgroundImage")));
            this.CMD_Vivienda8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Vivienda8.FlatAppearance.BorderSize = 0;
            this.CMD_Vivienda8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Vivienda8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Vivienda8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Vivienda8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Vivienda8.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Vivienda8.Location = new System.Drawing.Point(1049, 218);
            this.CMD_Vivienda8.Name = "CMD_Vivienda8";
            this.CMD_Vivienda8.Size = new System.Drawing.Size(310, 63);
            this.CMD_Vivienda8.TabIndex = 14;
            this.CMD_Vivienda8.Text = "VIVIENDA 8";
            this.CMD_Vivienda8.UseVisualStyleBackColor = true;
            this.CMD_Vivienda8.Click += new System.EventHandler(this.CMD_Vivienda8_Click);
            // 
            // CMD_Leasing
            // 
            this.CMD_Leasing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Leasing.BackgroundImage")));
            this.CMD_Leasing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Leasing.FlatAppearance.BorderSize = 0;
            this.CMD_Leasing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Leasing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Leasing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Leasing.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Leasing.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Leasing.Location = new System.Drawing.Point(1049, 141);
            this.CMD_Leasing.Name = "CMD_Leasing";
            this.CMD_Leasing.Size = new System.Drawing.Size(310, 63);
            this.CMD_Leasing.TabIndex = 15;
            this.CMD_Leasing.Text = "VIVIENDA LEASING";
            this.CMD_Leasing.UseVisualStyleBackColor = true;
            this.CMD_Leasing.Click += new System.EventHandler(this.CMD_Leasing_Click);
            // 
            // CMD_Heroes
            // 
            this.CMD_Heroes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Heroes.BackgroundImage")));
            this.CMD_Heroes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Heroes.FlatAppearance.BorderSize = 0;
            this.CMD_Heroes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Heroes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Heroes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Heroes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Heroes.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Heroes.Location = new System.Drawing.Point(1049, 372);
            this.CMD_Heroes.Name = "CMD_Heroes";
            this.CMD_Heroes.Size = new System.Drawing.Size(310, 63);
            this.CMD_Heroes.TabIndex = 16;
            this.CMD_Heroes.Text = "FONDO SOLIDARIDAD-HEROES";
            this.CMD_Heroes.UseVisualStyleBackColor = true;
            this.CMD_Heroes.Click += new System.EventHandler(this.CMD_Heroes_Click);
            // 
            // CMD_Agenda_cita
            // 
            this.CMD_Agenda_cita.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Agenda_cita.BackgroundImage")));
            this.CMD_Agenda_cita.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Agenda_cita.FlatAppearance.BorderSize = 0;
            this.CMD_Agenda_cita.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Agenda_cita.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Agenda_cita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Agenda_cita.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Agenda_cita.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Agenda_cita.Location = new System.Drawing.Point(1049, 674);
            this.CMD_Agenda_cita.Name = "CMD_Agenda_cita";
            this.CMD_Agenda_cita.Size = new System.Drawing.Size(310, 63);
            this.CMD_Agenda_cita.TabIndex = 17;
            this.CMD_Agenda_cita.Text = "AGENDAMIENTO CITA ";
            this.CMD_Agenda_cita.UseVisualStyleBackColor = true;
            this.CMD_Agenda_cita.Click += new System.EventHandler(this.CMD_Pretramite_Click);
            // 
            // CMD_Futuro
            // 
            this.CMD_Futuro.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Futuro.BackgroundImage")));
            this.CMD_Futuro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Futuro.FlatAppearance.BorderSize = 0;
            this.CMD_Futuro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Futuro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Futuro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Futuro.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Futuro.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Futuro.Location = new System.Drawing.Point(1049, 450);
            this.CMD_Futuro.Name = "CMD_Futuro";
            this.CMD_Futuro.Size = new System.Drawing.Size(310, 63);
            this.CMD_Futuro.TabIndex = 18;
            this.CMD_Futuro.Text = "FUTURO-CESANTIAS";
            this.CMD_Futuro.UseVisualStyleBackColor = true;
            this.CMD_Futuro.Click += new System.EventHandler(this.CMD_Futuro_Click);
            // 
            // CMD_Cuenta
            // 
            this.CMD_Cuenta.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Cuenta.BackgroundImage")));
            this.CMD_Cuenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Cuenta.FlatAppearance.BorderSize = 0;
            this.CMD_Cuenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Cuenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Cuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Cuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Cuenta.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Cuenta.Location = new System.Drawing.Point(1049, 602);
            this.CMD_Cuenta.Name = "CMD_Cuenta";
            this.CMD_Cuenta.Size = new System.Drawing.Size(310, 63);
            this.CMD_Cuenta.TabIndex = 19;
            this.CMD_Cuenta.Text = "INFORMACIÓN CUENTA";
            this.CMD_Cuenta.UseVisualStyleBackColor = true;
            this.CMD_Cuenta.Click += new System.EventHandler(this.CMD_InfoLeasing_Click);
            // 
            // CMD_Atencion_cita
            // 
            this.CMD_Atencion_cita.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Atencion_cita.BackgroundImage")));
            this.CMD_Atencion_cita.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Atencion_cita.FlatAppearance.BorderSize = 0;
            this.CMD_Atencion_cita.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Atencion_cita.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Atencion_cita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Atencion_cita.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Atencion_cita.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Atencion_cita.Location = new System.Drawing.Point(1049, 748);
            this.CMD_Atencion_cita.Name = "CMD_Atencion_cita";
            this.CMD_Atencion_cita.Size = new System.Drawing.Size(310, 63);
            this.CMD_Atencion_cita.TabIndex = 20;
            this.CMD_Atencion_cita.Text = "ATENCIÓN CITA PROGRAMADA";
            this.CMD_Atencion_cita.UseVisualStyleBackColor = true;
            this.CMD_Atencion_cita.Click += new System.EventHandler(this.CMD_Atencion_cita_Click);
            // 
            // pnl_TurnosInfoAfiliado
            // 
            this.pnl_TurnosInfoAfiliado.BackColor = System.Drawing.Color.Transparent;
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_RAFONDOH);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Turnofuturoafiliado);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Turnos);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Turnopretramiteafiliado);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.pnl_infoAfiliado);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_RENTA);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_TurnosRadicacion);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_turnoleasingafiliado);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CDM_Turno8afiliado);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_EstadoTramite);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Turno14afiliado);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Certificado);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Turnotramiteafiliado);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Atencion_cita);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.cmd_consulta);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_EstadCuenta);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Cuenta);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Futuro);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Agenda_cita);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Heroes);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Leasing);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Vivienda8);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Vivienda14);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.CMD_Tramite);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.label3);
            this.pnl_TurnosInfoAfiliado.Controls.Add(this.pnl_turnosTitle);
            this.pnl_TurnosInfoAfiliado.Location = new System.Drawing.Point(1024, 154);
            this.pnl_TurnosInfoAfiliado.Name = "pnl_TurnosInfoAfiliado";
            this.pnl_TurnosInfoAfiliado.Size = new System.Drawing.Size(1390, 881);
            this.pnl_TurnosInfoAfiliado.TabIndex = 12;
            this.pnl_TurnosInfoAfiliado.Visible = false;
            // 
            // CMD_RAFONDOH
            // 
            this.CMD_RAFONDOH.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RAFONDOH.BackgroundImage")));
            this.CMD_RAFONDOH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RAFONDOH.FlatAppearance.BorderSize = 0;
            this.CMD_RAFONDOH.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_RAFONDOH.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_RAFONDOH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RAFONDOH.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RAFONDOH.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RAFONDOH.Location = new System.Drawing.Point(629, 375);
            this.CMD_RAFONDOH.Name = "CMD_RAFONDOH";
            this.CMD_RAFONDOH.Size = new System.Drawing.Size(337, 63);
            this.CMD_RAFONDOH.TabIndex = 21;
            this.CMD_RAFONDOH.Text = "FONDO SOLIDARIDAD-HEROES";
            this.CMD_RAFONDOH.UseVisualStyleBackColor = true;
            this.CMD_RAFONDOH.Click += new System.EventHandler(this.CMD_RAFONDOH_Click);
            // 
            // CMD_Turnofuturoafiliado
            // 
            this.CMD_Turnofuturoafiliado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Turnofuturoafiliado.BackgroundImage")));
            this.CMD_Turnofuturoafiliado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Turnofuturoafiliado.FlatAppearance.BorderSize = 0;
            this.CMD_Turnofuturoafiliado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Turnofuturoafiliado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Turnofuturoafiliado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Turnofuturoafiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Turnofuturoafiliado.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Turnofuturoafiliado.Location = new System.Drawing.Point(629, 450);
            this.CMD_Turnofuturoafiliado.Name = "CMD_Turnofuturoafiliado";
            this.CMD_Turnofuturoafiliado.Size = new System.Drawing.Size(337, 63);
            this.CMD_Turnofuturoafiliado.TabIndex = 18;
            this.CMD_Turnofuturoafiliado.Text = "FUTURO-CESANTIAS";
            this.CMD_Turnofuturoafiliado.UseVisualStyleBackColor = true;
            this.CMD_Turnofuturoafiliado.Click += new System.EventHandler(this.CMD_Turnofuturoafiliado_Click);
            // 
            // CMD_Turnopretramiteafiliado
            // 
            this.CMD_Turnopretramiteafiliado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Turnopretramiteafiliado.BackgroundImage")));
            this.CMD_Turnopretramiteafiliado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Turnopretramiteafiliado.FlatAppearance.BorderSize = 0;
            this.CMD_Turnopretramiteafiliado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Turnopretramiteafiliado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Turnopretramiteafiliado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Turnopretramiteafiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Turnopretramiteafiliado.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Turnopretramiteafiliado.Location = new System.Drawing.Point(629, 531);
            this.CMD_Turnopretramiteafiliado.Name = "CMD_Turnopretramiteafiliado";
            this.CMD_Turnopretramiteafiliado.Size = new System.Drawing.Size(337, 63);
            this.CMD_Turnopretramiteafiliado.TabIndex = 17;
            this.CMD_Turnopretramiteafiliado.Text = "PRE-TRÁMITE";
            this.CMD_Turnopretramiteafiliado.UseVisualStyleBackColor = true;
            this.CMD_Turnopretramiteafiliado.Click += new System.EventHandler(this.CMD_Turnopretramiteafiliado_Click);
            // 
            // CMD_turnoleasingafiliado
            // 
            this.CMD_turnoleasingafiliado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_turnoleasingafiliado.BackgroundImage")));
            this.CMD_turnoleasingafiliado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_turnoleasingafiliado.FlatAppearance.BorderSize = 0;
            this.CMD_turnoleasingafiliado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_turnoleasingafiliado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_turnoleasingafiliado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_turnoleasingafiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_turnoleasingafiliado.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_turnoleasingafiliado.Location = new System.Drawing.Point(629, 142);
            this.CMD_turnoleasingafiliado.Name = "CMD_turnoleasingafiliado";
            this.CMD_turnoleasingafiliado.Size = new System.Drawing.Size(337, 63);
            this.CMD_turnoleasingafiliado.TabIndex = 15;
            this.CMD_turnoleasingafiliado.Text = "VIVIENDA LEASING";
            this.CMD_turnoleasingafiliado.UseVisualStyleBackColor = true;
            this.CMD_turnoleasingafiliado.Click += new System.EventHandler(this.CMD_turnoleasingafiliado_Click);
            // 
            // CDM_Turno8afiliado
            // 
            this.CDM_Turno8afiliado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CDM_Turno8afiliado.BackgroundImage")));
            this.CDM_Turno8afiliado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CDM_Turno8afiliado.FlatAppearance.BorderSize = 0;
            this.CDM_Turno8afiliado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CDM_Turno8afiliado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CDM_Turno8afiliado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CDM_Turno8afiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CDM_Turno8afiliado.ForeColor = System.Drawing.Color.Transparent;
            this.CDM_Turno8afiliado.Location = new System.Drawing.Point(629, 216);
            this.CDM_Turno8afiliado.Name = "CDM_Turno8afiliado";
            this.CDM_Turno8afiliado.Size = new System.Drawing.Size(337, 63);
            this.CDM_Turno8afiliado.TabIndex = 14;
            this.CDM_Turno8afiliado.Text = "VIVIENDA 8";
            this.CDM_Turno8afiliado.UseVisualStyleBackColor = true;
            this.CDM_Turno8afiliado.Click += new System.EventHandler(this.CDM_Turno8afiliado_Click);
            // 
            // CMD_Turno14afiliado
            // 
            this.CMD_Turno14afiliado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Turno14afiliado.BackgroundImage")));
            this.CMD_Turno14afiliado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Turno14afiliado.FlatAppearance.BorderSize = 0;
            this.CMD_Turno14afiliado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Turno14afiliado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Turno14afiliado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Turno14afiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Turno14afiliado.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Turno14afiliado.Location = new System.Drawing.Point(629, 294);
            this.CMD_Turno14afiliado.Name = "CMD_Turno14afiliado";
            this.CMD_Turno14afiliado.Size = new System.Drawing.Size(337, 63);
            this.CMD_Turno14afiliado.TabIndex = 12;
            this.CMD_Turno14afiliado.Text = "VIVIENDA 14";
            this.CMD_Turno14afiliado.UseVisualStyleBackColor = true;
            this.CMD_Turno14afiliado.Click += new System.EventHandler(this.CMD_Turno14afiliado_Click);
            // 
            // CMD_Turnotramiteafiliado
            // 
            this.CMD_Turnotramiteafiliado.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_Turnotramiteafiliado.BackgroundImage")));
            this.CMD_Turnotramiteafiliado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_Turnotramiteafiliado.FlatAppearance.BorderSize = 0;
            this.CMD_Turnotramiteafiliado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_Turnotramiteafiliado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_Turnotramiteafiliado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_Turnotramiteafiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_Turnotramiteafiliado.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_Turnotramiteafiliado.Location = new System.Drawing.Point(629, 611);
            this.CMD_Turnotramiteafiliado.Name = "CMD_Turnotramiteafiliado";
            this.CMD_Turnotramiteafiliado.Size = new System.Drawing.Size(337, 63);
            this.CMD_Turnotramiteafiliado.TabIndex = 4;
            this.CMD_Turnotramiteafiliado.Text = "TRÁMITE EN LÍNEA";
            this.CMD_Turnotramiteafiliado.UseVisualStyleBackColor = true;
            this.CMD_Turnotramiteafiliado.Click += new System.EventHandler(this.CMD_Turnotramiteafiliado_Click);
            // 
            // pnl_TurnosNAfiliados
            // 
            this.pnl_TurnosNAfiliados.BackColor = System.Drawing.Color.Transparent;
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_ViviendaNFuturo);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_ViviendaNFondo);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_ViviendaN14);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_ViviendaN8);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_ViviendaNLeasing);
            this.pnl_TurnosNAfiliados.Controls.Add(this.button2);
            this.pnl_TurnosNAfiliados.Controls.Add(this.lblnoafi);
            this.pnl_TurnosNAfiliados.Controls.Add(this.button4);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_RadiNFuturo);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_RadiNFondo);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_RadiViviendaN14);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_RadiViviendaN8);
            this.pnl_TurnosNAfiliados.Controls.Add(this.CMD_RadiViviendaNLeasing);
            this.pnl_TurnosNAfiliados.Location = new System.Drawing.Point(3556, 153);
            this.pnl_TurnosNAfiliados.Name = "pnl_TurnosNAfiliados";
            this.pnl_TurnosNAfiliados.Size = new System.Drawing.Size(735, 585);
            this.pnl_TurnosNAfiliados.TabIndex = 20;
            // 
            // CMD_ViviendaNFuturo
            // 
            this.CMD_ViviendaNFuturo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_ViviendaNFuturo.BackgroundImage")));
            this.CMD_ViviendaNFuturo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_ViviendaNFuturo.FlatAppearance.BorderSize = 0;
            this.CMD_ViviendaNFuturo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_ViviendaNFuturo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_ViviendaNFuturo.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_ViviendaNFuturo.Location = new System.Drawing.Point(399, 487);
            this.CMD_ViviendaNFuturo.Name = "CMD_ViviendaNFuturo";
            this.CMD_ViviendaNFuturo.Size = new System.Drawing.Size(305, 73);
            this.CMD_ViviendaNFuturo.TabIndex = 27;
            this.CMD_ViviendaNFuturo.Text = "FUTURO ";
            this.CMD_ViviendaNFuturo.UseVisualStyleBackColor = true;
            this.CMD_ViviendaNFuturo.Click += new System.EventHandler(this.CMD_ViviendaNFuturo_Click);
            // 
            // CMD_ViviendaNFondo
            // 
            this.CMD_ViviendaNFondo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_ViviendaNFondo.BackgroundImage")));
            this.CMD_ViviendaNFondo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_ViviendaNFondo.FlatAppearance.BorderSize = 0;
            this.CMD_ViviendaNFondo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_ViviendaNFondo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_ViviendaNFondo.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_ViviendaNFondo.Location = new System.Drawing.Point(399, 402);
            this.CMD_ViviendaNFondo.Name = "CMD_ViviendaNFondo";
            this.CMD_ViviendaNFondo.Size = new System.Drawing.Size(305, 77);
            this.CMD_ViviendaNFondo.TabIndex = 26;
            this.CMD_ViviendaNFondo.Text = "FONDO DE SOLIDARIDAD-HÉROES";
            this.CMD_ViviendaNFondo.UseVisualStyleBackColor = true;
            this.CMD_ViviendaNFondo.Click += new System.EventHandler(this.CMD_ViviendaNFondo_Click);
            // 
            // CMD_ViviendaN14
            // 
            this.CMD_ViviendaN14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_ViviendaN14.BackgroundImage")));
            this.CMD_ViviendaN14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_ViviendaN14.FlatAppearance.BorderSize = 0;
            this.CMD_ViviendaN14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_ViviendaN14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_ViviendaN14.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_ViviendaN14.Location = new System.Drawing.Point(399, 308);
            this.CMD_ViviendaN14.Name = "CMD_ViviendaN14";
            this.CMD_ViviendaN14.Size = new System.Drawing.Size(305, 77);
            this.CMD_ViviendaN14.TabIndex = 25;
            this.CMD_ViviendaN14.Text = "VIVIENDA 14";
            this.CMD_ViviendaN14.UseVisualStyleBackColor = true;
            this.CMD_ViviendaN14.Click += new System.EventHandler(this.button13_Click);
            // 
            // CMD_ViviendaN8
            // 
            this.CMD_ViviendaN8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_ViviendaN8.BackgroundImage")));
            this.CMD_ViviendaN8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_ViviendaN8.FlatAppearance.BorderSize = 0;
            this.CMD_ViviendaN8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_ViviendaN8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_ViviendaN8.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_ViviendaN8.Location = new System.Drawing.Point(399, 227);
            this.CMD_ViviendaN8.Name = "CMD_ViviendaN8";
            this.CMD_ViviendaN8.Size = new System.Drawing.Size(305, 73);
            this.CMD_ViviendaN8.TabIndex = 24;
            this.CMD_ViviendaN8.Text = "VIVIENDA 8";
            this.CMD_ViviendaN8.UseVisualStyleBackColor = true;
            this.CMD_ViviendaN8.Click += new System.EventHandler(this.CMD_ViviendaN8_Click);
            // 
            // CMD_ViviendaNLeasing
            // 
            this.CMD_ViviendaNLeasing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_ViviendaNLeasing.BackgroundImage")));
            this.CMD_ViviendaNLeasing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_ViviendaNLeasing.FlatAppearance.BorderSize = 0;
            this.CMD_ViviendaNLeasing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_ViviendaNLeasing.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_ViviendaNLeasing.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_ViviendaNLeasing.Location = new System.Drawing.Point(399, 148);
            this.CMD_ViviendaNLeasing.Name = "CMD_ViviendaNLeasing";
            this.CMD_ViviendaNLeasing.Size = new System.Drawing.Size(305, 73);
            this.CMD_ViviendaNLeasing.TabIndex = 23;
            this.CMD_ViviendaNLeasing.Text = "VIVIENDA LEASING";
            this.CMD_ViviendaNLeasing.UseVisualStyleBackColor = true;
            this.CMD_ViviendaNLeasing.Click += new System.EventHandler(this.CMD_ViviendaNLeasing_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Enabled = false;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(399, 76);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(305, 64);
            this.button2.TabIndex = 21;
            this.button2.Text = "INFORMACIÓN GENERAL";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblnoafi
            // 
            this.lblnoafi.AutoSize = true;
            this.lblnoafi.Font = new System.Drawing.Font("Modern No. 20", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnoafi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblnoafi.Location = new System.Drawing.Point(61, 15);
            this.lblnoafi.Name = "lblnoafi";
            this.lblnoafi.Size = new System.Drawing.Size(592, 45);
            this.lblnoafi.TabIndex = 0;
            this.lblnoafi.Text = "Turnos Apoderados / Autorizados";
            this.lblnoafi.Click += new System.EventHandler(this.label4_Click);
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Enabled = false;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Location = new System.Drawing.Point(24, 76);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(305, 64);
            this.button4.TabIndex = 22;
            this.button4.Text = "RADICACIÓN";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // CMD_RadiNFuturo
            // 
            this.CMD_RadiNFuturo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RadiNFuturo.BackgroundImage")));
            this.CMD_RadiNFuturo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RadiNFuturo.FlatAppearance.BorderSize = 0;
            this.CMD_RadiNFuturo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RadiNFuturo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RadiNFuturo.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RadiNFuturo.Location = new System.Drawing.Point(24, 487);
            this.CMD_RadiNFuturo.Name = "CMD_RadiNFuturo";
            this.CMD_RadiNFuturo.Size = new System.Drawing.Size(305, 73);
            this.CMD_RadiNFuturo.TabIndex = 5;
            this.CMD_RadiNFuturo.Text = "FUTURO ";
            this.CMD_RadiNFuturo.UseVisualStyleBackColor = true;
            this.CMD_RadiNFuturo.Click += new System.EventHandler(this.CMD_RadiNFuturo_Click);
            // 
            // CMD_RadiNFondo
            // 
            this.CMD_RadiNFondo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RadiNFondo.BackgroundImage")));
            this.CMD_RadiNFondo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RadiNFondo.FlatAppearance.BorderSize = 0;
            this.CMD_RadiNFondo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RadiNFondo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RadiNFondo.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RadiNFondo.Location = new System.Drawing.Point(24, 401);
            this.CMD_RadiNFondo.Name = "CMD_RadiNFondo";
            this.CMD_RadiNFondo.Size = new System.Drawing.Size(305, 77);
            this.CMD_RadiNFondo.TabIndex = 4;
            this.CMD_RadiNFondo.Text = "FONDO DE SOLIDARIDAD-HÉROES";
            this.CMD_RadiNFondo.UseVisualStyleBackColor = true;
            this.CMD_RadiNFondo.Click += new System.EventHandler(this.CMD_RadiNFondo_Click);
            // 
            // CMD_RadiViviendaN14
            // 
            this.CMD_RadiViviendaN14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RadiViviendaN14.BackgroundImage")));
            this.CMD_RadiViviendaN14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RadiViviendaN14.FlatAppearance.BorderSize = 0;
            this.CMD_RadiViviendaN14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RadiViviendaN14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RadiViviendaN14.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RadiViviendaN14.Location = new System.Drawing.Point(24, 308);
            this.CMD_RadiViviendaN14.Name = "CMD_RadiViviendaN14";
            this.CMD_RadiViviendaN14.Size = new System.Drawing.Size(305, 77);
            this.CMD_RadiViviendaN14.TabIndex = 3;
            this.CMD_RadiViviendaN14.Text = "VIVIENDA 14";
            this.CMD_RadiViviendaN14.UseVisualStyleBackColor = true;
            this.CMD_RadiViviendaN14.Click += new System.EventHandler(this.CMD_RadiViviendaN14_Click);
            // 
            // CMD_RadiViviendaN8
            // 
            this.CMD_RadiViviendaN8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RadiViviendaN8.BackgroundImage")));
            this.CMD_RadiViviendaN8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RadiViviendaN8.FlatAppearance.BorderSize = 0;
            this.CMD_RadiViviendaN8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RadiViviendaN8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RadiViviendaN8.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RadiViviendaN8.Location = new System.Drawing.Point(24, 227);
            this.CMD_RadiViviendaN8.Name = "CMD_RadiViviendaN8";
            this.CMD_RadiViviendaN8.Size = new System.Drawing.Size(305, 73);
            this.CMD_RadiViviendaN8.TabIndex = 2;
            this.CMD_RadiViviendaN8.Text = "VIVIENDA 8";
            this.CMD_RadiViviendaN8.UseVisualStyleBackColor = true;
            this.CMD_RadiViviendaN8.Click += new System.EventHandler(this.CMD_RadiViviendaN8_Click);
            // 
            // CMD_RadiViviendaNLeasing
            // 
            this.CMD_RadiViviendaNLeasing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RadiViviendaNLeasing.BackgroundImage")));
            this.CMD_RadiViviendaNLeasing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RadiViviendaNLeasing.FlatAppearance.BorderSize = 0;
            this.CMD_RadiViviendaNLeasing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RadiViviendaNLeasing.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RadiViviendaNLeasing.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RadiViviendaNLeasing.Location = new System.Drawing.Point(24, 148);
            this.CMD_RadiViviendaNLeasing.Name = "CMD_RadiViviendaNLeasing";
            this.CMD_RadiViviendaNLeasing.Size = new System.Drawing.Size(305, 73);
            this.CMD_RadiViviendaNLeasing.TabIndex = 1;
            this.CMD_RadiViviendaNLeasing.Text = "VIVIENDA LEASING";
            this.CMD_RadiViviendaNLeasing.UseVisualStyleBackColor = true;
            this.CMD_RadiViviendaNLeasing.Click += new System.EventHandler(this.CMD_RadiViviendaNLeasing_Click);
            // 
            // pnlTurnosBeneficiarios
            // 
            this.pnlTurnosBeneficiarios.BackColor = System.Drawing.Color.Transparent;
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_IBFUTURO);
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_IBFONDO);
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_IBCATORCE);
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_IBOCHO);
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_IBLEASING);
            this.pnlTurnosBeneficiarios.Controls.Add(this.button9);
            this.pnlTurnosBeneficiarios.Controls.Add(this.label4);
            this.pnlTurnosBeneficiarios.Controls.Add(this.button10);
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_RBFUTURO);
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_RBFONDO);
            this.pnlTurnosBeneficiarios.Controls.Add(this.button13);
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_RBOCHO);
            this.pnlTurnosBeneficiarios.Controls.Add(this.CMD_BRLEASING);
            this.pnlTurnosBeneficiarios.Location = new System.Drawing.Point(2839, 153);
            this.pnlTurnosBeneficiarios.Name = "pnlTurnosBeneficiarios";
            this.pnlTurnosBeneficiarios.Size = new System.Drawing.Size(711, 601);
            this.pnlTurnosBeneficiarios.TabIndex = 20;
            // 
            // CMD_IBFUTURO
            // 
            this.CMD_IBFUTURO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_IBFUTURO.BackgroundImage")));
            this.CMD_IBFUTURO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_IBFUTURO.FlatAppearance.BorderSize = 0;
            this.CMD_IBFUTURO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_IBFUTURO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_IBFUTURO.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_IBFUTURO.Location = new System.Drawing.Point(399, 487);
            this.CMD_IBFUTURO.Name = "CMD_IBFUTURO";
            this.CMD_IBFUTURO.Size = new System.Drawing.Size(305, 73);
            this.CMD_IBFUTURO.TabIndex = 27;
            this.CMD_IBFUTURO.Text = "FUTURO ";
            this.CMD_IBFUTURO.UseVisualStyleBackColor = true;
            this.CMD_IBFUTURO.Click += new System.EventHandler(this.CMD_IBFUTURO_Click);
            // 
            // CMD_IBFONDO
            // 
            this.CMD_IBFONDO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_IBFONDO.BackgroundImage")));
            this.CMD_IBFONDO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_IBFONDO.FlatAppearance.BorderSize = 0;
            this.CMD_IBFONDO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_IBFONDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_IBFONDO.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_IBFONDO.Location = new System.Drawing.Point(399, 402);
            this.CMD_IBFONDO.Name = "CMD_IBFONDO";
            this.CMD_IBFONDO.Size = new System.Drawing.Size(305, 77);
            this.CMD_IBFONDO.TabIndex = 26;
            this.CMD_IBFONDO.Text = "FONDO DE SOLIDARIDAD-HÉROES";
            this.CMD_IBFONDO.UseVisualStyleBackColor = true;
            this.CMD_IBFONDO.Click += new System.EventHandler(this.CMD_IBFONDO_Click);
            // 
            // CMD_IBCATORCE
            // 
            this.CMD_IBCATORCE.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_IBCATORCE.BackgroundImage")));
            this.CMD_IBCATORCE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_IBCATORCE.FlatAppearance.BorderSize = 0;
            this.CMD_IBCATORCE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_IBCATORCE.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_IBCATORCE.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_IBCATORCE.Location = new System.Drawing.Point(399, 308);
            this.CMD_IBCATORCE.Name = "CMD_IBCATORCE";
            this.CMD_IBCATORCE.Size = new System.Drawing.Size(305, 77);
            this.CMD_IBCATORCE.TabIndex = 25;
            this.CMD_IBCATORCE.Text = "VIVIENDA 14";
            this.CMD_IBCATORCE.UseVisualStyleBackColor = true;
            this.CMD_IBCATORCE.Click += new System.EventHandler(this.CMD_IBCATORCE_Click);
            // 
            // CMD_IBOCHO
            // 
            this.CMD_IBOCHO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_IBOCHO.BackgroundImage")));
            this.CMD_IBOCHO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_IBOCHO.FlatAppearance.BorderSize = 0;
            this.CMD_IBOCHO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_IBOCHO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_IBOCHO.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_IBOCHO.Location = new System.Drawing.Point(399, 229);
            this.CMD_IBOCHO.Name = "CMD_IBOCHO";
            this.CMD_IBOCHO.Size = new System.Drawing.Size(305, 73);
            this.CMD_IBOCHO.TabIndex = 24;
            this.CMD_IBOCHO.Text = "VIVIENDA 8";
            this.CMD_IBOCHO.UseVisualStyleBackColor = true;
            this.CMD_IBOCHO.Click += new System.EventHandler(this.CMD_IBOCHO_Click);
            // 
            // CMD_IBLEASING
            // 
            this.CMD_IBLEASING.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_IBLEASING.BackgroundImage")));
            this.CMD_IBLEASING.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_IBLEASING.FlatAppearance.BorderSize = 0;
            this.CMD_IBLEASING.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_IBLEASING.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_IBLEASING.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_IBLEASING.Location = new System.Drawing.Point(399, 148);
            this.CMD_IBLEASING.Name = "CMD_IBLEASING";
            this.CMD_IBLEASING.Size = new System.Drawing.Size(305, 73);
            this.CMD_IBLEASING.TabIndex = 23;
            this.CMD_IBLEASING.Text = "VIVIENDA LEASING";
            this.CMD_IBLEASING.UseVisualStyleBackColor = true;
            this.CMD_IBLEASING.Click += new System.EventHandler(this.CMD_IBLEASING_Click);
            // 
            // button9
            // 
            this.button9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button9.BackgroundImage")));
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.Enabled = false;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.Transparent;
            this.button9.Location = new System.Drawing.Point(399, 76);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(305, 64);
            this.button9.TabIndex = 21;
            this.button9.Text = "INFORMACIÓN GENERAL";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(168, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(386, 45);
            this.label4.TabIndex = 0;
            this.label4.Text = "Turnos Beneficiarios";
            // 
            // button10
            // 
            this.button10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button10.BackgroundImage")));
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button10.Enabled = false;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.button10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.ForeColor = System.Drawing.Color.Transparent;
            this.button10.Location = new System.Drawing.Point(24, 76);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(305, 64);
            this.button10.TabIndex = 22;
            this.button10.Text = "RADICACIÓN";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // CMD_RBFUTURO
            // 
            this.CMD_RBFUTURO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RBFUTURO.BackgroundImage")));
            this.CMD_RBFUTURO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RBFUTURO.FlatAppearance.BorderSize = 0;
            this.CMD_RBFUTURO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RBFUTURO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RBFUTURO.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RBFUTURO.Location = new System.Drawing.Point(24, 487);
            this.CMD_RBFUTURO.Name = "CMD_RBFUTURO";
            this.CMD_RBFUTURO.Size = new System.Drawing.Size(305, 73);
            this.CMD_RBFUTURO.TabIndex = 5;
            this.CMD_RBFUTURO.Text = "FUTURO ";
            this.CMD_RBFUTURO.UseVisualStyleBackColor = true;
            this.CMD_RBFUTURO.Click += new System.EventHandler(this.CMD_RBFUTURO_Click);
            // 
            // CMD_RBFONDO
            // 
            this.CMD_RBFONDO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RBFONDO.BackgroundImage")));
            this.CMD_RBFONDO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RBFONDO.FlatAppearance.BorderSize = 0;
            this.CMD_RBFONDO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RBFONDO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RBFONDO.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RBFONDO.Location = new System.Drawing.Point(24, 401);
            this.CMD_RBFONDO.Name = "CMD_RBFONDO";
            this.CMD_RBFONDO.Size = new System.Drawing.Size(305, 77);
            this.CMD_RBFONDO.TabIndex = 4;
            this.CMD_RBFONDO.Text = "FONDO DE SOLIDARIDAD-HÉROES";
            this.CMD_RBFONDO.UseVisualStyleBackColor = true;
            this.CMD_RBFONDO.Click += new System.EventHandler(this.CMD_RBFONDO_Click);
            // 
            // button13
            // 
            this.button13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button13.BackgroundImage")));
            this.button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button13.FlatAppearance.BorderSize = 0;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.ForeColor = System.Drawing.Color.Transparent;
            this.button13.Location = new System.Drawing.Point(24, 308);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(305, 77);
            this.button13.TabIndex = 3;
            this.button13.Text = "VIVIENDA 14";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click_1);
            // 
            // CMD_RBOCHO
            // 
            this.CMD_RBOCHO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_RBOCHO.BackgroundImage")));
            this.CMD_RBOCHO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_RBOCHO.FlatAppearance.BorderSize = 0;
            this.CMD_RBOCHO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_RBOCHO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_RBOCHO.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_RBOCHO.Location = new System.Drawing.Point(24, 227);
            this.CMD_RBOCHO.Name = "CMD_RBOCHO";
            this.CMD_RBOCHO.Size = new System.Drawing.Size(305, 73);
            this.CMD_RBOCHO.TabIndex = 2;
            this.CMD_RBOCHO.Text = "VIVIENDA 8";
            this.CMD_RBOCHO.UseVisualStyleBackColor = true;
            this.CMD_RBOCHO.Click += new System.EventHandler(this.CMD_RBOCHO_Click);
            // 
            // CMD_BRLEASING
            // 
            this.CMD_BRLEASING.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CMD_BRLEASING.BackgroundImage")));
            this.CMD_BRLEASING.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_BRLEASING.FlatAppearance.BorderSize = 0;
            this.CMD_BRLEASING.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_BRLEASING.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_BRLEASING.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_BRLEASING.Location = new System.Drawing.Point(24, 148);
            this.CMD_BRLEASING.Name = "CMD_BRLEASING";
            this.CMD_BRLEASING.Size = new System.Drawing.Size(305, 73);
            this.CMD_BRLEASING.TabIndex = 1;
            this.CMD_BRLEASING.Text = "VIVIENDA LEASING";
            this.CMD_BRLEASING.UseVisualStyleBackColor = true;
            this.CMD_BRLEASING.Click += new System.EventHandler(this.CMD_BRLEASING_Click);
            // 
            // pnl_decla
            // 
            this.pnl_decla.BackColor = System.Drawing.Color.Transparent;
            this.pnl_decla.Controls.Add(this.panel6);
            this.pnl_decla.Controls.Add(this.cmd_imprimir_dela);
            this.pnl_decla.Controls.Add(this.panel10);
            this.pnl_decla.Location = new System.Drawing.Point(434, 156);
            this.pnl_decla.Name = "pnl_decla";
            this.pnl_decla.Size = new System.Drawing.Size(584, 717);
            this.pnl_decla.TabIndex = 11;
            this.pnl_decla.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(584, 63);
            this.panel6.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(108, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(392, 45);
            this.label2.TabIndex = 0;
            this.label2.Text = "Declaración de Renta";
            // 
            // cmd_imprimir_dela
            // 
            this.cmd_imprimir_dela.BackColor = System.Drawing.Color.Transparent;
            this.cmd_imprimir_dela.BackgroundImage = global::FENIX_KIOSCO.Properties.Resources.B_Menu;
            this.cmd_imprimir_dela.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmd_imprimir_dela.FlatAppearance.BorderSize = 0;
            this.cmd_imprimir_dela.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.cmd_imprimir_dela.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmd_imprimir_dela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmd_imprimir_dela.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmd_imprimir_dela.ForeColor = System.Drawing.Color.Transparent;
            this.cmd_imprimir_dela.Location = new System.Drawing.Point(225, 643);
            this.cmd_imprimir_dela.Name = "cmd_imprimir_dela";
            this.cmd_imprimir_dela.Size = new System.Drawing.Size(163, 68);
            this.cmd_imprimir_dela.TabIndex = 5;
            this.cmd_imprimir_dela.Text = "IMPRIMIR";
            this.cmd_imprimir_dela.UseVisualStyleBackColor = false;
            this.cmd_imprimir_dela.Click += new System.EventHandler(this.cmd_imprimir_dela_Click);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Silver;
            this.panel10.Controls.Add(this.webBrowser1);
            this.panel10.Location = new System.Drawing.Point(50, 72);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(490, 554);
            this.panel10.TabIndex = 3;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(490, 554);
            this.webBrowser1.TabIndex = 18;
            // 
            // CMD_imprimirEstTramite
            // 
            this.CMD_imprimirEstTramite.BackColor = System.Drawing.Color.Transparent;
            this.CMD_imprimirEstTramite.BackgroundImage = global::FENIX_KIOSCO.Properties.Resources.B_Menu;
            this.CMD_imprimirEstTramite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CMD_imprimirEstTramite.FlatAppearance.BorderSize = 0;
            this.CMD_imprimirEstTramite.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.CMD_imprimirEstTramite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CMD_imprimirEstTramite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CMD_imprimirEstTramite.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMD_imprimirEstTramite.ForeColor = System.Drawing.Color.Transparent;
            this.CMD_imprimirEstTramite.Location = new System.Drawing.Point(131, 525);
            this.CMD_imprimirEstTramite.Name = "CMD_imprimirEstTramite";
            this.CMD_imprimirEstTramite.Size = new System.Drawing.Size(163, 68);
            this.CMD_imprimirEstTramite.TabIndex = 5;
            this.CMD_imprimirEstTramite.Text = "IMPRIMIR";
            this.CMD_imprimirEstTramite.UseVisualStyleBackColor = false;
            this.CMD_imprimirEstTramite.Click += new System.EventHandler(this.CMD_imprimirEstTramite_Click);
            // 
            // pnl_estadoTramitTitle
            // 
            this.pnl_estadoTramitTitle.BackColor = System.Drawing.Color.Transparent;
            this.pnl_estadoTramitTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_estadoTramitTitle.Controls.Add(this.lbl_EstadoTramite);
            this.pnl_estadoTramitTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_estadoTramitTitle.Location = new System.Drawing.Point(0, 0);
            this.pnl_estadoTramitTitle.Name = "pnl_estadoTramitTitle";
            this.pnl_estadoTramitTitle.Size = new System.Drawing.Size(149, 63);
            this.pnl_estadoTramitTitle.TabIndex = 2;
            // 
            // lbl_EstadoTramite
            // 
            this.lbl_EstadoTramite.AutoSize = true;
            this.lbl_EstadoTramite.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EstadoTramite.Font = new System.Drawing.Font("Modern No. 20", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EstadoTramite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_EstadoTramite.Location = new System.Drawing.Point(55, 7);
            this.lbl_EstadoTramite.Name = "lbl_EstadoTramite";
            this.lbl_EstadoTramite.Size = new System.Drawing.Size(338, 45);
            this.lbl_EstadoTramite.TabIndex = 0;
            this.lbl_EstadoTramite.Text = "Estado de Trámite";
            // 
            // pnl_ESTADO_TRAMITE
            // 
            this.pnl_ESTADO_TRAMITE.BackColor = System.Drawing.Color.Transparent;
            this.pnl_ESTADO_TRAMITE.Controls.Add(this.pnl_estadoTramitTitle);
            this.pnl_ESTADO_TRAMITE.Controls.Add(this.CMD_imprimirEstTramite);
            this.pnl_ESTADO_TRAMITE.Controls.Add(this.panel3);
            this.pnl_ESTADO_TRAMITE.Location = new System.Drawing.Point(15, 144);
            this.pnl_ESTADO_TRAMITE.Name = "pnl_ESTADO_TRAMITE";
            this.pnl_ESTADO_TRAMITE.Size = new System.Drawing.Size(149, 141);
            this.pnl_ESTADO_TRAMITE.TabIndex = 10;
            this.pnl_ESTADO_TRAMITE.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.rTxt_Tramites);
            this.panel3.Location = new System.Drawing.Point(14, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(402, 432);
            this.panel3.TabIndex = 3;
            // 
            // rTxt_Tramites
            // 
            this.rTxt_Tramites.BackColor = System.Drawing.Color.Silver;
            this.rTxt_Tramites.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTxt_Tramites.Location = new System.Drawing.Point(14, 14);
            this.rTxt_Tramites.Name = "rTxt_Tramites";
            this.rTxt_Tramites.Size = new System.Drawing.Size(371, 403);
            this.rTxt_Tramites.TabIndex = 17;
            this.rTxt_Tramites.Text = "";
            // 
            // Frm_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::FENIX_KIOSCO.Properties.Resources.Fondo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(4576, 1034);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_decla);
            this.Controls.Add(this.pnl_teclado);
            this.Controls.Add(this.pnl_Turno);
            this.Controls.Add(this.pnl_EstadoCuenta);
            this.Controls.Add(this.pnl_huella);
            this.Controls.Add(this.pnl_Inicio);
            this.Controls.Add(this.CMD_Cancelar);
            this.Controls.Add(this.pnl_TurnosNAfiliados);
            this.Controls.Add(this.pnlSapo);
            this.Controls.Add(this.pnl_ESTADO_TRAMITE);
            this.Controls.Add(this.pnl_Sombras);
            this.Controls.Add(this.CMD_Regresar);
            this.Controls.Add(this.CMD_Prioritario);
            this.Controls.Add(this.pnl_certPago);
            this.Controls.Add(this.pnl_TurnosInfoAfiliado);
            this.Controls.Add(this.pnl_NoAfiliado);
            this.Controls.Add(this.pnlTurnosBeneficiarios);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Frm_Principal";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Frm_Principal_Load);
            this.Shown += new System.EventHandler(this.Frm_Principal_Shown);
            this.pnl_infoAfiliado.ResumeLayout(false);
            this.pnl_infoAfiliado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_Fotografia)).EndInit();
            this.pnl_huella.ResumeLayout(false);
            this.pnl_huella.PerformLayout();
            this.pnl_vistaHuella.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptb_HuellaEstatica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_HuelaPrelim)).EndInit();
            this.panel4.ResumeLayout(false);
            this.pnl_EstadoCuenta.ResumeLayout(false);
            this.pnl_EstadoCuenta1.ResumeLayout(false);
            this.pnl_EstadoCuenta1.PerformLayout();
            this.pnl_TituloCuentas.ResumeLayout(false);
            this.pnl_TituloCuentas.PerformLayout();
            this.pnl_Turno.ResumeLayout(false);
            this.pnl_Turno.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_Logo)).EndInit();
            this.pnl_teclado.ResumeLayout(false);
            this.pnl_teclado.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnl_certPago.ResumeLayout(false);
            this.pnl_certPagContain.ResumeLayout(false);
            this.pnl_certPag_title.ResumeLayout(false);
            this.pnl_certPag_title.PerformLayout();
            this.pnlSapo.ResumeLayout(false);
            this.pnlSapo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.deviceTypesGroupBox.ResumeLayout(false);
            this.deviceTypesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_Inicio.ResumeLayout(false);
            this.pnl_Inicio.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnl_NoAfiliado.ResumeLayout(false);
            this.pnl_NoAfiliado.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.pnl_turnosTitle.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnl_TurnosInfoAfiliado.ResumeLayout(false);
            this.pnl_TurnosInfoAfiliado.PerformLayout();
            this.pnl_TurnosNAfiliados.ResumeLayout(false);
            this.pnl_TurnosNAfiliados.PerformLayout();
            this.pnlTurnosBeneficiarios.ResumeLayout(false);
            this.pnlTurnosBeneficiarios.PerformLayout();
            this.pnl_decla.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.pnl_estadoTramitTitle.ResumeLayout(false);
            this.pnl_estadoTramitTitle.PerformLayout();
            this.pnl_ESTADO_TRAMITE.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_infoAfiliado;
        private System.Windows.Forms.PictureBox ptb_Fotografia;
        private System.Windows.Forms.Label lbl_Cedula;
        private System.Windows.Forms.Label lbl_Nombres;
        private System.Windows.Forms.Button CMD_Turnos;
        private System.Windows.Forms.Panel pnl_huella;
        private System.Windows.Forms.Label lbl_respuesta;
        private System.Windows.Forms.Panel pnl_vistaHuella;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CMD_Certificado;
        private System.Windows.Forms.Button CMD_EstadoTramite;
        private System.Windows.Forms.Button CMD_EstadCuenta;
        private System.Windows.Forms.Panel pnl_EstadoCuenta;
        private System.Windows.Forms.Panel pnl_TituloCuentas;
        private System.Windows.Forms.Label lbl_EstadoCuentas;
        private System.Windows.Forms.Button CMD_ImprimirEstado;
        private System.Windows.Forms.Panel pnl_EstadoCuenta1;
        private System.Windows.Forms.Label lbl_esta_cuentas_1;
        private System.Windows.Forms.Button CMD_Regresar;
        private System.Windows.Forms.Button CMD_Cancelar;
        private System.Windows.Forms.Panel pnl_Sombras;
        private System.Windows.Forms.Panel pnl_certPago;
        private System.Windows.Forms.Button CMD_ImprimirCerti;
        private System.Windows.Forms.Panel pnl_certPagContain;
        private System.Windows.Forms.RichTextBox rTxt_Certificacion;
        private System.Windows.Forms.Panel pnl_certPag_title;
        private System.Windows.Forms.Label lbl_certPag_title;
        private System.Windows.Forms.Panel pnl_Turno;
        private System.Windows.Forms.Label lbl_TipoTurno;
        private System.Windows.Forms.Label lbl_Turno;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PictureBox ptb_Logo;
        private System.Windows.Forms.Label lbl_NombreAfi;
        private System.Windows.Forms.Label lbl_ceduAfi;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Windows.Forms.PictureBox ptb_HuelaPrelim;
        private System.Windows.Forms.Timer ObjPr_timer;
        private System.Windows.Forms.Timer ObjPr_timerHuella;
        private System.Windows.Forms.PictureBox ptb_HuellaEstatica;
        private System.Windows.Forms.Label LblPr_version;
        private System.Windows.Forms.Label lbl_tipoServicio;
        private System.Windows.Forms.Label lbl_HoraTurno;
        private System.Windows.Forms.Panel pnlSapo;
        private System.Windows.Forms.Label LblVersion;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button CMD_Prioritario;
        private System.Windows.Forms.Label lblMsjs;
        private System.Windows.Forms.Label lbl_Alertas;
        private System.Windows.Forms.Timer tmCierraNiegaTurno;
        private System.Windows.Forms.Timer tmGeneral;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.CheckBox cbAutoupdate;
        private System.Windows.Forms.CheckBox cbAutoplug;
        private System.Windows.Forms.GroupBox deviceTypesGroupBox;
        private System.Windows.Forms.CheckBox captureDeviceCheckBox;
        private System.Windows.Forms.CheckBox microphoneCheckBox;
        private System.Windows.Forms.CheckBox anyCheckBox;
        private System.Windows.Forms.CheckBox fScannerCheckBox;
        private System.Windows.Forms.CheckBox cameraCheckBox;
        private System.Windows.Forms.CheckBox irisScannerCheckBox;
        private System.Windows.Forms.CheckBox fingerScannerCheckBox;
        private System.Windows.Forms.CheckBox biometricDeviceCheckBox;
        private System.Windows.Forms.ComboBox formatsComboBox;
        private System.Windows.Forms.ComboBox biometricDevicePositionComboBox;
        private System.Windows.Forms.CheckBox cbAutomatic;
        private System.Windows.Forms.TreeView deviceTreeView;
        private System.Windows.Forms.ComboBox biometricDeviceImpressionTypeComboBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.PropertyGrid devicePropertyGrid;
        private System.Windows.Forms.Panel pnl_teclado;
        private System.Windows.Forms.Button cmd_3;
        private System.Windows.Forms.Button cmd_2;
        private System.Windows.Forms.Button cmd_1;
        private System.Windows.Forms.Button cmd_6;
        private System.Windows.Forms.Button cmd_5;
        private System.Windows.Forms.Button cmd_4;
        private System.Windows.Forms.Button cmd_9;
        private System.Windows.Forms.Button cmd_8;
        private System.Windows.Forms.Button cmd_7;
        private System.Windows.Forms.Button cmd_Accept;
        private System.Windows.Forms.Button cmd_0;
        private System.Windows.Forms.Button cmd_Cancel;
        private System.Windows.Forms.Button cmd_consulta;
        private System.Windows.Forms.Label lbl_Identificacion;
        private System.Windows.Forms.TextBox txt_Identificacion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnl_Inicio;
        private System.Windows.Forms.Button cmd_NoAfiliado;
        private System.Windows.Forms.Button cmd_Afiliado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnl_NoAfiliado;
        private System.Windows.Forms.Button cmd_Beneficiario;
        private System.Windows.Forms.Button cmd_Apoderado;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button CMD_TurnosRadicacion;
        private System.Windows.Forms.Button CMD_RENTA;
        private System.Windows.Forms.Panel pnl_turnosTitle;
        private System.Windows.Forms.Label lbl_TurnoTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CMD_Tramite;
        private System.Windows.Forms.Button CMD_Vivienda14;
        private System.Windows.Forms.Button CMD_Vivienda8;
        private System.Windows.Forms.Button CMD_Leasing;
        private System.Windows.Forms.Button CMD_Heroes;
        private System.Windows.Forms.Button CMD_Agenda_cita;
        private System.Windows.Forms.Button CMD_Futuro;
        private System.Windows.Forms.Button CMD_Cuenta;
        private System.Windows.Forms.Button CMD_Atencion_cita;
        private System.Windows.Forms.Panel pnl_TurnosInfoAfiliado;
        private System.Windows.Forms.Button CMD_Turnofuturoafiliado;
        private System.Windows.Forms.Button CMD_Turnopretramiteafiliado;
        private System.Windows.Forms.Button CMD_turnoleasingafiliado;
        private System.Windows.Forms.Button CDM_Turno8afiliado;
        private System.Windows.Forms.Button CMD_Turno14afiliado;
        private System.Windows.Forms.Button CMD_Turnotramiteafiliado;
        private System.Windows.Forms.Panel pnl_TurnosNAfiliados;
        private System.Windows.Forms.Button CMD_RadiNFuturo;
        private System.Windows.Forms.Button CMD_RadiNFondo;
        private System.Windows.Forms.Button CMD_RadiViviendaN14;
        private System.Windows.Forms.Button CMD_RadiViviendaN8;
        private System.Windows.Forms.Button CMD_RadiViviendaNLeasing;
        private System.Windows.Forms.Button CMD_ViviendaNFuturo;
        private System.Windows.Forms.Button CMD_ViviendaNFondo;
        private System.Windows.Forms.Button CMD_ViviendaN14;
        private System.Windows.Forms.Button CMD_ViviendaN8;
        private System.Windows.Forms.Button CMD_ViviendaNLeasing;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblnoafi;
        private System.Windows.Forms.Panel pnl_decla;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmd_imprimir_dela;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button CMD_imprimirEstTramite;
        private System.Windows.Forms.Panel pnl_estadoTramitTitle;
        private System.Windows.Forms.Label lbl_EstadoTramite;
        private System.Windows.Forms.Panel pnl_ESTADO_TRAMITE;
        private System.Windows.Forms.WebBrowser webBrowser3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox rTxt_Tramites;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LBL_MODO;
        private System.Windows.Forms.Label lbl_ceduNAfi;
        private System.Windows.Forms.Label lbl_NombreNoAfi;
        private System.Windows.Forms.Button CMD_RAFONDOH;
        private System.Windows.Forms.Panel pnlTurnosBeneficiarios;
        private System.Windows.Forms.Button CMD_IBFUTURO;
        private System.Windows.Forms.Button CMD_IBFONDO;
        private System.Windows.Forms.Button CMD_IBCATORCE;
        private System.Windows.Forms.Button CMD_IBOCHO;
        private System.Windows.Forms.Button CMD_IBLEASING;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button CMD_RBFUTURO;
        private System.Windows.Forms.Button CMD_RBFONDO;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button CMD_RBOCHO;
        private System.Windows.Forms.Button CMD_BRLEASING;
    }
}

