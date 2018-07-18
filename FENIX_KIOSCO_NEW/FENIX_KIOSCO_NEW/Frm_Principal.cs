#region usings
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using _C_ProgRes;
using _C_Devices_N_4_3;
using _C_Fenix_Kiosko;
using System.Data.SqlClient;
using Neurotec.Images;
using Neurotec.Biometrics.Gui;
using Neurotec.Biometrics;
using System.Resources;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using FENIX_KIOSCO;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security;
using Newtonsoft.Json;
using Neurotec;
using Neurotec.Devices;
using Neurotec.Media;
using FENIX_KIOSCO.Properties;
using System.Configuration;
using FENIX_KIOSCO.MCDIntegrationServices;



//using _Kiosko_WS;
#endregion
//V. Ant
namespace FENIX_KIOSCO
{
    public partial class Frm_Principal : Form
    {
        #region campos privados
        /// <summary>
        /// Usuario de windows
        /// </summary>
        private String stPr_UsuarioWin = SystemInformation.UserName;
        /// <summary>
        /// Usuario de la aplicacion, por defecto se asume que es el mismo usuario actual de windows
        /// </summary>
        private String stPr_UsuarioApp = SystemInformation.UserName;
        /// <summary>
        /// Nobre del archivo .exe sin extension
        /// </summary>
        private String stPr_ExeName = System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath);
        /// <summary>
        /// El nombre de exe con la extension.
        /// </summary>
        private String stPr_ExeName_Exe = System.IO.Path.GetFileName(Application.ExecutablePath);
        /// <summary>
        /// Nombre de la Empresa donde esta instalada la aplicacion
        /// </summary>
        private String stPr_NombreEmpresaApp = "";
        /// <summary>
        /// Nombre del producto.
        /// </summary>
        private String stPr_NombreApp = Application.ProductName;
        /// <summary>
        /// Version de la aplicacion
        /// </summary>
        private String stPr_VersionApp = Application.ProductVersion;
        /// <summary>
        /// El path de la aplicacion
        /// </summary>
        private String stPr_PathApp = Application.StartupPath;
        /// <summary>
        /// Nombre del archivo de log
        /// </summary>
        private String stPr_ArchivoLog = "";
        /// <summary>
        /// Directorio de windows
        /// </summary>
        private String stPr_WinDir = Environment.GetEnvironmentVariable("windir");
        /// <summary>
        /// Nombre del archivo de configuracion de la aplicacion
        /// </summary>
        private String stPr_Archivo_Confg = "";
        /// <summary>
        /// Nombre o dir IP del Servidor aDirector
        /// </summary>
        private String stPr_Serv = "";
        /// <summary>
        /// Puerto TCP del servidor aDirector
        /// </summary>
        private String StPr_inPuerto = "";
        /// <summary>
        /// Ruta base para obtener biometrias desde el servidor
        /// </summary>
        private String stPr_RutaBase = "";
        /// <summary>
        /// Parametros de la base de datos
        /// </summary>
        private String[] stPr_ParamBd;
        private String stPr_modo = "";
        //private String StPr_TOKEN = "";
        /// <summary>
        /// variable para validar si el afiliado tiene biometria
        /// </summary>
        private bool blPr_HayBio = true;
        /// <summary>
        /// Variable para indicar cuales biomtrias faltan
        /// </summary>
        private String stPr_BioFalta = "";
        /// <summary>
        /// Variable para controlar el estado del objeto TCP al traer las biometrias
        /// -9 Ejecutando, -1 ERROR, 1 Ok termine de traer
        /// </summary>
        //private bool terminedetraer = false;
        private int terminedetraer = 1;
        /// <summary>
        /// Variable para controlar el estado del objeto TCP al traer las biometrias
        /// </summary>
        private bool blPr_NoGA2 = false;
        /// <summary>
        /// objeto con la informacion de version de la aplicacion
        /// </summary>
        private FileVersionInfo VersionInfo = FileVersionInfo.GetVersionInfo(System.IO.Path.GetFileName(Application.ExecutablePath));
        /// <summary>
        /// variable con el tiempo de espera para el temporizador
        /// </summary>
        private int inPr_TimeOff = 20000;

        private int SP_TRAMITE = 1;
        //
        /// <summary>
        /// Nombre o dir IP del Servidor Digiturno
        /// </summary>
        private String stPr_ServDigi = "";

        private String StPr_InPuertoDig = "";



        private String stPr_MensajeError = "";

        //
        private ClasX_AppKiosko Obj_AppKiosko = null;
        private const String NOM_CLASE = "Frm_Principal";
        /// <summary>
        /// Maneja el tiempo maximo que espera el huellero para poder hacer una toma de huella
        /// </summary>
        private int TimeOutHuellero = 8000;
        private NDeviceManager _deviceManager;
        private readonly List<Frm_Principal> _captureForms = new List<Frm_Principal>();
        private bool bl_ExisteHuellero = false;

        #region info afiliado
        //info gral afi
        private int inPr_AO01ID;
        private ulong inPr_A001NUM_IDENTIFICACION;
        private String stPr_A001PRIMER_APELLIDO = "";
        private String stPr_A001SEGUNDO_APELLIDO = "";
        private String stPr_A001PRIMER_NOMBRE = "";
        private String stPr_A001SEGUNDO_NOMBRE = "";
        private bool blPr_desactivaturnosP = false;
        private bool blPr_NoAfiliado = false;
        private bool blPr_Beneficiario = false;
        private bool blPr_Apoderado = false;
        private bool blPr_Autorizado = false;
        private bool blPr_apo = false;
        private bool blPr_afi = false;
        private ulong inPr_DocNoAfiiado;
        #endregion


        #region Kioscos
        string st_Oficina1 = "CAN";
        string st_Oficina2 = "VENECIA";
        string st_Oficina3 = "BUCARAMANGA";
        string st_Oficina4 = "BARRANQUILLA";
        string st_Oficina5 = "CALI";
        string st_Oficina6 = "CARTAGENA";
        string st_Oficina7 = "FLORENCIA";
        string st_Oficina8 = "IBAGUE";
        string st_Oficina9 = "MEDELLIN";
        string st_Oficina10 = "UNIDAD";
        string st_Oficina11 = "KIOSCO";

        #endregion

        #region constantes

        #region mensajes predefinidos
        /// <summary>
        /// ¿ Desea Salir de la Aplicación ?
        /// </summary>
        private const String MENSAJE_APP_01 = "¿ Desea Salir de la Aplicación ?";
        /// <summary>
        /// No se tiene acceso al archivo de configuración de la aplicación.
        /// </summary>
        private const String MENSAJE_APP_02 = "No se tiene acceso al archivo de configuración de la aplicación.";
        /// <summary>
        /// Debe revisar la instalación de la aplicación.
        /// </summary>
        private const String MENSAJE_APP_03 = "Debe revisar la instalación de la aplicación.";
        /// <summary>
        /// No se tiene acceso a los servidores de consulta.
        /// </summary>
        private const String MENSAJE_APP_04 = "No se tiene acceso a los servidores de consulta.";
        /// <summary>
        /// Mensaje para log "CORRECTO"
        /// </summary>
        private const String MENSAJE_APP_05 = "CORRECTO.";
        /// <summary>
        /// Mensaje para log "FALLIDO"
        /// </summary>
        private const String MENSAJE_APP_06 = "FALLIDO.";
        /// <summary>
        /// Mensaje para log "Confirmacion num doc"
        /// </summary>
        private const String MENSAJE_APP_07 = "Su número de documento es: ";
        /// <summary> AGR 21072016
        /// Mensaje de inicio si no hay conexión a servidores
        /// </summary>
        private const String MENSAJE_APP_08 = "Error de conexión a servidores. Pongase en contacto con Centro de Servicio";
        /// <summary> AGR 21072016
        /// Mensaje complementario cuando huella no coincide con documen to
        /// </summary>
        private const String MENSAJE_APP_09 = "El registro de huella no coincide con N. de documento: ";
        /// <summary> AGR 21072016
        /// Mensaje complementario cuando huella no coincide con documento
        /// </summary>
        private const String MENSAJE_APP_10 = ". Por favor ingrese una huella valida";
        /// <summary>AGR 22072016
        /// Mensaje cuando no existen huellas de afiliado o tienen problemas
        /// </summary>
        private const String MENSAJE_APP_11 = "El afiliado con número de documento: ";
        /// <summary> AGR 22072016
        /// Mensaje complementario cuando no existen huellas de afiliado o el template tiene problemas
        /// </summary>
        private const String MENSAJE_APP_12 = ". Tiene inconvenientes con las huellas registradas. Se asignará turno B.";
        /// <summary>AGR 22072016
        /// Mensaje cuando no existen huellas de afiliado o el template tiene problemas
        /// </summary>
        private const String MENSAJE_APP_13 = "El afiliado con número de documento: ";
        /// <summary> AGR 22072016
        /// Mensaje complementario cuando no existe biometria de afiliado o falta alguna imagen
        /// </summary>
        private const String MENSAJE_APP_14 = ". Tiene inconvenientes con su biometría. Se asignará turno B.";
        /// <summary>AGR 22072016
        /// Mensaje cuando el número de documento ingresado no existe en la BD de GA2
        /// </summary>
        private const String MENSAJE_APP_15 = "El número de documento ingresado: ";
        /// <summary> AGR 22072016
        /// Mensaje complementario cuando el número de documento ingresado no existe en la BD de GA2
        /// </summary>
        private const String MENSAJE_APP_16 = ". No pertenece a un afiliado. Intente de nuevo";
        /// <summary>AGR 22072016
        /// Mensaje cuando el número de documento ingresado no existe en la BD de GA2
        /// </summary>
        private const String MENSAJE_APP_17 = "El número de documento ingresado: ";
        /// <summary> AGR 22072016
        /// Mensaje complementario cuando el número de documento ingresado no existe en la BD de GA2
        /// </summary>
        private const String MENSAJE_APP_18 = ". No pertenece a un afiliado. Se asignará turno preferencial.";
        /// <summary>
        /// 
        /// </summary>
        private const String MENSAJE_APP_19 = "Error de conexión a Fenix. No podemos procesar su solicitud en este momento, se asignará turno de información general.";
        /// <summary>
        /// 
        /// </summary>
        private const String MENSAJE_APP_20 = "Error de conexión a Fenix. No podemos procesar su solicitud en este momento, se asignará turno de información preferencial.";
        /// <summary>
        /// 
        /// </summary>
        private const String MENSAJE_APP_21 = "En estos momentos no podemos atender su solicitud. Disculpe las molestias.";
        /// <summary>
        /// 
        /// </summary>
        private const String MENSAJE_APP_22 = ". No posee registro de huellas en Base de Datos. Se asignará turno B.";
        #endregion

        #region pantallas
        private const String PANEL_TECLADO_INICIAL = "pnl_teclado";
        private const String PANEL_ESTADO_CUENTA = "pnl_EstadoCuenta";
        private const String PANEL_CAPTURA_HUELLA = "pnl_huella";
        private const String PANEL_AFILIADO = "pnl_afiliado";
        private const String PANEL_CONSULTAS = "pnl_Consultas";
        private const String BOTON_CANCELAR_PANTALLA = "CMD_Cancelar";
        private const String BOTON_REGRESAR_PANTALLAS = "CMD_Regresar";
        private const String PANEL_ESTADO_TRAMITE = "pnl_ESTADO_TRAMITE";
        private const String PANEL_DECLARACION = "pnl_decla";
        private const String PANEL_CERTIFICAO_PAGO = "pnl_certPago";
        private const String PANEL_TURNOSINFOAFILIADO = "pnl_Turnos";
        private const String PANEL_INICIO = "pnl_Inicio";
        private const String PANEL_TURNOSRADICACION = "pnl_TurnosRadicacion";
        private const String PANEL_NOAFILIADO = "pnl_NoAfiliado";
        private const String stPr_ESTADOCUENTA = "ESTADO DE CUENTA";
        private const String stPr_ESTADOTRAMITE = "ESTADO DE TRAMITE";
        private const String stPr_DECLARACION = "CERTIFICADO DECLARACIÓN DE RENTA";
        private const String stPr_CERTIFICADOPAGO = "CERTIFICADO DE PAGO";
        private const String PANEL_INFONOAFILIADO = "pnl_TurnosNAfiliados";
        private const String PANEL_INFOBENEFICIARIO = "pnlTurnosBeneficiarios";
        private const String PANEL_CEDULA = "pnl_infoAfiliado";

      
        #endregion

        #region turnos

        //private const string stPrc_FONDO_SOLIDARIDAD = "Fondo_de_Solidaridad";
        //private const string stPrc_SALDOS_CESANTIAS = "Saldos_y/o_Cesantias";
        //private const string stPrc_RETIRO_INSTITUCION = "Retiro_de_la_Institucion";
        //private const string stPrc_PRIMER_PAGO = "Primer_Pago";
        //private const string stPrc_SEGUNDO_PAGO = "Segundo_Pago";
        //private const string stPrc_MASVI = "MASVI";
       
        
        //NUEVOS TURNOS

        //TURNOS AFILIADOS INFORMACION GENERAL
        private const string stPrc_IVIVIENDA_CATORCE = "I VCatorce";
        private const string stPrc_IVIVIENDA_OCHO = "I VOcho";
        private const string stPrc_PREFERENCIAL = "PREFERENCIAL";
        private const string stPrc_PRIORITARIO = "Prioritario";
        private const string stPrc_IVIVIENDA_LEASING = "I VLeasing";
        private const string stPrc_IHEROES = "I FS.Heroes";
        private const string stPrc_PRETRAMITE = "Pretramite";
        private const string stPrc_BIOMETRIA = "Biometrias";
        private const string stPrc_INFORMACION_GENERAL = "Informacion General";
        private const string stPrc_ITRAMITE_LINEA = "I Tramite en linea";
        private const string stPrc_IAGENDACITA = "I Age. cita";
        private const string stPrc_IAGENDACITAP = "I Atencion Cita P";
        private const string stPrc_IFUTUROCESANTIA = "I FCesantias";
        private const string stPrc_ICUENTA = "I INFORMACION DE CUENTA";
        //
        private const string stPrc_FONDO = "Fondo de Solidaridad ";
        private const string stPrcCMD_Cuenta = "Cuenta Individual";
        //TURNOS AFILIADOS RADICACION
        private const string stPrc_RVIVIENDA_LEASING = "R VLeasing";
        private const string stPrc_RVIVIENDA_OCHO = "R VOcho";
        private const string stPrc_RVIVIENDA_CATORCE = "R VCatorce";
        private const string stPrc_RHEROES = "R FS.Heroes";
        private const string stPrc_RFUTURO = "R FCesantias";
        private const string stPrc_RPRETRAMITE = "R Pre tramite";
        private const string stPrc_RTRAMITE_LINEA = "R Tramite en linea";
        //TURNOS NO AFILIADOS APODERADOS RADICACION
        private const string stPrc_VIVIENDA_CATORCENA = "ViviendaN_14";
        private const string stPrc_ARVIVIENDA_LEASING = " ARB Vlesing";
        private const string stPrc_ARVIVIENDA_OCHO = "ARB Vocho";
        private const string stPrc_ARVIVIENDA_CATORCE = "ARB Vcatorce";
        private const string stPrc_ARHEROES = "ARB FS.Heroes";
        private const string stPrc_ARFUTURO = "ARB Fcesantias";
        //TURNOS NO AFILIADOS APODERADOS INFORMACION
        private const string stPrc_AILEASING = "AIB Vleasing";
        private const string stPrc_AIOCHO = "AIB Vocho";
        private const string stPrc_AICATORCE = "AIB Vcatorce";
        private const string stPrc_AIHEROES = "AIB FS.Heroes";
        private const string stPrc_AIFUTURO = "AIB Fcesantias";
        //TURNOS NO AFILIADOS BENEFICIARIOS RADICACION
        private const string stPrc_BRLEASING = "BRB Vlesing";
        private const string stPrc_BROCHO = "BRB Vocho";
        private const string stPrc_BRCATORCE = "BRB Vcatorce";
        private const string stPrc_BRHEROES = "BRB FS.Heroes";
        private const string stPrc_BRFUTURO = "BRB Fcesantias";
        //TURNOS NO AFILIADOS BENEFICIARIOS INFORMACION
        private const string stPrc_BILEASING = "BIB Vleasing";
        private const string stPrc_BIOCHO = "BIB Vocho";
        private const string stPrc_BICATORCE = "BIB Vcatorce";
        private const string stPrc_BIHEROES = "BIB FS.Heroes";
        private const string stPrc_BIFUTURO = "BIB Fcesantias";
        #endregion



        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Objetos
        /// <summary>
        /// Objeto para el Manejo del archivo de Configuracion de la aplicacion.
        /// </summary>
        private ClasX_Config ObjPr_ConfigApp = null;
        /// <summary>
        /// Objeto para el manejo de log de la aplicacion
        /// </summary>
        private ClasX_EventLog ObjPr_EventLog = null;
        /// <summary>
        /// Objeto para el manejo del dispositivo de captura de huellas
        /// </summary>
        private ClasX_FingerPrint_Device objPr_huellero_Iden_Veri = null;
        /// <summary>
        /// Vista previa de la huella capturada
        /// </summary>
        private NFView objPr_vista = null;
        /// <summary>
        /// Objeto para el manejo de utilidades
        /// </summary>
        private ClasX_Utils objPr_Utils = null;
        /// <summary>
        /// Hilo para traer la biometria desde el servidor
        /// </summary>
        private Thread traerbio;
        /// <summary>
        /// Objeto para conectar con servicios CIEL
        /// </summary>
       // private ClasX_Web_Service objPr_WS = null;

        //private ClasX_Web_Services_Reportes objPr_WS1 = null;

        #endregion

        #endregion

