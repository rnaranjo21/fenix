using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using System.Data.Sql;
using System.IO;
using System.Runtime.ExceptionServices;


namespace NBToolsNet
{
    public partial class FrmLogin : Form
    {
        private String _st_User = ""; // Codigo del usuario de la aplicacion
        private String _st_FileLog = ""; // Nombre el Archivo Log.
        //
        private String stPr_ArchivoConfigApp = ""; // Ruta y nombre del archivo de configuracion de la aplicacion,
        private String stPr_Archivo_InfoBds = ""; // Ruta y nombde del archivo de informacion de las bases de datos, para hacer conexion.
        //
        private Boolean blPr_AceptoInformacion = false; // Indica si ingreso los datos correctos del usuario
        //
        private CLNBTN_Cg ObjPr_Conf = null; // Para manejar la configuracion de la aplicacion.
        private CLNBTN_Cg ObjPr_InfoBDS_Fenix = null;// Para manejar la configuracion de la conexion a la base de datos, tipo fenix.
        //
        private CLNBTN_IQy ObjPr_InfoBD = null; // Define el objeto de la informacion de la base de datos con la cual va a trabajar.
        //
        private CLNBTN_Ul ObjPr_Utils = null; // Para utilizar las utilidades.
        //
        private String stPr_Nombre_App = ""; // Nombre de la aplicacion.
        private String stPr_Version_App = ""; // Version de la aplicacion.
        private String stPr_NombreEmpresa_App = ""; // Nombre de la empresa donde esta instalado la aplicacion.
        //
        private Boolean blPr_ServidorYaDefinido = false;
        private Boolean blPr_YaActivada = false;
        //
        private String stPr_NombreBd_XTrabajo = "";
        //
        private const int MAX_INTENTOS = 3; // Maximo numero de intentos
        private int inPr_NumIntentos = 0; // Numero de intentos
        //
        private String stPr_Servidor_Ori = "";
        private String stPr_Usuario_Ori = "";
        private String stPr_Clave_Ori = "";
        //
        private Boolean blPr_EscondeForma = false;
        //
        private String stPr_Seccion_BaseDeDatos = ""; // El nombre de la seccion de la base de datos, con la cual se va a conectar
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "FrmLogin";
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        //
        private const String ESTADO_ACTIVO = "-1";
        private const String ESTADO_NO_ACTIVO = "0";
        //
        private const String SECCION_BD_0 = "SYS_BD_ZERO"; // La seccion para leer los parametros de la base de datos.
        //private const String SECCION_BD_0_DEMO = "SYS_BD_ZERO_DEMO"; // La seccion para leer los parametros de conexion de la base de datos.
        private const String SECCION_BD_CONNECT_INFO = "BD_CONNECT_INFO"; // La seccion para leer los parametros de conexion con la base de datos, tipo de conexion Fenix.
        //
        private const String SECCION_BD_1 = "SYS_BD_ONE"; // La seccion para leer los parametros de la base de datos. Segunda Base de Datos.
        //
        private const String SECCION_ID_APP = "APP"; // Seccion donde esta el Id de la Aplicacion
        private const String SECCION_OBJETOS_APP = "OBJETOS"; // Seccion donde estan los objetos a registrar
        //
        private const String NEW_LINE = "\r\n"; // Caracteres para nueva linea
        // Los permossos
        private const String PERMISO_READ = "R";
        private const String PERMISO_WRITE = "W";
        private const String PERMISO_DELETE = "D";
        private const String PERMISO_DISPLAY = "Y";
        //////////////////////////////////////////////////////////////////////////////////////
        // Los Mensajes utilizados, en la parte del login.
        //////////////////////////////////////////////////////////////////////////////////////
        private const String MENSAJE_1 = "La información del la Base de Datos, no está definida en el archivo de configuración de la aplicación";
        private const String MENSAJE_2 = "El tipo de Conexión para la Base de Datos, está mal configurada o no está definida en el archivo de configuración de la aplicación";
        private const String MENSAJE_3 = "Acceso Negado";
        private const String MENSAJE_4 = "A continuación se debe definir la información de conexión de la Base de Datos, para poder trabajar con la aplicación";
        private const String MENSAJE_5 = "Atención";
        //
        private const String MENSAJE_6 = "¿ Está seguro de grabar esta información, para la conexión con la base de datos ?";
        private const String MENSAJE_7 = "Error al establecer la conexión";
        private const String MENSAJE_8 = "La conexión se estableció correctamente";
        private const String MENSAJE_9 = "Ingreso al Sistema";
        private const String MENSAJE_10 = "Ingreso Información de Conexión a la Base de Datos";
        //
        private const String MENSAJE_11 = "El código de Usuario:";
        private const String MENSAJE_12 = "No está registrado, como usuario válido en el sistema.";
        private const String MENSAJE_13 = "No está ACTIVO en el sistema.";
        private const String MENSAJE_14 = "No tiene contraseña asignada. Se presentará una ventana, para definir la contraseña del usuario.";
        private const String MENSAJE_15 = "Contraseña Inválida.";
        private const String MENSAJE_16 = "Los días de validez de la contraseña, han caducado. Se presentará una ventana, para definir la contraseña del usuario.";
        private const String MENSAJE_17 = "Cambio de Contraseña";
        private const String MENSAJE_18 = "La Contraseña actual no coincide, con la digitada.";
        private const String MENSAJE_19 = "Las nuevas Contraseñas, no son iguales.";
        private const String MENSAJE_20 = "La nueva Contraseña, debe ser diferente de la Contraseña actual.";
        private const String MENSAJE_21 = "Se ha cambiado la Contraseña del usuario.";
        private const String MENSAJE_22 = "Favor entrar en contacto con el Administrador del Sistema.";
        private const String MENSAJE_23 = "Hallando información de los servidores de Bases de Datos disponibles.";
        private const String MENSAJE_24 = "Por favor esperar unos segundos..............";
        private const String MENSAJE_25 = "Intentando establecer conexión con el servidor de Base de Datos.";
        //
        private const String MENSAJE_38 = "Error en la autenticación en el Dominio de Windows";
        //