        //public Form1()
        //{
        //        Thread tardar = new Thread(new ThreadStart(pantalla));
        //        tardar.Start();
        //        Thread.Sleep(1000);
        //        InitializeComponent();
        //        Inicializar();
        //        tardar.Abort();
        //        CheckForIllegalCrossThreadCalls = false;
        //}
        public Frm_Principal()
        {
            try
            {
                InitializeComponent();
                OnDeviceManagerChanged();
                NCore.ErrorSuppressed += new EventHandler<ErrorSuppressedEventArgs>(NCore_ErrorSuppressed);
                NewDeviceManager();
                //CheckForIllegalCrossThreadCalls = true;
                //CargaSplash();
                Inicializar();
            }
            catch (AccessViolationException ano)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "Form1()", "", ano.Message, "", "");
            }
            catch (Exception e)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "Form1()", "", e.Message, "", "");
            }
        }


        private void Inicializar()
        {
            try
            {

                String stL_PathArchivos_LogConf = "";
                String stL_PathArchivosLog = "";
                String stL_PathAux = "";
                //
                this.Location = Screen.PrimaryScreen.WorkingArea.Location;
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                this.pnl_Sombras.Location = new System.Drawing.Point(381, 340);
                this.pnl_Sombras.Size = new System.Drawing.Size(569, 675);
                //pnl_teclado.Left = ((this.Width - pnl_teclado.Width) / 2);
                //pnl_teclado.Top = ((this.Height - pnl_teclado.Height) / 2);
                //encima sempreEnc = new encima();
                //sempreEnc.SiempreEncima(this.Handle.ToInt32());
                //this.pnl_teclado.Location = new System.Drawing.Point(450, 167);

                this.pnl_teclado.Size = new System.Drawing.Size(430, 640);
                cargaPantallas();
                stL_PathArchivos_LogConf = stPr_PathApp;
                stL_PathAux = "C:\\" + stPr_ExeName + "\\" + stPr_ExeName + ".conf";
                if (File.Exists(stL_PathAux))
                {
                    stPr_PathApp = "C:\\" + stPr_ExeName;
                    stL_PathArchivos_LogConf = stPr_PathApp;
                }
                else
                {
                    stL_PathAux = stPr_WinDir + "\\" + stPr_ExeName + ".conf";
                    if (File.Exists(stL_PathAux))
                    {
                        stL_PathArchivos_LogConf = stPr_WinDir;
                    }
                }
                stL_PathArchivosLog = stPr_PathApp + "\\Logs";
                if (!Directory.Exists(stL_PathArchivosLog))
                    Directory.CreateDirectory(stL_PathArchivosLog);
                stPr_ArchivoLog = stL_PathArchivosLog + "\\" + stPr_ExeName + ".log";
                stPr_Archivo_Confg = stL_PathArchivos_LogConf + "\\" + stPr_ExeName + ".conf";
                ObjPr_EventLog = new _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, false, true, false);
                //Strail 20112014
                ObjPr_EventLog.setTextErrLog("Inicializar(), Inciando KIOSCO Verion : " + VersionInfo.FileVersion);
                objPr_Utils = new ClasX_Utils(stPr_UsuarioApp, stPr_ArchivoLog);

                //
                ObjPr_EventLog.setTextErrLog("frm_Principal.Inicializar(), Buscando Archivo de configuracion");

                if (File.Exists(stPr_Archivo_Confg))
                {
                    Obj_AppKiosko = new ClasX_AppKiosko(stPr_UsuarioApp, stPr_ArchivoLog, stPr_Archivo_Confg, false, true, false);
                    //
                    ObjPr_ConfigApp = new _C_ProgRes.ClasX_Config(stPr_Archivo_Confg, stPr_ArchivoLog, stPr_ArchivoLog);
                    //

                    String stL_Timeoff = "";
                    String stL_cursor = "";
                    stPr_modo = "";
                    //
                    stPr_RutaBase = "";
                    SP_TRAMITE = 0;
                    //
                    stPr_ParamBd = new String[9];
                    //
                    LblPr_version.Text = VersionInfo.FileVersion;
                    //
                    ObjPr_EventLog.setTextErrLog("frm_Principal.Inicializar(), inicia Lectura Informacion Archivo de Configuracion");
                    Obj_AppKiosko.Lee_Info_Basica_Archivo_Config(ref stPr_Serv, ref stPr_ServDigi, ref StPr_InPuertoDig, ref stL_Timeoff, ref stL_cursor, ref stPr_modo, ref StPr_inPuerto, ref stPr_RutaBase, ref SP_TRAMITE, ref stPr_ParamBd, ref stPr_NombreEmpresaApp);
                    //
                    inPr_TimeOff = Convert.ToInt32(stL_Timeoff);
                    //
                    //Strail
                    //Time Out huellero
                    string stmptout = ObjPr_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "TimeOutHuellero");
                    if (stmptout.Length > 0)
                    {
                        TimeOutHuellero = Convert.ToInt32(stmptout);
                    }
                    //
                    if (stL_cursor.Contains("off"))
                    {
                        this.Cursor.Dispose();
                        Cursor.Hide();
                    }
                    //
                    LBL_MODO.Text = stPr_modo.ToUpper();
                    //

                    if (stPr_modo.Equals("lite"))
                    {
                        CMD_Turnos.Visible = false;
                        CMD_Prioritario.Visible = false;
                        cmd_NoAfiliado.Visible = false;
                    }
                    else
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.Inicializar(), Conectando webService DigTurno");
                        //objPr_WS = new ClasX_Web_Service(stPr_UsuarioApp, stPr_ArchivoLog, stPr_Archivo_Confg);
                    }

                    if (!stPr_modo.Equals("lite"))
                    {
                        ObjPr_EventLog.setTextErrLog("Iniciando Test de conexion a servidores modo (FULL).....");
                        //
                        Obj_AppKiosko.VerificaFenix_GA2(stPr_Serv, StPr_inPuerto, stPr_ParamBd, ref inPr_A001NUM_IDENTIFICACION, ref stPr_MensajeError);
                        //ObjPr_EventLog.setTextErrLog("Revisando Conexion GA2" + stPr_Serv + "puerto" + StPr_inPuerto + "parametros base de Datos" + stPr_ParamBd);
                        //
                        Obj_AppKiosko.TestDigiturno(stPr_ServDigi, StPr_InPuertoDig, ref stPr_MensajeError);
                        ObjPr_EventLog.setTextErrLog("frm_Principal.Inicializar(), Ingresando en modo FULL OK ");
                        //
                        if (stPr_MensajeError.Length > 1)
                        {
                            //AGR 21072016 NMuestra mensaje de confirmación cuando hay error de conexión.
                            string dialogResult = MyMessageBox.ShowBox(stPr_MensajeError, "", "Aceptar", "Cancelar", false, 155);
                            MyMessageBox.ActiveForm.Activate();
                            MyMessageBox.ActiveForm.BringToFront();
                            if (dialogResult.Equals("1"))
                            {
                                Application.Exit();
                            }


                            if (dialogResult.Equals("2"))
                            {
                                Application.Exit();
                            }
                            ObjPr_EventLog.setTextErrLog("frm_Principal.Inicializar(), ERROR " + stPr_MensajeError);
                        }
                    }
                    else
                    {
                        ObjPr_EventLog.setTextErrLog("Iniciando Test de conexion a servidores modo (LITE).....");
                        //
                        Obj_AppKiosko.VerificaFenix_GA2(stPr_Serv, StPr_inPuerto, stPr_ParamBd, ref inPr_A001NUM_IDENTIFICACION, ref stPr_MensajeError);
                        ObjPr_EventLog.setTextErrLog("frm_Principal.Inicializar(), Ingresando en modo LITE OK ");
                        //
                        if (stPr_MensajeError.Length > 1)
                        {
                            MessageBox.Show(stPr_MensajeError, "Alerta!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            ObjPr_EventLog.setTextErrLog("frm_Principal.Inicializar(), ERROR " + stPr_MensajeError);
                        }
                    }
                }
                else
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.Inicializar(), ERROR No tengo acceso al archivo de configuracion ");
                    //Strail 20112014 MessageBox.Show(MENSAJE_APP_02 + ClasX_Constans.NEW_LINE + MENSAJE_APP_03 + ClasX_Constans.NEW_LINE + ClasX_Constans.NEW_LINE + ClasX_Constans.MENSAJE_22, ClasX_Constans.MENSAJE_5,MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //
                bool visible = pnl_teclado.Visible;
                if (!this.pnl_teclado.Visible)
                {
                    this.CMD_Regresar.Visible = false;
                    this.CMD_Cancelar.Visible = false;
                    this.CMD_Prioritario.Visible = false;
                }
                borraDirTemporales();

                ObjPr_EventLog.setTextErrLog("Inicializar(), Terminando OK");
            }
            catch (Exception e)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "Inicializar", "", e.Message, "", "");
            }
        }


        private void poneNumero(string numero)
        {
            try
            {
                //ObjPr_EventLog.setTextErrLog("frm_Principal.poneNumero(), Inicio : "+ numero );
                //
                if (txt_Identificacion.Text.Contains("Cédula..."))
                {
                    txt_Identificacion.Text = "";
                    txt_Identificacion.ForeColor = Color.Black;
                    txt_Identificacion.Text = numero;
                }
                else
                {
                    String stL_Longitud = txt_Identificacion.Text;
                    int inL_Longitud = stL_Longitud.Length;
                    if (inL_Longitud < 10) txt_Identificacion.Text += numero;
                }
                //ObjPr_EventLog.setTextErrLog("frm_Principal.poneNumero(), Termina : " + numero);
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "poneNumero", "", ex.Message, "", "");
            }
        }

        private void ObjPr_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pnl_huella.Visible)
                {
                    //detieneEscaner(true);
                    //Thread.Sleep(2000);
                    VuelveInicio();
                }
                else
                {
                    LimpiaPantallas();
                }
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "ObjPr_timer_Tick", "", ex.Message, "", "");
            }
        }

        private void ObjPr_timerHUella_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    ptb_HuellaEstatica.Visible = true;
            //    ptb_HuelaPrelim.Visible = false;
            //}
            //catch (Exception ex)
            //{
            //    ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "ObjPr_timerHUella_Tick", "", ex.Message, "", "");
            //}
        }

        private void StartTiemrHuella()
        {
            try
            {

                //Application.DoEvents();
                //ObjPr_EventLog.setTextErrLog("Iniciando timer huella");
                ObjPr_EventLog.setTextErrLog("frm_Principal.StartTiemrHuella(),  Iniciando ...");
                //ObjPr_timerHuella.Tick += new EventHandler(ObjPr_timerHUella_Tick);
                ObjPr_timerHuella.Interval = 2000;
                ObjPr_timerHuella.Enabled = true;
                ObjPr_timerHuella.Start();
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "FrmPrincipal", "StartTiemrHuella", "", ex.Message, "", "");
            }
        }

        private void StopTiemrHuella()
        {
            try
            {

                //Application.DoEvents();
                //ObjPr_EventLog.setTextErrLog("Deteniendo timer huella");
                ObjPr_EventLog.setTextErrLog("frm_Principal.StopTiemrHuella(),  Iniciando ...");
                ObjPr_timerHuella.Stop();
                ObjPr_timerHuella.Enabled = false;
                //ObjPr_timerHuella.Tick -= new EventHandler(ObjPr_timer_Tick);
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "FrmPrincipal", "StopTiemrHuella", "", ex.Message, "", "");
            }
        }
        /// <summary>
        /// Inicia timer general de la aplicacion
        /// Modificacion: 05112014, cambio el manejo de la aplicacion
        /// Quien:Strail Aparicio
        /// </summary>
        private void StartTimer()
        {
            try
            {
                //if (true) return;
                //Application.DoEvents();
                //ObjPr_EventLog.setTextErrLog("Iniciando timer aplicacion");
                //ObjPr_EventLog.setTextErrLog("frm_Principal.StartTimer(), Inicio");
                ObjPr_EventLog.setTextErrLog("frm_Principal.StartTimer(),  Iniciando ...");
                //ObjPr_timer.Tick += new EventHandler(ObjPr_timer_Tick);
                autorizaEjecutar = true;
                ObjPr_timer.Interval = inPr_TimeOff;
                ObjPr_timer.Enabled = true;
                ObjPr_timer.Start();
                ObjPr_EventLog.setTextErrLog("frm_Principal.StartTimer(), Termina");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "FrmPrincipal", "StartTimer", "", ex.Message, "", "");
            }
        }
        /// <summary>
        /// autorizaEjecutar, se utiliza para indicar si se ejecuta o no la funcion de los timer
        /// </summary>
        private bool autorizaEjecutar = false;
        private void StopTimer()
        {
            try
            {
                //if (true) return;
                //Application.DoEvents();
                ObjPr_EventLog.setTextErrLog("frm_Principal.StopTimer(), Inicio");
                autorizaEjecutar = false;
                ObjPr_timer.Stop();
                ObjPr_timer.Enabled = false;
                ObjPr_EventLog.setTextErrLog("frm_Principal.StopTimer(), Termina");
                //ObjPr_timer.Tick -= new EventHandler(ObjPr_timer_Tick);
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "FrmPrincipal", "StopTimer", "", ex.Message, "", "");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            { 
                poneNumero("1");
                cmd_Accept.Enabled = true;
                button1.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button12_Click", "", ex.Message, "", "");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("2");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button11_Click", "", ex.Message, "", "");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("3");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button10_Click", "", ex.Message, "", "");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("4");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button9_Click", "", ex.Message, "", "");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("5");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button8_Click", "", ex.Message, "", "");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("6");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button7_Click", "", ex.Message, "", "");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("7");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button6_Click", "", ex.Message, "", "");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("8");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button5_Click", "", ex.Message, "", "");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("9");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button4_Click", "", ex.Message, "", "");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                poneNumero("0");
                button1.Enabled = true;
                cmd_Accept.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button2_Click", "", ex.Message, "", "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txt_Identificacion.Text.Contains("Cédula..."))
                {
                    txt_Identificacion.Text = "Cédula...";
                    txt_Identificacion.ForeColor = Color.Black;
                    button1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "button1_Click", "", ex.Message, "", "");
            }
        }



        private void VuelveInicio()
        {
            try
            {
                StopTimer();
                StopTiemrHuella();
                //Thread.Sleep(1300);
                //
                if (estadoEscaneo == -3)
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.LimpiaPantallas(), HUELLERO A NULL");
                    estadoEscaneo = -1;
                    detieneEscaner(true); //intentando destruir todo el escanner NULL
                }
                else
                {
                    detieneEscaner(false);
                }
                //hayTemplateEnDispositivo = 0;
                //Application.DoEvents();
              
                ObjPr_EventLog.setTextErrLog("frm_Principal.VuelveInicio(), Inicia ...");
                if(traerbio!= null)
                {
                    traerbio.Abort();
                }
                
                //detieneEscaner(false);
                Application.DoEvents();
                if (!ejecutaFuncionArreglaPantalla)
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.VuelveInicio(), Saliendo, ejecucion no permitida ejecutaFuncionArreglaPantalla = " + ejecutaFuncionArreglaPantalla);
                    //return;
                }
                ejecutaFuncionArreglaPantalla = false;
                Application.DoEvents();

                if (imagen != null)
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.VuelveInicio(), Imagen Dispose ... ");
                    imagen.Dispose();
                    imagen = null;
                }
                cmd_Accept.Enabled = false;
                inPr_DocNoAfiiado = 0;
                blPr_NoAfiliado = false;
                blPr_Beneficiario = false;
                blPr_Apoderado = false;
                blPr_Autorizado = false;
                blPr_apo = false;
                blPr_afi = false;
                inPr_A001NUM_IDENTIFICACION = 0;
                stPr_A001PRIMER_NOMBRE = "";
                stPr_A001SEGUNDO_NOMBRE = "";
                stPr_A001PRIMER_APELLIDO = "";
                stPr_A001SEGUNDO_APELLIDO = "";
                blPr_NoGA2 = false;
                lbl_Identificacion.Text = "Identificación";
                lbl_Identificacion.ForeColor = Color.DimGray;
                this.pnl_Inicio.Visible = true;
                this.pnl_teclado.Visible = false;
                this.pnl_certPago.Visible = false;
               // this.pnl_Consultas.Visible = false;
                this.pnl_EstadoCuenta.Visible = false;
                this.pnl_TurnosInfoAfiliado.Visible = false;
                this.pnl_huella.Visible = false;
                this.pnl_ESTADO_TRAMITE.Visible = false;
                this.pnl_Turno.Visible = false;
                this.pnl_decla.Visible = false;
                webBrowser1.Navigate("about:black");
                webBrowser3.Navigate("about:black");
                this.CMD_Regresar.Visible = false;
                this.CMD_Cancelar.Visible = false;
                this.CMD_Prioritario.Visible = false;
                ptb_HuelaPrelim.Visible = true;
                ptb_HuellaEstatica.Visible = false;
                this.txt_Identificacion.Clear();
                //AGR 29-06-2016 desactiva modo prioritario
                this.blPr_desactivaturnosP = false;
                //Strail 05112014
                //if (traerbio.IsAlive == true)
                //{
                //    traerbio.Abort();
                //}
                if (terminedetraer == -9)
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.VuelveInicio(), Eliminando thread ..");

                    traerbio = null;
                    terminedetraer = -1;
                }
                // ASQC Julio 16 2017. Cambia Condicion.
                //if (!(stPr_modo.Equals("lite")))
                if ((stPr_modo.Equals("lite")))
                // FIn ASQC Julio 16 2017.
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.VuelveInicio(), Modo LITE detectado ..");
                    CMD_Turnos.Visible = false;
                    CMD_Prioritario.Visible = false;
                }
                else
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.VuelveInicio(), Modo FULL detectado ..");
                    CMD_Turnos.Visible = true;
                    CMD_Prioritario.Visible = false;
                  
                }
                if (!txt_Identificacion.Text.Contains("Cédula..."))
                {
                    txt_Identificacion.Text = "Cédula...";
                    txt_Identificacion.ForeColor = Color.Black;
                }
                ObjPr_EventLog.setTextErrLog("frm_Principal.VuelveInicio(), TERMINA OK : ");
                //Strail 23102014 Alzheimer();
                //Application.DoEvents();
                //FENIX_KIOSCO.Form1.ActiveForm.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "VuelveInicio", "", ex.Message, "", "");
            }
        }

        private int hayTemplateEnDispositivo = 0; //-9 Ejecutando, 1 Si, -1 no se pudo, 0 nada
        private int estadoEscaneo = 0; //0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error

        private bool haceTodo()
        {
            try
            {
                estadoEscaneo = -9;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error, -3 Template de mala calidad
                Application.DoEvents();

                ObjPr_EventLog.setTextErrLog("frm_Principal.haceTodo(), Inicia ... : " + inPr_A001NUM_IDENTIFICACION + " :: " + hayTemplateEnDispositivo);

                if (hayTemplateEnDispositivo == -1)
                {
                    hayTemplateEnDispositivo = 0;

                    label1.Refresh();
                    label1.Text = "Error en captura";
                    label1.Text += "\nIntente de nuevo";
                    //label1.AutoSize = true;
                    Application.DoEvents();
                    ObjPr_EventLog.setTextErrLog("frm_Principal.haceTodo(), ERROR : Template de mala calidad : ");
                    estadoEscaneo = -2;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                    return false;
                }
                else if (hayTemplateEnDispositivo > 0)
                {
                    hayTemplateEnDispositivo = 0;
                    lbl_respuesta.TextAlign = ContentAlignment.TopCenter;
                    label1.Refresh();
                    label1.Text = "Huella Capturada";
                    label1.Text += "\nPor favor espere...";
                    //label1.AutoSize = true;
                    ObjPr_EventLog.setTextErrLog("frm_Principal.haceTodo(), Huella Capturada OK : " + inPr_A001NUM_IDENTIFICACION);
                    byte[] btL_template = objPr_huellero_Iden_Veri.getTemplate();

                    if (btL_template != null)
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.dev_ListoGrabar(), Imagen y Template OK : " + inPr_A001NUM_IDENTIFICACION);
                        String stL_Cedula = txt_Identificacion.Text;
                        String[] stL_Respuesta = Obj_AppKiosko.Manage_Template(stL_Cedula, stPr_Serv, StPr_inPuerto, btL_template, inPr_A001NUM_IDENTIFICACION);
                        if (stL_Respuesta.Length > 1)
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.dev_ListoGrabar(), Respuesta de validacion en SERVIDOR : " + inPr_A001NUM_IDENTIFICACION + " :: Respta" + stL_Respuesta[2]);
                            if (stL_Respuesta[2].Contains("-1"))
                            {
                                Application.DoEvents();
                                ObjPr_EventLog.setTextErrLog("frm_Principal.dev_ListoGrabar(), No hay Templates en el Servidor para : " + inPr_A001NUM_IDENTIFICACION + " :: O existe un error en la red");
                                lbl_respuesta.TextAlign = ContentAlignment.TopCenter;
                                if ((stPr_modo.Equals("lite")))
                                {
                                    this.pnl_huella.Visible = false;
                                    this.niegaTurno();
                                    terminaAlgo(this.pnl_Turno, 7000);
                                    this.CMD_Prioritario.Visible = false;
                                    inPr_A001NUM_IDENTIFICACION = 0;
                                    stPr_A001PRIMER_NOMBRE = "";
                                    stPr_A001PRIMER_APELLIDO = "";
                                    stPr_A001SEGUNDO_NOMBRE = "";
                                    stPr_A001SEGUNDO_APELLIDO = "";
                                }
                                else // del if ((stPr_modo.Equals("lite")))
                                { // Inicio del if ((stPr_modo.Equals("lite")))
                                    lbl_Alertas.Text = "ANALISIS NO COMPATIBLE.";
                                    //AGR 22072016 Muestra mensaje cuando el afiliado existe en BD pero tiene inconvenientes con los templates o imagenes.
                                    if ((stPr_modo.Equals("full")))
                                    {
                                        string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_11 + txt_Identificacion.Text + MENSAJE_APP_12, "", "Aceptar", "Cancelar", true, 42);
                                        MyMessageBox.ActiveForm.Activate();
                                        MyMessageBox.ActiveForm.BringToFront();
                                        ObjPr_EventLog.setTextErrLog(MENSAJE_APP_11 + txt_Identificacion.Text + MENSAJE_APP_12);
                                        if (dialogResult.Equals("1"))
                                        {
                                            InfoGeneral("enrol_huella");
                                        }

                                        if (dialogResult.Equals("2"))
                                        {
                                            VuelveInicio();
                                        }
                                    }
                                    else
                                    {
                                        string dialogResult = MyMessageBox.ShowBox("Afiliado con N. de identificación: " + txt_Identificacion.Text + ". Tiene inconvenientes con su biometría", "", "Aceptar", "Cancelar", false, 155);
                                        MyMessageBox.ActiveForm.Activate();
                                        MyMessageBox.ActiveForm.BringToFront();
                                        if (dialogResult.Equals("1"))
                                        {
                                            VuelveInicio();
                                        }
                                    }
                                    VuelveInicio();
                                }
                            }
                            else if (stL_Respuesta[2].Contains("3003")) // error 3003 Cuando la huella no corresponde 
                            {
                                estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                Application.DoEvents();
                                ObjPr_EventLog.setTextErrLog("No corresponde al numero proporcionado se solicita una huella valida");
                                lbl_respuesta.TextAlign = ContentAlignment.TopCenter;
                                lbl_Alertas.Text = "ANALISIS NO COMPATIBLE";
                                //AGR 21072016 NMuestra mensaje de confirmación cuando el número de documento no coincide.
                                if ((stPr_modo.Equals("full")))
                                {
                                    string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_09 + txt_Identificacion.Text + MENSAJE_APP_10, "", "Aceptar", "Salir", false, 155);
                                    MyMessageBox.ActiveForm.Activate();
                                    MyMessageBox.ActiveForm.BringToFront();
                                    ObjPr_EventLog.setTextErrLog(MENSAJE_APP_09 + txt_Identificacion.Text + MENSAJE_APP_10);
                                    if (dialogResult.Equals("1"))
                                    {
                                        ObjPr_EventLog.setTextErrLog("Panel Captura huella.Error huella capturada no corresponde a un valor valido");
                                        VuelveInicio();
                                    }


                                    if (dialogResult.Equals("2"))
                                    {
                                        ObjPr_EventLog.setTextErrLog("Accion cancelada");
                                        VuelveInicio();
                                    }
                                }
                                else
                                {
                                    string dialogResult = MyMessageBox.ShowBox("El documento: " + txt_Identificacion.Text + "No coincide con la huella registrada.", "", "Aceptar", "Cancelar", false, 155);
                                    MyMessageBox.ActiveForm.Activate();
                                    MyMessageBox.ActiveForm.BringToFront();
                                    if (dialogResult.Equals("1"))
                                    {
                                        VuelveInicio();
                                    }
                                }
                            }
                            else if (stL_Respuesta[3].Contains(txt_Identificacion.Text) && stL_Respuesta[2].Contains("1"))
                            {
                                //17 Noviembre 2017 Se retira thread de traer biometría y se deja como proceso lineal
                                TraeBios();
                                //traerbio = new Thread(TraeBios);//Trae las biometrias relacionadas con el numero de cedula
                                //traerbio.Start();
                                String strL_tempDir = "C:\\fnx";
                                String strL_temp = "";
                                this.pnl_vistaHuella.Controls.Remove(objPr_vista);
                                strL_temp = strL_tempDir + "\\" + inPr_A001NUM_IDENTIFICACION + "\\Documento.jpg";
                                if(!File.Exists(strL_temp))
                                {
                                    strL_temp = strL_tempDir + "\\" + inPr_A001NUM_IDENTIFICACION + "\\DOCUMENTOSImagen1.jpg";
                                }
                                //int num = 1;
                                //while (terminedetraer == -9)
                                //{
                                //    MSJ("LOS DATOS DEL THREAD NO HAN TERMINADO ...");
                                //    label1.Refresh();
                                //    //lbl_respuesta.Text = "Buscando... Espere por favor";
                                //    label1.TextAlign = ContentAlignment.MiddleCenter;
                                //    label1.Text = "BUSCANDO...";
                                //    label1.Text += "\nPOR FAVOR ESPERE...";
                                //    num = num + 1;
                                //    if(num > 10)
                                //    {
                                //        //MessageBox.Show("Se supera intentos");
                                        
                                //        //VuelveInicio();
                                //    }
                                //}

                                if (blPr_HayBio)
                                {
                                    if (File.Exists(strL_temp))
                                    {
                                        estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                        MSJ("PRESENTANDO DOCUMENTO ...");
                                        Application.DoEvents();
                                        //Crea un stream en memoria para guardar la foto del afiliado
                                        StreamReader StreamFoto = new StreamReader(strL_temp);
                                        Image Foto = Image.FromStream(StreamFoto.BaseStream);
                                        StreamFoto.Close();
                                        //carga la imagen del stream en un picturebox
                                        ptb_Fotografia.Image = Foto;
                                        ptb_Fotografia.SizeMode = PictureBoxSizeMode.StretchImage;
                                        estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                    }
                                    else
                                    {
                                        estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                        MSJ("NO HAY BIOMETRIAS");
                                        Application.DoEvents();
                                        ObjPr_EventLog.setTextErrLog("No se encontro registro de documento " + inPr_A001NUM_IDENTIFICACION);
                                        //ObjPr_EventLog.setTextErrLog("No se encontro registro de foto para cedula " + inPr_A001NUM_IDENTIFICACION);
                                        //MessageBox.Show("Afiliado con CC: " + inPr_A001NUM_IDENTIFICACION + "no posee fotografía, se generará turno para enrolamiento");
                                        lbl_Alertas.Text = "FALTA: Documento";//Strail 18112014, Agrega las biometrias que faltan;
                                        //AGR 22072016 Muestra mensaje cuando el afiliado existe en BD pero tiene inconvenientes con las imagenes biometricas.
                                        if ((stPr_modo.Equals("full")))
                                        {
                                            string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_13 + txt_Identificacion.Text + MENSAJE_APP_14, "", "Aceptar", "Cancelar", true, 42);
                                            MyMessageBox.ActiveForm.Activate();
                                            MyMessageBox.ActiveForm.BringToFront();
                                            ObjPr_EventLog.setTextErrLog(MENSAJE_APP_13 + txt_Identificacion.Text + MENSAJE_APP_14);
                                            if (dialogResult.Equals("1"))
                                            {
                                                InfoGeneral("enrol");
                                            }

                                            if (dialogResult.Equals("2"))
                                            {
                                                VuelveInicio();
                                            }
                                        }
                                        else
                                        {
                                            string dialogResult = MyMessageBox.ShowBox("El afiliado: " + txt_Identificacion.Text + ". Posee inconvenientes con su biometría", "", "Aceptar", "Cancelar", false, 155);
                                            MyMessageBox.ActiveForm.Activate();
                                            MyMessageBox.ActiveForm.BringToFront();
                                            if (dialogResult.Equals("1"))
                                            {
                                                VuelveInicio();
                                            }
                                        }
                                        VuelveInicio();
                                    }

                                    String stL_nombre = armaNombre(stPr_A001PRIMER_APELLIDO, stPr_A001SEGUNDO_APELLIDO, stPr_A001PRIMER_NOMBRE, stPr_A001SEGUNDO_NOMBRE);
                                    this.lbl_Nombres.TextAlign = ContentAlignment.TopCenter;
                                    this.lbl_Cedula.TextAlign = ContentAlignment.TopCenter;
                                    this.lbl_Nombres.Text = stL_nombre;
                                    this.lbl_Cedula.Text = "CEDULA: " + txt_Identificacion.Text;
                                    String stL_NumAux = Convert.ToString(inPr_A001NUM_IDENTIFICACION);
                                    if (txt_Identificacion.Text.Equals(stL_NumAux))
                                    {
                                        this.lbl_Cedula.Text = "CEDULA: " + txt_Identificacion.Text;
                                    }
                                    MSJ("Presentando Panel de Afiliado ...");
                                    cambiaPantallas(PANEL_TURNOSINFOAFILIADO);

                                }
                                else
                                {
                                    estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                    Application.DoEvents();
                                    ObjPr_EventLog.setTextErrLog("No se encontro registro biometrico para cedula " + inPr_A001NUM_IDENTIFICACION);
                                    lbl_Alertas.Text = stPr_BioFalta;//Strail 18112014, Agrega las biometrias que faltan;
                                    stPr_BioFalta = "";
                                    //AGR 22072016 Muestra mensaje cuando el afiliado existe en BD pero tiene inconvenientes con las imagenes biometricas.
                                    if ((stPr_modo.Equals("full")))
                                    {
                                        string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_13 + txt_Identificacion.Text + MENSAJE_APP_14, "", "Aceptar", "Cancelar", true, 42);
                                        MyMessageBox.ActiveForm.Activate();
                                        MyMessageBox.ActiveForm.BringToFront();
                                        ObjPr_EventLog.setTextErrLog(MENSAJE_APP_13 + txt_Identificacion.Text + MENSAJE_APP_14);
                                        if (dialogResult.Equals("1"))
                                        {
                                            InfoGeneral("enrol");
                                        }

                                        if (dialogResult.Equals("2"))
                                        {
                                            VuelveInicio();
                                        }
                                    }
                                    else
                                    {
                                        string dialogResult = MyMessageBox.ShowBox("El afiliado: " + txt_Identificacion.Text + ". Posee inconvenientes con su biometría", "", "Aceptar", "Cancelar", false, 155);
                                        MyMessageBox.ActiveForm.Activate();
                                        MyMessageBox.ActiveForm.BringToFront();
                                        if (dialogResult.Equals("1"))
                                        {
                                            VuelveInicio();
                                        }
                                    }

                                    VuelveInicio();
                                }
                                estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                            }
                            else
                            {
                                estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                Application.DoEvents();
                                ObjPr_EventLog.setTextErrLog("frm_Principal.haceTodo(), ERROR en el servidor  : (" + stL_Respuesta[2] + ")");
                                lbl_respuesta.TextAlign = ContentAlignment.TopCenter;
                                lbl_Alertas.Text = "ERROR EN SERVIDORES (" + stL_Respuesta[2] + ")";
                                InfoGeneral("");

                            }
                            estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                            return true;
                        }
                        else
                        {
                            estadoEscaneo = -2;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                            ObjPr_EventLog.setTextErrLog("frm_Principal.dev_ListoGrabar(), ERROR en la respuesta del Srv aDIRECTOR : " + inPr_A001NUM_IDENTIFICACION);
                            MyMessageBox.ShowBox("Error en la transmisión de archivos, por favor intente nuevamente", "", "Aceptar", "Cancelar", false, 155);
                            MyMessageBox.ActiveForm.Activate();
                            MyMessageBox.ActiveForm.BringToFront();
                            VuelveInicio();
                            return false;
                        }
                        //estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error

                    }
                    else
                    {
                        estadoEscaneo = -2;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                        ObjPr_EventLog.setTextErrLog("frm_Principal.haceTodo(), ERROR en el Template  : " + inPr_A001NUM_IDENTIFICACION);
                        if ((stPr_modo.Equals("full")))
                        {
                            string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_11 + txt_Identificacion.Text + MENSAJE_APP_22, "", "Aceptar", "Cancelar", true, 42);
                            MyMessageBox.ActiveForm.Activate();
                            MyMessageBox.ActiveForm.BringToFront();
                            ObjPr_EventLog.setTextErrLog(MENSAJE_APP_11 + txt_Identificacion.Text + MENSAJE_APP_22);
                            if (dialogResult.Equals("1"))
                            {
                                InfoGeneral("enrol_huella");
                            }

                            if (dialogResult.Equals("2"))
                            {
                                VuelveInicio();
                            }
                        }
                        else
                        {
                            string dialogResult = MyMessageBox.ShowBox("Afiliado con N. de identificación: " + txt_Identificacion.Text + ". Tiene inconvenientes con su biometría", "", "Aceptar", "Cancelar", false, 155);
                            MyMessageBox.ActiveForm.Activate();
                            MyMessageBox.ActiveForm.BringToFront();
                            if (dialogResult.Equals("1"))
                            {
                                VuelveInicio();
                            }
                        }
                        return false;
                    }
                    //estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                }
                else
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.haceTodo(), ERROR : No entiendo hayTemplateEnDispositivo : " + hayTemplateEnDispositivo);
                    estadoEscaneo = -2;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                    hayTemplateEnDispositivo = 0;
                    return false;
                }
                //estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error

                //}
                //else
                //{
                //    //VuelveInicio();
                //    ObjPr_EventLog.setTextErrLog("frm_Principal.haceTodo(), ERROR : En el dispositivo de huella");
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                estadoEscaneo = -2;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                hayTemplateEnDispositivo = 0;
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "haceTodo", "", ex.Message, "", "");
                return false;
            }
        }

        private void MSJ(string dato)
        {
            try
            {
                lblMsjs.Text = dato;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "MSJ", "", ex.Message, "", "");
            }
        }


        void esperarNTiempo(int ntiempo)
        {
            for (int i = 0; i < ntiempo; i++)
            {
                Application.DoEvents();
            }
        }
        /// <summary>
        /// Se ejecuta al hacer clic en el boton verde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            
            if (blPr_Apoderado)
            {
                
               inPr_DocNoAfiiado = Convert.ToUInt64(txt_Identificacion.Text);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Numero de cedula no Afiliado  : " + inPr_DocNoAfiiado);
                txt_Identificacion.Text = "";
                lbl_Identificacion.Text = "Identificación Afiliado";
                lbl_Identificacion.ForeColor = Color.Green;
                blPr_Apoderado = false;
                cmd_Accept.Enabled = false;
                blPr_apo = true;
            }else if (blPr_NoAfiliado && blPr_apo)
                {
                    
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Inicia ...");
                   
                    bool bPermiso = daPermisoEjecutar();


                    lbl_respuesta.Text = "";
                    this.lbl_Alertas.Text = "";
                    lbl_ceduAfi.Text = "";

                    if (txt_Identificacion.Text.Length < 5 || txt_Identificacion.Text.StartsWith("0") || txt_Identificacion.Text.Contains("Cédula...") || txt_Identificacion.Text.StartsWith(" "))
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Numero de cedula no valido : " + txt_Identificacion.Text);
                        txt_Identificacion.ForeColor = Color.Black;
                        txt_Identificacion.Text = "Cédula...";

                    }
                    else //Si es un numero de cedula que no inicia con cero 0 y tiene mas de 5 caracteres
                    {
                        try
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Inicia Proceso para CEDULA : " + txt_Identificacion.Text);
                            //
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Validando aDIRECTOR (" + stPr_Serv + ":" + StPr_inPuerto + ")");
                            //Strail

                            MSJ("Validando Conexion aDIRECTOR");
                            bool testconexion = Obj_AppKiosko.Valida_Conexion_ADirector(stPr_Serv, StPr_inPuerto);
                            //
                            if (testconexion)//eRROR DE CONEXION
                            {

                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(),  aDIRECTOR OK ...");
                                lblMsjs.Text = "Validando Conexion aDIRECTOR OK";
                                //
                                MSJ("Consultando Cedula GA2");
                                if (consultaCedulaEnGA2())
                                {
                                    MSJ("Cedula GA2 OK");
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Se obtuvieron resultados de SP: SP_010_LEE_AFILIADO_GA2 para cc: " + txt_Identificacion.Text);
                                    Application.DoEvents();
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(),  Iniciando THREAD para : " + txt_Identificacion.Text);
                                    cambiaPantallas(PANEL_INFONOAFILIADO);
                                    
                                }
                                else
                                {
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Turno General para : " + txt_Identificacion.Text);
                                    MSJ("No existe en GA2");
                                    pnl_teclado.Visible = false;
                                    stPr_A001PRIMER_APELLIDO = txt_Identificacion.Text; //Strail 18112014, para cuando la persona no existe en GA2, ponemos la cedula
                                    stPr_A001PRIMER_NOMBRE = "";
                                    stPr_A001SEGUNDO_APELLIDO = "";
                                    stPr_A001SEGUNDO_NOMBRE = "";
                                    //Strail 18112014
                                    lbl_Alertas.Text = "* NO REGISTRADO EN GA2";
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Alerta : " + lbl_Alertas.Text);
                                    ////////////////////////////////////////////////
                                    // ASQC Feb 25 2.014
                                    ////////////////////////////////////////////////
                                    // Si esta en modo LITE, no debe generar turno y presenta 
                                    // mensaje que el turno no se puede generar en estos momentos.
                                    if ((stPr_modo.Equals("lite")))
                                    {
                                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Modo LITE : ");
                                        this.niegaTurno();
                                        terminaAlgo(this.pnl_Turno, 7000);

                                        //this.pnl_Turno.Visible = false;
                                        this.CMD_Prioritario.Visible = false;
                                        //LimpiaPantallas();
                                    }
                                    else
                                    {
                                        blPr_NoGA2 = true;

                                        // Genera el turno de general.
                                        // ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), TURNO GENERAL : " + lbl_Alertas.Text);
                                        //AGR 22072016 Muestra mensaje de confirmación cuando el número de documento ingresado no existe en BD GA2".
                                        if ((stPr_modo.Equals("full")))
                                        {
                                            this.CMD_Cancelar.Visible = false;
                                            //MSJ("No existe en GA2 verifique el número de Cedula" + txt_Identificacion.Text);
                                            ObjPr_EventLog.setTextErrLog(MENSAJE_APP_15 + txt_Identificacion.Text + MENSAJE_APP_16);
                                            string dialogResult = MyMessageBox.ShowBox(" Usuario no existe en la base de datos , verifique número de Cedula :" + txt_Identificacion.Text + " ", "", "Aceptar", "Cancelar", false, 155);
                                            MyMessageBox.ActiveForm.Activate();
                                            MyMessageBox.ActiveForm.BringToFront();
                                            VuelveInicio();

                                            //if (dialogResult.Equals("1"))
                                            //{
                                            // InfoGeneral("No GA2");
                                            //}
                                            //if (dialogResult.Equals("2"))
                                            //{
                                            // ObjPr_EventLog.setTextErrLog("Accion cancelada. VuelveInicio");

                                            //}
                                        }
                                        else
                                        {
                                            string dialogResult = MyMessageBox.ShowBox("Usuario no existe en la base de datos , verifique  número de Cedula :" + txt_Identificacion.Text + " ", "", "Aceptar", "Cancelar", false, 155);
                                            MyMessageBox.ActiveForm.Activate();
                                            MyMessageBox.ActiveForm.BringToFront();
                                            //if (dialogResult.Equals("1"))
                                            //{
                                            VuelveInicio();
                                            //}
                                        }
                                        VuelveInicio();
                                    }
                                    ////////////////////////////////////////////////
                                    // Fin ASQC Feb 25 2.014
                                    ////////////////////////////////////////////////
                                }

                            }//Cierra if de no conexion FENIX

                            else //sI NO HAY CONEXION CON FENIX
                            {

                                //Strail 18112014 MessageBox.Show(MENSAJE_APP_04 + ClasX_Constans.NEW_LINE + MENSAJE_APP_03 + ClasX_Constans.NEW_LINE + ClasX_Constans.NEW_LINE + ClasX_Constans.MENSAJE_22, ClasX_Constans.MENSAJE_5);
                                //ObjPr_EventLog.setTextErrLog("ERROR accediendo a servidores Fenix.");
                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), ERROR De Red, No Hay conexion con el aDIRECTOR");
                                //Strail 18112014
                                lbl_Alertas.Text = "ERROR DE CONEXION A FENIX";
                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), TURNO GENERAL : " + lbl_Alertas.Text);
                                //
                                //AGR 26072016
                                if ((stPr_modo.Equals("full")))
                                {
                                    string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_19, "", "Aceptar", "Cancelar", true, 42);
                                    MyMessageBox.ActiveForm.Activate();
                                    MyMessageBox.ActiveForm.BringToFront();
                                    ObjPr_EventLog.setTextErrLog(MENSAJE_APP_19);
                                    if (dialogResult.Equals("1"))
                                    {
                                        InfoGeneral("");
                                    }
                                    if (dialogResult.Equals("2"))
                                    {
                                        ObjPr_EventLog.setTextErrLog("Accion cancelada. VuelveInicio");
                                        VuelveInicio();
                                    }
                                }
                                else
                                {
                                    string dialogResult = MyMessageBox.ShowBox("Error de conexión al sistema de información", "", "Aceptar", "Cancelar", false, 155);
                                    MyMessageBox.ActiveForm.Activate();
                                    MyMessageBox.ActiveForm.BringToFront();
                                    if (dialogResult.Equals("1"))
                                    {
                                        VuelveInicio();
                                    }
                                }
                                VuelveInicio();

                            }//Cierra. Else No hay conexion FENIX

                            pnl_teclado.Enabled = true;
                            //

                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Termina OK");

                        }
                        catch (Exception ex)
                        {

                            ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Aceptar_Click", "", ex.Message.ToString(), "", "");
                        }
                    }
                }




           
           if (blPr_Beneficiario)
            {  //no aafiliado G2
               
                inPr_A001NUM_IDENTIFICACION = Convert.ToUInt64(txt_Identificacion.Text);
                inPr_DocNoAfiiado = Convert.ToUInt64(txt_Identificacion.Text);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Numero de cedula no Afiliado  : " + inPr_DocNoAfiiado);
                txt_Identificacion.Text = "";
                lbl_Identificacion.Text = "Identificación Afiliado";
                lbl_Identificacion.ForeColor = Color.Green;
                blPr_Beneficiario = false;
                cmd_Accept.Enabled = false;
                blPr_afi = true;
               }else if (blPr_NoAfiliado && blPr_afi)
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Inicia ...");
               
                  //cuentaparasalir = 0;
                    //Strail, Activo el permiso para hacer limpiapantallas 
                    bool bPermiso = daPermisoEjecutar();

                    
                    lbl_respuesta.Text = "";
                    this.lbl_Alertas.Text = "";
                    lbl_ceduAfi.Text = "";
                    
                    if (txt_Identificacion.Text.Length < 5 || txt_Identificacion.Text.StartsWith("0") || txt_Identificacion.Text.Contains("Cédula...") || txt_Identificacion.Text.StartsWith(" "))
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Numero de cedula no valido : " + txt_Identificacion.Text);
                        txt_Identificacion.ForeColor = Color.Black;
                        txt_Identificacion.Text = "Cédula...";

                    }
                    else //Si es un numero de cedula que no inicia con cero 0 y tiene mas de 5 caracteres
                    {
                        try
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Inicia Proceso para CEDULA : " + txt_Identificacion.Text);
                            //
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Validando aDIRECTOR (" + stPr_Serv + ":" + StPr_inPuerto + ")");
                            //Strail

                            MSJ("Validando Conexion aDIRECTOR");
                            bool testconexion = Obj_AppKiosko.Valida_Conexion_ADirector(stPr_Serv, StPr_inPuerto);
                            //
                            if (testconexion)//eRROR DE CONEXION
                            {

                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(),  aDIRECTOR OK ...");
                                lblMsjs.Text = "Validando Conexion aDIRECTOR OK";
                                //
                                MSJ("Consultando Cedula GA2");
                                if (consultaCedulaEnGA2())
                                {
                                    MSJ("Cedula GA2 OK");
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Se obtuvieron resultados de SP: SP_010_LEE_AFILIADO_GA2 para cc: " + txt_Identificacion.Text);
                                    Application.DoEvents();
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(),  Iniciando THREAD para : " + txt_Identificacion.Text);
                                    cambiaPantallas(PANEL_INFOBENEFICIARIO);
                                    
                                }
                                else
                                {
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Turno General para : " + txt_Identificacion.Text);
                                    MSJ("No existe en GA2");
                                    pnl_teclado.Visible = false;
                                    stPr_A001PRIMER_APELLIDO = txt_Identificacion.Text; //Strail 18112014, para cuando la persona no existe en GA2, ponemos la cedula
                                    stPr_A001PRIMER_NOMBRE = "";
                                    stPr_A001SEGUNDO_APELLIDO = "";
                                    stPr_A001SEGUNDO_NOMBRE = "";
                                    //Strail 18112014
                                    lbl_Alertas.Text = "* NO REGISTRADO EN GA2";
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Alerta : " + lbl_Alertas.Text);
                                    ////////////////////////////////////////////////
                                    // ASQC Feb 25 2.014
                                    ////////////////////////////////////////////////
                                    // Si esta en modo LITE, no debe generar turno y presenta 
                                    // mensaje que el turno no se puede generar en estos momentos.
                                    if ((stPr_modo.Equals("lite")))
                                    {
                                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Modo LITE : ");
                                        this.niegaTurno();
                                        terminaAlgo(this.pnl_Turno, 7000);

                                        //this.pnl_Turno.Visible = false;
                                        this.CMD_Prioritario.Visible = false;
                                        //LimpiaPantallas();
                                    }
                                    else
                                    {
                                        blPr_NoGA2 = true;
                                      
                                        // Genera el turno de general.
                                       // ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), TURNO GENERAL : " + lbl_Alertas.Text);
                                        //AGR 22072016 Muestra mensaje de confirmación cuando el número de documento ingresado no existe en BD GA2".
                                        if ((stPr_modo.Equals("full")))
                                        {
                                            this.CMD_Cancelar.Visible = false;
                                            //MSJ("No existe en GA2 verifique el número de Cedula" + txt_Identificacion.Text);
                                            ObjPr_EventLog.setTextErrLog(MENSAJE_APP_15 + txt_Identificacion.Text + MENSAJE_APP_16);
                                            string dialogResult = MyMessageBox.ShowBox(" Usuario no existe en la base de datos , verifique número de Cedula :" + txt_Identificacion.Text + " ", "", "Aceptar", "Cancelar", false, 155);
                                            MyMessageBox.ActiveForm.Activate();
                                            MyMessageBox.ActiveForm.BringToFront();
                                            VuelveInicio();
                                           
                                            //if (dialogResult.Equals("1"))
                                            //{
                                               // InfoGeneral("No GA2");
                                            //}
                                            //if (dialogResult.Equals("2"))
                                            //{
                                               // ObjPr_EventLog.setTextErrLog("Accion cancelada. VuelveInicio");
                                               
                                            //}
                                        }
                                        else
                                        {
                                            string dialogResult = MyMessageBox.ShowBox("Usuario no existe en la base de datos , verifique  número de Cedula :" + txt_Identificacion.Text + " ", "", "Aceptar", "Cancelar", false, 155);
                                            MyMessageBox.ActiveForm.Activate();
                                           MyMessageBox.ActiveForm.BringToFront();
                                            //if (dialogResult.Equals("1"))
                                            //{
                                                VuelveInicio();
                                            //}
                                        }
                                        VuelveInicio();
                                    }
                                    ////////////////////////////////////////////////
                                    // Fin ASQC Feb 25 2.014
                                    ////////////////////////////////////////////////
                                }

                            }//Cierra if de no conexion FENIX

                            else //sI NO HAY CONEXION CON FENIX
                            {

                                //Strail 18112014 MessageBox.Show(MENSAJE_APP_04 + ClasX_Constans.NEW_LINE + MENSAJE_APP_03 + ClasX_Constans.NEW_LINE + ClasX_Constans.NEW_LINE + ClasX_Constans.MENSAJE_22, ClasX_Constans.MENSAJE_5);
                                //ObjPr_EventLog.setTextErrLog("ERROR accediendo a servidores Fenix.");
                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), ERROR De Red, No Hay conexion con el aDIRECTOR");
                                //Strail 18112014
                                lbl_Alertas.Text = "ERROR DE CONEXION A FENIX";
                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), TURNO GENERAL : " + lbl_Alertas.Text);
                                //
                                //AGR 26072016
                                if ((stPr_modo.Equals("full")))
                                {
                                    string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_19, "", "Aceptar", "Cancelar", true, 42);
                                    MyMessageBox.ActiveForm.Activate();
                                    MyMessageBox.ActiveForm.BringToFront();
                                    ObjPr_EventLog.setTextErrLog(MENSAJE_APP_19);
                                    if (dialogResult.Equals("1"))
                                    {
                                        InfoGeneral("");
                                    }
                                    if (dialogResult.Equals("2"))
                                    {
                                        ObjPr_EventLog.setTextErrLog("Accion cancelada. VuelveInicio");
                                        VuelveInicio();
                                    }
                                }
                                else
                                {
                                    string dialogResult = MyMessageBox.ShowBox("Error de conexión al sistema de información", "", "Aceptar", "Cancelar", false, 155);
                                    MyMessageBox.ActiveForm.Activate();
                                    MyMessageBox.ActiveForm.BringToFront();
                                    if (dialogResult.Equals("1"))
                                    {
                                        VuelveInicio();
                                    }
                                }
                                VuelveInicio();

                            }//Cierra. Else No hay conexion FENIX

                            pnl_teclado.Enabled = true;
                            //

                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Termina OK");

                        }
                        catch (Exception ex)
                        {

                            ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Aceptar_Click", "", ex.Message.ToString(), "", "");
                        }
                    }
                }


           if (!blPr_NoAfiliado)
                {
                    //ejecutandoLPVI = false;
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Inicia ...");
                    //ObjPr_FingerPrint_Device.UpdateScannerList();
                    cuentaparasalir = 0;
                    //Strail, Activo el permiso para hacer limpiapantallas 
                    bool bPermiso = daPermisoEjecutar();

                    //MSJ("DETENEIENDO SCANNER FALSE");
                    //detieneEscaner(false);
                    //MSJ("ACTIVANDO ESCANNER");
                    estadoEscaneo = 0;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                    //bool escannerActivo=activarEscaner();
                    //esperarNTiempo(2000);

                    lbl_respuesta.Text = "";
                    this.lbl_Alertas.Text = "";
                    lbl_ceduAfi.Text = "";

                    //Valida que el numero de cedula no sea menor de 5 o que no inicie con cero 0
                    if (txt_Identificacion.Text.Length < 5 || txt_Identificacion.Text.StartsWith("0") || txt_Identificacion.Text.Contains("Cédula..."))
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Numero de cedula no valido : " + txt_Identificacion.Text);
                        txt_Identificacion.ForeColor = Color.Black;
                        txt_Identificacion.Text = "Cédula...";

                    }
                    else //Si es un numero de cedula que no inicia con cero 0 y tiene mas de 5 caracteres
                    {
                        try
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Inicia Proceso para CEDULA : " + txt_Identificacion.Text);
                            //
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Validando aDIRECTOR (" + stPr_Serv + ":" + StPr_inPuerto + ")");
                            //Strail

                            MSJ("Validando Conexion aDIRECTOR");
                            bool testconexion = Obj_AppKiosko.Valida_Conexion_ADirector(stPr_Serv, StPr_inPuerto);
                            //
                            if (testconexion)//eRROR DE CONEXION
                            {

                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(),  aDIRECTOR OK ...");
                                lblMsjs.Text = "Validando Conexion aDIRECTOR OK";
                                //
                                MSJ("Consultando Cedula GA2");
                                if (consultaCedulaEnGA2())
                                {
                                    MSJ("Cedula GA2 OK");
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Se obtuvieron resultados de SP: SP_010_LEE_AFILIADO_GA2 para cc: " + txt_Identificacion.Text);
                                    //Application.DoEvents();
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(),  Iniciando THREAD para : " + txt_Identificacion.Text);
                                    MSJ("Iniciando THREAD");
                                    //traerbio = new Thread(TraeBios);//Trae las biometrias relacionadas con el numero de cedula
                                    //traerbio.Start();
                                    //Strail , debug TraeBios();
                                    hayTemplateEnDispositivo = -9;
                                    MSJ("ACTIVANDO ESCANNER");
                                    estadoEscaneo = 0;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                    bool escannerActivo = activarEscaner();

                                    Thread.Sleep(100);
                                    apagaTodosLosPanels();
                                    //esperarNTiempo(3000);



                                    if (!escannerActivo)
                                    {
                                        estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                        VuelveInicio();

                                    }
                                    else
                                    {
                                        MSJ("OK, Presentando pantalla de huella");
                                        pnl_vistaHuella.Visible = true;

                                        this.ptb_HuellaEstatica.Visible = true;
                                        cambiaPantallas(PANEL_CAPTURA_HUELLA);
                                        MSJ("SCANNER OK, INICIA TOMA DE HUELLA ...");
                                        if (objPr_huellero_Iden_Veri.getErrorenHuellero())
                                        {
                                            MSJ("Error en el huellero");
                                            ObjPr_EventLog.setTextErrLog("frm_Principal.ActivaEscaner(), ERROR en Huellero ");
                                            //VuelveInicio();
                                        }
                                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(),  Iniciando THREAD para : " + txt_Identificacion.Text);
                                        MSJ("Iniciando THREAD");
                                        //traerbio = new Thread(TraeBios);//Trae las biometrias relacionadas con el numero de cedula
                                        //traerbio.Start();
                                        estadoEscaneo = 1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                                    }

                                }
                                else
                                {
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Turno General para : " + txt_Identificacion.Text);
                                    MSJ("No existe en GA2 Genera turno General");
                                    pnl_teclado.Visible = false;
                                    stPr_A001PRIMER_APELLIDO = txt_Identificacion.Text; //Strail 18112014, para cuando la persona no existe en GA2, ponemos la cedula
                                    stPr_A001PRIMER_NOMBRE = "";
                                    stPr_A001SEGUNDO_APELLIDO = "";
                                    stPr_A001SEGUNDO_NOMBRE = "";
                                    //Strail 18112014
                                    lbl_Alertas.Text = "* NO REGISTRADO EN GA2";
                                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Alerta : " + lbl_Alertas.Text);
                                    ////////////////////////////////////////////////
                                    // ASQC Feb 25 2.014
                                    ////////////////////////////////////////////////
                                    // Si esta en modo LITE, no debe generar turno y presenta 
                                    // mensaje que el turno no se puede generar en estos momentos.
                                    if ((stPr_modo.Equals("lite")))
                                    {
                                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Modo LITE : ");
                                        this.niegaTurno();
                                        terminaAlgo(this.pnl_Turno, 7000);

                                        //this.pnl_Turno.Visible = false;
                                        this.CMD_Prioritario.Visible = false;
                                        //LimpiaPantallas();
                                    }
                                    else
                                    {
                                        blPr_NoGA2 = true;
                                        // Genera el turno de general.
                                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), TURNO GENERAL : " + lbl_Alertas.Text);
                                        //AGR 22072016 Muestra mensaje de confirmación cuando el número de documento ingresado no existe en BD GA2".
                                        if ((stPr_modo.Equals("full")))
                                        {
                                            
                                            string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_15 + txt_Identificacion.Text + MENSAJE_APP_16, "", "Aceptar", "Salir", false, 155);
                                            MyMessageBox.ActiveForm.Activate();
                                            MyMessageBox.ActiveForm.BringToFront();
                                            ObjPr_EventLog.setTextErrLog(MENSAJE_APP_15 + txt_Identificacion.Text + MENSAJE_APP_16);
                                            if (dialogResult.Equals("1"))
                                            {
                                                ObjPr_EventLog.setTextErrLog("Nuemero de Identifiacion de Usuario errado. VuelveInicio");
                                                VuelveInicio();
                                            }
                                            if (dialogResult.Equals("2"))
                                            {
                                                ObjPr_EventLog.setTextErrLog("Accion cancelada. VuelveInicio");
                                                VuelveInicio();
                                            }
                                        }
                                        else
                                        {
                                            string dialogResult = MyMessageBox.ShowBox("El documento: " + txt_Identificacion.Text + ". No posee información.", "", "Aceptar", "Cancelar", false, 155);
                                            MyMessageBox.ActiveForm.Activate();
                                            MyMessageBox.ActiveForm.BringToFront();
                                            if (dialogResult.Equals("1"))
                                            {
                                                VuelveInicio();
                                            }
                                        }
                                        VuelveInicio();
                                    }
                                    ////////////////////////////////////////////////
                                    // Fin ASQC Feb 25 2.014
                                    ////////////////////////////////////////////////
                                }

                            }//Cierra if de no conexion FENIX

                            else //sI NO HAY CONEXION CON FENIX
                            {

                                //Strail 18112014 MessageBox.Show(MENSAJE_APP_04 + ClasX_Constans.NEW_LINE + MENSAJE_APP_03 + ClasX_Constans.NEW_LINE + ClasX_Constans.NEW_LINE + ClasX_Constans.MENSAJE_22, ClasX_Constans.MENSAJE_5);
                                //ObjPr_EventLog.setTextErrLog("ERROR accediendo a servidores Fenix.");
                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), ERROR De Red, No Hay conexion con el aDIRECTOR");
                                //Strail 18112014
                                lbl_Alertas.Text = "ERROR DE CONEXION A FENIX";
                                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), TURNO GENERAL : " + lbl_Alertas.Text);
                                //
                                //AGR 26072016
                                if ((stPr_modo.Equals("full")))
                                {
                                    string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_19, "", "Aceptar", "Cancelar", true, 42);
                                    MyMessageBox.ActiveForm.Activate();
                                    MyMessageBox.ActiveForm.BringToFront();
                                    ObjPr_EventLog.setTextErrLog(MENSAJE_APP_19);
                                    if (dialogResult.Equals("1"))
                                    {
                                        InfoGeneral("");
                                    }
                                    if (dialogResult.Equals("2"))
                                    {
                                        ObjPr_EventLog.setTextErrLog("Accion cancelada. VuelveInicio");
                                        VuelveInicio();
                                    }
                                }
                                else
                                {
                                    string dialogResult = MyMessageBox.ShowBox("Error de conexión al sistema de información", "", "Aceptar", "Cancelar", false, 155);
                                    MyMessageBox.ActiveForm.Activate();
                                    MyMessageBox.ActiveForm.BringToFront();
                                    if (dialogResult.Equals("1"))
                                    {
                                        VuelveInicio();
                                    }
                                }
                                VuelveInicio();

                            }//Cierra. Else No hay conexion FENIX

                            pnl_teclado.Enabled = true;
                            //

                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Aceptar_Click(), Termina OK");

                        }
                        catch (Exception ex)
                        {

                            ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Aceptar_Click", "", ex.Message.ToString(), "", "");
                        }
                    }
                }
            
        }



        public void dev_avisarTermine(NFView objR_vista)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.dev_avisarTermine(), Inicia ..");
                //StopTimer();
                //StopTiemrHuella();
                ObjPr_EventLog.setTextErrLog("frm_Principal.dev_avisarTermine(), Termina ..");

            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "dev_avisarTermine", "", ex.Message.ToString(), "", "");
            }
        }

        private int intentosDedo = 0;
        public void dev_ListoGrabar(int cuenta)
        {
            try
            {
                hayTemplateEnDispositivo = cuenta;
                if (estadoEscaneo == 1 || intentosDedo < 2) //0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                {
                    intentosDedo++;
                    if (hayTemplateEnDispositivo > 0)
                    {
                        MSJ("HUELLA TOMADA OK ...");
                        intentosDedo = 0;
                        if(!haceTodo())
                        {
                            LimpiaPantallas();
                        }
                        
                    }
                    else if (hayTemplateEnDispositivo == -1)
                    {

                        estadoEscaneo = -3;
                        StopTimer();
                        StopTiemrHuella();
                        VuelveInicio();
                        MSJ("TEMPLATE DE MALA CALIDAD, EL DEDO ESTA SUCIO O MAL PUESTO ...");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.dev_ListoGrabar(), Template de mala calidad inicia toma ... ");
                    }
                }
                //else if (estadoEscaneo == -3)
                //{
                //    MSJ("ERROR TOMANDO LA HUELLA ...");
                //    ObjPr_EventLog.setTextErrLog("frm_Principal.dev_ListoGrabar(), Template de mala calidad inicia toma ... ");
                //    //haceTodo();
                //}

            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "dev_ListoGrabar", "", ex.Message.ToString(), "", "");

            }
        }

        private void InfoGeneral(string SERVICIO)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.InfoGeneral(), Inicia : " + SERVICIO);
                estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                pnl_huella.Visible = false;
                if (SERVICIO.Equals(""))
                {
                    AsignaTurnos(stPrc_INFORMACION_GENERAL);
                }
                else
                {
                    if (SERVICIO.Contains("No GA2"))
                    {
                        AsignaTurnos(stPrc_INFORMACION_GENERAL);
                    }

                    else
                    {
                        AsignaTurnos(stPrc_BIOMETRIA);
                    }

                    if (SERVICIO.Contains("No GA2_TP"))
                    {
                        AsignaTurnos(stPrc_PREFERENCIAL);
                    }
                }
            }
            catch (Exception e)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.exe", "Frm_Principal", "InfoGeneral", "", "Error: " + e.Message, "", "");
            }
        }


        private void InfoGeneralP(string SERVICIO)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.InfoGeneral(), Inicia : " + SERVICIO);
                estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                pnl_huella.Visible = false;
                if (SERVICIO.Equals(""))
                {
                    AsignaTurnos(stPrc_INFORMACION_GENERAL);
                }
                else
                {
                    if (SERVICIO.Contains("No GA2_TP"))
                    {
                        AsignaTurnos(stPrc_PREFERENCIAL);
                    }
                    else
                    {
                        AsignaTurnos(stPrc_BIOMETRIA);
                    }
                }
            }
            catch (Exception e)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.exe", "Frm_Principal", "InfoGeneralP", "", "Error: " + e.Message, "", "");
            }
        }


        private void cmd_consulta_Click(object sender, EventArgs e)
        {
            try
            {
                CMD_ImprimirCerti.Enabled = true;
                CMD_ImprimirEstado.Enabled = true;
                CMD_imprimirEstTramite.Enabled = true;
                cambiaPantallas(PANEL_CONSULTAS);
                this.CMD_Prioritario.Visible = false;
                this.CMD_Regresar.Visible = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.exe", "Frm_Principal", "cmd_consulta_Click", "", "Error: " + ex.Message, "", "");
            }
        }

        private String aMoneda(String cadenaBD)
        {
            Double repNumerica;
            String SalidaMoneda;
            try
            {
                repNumerica = Convert.ToDouble(cadenaBD);
                SalidaMoneda = (repNumerica.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
                return SalidaMoneda;
            }
            catch (Exception e)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.exe", "Frm_Principal", "aMoneda", "", "Error: " + e.Message, "", "");
                return "";
            }
        }
        private String completaTexto(String texto)
        {
            String textosalida;
            String[] textoArreglo;
            try
            {
                textoArreglo = texto.Split(':');
                if (textoArreglo.Length > 1)
                {
                    int longitud = textoArreglo[0].Length + textoArreglo[1].Length;
                    if (longitud < 40)
                    {
                        String puntos = ":";
                        for (int i = longitud; i < 40; i++)
                        {
                            puntos += " ";
                        }
                        textosalida = textoArreglo[0] + puntos + textoArreglo[1];
                    }
                    else
                    {
                        textosalida = textoArreglo[0] + ":" + textoArreglo[1];
                    }
                }
                else
                {
                    textosalida = texto;
                }
                return textosalida;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "completaTexto", "", ex.Message, "", "");
                return null;
            }
        }
        private String completaTexto(String texto1, String texto2)
        {
            String textosalida = texto1 + " " + texto2;
            try
            {
                int longitud = (texto1 + " " + texto2).Length;
                if ((texto1 + " " + texto2).Length < 40)
                {
                    String puntos = "";
                    for (int i = longitud; i < 40; i++)
                    {
                        puntos += " ";
                    }
                    textosalida = texto1 + puntos + texto2;
                }
                if ((texto1 + " " + texto2).Length > 40)
                {
                    return texto1 + "\n" + texto2;
                }

                return textosalida;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "completaTexto", "", ex.Message, "", "");
                return null;
            }
        }

        private void escribeEnTextBox(RichTextBox richTextBox, String stR_textoEscribir)
        {
            try
            {
                richTextBox.SelectionFont = new Font("Courier New", 9, FontStyle.Regular);
                richTextBox.AppendText(stR_textoEscribir);
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "escribeEnTextBox", "", ex.Message, "", "");
            }

        }
        private void encabezado(RichTextBox cajaTexto)
        {
            try
            {
                cajaTexto.SelectionFont = new Font("Courier New", 9, FontStyle.Regular);
                String stL_nombre = armaNombre(stPr_A001PRIMER_APELLIDO, stPr_A001SEGUNDO_APELLIDO, stPr_A001PRIMER_NOMBRE, stPr_A001SEGUNDO_NOMBRE);
                String stencabezado = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Encabezado");
                Bitmap objL_bitmap = new Bitmap(stencabezado);
                Clipboard.SetDataObject(objL_bitmap);
                DataFormats.Format formato = DataFormats.GetFormat(DataFormats.Bitmap);
                cajaTexto.Paste(formato);
                cajaTexto.SelectionFont = new Font("Courier New", 12, FontStyle.Regular);
                String stentidad = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Entidad");
                cajaTexto.AppendText("\n" + stentidad + "\n");
                escribeEnTextBox(cajaTexto, "\n");
                escribeEnTextBox(cajaTexto, "OFICINA      FECHA    HORA\n");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "encabezado", "", ex.Message, "", "");
            }
        }
        private void CMD_EstadCuenta_Click(object sender, EventArgs e)
        {
            int inL_Puerto = Convert.ToInt16(StPr_inPuerto);
            String stL_nombreK = "";
            ClasX_Consultas objL_Consultas = new ClasX_Consultas(stPr_ExeName_Exe, ObjPr_EventLog, stPr_ParamBd);
            SqlDataReader rdr = null;

            CMD_EstadCuenta.Text = "CONSULTANDO...";
            CMD_RENTA.Enabled = false;
            CMD_Certificado.Enabled = false;
            CMD_EstadoTramite.Enabled = false;
            CMD_EstadCuenta.Enabled = false;
            
            //CMD_ImprimirEstado.Enabled = true;
            //string cc = "79345472";
           //int Cta_Numero = 301819;
            try {
                //consulta Sp para #cuenta
                String temp = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
                stL_nombreK = temp;
                temp = ObjPr_ConfigApp.LeeLlave_Seccion(temp, "NOMBRE");
                String[] infoConsulta = Obj_AppKiosko.obtieneToken(stPr_Serv, StPr_inPuerto);//posicion 0 = token, posicion 1 = usuario aDirector
                stL_nombreK += "-" + infoConsulta[1] + "-" + Obj_AppKiosko.Get_My_Ip(stPr_Serv, StPr_inPuerto);
                char[] delim = { ' ', '_' };
                String[] stmpt = temp.Split(delim);
                int cont = 0;
                for (int i = 0; i < stmpt.Length; i++)
                {
                    String tmp = stmpt[i].ToUpper();
                    if (tmp.Contains(st_Oficina1) || tmp.Contains(st_Oficina2) || tmp.Contains(st_Oficina3) || tmp.Contains(st_Oficina4) || tmp.Contains(st_Oficina5) || tmp.Contains(st_Oficina6) || tmp.Contains(st_Oficina7) || tmp.Contains(st_Oficina8) || tmp.Contains(st_Oficina9) || tmp.Contains(st_Oficina10) || tmp.Contains(st_Oficina11))
                    //if (tmp.Contains("CAN") || tmp.Contains("VENECIA") || tmp.Contains("CALI") || tmp.Contains("MEDELLIN") || tmp.Contains("CARTAGENA") || tmp.Contains("BARRANQUILLA") || tmp.Contains("BUCARAMANGA") || tmp.Contains("IBAGUE") || tmp.Contains("FLORENCIA") || tmp.Contains("TOLEMAIDA"))
                    {
                        break;
                    }
                    cont++;
                }

                rdr = objL_Consultas.EjecutaSP("SP_021_ACT_CONSUL_KIOSKO", txt_Identificacion.Text + ",'" + infoConsulta[0] + "','" + stL_nombreK + "','1'");
                rdr = objL_Consultas.EjecutaSP("SP_019_LEE_AFILIADO_CUENTAS_GA2", txt_Identificacion.Text);
                if (rdr != null)
                {
                    long cuenta = 0;
                    while (rdr.Read())
                    {
                        if (!DBNull.Value.Equals(Convert.ToInt64(rdr[0]))) cuenta = Convert.ToInt64(rdr[0]);
                        //fin cinsulta cuenta
                    }
                        //inicia consume web services para generar reporte
                        MCDIntegrationServices.wsModeloCanonicoDatosSoapClient client = new wsModeloCanonicoDatosSoapClient();
                       var a = client.GenerarReporte("REPORTE_HABERES", txt_Identificacion.Text, Convert.ToInt32(cuenta));
                       //var a = client.GenerarReporte("REPORTE_HABERES", cc, Cta_Numero);
                        client.Close();
                        
                        if (a.Report != null && a.Report.Length > 0)
                        {
                                String strL_tempDir = "C:\\fnx";
                               
                                
                            MemoryStream ms = new MemoryStream(a.Report);
                            FileStream file = new FileStream(strL_tempDir+ "\\" + inPr_A001NUM_IDENTIFICACION + "\\estadocuenta.pdf", FileMode.Create, FileAccess.Write);
                            ms.WriteTo(file);
                            file.Close();
                            ms.Close();
                            
                           
                            webBrowser3.Navigate(Convert.ToString("File://"+strL_tempDir + "\\" + inPr_A001NUM_IDENTIFICACION + "\\estadocuenta.pdf"));
                          
                            //carga la imagen del stream en un picturebox
                           
                            //string dialogResult = MyMessageBox.ShowBox("Se genero Certficado de Decalracion de Renta", "", "Aceptar", "Cancelar", true, 42);
                          //  MyMessageBox.ActiveForm.Activate();
                            //MyMessageBox.ActiveForm.BringToFront();

                            
                            objL_Consultas.CierraCOnexiones();
                            cambiaPantallas(PANEL_ESTADO_CUENTA);
                            this.CMD_Prioritario.Visible = false;
                            this.CMD_Regresar.Visible = true;
                        }
                        else
                        {
                            string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_21, "", "Aceptar", "Salir", false, 155);
                            MyMessageBox.ActiveForm.Activate();
                            MyMessageBox.ActiveForm.BringToFront();
                            ObjPr_EventLog.setTextErrLog(" No Se genero  Estado de cuenta");
                            if (dialogResult.Equals("1"))
                            {
                                lblMsjs.Text = "Error de conexión a WEB SERVICES CANONICOS  O GA2";
                                CMD_EstadCuenta.Enabled = true;
                                CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                                CMD_Certificado.Enabled = true;
                                CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                                CMD_EstadoTramite.Enabled = true;
                                CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                                CMD_RENTA.Enabled = true;
                                CMD_RENTA.Text = stPr_DECLARACION;
                            }
                            if (dialogResult.Equals("2"))
                            {
                                VuelveInicio();
                            }
                            
                            
                        }

                        
                    
                   
                }
                else
                {
                    lblMsjs.Text = "Error conexión a GA2";
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Estado_Cuenta_Click(), Error en consula a GA2");
                    string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_21, "", "Aceptar", "Salir", false, 155);
                    MyMessageBox.ActiveForm.Activate();
                    MyMessageBox.ActiveForm.BringToFront();
                    ObjPr_EventLog.setTextErrLog(MENSAJE_APP_17 + txt_Identificacion.Text + MENSAJE_APP_18);
                    if (dialogResult.Equals("1"))
                    {
                        lblMsjs.Text = "Error de conexión a GA2";
                        CMD_EstadCuenta.Enabled = true;
                        CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                        CMD_Certificado.Enabled = true;
                        CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                        CMD_EstadoTramite.Enabled = true;
                        CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                        CMD_RENTA.Enabled = true;
                        CMD_RENTA.Text = stPr_DECLARACION;
                    }
                    if (dialogResult.Equals("2"))
                    {
                        VuelveInicio();
                    }
                }



                CMD_EstadCuenta.Enabled = true;
                CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                CMD_Certificado.Enabled = true;
                CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                CMD_EstadoTramite.Enabled = true;
                CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                CMD_RENTA.Enabled = true;
                CMD_RENTA.Text = stPr_DECLARACION;
            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Estado_Cuenta_Click(), Generar Certificado de Renta");
           
            
           
            }
            catch (Exception ex)
            {
                
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Estado_de_Cuenta_Click", "", ex.Message.ToString(), "", "");
                MyMessageBox.ActiveForm.Activate();
                MyMessageBox.ActiveForm.BringToFront();
                CMD_EstadCuenta.Enabled = true;
                CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                CMD_Certificado.Enabled = true;
                CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                CMD_EstadoTramite.Enabled = true;
                CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                CMD_RENTA.Enabled = true;
                CMD_RENTA.Text = stPr_DECLARACION;
                VuelveInicio();
            
            }
         
          
        
        }
        private String ajustarLongitud(String str_texto)
        {
            String stl_texto = str_texto;
            try
            {
                String[] partes = stl_texto.Split(new char[] { ' ' });
                stl_texto = partes[0];
                int cont = 0;
                for (int i = 1; i < partes.Length; i++)
                {
                    cont = i + 1;
                    if ((stl_texto + partes[i]).Length < 40)
                    {
                        stl_texto += " " + partes[i];
                    }
                    else
                    {
                        stl_texto += "\n    " + partes[i];
                        break;
                    }
                }
                if (cont == partes.Length) return stl_texto;
                for (int i = cont; i < partes.Length; i++)
                {
                    stl_texto += " " + partes[i];
                }
                return stl_texto;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "ajustarLongitud", "", ex.Message, "", "");
                return stl_texto;
            }
        }
        private String EliminaAcento(String stR_cadena)
        {
            String stL_retorno = stR_cadena;
            try
            {
                if (String.IsNullOrEmpty(stR_cadena)) return stL_retorno;
                byte[] btL_tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(stL_retorno);
                stL_retorno = System.Text.Encoding.UTF8.GetString(btL_tempBytes);
                return stL_retorno;
            }
            catch (Exception ex)
            {
                stL_retorno = stR_cadena;
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "EliminaAcento", "", ex.Message, "", "");
                return stL_retorno;
            }
        }
        private void cargaPantallas()
        {
            try
            {
                //this.pnl_Turnos.Location = new System.Drawing.Point(382, 156);
                //this.pnl_Turnos.Size = new System.Drawing.Size(708, 525);
                //this.pnl_certPago.Location = new System.Drawing.Point(402, 167);
                this.pnl_certPago.Size = new System.Drawing.Size(430, 606);
                //this.pnl_ESTADO_TRAMITE.Location = new System.Drawing.Point(402, 167);
                this.pnl_ESTADO_TRAMITE.Size = new System.Drawing.Size(430, 606);
                //this.pnl_huella.Location = new System.Drawing.Point(402, 167);
                this.pnl_huella.Size = new System.Drawing.Size(430, 606);
                //this.pnl_afiliado.Location = new System.Drawing.Point(402, 167);
               // this.pnl_afiliado.Size = new System.Drawing.Size(430, 606);
                //this.pnl_Consultas.Location = new System.Drawing.Point(402, 167);
               // this.pnl_Consultas.Size = new System.Drawing.Size(430, 606);
                //this.pnl_EstadoCuenta.Location = new System.Drawing.Point(402, 167);
               // this.pnl_EstadoCuenta.Size = new System.Drawing.Size(430, 606);
                this.pnl_Turno.Location = new System.Drawing.Point(200, 353);
                this.pnl_Turno.Size = new System.Drawing.Size(430, 315);
                this.pnl_Inicio.Size = new System.Drawing.Size(430, 606);
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "cargaPantallas", "", ex.Message, "", "");
            }
        }
        private bool apagaTodosLosPanels()
        {
            try
            {
                this.pnl_certPago.Visible = false;
                this.pnl_teclado.Visible = false;
                this.pnl_EstadoCuenta.Visible = false;
                Application.DoEvents();
               
               // this.pnl_Consultas.Visible = false;
                this.pnl_huella.Visible = false;
                Application.DoEvents();
                this.pnl_TurnosInfoAfiliado.Visible = false;
                //this.pnl_TurnosRadicacion.Visible = false;
                this.pnl_Turno.Visible = false;
                this.pnl_ESTADO_TRAMITE.Visible = false;
                this.pnl_Inicio.Visible = false;
                this.pnl_NoAfiliado.Visible = false;
                Application.DoEvents();
                //Botones
                this.CMD_Cancelar.Visible = false;
                this.CMD_Prioritario.Visible = false;
                this.CMD_Regresar.Visible = false;
                Application.DoEvents();
                return true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "apagaTodosLosPanels", "", ex.Message, "", "");
                return false;
            }
        }
        /// <summary>
        /// Metodo para intercambiar las pantallas
        /// </summary>
        /// <param name="stL_nombrePantalla">Panel actual que tiene la propiedad visible = true</param>
        private void cambiaPantallas(String stL_nombrePantalla)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  Inicia ...");
                //StopTimer();
                Application.DoEvents();
                apagaTodosLosPanels();
                if (stL_nombrePantalla.Equals(PANEL_TURNOSINFOAFILIADO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL TURNOS AFILIADOS");

                    //Application.DoEvents();
                    //this.pnl_certPago.Visible = false;
                    //this.pnl_teclado.Visible = false;
                    //this.pnl_EstadoCuenta.Visible = false;
                    //this.pnl_afiliado.Visible = false;
                    //this.pnl_Consultas.Visible = false;
                    //this.pnl_huella.Visible = false;
                    //pnl_TurnosRadicacion.Visible = false;
                    this.pnl_TurnosInfoAfiliado.Visible = true;
                    pnl_TurnosInfoAfiliado.BringToFront();
                    pnl_TurnosInfoAfiliado.Left = ((this.Width - pnl_teclado.Width) / 2 - 550);
                    pnl_TurnosInfoAfiliado.Top = ((this.Height - pnl_teclado.Height) / 2 );
                   // pnl_infoAfiliado.Left = ((this.Width - pnl_teclado.Width) / 2 - 680);
                    //pnl_TurnosInfoAfiliado.Top = ((this.Height - pnl_teclado.Height) / 2 + 45);
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    this.CMD_Cancelar.Visible = true;
                    this.CMD_Prioritario.Visible = true;
                   // this.CMD_Regresar.Visible = true;
                    if (stPr_modo.Equals("lite"))
                    {
                        this.CMD_Cancelar.Visible = true;
                        this.CMD_Turnos.Visible = false;
                        this.CMD_Prioritario.Visible = false;
                        this.CMD_TurnosRadicacion.Visible = false;
                        this.CMD_turnoleasingafiliado.Visible = false;
                        this.CDM_Turno8afiliado.Visible = false;
                        this.CMD_Turno14afiliado.Visible = false;
                        this.CMD_Turnofuturoafiliado.Visible = false;
                        this.CMD_Turnopretramiteafiliado.Visible = false;
                        this.CMD_Turnotramiteafiliado.Visible = false;
                        this.CMD_Leasing.Visible = false;
                        this.CMD_Vivienda14.Visible = false;
                        this.CMD_Vivienda8.Visible = false;
                        this.CMD_Heroes.Visible = false;
                        this.CMD_Futuro.Visible = false;
                        this.CMD_Tramite.Visible = false;
                        this.CMD_Cuenta.Visible = false;
                        this.CMD_Agenda_cita.Visible = false;
                        CMD_Atencion_cita.Visible = false;
                        pnl_TurnosInfoAfiliado.BringToFront();
                        pnl_TurnosInfoAfiliado.Left = ((this.Width - pnl_teclado.Width) / 2 - 200);
                        pnl_TurnosInfoAfiliado.Top = ((this.Height - pnl_teclado.Height) / 2);
                        label6.Left = ((this.Width - pnl_TurnosInfoAfiliado.Width) / 2 - 590);
                        pnl_infoAfiliado.Left = ((this.Width - pnl_teclado.Width) / 2 - 645);
                        
                        //CMD_Cancelar.Left = ((this.Width - pnl_infoAfiliado.Width) / 2 + 300);
                       

                    }
                    if (blPr_desactivaturnosP == true)
                    {
                        this.CMD_Turnos.Visible = false;
                    }
                    Application.DoEvents();
                    StartTimer();
                }

                if (stL_nombrePantalla.Equals(PANEL_CERTIFICAO_PAGO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL_CERTIFICAO_PAGO");
                    //StopTimer();
                    //Application.DoEvents();
                    //this.pnl_Turnos.Visible = false;
                    //this.pnl_teclado.Visible = false;
                    //this.pnl_EstadoCuenta.Visible = false;
                    //this.pnl_afiliado.Visible = false;
                    //this.pnl_Consultas.Visible = false;
                    //this.pnl_huella.Visible = false;
                    this.pnl_certPago.Visible = true;
                    pnl_certPago.BringToFront();
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    this.CMD_Cancelar.Visible = true;
                    this.CMD_ImprimirCerti.Enabled = true;
                    //this.CMD_Prioritario.Visible = false;
                    this.CMD_Regresar.Visible = true;
                    Application.DoEvents();
                    StartTimer();
                }
                if (stL_nombrePantalla.Equals(PANEL_ESTADO_TRAMITE))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL_ESTADO_TRAMITE");
                    //StopTimer();
                    //Application.DoEvents();
                    //this.pnl_Turnos.Visible = false;
                    //this.pnl_huella.Visible = false;
                    //this.pnl_certPago.Visible = false;
                    //this.pnl_teclado.Visible = false;
                    //this.pnl_EstadoCuenta.Visible = false;
                    //this.pnl_afiliado.Visible = false;
                    //this.pnl_Consultas.Visible = false;
                    this.pnl_ESTADO_TRAMITE.Visible = true;
                    pnl_ESTADO_TRAMITE.BringToFront();
                    pnl_ESTADO_TRAMITE.Left = ((this.Width - pnl_teclado.Width) / 2 - 100);
                    pnl_ESTADO_TRAMITE.Top = ((this.Height - pnl_teclado.Height) / 2);
                    this.CMD_Cancelar.Visible = true;
                    CMD_ImprimirEstado.Enabled = true;
                    this.CMD_Prioritario.Visible = true;
                    this.CMD_Regresar.Visible = true;
                    Application.DoEvents();
                    StartTimer();
                }
                if (stL_nombrePantalla.Equals(PANEL_DECLARACION))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL_DECLARACION");
                    //StopTimer();
                    //Application.DoEvents();
                    //this.pnl_Turnos.Visible = false;
                    //this.pnl_huella.Visible = false;
                    //this.pnl_certPago.Visible = false;
                    //this.pnl_teclado.Visible = false;
                    //this.pnl_EstadoCuenta.Visible = false;
                    //this.pnl_afiliado.Visible = false;
                    //this.pnl_Consultas.Visible = false;

                    this.pnl_decla.Visible = true;
                    pnl_decla.BringToFront();
                    pnl_decla.Left = ((this.Width - pnl_teclado.Width) / 2 - 100);
                    pnl_decla.Top = ((this.Height - pnl_teclado.Height) / 2);
                    this.CMD_Cancelar.Visible = true;
                    this.cmd_imprimir_dela.Enabled = true;
                    //this.CMD_Prioritario.Visible = false;
                    this.CMD_Regresar.Visible = true;
                    Application.DoEvents();
                    StartTimer();
                }
                if (stL_nombrePantalla.Equals(PANEL_CAPTURA_HUELLA))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL_CAPTURA_HUELLA");
                    //StopTiemrHuella();
                    label1.Text = "Coloque su dedo";
                    label1.Text += "\nen el dispositivo ";
                    //label1.AutoSize = true;
                    this.pnl_huella.Visible = true;
                    this.ptb_HuellaEstatica.Visible = true;
                    ptb_HuellaEstatica.BringToFront();
                    //this.CMD_Prioritario.Enabled = false;
                    //StopTimer();
                    Application.DoEvents();
                    //this.pnl_Turnos.Visible = false;
                    //this.pnl_certPago.Visible = false;
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    //this.pnl_teclado.Visible = false;
                    //this.pnl_EstadoCuenta.Visible = false;
                    //this.pnl_afiliado.Visible = false;
                    //this.pnl_Consultas.Visible = false;
                    this.CMD_Cancelar.Visible = true;
                   
                    StartTiemrHuella();
                    StartTimer();
                }
                if (stL_nombrePantalla.Equals(PANEL_AFILIADO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL_AFILIADO");
                    //StopTimer();
                    //if ((stPr_modo.Equals("lite")))
                    //{
                    //    CMD_Turnos.Visible = false;
                    //    CMD_Prioritario.Visible = false;
                    //}
                    //else
                    //{
                    //    CMD_Turnos.Visible = true;
                    //    CMD_Prioritario.Visible = true;
                    //    CMD_Prioritario.Enabled = true;
                    //}
                    //Application.DoEvents();
                    //this.pnl_Turnos.Visible = false;
                    //this.pnl_certPago.Visible = false;
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    //this.pnl_huella.Visible = false;
                    //this.pnl_Consultas.Visible = false;
                    //this.pnl_teclado.Visible = false;
                    //this.pnl_EstadoCuenta.Visible = false;
                   
                    
                    this.CMD_Cancelar.Visible = true;
                    //this.CMD_Regresar.Visible = false;
                    if (!(stPr_modo.Equals("lite")))
                    {
                        this.CMD_Prioritario.Visible = true;
                        this.CMD_Turnos.Visible = true;
                        this.CMD_Prioritario.Visible = true;
                        this.CMD_Prioritario.Enabled = true;
                    }
                    if (blPr_desactivaturnosP == true)
                    {
                        this.CMD_Turnos.Visible = false;
                    }
                    Application.DoEvents();
                    StartTimer();
                }
                if (stL_nombrePantalla.Equals(PANEL_CONSULTAS))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL_CONSULTAS");
                    //StopTimer();
                    //Application.DoEvents();
                    //detieneEscaner();
                    //this.pnl_Turnos.Visible = false;
                    //this.pnl_certPago.Visible = false;
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    //this.pnl_afiliado.Visible = false;
                    //this.pnl_teclado.Visible = false;
                    //this.pnl_EstadoCuenta.Visible = false;
                    //this.pnl_afiliado.Visible = false;
                    //this.pnl_huella.Visible = false;
                   // this.pnl_Consultas.Visible = true;
                   // pnl_Consultas.BringToFront();
                    this.CMD_Cancelar.Visible = true;
                    this.CMD_Regresar.Visible = true;
                    //this.CMD_Prioritario.Visible = false;
                    //Application.DoEvents();
                    //StartTimer();
                }
                if (stL_nombrePantalla.Equals(PANEL_ESTADO_CUENTA))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL_ESTADO_CUENTA");
                    //StopTimer();
                    //Application.DoEvents();
                    //this.pnl_Turnos.Visible = false;
                    //this.pnl_certPago.Visible = false;
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    //this.pnl_Consultas.Visible = false;
                    //this.pnl_huella.Visible = false;
                    //this.pnl_afiliado.Visible = false;
                    //this.pnl_teclado.Visible = false;

                    this.pnl_EstadoCuenta.Visible = true;
                    pnl_EstadoCuenta.BringToFront();
                    pnl_EstadoCuenta.Left = ((this.Width - pnl_teclado.Width) / 2 - 100);
                    pnl_EstadoCuenta.Top = ((this.Height - pnl_teclado.Height) / 2);
                    this.CMD_Cancelar.Visible = true;
                    CMD_ImprimirEstado.Enabled = true;
                   this.CMD_Regresar.Visible = true;
                    //this.CMD_Prioritario.Visible = false;
                    Application.DoEvents();
                    StartTimer();
                }


               

                if (stL_nombrePantalla.Equals(PANEL_NOAFILIADO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL TURNOS");

                    this.pnl_NoAfiliado.Visible = true;
                    pnl_NoAfiliado.BringToFront();
                    //pnl_NoAfiliado.Left = ((this.Width - pnl_teclado.Width) / 2 - 100);
                    //pnl_NoAfiliado.Top = ((this.Height - pnl_teclado.Height) / 2 + 45);
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    this.CMD_Cancelar.Visible = true;
                    this.CMD_Prioritario.Visible = false;
                    this.CMD_Regresar.Visible = true;
                    Application.DoEvents();
                    StartTimer();
                }
                if (stL_nombrePantalla.Equals(PANEL_INFONOAFILIADO))
                {
                                      
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL TURNOS NO AFILIADOS  ");
                    this.pnl_TurnosNAfiliados.Visible = true;
                    pnl_TurnosNAfiliados.BringToFront();
                    pnl_TurnosNAfiliados.Left = ((this.Width - pnl_teclado.Width) / 2 - 100);
                    pnl_TurnosNAfiliados.Top = ((this.Height - pnl_teclado.Height) / 2 );
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    this.CMD_Cancelar.Visible = true;
                    this.CMD_Prioritario.Visible = true;
                    Application.DoEvents();
                    StartTimer();
                }
                if (stL_nombrePantalla.Equals(PANEL_INFOBENEFICIARIO))
                {
                                      
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL TURNOS NO AFILIADOS  ");
                    
                    //this.pnl_TurnosNAfiliados.Visible = false;
                    this.pnlTurnosBeneficiarios.Visible = true;
                    pnlTurnosBeneficiarios.BringToFront();
                    pnlTurnosBeneficiarios.Left = ((this.Width - pnl_teclado.Width) / 2 - 100);
                    pnlTurnosBeneficiarios.Top = ((this.Height - pnl_teclado.Height) / 2);
                    //this.pnl_ESTADO_TRAMITE.Visible = false;
                    this.CMD_Cancelar.Visible = true;
                    this.CMD_Prioritario.Visible = true;
                    
                    Application.DoEvents();
                    StartTimer();
                }
               
                if (stL_nombrePantalla.Equals(PANEL_INICIO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  PANEL INICIO");
                    this.pnl_Inicio.Visible = true;
                    pnl_Inicio.BringToFront();
                    this.CMD_Cancelar.Visible = false;
                    this.CMD_Regresar.Visible = false;
                    this.CMD_Prioritario.Visible = false;
                    if (stPr_modo.Equals("lite"))
                    {                       

                      
                        
                        this.cmd_NoAfiliado.Visible = false;
                       

                    }
                    if (blPr_desactivaturnosP == true)
                    {
                        this.CMD_Turnos.Visible = false;
                    }

                }

                if (stL_nombrePantalla.Equals(PANEL_TECLADO_INICIAL))
                {
                    Application.DoEvents();
                    StartTimer();
                    ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(), PANEL_TECLADO_INICIAL");
                    lbl_Identificacion.ForeColor = Color.Red;
                    this.pnl_teclado.Visible = true;
                    this.pnl_teclado.BringToFront();
                   // cmd_Accept.Enabled = false;
                    this.CMD_Cancelar.Visible = true;
                    this.CMD_Regresar.Visible = false;
                    if(blPr_Apoderado)
                    {
                        lbl_Identificacion.Text = "Identificación Apoderado/Autorizado";
                        
                    }
                    if (blPr_Autorizado)
                    {
                        lbl_Identificacion.Text = "Identificación Autorizado";
                    }
                    if (blPr_Beneficiario)
                    {
                        lbl_Identificacion.Text = "Identificación Beneficiario";
                    }

                   
                }


                Application.DoEvents();
                StartTimer();
               
                
                ObjPr_EventLog.setTextErrLog("frm_Principal.cambiaPantallas(),  Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "cambiaPantallas", "", ex.Message, "", "");
            }
        }

        private String armaNombre(String stR_PApellido, String stR_SApellido, String stR_PNombre, String stR_SNombre)
        {
            try
            {
                String stL_nomAux = stPr_A001PRIMER_NOMBRE;
                if (!stPr_A001SEGUNDO_NOMBRE.Equals(""))
                {
                    stL_nomAux = stL_nomAux + " " + stPr_A001SEGUNDO_NOMBRE;
                }
                if (!stPr_A001SEGUNDO_APELLIDO.Equals(""))
                {
                    stL_nomAux = stL_nomAux + " " + stPr_A001PRIMER_APELLIDO + " " + stPr_A001SEGUNDO_APELLIDO;
                }
                else
                {
                    stL_nomAux = stL_nomAux + " " + stPr_A001PRIMER_APELLIDO;
                }
                return stL_nomAux;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "armaNombre", "", ex.Message, "", "");
                return "";
            }
        }

        private void borraDirTemporales()
        {
            try

            {

                ObjPr_EventLog.setTextErrLog("frm_Principal.borraDirTemporales(), Inicia");
                if (Directory.Exists("C:\\fnx"))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.borraDirTemporales(), Borrando C:\\fnx ... ");
                    Directory.Delete("C:\\fnx", true);
                    ObjPr_EventLog.setTextErrLog("frm_Principal.borraDirTemporales(), C:\\fnx Borrado OK ... ");
                }
                if (Directory.Exists("C:\\fnxtemp\\"))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.borraDirTemporales(), Borrando C:\\fnxtemp\\ ... ");
                    Directory.Delete("C:\\fnxtemp\\", true);
                    ObjPr_EventLog.setTextErrLog("frm_Principal.borraDirTemporales(), C:\\fnxtemp\\ Borrado OK ... ");
                }
                ObjPr_EventLog.setTextErrLog("frm_Principal.borraDirTemporales(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "borraDirTemporales", "", ex.Message, "", "");
            }
        }



        private bool escaneractivo = false;
        /// <summary>
        /// Permite activar el Escanner de Huella
        /// </summary>
        /// <returns></returns>
        private bool activarEscaner()
        {
            try
            {
                objPr_huellero_Iden_Veri = null;
                ObjPr_EventLog.setTextErrLog("frm_Principal.ActivaEscaner(),  Cargando dispositivo de Huella ...");
                MSJ("Cargando Dispositivo...");

                objPr_huellero_Iden_Veri = new _C_Devices_N_4_3.ClasX_FingerPrint_Device(stPr_UsuarioApp, stPr_ArchivoLog);
                UpdateDeviceList();
                if (objPr_huellero_Iden_Veri.getLicStatus())
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.ActivaEscaner(),  Licencias de Huella Ok");
                    MSJ("Licencia de huellero OK ...");
                    if(bl_ExisteHuellero)
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.ActivaEscaner(),  Estado del Huellero OK ");
                        MSJ("Estado del Huellero OK ...");
                        objPr_huellero_Iden_Veri.setTimeoutHuellero(TimeOutHuellero);
                        objPr_huellero_Iden_Veri.avisarTermine += new avisaFin(dev_avisarTermine);
                        objPr_huellero_Iden_Veri.AvisaGrabar += new ListoParaGrabar(dev_ListoGrabar);
                        //
                        objPr_huellero_Iden_Veri.ScannAction(-1);
                        Application.DoEvents();
                        escaneractivo = true;
                        return true;
                    }
                    else
                    {
                        escaneractivo = false;
                        MSJ("No se detecta Huellero");
                        return false;
                    }
                }
                else
                {
                    escaneractivo = false;
                    MSJ("No se detecta Licencia");
                    return false;
                }             
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "ActivaEscaner", "", ex.Message, "", "");
                return false;
            }
        }



        int contarMatame = 0; // hasta 1300
        /// <summary>
        /// Permite detMatameener el escanner de huella
        /// </summary>
        /// <param name="ponerennull"></param>
        /// <returns></returns>
        private bool detieneEscaner(bool ponerennull)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), Inicia");
                if (!escaneractivo)
                {
                    return false;
                }
                //Strail 19012015, intentando detener el escanner
                //Puedo decir que es una PHP
                objPr_huellero_Iden_Veri.ScannAction(0);
                //
                contarMatame = 0;
                while (estadoEscaneo == -9)
                {
                    //ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), Esperando estadoEscaneo : " + estadoEscaneo);
                    contarMatame++;
                    if (contarMatame >= 800)
                    {
                        objPr_huellero_Iden_Veri.CancelScanning();
                        break;
                    }
                    Application.DoEvents();
                }

                if (objPr_huellero_Iden_Veri != null)
                {

                    ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), Cancelando Escaner ... ");

                    if (objPr_huellero_Iden_Veri.CancelScanning())
                    {
                        if (objPr_huellero_Iden_Veri.StopAction())
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), Deteniendo Escaner ... ");
                            if (ponerennull)
                            {
                                ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), Destruimos Objeto : ObjPr_FingerPrint_Device = NULL");

                                objPr_huellero_Iden_Veri = null;

                            }
                            ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), Escaner detenido OK");
                            escaneractivo = false;
                            return true;
                        }
                        else
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), ERROR no se puede Stopiar el escanner");
                            //ObjPr_FingerPrint_Device = null;
                            return false;
                        }
                    }
                    else
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), ERROR no se puede Cancelar el escanner");
                        //ObjPr_FingerPrint_Device = null;
                        return false;
                    }
                }
                else
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.detieneEscaner(), Escanner esta en NULL ");
                    //ObjPr_FingerPrint_Device = null;
                    return true;
                }

            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "detieneEscaner", "", ex.Message, "", "");
                //ObjPr_FingerPrint_Device = null;
                return false;
            }
        }

        private bool consultaCedulaEnGA2()
        {
            try
            {

                if (!txt_Identificacion.Text.Contains("Cédula..."))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.consultaCedulaEnGA2(),  Ejecutando SP: SP_010_LEE_AFILIADO_GA2 para cc: " + txt_Identificacion.Text);
                    ClasX_Consultas objL_Consultas = new ClasX_Consultas(stPr_ExeName_Exe, ObjPr_EventLog, stPr_ParamBd);
                    SqlDataReader rdr = objL_Consultas.EjecutaSP("SP_010_LEE_AFILIADO_GA2", txt_Identificacion.Text);
                    inPr_A001NUM_IDENTIFICACION = Convert.ToUInt64(txt_Identificacion.Text);
                    if (rdr != null) // Valida que hay registros en el Data Reader
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.consultaCedulaEnGA2(),  rdr OK ...");
                        int inL_contador = 0;
                        while (rdr.Read())
                        {
                            if (!DBNull.Value.Equals(Convert.ToInt32(rdr[0]))) inPr_AO01ID = Convert.ToInt32(rdr[0]);
                            if (!DBNull.Value.Equals(Convert.ToUInt64(rdr[1]))) inPr_A001NUM_IDENTIFICACION = Convert.ToUInt64(rdr[1]);
                            if (!DBNull.Value.Equals(Convert.ToString(rdr[2]))) stPr_A001PRIMER_APELLIDO = Convert.ToString(rdr[2]);
                            if (!DBNull.Value.Equals(Convert.ToString(rdr[3]))) stPr_A001SEGUNDO_APELLIDO = Convert.ToString(rdr[3]);
                            if (!DBNull.Value.Equals(Convert.ToString(rdr[4]))) stPr_A001PRIMER_NOMBRE = Convert.ToString(rdr[4]);
                            if (!DBNull.Value.Equals(Convert.ToString(rdr[5]))) stPr_A001SEGUNDO_NOMBRE = Convert.ToString(rdr[5]);
                            inL_contador++;
                        } while (rdr.NextResult()) ;
                        objL_Consultas.CierraCOnexiones();
                        if (inL_contador != 0)
                        {
                            return true;
                        }
                        else
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.consultaCedulaEnGA2(),  ERROR No hay registro");
                            return false;
                        }
                    }
                    else
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.consultaCedulaEnGA2(),  ERROR No existen datos");
                        return false;
                    }
                }
                else
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.consultaCedulaEnGA2(),  ERROR No hay una cedula en el textbox");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "consultaCedulaEnGA2", "", ex.Message, "", "");
                return false;
            }

        }

        private bool ejecutaFuncionArreglaPantalla = true;
        private bool ejecutandoLPVI = false;

        private bool daPermisoEjecutar()
        {
            try
            {
                //Strail 29122014, variable para el manejo de ejecuion de funcionaes VuelveInicio, LimpiaPantallas
                //ObjPr_EventLog.setTextErrLog("frm_Principal.daPermisoEjecutar(), inicia");
                int int_timeoutEspera = 0;
                while (ejecutandoLPVI)
                {
                    int_timeoutEspera++;
                    if (int_timeoutEspera > 500)
                    {
                        //ObjPr_EventLog.setTextErrLog("frm_Principal.daPermisoEjecutar(), Esperando ...");
                        ejecutaFuncionArreglaPantalla = true;
                        Application.DoEvents();
                        return true;
                        
                    }
                }
                ejecutaFuncionArreglaPantalla = true;
                return true;
                //ObjPr_EventLog.setTextErrLog("frm_Principal.daPermisoEjecutar(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "daPermisoEjecutar", "", ex.Message, "", "");
                return false;
            }
        }

        private void LimpiaPantallas()
        {
            try
            {
                ejecutandoLPVI = true;
                ObjPr_EventLog.setTextErrLog("frm_Principal.LimpiaPantallas(), Inicia ...");
                this.pnl_Inicio.Visible = true;
                this.pnl_teclado.Visible = false;
                this.pnl_certPago.Visible = false;
                //this.pnl_Consultas.Visible = false;
                this.pnl_EstadoCuenta.Visible = false;
                this.pnl_huella.Visible = false;
                this.pnl_ESTADO_TRAMITE.Visible = false;
                this.pnlTurnosBeneficiarios.Visible = false; 
                this.CMD_Regresar.Visible = false;
                this.CMD_Cancelar.Visible = false;
                this.pnl_TurnosInfoAfiliado.Visible = false;
                this.blPr_Beneficiario = false;
                this.blPr_Apoderado = false;
                this.blPr_apo = false;
                this.blPr_afi = false;
                this.blPr_NoAfiliado = false;
                this.pnl_NoAfiliado.Visible = false;
                this.pnl_TurnosNAfiliados.Visible = false;
                Application.DoEvents();
                VuelveInicio();

                if (imagen != null)
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.LimpiaPantallas(), Imagen Dispose ... ");
                    imagen.Dispose();
                    imagen = null;
                }
                inPr_A001NUM_IDENTIFICACION = 0;
                stPr_A001PRIMER_NOMBRE = "";
                stPr_A001SEGUNDO_NOMBRE = "";
                stPr_A001PRIMER_APELLIDO = "";
                stPr_A001SEGUNDO_APELLIDO = "";
                blPr_NoGA2 = false;

                blPr_desactivaturnosP = false;
                String stPr_numdoc = txt_Identificacion.Text;
                this.lbl_ceduAfi.Text = "CEDULA:";
                this.pnl_vistaHuella.Controls.Add(ptb_HuelaPrelim);
                this.pnl_vistaHuella.Controls.Add(ptb_HuellaEstatica);
                //ptb_HuelaPrelim.Visible = true;
                //ptb_HuellaEstatica.Visible = false;
                //Strail 05112014
                //if (traerbio.IsAlive)
                //{
                //    traerbio.Abort();
                //}
                if (terminedetraer == -9)
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.LimpiaPantallas(), Eliminando thread ..");

                    traerbio = null;
                    terminedetraer = -1;
                }
                // ASQC Julio 16 2017. Cambia Condicion.
                //if (!(stPr_modo.Equals("lite")))
                if ((stPr_modo.Equals("lite")))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.LimpiaPantallas(), Modo LITE detectado ..");
                    CMD_Turnos.Visible = false;
                    CMD_Prioritario.Visible = false;
                }
                else
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.LimpiaPantallas(), Modo FULL detectado ..");
                    CMD_Prioritario.Visible = false;
                  
                }

                KillAdobe("AcroRd32");
                this.CMD_Prioritario.Visible = false;
                CMD_EstadCuenta.Text = "ESTADO CUENTA";
                CMD_EstadCuenta.Enabled = true;
                CMD_EstadoTramite.Enabled = true;
                CMD_EstadoTramite.Text = "ESTADO TRAMITE";
                CMD_Certificado.Text = "CERTIFICADO PAGO";
                CMD_Certificado.Enabled = true;
                if (!txt_Identificacion.Text.Contains("Cédula..."))
                {
                    txt_Identificacion.Text = "Cédula...";
                    txt_Identificacion.ForeColor = Color.Black;
                }

                ejecutandoLPVI = false;
                lblMsjs.Text = "Presentando pantalla principal";

                ObjPr_EventLog.setTextErrLog("frm_Principal.LimpiaPantallas(), TERMINA OK : " + stPr_numdoc);
                //ObjPr_EventLog.setTextErrLog("---------- Finalizando proceso para cc: " + stPr_numdoc + " ----------");
                //Strail 23102014 Alzheimer();
                //Application.DoEvents();
            }
            catch (Exception ex)
            {
                ejecutandoLPVI = false;
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "LimpiaPantallas", "", ex.Message, "", "");
            }
        }
        private void CMD_Cancelar_Click(object sender, EventArgs e)
        {
            try
            {
                CMD_Cancelar.Visible = false;
                Thread.Sleep(1000);
                //VuelveInicio();
                LimpiaPantallas();
                //Inicializar();
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Cancelar_Click", "", ex.Message, "", "");
            }
        }

        private void CMD_ImprimirEstado_Click(object sender, EventArgs e)
        {
            String strL_tempDir = "C:\\fnx";
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ImprimirEstado_cuenta_Click(), Iniciando ...");
                cmd_imprimir_dela.Enabled = false;
                this.pnl_decla.Enabled = false;
                
                string Filepath = strL_tempDir + "\\" + inPr_A001NUM_IDENTIFICACION + "\\estadocuenta.pdf";
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";
                            
                proc.StartInfo.FileName =
                  @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";
                proc.StartInfo.Arguments = String.Format(@"/p /h {0}", Filepath);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();
                KillAdobe("AcroRd32");
                LimpiaPantallas();
                this.pnl_decla.Enabled = true;
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ImprimirEstado_cuenta_Click(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "frm_Principal.CMD_ImprimirEstado_cuenta)", "", ex.Message.ToString(), "", "");
            }
            
        }

        private void CMD_EstadoTramite_Click(object sender, EventArgs e)
        {
            CMD_EstadoTramite.Text = "CONSULTANDO...";
            ClasX_Consultas objL_Consultas = new ClasX_Consultas(stPr_ExeName_Exe, ObjPr_EventLog, stPr_ParamBd);
            SqlDataReader rdr = null;
            String stL_nombreK = "";
            CMD_EstadoTramite.Enabled = false;
            CMD_Certificado.Enabled = false;
            CMD_EstadCuenta.Enabled = false;
            CMD_imprimirEstTramite.Enabled = true;
            CMD_RENTA.Enabled = false;
            
            rTxt_Tramites.Clear();
            try
            {
                encabezado(rTxt_Tramites);
                String temp = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
                stL_nombreK = temp;
                String[] infoConsulta = Obj_AppKiosko.obtieneToken(stPr_Serv, StPr_inPuerto);//posicion 0 = token, posicion 1 = usuario aDirector
                stL_nombreK += "-" + infoConsulta[1] + "-" + Obj_AppKiosko.Get_My_Ip(stPr_Serv, StPr_inPuerto);
                temp = ObjPr_ConfigApp.LeeLlave_Seccion(temp, "NOMBRE");
                char[] delim = { ' ', '_' };
                String[] stmpt = temp.Split(delim);
                int cont = 0;
                for (int i = 0; i < stmpt.Length; i++)
                {
                    String tmp = stmpt[i].ToUpper();
                    if (tmp.Contains(st_Oficina1) || tmp.Contains(st_Oficina2) || tmp.Contains(st_Oficina3) || tmp.Contains(st_Oficina4) || tmp.Contains(st_Oficina5) || tmp.Contains(st_Oficina6) || tmp.Contains(st_Oficina7) || tmp.Contains(st_Oficina8) || tmp.Contains(st_Oficina9) || tmp.Contains(st_Oficina10) || tmp.Contains(st_Oficina11))
                    {
                        break;
                    }
                    cont++;
                }
                escribeEnTextBox(rTxt_Tramites, stmpt[cont] + "    ");
                temp = DateTime.Now.ToString();
                stmpt = temp.Split(delim);
                escribeEnTextBox(rTxt_Tramites, stmpt[0] + "   " + stmpt[1] + stmpt[2] + "\nNoCONSULTA: " + infoConsulta[0] + "\n");
                temp = armaNombre(stPr_A001PRIMER_APELLIDO, stPr_A001SEGUNDO_APELLIDO, stPr_A001PRIMER_NOMBRE, stPr_A001SEGUNDO_NOMBRE);
                temp = temp.ToUpper();
                escribeEnTextBox(rTxt_Tramites, temp + "\n");
                rdr = objL_Consultas.EjecutaSP("SP_021_ACT_CONSUL_KIOSKO", txt_Identificacion.Text + ",'" + infoConsulta[0] + "','" + stL_nombreK + "','3'");
                rdr = objL_Consultas.EjecutaSP("SP_020_LEE_AFILIADO_INF_CAT", txt_Identificacion.Text);
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        String stL_linea = "";
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[9]))) stL_linea = "CATEGORIA: " + Convert.ToString(rdr[9]) + "\n";
                        escribeEnTextBox(rTxt_Tramites, stL_linea);
                    }
                }
                escribeEnTextBox(rTxt_Tramites, "C.C: " + txt_Identificacion.Text + "\n      CONSULTA DE TRAMITES\n\n");
                if (SP_TRAMITE == 1) rdr = objL_Consultas.EjecutaSP("SP_012_LEE_AFILIADO_ESTADO_TRAMITE_GA2", txt_Identificacion.Text);

                if (SP_TRAMITE == 2) rdr = objL_Consultas.EjecutaSP("SP_017_LEE_AFILIADO_ESTADO_TRAMITE_GA2", txt_Identificacion.Text);
                if (rdr != null)
                {
                    String stL_linea = "";
                    char[] delimitador = { ' ' };
                    int inL_contador = 0;
                    while (rdr.Read())
                    {
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[2]))) stL_linea = Convert.ToString(rdr[2]) + "\n";
                        escribeEnTextBox(rTxt_Tramites, stL_linea);
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[8]))) stL_linea = "NUMERO RADICACION: " + Convert.ToString(rdr[8]) + "\n";
                        escribeEnTextBox(rTxt_Tramites, stL_linea);
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[3]))) stL_linea = Convert.ToString(rdr[3]);
                        String[] lineas = stL_linea.Split(delimitador);
                        stL_linea = "FECHA RADICACION: " + lineas[0] + "\n";
                        escribeEnTextBox(rTxt_Tramites, stL_linea);
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[6]))) stL_linea = "ESTADO:" + Convert.ToString(rdr[6]) + "\n";
                        escribeEnTextBox(rTxt_Tramites, stL_linea + "\n");
                        inL_contador++;
                    } while (rdr.NextResult()) ;
                    if (inL_contador == 0)
                    {
                        escribeEnTextBox(rTxt_Tramites, "   AFILIADO NO PRESENTA TRAMITES EN LA ENTIDAD\n");
                    }

                    piePagina(rTxt_Tramites);
                    CMD_EstadoTramite.Enabled = true;
                    cambiaPantallas(PANEL_ESTADO_TRAMITE);
                    this.CMD_Prioritario.Visible = false;
                    //this.CMD_Regresar.Visible = true;
                }
                else
                {
                    CMD_EstadoTramite.Enabled = true;

                    lblMsjs.Text = "Error consultando en GA2";
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Certificado_Click(), Error en consula a GA2");
                    string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_21, "", "Aceptar", "Salir", false, 155);
                    MyMessageBox.ActiveForm.Activate();
                    MyMessageBox.ActiveForm.BringToFront();
                    ObjPr_EventLog.setTextErrLog(MENSAJE_APP_17 + txt_Identificacion.Text + MENSAJE_APP_18);
                    if (dialogResult.Equals("1"))
                    {
                        lblMsjs.Text = "Error de conexión a GA2";
                        CMD_EstadCuenta.Enabled = true;
                        CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                        CMD_Certificado.Enabled = true;
                        CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                        CMD_EstadoTramite.Enabled = true;
                        CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                        CMD_RENTA.Enabled = true;
                        CMD_RENTA.Text = stPr_DECLARACION;
                    }
                    if (dialogResult.Equals("2"))
                    {
                        VuelveInicio();
                    }
                    //MessageBox.Show("No se ha encontrado informacion", "No se obtuvieron resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CMD_EstadCuenta.Enabled = true;
                CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                CMD_Certificado.Enabled = true;
                CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                CMD_EstadoTramite.Enabled = true;
                CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                CMD_RENTA.Enabled = true;
                CMD_RENTA.Text = stPr_DECLARACION;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_EstadoTramite_Click", "", ex.Message.ToString(), "", "");
            }

        }
        private void piePagina(RichTextBox cajaTexto)
        {
            try
            {
                escribeEnTextBox(cajaTexto, "\n        " + ObjPr_ConfigApp.LeeLlave_Seccion("INFO_PIE_PAGINA", "Linea_1_Pie_1") + "\n");
                escribeEnTextBox(cajaTexto, "  " + ObjPr_ConfigApp.LeeLlave_Seccion("INFO_PIE_PAGINA", "Linea_2_Pie_1") + "\n");
                escribeEnTextBox(cajaTexto, "   " + ObjPr_ConfigApp.LeeLlave_Seccion("INFO_PIE_PAGINA", "Linea_1_Pie_2") + "\n");
                escribeEnTextBox(cajaTexto, "       " + ObjPr_ConfigApp.LeeLlave_Seccion("INFO_PIE_PAGINA", "Linea_2_Pie_2") + "\n");
                escribeEnTextBox(cajaTexto, "   " + ObjPr_ConfigApp.LeeLlave_Seccion("INFO_PIE_PAGINA", "Linea_1_Pie_3") + "\n");
                escribeEnTextBox(cajaTexto, "    " + ObjPr_ConfigApp.LeeLlave_Seccion("INFO_PIE_PAGINA", "Linea_2_Pie_3") + "\n");
                escribeEnTextBox(cajaTexto, "         " + ObjPr_ConfigApp.LeeLlave_Seccion("INFO_PIE_PAGINA", "Linea_1_Pie_4") + "\n");
                escribeEnTextBox(cajaTexto, "___________________________________________\n");
                escribeEnTextBox(cajaTexto, "\n     " + ObjPr_ConfigApp.LeeLlave_Seccion("INFO_PIE_PAGINA", "Linea_2_Pie_4") + "\n");
                escribeEnTextBox(cajaTexto, "___________________________________________\n");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "piePagina", "", ex.Message.ToString(), "", "");
            }
        }
        System.Drawing.Font printFont;
        System.IO.StreamReader fileToPrint;
        private void CMD_imprimirEstTramite_Click(object sender, EventArgs e)
        {
            try
            {
                CMD_imprimirEstTramite.Enabled = false;
                this.CMD_EstadoTramite.Enabled = false;
                this.rTxt_Tramites.SaveFile("Info.txt", RichTextBoxStreamType.PlainText);
                imprime2();
                //fileToPrint.Close();
                if (File.Exists("temp.txt")) File.Delete("temp.txt");
                if (File.Exists("Info.txt")) File.Delete("Info.txt");
                LimpiaPantallas();
                this.CMD_EstadoTramite.Enabled = true;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_imprimirEstTramite_Click", "", ex.Message.ToString(), "", "");
            }
            finally
            {
                //printDocument2.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage1);
            }
        }

        private String[] FormatFecha(String fecha)
        {
            String[] fecha_format = new String[3];
            try
            {
                fecha_format[0] = fecha.Substring(0, 4);
                fecha_format[1] = fecha.Substring(4, 2);
                fecha_format[2] = fecha.Substring(6, 2);
                int mes = Convert.ToInt16(fecha_format[1]);
                return fecha_format;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO.EXE", "Frm_Principal", "FormatFecha", "", "Error: " + ex.Message, "", "");
                return null;
            }
        }

        private void CMD_Certificado_Click(object sender, EventArgs e)
        {
            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Certificado_Click(), Iniciando ...");
            CMD_Certificado.Text = "CONSULTANDO...";
            CMD_Certificado.Enabled = false;
            CMD_EstadCuenta.Enabled = false;
            CMD_EstadoTramite.Enabled = false;
            cmd_imprimir_dela.Enabled = false;
            CMD_ImprimirCerti.Enabled = true;
            SqlDataReader rdr = null;
            String stL_nombreK = "";
            String stL_LFecha = DateTime.Now.ToString();
            String stL_nombre = armaNombre(stPr_A001PRIMER_APELLIDO, stPr_A001SEGUNDO_APELLIDO, stPr_A001PRIMER_NOMBRE, stPr_A001SEGUNDO_NOMBRE);
            rTxt_Certificacion.Clear();
            ClasX_Consultas objL_Consultas = new ClasX_Consultas(stPr_ExeName_Exe, ObjPr_EventLog, stPr_ParamBd);
            try
            {
                encabezado(rTxt_Certificacion);
                String temp = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
                stL_nombreK = temp;
                String[] infoConsulta = Obj_AppKiosko.obtieneToken(stPr_Serv, StPr_inPuerto);//posicion 0 = token, posicion 1 = usuario aDirector
                //
                stL_nombreK += "-" + infoConsulta[1] + "-" + Obj_AppKiosko.Get_My_Ip(stPr_Serv, StPr_inPuerto);
                temp = ObjPr_ConfigApp.LeeLlave_Seccion(temp, "NOMBRE");
                char[] delim = { ' ', '_' };
                String[] stmpt = temp.Split(delim);
                int cont = 0;
                for (int i = 0; i < stmpt.Length; i++)
                {
                    String tmp = stmpt[i].ToUpper();
                    if (tmp.Contains(st_Oficina1) || tmp.Contains(st_Oficina2) || tmp.Contains(st_Oficina3) || tmp.Contains(st_Oficina4) || tmp.Contains(st_Oficina5) || tmp.Contains(st_Oficina6) || tmp.Contains(st_Oficina7) || tmp.Contains(st_Oficina8) || tmp.Contains(st_Oficina9) || tmp.Contains(st_Oficina10) || tmp.Contains(st_Oficina11))
                    {
                        break;
                    }
                    cont++;
                }
                escribeEnTextBox(rTxt_Certificacion, stmpt[cont] + "    ");
                temp = DateTime.Now.ToString();
                stmpt = temp.Split(delim);
                escribeEnTextBox(rTxt_Certificacion, stmpt[0] + "   " + stmpt[1] + stmpt[2] + "\nNoCONSULTA: " + infoConsulta[0] + "\n");
                temp = armaNombre(stPr_A001PRIMER_APELLIDO, stPr_A001SEGUNDO_APELLIDO, stPr_A001PRIMER_NOMBRE, stPr_A001SEGUNDO_NOMBRE);
                temp = temp.ToUpper();
                escribeEnTextBox(rTxt_Certificacion, temp + "\n");
                rdr = objL_Consultas.EjecutaSP("SP_021_ACT_CONSUL_KIOSKO", txt_Identificacion.Text + ",'" + infoConsulta[0] + "','" + stL_nombreK + "','2'");
                rdr = objL_Consultas.EjecutaSP("SP_020_LEE_AFILIADO_INF_CAT", txt_Identificacion.Text);
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        String stL_linea = "";
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[9]))) stL_linea = "CATEGORIA: " + Convert.ToString(rdr[9]) + "\n";
                        escribeEnTextBox(rTxt_Certificacion, stL_linea);
                    }
                }
                escribeEnTextBox(rTxt_Certificacion, "C.C: " + txt_Identificacion.Text + "\n      CONSULTA DE TRAMITES\n\n");
                escribeEnTextBox(rTxt_Certificacion, "CERTIFICADO DE PAGO\n");
                escribeEnTextBox(rTxt_Certificacion, "FECHA PAGO      N°PAGO      VALOR\n");
                rdr = objL_Consultas.EjecutaSP("SP_013_LEE_AFILIADO_CERTI_PAGO_GA2", txt_Identificacion.Text);
                CMD_Certificado.Enabled = false;
                if (rdr != null)
                {
                    String stL_linea = "";
                    String[] FECHA;
                    String aux = "";
                    int counter = 0;
                    while (rdr.Read())
                    {
                        counter++;
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[4])))
                        {
                            stL_linea = Convert.ToString(rdr[4]);
                            FECHA = FormatFecha(stL_linea);
                            stL_linea = FECHA[2] + "/" + FECHA[1] + "/" + FECHA[0] + "  ";
                            //escribeEnTextBox(rTxt_Certificacion, stL_linea);
                        }
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[3])))
                        {
                            stL_linea += "    " + Convert.ToString(rdr[3]);
                        }
                        if (!DBNull.Value.Equals(Convert.ToString(rdr[2])))
                        {
                            aux = aMoneda(Convert.ToString(rdr[2]));
                            stL_linea = completaTexto(stL_linea, aux);
                            escribeEnTextBox(rTxt_Certificacion, stL_linea);
                        }
                        escribeEnTextBox(rTxt_Certificacion, "\n");
                    } while (rdr.NextResult()) ;
                    if (counter < 1) escribeEnTextBox(rTxt_Certificacion, "    AFILIADO NO PRESENTA PAGOS\n");
                    piePagina(rTxt_Certificacion);
                    CMD_Certificado.Text = stPr_CERTIFICADOPAGO ;
                    CMD_Certificado.Enabled = true;
                    CMD_EstadCuenta.Enabled = true;
                    CMD_EstadoTramite.Enabled = true;
                    CMD_RENTA.Enabled = true;
                    cambiaPantallas(PANEL_CERTIFICAO_PAGO);
                    this.CMD_Prioritario.Visible = false;
                    //this.CMD_Regresar.Visible = true;

                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Certificado_Click(), Terminando OK");
                }
                else
                {
                    lblMsjs.Text = "Error conexión a GA2";
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Certificado_Click(), Error en consula a GA2");
                    string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_21, "", "Aceptar", "Salir", false, 155);
                    MyMessageBox.ActiveForm.Activate();
                    MyMessageBox.ActiveForm.BringToFront();
                    ObjPr_EventLog.setTextErrLog(MENSAJE_APP_17 + txt_Identificacion.Text + MENSAJE_APP_18);
                    if (dialogResult.Equals("1"))
                    {
                        lblMsjs.Text = "Error de conexión a GA2";
                        CMD_EstadCuenta.Enabled = true;
                        CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                        CMD_Certificado.Enabled = true;
                        CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                        CMD_EstadoTramite.Enabled = true;
                        CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                        CMD_RENTA.Enabled = true;
                        CMD_RENTA.Text = stPr_DECLARACION;
                    }
                    if (dialogResult.Equals("2"))
                    {
                        VuelveInicio();
                    }

                }
                CMD_EstadCuenta.Enabled = true;
                CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                CMD_Certificado.Enabled = true;
                CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                CMD_EstadoTramite.Enabled = true;
                CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                CMD_RENTA.Enabled = true;
                CMD_RENTA.Text = stPr_DECLARACION;

            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Certificado_Click", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_ImprimirCerti_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ImprimirCerti_Click(), Iniciando ...");
                CMD_ImprimirCerti.Enabled = false;
                this.pnl_certPago.Enabled = false;
                this.rTxt_Certificacion.SaveFile("Info.txt", RichTextBoxStreamType.PlainText);
                imprime2();
                //fileToPrint.Close();
                if (File.Exists("temp.txt")) File.Delete("temp.txt");
                if (File.Exists("Info.txt")) File.Delete("Info.txt");
                LimpiaPantallas();
                this.pnl_certPago.Enabled = true;
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ImprimirCerti_Click(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_imprimirEstTramite_Click", "", ex.Message.ToString(), "", "");
            }
            finally
            {
                //printDocument2.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage1);
            }
        }

        private void CMD_Turnos_Click(object sender, EventArgs e)
        {
            try
            {
                //ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Turnos_Click(), Iniciando ...");
                this.CMD_Regresar.Visible = true;
                cambiaPantallas(PANEL_TURNOSINFOAFILIADO);
                this.CMD_Prioritario.Visible = false;
                this.CMD_Regresar.Visible = true;
                //ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Turnos_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Turnos_Click", "", ex.Message.ToString(), "", "");
            }
        }
        //CLICK BOTON TRAMITE EN LINEA.
        private void CMD_InfoGral_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Tramite_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_ITRAMITE_LINEA);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Tramite_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Tramite_Click", "", ex.Message.ToString(), "", "");
            }
        }
        //Modificando el metodo de impresion
        //Strail 23122014
        //
        private void imprime1()
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.imprime1(), iniciando ...");
                Application.DoEvents();
                //printFont = new System.Drawing.Font("Courier New", 8);
                //ObjPr_EventLog.setTextErrLog("Entrando a impresion");
                //printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                //Application.DoEvents();
                //Application.DoEvents();
                //ObjPr_EventLog.setTextErrLog("Imprimiendo ...");
                //printDocument1 = null;
                //printDocument1 = new System.Drawing.Printing.PrintDocument();
                //this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage_1);
                printDocument1.PrintController = new StandardPrintController();
                printDocument1.PrinterSettings.PrinterName = "RONGTA RP80 Printer";
                printDocument1.Print();
                //Application.DoEvents();
                //ObjPr_EventLog.setTextErrLog("Impresion terminada");
                printDocument1.Dispose();
                //printDocument1.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage);
                //ObjPr_EventLog.setTextErrLog("Hecho .Dispose() OK");
                Application.DoEvents();
                //printDocument1 = null;
                ObjPr_EventLog.setTextErrLog("frm_Principal.imprime1(), Terminado OK ");

            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "imprime1", "", ex.Message.ToString() + ex.StackTrace, "", "");
                //printDocument1 = null;
            }
            finally
            {
                //printDocument1.PrintPage -= new PrintPageEventHandler(printDocument1_PrintPage);
            }
        }

        private void imprime2()
        {
      
            
                try
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.imprime2(), iniciando ...");
                    //Strail 23122014 fileToPrint = new System.IO.StreamReader("Info.txt");
                    printFont = new System.Drawing.Font("Courier New", 8);
                    //printDocument2.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage1);
                    
                    //this.printDocument2 = new System.Drawing.Printing.PrintDocument();
                    //this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);

                    printDocument2.PrintController = new StandardPrintController();
                    printDocument2.PrinterSettings.PrinterName = "RONGTA RP80 Printer";
                    printDocument2.Print();
                    printDocument2.Dispose();
                    //printDocument1_PrintPage1
                    //printDocument2 = null;
                    //fileToPrint.Close();////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ObjPr_EventLog.setTextErrLog("frm_Principal.imprime2(), Termina OK");
                }
                
          
            catch (Exception ex)
            {
               
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "imprime2", "", ex.Message.ToString(), "", "");
           
            }
           

        }

        private void DibujaTurno()
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.DibujaTurno(), Inicia");
                this.pnl_Turno.Visible = true;
                //terminaAlgo(this.pnl_Turno,5000);
                Application.DoEvents();

                ObjPr_EventLog.setTextErrLog("Frm_Principal.DibujaTurno(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO", "Frm_Principal", "DibujaTurno", "", ex.Message, "", "");
            }
        }

        private void niegaTurno()
        {
            try
            {
                lbl_Turno.Visible = false;
                ObjPr_EventLog.setTextErrLog("frm_Principal.niegaTurno(), Inicia ");
                if (!txt_Identificacion.Text.Contains("Cédula..."))
                {
                    txt_Identificacion.Text = "Cédula...";
                    txt_Identificacion.ForeColor = Color.Black;
                }
                lbl_tipoServicio.Text = "SERVICIO DIGITURNO NO DISPONIBLE";
                lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                String cad1 = ObjPr_ConfigApp.LeeLlave_Seccion("APP", "msjnoturno1");
                String cad2 = ObjPr_ConfigApp.LeeLlave_Seccion("APP", "msjnoturno2");
                this.lbl_TipoTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lbl_TipoTurno.Location = new System.Drawing.Point(50, 86);
                this.lbl_TipoTurno.Text = ObjPr_ConfigApp.LeeLlave_Seccion("APP", "msjnoturno1") + "\n  " + ObjPr_ConfigApp.LeeLlave_Seccion("APP", "msjnoturno2");
                DibujaTurno();
                tmCierraNiegaTurno.Interval = 60000;
                tmCierraNiegaTurno.Enabled = true;
                lblMsjs.Text = "SERVICIO DIGITURNO NO DISPONIBLE";
                ObjPr_EventLog.setTextErrLog("frm_Principal.niegaTurno(), Termina OK ");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO", "Frm_Principal", "niegaTurno", "", ex.Message, "", "");
            }
        }




        /// <summary>
        /// Nuevo manejo de turnos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AsignaTurnos(String TipoTurno)
        {
            ClasX_Encripta ObjL_Encripta = null;
            ClasX_Config ObjL_ConfigApp = null;
            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), Inicia ...");
            MSJ("ASIGNANDO TURNO " + TipoTurno);
            string stl_MensajeErrorTurno1 = "TIPO_IDENTIFICACION_INVALIDO";
            string stl_MensajeErrorTurno2 = "SUCURSAL_INVALIDA";
            string stl_MensajeErrorTurno3 = "AUTENTICACION_INVALIDA";
            string stl_MensajeErrorTurno4 = "USUARIO_NO_ORIENTADOR";
            string stl_MensajeErrorTurno5 = "TIPO_TURNO_INVALIDO";
            string stl_MensajeErrorTurno6 = "CAMPO_VACIO_REQUERIDO";
            string stl_MensajeErrorTurno7 = "ERROR_GENERAL";

            //String stL_turno = "";
            string stL_FEchaHoraTurno = "";
            lbl_Turno.Visible = true;
            CMD_Prioritario.Visible = false;
            lbl_tipoServicio.Text = "";
            lbl_ceduAfi.Text = "";
            lbl_HoraTurno.Text = "";
            lbl_TipoTurno.Text = "";
            this.lbl_TipoTurno.Location = new System.Drawing.Point(153, 86);
            
            ObjL_ConfigApp = new ClasX_Config(stPr_Archivo_Confg,stPr_UsuarioApp, stPr_ArchivoLog);
            ObjL_Encripta = new ClasX_Encripta(stPr_UsuarioApp, stPr_ArchivoLog);

            //Descifra usuario de INFOTURNO
            string stl_usuarioInfoT = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Usuario_Infoturno");
            stl_usuarioInfoT = ObjL_Encripta.DesEncriptaInfo(stl_usuarioInfoT, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //Descifra password de usuario INFOTURNO
            string stl_pwInfoT = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "pw_Infoturno");
            stl_pwInfoT = ObjL_Encripta.DesEncriptaInfo(stl_pwInfoT, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            string stl_webInfoT = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Url_Web_Ser_Infoturno");
            string stl_Sucursal = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
            ////Cifra
            //string stl_encriptaUSER = ObjL_Encripta.EncriptaInfo("K_Can", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER1 = ObjL_Encripta.EncriptaInfo("K_Venecia", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER2 = ObjL_Encripta.EncriptaInfo("K_Cali", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER3 = ObjL_Encripta.EncriptaInfo("K_Medellin", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER4 = ObjL_Encripta.EncriptaInfo("K_Cartagena", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER5 = ObjL_Encripta.EncriptaInfo("K_Barranquilla", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER6 = ObjL_Encripta.EncriptaInfo("K_Bucaramanga", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER7 = ObjL_Encripta.EncriptaInfo("K_Ibague", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER8 = ObjL_Encripta.EncriptaInfo("K_Tolemaida", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER9 = ObjL_Encripta.EncriptaInfo("K_Espinal", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER10 = ObjL_Encripta.EncriptaInfo("K_Apiay", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER11 = ObjL_Encripta.EncriptaInfo("K_Covenas", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER12 = ObjL_Encripta.EncriptaInfo("K_Larandia", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER13 = ObjL_Encripta.EncriptaInfo("K_UnidadMovil1", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER14 = ObjL_Encripta.EncriptaInfo("K_UnidadMovil2", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
            //string stl_encriptaUSER15 = ObjL_Encripta.EncriptaInfo("K_Florencia", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");

            //string stl_desencripta = ObjL_Encripta.DesEncriptaInfo("mtp/Ru2rSYotDsMQdkPRGA==", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk="); 

            //Turno1 = IVIVIENDA CATORCE
            string stl_Turno1InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno1").ToUpper();
            //Turno2 = IVIVIENDA OCHO
            string stl_Turno2InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno2").ToUpper();
            //Turno3 = PRREFERENCIAL
            string stl_Turno3InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno3").ToUpper();
            //Turno4 = IVIVIENDA LEASING
            string stl_Turno4InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno4").ToUpper();
            //Turno5 = HEROES
            string stl_Turno5InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno5").ToUpper();
            //Turno6 = ITRAMITE EN LINEA
            string stl_Turno6InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno6").ToUpper();
            //Turno7 = BIOMETRIA
            string stl_Turno7InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno7").ToUpper();
            //Turno8 = IAGENDACITA
            string stl_Turno8InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno8").ToUpper();
            //Turno9 = IFCENSANTIAS
            string stl_Turno9InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno9").ToUpper();
            //Turno10 = IATENCION CITA P
            string stl_Turno10InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno10").ToUpper();
            //Turno11 = RADICACION VIVIENDA LEASING
            string stl_Turno11InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno11").ToUpper();
            //Turno12 = RADICACION VIVINEDA OCHO 
            string stl_Turno12InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno12").ToUpper();
            //Turno13 = RADICACION VIVIENDA CATORCE
            string stl_Turno13InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno13").ToUpper();
            //Turno14 = RADICACION FONDO-HEROES
            string stl_Turno14InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno14").ToUpper();
            //Turno15 = RADICACION FUTURO-CESANTIAS
            string stl_Turno15InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno15").ToUpper();
            //Turno16 = RADICACION PRETRAMITE
            string stl_Turno16InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno16").ToUpper();
            //Turno17 = RADICACION PRETRAMITE
            string stl_Turno17InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno17").ToUpper();
            //Turno18 = RADICACION APODERADO  VIVIENDA LEASING
            string stl_Turno18InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno18").ToUpper();
            //Turno19 = RADICACION APODERADO  VIVIENDA OCHO
            string stl_Turno19InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno19").ToUpper();
            //Turno20 = RADICACION APODERADO  VIVIENDA CATORCE
            string stl_Turno20InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno20").ToUpper();
            //Turno21 = RADICACION APODERADO  HEROES
            string stl_Turno21InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno21").ToUpper();
            //Turno22 = RADICACION APODERADO  FUTURO
            string stl_Turno22InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno22").ToUpper();
            //Turno23 = INFORMACION APODERADO  LEASING
            string stl_Turno23InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno23").ToUpper();
            //Turno24 = INFORMACION APODERADO  VIVIENDA OCHO
            string stl_Turno24InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno24").ToUpper();
            //Turno25 = INFORMACION APODERADO  VIVIENDA CATORCE
            string stl_Turno25InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno25").ToUpper();
            //Turno26 = INFORMACION APODERADO HEROES
            string stl_Turno26InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno26").ToUpper();
            //Turno27 = INFORMACION APODERADO HEROES
            string stl_Turno27InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno27").ToUpper();
            //Turno28 = RADICACION BENEFICIARIO VIVIENDA LEASING
            string stl_Turno28InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno28").ToUpper();
            //Turno29 = RADICACION BENEFICIARIO VIVIENDA OCHO
            string stl_Turno29InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno29").ToUpper();
            //Turno30 = RADICACION BENEFICIARIO VIVIENDA CATORCE
            string stl_Turno30InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno30").ToUpper();
            //Turno31 = RADICACION BENEFICIARIO HEROES
            string stl_Turno31InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno31").ToUpper();
            //Turno32 = RADICACION BENEFICIARIO FUTURO
            string stl_Turno32InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno32").ToUpper();
            //Turno33 = INFORMACION BENEFICIARIO LEASING
            string stl_Turno33InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno33").ToUpper();
            //Turno34 = INFORMACION BENEFICIARIO OCHO
            string stl_Turno34InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno34").ToUpper();
            //Turno35 = INFORMACION BENEFICIARIO CATORCE
            string stl_Turno35InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno35").ToUpper();
            //Turno36 = INFORMACION BENEFICIARIO HEROES 
            string stl_Turno36InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno36").ToUpper();
            //Turno37 = INFORMACION BENEFICIARIO FUTURO 
            string stl_Turno37InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno37").ToUpper();
            //Turno38 = INFORMACION AFILIADO CUENTA INDIVIDUAL 
            string stl_Turno38InfoT = ObjPr_ConfigApp.LeeLlave_Seccion("SERVICIOS_INFOTURNO", "Turno38").ToUpper();
            string url = stl_webInfoT;
            string stl_EstadoTurno = "";
            string stl_MensajeTurno = "";
            string stl_cedula = "Cédula de Ciudadanía";
            try
            {
                
                //PREFERENCIAL                    
                if (TipoTurno.Equals(stPrc_PREFERENCIAL))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_PREFERENCIAL");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno3InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0 )
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_PREFERENCIAL + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_PREFERENCIAL + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;
                         
                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_TipoTurno.Text.ToUpper();
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: "+ stPrc_PREFERENCIAL + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_PREFERENCIAL + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_PREFERENCIAL + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_PREFERENCIAL + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }



                //Vivienda catorce
                if (TipoTurno.Equals(stPrc_IVIVIENDA_CATORCE))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_VIVIENDA_CATORCE");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno1InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_IVIVIENDA_CATORCE + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_CATORCE + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                        
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_CATORCE + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_CATORCE + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }

                }
                //Vivienda ocho
                if (TipoTurno.Equals(stPrc_IVIVIENDA_OCHO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_VIVIENDA_OCHO");
                        Infoturno infoturno = new Infoturno();
                        infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                        infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        infoturno.tipoIdentificacionCliente = stl_cedula;
                        infoturno.sucursal = stl_Sucursal;
                        infoturno.tipoTurno = stl_Turno2InfoT;
                        infoturno.usuario = stl_usuarioInfoT;
                        infoturno.clave = stl_pwInfoT;
                        string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                        var responseP = HttpPost(url, DatosTurnoP);
                        var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                        stl_EstadoTurno = respObjectP.estado;
                        stl_MensajeTurno = respObjectP.mensaje;
                        if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_IVIVIENDA_OCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_OCHO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                        else
                        {
                            if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                            {
                                ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_OCHO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                            }
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_OCHO + " Negando Turno ...");
                            niegaTurno();

                            terminaAlgo(this.pnl_Turno, 10000);
                        }


               
                }

                //VIVIENDA LEASING
                if (TipoTurno.Equals(stPrc_IVIVIENDA_LEASING))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_VIVIENDA_LEASING");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno4InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_IVIVIENDA_LEASING + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_LEASING + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_LEASING + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_LEASING + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }



                }


                //HEROES
                if (TipoTurno.Equals(stPrc_IHEROES))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_HEROES");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno5InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_IHEROES + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IHEROES + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IHEROES + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IHEROES + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }



                }


                //PRETRAMITE
                if (TipoTurno.Equals(stPrc_ITRAMITE_LINEA))
                {

                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_ITRAMITE_LINEA");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno6InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ITRAMITE_LINEA + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ITRAMITE_LINEA + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ITRAMITE_LINEA + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ITRAMITE_LINEA + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }



                }




                //BIOMETRIA
                if (TipoTurno.Equals(stPrc_BIOMETRIA))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BIOMETRIA");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno7InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BIOMETRIA + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIOMETRIA + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIOMETRIA + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIOMETRIA + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }



                }




                //IAGENDA CITA 
                if (TipoTurno.Equals(stPrc_IAGENDACITA))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_IAGENDACITA");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno8InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_IAGENDACITA + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IAGENDACITA + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IAGENDACITA + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IAGENDACITA + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }


                //FUTURO
                if (TipoTurno.Equals(stPrc_IFUTUROCESANTIA))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_IFUTUROCESANTIA");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno9InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_IFUTUROCESANTIA + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IFUTUROCESANTIA + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IFUTUROCESANTIA + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IFUTUROCESANTIA + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }


                //ATENCION CITA PROGRAMADA 
                if (TipoTurno.Equals(stPrc_IAGENDACITAP))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_IAGENDACITAP");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno10InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_IAGENDACITAP + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IAGENDACITAP + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IAGENDACITAP + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IAGENDACITAP + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //RADICACION VIVIENDA LEASING
                if (TipoTurno.Equals(stPrc_RVIVIENDA_LEASING))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_RVIVIENDA_LEASING");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno11InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_RVIVIENDA_LEASING + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_LEASING + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_LEASING + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_LEASING + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }



                }
                if (TipoTurno.Equals(stPrc_RVIVIENDA_OCHO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_RVIVIENDA_OCHO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno12InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_RVIVIENDA_OCHO + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_OCHO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_OCHO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_OCHO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }


 


                }

                if (TipoTurno.Equals(stPrc_RVIVIENDA_CATORCE))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_RVIVIENDA_CATORCE");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno13InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_RVIVIENDA_CATORCE + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_CATORCE + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_CATORCE + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RVIVIENDA_CATORCE + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }





                }

                if (TipoTurno.Equals(stPrc_RHEROES))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_RHEROES");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno14InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_RHEROES + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RHEROES + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RHEROES + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RHEROES + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }





                }

                if (TipoTurno.Equals(stPrc_RFUTURO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_RFUTURO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno15InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_RFUTURO + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RFUTURO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RFUTURO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RFUTURO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }





                }

                if (TipoTurno.Equals(stPrc_RPRETRAMITE))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_RPRETRAMITE");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno16InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_RPRETRAMITE + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RPRETRAMITE + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RPRETRAMITE + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RPRETRAMITE + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }





                }

                if (TipoTurno.Equals(stPrc_RTRAMITE_LINEA))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_RTRAMITE_LINEA");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno17InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Visible = false;
                        lbl_NombreNoAfi.Visible = false;
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_RTRAMITE_LINEA + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RTRAMITE_LINEA + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                        this.pnl_Turno.Visible = false;
                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RTRAMITE_LINEA + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_RTRAMITE_LINEA + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }





                }


                //TURNOS APODERADOS RADICACION 
                if (TipoTurno.Equals(stPrc_ARVIVIENDA_LEASING))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_ARVIVIENDA_LEASING");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno18InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARVIVIENDA_LEASING + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_LEASING + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARVIVIENDA_LEASING + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_LEASING + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_LEASING + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_LEASING + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }
                // TURNO 19 RADICACION APODERADOS VIVIENDA 8
                if (TipoTurno.Equals(stPrc_ARVIVIENDA_OCHO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_ARVIVIENDA_OCHO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno19InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARVIVIENDA_OCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_OCHO + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARVIVIENDA_OCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_OCHO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_OCHO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_OCHO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                // TURNO 20 RADICACION APODERADOS VIVIENDA 14
                if (TipoTurno.Equals(stPrc_ARVIVIENDA_CATORCE))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_ARVIVIENDA_CATORCE");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno20InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARVIVIENDA_CATORCE + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_CATORCE + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARVIVIENDA_CATORCE + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_CATORCE + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_CATORCE + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARVIVIENDA_CATORCE + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }
                //TURNO 21 RADICACION APODERADO HEROES
                if (TipoTurno.Equals(stPrc_ARHEROES))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_ARHEROES");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno21InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARHEROES + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARHEROES + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARHEROES + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARHEROES + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARHEROES + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARHEROES + " Negando Turno ...");
                        niegaTurno();
                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                if (TipoTurno.Equals(stPrc_ARFUTURO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_ARFUTURO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno22InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARFUTURO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARFUTURO + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ARFUTURO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARFUTURO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARFUTURO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ARFUTURO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }
                //TURNO APODERADO INFORMACION VIVIENDA LEASING
                if (TipoTurno.Equals(stPrc_AILEASING))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_AILEASING");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno23InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AILEASING + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AILEASING + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AILEASING + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AILEASING + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AILEASING + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AILEASING + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }
                //TURNO APODERADO INFORMACION VIVIENDA OCHO 
                if (TipoTurno.Equals(stPrc_AIOCHO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_AIOCHO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno24InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AIOCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIOCHO + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AIOCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIOCHO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIOCHO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIOCHO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO APODERADO INFORMACION VIVIENDA CATORCE
                if (TipoTurno.Equals(stPrc_AICATORCE))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_AICATORCE");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno25InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AICATORCE + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AICATORCE + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AICATORCE + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AICATORCE + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AICATORCE + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AICATORCE + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO APODERADO INFORMACION HEROES
                if (TipoTurno.Equals(stPrc_AIHEROES))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_AIHEROES");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno26InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AIHEROES + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIHEROES + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AIHEROES + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIHEROES + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIHEROES + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIHEROES + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO APODERADO INFORMACION FUTURO 
                if (TipoTurno.Equals(stPrc_AIFUTURO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_AIFUTURO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno27InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AIFUTURO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIFUTURO + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_AIFUTURO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIFUTURO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIFUTURO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_AIFUTURO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO BENEFICIARIO RADICACION VIVIENDA LEASING 
                if (TipoTurno.Equals(stPrc_BRLEASING))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BRLEASING");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno28InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BRLEASING + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRLEASING + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BRLEASING + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRLEASING + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRLEASING + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRLEASING + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO BENEFICIARIO RADICACION VIVIENDA OCHO 
                if (TipoTurno.Equals(stPrc_BROCHO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BROCHO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno29InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BROCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BROCHO + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BROCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BROCHO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BROCHO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BROCHO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO BENEFICIARIO RADICACION VIVIENDA CATORCE
                if (TipoTurno.Equals(stPrc_BRCATORCE))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BRCATORCE");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno30InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BRCATORCE + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRCATORCE + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BRCATORCE + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRCATORCE + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRCATORCE + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRCATORCE + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO BENEFICIARIO RADICACION HEROES
                if (TipoTurno.Equals(stPrc_BRHEROES))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BRHEROES");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno31InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BRHEROES + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRHEROES + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BRHEROES + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRHEROES + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRHEROES + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRHEROES + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO BENEFICIARIO RADICACION FUTURO
                if (TipoTurno.Equals(stPrc_BRFUTURO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BRFUTURO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno32InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BRFUTURO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRFUTURO + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BRFUTURO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRFUTURO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRFUTURO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BRFUTURO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }
                //TURNO BENEFICIARIO INFORMACION LEASING 
                if (TipoTurno.Equals(stPrc_BILEASING))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BILEASING");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno33InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BILEASING + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BILEASING + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BILEASING + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BILEASING + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BILEASING + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BILEASING + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO BENEFICIARIO INFORMACION OCHO
                if (TipoTurno.Equals(stPrc_BIOCHO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BIOCHO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno34InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BIOCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIOCHO + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BIOCHO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIOCHO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIOCHO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIOCHO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO BENEFICIARIO INFORMACION CATORCE
                if (TipoTurno.Equals(stPrc_BICATORCE))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BICATORCE");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno35InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BICATORCE + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BICATORCE + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BICATORCE + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BICATORCE + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BICATORCE + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BICATORCE + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                //TURNO BENEFICIARIO INFORMACION HEROES
                if (TipoTurno.Equals(stPrc_BIHEROES))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BIHEROES");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno36InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BIHEROES + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIHEROES + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BIHEROES + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIHEROES + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIHEROES + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIHEROES + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                if (TipoTurno.Equals(stPrc_BIFUTURO))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_BIFUTURO");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno37InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BIFUTURO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIFUTURO + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_BIFUTURO + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIFUTURO + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIFUTURO + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_BIFUTURO + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }

                if (TipoTurno.Equals(stPrc_ICUENTA))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_ICUENTA");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno38InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        if (inPr_DocNoAfiiado != 0)
                        {
                            lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);

                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ICUENTA + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ICUENTA + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                            this.pnl_Turno.Visible = false;

                        }
                        else
                        {
                            lbl_ceduNAfi.Visible = false;
                            lbl_NombreNoAfi.Visible = false;
                            stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                            lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                            this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                            lbl_TipoTurno.Text.ToUpper();
                            lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                            lbl_tipoServicio.Text = infoturno.tipoTurno;
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + responseP);
                            DibujaTurno();
                            this.pnl_Turno.Visible = true;
                            capturaPantalla();
                            imprime1();
                            //Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO " +stPrc_PREFERENCIAL);
                            Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_ICUENTA + ".");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ICUENTA + "Imprimiendo : " + inPr_A001NUM_IDENTIFICACION);
                            this.pnl_Turno.Visible = false;
                        }

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ICUENTA + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_ICUENTA + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }
                }
                //TURNO NO AFILIADO VIENDA 14 INFORMACION GENERAL 

                if (TipoTurno.Equals(stPrc_VIVIENDA_CATORCENA))
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), stPrc_VIVIENDA_CATORCENA");
                    Infoturno infoturno = new Infoturno();
                    infoturno.documentoCliente = inPr_A001NUM_IDENTIFICACION;
                    infoturno.nombreCliente = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                    infoturno.tipoIdentificacionCliente = stl_cedula;
                    infoturno.sucursal = stl_Sucursal;
                    infoturno.tipoTurno = stl_Turno1InfoT;
                    infoturno.usuario = stl_usuarioInfoT;
                    infoturno.clave = stl_pwInfoT;
                    string DatosTurnoP = JsonConvert.SerializeObject(infoturno);
                    var responseP = HttpPost(url, DatosTurnoP);
                    var respObjectP = JsonConvert.DeserializeObject<ServiceResponse>(responseP);
                    stl_EstadoTurno = respObjectP.estado;
                    stl_MensajeTurno = respObjectP.mensaje;
                    if (respObjectP.estado.Contains("TURNO_GENERADO_EXITOSAMENTE"))
                    {
                        lbl_ceduNAfi.Text = Convert.ToString(inPr_DocNoAfiiado);
                      
                        stL_FEchaHoraTurno = Convierte_DateTime(respObjectP.datos.fechaGeneracion).ToString();
                        lbl_ceduAfi.Text = stPr_A001PRIMER_NOMBRE + " " + stPr_A001PRIMER_APELLIDO;
                        this.lbl_TipoTurno.Text = respObjectP.datos.secuencia;
                        lbl_HoraTurno.Text = stL_FEchaHoraTurno;
                        lbl_tipoServicio.Text = infoturno.tipoTurno;
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),  TURNO : " + respObjectP.datos.secuencia);
                        DibujaTurno();
                        this.pnl_Turno.Visible = true;
                        capturaPantalla();
                        imprime1();
                        Obj_AppKiosko.Write_Command_Srv(stPr_Serv, StPr_inPuerto, "GENERA TURNO: " + stPrc_VIVIENDA_CATORCENA + ".");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_IVIVIENDA_CATORCE + "Imprimiendo Afiliado  : " + inPr_A001NUM_IDENTIFICACION + "Imprimiendo no Afiliado : " + Convert.ToString(inPr_DocNoAfiiado));
                        this.pnl_Turno.Visible = false;

                    }

                    else
                    {
                        if (stl_EstadoTurno.Contains(stl_MensajeErrorTurno1) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno2) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno3) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno4) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno5) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno6) || stl_EstadoTurno.Contains(stl_MensajeErrorTurno7))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_VIVIENDA_CATORCENA + stl_EstadoTurno + ": " + stl_MensajeTurno + ".");
                        }
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), " + stPrc_VIVIENDA_CATORCENA + " Negando Turno ...");
                        niegaTurno();

                        terminaAlgo(this.pnl_Turno, 10000);
                    }

                }








                LimpiaPantallas();
                ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(), Termina OK");
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        MessageBox.Show(ex.Message);
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),(HTTP Status Code: " + (int)response.StatusCode);
                        MyMessageBox.ShowBox("No es posible establecer conexión con el servicio de turnos.", "Error de Conexión", "Aceptar", "Cancelar", false, 155);
                        MyMessageBox.ActiveForm.Activate();
                        MyMessageBox.ActiveForm.BringToFront();
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),No es posible establecer conexión con el servicio de turnos. '" + ex.Message + "'");
                    }
                    else
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),No es posible establecer conexión con el servicio de turnos. '" + ex.Message + "'");
                    }
                }
                else
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.AsignaTurnos(),No es posible establecer conexión con el servicio de turnos. '" + ex.Message + "'");
                }
                niegaTurno();
                terminaAlgo(this.pnl_Turno, 10000);
            }
            catch (Exception ex)
            {
                LimpiaPantallas();
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "AsignaTurnos", "", ex.Message.ToString(), "", "");
            }
        }
        















        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.printDocument1_PrintPage(), Inicia");
                e.Graphics.DrawImage(imagen, 0, 0);
                ObjPr_EventLog.setTextErrLog("Frm_Principal.printDocument1_PrintPage(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO", "Frm_Principal", "printDocument1_PrintPage", "", ex.Message, "", "");
            }
        }

        private void printDocument1_PrintPage1(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.printDocument1_PrintPage1(), Inicia");
                //Strail 23122014
                fileToPrint = new System.IO.StreamReader("Info.txt");
                float yPos = 0f;
                int count = 0;
                float leftMargin = 20.0F;
                //float leftMargin = e.MarginBounds.Left;
                float topMargin = e.MarginBounds.Top;
                string line = null;
                float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
                while (count < linesPerPage)
                {
                    line = fileToPrint.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    yPos = 2 + count * printFont.GetHeight(e.Graphics);
                    e.Graphics.DrawString(line, printFont, Brushes.Black, 2, yPos, new StringFormat());
                    count++;
                }
                if (line != null)
                {
                    e.HasMorePages = true;
                }
                fileToPrint.Close();
                ObjPr_EventLog.setTextErrLog("Frm_Principal.printDocument1_PrintPage1(), Rermina OK");
            }
            catch (Exception ex)
            {
                fileToPrint.Close();
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO", "Frm_Principal", "printDocument1_PrintPage1", "", ex.Message, "", "");
            }
        }
        Bitmap imagen;
        private void capturaPantalla()
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.capturaPantalla(), Inicia");
                Graphics g = pnl_Turno.CreateGraphics();
                Size s = pnl_Turno.Size;
                imagen = new Bitmap(s.Width, s.Height, g);
                Graphics g2 = Graphics.FromImage(imagen);
                int x = pnl_Turno.Location.X;
                int y = pnl_Turno.Location.Y;
                g2.CopyFromScreen(x, y, 0, 0, s);
                Application.DoEvents();
                ObjPr_EventLog.setTextErrLog("Frm_Principal.capturaPantalla(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO", "Frm_Principal", "capturaPantalla", "", ex.Message, "", "");
            }
        }



       

        private void CMD_Acreditacion_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Fondo_Click(), Inicia");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_FONDO);
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Fondo_Click(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO", "Frm_Principal", "CMD_Fondo_Click", "", ex.Message, "", "");
            }
        }

        private void CMD_Regresar_Click(object sender, EventArgs e)
        {
            try
            {
                ///ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Regresar_Click(), Inicia");
                if(pnl_teclado.Visible)
                {
                    pnl_Inicio.Visible = true;
                    pnl_teclado.Visible = false;
                    CMD_Regresar.Visible = false;
                    cmd_Accept.Enabled = false;
                    txt_Identificacion.Text="Cédula....";
                }
                else
                {
                    if (inPr_A001NUM_IDENTIFICACION == 0 || inPr_A001NUM_IDENTIFICACION == 1)
                    {
                        cambiaPantallas(PANEL_INICIO);
                    }
                    else
                    {
                        if (pnl_TurnosInfoAfiliado.Visible)
                        {
                            pnl_TurnosInfoAfiliado.Visible = false;

                            pnl_Inicio.Visible = true;
                            pnl_Inicio.BringToFront();
                        }
                        else
                        {
                            pnl_TurnosInfoAfiliado.Visible = true;
                            CMD_Prioritario.Visible = true;
                            pnl_Inicio.BringToFront();
                            webBrowser1.Navigate("about:black");
                            webBrowser3.Navigate("about:black");
                            pnl_decla.Visible = false;
                            pnl_EstadoCuenta.Visible = false;
                            pnl_ESTADO_TRAMITE.Visible = false;
                            pnl_certPago.Visible = false;
                            CMD_Regresar.Visible = false;
                        }

                        if (stPr_modo.Equals("lite"))
                        {
                            CMD_Prioritario.Visible = false;
                        }
                    }
                }
                ///ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Regresar_Click(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO", "Frm_Principal", "CMD_Regresar_Click", "", ex.Message, "", "");
            }
        }

        private void ValidaCOnexionesFenixyGA2()
        {
            try
            {
                MSJ("Validando Conexion aDIRECTOR");
                bool testconexion = Obj_AppKiosko.Valida_Conexion_ADirector(stPr_Serv, StPr_inPuerto);
                //
                if (testconexion)//eRROR DE CONEXION
                {
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(),  aDIRECTOR OK ...");
                    lblMsjs.Text = "Validando Conexion aDIRECTOR OK";
                    //
                    MSJ("Consultando Cedula GA2");
                    if (consultaCedulaEnGA2())
                    {
                        MSJ("Cedula GA2 OK");
                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(), Se obtuvieron resultados de SP: SP_010_LEE_AFILIADO_GA2 para cc: " + txt_Identificacion.Text);
                        //Habilitar para asignar turno Sin verificar//
                        //AsignaTurnos(stPrc_PREFERENCIAL);
                        //
                        //////////////////////
                        //////DESACTIVAR COMENTARIOS PARA ASIGNAR TURNO CON VERIFICACIÓN
                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(),  Iniciando THREAD para : " + txt_Identificacion.Text);
                        MSJ("Iniciando THREAD");
                        //traerbio = new Thread(TraeBios);//Trae las biometrias relacionadas con el numero de cedula
                        //traerbio.Start();
                        ////debug TraeBios();
                        hayTemplateEnDispositivo = -9;
                        MSJ("ACTIVANDO ESCANNER");
                        estadoEscaneo = 0;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                        bool escannerActivo = activarEscaner();
                        Thread.Sleep(600);
                        apagaTodosLosPanels();
                        //esperarNTiempo(3000);

                        if (!escannerActivo)
                        {
                            estadoEscaneo = -1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                            MSJ("PROBLEMA EN EL SCANNER VuelveInicio");
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(),  ERROR Con el Scanner : VuelveInicio");
                            VuelveInicio();
                        }
                        else
                        {
                            MSJ("OK, Presentando pantalla de huella");
                            pnl_vistaHuella.Visible = true;
                            this.ptb_HuellaEstatica.Visible = true;
                            cambiaPantallas(PANEL_CAPTURA_HUELLA);
                            MSJ("SCANNER OK, INICIA TOMA DE HUELLA ...");
                            if (objPr_huellero_Iden_Veri.getErrorenHuellero())
                            {
                                MSJ("Error en el huellero, Licencia caida");
                                ObjPr_EventLog.setTextErrLog("frm_Principal.ActivaEscaner(), ERROR licencias caidas ");
                            }
                            estadoEscaneo = 1;//0 Inactivo, -9 Ejecutando, 1 Ejecutar, -1 Detener, -2 Error
                        }
                        //////////////////////
                    }
                    else
                    {
                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(), Turno Preferencial para : " + txt_Identificacion.Text);
                        MSJ("No existe en GA2, Genera turno Preferencial");
                        pnl_teclado.Visible = false;
                        stPr_A001PRIMER_APELLIDO = txt_Identificacion.Text; //Andrés 23062016, para cuando la persona no existe en GA2, ponemos la cedula
                        stPr_A001PRIMER_NOMBRE = "";
                        stPr_A001SEGUNDO_APELLIDO = "";
                        stPr_A001SEGUNDO_NOMBRE = "";
                        //Andrés 23062016
                        lbl_Alertas.Text = "* NO REGISTRADO EN GA2";
                        ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(), Alerta : " + lbl_Alertas.Text);
                        ////////////////////////////////////////////////
                        // ASQC Feb 25 2.014
                        ////////////////////////////////////////////////
                        // Si esta en modo LITE, no debe generar turno y presenta 
                        // mensaje que el turno no se puede generar en estos momentos.
                        if ((stPr_modo.Equals("lite")))
                        {
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(), Modo LITE : ");
                            this.niegaTurno();
                            terminaAlgo(this.pnl_Turno, 7000);
                            //this.pnl_Turno.Visible = false;
                            this.CMD_Prioritario.Visible = false;
                            //LimpiaPantallas();
                        }
                        else
                        {
                            blPr_NoGA2 = true;
                            // Genera el turno de general.
                            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(), TURNO PREFERENCIAL : " + lbl_Alertas.Text);
                            InfoGeneralP("No GA2_TP");
                            VuelveInicio();
                        }
                        ////////////////////////////////////////////////
                        // Fin ASQC Feb 25 2.014
                        ////////////////////////////////////////////////
                    }
                }//Cierra if de no conexion FENIX

                else //sI NO HAY CONEXION CON FENIX
                {
                    //Strail 18112014 MessageBox.Show(MENSAJE_APP_04 + ClasX_Constans.NEW_LINE + MENSAJE_APP_03 + ClasX_Constans.NEW_LINE + ClasX_Constans.NEW_LINE + ClasX_Constans.MENSAJE_22, ClasX_Constans.MENSAJE_5);
                    //ObjPr_EventLog.setTextErrLog("ERROR accediendo a servidores Fenix.");
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(), ERROR De Red, No Hay conexion con el aDIRECTOR");
                    //Strail 18112014
                    lbl_Alertas.Text = "ERROR DE CONEXION A FENIX";
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(), TURNO PREFERENCIAL : " + lbl_Alertas.Text);
                    InfoGeneral("");
                    LimpiaPantallas();
                }//Cierra. Else No hay conexion FENIX
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "ValidaConexionesFenixyGA2", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_Prioritario_Click(object sender, EventArgs e)
        {
            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Prioritario_Click(), Inicia ...");
            blPr_desactivaturnosP = true;
            if (pnl_TurnosNAfiliados.Visible == true)
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Prioritario_Click() Panel no Afiliados, Inicia");
                this.pnl_TurnosNAfiliados.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_PREFERENCIAL);
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Prioritario_Click(), Panel no Afiliados Termina OK");
            }
            if (pnlTurnosBeneficiarios.Visible == true)
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Prioritario_Click() Panel no Afiliados, Inicia");
                this.pnlTurnosBeneficiarios.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_PREFERENCIAL);
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Prioritario_Click(), Panel no Afiliados Termina OK");
            }
         if(pnl_TurnosInfoAfiliado.Visible==true)
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Prioritario_Click(), Panel Afiliados Inicia");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_PREFERENCIAL);
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Prioritario_Click(), Panel Afiliados Termina OK");
            }
         
        }

        public void TraeBios()
        {
            try
            {
                String stL_NoCedula = txt_Identificacion.Text;
                //
                Obj_AppKiosko.TraeBios(stPr_PathApp, stL_NoCedula, inPr_A001NUM_IDENTIFICACION, stPr_Serv, StPr_inPuerto, stPr_RutaBase, ref terminedetraer, ref blPr_HayBio, ref stPr_BioFalta);
                //
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, false, true, false);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "TraeBios", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "TraeBios", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }
        //private void CargaSplash()
        //{
        //    using (SplashScreen fwait = new SplashScreen())
        //        try
        //        {
        //            fwait.ConectaFenix = "Fenix";
        //            fwait.ConectaBDFenix = "BDFenix";
        //            fwait.ConectaGA2 = "GA2";
        //            fwait.ConectaDigi = "Digiturno";
        //            fwait.ActionToExecute = Inicializar;
        //            if (fwait.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //            {
        //                MessageBox.Show("OK");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Cancel or error");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CargaSplash", "", ex.Message.ToString(), "", "");
        //        }
        //}

        protected virtual void OnFormClosing1(FormClosingEventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("----TERMINANDO OPERACION NORMAL DE KIOSKO----");
            }
            //catch (System.AccessViolationException ex_0)
            //{
            //    ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "OnFormClosing.System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
            //}
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "OnFormClosing1", "", ex.Message.ToString(), "", "");
            }
        }

        private void pnl_Turno_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            printDocument1_PrintPage1(sender, e);
        }

        private void pnl_EstadoCuenta_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ObjPr_timer_Tick_1(object sender, EventArgs e)
        {

            if (autorizaEjecutar)
            {
                StopTimer();
                StopTiemrHuella();
                ObjPr_timer_Tick(sender, e);
            }
        }

        private void ObjPr_timerHuella_Tick_1(object sender, EventArgs e)
        {
            try
            {
                ptb_HuellaEstatica.Visible = true;
                ptb_HuelaPrelim.Visible = false;
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "ObjPr_timerHuella_Tick_1", "", ex.Message, "", "");
            }
            //ObjPr_timerHUella_Tick(sender, e);
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            printDocument1_PrintPage(sender, e);
        }

        private void tmCierraNiegaTurno_Tick(object sender, EventArgs e)
        {
            this.pnl_Turno.Visible = false;
            tmCierraNiegaTurno.Enabled = false;
        }

        void terminaAlgo(System.Windows.Forms.Panel pnl, int tiempo)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("FrmPrincial.terminaAlgo(), Inicia");
                panelGeneral = pnl;
                tmGeneral.Interval = tiempo;
                tmGeneral.Enabled = true;
                ObjPr_EventLog.setTextErrLog("FrmPrincial.terminaAlgo(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "terminaAlgo", "", ex.Message.ToString(), "", "");
            }
        }
        private System.Windows.Forms.Panel panelGeneral = null;

        private void tmGeneral_Tick(object sender, EventArgs e)
        {
            try
            {
                panelGeneral.Visible = false;
                tmGeneral.Enabled = false;
                LimpiaPantallas();
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "tmGeneral_Tick", "", ex.Message.ToString(), "", "");
            }
        }

        private int cuentaparasalir = 0;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "tmGeneral_Tick", "", ex.Message.ToString(), "", "");
            }
        }

        public class Infoturno
        {
            public ulong documentoCliente { get; set; }
            public string nombreCliente { get; set; }
            public string tipoIdentificacionCliente { get; set; }
            public string sucursal { get; set; }
            public string tipoTurno { get; set; }
            public string usuario { get; set; }
            public string clave { get; set; }
        }


        static string HttpPost(string url, string data)
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/json";
            byte[] formData = UTF8Encoding.UTF8.GetBytes(data);
            req.ContentLength = formData.Length;
            using (Stream post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }
            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// Método de conversión timestamp de UNIX (turnos)
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime Convierte_DateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void CMD_Vivienda14_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Vivienda14_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_IVIVIENDA_CATORCE);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Vivienda14_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Vivienda14_Click", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_Vivienda8_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Vivienda8_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_IVIVIENDA_OCHO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Vivienda8_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Vivienda8_Click", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_Leasing_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Leasing_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_IVIVIENDA_LEASING);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Leasing_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Leasing_Click", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_Heroes_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Heroes_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_IHEROES);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Heroes_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Heroes_Click", "", ex.Message.ToString(), "", "");
            }
        }
        //Agendamiento cita informacion general 15/06/2018 RN
        private void CMD_Pretramite_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Agenda_Cita_Info_General(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;
               
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_IAGENDACITA);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Agenda_Cita_Info_General(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Agenda_Cita_Info_General()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_Futuro_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Futuro_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_IFUTUROCESANTIA);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Futuro_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Futuro_Click", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_InfoLeasing_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_Cuenta_Click(), Inicia");
                this.pnl_TurnosInfoAfiliado.Visible = false;
                
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_ICUENTA);
                ObjPr_EventLog.setTextErrLog("Frm_Principal.CMD_CMD_Cuenta_Click(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError("FENIX_KIOSCO", "Frm_Principal", "CMD_Cuenta_Click", "", ex.Message, "", "");
            }
        }























        private void deviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void OnSelectedDeviceChanged()
        {
            NDevice device = GetSelectedDevice();
            NCaptureDevice captureDevice = device as NCaptureDevice;
            NCamera camera = device as NCamera;
            NMicrophone microphone = device as NMicrophone;
            NBiometricDevice biometricDevice = device as NBiometricDevice;
            NFScanner fScanner = device as NFScanner;
            NIrisScanner irisScanner = device as NIrisScanner;
            bool isCaptureDevice = camera != null || microphone != null || fScanner != null || irisScanner != null;

            //disconnectToolStripMenuItem.Enabled = device != null && device.IsDisconnectable;
            //showPluginToolStripMenuItem.Enabled = device != null;
            typeLabel.Text = device == null ? null : string.Format("Type: {0}", device.GetType());
            devicePropertyGrid.SelectedObject = device;
            biometricDeviceImpressionTypeComboBox.Visible = fScanner != null;
            biometricDevicePositionComboBox.Visible = fScanner != null || irisScanner != null;
            //llCheckBox.Visible = lrCheckBox.Visible = lmCheckBox.Visible = liCheckBox.Visible = ltCheckBox.Visible
            //= rtCheckBox.Visible = riCheckBox.Visible = rmCheckBox.Visible = rrCheckBox.Visible = rlCheckBox.Visible = fScanner != null;
            //tbMiliseconds.Visible = cbUseTimeout.Visible = lblMiliseconds.Visible =
            cbAutomatic.Visible = isCaptureDevice && biometricDevice != null;
            //cbGatherImages.Visible = deviceCaptureButton.Visible = isCaptureDevice;
            //formatsComboBox.Visible = customizeFormatButton.Visible = false;
            //startSequenceButton.Visible = endSequenceButton.Visible = biometricDevice != null;

            if (fScanner != null)
            {
                biometricDeviceImpressionTypeComboBox.BeginUpdate();
                biometricDevicePositionComboBox.BeginUpdate();
                biometricDeviceImpressionTypeComboBox.Items.Clear();
                biometricDevicePositionComboBox.Items.Clear();
                try
                {
                    if (fScanner.IsAvailable)
                    {
                        foreach (NFImpressionType impressionType in fScanner.GetSupportedImpressionTypes())
                        {
                            biometricDeviceImpressionTypeComboBox.Items.Add(impressionType);
                        }
                        if (biometricDeviceImpressionTypeComboBox.Items.Count != 0) biometricDeviceImpressionTypeComboBox.SelectedIndex = 0;
                        foreach (NFPosition position in fScanner.GetSupportedPositions())
                        {
                            biometricDevicePositionComboBox.Items.Add(position);
                        }
                        if (biometricDevicePositionComboBox.Items.Count != 0) biometricDevicePositionComboBox.SelectedIndex = 0;
                    }
                }
				finally // because it may become unavailable in process
                {
                    biometricDeviceImpressionTypeComboBox.EndUpdate();
                    biometricDevicePositionComboBox.EndUpdate();
                }
            }
            else if (irisScanner != null)
            {
                biometricDevicePositionComboBox.BeginUpdate();
                biometricDevicePositionComboBox.Items.Clear();
                try
                {
                    if (irisScanner.IsAvailable)
                    {
                        foreach (NEPosition position in irisScanner.GetSupportedPositions())
                        {
                            biometricDevicePositionComboBox.Items.Add(position);
                        }
                        if (biometricDevicePositionComboBox.Items.Count != 0) biometricDevicePositionComboBox.SelectedIndex = 0;
                    }
                }
				finally // because it may become unavailable in process
                {
                    biometricDevicePositionComboBox.EndUpdate();
                }
            }

            if (captureDevice != null)
            {
                formatsComboBox.BeginUpdate();
                try
                {
                    formatsComboBox.Items.Clear();
                    foreach (NMediaFormat format in captureDevice.GetFormats())
                    {
                        formatsComboBox.Items.Add(format);
                    }
                    NMediaFormat currentFormat = captureDevice.GetCurrentFormat();
                    if (currentFormat != null)
                    {
                        int formatIndex = formatsComboBox.Items.IndexOf(currentFormat);
                        if (formatIndex == -1)
                        {
                            formatsComboBox.Items.Add(currentFormat);
                            formatsComboBox.SelectedIndex = formatsComboBox.Items.Count - 1;
                        }
                        else
                        {
                            formatsComboBox.SelectedIndex = formatIndex;
                        }
                    }
                }
                finally
                {
                    formatsComboBox.EndUpdate();
                }
                //formatsComboBox.Visible = customizeFormatButton.Visible = true;
                formatsComboBox.Visible = true;
            }
        }

        private NDevice GetSelectedDevice()
        {
            return deviceTreeView.SelectedNode == null ? null : (NDevice)deviceTreeView.SelectedNode.Tag;
        }

        private void SetSelectedDevice(NDevice device)
        {
            if (device == null)
            {
                deviceTreeView.SelectedNode = null;
            }
            else
            {
                foreach (TreeNode node in deviceTreeView.Nodes)
                {
                    if (device.Equals(node.Tag))
                    {
                        deviceTreeView.SelectedNode = node;
                        break;
                    }
                }
            }
        }

        private void SetDeviceManager(NDeviceManager value)
        {
            if (value == _deviceManager) return;
            if (_deviceManager != null)
            {
                _deviceManager.DevicesChanging -= new EventHandler<EventArgs>(deviceManager_DevicesChanging);
                _deviceManager.DevicesChanged -= new EventHandler<EventArgs>(deviceManager_DevicesChanged);
                _deviceManager.DeviceAdded -= new EventHandler<NDeviceManagerDeviceEventArgs>(deviceManager_DeviceAdded);
                _deviceManager.DeviceRemoved -= new EventHandler<NDeviceManagerDeviceEventArgs>(deviceManager_DeviceRemoved);
                _deviceManager.DevicesRefreshed -= new EventHandler<EventArgs>(deviceManager_DevicesRefreshed);
                _deviceManager.Dispose();
                _deviceManager = null;
            }
            _deviceManager = value;
            OnDeviceManagerChanged();
        }

        private void CloseDeviceManager()
        {
            SetDeviceManager(null);
        }

        void deviceManager_DevicesChanging(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                deviceTreeView.BeginUpdate();
            }));
        }

        void deviceManager_DevicesChanged(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                deviceTreeView.EndUpdate();
            }));
        }

        void deviceManager_DeviceAdded(object sender, NDeviceManagerDeviceEventArgs e)
        {
            BeginInvoke(new Action<NDevice>(delegate(NDevice device)
            {
                AddDevice(device);
                if (deviceTreeView.SelectedNode == null) deviceTreeView.SelectedNode = deviceTreeView.Nodes[0];
            }), e.Device);
        }

        void deviceManager_DeviceRemoved(object sender, NDeviceManagerDeviceEventArgs e)
        {
            BeginInvoke(new Action<NDevice>(delegate(NDevice device)
            {
                RemoveDevice(device);
            }), e.Device);
        }

        void deviceManager_DevicesRefreshed(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                //Log("Refreshing device list...");
                UpdateDeviceList();
            }));
        }

        private void OnDeviceManagerChanged()
        {
            if (_deviceManager == null)
            {
                UpdateDeviceList();
            }
            else
            {
                lock (_deviceManager.Devices.SyncRoot)
                {
                    UpdateDeviceList();
                    _deviceManager.DevicesChanging += new EventHandler<EventArgs>(deviceManager_DevicesChanging);
                    _deviceManager.DevicesChanged += new EventHandler<EventArgs>(deviceManager_DevicesChanged);
                    _deviceManager.DeviceAdded += new EventHandler<NDeviceManagerDeviceEventArgs>(deviceManager_DeviceAdded);
                    _deviceManager.DeviceRemoved += new EventHandler<NDeviceManagerDeviceEventArgs>(deviceManager_DeviceRemoved);
                    _deviceManager.DevicesRefreshed += new EventHandler<EventArgs>(deviceManager_DevicesRefreshed);
                }
            }
            //closeToolStripMenuItem.Enabled = refreshToolStripMenuItem.Enabled = _deviceManager != null;
            //connectToolStripMenuItem.Enabled = _deviceManager != null;
            //Text = _deviceManager == null ? stPr_NombreApp : string.Format("{0} (Device types: {1}, Auto update: {2})", stPr_NombreApp, _deviceManager.DeviceTypes, _deviceManager.AutoUpdate);
        }

        private void AddDevice(NDevice device)
        {
            //Log("Added device: {0}", device.Id);
            TreeNode deviceTreeNode = CreateDeviceNode(device);
            lock (device.Owner.Devices.SyncRoot) // Do not interfer with asynchronous device addition/removal
            {
                if (device.Parent == null)
                {
                    deviceTreeView.Nodes.Add(deviceTreeNode);
                }
                else
                {
                    TreeNode parentTreeNode = GetDeviceNode(device.Parent);
                    if (parentTreeNode != null)
                    {
                        parentTreeNode.Nodes.Add(deviceTreeNode);
                    }
                }
                foreach (NDevice child in device.Children)
                {
                    TreeNode childTreeNode = GetDeviceNode(child);
                    if (childTreeNode != null)
                    {
                        (childTreeNode.Parent == null ? deviceTreeView.Nodes : childTreeNode.Parent.Nodes).Remove(childTreeNode);
                        deviceTreeNode.Nodes.Add(childTreeNode);
                    }
                }
            }
        }

        private void RemoveDevice(NDevice device)
        {
            TreeNode deviceTreeNode = GetDeviceNode(device);
            bool isSelected = deviceTreeView.SelectedNode == deviceTreeNode;
            TreeNode[] childTreeNodes = new TreeNode[deviceTreeNode.Nodes.Count];
            for (int i = deviceTreeNode.Nodes.Count - 1; i >= 0; i--)
            {
                childTreeNodes[i] = deviceTreeNode.Nodes[i];
            }
            deviceTreeNode.Nodes.Clear();
            deviceTreeView.Nodes.AddRange(childTreeNodes);
            (deviceTreeNode.Parent == null ? deviceTreeView.Nodes : deviceTreeNode.Parent.Nodes).Remove(deviceTreeNode);
            if (isSelected)
            {
                try
                {
                    OnSelectedDeviceChanged();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private TreeNode GetDeviceNode(NDevice device, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag == device) return node;
                TreeNode subNode = GetDeviceNode(device, node.Nodes);
                if (subNode != null) return subNode;
            }
            return null;
        }

        private TreeNode GetDeviceNode(NDevice device)
        {
            return GetDeviceNode(device, deviceTreeView.Nodes);
        }

        private TreeNode CreateDeviceNode(NDevice device)
        {
            TreeNode deviceTreeNode = new TreeNode(device.DisplayName);
            deviceTreeNode.Tag = device;
            return deviceTreeNode;
        }

        private void FoundDevice(TreeNodeCollection nodes, NDevice device)
        {
            TreeNode deviceTreeNode = CreateDeviceNode(device);
            nodes.Add(deviceTreeNode);
            foreach (NDevice child in device.Children)
            {
                FoundDevice(deviceTreeNode.Nodes, child);
            }
        }

        private void UpdateDeviceList()
        {
            deviceTreeView.BeginUpdate();
            try
            {
                deviceTreeView.Nodes.Clear();
                if (_deviceManager != null)
                {
                    Monitor.Enter(_deviceManager.Devices.SyncRoot); // Do not interfer with asynchronous device addition/removal
                    try
                    {
                        foreach (NDevice device in _deviceManager.Devices)
                        {
                            if (device.Parent == null)
                            {
                                FoundDevice(deviceTreeView.Nodes, device);
                                if (device.DisplayName.Contains("Futronic") || device.DisplayName.Contains("Digital")||device.DisplayName.Contains("Upek"))
                                {
                                    bl_ExisteHuellero = true;
                                }
                                else
                                {
                                    bl_ExisteHuellero = false;
                                }
                            }
                        }
                    }
                    finally
                    {
                        Monitor.Exit(_deviceManager.Devices.SyncRoot);
                    }
                }
                if (deviceTreeView.Nodes.Count != 0)
                {
                    deviceTreeView.SelectedNode = deviceTreeView.Nodes[0];
                }
                else
                {
                    try
                    {
                        OnSelectedDeviceChanged();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            finally
            {
                deviceTreeView.EndUpdate();
            }
        }

        public NDeviceType DeviceTypes
        {
            get
            {
                NDeviceType value = NDeviceType.None;
                foreach (CheckBox checkBox in deviceTypesGroupBox.Controls)
                {
                    if (checkBox.Checked) value |= (NDeviceType)checkBox.Tag;
                }
                return value;
            }
            set
            {
                foreach (CheckBox checkBox in deviceTypesGroupBox.Controls)
                {
                    checkBox.Checked = (value & (NDeviceType)checkBox.Tag) != 0;
                }
            }
        }

        public bool AutoPlug
        {
            get
            {
                return cbAutoplug.Checked;
            }
            set
            {
                cbAutoplug.Checked = value;
            }
        }

        public bool AutoUpdate
        {
            get
            {
                return cbAutoupdate.Checked;
            }
            set
            {
                cbAutoupdate.Checked = value;
            }
        }

        private void ScanDispositivo()
        {
            if (DeviceTypes == NDeviceType.None)
            {
                DeviceTypes = NDeviceType.Any;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {

        }

        private void NewDeviceManager()
        {
            Settings settings = Settings.Default;
            anyCheckBox.Tag = NDeviceType.Any;
            captureDeviceCheckBox.Tag = NDeviceType.CaptureDevice;
            microphoneCheckBox.Tag = NDeviceType.Microphone;
            cameraCheckBox.Tag = NDeviceType.Camera;
            biometricDeviceCheckBox.Tag = NDeviceType.BiometricDevice;
            fScannerCheckBox.Tag = NDeviceType.FScanner;
            fingerScannerCheckBox.Tag = NDeviceType.FingerScanner;
            irisScannerCheckBox.Tag = NDeviceType.IrisScanner;
            DeviceTypes = NDeviceType.Any;
            DeviceTypes = settings.DeviceTypes;
            AutoPlug = settings.AutoPlug;
            AutoUpdate = settings.AutoUpdate;
            SetDeviceManager(new NDeviceManager(DeviceTypes, AutoPlug, AutoUpdate, (SynchronizationContext)null));
            settings.DeviceTypes = DeviceTypes;
            settings.AutoPlug = AutoPlug;
            settings.AutoUpdate = AutoUpdate;
            settings.Save();
        }

        private void deviceTypeCheckBox_Click(object sender, EventArgs e)
        {

        }

        void NCore_ErrorSuppressed(object sender, ErrorSuppressedEventArgs ea)
        {
            ClasX_EventLog ObjL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, false, true, false);

            BeginInvoke(new MethodInvoker(delegate { ObjL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ptbVista_Frontal_Click", "", ea.Error.ToString(), "", ""); }));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
        int numclick;
        private void LblPr_version_Click(object sender, EventArgs e)
        {
            numclick++;
            if(numclick == 4)
            {
                numclick = 0;
                Environment.Exit(1);
            }

        }

        private void Frm_Principal_Shown(object sender, EventArgs e)
        {
            int AlineaPantIzq = ((this.Width - pnl_teclado.Width) / 2);
            int alineaPanArri = ((this.Height - pnl_teclado.Height) / 2 + 20);
            pnl_teclado.Left = AlineaPantIzq;
            pnl_teclado.Top = alineaPanArri;
            pnl_TurnosInfoAfiliado.Left = ((this.Width - 100) - 100);
            pnl_TurnosInfoAfiliado.Top = alineaPanArri;
            pnl_certPago.Left = AlineaPantIzq;
            pnl_certPago.Top = alineaPanArri;
           // pnl_Consultas.Left = AlineaPantIzq;
            //pnl_Consultas.Top = alineaPanArri;
            pnl_EstadoCuenta.Left = AlineaPantIzq;
            pnl_EstadoCuenta.Top = alineaPanArri;
            pnl_huella.Left = AlineaPantIzq;
            pnl_huella.Top = alineaPanArri;
            pnl_Inicio.Left = AlineaPantIzq;
            pnl_Inicio.Top = alineaPanArri;
            pnl_NoAfiliado.Left = AlineaPantIzq;
            pnl_NoAfiliado.Top = alineaPanArri;
            //pnl_Turnos.Left = ((this.Width - pnl_teclado.Width) / 2);
            //pnl_Turnos.Top = ((this.Height - pnl_teclado.Height) / 2);
            pnl_ESTADO_TRAMITE.Left = AlineaPantIzq;
            pnl_ESTADO_TRAMITE.Top = alineaPanArri + 100;;
            //CMD_Cancelar.Left = AlineaPantIzq + 1000;
            CMD_Cancelar.Left = ((this.Width - 100) - 100);
            CMD_Cancelar.Top = alineaPanArri + 120;
            CMD_Prioritario.Top = alineaPanArri + 120;
            //CMD_Prioritario.Left = ((this.Width - 100) - 100);
            //CMD_Regresar.Left = AlineaPantIzq - 500;
            CMD_Regresar.Top = alineaPanArri + 120;
        }

       
        //


        private void cmd_1_MouseEnter(object sender, EventArgs e)
        {
            cmd_1.Image = Properties.Resources.B1_P;
        }

        private void cmd_1_MouseLeave(object sender, EventArgs e)
        {
            cmd_1.Image = Properties.Resources.B1_UP;
        }

        private void cmd_2_MouseEnter(object sender, EventArgs e)
        {
            cmd_2.Image = Properties.Resources.B2_P;
        }

        private void cmd_2_MouseLeave(object sender, EventArgs e)
        {
            cmd_2.Image = Properties.Resources.B2_UP;
        }

        private void cmd_3_MouseEnter(object sender, EventArgs e)
        {
            cmd_3.Image = Properties.Resources.B3_P;
        }

        private void cmd_3_MouseLeave(object sender, EventArgs e)
        {
            cmd_3.Image = Properties.Resources.B3_UP;
        }

        private void cmd_4_MouseEnter(object sender, EventArgs e)
        {
            cmd_4.Image = Properties.Resources.B4_P;
        }

        private void cmd_4_MouseLeave(object sender, EventArgs e)
        {
            cmd_4.Image = Properties.Resources.B4_UP;
        }

        private void cmd_5_MouseEnter(object sender, EventArgs e)
        {
            cmd_5.Image = Properties.Resources.B5_P;
        }

        private void cmd_5_MouseLeave(object sender, EventArgs e)
        {
            cmd_5.Image = Properties.Resources.B5_UP;
        }

        private void cmd_6_MouseEnter(object sender, EventArgs e)
        {
            cmd_6.Image = Properties.Resources.B6_P;
        }

        private void cmd_6_MouseLeave(object sender, EventArgs e)
        {
            cmd_6.Image = Properties.Resources.B6_UP;
        }

        private void cmd_7_MouseEnter(object sender, EventArgs e)
        {
            cmd_7.Image = Properties.Resources.B7_P;
        }

        private void cmd_7_MouseLeave(object sender, EventArgs e)
        {
            cmd_7.Image = Properties.Resources.B7_UP;
        }

        private void cmd_8_MouseEnter(object sender, EventArgs e)
        {
            cmd_8.Image = Properties.Resources.B8_P;
        }

        private void cmd_8_MouseLeave(object sender, EventArgs e)
        {
            cmd_8.Image = Properties.Resources.B8_UP;
        }

        private void cmd_9_MouseEnter(object sender, EventArgs e)
        {
            cmd_9.Image = Properties.Resources.B9_P;
        }

        private void cmd_9_MouseLeave(object sender, EventArgs e)
        {
            cmd_9.Image = Properties.Resources.B9_UP;
        }

        private void cmd_0_MouseEnter(object sender, EventArgs e)
        {
            cmd_0.Image = Properties.Resources.B0_P;
        }

        private void cmd_0_MouseLeave(object sender, EventArgs e)
        {
            cmd_0.Image = Properties.Resources.B0_UP;
        }

        private void cmd_Accept_MouseEnter(object sender, EventArgs e)
        {
            cmd_Accept.Image = Properties.Resources.B_AceptarP;
        }

        private void cmd_Accept_MouseLeave(object sender, EventArgs e)
        {
            cmd_Accept.Image = Properties.Resources.B_AceptarUP;
        }

        private void cmd_Cancel_MouseEnter(object sender, EventArgs e)
        {
            cmd_Cancel.Image = Properties.Resources.B_CancelarP;
        }
        private void cmd_Cancel_MouseLeave(object sender, EventArgs e)
        {
            cmd_Cancel.Image = Properties.Resources.B_CancelarUP;
        }

        private void CMD_Prioritario_MouseEnter(object sender, EventArgs e)
        {
            CMD_Prioritario.Image = Properties.Resources.B_AdicP_Discap1;
        }
        private void CMD_Prioritario_MouseLeave(object sender, EventArgs e)
        {
            CMD_Prioritario.Image = Properties.Resources.B_AdicUP_Discap1;
        }

        private void CMD_Regresar_MouseEnter(object sender, EventArgs e)
        {
            CMD_Regresar.Image = Properties.Resources.B_RegresarP;
        }
        private void CMD_Regresar_MouseLeave(object sender, EventArgs e)
        {
            CMD_Regresar.Image = Properties.Resources.B_RegresarUP;
        }

        private void cmd_Afiliado_Click(object sender, EventArgs e)
        {
            pnl_Inicio.Visible = false;
            pnl_teclado.Visible = true;
            CMD_Regresar.Visible = true;
        }

        private void CMD_TurnosRadicacion_Click(object sender, EventArgs e)
        {
            try
            {
                //ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Turnos_Click(), Iniciando ...");
                this.CMD_Regresar.Visible = true;
                cambiaPantallas(PANEL_TURNOSRADICACION);
                pnl_NoAfiliado.BringToFront();
                this.CMD_Prioritario.Visible = false;
                this.CMD_Regresar.Visible = true;
                //ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Turnos_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Turnos_Click", "", ex.Message.ToString(), "", "");
            }
        }

        private void cmd_NoAfiliado_Click(object sender, EventArgs e)
        {
            blPr_NoAfiliado = true;
            pnl_Inicio.Visible = false;
            cambiaPantallas(PANEL_NOAFILIADO);
            pnl_NoAfiliado.Visible = true;
            pnl_NoAfiliado.BringToFront();
            CMD_Regresar.Visible = true;
        }

        private void cmd_Apoderado_Click(object sender, EventArgs e)
        {
        blPr_Apoderado = true;
        cambiaPantallas(PANEL_TECLADO_INICIAL);

        }

        private void cmd_Autorizado_Click(object sender, EventArgs e)
        {
        blPr_Autorizado = true;
        cambiaPantallas(PANEL_TECLADO_INICIAL);
        }

        private void cmd_Beneficiario_Click(object sender, EventArgs e)
        {
        blPr_Beneficiario = true;
        cambiaPantallas(PANEL_TECLADO_INICIAL);
        }

        private void Frm_Principal_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txt_Identificacion.Text = txt_Identificacion.Text.Substring(0,txt_Identificacion.Text.Count()-1);
            if (txt_Identificacion.Text.Count() <= 0)
            {
                button1.Enabled=false;
                cmd_Accept.Enabled = false;

                
            }else
            {
                button1.Enabled =true;
                cmd_Accept.Enabled = true;
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {

            int inL_Puerto = Convert.ToInt16(StPr_inPuerto);
            String stL_nombreK = "";
            ClasX_Consultas objL_Consultas = new ClasX_Consultas(stPr_ExeName_Exe, ObjPr_EventLog, stPr_ParamBd);
            SqlDataReader rdr = null;
            
            
            CMD_RENTA.Text = "CONSULTANDO...";
            CMD_RENTA.Enabled = false;
            CMD_Certificado.Enabled = false;
            CMD_EstadoTramite.Enabled = false;
            CMD_EstadCuenta.Enabled = false;
            //CMD_ImprimirEstado.Enabled = true;
          // string cc = "79345472";
           //int Cta_Numero = 301819;
            try {
                //consulta Sp para #cuenta
                String temp = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
                stL_nombreK = temp;
                temp = ObjPr_ConfigApp.LeeLlave_Seccion(temp, "NOMBRE");
                String[] infoConsulta = Obj_AppKiosko.obtieneToken(stPr_Serv, StPr_inPuerto);//posicion 0 = token, posicion 1 = usuario aDirector
                stL_nombreK += "-" + infoConsulta[1] + "-" + Obj_AppKiosko.Get_My_Ip(stPr_Serv, StPr_inPuerto);
                char[] delim = { ' ', '_' };
                String[] stmpt = temp.Split(delim);
                int cont = 0;
                for (int i = 0; i < stmpt.Length; i++)
                {
                    String tmp = stmpt[i].ToUpper();
                    if (tmp.Contains(st_Oficina1) || tmp.Contains(st_Oficina2) || tmp.Contains(st_Oficina3) || tmp.Contains(st_Oficina4) || tmp.Contains(st_Oficina5) || tmp.Contains(st_Oficina6) || tmp.Contains(st_Oficina7) || tmp.Contains(st_Oficina8) || tmp.Contains(st_Oficina9) || tmp.Contains(st_Oficina10) || tmp.Contains(st_Oficina11))
                    //if (tmp.Contains("CAN") || tmp.Contains("VENECIA") || tmp.Contains("CALI") || tmp.Contains("MEDELLIN") || tmp.Contains("CARTAGENA") || tmp.Contains("BARRANQUILLA") || tmp.Contains("BUCARAMANGA") || tmp.Contains("IBAGUE") || tmp.Contains("FLORENCIA") || tmp.Contains("TOLEMAIDA"))
                    {
                        break;
                    }
                    cont++;
                }
                rdr = objL_Consultas.EjecutaSP("SP_021_ACT_CONSUL_KIOSKO", txt_Identificacion.Text + ",'" + infoConsulta[0] + "','" + stL_nombreK + "','4'");
                rdr = objL_Consultas.EjecutaSP("SP_019_LEE_AFILIADO_CUENTAS_GA2", txt_Identificacion.Text);
                if (rdr != null)
                {
                    long cuenta = 0;
                    while (rdr.Read())
                    {
                        if (!DBNull.Value.Equals(Convert.ToInt64(rdr[0]))) cuenta = Convert.ToInt64(rdr[0]);
                        //fin cinsulta cuenta
                    }
                        //inicia consume web services para generar reporte
                        MCDIntegrationServices.wsModeloCanonicoDatosSoapClient client = new wsModeloCanonicoDatosSoapClient();
                      var a = client.GenerarReporte("DECLARACION_RENTA", txt_Identificacion.Text, Convert.ToInt32(cuenta));
                      //  var a = client.GenerarReporte("DECLARACION_RENTA", cc, Cta_Numero);
                        client.Close();
                        
                        if (a.Report != null && a.Report.Length > 0)
                        {
                                String strL_tempDir = "C:\\fnx";
                               
                                
                            MemoryStream ms = new MemoryStream(a.Report);
                            FileStream file = new FileStream(strL_tempDir+ "\\" + inPr_A001NUM_IDENTIFICACION + "\\Declaracion.pdf", FileMode.Create, FileAccess.Write);
                            ms.WriteTo(file);
                            file.Close();
                            ms.Close();

                           
                            webBrowser1.Navigate(Convert.ToString("File://" + strL_tempDir + "\\" + inPr_A001NUM_IDENTIFICACION + "\\Declaracion.pdf"));
                          
                            //carga la imagen del stream en un picturebox
                           
                            //string dialogResult = MyMessageBox.ShowBox("Se genero Certficado de Decalracion de Renta", "", "Aceptar", "Cancelar", true, 42);
                          //  MyMessageBox.ActiveForm.Activate();
                            //MyMessageBox.ActiveForm.BringToFront();

                            //VuelveInicio();
                            objL_Consultas.CierraCOnexiones();
                            cambiaPantallas(PANEL_DECLARACION);
                            this.CMD_Prioritario.Visible = false;
                            this.CMD_Regresar.Visible = true;
                        }
                        else
                        {
                            string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_21, "", "Aceptar", "Salir", false, 155);
                            MyMessageBox.ActiveForm.Activate();
                            MyMessageBox.ActiveForm.BringToFront();
                            ObjPr_EventLog.setTextErrLog(" No Se genero Certficado de Declaracion de Renta");
                            if (dialogResult.Equals("1"))
                            {
                                lblMsjs.Text = "Error de conexión WEB Sevices Canonicos o  GA2";
                                CMD_EstadCuenta.Enabled = true;
                                CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                                CMD_Certificado.Enabled = true;
                                CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                                CMD_EstadoTramite.Enabled = true;
                                CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                                CMD_RENTA.Enabled = true;
                                CMD_RENTA.Text = stPr_DECLARACION;
                            }
                            if (dialogResult.Equals("2"))
                            {
                                VuelveInicio();
                            }
                            
                            
                        }

                        
                    
                   
                }
                else
                {
                    lblMsjs.Text = "Error conexión a GA2";
                    ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Certificado_Click(), Error en consula a GA2");
                    string dialogResult = MyMessageBox.ShowBox(MENSAJE_APP_21, "", "Aceptar", "Salir", false, 155);
                    MyMessageBox.ActiveForm.Activate();
                    MyMessageBox.ActiveForm.BringToFront();
                    ObjPr_EventLog.setTextErrLog(MENSAJE_APP_17 + txt_Identificacion.Text + MENSAJE_APP_18);
                    if (dialogResult.Equals("1"))
                    {
                        lblMsjs.Text = "Error de conexión a GA2";
                        CMD_EstadCuenta.Enabled = true;
                        CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                        CMD_Certificado.Enabled = true;
                        CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                        CMD_EstadoTramite.Enabled = true;
                        CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                        CMD_RENTA.Enabled = true;
                        CMD_RENTA.Text = stPr_DECLARACION;
                    }
                    if (dialogResult.Equals("2"))
                    {
                        VuelveInicio();
                    }
                }



                CMD_EstadCuenta.Enabled = true;
                CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                CMD_Certificado.Enabled = true;
                CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                CMD_EstadoTramite.Enabled = true;
                CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                CMD_RENTA.Enabled = true;
                CMD_RENTA.Text = stPr_DECLARACION;
            ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RENTA_Click(), Generar Certificado de Renta");
           
            
           
            }
            catch (Exception ex)
            {
                
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RENTA_Click", "", ex.Message.ToString(), "", "");
                string dialogResult = MyMessageBox.ShowBox("Excepción:"+ex, "", "Aceptar", "Salir", false, 155);
                MyMessageBox.ActiveForm.Activate();
                MyMessageBox.ActiveForm.BringToFront();
                CMD_EstadCuenta.Enabled = true;
                CMD_EstadCuenta.Text = stPr_ESTADOCUENTA;
                CMD_Certificado.Enabled = true;
                CMD_Certificado.Text = stPr_CERTIFICADOPAGO;
                CMD_EstadoTramite.Enabled = true;
                CMD_EstadoTramite.Text = stPr_ESTADOTRAMITE;
                CMD_RENTA.Enabled = true;
                CMD_RENTA.Text = stPr_DECLARACION;
                VuelveInicio();
            }
         
          
        }

        private void cmd_imprimir_dela_Click(object sender, EventArgs e)
        {
            String strL_tempDir = "C:\\fnx";
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Imprimir_Declaraciond de Renta_Click(), Iniciando ...");
                cmd_imprimir_dela.Enabled = false;
                this.pnl_decla.Enabled = false;
                string Filepath = @strL_tempDir + "\\" + inPr_A001NUM_IDENTIFICACION + "\\Declaracion.pdf";
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";

                //Define location of adobe reader/command line
                //switches to launch adobe in "print" mode
                proc.StartInfo.FileName =
                  @"C:\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe";
                proc.StartInfo.Arguments = String.Format(@"/p /h {0}", Filepath);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();
                KillAdobe("AcroRd32");
                LimpiaPantallas();
                this.pnl_decla.Enabled = true;
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Imprimir_Declaracion(), Termina OK");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Imprimir_Declaracion", "", ex.Message.ToString(), "", "");
            }
            
        }
        private static bool KillAdobe(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses().Where(
                         clsProcess => clsProcess.ProcessName.StartsWith(name)))
            {
                clsProcess.Kill();
                return true;
            }
            return false;
        }
        private void button13_Click(object sender, EventArgs e)
        {

           try
            {

                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaN14(), Iniciando ...");
                
               
               this.pnl_TurnosNAfiliados.Visible = false;
               estadoEscaneo = -1;
               apagaTodosLosPanels(); 
               string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                           
                 this.pnl_Turno.Visible = true;
                 Application.DoEvents();
                 AsignaTurnos(stPrc_AICATORCE);
                 ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaN14(), Termina Ok");
           
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_ViviendaN14", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_ViviendaN8_Click(object sender, EventArgs e)
        {
          try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaN8(), Iniciando ...");
                estadoEscaneo = -1;
                apagaTodosLosPanels(); 
                this.pnl_TurnosNAfiliados.Visible = false;
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
               
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_AIOCHO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaN8(), Termina Ok");
           
                 }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_ViviendaN8", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_ViviendaNLeasing_Click(object sender, EventArgs e)
        {
           try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaNLeasing(), Iniciando ..."); 
               this.pnl_TurnosNAfiliados.Visible = false;
               estadoEscaneo = -1;
               apagaTodosLosPanels(); 
               string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                
                 this.pnl_Turno.Visible = true;
                 Application.DoEvents();
                 AsignaTurnos(stPrc_AILEASING);
                 ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaNLeasing(), Termina Ok");
               
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_ViviendaNLeasing", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_ViviendaNFondo_Click(object sender, EventArgs e)
        {
           try
           {
               ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaNFondo(), Iniciando ...");
                this.pnl_TurnosNAfiliados.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels(); 
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
               
               
               this.pnl_Turno.Visible = true;
               Application.DoEvents();
               AsignaTurnos(stPrc_AIHEROES);
               ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaNFondo(), Termina Ok");
               
           }
           catch (Exception ex)
           {
               ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_ViviendaNFondo", "", ex.Message.ToString(), "", "");
           }
        }

        private void CMD_ViviendaNFuturo_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaNFuturo(), Iniciando ...");
                this.pnl_TurnosNAfiliados.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels(); 
                string dialogResult = MyMessageBox.ShowBox("Se realizaráre la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);

             
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_AIFUTURO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_ViviendaNFuturo(), Termina Ok");
            
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_ViviendaNFuturo", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_RadiViviendaNLeasing_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiViviendaNLeasing(), Iniciando ...");
                this.pnl_TurnosNAfiliados.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                
                this.pnl_Turno.Visible = true;
                
                Application.DoEvents();
                AsignaTurnos(stPrc_ARVIVIENDA_LEASING);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiViviendaNLeasing(), Termina Ok");
                
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RadiViviendaNLeasing", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_RadiViviendaN8_Click(object sender, EventArgs e)
        {
           try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiViviendaN8(), Iniciando ...");
                this.pnl_TurnosNAfiliados.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_ARVIVIENDA_OCHO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiViviendaN8_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RadiViviendaN8", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_RadiViviendaN14_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiViviendaN14(), Iniciando ...");
                this.pnl_TurnosNAfiliados.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                       
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_ARVIVIENDA_CATORCE);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiViviendaN14(), Termina Ok");
                
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RadiViviendaN14", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_RadiNFondo_Click(object sender, EventArgs e)
        {
           try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiNFondo(), Iniciando ...");
                this.pnl_TurnosNAfiliados.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                              
                 this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_ARHEROES);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiNFondo(), Termina Ok");
                
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RadiNFondo", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_RadiNFuturo_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiNFuturo(), Iniciando ...");
                this.pnl_TurnosNAfiliados.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
               
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_ARFUTURO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RadiNFuturo(), Termina Ok");
                 
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RadiNFuturo", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_Atencion_cita_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Atencion_cita_Clickl(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_IAGENDACITAP);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Atencion_cita_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Atencion_cita_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_turnoleasingafiliado_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_turnoleasingafiliado_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_RVIVIENDA_LEASING);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_turnoleasingafiliado_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_turnoleasingafiliado_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CDM_Turno8afiliado_Click(object sender, EventArgs e)
        {
           try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CDM_Turno8afiliado_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_RVIVIENDA_OCHO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CDM_Turno8afiliado_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CDM_Turno8afiliado_Click()", "", ex.Message.ToString(), "", "");
            }

        }

        private void CMD_Turno14afiliado_Click(object sender, EventArgs e)
        {
            
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Turno14afiliado_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_RVIVIENDA_CATORCE);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Turno14afiliado_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Turno14afiliado_Click()", "", ex.Message.ToString(), "", "");
            }

        }

        private void CMD_RAFONDOH_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RAFONDOH_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_RHEROES);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RAFONDOH_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RAFONDOH_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_Turnofuturoafiliado_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Turnofuturoafiliado_Click(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_RFUTURO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_Turnofuturoafiliado_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_Turnofuturoafiliado_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_Turnopretramiteafiliado_Click(object sender, EventArgs e)
        {
            
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.stPrc_RPRETRAMITE(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_RPRETRAMITE);
                ObjPr_EventLog.setTextErrLog("frm_Principal.stPrc_RPRETRAMITE(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "stPrc_RPRETRAMITE()", "", ex.Message.ToString(), "", "");
            }

        }

        private void CMD_Turnotramiteafiliado_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.stPrc_RTRAMITE_LINEA(), Iniciando ...");
                this.pnl_TurnosInfoAfiliado.Visible = false;

                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_RTRAMITE_LINEA);
                ObjPr_EventLog.setTextErrLog("frm_Principal.stPrc_RTRAMITE_LINEA(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "stPrc_RTRAMITE_LINEA()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_BRLEASING_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_BRLEASING_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BRLEASING);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_BRLEASING_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_BRLEASING_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_RBOCHO_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RBOCHO_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BROCHO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RBOCHO_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RBOCHO_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RBCATORCE_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BRCATORCE);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RBCATORCE_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RBCATORCE_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_RBFONDO_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RBFONDO_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BRHEROES);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RBFONDO_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RBFONDO_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_RBFUTURO_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RBFUTURO_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BRFUTURO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_RBFUTURO_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_RBFUTURO_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_IBLEASING_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.stPrc_BILEASING(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BILEASING);
                ObjPr_EventLog.setTextErrLog("frm_Principal.stPrc_BILEASING(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "stPrc_BILEASING()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_IBOCHO_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_IBOCHO_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BIOCHO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_IBOCHO_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_IBOCHO_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_IBCATORCE_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_IBCATORCE_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BICATORCE);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_IBCATORCE_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_IBCATORCE_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_IBFONDO_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_IBFONDO_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BIHEROES);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_IBFONDO_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_IBFONDO_Click()", "", ex.Message.ToString(), "", "");
            }
        }

        private void CMD_IBFUTURO_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_IBFUTURO_Click(), Iniciando ...");
                this.pnlTurnosBeneficiarios.Visible = false;
                estadoEscaneo = -1;
                apagaTodosLosPanels();
                string dialogResult = MyMessageBox.ShowBox("Se realizará la verificación de la identificación y documentación correspondiente", "", "Aceptar", "Salir", false, 155);
                this.pnl_Turno.Visible = true;
                Application.DoEvents();
                AsignaTurnos(stPrc_BIFUTURO);
                ObjPr_EventLog.setTextErrLog("frm_Principal.CMD_IBFUTURO_Click(), Termina Ok");
            }
            catch (Exception ex)
            {
                ObjPr_EventLog.outMensajError(stPr_ExeName_Exe, "Frm_Principal", "CMD_IBFUTURO_Click()", "", ex.Message.ToString(), "", "");
            }

        }
        

     

       

       

        

       
      
       


    }
}