        //
        public FrmLogin()
        {
            InitializeComponent();
        }


        [HandleProcessCorruptedStateExceptions]
        public void GetParam(String LicName , String WinTittle, String UserName, String LogFile, String ConfFile, String AppInfo_Name, String AppInfo_Ver, String AppInfo_Cia, String InfBdFile, String DBName4Work, ref CLNBTN_IQy Obj_BaseDeDatos, String DBSectionName = "")
        {
            // Toma los parametros.
            String stL_Aux = "";
            String stL_Mensaje1 = "";
            String stL_Mensaje2 = "";
            CLNBTN_IQy.inDB_Types inL_TipoBD = 0;
            CLNBTN_IQy.inConnect_Type inL_TipoConexion = 0;
            CLNBTN_Es ObjL_Encrip = null;
            //
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                // Toma la seccion de la base de datos con la cual va a trabajar.
                stPr_Seccion_BaseDeDatos = DBSectionName;
                // Si viene vacia, toma la seccion SYS_BD_ZERO
                if (stPr_Seccion_BaseDeDatos.Length == 0)
                {
                    stPr_Seccion_BaseDeDatos = SECCION_BD_0;
                }
                // Guarda textos originales.
                stPr_Servidor_Ori = cmbServidores.Text;
                stPr_Usuario_Ori = TxtUsuario.Text;
                stPr_Clave_Ori = TxtClave.Text;
                //
                inPr_NumIntentos = 0;
                LblMensaje.Text = "";
                //
                this.Text = WinTittle;
                _st_User = UserName;
                _st_FileLog = LogFile;
                stPr_ArchivoConfigApp = ConfFile;
                stPr_Archivo_InfoBds = InfBdFile;
                stPr_NombreBd_XTrabajo = DBName4Work;
                // Crea instancia para la clase que maneja las configuraciones
                ObjPr_Conf = new CLNBTN_Cg(stPr_ArchivoConfigApp, _st_User, _st_FileLog, _st_Lic);
                //
                ObjPr_Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _st_Lic);
                //
                stPr_Nombre_App = AppInfo_Name;
                stPr_Version_App = AppInfo_Ver;
                stPr_NombreEmpresa_App = AppInfo_Cia;
                // Coloca aplicacion y version
                this.LblModuloVersion.Text = stPr_Nombre_App + " " + stPr_Version_App;
                this.LblNombreCia.Text = stPr_NombreEmpresa_App;
                // La informacion de la base de datos para manejar la conexion con la base de datos.
                ObjPr_InfoBD = new CLNBTN_IQy(_st_Lic);
                ObjPr_InfoBD = Obj_BaseDeDatos;
                //
                // Ejemplo del archivo de configuracion
                //       [SYS_BD_ZERO]
                //Name=Administracion
                //Engine=0
                //Security=4
                //Path=
                //URL=
                //Server=
                //IPAddress=
                //;
                ///////////////////////////////////////////////////////////////
                // Halla Informacion de la base de datos
                ///////////////////////////////////////////////////////////////
                stL_Mensaje1 = "";
                // Nombre de la base de datos.
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.ReadAKeyFromSection(stPr_Seccion_BaseDeDatos, "Name");
                if (stL_Aux.Length == 0)
                {
                    stL_Mensaje1 = MENSAJE_1;
                }
                else
                {
                    ObjPr_InfoBD.setDataBaseName(stL_Aux);
                }
                // tipo de Motor de Base de Datos
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.ReadAKeyFromSection(stPr_Seccion_BaseDeDatos, "Engine");
                if (stL_Aux.Length == 0)
                {
                    stL_Mensaje1 = MENSAJE_1;
                }
                else
                {
                    inL_TipoBD = (CLNBTN_IQy.inDB_Types)(Convert.ToInt32(stL_Aux));
                    //
                    ObjPr_InfoBD.setDataBaseEngine_Type(inL_TipoBD);
                }
                // Tipo de Conexion
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.ReadAKeyFromSection(stPr_Seccion_BaseDeDatos, "Security");
                if (stL_Aux.Length == 0)
                {
                    stL_Mensaje1 = MENSAJE_1;
                }
                else
                {
                    inL_TipoConexion = (CLNBTN_IQy.inConnect_Type)(Convert.ToInt32(stL_Aux));
                    //
                    ObjPr_InfoBD.setDataBaseConn_Type(inL_TipoConexion);
                }
                //
                // Path de la base de datos.
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.ReadAKeyFromSection(stPr_Seccion_BaseDeDatos, "Path");
                ObjPr_InfoBD.setDataBasePath(stL_Aux);
                // URL 
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.ReadAKeyFromSection(stPr_Seccion_BaseDeDatos, "URL");
                ObjPr_InfoBD.setServer_URL(stL_Aux);
                // Nombre de Servidor 
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.ReadAKeyFromSection(stPr_Seccion_BaseDeDatos, "Server");
                if (stL_Aux.Length == 0)
                {
                    //stL_Mensaje1 = MENSAJE_1;
                }
                else
                {
                    ObjPr_InfoBD.setServerName(stL_Aux);
                }
                // Ip Address
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.ReadAKeyFromSection(stPr_Seccion_BaseDeDatos, "IPAddress");
                ObjPr_InfoBD.setServer_IP_Address(stL_Aux);
                //
                CmdCambiarClave.Enabled = false;
                //
                switch (ObjPr_InfoBD.get_DataBaseConn_Type())
                { // Inicio del switch (ObjPr_InfoBD.getTipoConexion())
                    // 
                    case CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL:
                        // Usuario y clave de la bd, vienen en archivo de configuraciones
                        TxtUsuario.Text = _st_User;
                        break;
                    case CLNBTN_IQy.inConnect_Type.TYPE_2_CONNECT_USER_APP:
                        // Usuario de la aplicacion, mismo usuario de la base de datos
                        TxtUsuario.Text = _st_User;
                        break;
                    case CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN:
                        // Usuario Windows es el mismo usuario de la base de datos
                        TxtUsuario.Text = _st_User;
                        TxtUsuario.Enabled = false;
                        GrpClave.Visible = false;
                        TxtClave.Text = "";
                        CmdCambiarClave.Visible = false;
                        break;
                    case CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT:
                        // Conexion tipo Fenix.
                        TxtUsuario.Text = _st_User;
                        if (File.Exists(stPr_Archivo_InfoBds))
                        {
                            ObjPr_InfoBDS_Fenix = new CLNBTN_Cg(stPr_Archivo_InfoBds, _st_User, _st_FileLog, _st_Lic);
                            ObjL_Encrip = new CLNBTN_Es(_st_User, _st_FileLog, _st_Lic);
                            // Halla y DesEncripta la informacion del servidor
                            stL_Aux = "";
                            stL_Aux = ObjPr_InfoBDS_Fenix.ReadAKeyFromSection(SECCION_BD_CONNECT_INFO, "ServerName");
                            if (stL_Aux.Length != 0)
                            {
                                stL_Aux = ObjL_Encrip.File2Des(stL_Aux, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                ObjPr_InfoBD.setServerName(stL_Aux);
                                blPr_ServidorYaDefinido = true;
                            }
                            // Halla y DesEncripta la informacion de la base de datos
                            stL_Aux = "";
                            stL_Aux = ObjPr_InfoBDS_Fenix.ReadAKeyFromSection(SECCION_BD_CONNECT_INFO, "DBName");
                            if (stL_Aux.Length != 0)
                            {
                                stL_Aux = ObjL_Encrip.File2Des(stL_Aux, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                ObjPr_InfoBD.setDataBaseName(stL_Aux);
                            }
                            // Halla y DesEncripta la informacion del usuario
                            stL_Aux = "";
                            stL_Aux = ObjPr_InfoBDS_Fenix.ReadAKeyFromSection(SECCION_BD_CONNECT_INFO, "UID");
                            if (stL_Aux.Length != 0)
                            {
                                stL_Aux = ObjL_Encrip.File2Des(stL_Aux, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                ObjPr_InfoBD.setDataBase_UserID(stL_Aux);
                            }
                            // Halla y DesEncripta la informacion de la clave
                            stL_Aux = "";
                            stL_Aux = ObjPr_InfoBDS_Fenix.ReadAKeyFromSection(SECCION_BD_CONNECT_INFO, "PWDID");
                            if (stL_Aux.Length != 0)
                            {
                                stL_Aux = ObjL_Encrip.File2Des(stL_Aux, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                ObjPr_InfoBD.setDataBase_UserPWD(stL_Aux);
                            }
                        }
                        break;
                    default:
                        //
                        stL_Mensaje2 = MENSAJE_2;
                        break;
                } // Fin de switch (ObjPr_InfoBD.getTipoConexion())
                //
                if (stL_Mensaje1.Length > 0)
                {
                    LblMensaje.Text = stL_Mensaje1;
                }
                if (stL_Mensaje2.Length > 0)
                {
                    if (LblMensaje.Text.Length == 0)
                    {
                        LblMensaje.Text = stL_Mensaje2;
                    }
                    else
                    {
                        LblMensaje.Text = LblMensaje.Text + NEW_LINE + stL_Mensaje2;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetParam. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetParam. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void BringBackParam(ref Boolean InfoIsAccepted, ref CLNBTN_IQy ObDbInfo)
        {
            // Devuelve los parametros
            try
            {
                //
                InfoIsAccepted = blPr_AceptoInformacion;
                // Devuelve el objeto con la informacion de la base de datos.
                ObDbInfo = ObjPr_InfoBD;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringBackParam. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringBackParam. Exception", "", ex.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private void FrmLogin_Shown(Object sender, EventArgs e)
        {
            try
            {
                /////////////////////////////////////////////////////////
                if (blPr_YaActivada == false)
                {
                    blPr_YaActivada = true;
                    HallaListaServidores();
                }
                HabilitaBotones();
                // si tiene ue ocultar la forma con base en el valor de esta bandera
                // lo hace
                if (blPr_EscondeForma)
                {
                    blPr_AceptoInformacion = false;
                    // Cierra la forma
                    this.Hide();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin_Shown. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin_Shown. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void CmdCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                blPr_AceptoInformacion = false;
                // Cierra la forma
                this.Hide();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(1). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(1). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            CLNBTN_Qy Query = null;
            CLNBTN_Sg ObjL_Secur = null;
            Boolean blL_Conectar = true;
            Boolean blL_AceptoDatos = false;
            Boolean blL_Se_Conecto = false;
            String stL_MensajeValidaUso = "";
            Boolean blL_PideCambioClave = false;
            Boolean blL_Definio_Servidor = false;
            try
            {
                //
                LblMensaje.Text = "";
                Application.DoEvents();
                //
                inPr_NumIntentos = inPr_NumIntentos + 1;
                if (inPr_NumIntentos > MAX_INTENTOS)
                {
                    LblMensaje.Text = MENSAJE_3;
                }
                else
                {
                    LblMensaje.Text = MENSAJE_25 + NEW_LINE + MENSAJE_24;
                    Application.DoEvents();
                    // Asigna informacion
                    Query = new CLNBTN_Qy(_st_User, _st_FileLog, _st_Lic);
                    //
                    ObjPr_InfoBD.setServerName(cmbServidores.Text);
                    ObjL_Secur = new CLNBTN_Sg(_st_User, _st_FileLog, stPr_ArchivoConfigApp, stPr_Nombre_App, stPr_Version_App, stPr_NombreEmpresa_App, stPr_Archivo_InfoBds, stPr_NombreBd_XTrabajo, _st_Lic);
                    ObjL_Secur.Is_A_Valid_DB_Conn(ref blL_Definio_Servidor, ref blL_Conectar, ref blL_Se_Conecto, ref ObjPr_InfoBD, ObjPr_Conf, TxtUsuario.Text, TxtClave.Text, stPr_Seccion_BaseDeDatos);
                    //
                    if (blL_Definio_Servidor)
                    {
                        // Si definio el servidor lo coloca en la lista y deshabilita el combobox
                        List<String> LstServidores = new List<String>();
                        // Asigna el Servior, que tiene la BD
                        LstServidores.Add(ObjPr_InfoBD.getServerName());
                        this.cmbServidores.DataSource = LstServidores;
                        cmbServidores.Enabled = false;
                    }
                    // Hace la conexion
                    Query.setDataBaseInfo(ObjPr_InfoBD);
                    if (blL_Conectar)
                    { // del if (blL_Conectar)
                        Query.ConnectDataBase();
                        //
                        if (Query.getIs_Connected())
                        {
                            blL_Se_Conecto = true;
                        }
                    } // del if (blL_Conectar)
                    //
                    if ( blL_Se_Conecto ) 
                    {
                        // Valida el usuario
                        ObjL_Secur = new CLNBTN_Sg(_st_User, _st_FileLog, stPr_ArchivoConfigApp, stPr_Nombre_App, stPr_Version_App, stPr_NombreEmpresa_App, stPr_Archivo_InfoBds, stPr_NombreBd_XTrabajo, _st_Lic);
                        //
                        ObjL_Secur.Is_A_Valid_User_Access(ref blL_AceptoDatos, ref stL_MensajeValidaUso, ref blL_PideCambioClave, ref ObjPr_InfoBD, false);
                        // Presenta el mensaje del usuario
                        LblMensaje.Text = stL_MensajeValidaUso;
                        Application.DoEvents();
                        //
                        if (blL_PideCambioClave)
                        {
                            // Presenta ventana para cambio de clave
                            MessageBox.Show(stL_MensajeValidaUso, MENSAJE_5);
                            Application.DoEvents();
                            //
                            ObjL_Secur.Let_ShowFrmChPwd(ref blL_AceptoDatos, ref ObjPr_InfoBD);
                            if (blL_AceptoDatos)
                            {
                                blPr_AceptoInformacion = true;
                                // Cierra la forma
                                this.Hide();
                            }
                            //
                        }
                        else
                        {
                            if (blL_AceptoDatos)
                            {
                                blPr_AceptoInformacion = true;
                                // Cierra la forma
                                this.Hide();
                            }
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(2). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(2). Exception", "", ex.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private void HallaListaServidores()
        {
            // Halla la lista de los servidores de SQL Server.
            // URL de consulta : http://programandoenpuntonet.blogspot.com/2009/01/obtener-instancias-de-sql-server-y.html
            // Creamos una lista para que sea el origen de datos del combobox
            List<String> LstServidores = new List<String>();
            Boolean blL_EstadoCombo = false;
            Boolean blL_EstadoUsuario = false;
            Boolean blL_EstacoClave = false;
            //
            CLNBTN_Qy Query = null;
            CLNBTN_Sg ObjL_Secur = null;
            Boolean blL_Conectar = true;
            Boolean blL_Se_Conecto = false;
            Boolean blL_Definio_Servidor = false;
            //
            try
            {
                // Guarda los estados de los controles
                blL_EstadoCombo = cmbServidores.Enabled;
                blL_EstadoUsuario = TxtUsuario.Enabled;
                blL_EstacoClave = TxtClave.Enabled;
                // Los Coloca en false, mientras halla la lista de los servidores
                cmbServidores.Enabled = false;
                TxtUsuario.Enabled = false;
                TxtClave.Enabled = false;
                CmdCancelar.Enabled = false;
                //
                LblMensaje.Text = MENSAJE_23 + NEW_LINE + MENSAJE_24;
                Application.DoEvents();
                Application.DoEvents();
                //
                if (ObjPr_InfoBD.getDataBaseEngine_Type() == CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER)
                { // del if (ObjPr_InfoBD.getTipoBD() == CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER) 
                    //
                    if (ObjPr_InfoBD.get_DataBaseConn_Type() == CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT )
                    {
                        if (blPr_ServidorYaDefinido)
                        { // Inicio del if (blPr_ServidorYaDefinido)
                            // Asigna el Servior, que tiene la BD
                            if (ObjPr_InfoBD.getServerName().Length > 0)
                            {
                                LstServidores.Add(ObjPr_InfoBD.getServerName());
                                this.cmbServidores.DataSource = LstServidores;
                                //this.cmbServidores.Enabled = false;
                                blL_EstadoCombo = false;
                            }
                        } // fin del if (blPr_ServidorYaDefinido)
                        else // del if (blPr_ServidorYaDefinido)
                        { // Inicio del else if (blPr_ServidorYaDefinido)
                            // Prueba Conexion
                            LblMensaje.Text = MENSAJE_25 + NEW_LINE + MENSAJE_24;
                            Application.DoEvents();
                            // Asigna informacion
                            Query = new CLNBTN_Qy(_st_User, _st_FileLog, _st_Lic);
                            //
                            ObjPr_InfoBD.setServerName(cmbServidores.Text);
                            ObjL_Secur = new CLNBTN_Sg(_st_User, _st_FileLog, stPr_ArchivoConfigApp, stPr_Nombre_App, stPr_Version_App, stPr_NombreEmpresa_App, stPr_Archivo_InfoBds, stPr_NombreBd_XTrabajo, _st_Lic);
                            ObjL_Secur.Is_A_Valid_DB_Conn(ref blL_Definio_Servidor, ref blL_Conectar, ref blL_Se_Conecto, ref ObjPr_InfoBD, ObjPr_Conf, TxtUsuario.Text, TxtClave.Text, stPr_Seccion_BaseDeDatos);
                            //
                            if (blL_Definio_Servidor)
                            {
                                // Si definio el servidor lo coloca en la lista y deshabilita el combobox
                                // Asigna el Servior, que tiene la BD
                                LstServidores.Add(ObjPr_InfoBD.getServerName());
                                this.cmbServidores.DataSource = LstServidores;
                                //
                                blL_EstadoCombo = false;
                            }
                            else
                            {
                                // Prende bandera para ocultar la forma y salir de la forma del login.
                                blPr_EscondeForma = true;
                            }

                            //
                        } // Fin de if (blPr_ServidorYaDefinido)
                    }
                    else
                    {
                        SqlDataSourceEnumerator servidores;
                        System.Data.DataTable tablaServidores;
                        //String servidor;
                        //
                        servidores = SqlDataSourceEnumerator.Instance;
                        tablaServidores = new DataTable();
                        //
                        // Comprobamos que no se haya cargado ya el combobox
                        if (tablaServidores.Rows.Count == 0)
                        {
                            // Obtenemos un dataTable con la información sobre las instancias visibles
                            // de SQL Server 2000 y 2005
                            tablaServidores = servidores.GetDataSources();
                            // Recorremos el dataTable y añadimos un valor nuevo a la lista con cada fila
                            foreach (DataRow rowServidor in tablaServidores.Rows)
                            {
                                // La instancia de SQL Server puede tener nombre de instancia 
                                //o únicamente el nombre del servidor, comprobamos si hay 
                                //nombre de instancia para mostrarlo
                                if (String.IsNullOrEmpty(rowServidor["InstanceName"].ToString()))
                                    LstServidores.Add(rowServidor["ServerName"].ToString());
                                else
                                    LstServidores.Add(rowServidor["ServerName"] + "\\" + rowServidor["InstanceName"]);
                            }

                            // Asignamos al origen de datos del combobox la lista con 
                            // las instancias de servidores
                            if (LstServidores.Count == 0)
                            {
                                // Asigna el Servior, que tiene la BD
                                LstServidores.Add(ObjPr_InfoBD.getServerName());
                            }
                            this.cmbServidores.DataSource = LstServidores;
                        }
                        //
                        //SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                        //System.Data.DataTable table = instance.GetDataSources();
                        //
                        //foreach (System.Data.DataRow row in table.Rows)
                        //{
                        //    foreach (System.Data.DataColumn col in table.Columns)
                        //    {
                        //        Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                        //    }
                        //}
                        //
                    }
                } // del if (ObjPr_InfoBD.getTipoBD() == CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER)
                else // del if (ObjPr_InfoBD.getTipoBD() == CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER)
                { // del else del if (ObjPr_InfoBD.getTipoBD() == CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER)
                    // Para los otros tipos de servidores.
                    // Asigna el Servior, que tiene la BD
                    LstServidores.Add(ObjPr_InfoBD.getServerName());
                    // Asignamos la lista al combo 
                    this.cmbServidores.DataSource = LstServidores;
                } // del if (ObjPr_InfoBD.getTipoBD() == CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER)
                // Limpia mensaje y deja los controles con el estado que estaban originalmente
                LblMensaje.Text = "";
                //
                cmbServidores.Enabled = blL_EstadoCombo;
                TxtUsuario.Enabled = blL_EstadoUsuario;
                TxtClave.Enabled = blL_EstacoClave;
                //
                CmdCancelar.Enabled = true;
                //
                Application.DoEvents();
                Application.DoEvents();
                //
             }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(3). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(3). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void HabilitaBotones()
        {
            // Habilita los botones.
            try
            {
                //
                CmdAceptar.Enabled = false;
                //
                CmdCambiarClave.Enabled = false;
                //
                // Si los contenidos de los campos son diferentes a los textos originales.
                if (cmbServidores.Text != stPr_Servidor_Ori && TxtUsuario.Text != stPr_Usuario_Ori && TxtClave.Text != stPr_Clave_Ori)
                {
                    switch (ObjPr_InfoBD.get_DataBaseConn_Type())
                    { // Inicio del switch (ObjPr_InfoBD.getTipoConexion())
                        case CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN:
                            // Usuario Windows es el mismo usuario de la base de datos
                            if ((cmbServidores.Text.Length > 0 && TxtUsuario.Text.Length > 0)) 
                            {
                                CmdAceptar.Enabled = true;
                                //
                                CmdCambiarClave.Enabled = true;
                            }
                            break;
                        default:
                            if ((cmbServidores.Text.Length > 0 && TxtUsuario.Text.Length > 0 && TxtClave.Text.Length > 0)) 
                            {
                                CmdAceptar.Enabled = true;
                                //
                                CmdCambiarClave.Enabled = true;
                            }
                            break;
                    } // fin del switch (ObjPr_InfoBD.getTipoConexion())
                }
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(4). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(4). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void TxtServidor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(5). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(5). Exception", "", ex.Message.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(6). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(6). Exception", "", ex.Message.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void TxtClave_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(7). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(7). Exception", "", ex.Message.ToString());
            }
        }


         [HandleProcessCorruptedStateExceptions]
        private void cmbServidores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(8). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(8). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void cmbServidores_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(9). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(9). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void CmdCambiarClave_Click(object sender, EventArgs e)
        {
            CLNBTN_Qy Query = null;
            CLNBTN_Sg ObjL_Secur = null;
            Boolean blL_Conectar = true;
            Boolean blL_AceptoDatos = false;
            Boolean blL_Se_Conecto = false;
            String stL_MensajeValidaUso = "";
            Boolean blL_PideCambioClave = false;
            Boolean blL_Definio_Servidor = false;
            try
            {
                //
                LblMensaje.Text = "";
                inPr_NumIntentos = inPr_NumIntentos + 1;
                if (inPr_NumIntentos > MAX_INTENTOS)
                {
                    LblMensaje.Text = MENSAJE_3;
                }
                else
                {
                    // Asigna informacion
                    Query = new CLNBTN_Qy(_st_User, _st_FileLog, _st_Lic);
                    //
                    ObjPr_InfoBD.setServerName(cmbServidores.Text);
                    ObjL_Secur = new CLNBTN_Sg(_st_User, _st_FileLog, stPr_ArchivoConfigApp, stPr_Nombre_App, stPr_Version_App, stPr_NombreEmpresa_App, stPr_Archivo_InfoBds, stPr_NombreBd_XTrabajo, _st_Lic);
                    ObjL_Secur.Is_A_Valid_DB_Conn(ref blL_Definio_Servidor, ref blL_Conectar, ref blL_Se_Conecto, ref ObjPr_InfoBD, ObjPr_Conf, TxtUsuario.Text, TxtClave.Text, stPr_Seccion_BaseDeDatos);
                    //
                    if (blL_Definio_Servidor)
                    {
                        // Si definio el servidor lo coloca en la lista y deshabilita el combobox
                        List<String> LstServidores = new List<String>();
                        // Asigna el Servior, que tiene la BD
                        LstServidores.Add(ObjPr_InfoBD.getServerName());
                        this.cmbServidores.DataSource = LstServidores;
                        cmbServidores.Enabled = false;
                    }
                    //
                    // Hace la conexion
                    Query.setDataBaseInfo(ObjPr_InfoBD);
                    if (blL_Conectar)
                    { // del if (blL_Conectar)
                        Query.ConnectDataBase();
                        //
                        if (Query.getIs_Connected())
                        {
                            blL_Se_Conecto = true;
                        }
                    } // del if (blL_Conectar)
                    //
                    if (blL_Se_Conecto)
                    {
                        // Valida el usuario
                        ObjL_Secur = new CLNBTN_Sg(_st_User, _st_FileLog, stPr_ArchivoConfigApp, stPr_Nombre_App, stPr_Version_App, stPr_NombreEmpresa_App, stPr_Archivo_InfoBds, stPr_NombreBd_XTrabajo, _st_Lic);
                        //
                        ObjL_Secur.Is_A_Valid_User_Access(ref blL_AceptoDatos, ref stL_MensajeValidaUso, ref blL_PideCambioClave, ref ObjPr_InfoBD, true);
                        //
                        // Presenta el mensaje del usuario
                        LblMensaje.Text = stL_MensajeValidaUso;
                        //
                        if (blL_PideCambioClave)
                        {
                            // Presenta ventana para cambio de clave
                            //
                            ObjL_Secur.Let_ShowFrmChPwd(ref blL_AceptoDatos, ref ObjPr_InfoBD);
                            if (blL_AceptoDatos)
                            {
                                blPr_AceptoInformacion = true;
                                // Cierra la forma
                                this.Hide();
                            }
                        }
                        else
                        {
                            if (blL_AceptoDatos)
                            {
                                blPr_AceptoInformacion = true;
                                // Cierra la forma
                                this.Hide();
                            }
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(10). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmLogin(10). Exception", "", ex.Message.ToString());
            }
        }

        private void LblMensaje_Click(object sender, EventArgs e)
        {

        }
    }
}
