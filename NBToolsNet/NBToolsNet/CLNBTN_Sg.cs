using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.DirectoryServices;
using System.Management;
using System.Security.Principal;
using System.Data;
using System.Runtime.ExceptionServices;


namespace NBToolsNet
{
    public class CLNBTN_Sg
    {
        // Clase Equivalente : ClasX_Security
        //////////////////////////////////////////////////////////////////
        // Clase Para manejo de las operaciones de seguridad de las aplicaciones
        // 
        // Autor : Alvaro S. Quimbaya C.
        // Fecha : Oct 24 3 2012.
        // Empresa : Strail SAS
        //////////////////////////////////////////////////////////////////
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        private string _st_User = "CLNBTN_Sg";
        private string _st_FileLog = "C:\\Windows\\CLNBTN_Sg.log";
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Sg";
        //
        private String _st_ConfFile = ""; // Ruta y nombre del archivo de configuracion de la aplicacion.
        private String _st_InfDBFile = ""; // Ruta y nombde del archivo de informacion de las bases de datos, para hacer conexion.
        //
        private String _st_AppInfo_Name = ""; // Nombre de la aplicacion.
        private String _st_AppInfo_Ver = ""; // Version de la aplicacion.
        private String _st_AppInfo_Cia = ""; // Nombre de la empresa donde esta instalado la aplicacion.
        private String _st_DBName4Work = ""; // Nombre de la base de datos, de trabajo. Si viene definida, no la deja cambiar en la ventana donde pide la informacion del servidor, bd, usauario y clave.
        //
        private String _st_AppInfo_SgCod = ""; // Codigo de la aplicacion en el esquema de seguridad.
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


        public CLNBTN_Sg( String LicName)
        {
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Sg(1). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Sg(1). Exception", "", ex.Message.ToString());
            }
        }


        public CLNBTN_Sg(String UserName, String LogFile, String ConfFile, String AppInfo_Name, String AppInfo_Ver, String AppInfo_Cia, String InfBdFile, String DBName4Work, String LicName)
        {
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    _st_User = UserName;
                    _st_FileLog = LogFile;
                    _st_ConfFile = ConfFile;
                    _st_AppInfo_Name = AppInfo_Name;
                    _st_AppInfo_Ver = AppInfo_Ver;
                    _st_AppInfo_Cia = AppInfo_Cia;
                    _st_InfDBFile = InfBdFile;
                    _st_DBName4Work = DBName4Work;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Sg(2). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Sg(2). Exception", "", ex.Message.ToString());
            }
        }


        public CLNBTN_Sg(String UserName, String LogFile, String ConfFile, String AppInfo_Name, String AppInfo_Ver, String AppInfo_Cia, String InfBdFile, String DBName4Work, bool OutLineConsole, bool OutFileLog, bool OutWindow , String LicName)
        {
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    _st_User = UserName;
                    _st_FileLog = LogFile;
                    //
                    _bl_OutFileLog = OutFileLog;
                    _bl_OutLineConsole = OutLineConsole;
                    _bl_OutWindow = OutWindow;
                    //
                    _st_ConfFile = ConfFile;
                    _st_AppInfo_Name = AppInfo_Name;
                    _st_AppInfo_Ver = AppInfo_Ver;
                    _st_AppInfo_Cia = AppInfo_Cia;
                    _st_InfDBFile = InfBdFile;
                    _st_DBName4Work = DBName4Work;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Sg(3). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Sg(3). Exception", "", ex.Message.ToString());
            }
        }



        public CLNBTN_Sg(String UserName, String LogFile, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
        {
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    _st_User = UserName;
                    _st_FileLog = LogFile;
                    //
                    _bl_OutFileLog = OutFileLog;
                    _bl_OutLineConsole = OutLineConsole;
                    _bl_OutWindow = OutWindow;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Sg(4). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Sg(4). Exception", "", ex.Message.ToString());
            }
        }



        public bool getOutFileLog()
        {
            return _bl_OutFileLog;
        }


        public bool getOutLineConsole()
        {
            return _bl_OutLineConsole;
        }


        public bool getOutWindow()
        {
            return _bl_OutWindow;
        }

        public string getUser()
        {
            return _st_User;
        }

        public string getFileLog()
        {
            return _st_FileLog;
        }

        public string getConfFile()
        {
            return _st_ConfFile;
        }

        public string getInfDBFile()
        {
            return _st_InfDBFile;
        }

        public string getAppInfo_Name()
        {
            return _st_AppInfo_Name;
        }

        public string getAppInfo_Ver()
        {
            return _st_AppInfo_Ver;
        }

        public string getAppInfo_Cia()
        {
            return _st_AppInfo_Cia;
        }

        public string getDBName4Work()
        {
            return _st_DBName4Work;
        }

        public string getAppInfo_CodSeg()
        {
            /// Propiedad : getCodAPP_Seguridad_XConfig
            /// Halla el codigo de la aplicacion definido en el arcivo de configuracion de la aplicacion
            /// Lee del archivo de configuracion, definido en la variable privada stPr_ArchivoConfigApp
            /// </summary>
            /// <returns>stPr_CodAPP_Seguridad = Codigo de la aplicacion definido en el archivo de configuracion</returns>
            String stL_App = "";
            try
            {
                _st_AppInfo_SgCod = "";
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    CLNBTN_Cg ObjL_Config = new CLNBTN_Cg(_st_ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    stL_App = "";
                    stL_App = ObjL_Config.ReadAKeyFromSection(SECCION_ID_APP, "ID");
                    if (stL_App.Length > 0)
                    { // if (stL_Aux.Length > 0)
                        // Cambia la aplicacion
                        _st_AppInfo_SgCod = stL_App.Trim();
                    }
                }
                return _st_AppInfo_SgCod;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "getAppInfo_CodSeg. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "getAppInfo_CodSeg. Exception", "", ex.Message.ToString());
            }
            return _st_AppInfo_SgCod;
        }


        public void setOutFileLog(bool OutFileLog)
        {
            _bl_OutFileLog = OutFileLog;
        }

        public void setOutLineConsole(bool OutLineConsole)
        {
            _bl_OutLineConsole = OutLineConsole;
        }

        public void setOutWindow(bool blR_SalDialog)
        {
            _bl_OutWindow = blR_SalDialog;
        }

        public void setUser(string stR_User)
        {
            _st_User = stR_User;
        }

        public void setFileLog(string FileLog)
        {
            _st_FileLog = FileLog;
        }

        public void setConfFile(string Datum)
        {
            _st_ConfFile = Datum;
        }

        public void setInfDBFile(string Datum)
        {
            _st_InfDBFile = Datum;
        }

        public void setAppInfo_Name(string Datum)
        {
            _st_AppInfo_Name = Datum;
        }


        public void setAppInfo_Ver(string Datum)
        {
            _st_AppInfo_Ver = Datum;
        }

        public void setAppInfo_Cia(string Datum)
        {
            _st_AppInfo_Cia = Datum;
        }

        public void setDBName4Work(string Datum)
        {
            _st_DBName4Work = Datum;
        }

        public void setAppInfo_CodSeg(string Datum)
        {
            _st_AppInfo_SgCod = Datum;
        }



        [HandleProcessCorruptedStateExceptions]
        public void Let_Login(ref Boolean InfoIsAccepted, ref CLNBTN_IQy ObDbInfo, Boolean ShowMess2User_Authen = false)
        {
            /// Metodo : Login
            /// Hace el login del usuario en la aplicacion, ya sea SSO o via Validacion del usuario en una  base de datos
            /// Devuelve los paramtros asi:
            /// InfoIsAccepted = true, si se hizo login correctamente.
            /// ObDbInfo = Informacion actualizada de la base de datos.
            ///
            /// <param name="InfoIsAccepted">true, si se hizo login correctamente.</param>
            /// <param name="ObDbInfo">Informacion actualizada de la base de datos.</param>
            /// <param name="ShowMess2User_Authen">True = Presenta mensaje de error de autenticacion del usuario windows en el Directorio Activo del Dominio</param>
            // Valida el login del usuario, ya sea por SSO o Contra base de datos.
            // Crea instancia para la clase que maneja las configuraciones
            //
            Boolean blL_Validar_Acceso_SSO = false;
            Boolean blL_NO_Validar_LDAP = true;
            Boolean blL_Definio_Servidor = false;
            Boolean blL_Conectar = true;
            Boolean blL_Se_Conecto = false;
            // Indica si la validacion en el directorio activo fue exitosa o no
            Boolean blL_Validacion_En_DA = false;
            //
            String stL_Aux = "";
            String stL_Mensaje1 = "";
            CLNBTN_IQy.inDB_Types inL_TipoBD = 0;
            CLNBTN_IQy.inConnect_Type inL_TipoConexion = 0;
            //
            Boolean blL_AceptoDatos = false;
            String stL_MensajeValidaUso = "";
            Boolean blL_PideCambioClave = false;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    CLNBTN_Cg ObjL_Conf = new CLNBTN_Cg(_st_ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    // Lee del archivo de configuracion
                    // si debe ir por SSO
                    stL_Aux = "";
                    stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_ID_APP, "SSO");
                    if (stL_Aux.Length != 0)
                    {
                        if (stL_Aux.Trim() == "-1")
                        {
                            blL_Validar_Acceso_SSO = true;
                        }
                    }
                    // Mira si debe validar via LDAP
                    stL_Aux = "";
                    stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_ID_APP, "NoValida_LDAP");
                    if (stL_Aux.Length != 0)
                    {
                        if (stL_Aux.Trim() == "-1")
                        {
                            blL_NO_Validar_LDAP = true;
                        }
                    }
                    //////////////////////////////////////////////////////////////////////////
                    // Valida SSO via LDAP
                    //////////////////////////////////////////////////////////////////////////
                    if (blL_Validar_Acceso_SSO)
                    { // del if (blL_Validar_Acceso_SSO)
                        // Si no tiene que valiar en el directorio activo, prende la bandera
                        // que la validacion en el DA, fue exitosa.
                        if (blL_NO_Validar_LDAP)
                        {
                            blL_Validacion_En_DA = true;
                        }
                        else
                        {
                            // Valida en el directorio activo.
                            if (this.IsAnActiveDir(ShowMess2User_Authen))
                            { // del  if (this.InfDirectorioActivo(ShowMess2User_Authen))
                                // Si la validacion en el DA, fue exitosa prede bandera
                                // para indicar que fue exitosa la autenticacion
                                blL_Validacion_En_DA = true;
                            }
                        }
                        ////////////////////////////////////////////////////////////////
                        if (blL_Validacion_En_DA)
                        { // del if ( blL_Validacion_En_DA )
                            //
                            // La informacion de la base de datos para manejar la conexion con la base de datos.
                            ObDbInfo = new CLNBTN_IQy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
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
                            stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_BD_0, "Name");
                            if (stL_Aux.Length == 0)
                            {
                                stL_Mensaje1 = MENSAJE_1;
                            }
                            else
                            {
                                ObDbInfo.setDataBaseName(stL_Aux);
                            }
                            // tipo de Motor de Base de Datos
                            stL_Aux = "";
                            stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_BD_0, "Engine");
                            if (stL_Aux.Length == 0)
                            {
                                stL_Mensaje1 = MENSAJE_1;
                            }
                            else
                            {
                                inL_TipoBD = (CLNBTN_IQy.inDB_Types)(Convert.ToInt32(stL_Aux));
                                //
                                ObDbInfo.setDataBaseEngine_Type(inL_TipoBD);
                            }
                            // Tipo de Conexion
                            stL_Aux = "";
                            stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_BD_0, "Security");
                            if (stL_Aux.Length == 0)
                            {
                                stL_Mensaje1 = MENSAJE_1;
                            }
                            else
                            {
                                inL_TipoConexion = (CLNBTN_IQy.inConnect_Type)(Convert.ToInt32(stL_Aux));
                                //
                                ObDbInfo.setDataBaseConn_Type(inL_TipoConexion);
                            }
                            //
                            // Path de la base de datos.
                            stL_Aux = "";
                            stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_BD_0, "Path");
                            ObDbInfo.setDataBasePath(stL_Aux);
                            // URL 
                            stL_Aux = "";
                            stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_BD_0, "URL");
                            ObDbInfo.setServer_URL(stL_Aux);
                            // Nombre de Servidor 
                            stL_Aux = "";
                            stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_BD_0, "Server");
                            if (stL_Aux.Length == 0)
                            {
                                //stL_Mensaje1 = MENSAJE_1;
                            }
                            else
                            {
                                ObDbInfo.setServerName(stL_Aux);
                            }
                            // Ip Address
                            stL_Aux = "";
                            stL_Aux = ObjL_Conf.ReadAKeyFromSection(SECCION_BD_0, "IPAddress");
                            ObDbInfo.setServer_IP_Address(stL_Aux);
                            //////////////////////////////////////////////////////////////////////////////
                            // Valida la conexion con la base de datos
                            //////////////////////////////////////////////////////////////////////////////
                            switch (ObDbInfo.get_DataBaseConn_Type())
                            { // Inicio del switch (ObDbInfo.getTipoConexion())
                                // 
                                case CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT:
                                    // Conexion tipo Fenix.
                                    if (File.Exists(_st_InfDBFile))
                                    {
                                        CLNBTN_Cg ObDbInfoS_Fenix = new CLNBTN_Cg(_st_InfDBFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                                        CLNBTN_Es ObjL_Encrip = new CLNBTN_Es(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                                        // Halla y DesEncripta la informacion del servidor
                                        stL_Aux = "";
                                        stL_Aux = ObDbInfoS_Fenix.ReadAKeyFromSection(SECCION_BD_CONNECT_INFO, "ServerName");
                                        if (stL_Aux.Length != 0)
                                        {
                                            stL_Aux = ObjL_Encrip.File2Des(stL_Aux, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                            ObDbInfo.setServerName(stL_Aux);
                                        }
                                        // Halla y DesEncripta la informacion de la base de datos
                                        stL_Aux = "";
                                        stL_Aux = ObDbInfoS_Fenix.ReadAKeyFromSection(SECCION_BD_CONNECT_INFO, "DBName");
                                        if (stL_Aux.Length != 0)
                                        {
                                            stL_Aux = ObjL_Encrip.File2Des(stL_Aux, "","FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                            ObDbInfo.setDataBaseName(stL_Aux);
                                        }
                                        // Halla y DesEncripta la informacion del usuario
                                        stL_Aux = "";
                                        stL_Aux = ObDbInfoS_Fenix.ReadAKeyFromSection(SECCION_BD_CONNECT_INFO, "UID");
                                        if (stL_Aux.Length != 0)
                                        {
                                            stL_Aux = ObjL_Encrip.File2Des(stL_Aux, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                            ObDbInfo.setDataBase_UserID(stL_Aux);
                                        }
                                        // Halla y DesEncripta la informacion de la clave
                                        stL_Aux = "";
                                        stL_Aux = ObDbInfoS_Fenix.ReadAKeyFromSection(SECCION_BD_CONNECT_INFO, "PWDID");
                                        if (stL_Aux.Length != 0)
                                        {
                                            stL_Aux = ObjL_Encrip.File2Des(stL_Aux, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                            ObDbInfo.setDataBase_UserPWD(stL_Aux);
                                        }
                                    }
                                    break;
                                default:
                                    //
                                    break;
                            } // Fin de switch (ObDbInfo.getTipoConexion())
                            ///////////////////////////////////////////////////////////////////
                            // Valida la conexion con la base de datos
                            ///////////////////////////////////////////////////////////////////
                            this.Is_A_Valid_DB_Conn(ref blL_Definio_Servidor, ref blL_Conectar, ref blL_Se_Conecto, ref ObDbInfo, ObjL_Conf, "", "", SECCION_BD_0);
                            //
                            if (blL_Se_Conecto)
                            {
                                // Marca la clase de informacion de la base de datos, como :
                                // - Esta trabajando en esquema de SSO
                                ObDbInfo.setAccess_By_SSO(true);
                                // - Asigna a la propiedad set_UsuarioApp. el usuario de windows.
                                ObDbInfo.setUser(SystemInformation.UserName);
                                this.Is_A_Valid_User_Access(ref blL_AceptoDatos, ref stL_MensajeValidaUso, ref blL_PideCambioClave, ref ObDbInfo, false);
                                if (blL_AceptoDatos)
                                {
                                    InfoIsAccepted = true;
                                }
                                else
                                {
                                    // Presenta el mensaje.
                                    CLNBTN_Ul Obj_Util = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                                    Obj_Util.ShowMessage2User(MENSAJE_9, "", stL_MensajeValidaUso);
                                    Obj_Util = null;
                                }
                            }
                        } // del  if ( blL_Validacion_En_DA )
                        else // del  if ( blL_Validacion_En_DA )
                        { // del Else del  if ( blL_Validacion_En_DA )
                            ////////////////////////////////////////////////////////////////////////
                            // Valida via base de datos, con la segunda base de datos.
                            ////////////////////////////////////////////////////////////////////////
                            this.Let_ShowFrmLogin(ref InfoIsAccepted, ref ObDbInfo, SECCION_BD_1);
                        } // del else del if ( blL_Validacion_En_DA )
                    } // del if (blL_Validar_Acceso_SSO)
                    else // del if (blL_Validar_Acceso_SSO)
                    { // del else del if (blL_Validar_Acceso_SSO)
                        // Valida via base de datos.
                        this.Let_ShowFrmLogin(ref InfoIsAccepted, ref ObDbInfo);
                    } // del else del if (blL_Validar_Acceso_SSO)
                    //
                    //////////////////////////////////////////////////////
                    // Si se hizo acceso via SSO o Base de datos
                    //////////////////////////////////////////////////////
                    if (InfoIsAccepted)
                    {
                        this.RegAppObjts(ObDbInfo);
                        // Registra el Ingreso del usuario en la aplicacion.
                        this.Let_Write_UserAppAccess(ref ObDbInfo);
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Login. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Login. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void Let_ShowFrmLogin(ref Boolean InfoIsAccepted, ref CLNBTN_IQy ObDbInfo, String DBSectionName = "")
        {
            /// 
            /// Metodo : ShowFrmLogin
            /// Presenta la forma de login, cuando se debe validar el usuario por la base de datos.
            /// Devuelve estos parametros :
            /// blR_AceptoInfo = true, si se hizo login correctamente.
            /// Obj_BaseDeDatos = Informacion actualizada de la base de datos.
            /// </summary>
            /// <param name="blR_AceptoInfo">true, si se hizo login correctamente.</param>
            /// <param name="Obj_BaseDeDatos">Informacion actualizada de la base de datos.</param>
            // Presenta la ventana del login y se devuelve true o false
            // Dependiendo si ingreso o no correctamente el usuario.
            FrmLogin Forma = new FrmLogin();
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    Forma.GetParam(_st_Lic, MENSAJE_9, _st_User, _st_FileLog, _st_ConfFile, _st_AppInfo_Name, _st_AppInfo_Ver, _st_AppInfo_Cia, _st_InfDBFile, _st_DBName4Work, ref ObDbInfo, DBSectionName);
                    Forma.ShowDialog();
                    //
                    Forma.BringBackParam(ref InfoIsAccepted, ref ObDbInfo);
                    Forma.Close();
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Login. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Login. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void Let_ShowFrmExtDBInf(ref Boolean InfoIsAccepted, ref CLNBTN_IQy ObDbInfo)
        {
            /// 
            /// Metodo : ShowFrmInfoBdFenix
            /// Presenta la ventana para captura del servidor base de datos , usuario y clave
            /// para poer conectar a la base de datos. Estilo FEnix.
            ///  Devuelve estos parametros :
            /// blR_AceptoInfo = true, si se registro la informacion correctamente.
            /// Obj_BaseDeDatos = Informacion actualizada de la base de datos.
            /// </summary>
            /// <param name="blR_AceptoInfo">true, si se registro la informacion correctamente.</param>
            /// <param name="Obj_BaseDeDatos">Informacion actualizada de la base de datos.</param>
            // Presenta la ventana de captura de informacion de servidor, bd, usuatio y clave
            // y se devuelve true o false
            // Dependiendo si ingreso o no correctamente el usuario.
            //FrmInfoBdFenix Forma = new FrmInfoBdFenix();
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    //Forma.TomaParametros(ClasX_Constans.MENSAJE_10, stPr_UsuarioAPP, stPr_ArchivoLog, stPr_ArchivoConfigApp, stPr_Nombre_App, stPr_Version_App, stPr_NombreEmpresa_App, stPr_Archivo_InfoBds, stPr_NombreBd_XTrabajo, ref Obj_BaseDeDatos);
                    //Forma.ShowDialog();
                    ////
                    //Forma.DevuelveParametros(ref blR_AceptoInfo, ref Obj_BaseDeDatos);
                    //Forma.Close();
                    ////
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_ShowFrmExtDBInf. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_ShowFrmExtDBInf. Exception", "", ex.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void Let_ShowFrmChPwd(ref Boolean InfoIsAccepted, ref CLNBTN_IQy ObDbInfo)
        {
            /// 
            /// Metodo : ShowFrmCambiarClave
            /// Presenta la forma para el cambio de la clave del usuario.
            /// Devuelve estos parametros :
            /// blR_AceptoInfo = true, si se hizo el cambio de la clave correctamente.
            /// Obj_BaseDeDatos = Informacion actualizada de la base de datos.
            /// </summary>
            /// <param name="blR_AceptoInfo">true, si se hizo el cambio de la clave correctamente.</param>
            /// <param name="Obj_BaseDeDatos">Informacion actualizada de la base de datos.</param>
            // Presenta la ventana de captura de informacion de servidor, bd, usuatio y clave
            // y se devuelve true o false
            // Dependiendo si ingreso o no correctamente el usuario.
            FrmCambiarClave Forma = new FrmCambiarClave();
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    Forma.GetParam(_st_Lic, MENSAJE_17, _st_User, _st_FileLog, _st_ConfFile, _st_AppInfo_Name, _st_AppInfo_Ver, _st_AppInfo_Cia, ref ObDbInfo);
                    Forma.ShowDialog();
                    //
                    Forma.BringBackParam(ref InfoIsAccepted, ref ObDbInfo);
                    Forma.Close();
                    
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_ShowFrmChPwd. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_ShowFrmChPwd. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void Is_A_Valid_DB_Conn(ref Boolean ServerIsDefined, ref Boolean Have2Connect, ref Boolean GetConnected, ref CLNBTN_IQy ObDbInfo, CLNBTN_Cg ObConf, String UserID, String UserPWD, String DBSectionName)
        {
            /// 
            /// Metodo : ValidaConexionBD
            /// Encargado de validar y realizar la conexion con la base de datos
            /// Dependiendo del tipo de motor de la base de datos y el esquema de conexion
            /// Se llama, por ejemplo, en el acepta de la ventana del login.
            /// Devuelve estos parametros :
            /// ServerIsDefined = true si definio el servidor.
            /// Have2Connect    = true, si puede conectar desde donde se llama este metodo.
            /// GetConnected  = true, si se hizo login en la base de datos correctamente.
            /// ObDbInfo = Informacion actualizada de la base de datos.
            /// 
            /// </summary>
            /// <param name="ServerIsDefined"> true si definio el servidor.</param>
            /// <param name="Have2Connect"> true, si puede conectar desde donde se llama este metodo.</param>
            /// <param name="GetConnected">true, si se hizo login en la base de datos correctamente.</param>
            /// <param name="ObDbInfo">Informacion actualizada de la base de datos.</param>
            /// <param name="ObConf">Objeto de la clase de configuracion de la aplicacion</param>
            /// <param name="UserID">Codigo del usuario que se esta validando para hacer el login</param>
            /// <param name="UserPWD">La clave del usuario que se esta validando para hacer el login</param>
            // Valida la conexion con la base de datos.
            CLNBTN_Qy Query = null;
            CLNBTN_Es ObjL_Encrip = null;
            CLNBTN_Sg ObjL_Secur = null;
            //
            String stL_Encript = "";
            Boolean blL_AceptoDatos = false;
            // Arma el nombre de la seccion de donde debe leer la informacion de usuario y clave para 
            // la conexion a la base de datos.
            // Si la seccion de la base de datos es :
            // SYS_BD_ONE
            // Los datos para la conexion estan en la seccion
            // SYS_BD_ONE_DEMO
            String stL_Nombre_Seccion = DBSectionName + "_DEMO";
            //
            try
            {
                ServerIsDefined = false;
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    //
                    switch (ObDbInfo.get_DataBaseConn_Type())
                    { // Inicio del switch (ObDbInfo.getTipoConexion())
                        // 
                        case CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL:
                            // Usuario y clave de la bd, vienen en archivo de configuraciones
                            // Encripta la clave del usuario
                            ObjL_Encrip = new CLNBTN_Es(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                            //
                            stL_Encript = "";
                            stL_Encript = ObConf.ReadAKeyFromSection(stL_Nombre_Seccion, "F");
                            if (stL_Encript.Length == 0)
                            {
                                ObDbInfo.setDataBase_UserID(UserID);
                            }
                            else
                            {
                                stL_Encript = ObjL_Encrip.File2Des(stL_Encript, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                ObDbInfo.setDataBase_UserID(stL_Encript);
                            }
                            stL_Encript = "";
                            stL_Encript = ObConf.ReadAKeyFromSection(stL_Nombre_Seccion, "D");
                            if (stL_Encript.Length == 0)
                            {
                                ObDbInfo.setDataBase_UserPWD(UserPWD);
                            }
                            else
                            {
                                stL_Encript = ObjL_Encrip.File2Des(stL_Encript, "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                ObDbInfo.setDataBase_UserPWD(stL_Encript);
                            }
                            // Cambia el usuario de la aplicacion, el que se registra en el campo de Usuario
                            ObDbInfo.setUser(UserID);
                            ObDbInfo.setUserApp_PWD(UserPWD);
                            //
                            break;
                        case CLNBTN_IQy.inConnect_Type.TYPE_2_CONNECT_USER_APP:
                            // Usuario de la aplicacion, mismo usuario de la base de datos
                            ObDbInfo.setDataBase_UserID(UserID);
                            // Encripta la clave del usuario
                            ObjL_Encrip = new CLNBTN_Es(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                            //
                            stL_Encript = ObjL_Encrip.File2Des(Convert.ToString(UserPWD), "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                            ObDbInfo.setDataBase_UserPWD(stL_Encript);
                            // Cambia el usuario de la aplicacion, el que se registra en el campo de Usuario
                            ObDbInfo.setUser(UserID);
                            ObDbInfo.setUserApp_PWD(UserPWD);
                            //
                            break;
                        case CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN:
                            // Usuario Windows es el mismo usuario de la base de datos
                            ObDbInfo.setDataBase_UserID(UserID);
                            ObDbInfo.setDataBase_UserPWD(UserPWD);
                            // Cambia el usuario de la aplicacion, el que se registra en el campo de Usuario
                            ObDbInfo.setUser(UserID);
                            ObDbInfo.setUserApp_PWD("");
                            //
                            break;
                        case CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT:
                            // Conexion tipo Fenix.
                            if (File.Exists(_st_InfDBFile))
                            {
                                // Hace la conexion
                                Query.setDataBaseInfo(ObDbInfo);
                                Query.ConnectDataBase();
                                // Si no conecto presenta ventana para capturar informacion de servidor, usuario y clave.
                                if (!Query.getIs_Connected())
                                {
                                    //
                                    MessageBox.Show(MENSAJE_4, MENSAJE_5);
                                    Application.DoEvents();
                                    //
                                    ObjL_Secur = new CLNBTN_Sg(_st_User, _st_FileLog, _st_ConfFile, _st_AppInfo_Name, _st_AppInfo_Ver, _st_AppInfo_Cia, _st_InfDBFile, _st_DBName4Work, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                                    ObjL_Secur.Let_ShowFrmExtDBInf(ref blL_AceptoDatos, ref ObDbInfo);
                                    if (blL_AceptoDatos)
                                    {
                                        ServerIsDefined = true;
                                        Have2Connect = false;
                                        GetConnected = true;
                                    }
                                    else
                                    {
                                        Have2Connect = false;
                                        GetConnected = false;
                                    }
                                }
                                else
                                {
                                    Have2Connect = true;
                                    GetConnected = true;
                                }
                            }
                            else
                            {
                                // Llama ventana para capturar datos del servidor, usuario y clave.
                                //
                                MessageBox.Show(MENSAJE_4, MENSAJE_5);
                                Application.DoEvents();
                                //
                                ObjL_Secur = new CLNBTN_Sg(_st_User, _st_FileLog, _st_ConfFile, _st_AppInfo_Name, _st_AppInfo_Ver, _st_AppInfo_Cia, _st_InfDBFile, _st_DBName4Work, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                                ObjL_Secur.Let_ShowFrmExtDBInf(ref blL_AceptoDatos, ref ObDbInfo);
                                if (blL_AceptoDatos)
                                {
                                    ServerIsDefined = true;
                                    Have2Connect = false;
                                    GetConnected = true;
                                }
                                else
                                {
                                    Have2Connect = false;
                                    GetConnected = false;
                                }
                            }
                            // Cambia el usuario de la aplicacion, el que se registra en el campo de Usuario
                            ObDbInfo.setUser(UserID);
                            ObDbInfo.setUserApp_PWD(UserPWD);
                            //
                            break;
                        default:
                            //
                            break;
                    } // Fin de switch (ObDbInfo.getTipoConexion())
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_DB_Conn. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_DB_Conn. Exception", "", ex.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void Is_A_Valid_User_Access(ref Boolean InfoIsAccepted, ref String Mess2User, ref Boolean ChgPWDIsDone, ref CLNBTN_IQy ObDbInfo, Boolean CalledFromChgPwd, Boolean LoginTypeExt = false)
        {
            /// 
            /// Metodo : Is_A_Valid_User_Access
            /// Encargado de validar el acceso del usuario a la aplicacion
            /// validando el usuario contra las tablas de la base de datos
            /// donde esta la informacion de los usuarios validos para el sistema.
            /// Devuelve estos parametros :
            /// InfoIsAccepted      = true, Si la informacion del usuario esta correcta.
            /// Mess2User  = Mensaje que indica que error se presento, durante la validacion
            /// ChgPWDIsDone = true, si se debe llamar la ventana de cambio de clave.
            /// ObDbInfo = Informacion actualizada de la base de datos.
            /// </summary>
            /// <param name="InfoIsAccepted">true, Si la informacion del usuario esta correcta.</param>
            /// <param name="Mess2User">Mensaje que indica que error se presento, durante la validacion</param>
            /// <param name="ChgPWDIsDone">true, si se debe llamar la ventana de cambio de clave.</param>
            /// <param name="ObDbInfo">Informacion actualizada de la base de datos.</param>
            /// <param name="CalledFromChgPwd">true = Indica si se esta llamando desde una opcion o un boton de cambio de clave</param>
            /// <param name="LoginTypeExt">True = Indica que es llamado desde el login de aplicaciones tipo fenix, y no valida la fecha de la clave</param>
            // Valida el acceso del usuario, usuario de la aplicacion.
            Boolean blL_UsuarioOk = false;
            String stL_MensajeSalida = "";
            String stL_ClaveUsuario = "";
            String stL_EstadoUsuario = "";
            Boolean blL_DebeValidarClave = true;
            String stL_ClaveEncriptada = "";
            String stL_FechaServidor = "";
            String stL_FechaClave = "";
            Int32 inL_DiasClave = 0;
            //
            CLNBTN_Qy Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
            CLNBTN_Ul Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
            CLNBTN_Es Encrip = new CLNBTN_Es(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
            //
            CLNBTN_Lg objL_Log_1 = new CLNBTN_Lg(_st_User, _st_FileLog, false, _bl_OutFileLog, false);
            // Define DataTable, para los Datos del Query
            DataTable DatUsuarios = null;
            //
            try
            {
                ChgPWDIsDone = false;
                //
                if (_st_Lic.Length == 0)
                {
                    InfoIsAccepted = false;
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    Query.setDataBaseInfo(ObDbInfo);
                    //
                    stL_FechaServidor = Utils.BringMeServerDate(ObDbInfo);
                    //
                    objL_Log_1.WriteTextInLog(_st_Relac + "."+ _st_RelacSon + "." + "Is_A_Valid_User_Access. Before Select");
                    Query.ToDo_SELECT("*");
                    Query.ToDo_FROM("t00usuarios");
                    Query.ToDo_WHERE("A00USUARIOWIN", "'" + ObDbInfo.getUser() + "'");
                    Query.ToDo_EXECUTE_SQL(ref DatUsuarios);
                    Query.ToDo_CLOSE();
                    //
                    if (DatUsuarios != null)
                    { // del if (DatUsuarios != null)
                        if (!(DatUsuarios.Rows.Count > 0))
                        { // El usuario no esta registrado en el sistema
                            stL_MensajeSalida = MENSAJE_11 + " " + ObDbInfo.getUser() + NEW_LINE + MENSAJE_12;
                        }
                        else
                        {
                            objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "Is_A_Valid_User_Access. Before int inL_Row = 0");
                            for (int inL_Row = 0; inL_Row < DatUsuarios.Rows.Count; inL_Row++)
                            { // inicio del  for (int inL_Row = 0; inL_Row < DatUsuarios.Rows.Count; inL_Row++)
                                // Toma la informacion de la fila
                                DataRow Info_Fila = DatUsuarios.Rows[inL_Row];
                                //
                                // Si esta entrando via SSO, no valida demas condiciones del usuario.
                                if (ObDbInfo.getAccess_By_SSO())
                                {
                                    objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "Is_A_Valid_User_Access. Before Getting User Status");
                                    // Valida el Estado del Usuario.
                                    stL_EstadoUsuario = "";
                                    //-->>if (!DBNull.Value.Equals(Info_Fila["A00ESTADO"].ToString())) stL_EstadoUsuario = Info_Fila["A00ESTADO"].ToString();
                                    //
                                    if ( Info_Fila["A00ESTADO"].ToString().Length > 0 ) 
                                    {
                                        stL_EstadoUsuario = Info_Fila["A00ESTADO"].ToString();
                                    }
                                    //
                                    objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "Is_A_Valid_User_Access. User Status :" + stL_EstadoUsuario);
                                    if (stL_EstadoUsuario.Equals(Convert.ToString(ESTADO_NO_ACTIVO)))
                                    {
                                        // el usuario no esta activo
                                        stL_MensajeSalida = MENSAJE_11 + " " + ObDbInfo.getUser() + NEW_LINE + MENSAJE_13;
                                    }
                                    else
                                    {
                                        blL_UsuarioOk = true;
                                        objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "Is_A_Valid_User_Access. User Status True");
                                    }
                                }
                                else
                                {
                                    stL_EstadoUsuario = "";
                                    //-->>if (!DBNull.Value.Equals(Info_Fila["A00ESTADO"].ToString())) stL_EstadoUsuario = Info_Fila["A00ESTADO"].ToString();
                                    if (Info_Fila["A00ESTADO"].ToString().Length > 0)
                                    {
                                        stL_EstadoUsuario = Info_Fila["A00ESTADO"].ToString();
                                    }
                                    if (stL_EstadoUsuario.Equals(Convert.ToString(ESTADO_NO_ACTIVO)))
                                    {
                                        // el usuario no esta activo
                                        stL_MensajeSalida = MENSAJE_11 + " " + ObDbInfo.getUser() + NEW_LINE + MENSAJE_13;
                                    }
                                    else
                                    {
                                        // Valida que tenga Clave Asignada el usuario.
                                        stL_ClaveUsuario = "";
                                        //-->>if (!DBNull.Value.Equals(Info_Fila["A00CLAVE"].ToString())) stL_ClaveUsuario = Info_Fila["A00CLAVE"].ToString();
                                        if (Info_Fila["A00CLAVE"].ToString().Length > 0)
                                        {
                                            stL_ClaveUsuario = Info_Fila["A00CLAVE"].ToString();
                                        }
                                        ////////////////////////////////////////////////////////////////////////////////////////
                                        // Esto se hizo para MySQL, por que la clave encriptada por ejemplo es :
                                        // 'y8P1n2MeVdd42/JMlAg5UQ==' 
                                        // Y en la base de datos queda grabada asi:
                                        // 'y8P1n2MeVdd42/JMlAg5UQ = ='
                                        // Al leerla viene con los espacios, y se le quitan
                                        // para que concuerde con el encriptado.
                                        // Encoding.ASCII.GetBytes(stL_ClaveUsuario.Substring( 24, 1))[0] = 32
                                        char c = (char)32;
                                        // Quita espacios adicionales
                                        stL_ClaveUsuario = Utils.GetOff_SpacesAdic(stL_ClaveUsuario);
                                        stL_ClaveUsuario = Utils.GetOff_CharFromString(stL_ClaveUsuario, Convert.ToString(c));
                                        ////////////////////////////////////////////////////////////////////////////////////////
                                        //
                                        stL_FechaClave = "";
                                        //-->>if (!DBNull.Value.Equals(Info_Fila["A00FECHA_CLAVE"].ToString())) stL_FechaClave = Info_Fila["A00FECHA_CLAVE"].ToString();
                                        if (Info_Fila["A00FECHA_CLAVE"].ToString().Length > 0)
                                        {
                                            stL_FechaClave = Info_Fila["A00FECHA_CLAVE"].ToString();
                                        }
                                        //
                                        inL_DiasClave = 0;
                                        //if (!DBNull.Value.Equals(Info_Fila["A00DIAS_CLAVE"].ToString()))
                                        //{
                                        //    if (Info_Fila["A00DIAS_CLAVE"].ToString().Length > 0) inL_DiasClave = Convert.ToInt16(Info_Fila["A00DIAS_CLAVE"].ToString());
                                        //}
                                        if (Info_Fila["A00DIAS_CLAVE"].ToString().Length > 0)
                                        {
                                            inL_DiasClave = Convert.ToInt16(Info_Fila["A00DIAS_CLAVE"].ToString());
                                        }
                                        //
                                        if ((ObDbInfo.getDataBaseEngine_Type() == CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL) && (ObDbInfo.get_DataBaseConn_Type() == CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN))
                                        {
                                            blL_DebeValidarClave = false;
                                        }
                                        // Si tiene que validar la clave
                                        if (blL_DebeValidarClave)
                                        { // del if (blL_DebeValidarClave)
                                            if (stL_ClaveUsuario.Length == 0)
                                            {
                                                stL_MensajeSalida = MENSAJE_11 + " " + ObDbInfo.getUser() + NEW_LINE + MENSAJE_14;
                                                ChgPWDIsDone = true;
                                                // Limpia la informacion de la clave
                                                ObDbInfo.setUser("");
                                                ObDbInfo.setUserApp_PWD_Enc("");
                                            }
                                            else
                                            {
                                                // Si tiene que leer y validar la clave lo hace.
                                                // Encripta la clave que viene de la ventana
                                                stL_ClaveEncriptada = Encrip.File2Es(ObDbInfo.getUserApp_PWD(), "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                                if (stL_ClaveUsuario != stL_ClaveEncriptada)
                                                {
                                                    stL_MensajeSalida = MENSAJE_15;
                                                }
                                                else
                                                {
                                                    // Valida los dias de la clave
                                                    if (stL_FechaClave.Length == 0)
                                                    {
                                                        // Si viene desde el logiin de Fenix, no hace esta validacion.
                                                        if (LoginTypeExt)
                                                        {
                                                            blL_UsuarioOk = true;
                                                        }
                                                        else
                                                        {
                                                            stL_MensajeSalida = MENSAJE_16;
                                                            ChgPWDIsDone = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // Si viene desde el logiin de Fenix, no hace esta validacion.
                                                        if (LoginTypeExt)
                                                        {
                                                            blL_UsuarioOk = true;
                                                        }
                                                        else // del if (LoginTypeExt)
                                                        { // inicio del else del if (LoginTypeExt)
                                                            // Si los dias son cero, nunca pide cambio de clave.
                                                            if (inL_DiasClave == 0)
                                                            {
                                                                blL_UsuarioOk = true;
                                                            }
                                                            else // del if (inL_DiasClave == 0)
                                                            { // Inicio del else del if (inL_DiasClave == 0)
                                                                if (Utils.BringMe_DifDates(stL_FechaServidor, stL_FechaClave, "D") >= inL_DiasClave)
                                                                {
                                                                    stL_MensajeSalida = MENSAJE_16;
                                                                    ChgPWDIsDone = true;
                                                                }
                                                                else
                                                                {
                                                                    blL_UsuarioOk = true;
                                                                }
                                                            } // fin del else del if (inL_DiasClave == 0)
                                                        } // fin del else del if (LoginTypeExt)
                                                    }
                                                }
                                            }
                                        } // del if (blL_DebeValidarClave)
                                        else // del if (blL_DebeValidarClave)
                                        { // del else del if (blL_DebeValidarClave)
                                            blL_UsuarioOk = true;
                                        } // del else if (blL_DebeValidarClave)
                                    }
                                } // del else del if (ObDbInfo.getAcceso_SSO())
                            } // Fin del  for (int inL_Row = 0; inL_Row < DatUsuarios.Rows.Count; inL_Row++)
                        }
                    } // fin del if (DatUsuarios != null)
                    // --------------------------------
                    if (blL_UsuarioOk)
                    {
                        // Si esta siendo llamada desde el boton de cambio de clave
                        // envia la bandera de cambiar clave en true
                        if (CalledFromChgPwd)
                        {
                            ChgPWDIsDone = true;
                        }
                        InfoIsAccepted = true;
                        // Lee los grupos del usuario
                    }
                    Mess2User = stL_MensajeSalida;
                    objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "Is_A_Valid_User_Access. Before Exit. InfoIsAccepted is : " + InfoIsAccepted.ToString());
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_User_Access. System.AccessViolationException", "", ex_0.Message.ToString());
                if (DatUsuarios != null)
                {
                    if (Query != null)
                    {
                        Query.ToDo_CLOSE();
                    }
                }
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_User_Access. Exception", "", ex.Message.ToString());
                if (DatUsuarios != null)
                {
                    if (Query != null)
                    {
                        Query.ToDo_CLOSE();
                    }
                }
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void BringMe_ObjPermission(String AppName, String ObjName, CLNBTN_IQy ObDbInfo, ref Boolean CanRead, ref Boolean CanWrite, ref Boolean CanDelete, ref Boolean CanDisplay)
        {
            /// 
            /// Metodo : HallaPermisos_Objeto.
            /// Sobre Carga Uno.
            /// Encargado de hallar los permisos de :
            /// Read, Write, Delete y Display
            /// sobre un objeto de una aplicacion
            /// para los grupos del usuario de la aplicacion
            /// Devuelve los parametros :
            /// blR_Read = true, tiene permiso de lectura
            /// blR_Write = true tiene permiso de escritura
            /// blR_Delete = true tiene permiso de eliminacion
            /// blR_Display = true tiene permiso de despliegue
            /// Llama al metodo privado LeePermisosObjeto, para validar los permisos
            /// </summary>
            /// <param name="st_Aplicacion">Codigo de la Aplicacion, por ejemplo FENIX30</param>
            /// <param name="st_Objeto">Codigo del Objeto, por ejemplo FICHA_PERSONAL</param>
            /// <param name="ObDbInfo">Informacion de la base de datos</param>
            /// <param name="blR_Read">true, tiene permiso de lectura</param>
            /// <param name="blR_Write">true tiene permiso de escritura</param>
            /// <param name="blR_Delete"> true tiene permiso de eliminacion</param>
            /// <param name="blR_Display">true tiene permiso de despliegue</param>
            // Halla los permisos de un objeto de una aplicacion
            Boolean blL_PermisosObjeto = false;
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    this.Read_ObjPermission(AppName, ObjName, ObDbInfo, ref CanRead, ref CanWrite, ref CanDelete, ref CanDisplay, "", ref blL_PermisosObjeto);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ObjPermission(1). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ObjPermission(1). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void BringMe_ObjPermission(String AppName, String ObjName, CLNBTN_IQy ObDbInfo, String Type2Validate, ref Boolean TypeObjOperation)
        {
            /// 
            /// Metodo : HallaPermisos_Objeto.
            /// Sobre Carga Dos.
            /// Encargado de hallar el tipo de Permiso ya sea  :
            /// Read, Write, Delete y Display
            /// Dependiendo del tipo de permiso, definido en el parametro:
            /// Type2Validate = "R" = Leer
            /// Type2Validate = "W" = Escribir
            /// Type2Validate = "D" = Eliminar
            /// Type2Validate = "Y" = Display, Desplegar
            /// sobre un objeto de una aplicacion
            /// para los grupos del usuario de la aplicacion
            /// Devuelve los parametros :
            /// TypeObjOperation = true, si se tiene el permiso asignado, depeniendo del tipo de permiso.
            /// Llama al metodo privado LeePermisosObjeto, para validar los permisos
            /// </summary>
            /// <param name="AppName">Codigo de la Aplicacion, por ejemplo FENIX30</param>
            /// <param name="ObjName">Codigo del Objeto, por ejemplo FICHA_PERSONAL</param>
            /// <param name="ObDbInfo">Informacion de la base de datos</param>
            /// <param name="Type2Validate">Tipo de Permiso a validar</param>
            /// <param name="TypeObjOperation">true, si se tiene el permiso asignado, depeniendo del tipo de permiso.</param>
            // Halla los permisos de un objeto de una aplicacion
            Boolean blR_Read = false;
            Boolean blR_Write = false;
            Boolean blR_Delete = false;
            Boolean blR_Display = false;
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    this.Read_ObjPermission(AppName, ObjName, ObDbInfo, ref blR_Read, ref blR_Write, ref blR_Delete, ref blR_Display, Type2Validate, ref TypeObjOperation);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ObjPermission(2). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ObjPermission(2). Exception", "", ex.Message.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void Read_ObjPermission(String AppName, String ObjName, CLNBTN_IQy ObDbInfo, ref Boolean CanRead, ref Boolean CanWrite, ref Boolean CanDelete, ref Boolean CanDisplay, String Type2Validate, ref Boolean TypeObjOperation)
        {
            /// 
            /// Metodo Privado : LeePermisosObjeto
            /// Encargado de validar los permisos de un objeto de una aplicacion.
            /// Si el parametro :
            /// Type2Validate es vacio halla los permisos:
            ///  Devuelve los parametros :
            /// CanRead = true, tiene permiso de lectura
            /// CanWrite = true tiene permiso de escritura
            /// CanDelete = true tiene permiso de eliminacion
            /// CanDisplay = true tiene permiso de despliegue
            /// Si el parametro:
            /// Type2Validate, no es vacio valida el permiso, dependiendo si es read, write, delete o display
            /// Dependiendo del tipo de permiso, definido en el parametro:
            /// Type2Validate = "R" = Leer
            /// Type2Validate = "W" = Escribir
            /// Type2Validate = "D" = Eliminar
            /// Type2Validate = "Y" = Display, Desplegar
            /// sobre un objeto de una aplicacion
            /// para los grupos del usuario de la aplicacion
            /// Devuelve los parametros :
            /// TypeObjOperation = true, si se tiene el permiso asignado, depeniendo del tipo de permiso.
            /// 
            /// </summary>
            /// <param name="AppName">Codigo de la Aplicacion, por ejemplo FENIX30</param>
            /// <param name="ObjName">Codigo del Objeto, por ejemplo FICHA_PERSONAL</param>
            /// <param name="ObDbInfo">Informacion de la base de datos</param>
            /// <param name="CanRead">true, tiene permiso de lectura</param>
            /// <param name="CanWrite">true tiene permiso de escritura</param>
            /// <param name="CanDelete"> true tiene permiso de eliminacion</param>
            /// <param name="CanDisplay">true tiene permiso de despliegue</param>
            /// <param name="Type2Validate">Tipo de Permiso a validar</param>
            /// <param name="TypeObjOperation">true, si se tiene el permiso asignado, depeniendo del tipo de permiso.</param>
            CLNBTN_Qy Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
            CLNBTN_Ul Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
            String stL_Sql = "";
            String stL_Aux = "";
            try
            {
                //
                CanRead = false;
                CanWrite = false;
                CanDelete = false;
                CanDisplay = false;
                TypeObjOperation = false;
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    Query.setDataBaseInfo(ObDbInfo);
                    //
                    // Selecciona de la tabla de permisos
                    stL_Sql = " * FROM t05app_objt_permission WHERE ";
                    // Para los registros de la aplicacion que entra como parametro
                    stL_Sql += " A05IDAPP IN ( SELECT DISTINCT A03IDAPP FROM t03apps WHERE A03CODIGO_APP = '" + AppName.Trim() + "' ) ";
                    // Para el Objeto que entra como parametro
                    stL_Sql += " AND A05IDOBJETO IN ( SELECT DISTINCT A04IDOBJETO FROM t04app_objts , t03apps WHERE A04IDAPP = A03IDAPP ";
                    stL_Sql += " AND A04OBJETO_APP = '" + ObjName + "' ";
                    stL_Sql += " AND A03CODIGO_APP = '" + AppName + "' ) ";
                    // PAra los grupos en los cuales esta el usuario.
                    stL_Sql += " AND A05IDGRUPO IN ( SELECT DISTINCT A02IDGRUPO FROM t02usuario_grupos , t00usuarios ";
                    stL_Sql += " WHERE A02IDUSUARIO = A00IDUSUARIO ";
                    stL_Sql += " AND A00USUARIOWIN = '" + ObDbInfo.getUser() + "' )  ";
                    //
                    // Define DataTable, para los Datos del Query
                    DataTable DatObjetos = null;
                    //
                    Query.ToDo_SELECT(stL_Sql);
                    Query.ToDo_EXECUTE_SQL(ref DatObjetos);
                    Query.ToDo_CLOSE();
                    //
                    if (DatObjetos != null)
                    { // del if (DatObjetos != null)
                        for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                        { // del for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                            // Importante : Como esta dentro del LOOP, entonces recorre el recordset, y si la variable para el permiso esta en FALSE
                            // Verifica si se tiene otorgado el permiso. Con esto si el usuario pertenece a mas de un grupo
                            // Se valida en todos los grupos, para saber cual tiene el tipo de permiso otorgado
                            //
                            // Toma la informacion de la fila
                            DataRow Info_Fila = DatObjetos.Rows[inL_Row];
                            //
                            //do
                            //{
                            if (Type2Validate.Length == 0)
                            { // if (Type2Validate.Length == 0)
                                if (!CanRead)
                                {
                                    stL_Aux = "";
                                    //-->>if (!DBNull.Value.Equals(Info_Fila["A05READ"].ToString())) stL_Aux = Info_Fila["A05READ"].ToString();
                                    if (Info_Fila["A05READ"].ToString().Length > 0) stL_Aux = Info_Fila["A05READ"].ToString();
                                    //
                                    CanRead = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                                }
                                if (!CanWrite)
                                {
                                    stL_Aux = "";
                                    //-->>if (!DBNull.Value.Equals(Info_Fila["A05WRITE"].ToString())) stL_Aux = Info_Fila["A05WRITE"].ToString();
                                    if (Info_Fila["A05WRITE"].ToString().Length > 0) stL_Aux = Info_Fila["A05WRITE"].ToString();
                                    CanWrite = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                                }
                                if (!CanDelete)
                                {
                                    stL_Aux = "";
                                    //-->>if (!DBNull.Value.Equals(Info_Fila["A05DELETE"].ToString())) stL_Aux = Info_Fila["A05DELETE"].ToString();
                                    if (Info_Fila["A05DELETE"].ToString().Length > 0) stL_Aux = Info_Fila["A05DELETE"].ToString();
                                    CanDelete = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                                }
                                if (!CanDisplay)
                                {
                                    stL_Aux = "";
                                    //-->>if (!DBNull.Value.Equals(Info_Fila["A05DISPLAY"].ToString())) stL_Aux = Info_Fila["A05DISPLAY"].ToString();
                                    if (Info_Fila["A05DISPLAY"].ToString().Length > 0) stL_Aux = Info_Fila["A05DISPLAY"].ToString();
                                    CanDisplay = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                                }
                            } // fin del if (Type2Validate.Length == 0)
                            else // del if (Type2Validate.Length == 0)
                            { // Inicio del else del if (Type2Validate.Length == 0)
                                switch (Type2Validate)
                                { // inicio del switch (Type2Validate)
                                    case PERMISO_READ:
                                        //
                                        if (!TypeObjOperation)
                                        {
                                            stL_Aux = "";
                                            //-->>if (!DBNull.Value.Equals(Info_Fila["A05READ"].ToString())) stL_Aux = Info_Fila["A05READ"].ToString();
                                            if (Info_Fila["A05READ"].ToString().Length > 0) stL_Aux = Info_Fila["A05READ"].ToString();
                                            TypeObjOperation = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                                        }
                                        break;
                                    case PERMISO_WRITE:
                                        //
                                        if (!TypeObjOperation)
                                        {
                                            stL_Aux = "";
                                            //-->>if (!DBNull.Value.Equals(Info_Fila["A05WRITE"].ToString())) stL_Aux = Info_Fila["A05WRITE"].ToString();
                                            if (Info_Fila["A05WRITE"].ToString().Length > 0) stL_Aux = Info_Fila["A05WRITE"].ToString();
                                            TypeObjOperation = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                                        }
                                        break;
                                    case PERMISO_DELETE:
                                        //
                                        if (!TypeObjOperation)
                                        {
                                            stL_Aux = "";
                                            //-->>if (!DBNull.Value.Equals(Info_Fila["A05DELETE"].ToString())) stL_Aux = Info_Fila["A05DELETE"].ToString();
                                            if (Info_Fila["A05DELETE"].ToString().Length > 0) stL_Aux = Info_Fila["A05DELETE"].ToString();
                                            TypeObjOperation = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                                        }
                                        break;
                                    case PERMISO_DISPLAY:
                                        //
                                        if (!TypeObjOperation)
                                        {
                                            stL_Aux = "";
                                            //-->>if (!DBNull.Value.Equals(Info_Fila["A05DISPLAY"].ToString())) stL_Aux = Info_Fila["A05DISPLAY"].ToString();
                                            if (Info_Fila["A05DISPLAY"].ToString().Length > 0) stL_Aux = Info_Fila["A05DISPLAY"].ToString();
                                            TypeObjOperation = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                                        }
                                        break;
                                    default:
                                        //
                                        TypeObjOperation = false;
                                        break;
                                } // del switch (Type2Validate)
                            } // Fin del Else del if (Type2Validate.Length == 0)
                            //}
                            //
                        } // del for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                    } // del if (DatObjetos != null)
                    //-->>Query.ToDo_CLOSE();
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Read_ObjPermission. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Read_ObjPermission. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void RegAppObjts(CLNBTN_IQy ObDbInfo)
        {
            /// 
            /// Metodo : RegistraObjetosAplicacion
            /// Graba la infomacion o registra los datos de la aplicacion y los objetos que maneja
            /// en la base de datos
            /// Actualiza la tablas, t03apps y t04app_objts
            /// Con base en el archivo .Conf de la aplicacion, definido en la variable local :
            /// stPr_ArchivoConfigApp
            /// </summary>
            /// <param name="ObDbInfo">Informacion de la base de datos</param>
            // Registra los objetos de la aplicacion, con base en el archivo de configuracion
            // de la aplicacion
            // los objetos estan definidos en la seccion
            CLNBTN_Cg ObjL_Config = new CLNBTN_Cg(_st_ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
            CLNBTN_Qy Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
            CLNBTN_Ul Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
            String stL_App = "";
            String stL_Aux = "";
            String stL_Aux1 = "";
            Boolean blL_Existe = false;
            decimal lnL_Id_App = 0;
            String[] stL_Objetos = new String[50];
            String[] stL_Desc_Objetos = new String[50];
            Boolean[] bL_Objetos_Existe = new Boolean[50];
            String stL_ListaObjetos = "";
            DataTable DatTable_Objs = null;
            //
            CLNBTN_Lg objL_Log_1 = new CLNBTN_Lg(_st_User, _st_FileLog, false, _bl_OutFileLog, false);
            //
            try
            {
                objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "RegAppObjts. Before int i = 0");
                for (int i = 0; i < stL_Objetos.Length; i++)
                { // Inicio del for (int i = 0; i < stL_Objetos.Length; i++)
                    stL_Objetos[i] = "";
                    stL_Desc_Objetos[i] = "";
                    bL_Objetos_Existe[i] = false;
                } // Fin del for (int i = 0; i < stL_Objetos.Length; i++)
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "RegAppObjts. Before setDataBaseInfo");
                    //
                    Query.setDataBaseInfo(ObDbInfo);
                    //
                    stL_App = "";
                    stL_App = ObjL_Config.ReadAKeyFromSection(SECCION_ID_APP, "ID");
                    objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "RegAppObjts. Before IDAPP = " + stL_App);
                    //
                    if (stL_App.Length > 0)
                    { // if (stL_Aux.Length > 0)
                        // Cambia la aplicacion
                        _st_AppInfo_SgCod = stL_App.Trim();
                        //
                        // Define DataTable, para los Datos del Query
                        DataTable DatTableT03 = null;
                        //
                        objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "RegAppObjts. Before Select Table t03apps ");
                        //
                        Query.ToDo_SELECT("*");
                        Query.ToDo_FROM("t03apps");
                        Query.ToDo_WHERE("A03CODIGO_APP", " '" + stL_App.Trim() + "' ");
                        Query.ToDo_EXECUTE_SQL(ref DatTableT03);
                        Query.ToDo_CLOSE();
                        //
                        if (DatTableT03 != null)
                        {
                            objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "RegAppObjts. Before int inL_Row = 0 In Select Table t03apps ");
                            for (int inL_Row = 0; inL_Row < DatTableT03.Rows.Count; inL_Row++)
                            {
                                // Toma la informacion de la fila
                                DataRow Info_Fila = DatTableT03.Rows[inL_Row];
                                blL_Existe = true;
                                lnL_Id_App = 0;
                                //if (!DBNull.Value.Equals(Info_Fila["A03IDAPP"].ToString()))
                                //{
                                //    if (Info_Fila["A03IDAPP"].ToString().Length > 0) lnL_Id_App = Convert.ToInt16(Info_Fila["A03IDAPP"].ToString());
                                //}
                                if (Info_Fila["A03IDAPP"].ToString().Length > 0) 
                                {
                                    lnL_Id_App = Convert.ToInt16(Info_Fila["A03IDAPP"].ToString());
                                }
                                objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "RegAppObjts. IdApp from Table t03apps " + lnL_Id_App.ToString());
                            }
                        }
                        //-->>Query.ToDo_CLOSE();
                        //
                        if (!blL_Existe)
                        { // if (!blL_Existe)
                            stL_Aux = "";
                            stL_Aux = ObjL_Config.ReadAKeyFromSection(SECCION_ID_APP, "Desc");
                            if (stL_Aux.Length == 0)
                            {
                                stL_Aux = stL_App;
                            }
                            //
                            Query.ToDo_INSERT("t03apps");
                            Query.ToDo_SET("A03CODIGO_APP", stL_App.Trim());
                            Query.ToDo_SET("A03DESCRIPCION", stL_Aux.Trim());
                            Query.ToDo_EXECUTE_SQL();
                            Query.ToDo_CLOSE();
                            // Lee nuevamente, para tomar el ID de la tabla
                            Query.ToDo_SELECT("*");
                            Query.ToDo_FROM("t03apps");
                            Query.ToDo_WHERE("A03CODIGO_APP", " '" + stL_App.Trim() + "' ");
                            Query.ToDo_EXECUTE_SQL(ref DatTableT03);
                            Query.ToDo_CLOSE();
                            //
                            if (DatTableT03 != null)
                            {
                                for (int inL_Row = 0; inL_Row < DatTableT03.Rows.Count; inL_Row++)
                                {
                                    // Toma la informacion de la fila
                                    DataRow Info_Fila = DatTableT03.Rows[inL_Row];
                                    lnL_Id_App = 0;
                                    //if (!DBNull.Value.Equals(Info_Fila["A03IDAPP"].ToString()))
                                    //{
                                    //    if (Info_Fila["A03IDAPP"].ToString().Length > 0) lnL_Id_App = Convert.ToInt16(Info_Fila["A03IDAPP"].ToString());
                                    //}
                                    if (Info_Fila["A03IDAPP"].ToString().Length > 0)
                                    {
                                        lnL_Id_App = Convert.ToInt16(Info_Fila["A03IDAPP"].ToString());
                                    }
                                }
                            }
                            //-->>Query.ToDo_CLOSE();
                        } // if (!blL_Existe)
                        //
                        // ------------------------------------------
                        // Lee del archivo de configuracion, todos los objetos y sus descripciones.
                        // ------------------------------------------
                        for (int inL_Index = 0; inL_Index < stL_Objetos.Length; inL_Index++)
                        { // Inicio del for (int inL_Index = 0; inL_Index < stL_Objetos.Length; inL_Index++)
                            //
                            stL_Aux = "";
                            stL_Aux = ObjL_Config.ReadAKeyFromSection(SECCION_OBJETOS_APP, Convert.ToString(inL_Index));
                            if (stL_App.Length > 0 && stL_Aux.Length > 0)
                            { // Inicio if (stL_Aux.Length > 0 && stL_Aux.Length > 0 )
                                //
                                stL_Objetos[inL_Index] = stL_Aux;
                                // Va armando la lista de los objetos para hacer el select con un IN ( LISTA OBJETOS ) 
                                if (stL_ListaObjetos.Length == 0)
                                {
                                    stL_ListaObjetos = " '" + stL_Aux + "' ";
                                }
                                else
                                {
                                    stL_ListaObjetos += " , '" + stL_Aux + "' ";
                                }
                                stL_Aux1 = "";
                                stL_Aux1 = ObjL_Config.ReadAKeyFromSection(SECCION_OBJETOS_APP, Convert.ToString(inL_Index) + "-Desc");
                                stL_Desc_Objetos[inL_Index] = stL_Aux1;
                                //
                            } // FIn if (stL_Aux.Length > 0 && stL_Aux.Length > 0 )
                            //
                        } // Fin for (int inL_Index = 0; inL_Index < stL_Objetos.Length; inL_Index++)
                        // -----------------------------------------------
                        // Hace select de los objetos que ya existen en la bd
                        // -----------------------------------------------
                        if (stL_ListaObjetos.Length > 0)
                        { // Inicio del if (stL_ListaObjetos.Length > 0)
                            String stL_Sql = "";
                            // Arma la condicion con la lista de los objetos.
                            stL_Sql = " A04IDAPP = '" + Convert.ToString(lnL_Id_App) + "' ";
                            stL_Sql += " AND a04objeto_app IN ( " + stL_ListaObjetos + " ) ";
                            Query.ToDo_SELECT("a04objeto_app");
                            Query.ToDo_FROM("t04app_objts");
                            Query.ToDo_WHERE(stL_Sql);
                            Query.ToDo_EXECUTE_SQL(ref DatTable_Objs);
                            Query.ToDo_CLOSE();
                            //
                            if (DatTable_Objs != null)
                            { // inicio del if (DatTable_Objs != null)
                                //
                                for (int inL_Row = 0; inL_Row < DatTable_Objs.Rows.Count; inL_Row++)
                                {
                                    // Toma la informacion de la fila
                                    DataRow Info_Fila = DatTable_Objs.Rows[inL_Row];
                                    String stL_Objeto = "";
                                    //-->>if (!DBNull.Value.Equals(Info_Fila["a04objeto_app"].ToString())) stL_Objeto = Info_Fila["a04objeto_app"].ToString();
                                    if (Info_Fila["a04objeto_app"].ToString().Length > 0) stL_Objeto = Info_Fila["a04objeto_app"].ToString();
                                    // Ubica el objecto en el arreglo y lo marca como existente
                                    for (int i = 0; i < stL_Objetos.Length; i++)
                                    { // Inicio del for (int i = 0; i < stL_Objetos.Length; i++)
                                        if (stL_Objetos[i] == stL_Objeto)
                                        {
                                            bL_Objetos_Existe[i] = true;
                                            break;
                                        }
                                    } // Fin del for (int i = 0; i < stL_Objetos.Length; i++)
                                }
                                //
                            } // Fin del if (DatTable_Objs != null)
                            //-->>Query.ToDo_CLOSE();
                            // Recorre el arreglo y procesa los ojetos que no existen.
                            for (int i = 0; i < bL_Objetos_Existe.Length; i++)
                            { // Inicio del for (int i = 0; i < stL_Objetos.Length; i++)
                                if (!bL_Objetos_Existe[i] && stL_Objetos[i].Trim().Length > 0)
                                { // Inicio del if (!bL_Objetos_Existe[i] && stL_Objetos[i].Trim().Length > 0 )
                                    blL_Existe = false;
                                    Query.ToDo_SELECT("*");
                                    Query.ToDo_FROM("t04app_objts");
                                    Query.ToDo_WHERE("A04IDAPP", " '" + Convert.ToString(lnL_Id_App) + "' ");
                                    Query.ToDo_AND("A04OBJETO_APP", " '" + stL_Objetos[i].Trim() + "' ");
                                    Query.ToDo_EXECUTE_SQL(ref DatTable_Objs);
                                    Query.ToDo_CLOSE();
                                    //
                                    if (DatTable_Objs != null)
                                    {
                                        if (DatTable_Objs.Rows.Count > 0)
                                        {
                                            blL_Existe = true;
                                        }
                                    }
                                    //-->>Query.ToDo_CLOSE();
                                    //
                                    if (!blL_Existe)
                                    { // Inicio del if (!blL_Existe)
                                        //
                                        stL_Aux = stL_Objetos[i].Trim();
                                        stL_Aux1 = "";
                                        stL_Aux1 = stL_Desc_Objetos[i].Trim();
                                        //
                                        if (stL_Aux1.Length == 0)
                                        {
                                            stL_Aux1 = stL_Aux;
                                        }
                                        Query.ToDo_INSERT("t04app_objts");
                                        Query.ToDo_SET("A04IDAPP", Convert.ToString(lnL_Id_App));
                                        Query.ToDo_SET("A04OBJETO_APP", stL_Aux.Trim());
                                        Query.ToDo_SET("A04DESCRIPCION", stL_Aux1.Trim());
                                        Query.ToDo_EXECUTE_SQL();
                                        Query.ToDo_CLOSE();
                                        //
                                    } // Foin del if (!blL_Existe)
                                } // Fin del if (!bL_Objetos_Existe[i] && stL_Objetos[i].Trim().Length > 0 )
                            } // Fin del for (int i = 0; i < stL_Objetos.Length; i++)
                        } // Fin de if (stL_ListaObjetos.Length > 0)
                        // -------------------------------------------------------------------
                        // CODIGO ANTERIOR 
                        // -------------------------------------------------------------------
                        //for (int inL_Index = 1; inL_Index <= 50; inL_Index++)
                        //{ // del for
                        //    //
                        //    stL_Aux = "";
                        //    stL_Aux = ObjL_Config.ReadAKeyFromSection(SECCION_OBJETOS_APP, Convert.ToString(inL_Index));
                        //    if (stL_App.Length > 0 && stL_Aux.Length > 0 )
                        //    { // if (stL_Aux.Length > 0 && stL_Aux.Length > 0 )
                        //        //
                        //        // Define DataTable, para los Datos del Query
                        //        DataTable DatTableT04 = null;
                        //        blL_Existe = false;
                        //        Query.SELECT("*");
                        //        Query.FROM("t04app_objts");
                        //        Query.WHERE("A04IDAPP", " '" + Convert.ToString(lnL_Id_App) + "' ");
                        //        Query.AND ( "A04OBJETO_APP", " '" + stL_Aux.Trim() + "' ");
                        //        Query.EXECUTE_SQL(ref DatTableT04);
                        //        if (DatTableT04 != null)
                        //        {
                        //            if ( DatTableT04.Rows.Count > 0 ) 
                        //            {
                        //                blL_Existe = true;
                        //            }
                        //        }
                        //        Query.CLOSE();
                        //         //
                        //         if ( !blL_Existe) 
                        //         {
                        //             stL_Aux1 = "";
                        //             stL_Aux1 = ObjL_Config.ReadAKeyFromSection(SECCION_OBJETOS_APP, Convert.ToString(inL_Index) + "-Desc");
                        //             //
                        //             if (stL_Aux1.Length == 0)
                        //             {
                        //                 stL_Aux1 = stL_Aux;
                        //             }
                        //             Query.INSERT("t04app_objts");
                        //            Query.SET("A04IDAPP", Convert.ToString(lnL_Id_App));
                        //            Query.SET("A04OBJETO_APP", stL_Aux.Trim());
                        //            Query.SET("A04DESCRIPCION", stL_Aux1.Trim());
                        //            Query.EXECUTE_SQL();
                        //            Query.CLOSE();
                        //         }
                        //         //
                        //    } // fin del if (stL_Aux.Length > 0 && stL_Aux.Length > 0 )
                        //     else
                        //     {
                        //         break ; // sale del for
                        //     }
                        //    //
                        //} // fin del for
                        // -------------------------------------------------------------------
                        // FIN CODIGO ANTERIOR 
                        // -------------------------------------------------------------------
                    } // del if (stL_Aux.Length > 0)
                }
                objL_Log_1.WriteTextInLog(_st_Relac + "." + _st_RelacSon + "." + "RegAppObjts. Before Exit.");
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "RegAppObjts. System.AccessViolationException", "", ex_0.Message.ToString());
                if (DatTable_Objs != null)
                {
                    if (Query != null)
                    {
                        Query.ToDo_CLOSE();
                    }
                }
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "RegAppObjts. Exception", "", ex.Message.ToString());
                if (DatTable_Objs != null)
                {
                    if (Query != null)
                    {
                        Query.ToDo_CLOSE();
                    }
                }
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public bool IsAnActiveDir(Boolean bl_PresentaMensajeNoAutenticado)
        {
            /// 
            /// Metodo: InfDirectorioActivo
            /// Hace la validacion en el directorio Activo, para el usuario que ha hecho login en la maquina de Windows.
            /// </summary>
            /// <param name="bl_PresentaMensajeNoAutenticado">True = Si presenta mensaje de error de auntenticacion</param>
            /// <returns>True = La Autenticacion se pudo hacer. </returns>
            string stL_NombreSv_LDAP = null;
            string stL_PuertoSv_LDAP = null;
            //string stL_UsuarioWindows = null;
            //string stL_DominioWindows = null;
            bool blL_Resultado = false;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Crea nueva instancia para leer la informacion del servidor y el puerto para validar via LDAP.
                    CLNBTN_Cg ObjL_ConfigApp = new CLNBTN_Cg(_st_ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    //
                    stL_NombreSv_LDAP = ObjL_ConfigApp.ReadAKeyFromSection("Servidor_LDAP", "Name");
                    stL_PuertoSv_LDAP = ObjL_ConfigApp.ReadAKeyFromSection("Servidor_LDAP", "Puerto");
                    string stL_Path = "LDAP://" + stL_NombreSv_LDAP + ":" + stL_PuertoSv_LDAP;
                    //
                    //stL_UsuarioWindows = SystemInformation.UserName;
                    //stL_DominioWindows = SystemInformation.UserDomainName;
                    //
                    if (this.IsAValidAuthentic(stL_Path) == true)
                    {
                        blL_Resultado = true;

                    }
                    else
                    {
                        if (bl_PresentaMensajeNoAutenticado)
                        {
                            CLNBTN_Ul Obj_Util = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                            Obj_Util.ShowMessage2User(MENSAJE_9, "", MENSAJE_38);
                            Obj_Util = null;
                        }
                    }
                }
                return blL_Resultado;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "IsAnActiveDir. System.AccessViolationException", "", ex_0.Message.ToString());
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "IsAnActiveDir. Exception", "", ex.Message.ToString());
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public bool IsAValidAuthentic(String Path2Validate)
        {
            /// 
            /// Metodo: ValidaAutenticacion
            /// realiza la validacion del paht ante el directorio activo
            /// Devuele True o False, si se pudo hacer o no la autenticacion.
            /// </summary>
            /// <param name="st_Path">Informacion a validar, la cual contiene LDA://SERVIDOR:PUERTO</param>
            /// <returns>True o False</returns>
            //
            DirectoryEntry entry = new DirectoryEntry(Path2Validate);
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return false;
                }
                else
                {
                    DirectorySearcher search = new DirectorySearcher(entry);
                    SearchResult result = search.FindOne();
                    if (result == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                // Importante: Manda el ultimo parametro el FALSE, para que no presente el error por la pantalla
                // por que es cuando se esta inicializando la aplicacion, y no hay nada ejecutando frente al usuario
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "IsAValidAuthentic. System.AccessViolationException", "", ex_0.Message.ToString());
                return false;
            }
            catch (Exception ex)
            {
                // Importante: Manda el ultimo parametro el FALSE, para que no presente el error por la pantalla
                // por que es cuando se esta inicializando la aplicacion, y no hay nada ejecutando frente al usuario
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "IsAValidAuthentic. Exception", "", ex.Message.ToString());
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void Let_Write_UserAppAccess(ref CLNBTN_IQy ObDbInfo)
        { // Inicio del Registra_Ingreso_Usuario_App
            /// 
            /// Metodo : Registra_Ingreso_Usuario_App
            /// Registra el ingreso del usuario a la aplicacion, grabando los datos en la tabla:
            /// t06usuario_accesos, en caso que exista en la base de datos con la cual esta trabajando.
            /// En el objeto de la informacion de la base de datos :
            /// ObDbInfo
            /// Coloca el Id de la Tabla, t06usuario_accesos, que se genero.
            /// </summary>
            /// <param name="ObDbInfo">Por Referencia. Informacion de la base de datos con la cual va a trabajar.</param>
            // Encargada de Registrar el ingreso del usuario a la aplicacion
            // Actualiza la tabla : t06usuario_accesos, en caso que exista en la base de datos con la cual esta trabajando.
            //
            String stL_App = "";
            decimal lnL_Id_App = 0;
            decimal lnL_Id_Usuario = 0;
            //
            String stL_UsuarioWin = SystemInformation.UserName;
            String stL_DominioWin = SystemInformation.UserDomainName;
            String stL_NombreMaquina = "";
            String stL_IP = "";
            String stL_UsuarioApp = "";
            CLNBTN_IQy.inDB_Types in_TipoServidor = CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
            long lnL_ID_T06 = 0;
            Boolean blL_Graba_Fecha_Ingreso = false;
            //
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    stL_UsuarioApp = ObDbInfo.getUser();
                    //  
                    if (stL_UsuarioApp.Length > 0)
                    { // if (stL_UsuarioApp.Length > 0)
                        CLNBTN_Cg ObjL_Config = new CLNBTN_Cg(_st_ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                        CLNBTN_Qy Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                        CLNBTN_Ul Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                        //
                        CLNBTN_QyV Obj_Valid = new CLNBTN_QyV(_st_User, _st_FileLog, ObDbInfo, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                        //
                        Query.setDataBaseInfo(ObDbInfo);
                        // Valida si la tabla t06usuario_accesos, existe en la base de datos
                        if (Obj_Valid.Is_A_Valid_DBObject("t06usuario_accesos"))
                        { // inicio del if ( Obj_Valid.Object_Exists("t06usuario_accesos"))
                            stL_App = "";
                            stL_App = ObjL_Config.ReadAKeyFromSection(SECCION_ID_APP, "ID");
                            if (stL_App.Length > 0)
                            { // if (stL_Aux.Length > 0)
                                //
                                // Define DataTable, para los Datos del Query
                                DataTable DatTableT03 = null;
                                //
                                Query.ToDo_SELECT("*");
                                Query.ToDo_FROM("t03apps");
                                Query.ToDo_WHERE("A03CODIGO_APP", " '" + stL_App.Trim() + "' ");
                                Query.ToDo_EXECUTE_SQL(ref DatTableT03);
                                Query.ToDo_CLOSE();
                                //
                                if (DatTableT03 != null)
                                {
                                    for (int inL_Row = 0; inL_Row < DatTableT03.Rows.Count; inL_Row++)
                                    {
                                        // Toma la informacion de la fila
                                        DataRow Info_Fila = DatTableT03.Rows[inL_Row];
                                        lnL_Id_App = 0;
                                        //if (!DBNull.Value.Equals(Info_Fila["A03IDAPP"].ToString()))
                                        //{
                                            if (Info_Fila["A03IDAPP"].ToString().Length > 0) lnL_Id_App = Convert.ToInt16(Info_Fila["A03IDAPP"].ToString());
                                        //}
                                    }
                                }
                                //-->>Query.ToDo_CLOSE();
                                // Halla el Id del Usuario
                                Query.ToDo_SELECT("A00IDUSUARIO");
                                Query.ToDo_FROM("t00usuarios");
                                Query.ToDo_WHERE("A00USUARIOWIN", " '" + stL_UsuarioApp.Trim() + "' ");
                                Query.ToDo_EXECUTE_SQL(ref DatTableT03);
                                Query.ToDo_CLOSE();
                                //
                                if (DatTableT03 != null)
                                {
                                    for (int inL_Row = 0; inL_Row < DatTableT03.Rows.Count; inL_Row++)
                                    {
                                        // Toma la informacion de la fila
                                        DataRow Info_Fila = DatTableT03.Rows[inL_Row];
                                        lnL_Id_Usuario = 0;
                                        //if (!DBNull.Value.Equals(Info_Fila["A00IDUSUARIO"].ToString()))
                                        //{
                                            if (Info_Fila["A00IDUSUARIO"].ToString().Length > 0) lnL_Id_Usuario = Convert.ToInt16(Info_Fila["A00IDUSUARIO"].ToString());
                                        //}
                                    }
                                }
                                //-->>Query.ToDo_CLOSE();
                                //
                                if (lnL_Id_App > 0 && lnL_Id_Usuario > 0)
                                { // inicio del if (lnL_Id_App > 0 && lnL_Id_Usuario > 0 )
                                    // Halla nombre de la maquina y la IP
                                    Utils.BringMe_MachineName_IpAdd(ref stL_NombreMaquina, ref stL_IP);
                                    //
                                    // El tipo de servidor de la base de datos.
                                    in_TipoServidor = ObDbInfo.getDataBaseEngine_Type();
                                    //
                                    Query.ToDo_INSERT("t06usuario_accesos");
                                    Query.ToDo_SET("A06IDUSUARIO", Convert.ToString(lnL_Id_Usuario));
                                    Query.ToDo_SET("A06IDAPP", Convert.ToString(lnL_Id_App));
                                    // Para los motores diferentes de SQL SERVER y PostGreSQL, se graba la fecha y hora.
                                    switch (in_TipoServidor)
                                    { // inicio del switch (in_TipoServidor)
                                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                                            blL_Graba_Fecha_Ingreso = false;
                                            break;
                                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                                            blL_Graba_Fecha_Ingreso = false;
                                            break;
                                        default:
                                            blL_Graba_Fecha_Ingreso = true;
                                            break;
                                    } // FIn del switch (in_TipoServidor)
                                    if (blL_Graba_Fecha_Ingreso)
                                    {
                                        Query.ToDo_SET("A06FECHA_LOGIN", Utils.BringMeServerDate(ObDbInfo, true, true));
                                    }
                                    //
                                    Query.ToDo_SET("A06USUARIO_WIN", stL_UsuarioApp.Trim());
                                    Query.ToDo_SET("A06DOMINIO_WIN", stL_DominioWin.Trim());
                                    Query.ToDo_SET("A06MAQUINA", stL_NombreMaquina.Trim());
                                    Query.ToDo_SET("A06IP", stL_IP.Trim());
                                    Query.ToDo_EXECUTE_SQL();
                                    Query.ToDo_CLOSE();
                                    //
                                    // Halla el Id Generado en la tabla.
                                    //
                                    Query.ToDo_SELECT(" MAX(A06ID) ");
                                    Query.ToDo_FROM("t06usuario_accesos");
                                    Query.ToDo_WHERE("A06IDUSUARIO", Convert.ToString(lnL_Id_Usuario));
                                    Query.ToDo_AND("A06IDAPP", Convert.ToString(lnL_Id_App));
                                    Query.ToDo_AND("A06USUARIO_WIN", " '" + stL_UsuarioApp.Trim() + "' ");
                                    Query.ToDo_AND("A06DOMINIO_WIN", " '" + stL_DominioWin.Trim() + "' ");
                                    Query.ToDo_AND("A06MAQUINA", " '" + stL_NombreMaquina.Trim() + "' ");
                                    Query.ToDo_AND("A06IP", " '" + stL_IP.Trim() + "' ");
                                    //
                                    switch (in_TipoServidor)
                                    { // inicio del switch (in_TipoServidor)
                                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                                            // SQl Server.
                                            Query.ToDo_AND("ISNULL ( A06FECHA_LOGOUT , '' ) = '' ", "");
                                            break;
                                        case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                                            // MY SQL
                                            Query.ToDo_AND("IFNULL( A06FECHA_LOGOUT , '' ) = '' ", "");
                                            break;
                                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                                            // PostGreSQL
                                            Query.ToDo_AND("( A06FECHA_LOGOUT IS NULL ) ", "");
                                            break;
                                        default:
                                            //
                                            Query.ToDo_AND("A06FECHA_LOGOUT = NULL ", "");
                                            break;
                                    } // Fin inicio del switch (in_TipoServidor)
                                    // Define DataTable, para los Datos del Query
                                    DataTable DatTableT06 = null;
                                    Query.ToDo_EXECUTE_SQL(ref DatTableT06);
                                    Query.ToDo_CLOSE();
                                    //
                                    if (DatTableT06 != null)
                                    {
                                        for (int inL_Row = 0; inL_Row < DatTableT06.Rows.Count; inL_Row++)
                                        {
                                            // Toma la informacion de la fila
                                            DataRow Info_Fila = DatTableT06.Rows[inL_Row];
                                            lnL_ID_T06 = 0;
                                            //if (!DBNull.Value.Equals(Info_Fila[0].ToString()))
                                            //{
                                                if (Info_Fila[0].ToString().Length > 0) lnL_ID_T06 = Convert.ToInt32(Info_Fila[0].ToString());
                                            //}
                                        }
                                    }
                                    //-->>Query.ToDo_CLOSE();
                                    // IMPORTANTE:
                                    // Devuelve el ID de la tabla T06
                                    ObDbInfo.setUserApp_Login_ID(lnL_ID_T06);
                                    //
                                } // fin del if (lnL_Id_App > 0 && lnL_Id_Usuario > 0)
                            } // fin del if (stL_App.Length > 0)
                        } // fin del if ( Obj_Valid.Object_Exists("t06usuario_accesos"))
                    } // del if (stL_UsuarioApp.Length > 0)
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Write_UserAppAccess. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Write_UserAppAccess. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void Let_Write_UserAppAccessOut(ref CLNBTN_IQy ObDbInfo)
        { // Inicio del Registra_Salida_Usuario_App
            /// 
            /// Metodo : Registra_Salida_Usuario_App       
            /// Encargado de actualizar la fecha y hora de salida de la aplicacion
            /// Actualiza el campo : A06FECHA_LOGOUT
            /// De la tabla : t06usuario_accesos
            /// con el Id ( A06ID ) , que se genero cuando se ingreso a la aplicacion
            /// y que el Metodo : Registra_Ingreso_Usuario_App, lo grabado en la propiedad:
            /// ObDbInfo.getID_Ingreso_Usuario_App()
            /// Al final llama el metodo:
            /// ObDbInfo.setID_Ingreso_Usuario_App(0)
            /// Dejando el Id en cero ( 0 ) 
            /// De esta forma se graba la hora y fecha en la que el usuario sale de la aplicacion.
            /// 
            /// <param name="ObDbInfo">Por Referencia. Informacion de la base de datos con la cual va a trabajar.</param>
            // Encargada de Registrar la salida del usuario de la aplicacion
            // Actualiza la tabla : t06usuario_accesos, en caso que exista en la base de datos con la cual esta trabajando.
            //
            String stL_App = "";
            decimal lnL_Id_App = 0;
            decimal lnL_Id_Usuario = 0;
            //
            String stL_UsuarioWin = SystemInformation.UserName;
            String stL_DominioWin = SystemInformation.UserDomainName;
            String stL_NombreMaquina = "";
            String stL_IP = "";
            //
            String stL_UsuarioApp = "";
            CLNBTN_IQy.inDB_Types in_TipoServidor = CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    stL_UsuarioApp = ObDbInfo.getUser();
                    if (stL_UsuarioApp.Length > 0)
                    { // if (stL_UsuarioApp.Length > 0)
                        CLNBTN_Cg ObjL_Config = new CLNBTN_Cg(_st_ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                        CLNBTN_Qy Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                        CLNBTN_Ul Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                        //
                        CLNBTN_QyV Obj_Valid = new CLNBTN_QyV(_st_User, _st_FileLog, ObDbInfo, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                        //
                        Query.setDataBaseInfo(ObDbInfo);
                        // Valida si la tabla t06usuario_accesos, existe en la base de datos
                        if (Obj_Valid.Is_A_Valid_DBObject("t06usuario_accesos"))
                        { // inicio del if ( Obj_Valid.Object_Exists("t06usuario_accesos"))
                            stL_App = "";
                            stL_App = ObjL_Config.ReadAKeyFromSection(SECCION_ID_APP, "ID");
                            if (stL_App.Length > 0)
                            { // if (stL_Aux.Length > 0)
                                //
                                // Define DataTable, para los Datos del Query
                                DataTable DatTableT03 = null;
                                //
                                Query.ToDo_SELECT("*");
                                Query.ToDo_FROM("t03apps");
                                Query.ToDo_WHERE("A03CODIGO_APP", " '" + stL_App.Trim() + "' ");
                                Query.ToDo_EXECUTE_SQL(ref DatTableT03);
                                Query.ToDo_CLOSE();
                                //
                                if (DatTableT03 != null)
                                {
                                    for (int inL_Row = 0; inL_Row < DatTableT03.Rows.Count; inL_Row++)
                                    {
                                        // Toma la informacion de la fila
                                        DataRow Info_Fila = DatTableT03.Rows[inL_Row];
                                        lnL_Id_App = 0;
                                        //if (!DBNull.Value.Equals(Info_Fila["A03IDAPP"].ToString()))
                                        //{
                                            if (Info_Fila["A03IDAPP"].ToString().Length > 0) lnL_Id_App = Convert.ToInt16(Info_Fila["A03IDAPP"].ToString());
                                        //}
                                    }
                                }
                                //-->>Query.ToDo_CLOSE();
                                // Halla el Id del Usuario
                                Query.ToDo_SELECT("A00IDUSUARIO");
                                Query.ToDo_FROM("t00usuarios");
                                Query.ToDo_WHERE("A00USUARIOWIN", " '" + stL_UsuarioApp.Trim() + "' ");
                                Query.ToDo_EXECUTE_SQL(ref DatTableT03);
                                Query.ToDo_CLOSE();
                                //
                                if (DatTableT03 != null)
                                {
                                    for (int inL_Row = 0; inL_Row < DatTableT03.Rows.Count; inL_Row++)
                                    {
                                        // Toma la informacion de la fila
                                        DataRow Info_Fila = DatTableT03.Rows[inL_Row];
                                        lnL_Id_Usuario = 0;
                                        //if (!DBNull.Value.Equals(Info_Fila["A00IDUSUARIO"].ToString()))
                                        //{
                                            if (Info_Fila["A00IDUSUARIO"].ToString().Length > 0) lnL_Id_Usuario = Convert.ToInt16(Info_Fila["A00IDUSUARIO"].ToString());
                                        //}
                                    }
                                }
                                //-->>Query.ToDo_CLOSE();
                                ///////////////////
                                if (lnL_Id_App > 0 && lnL_Id_Usuario > 0)
                                { // inicio del if (lnL_Id_App > 0 && lnL_Id_Usuario > 0 )
                                    if (ObDbInfo.getUserApp_Login_ID() > 0)
                                    { // Inicio del if ( ObDbInfo.getID_Ingreso_Usuario_App() > 0 ) 
                                        // hace el update en la tabla t06
                                        // Halla nombre de la maquina y la IP
                                        Utils.BringMe_MachineName_IpAdd(ref stL_NombreMaquina, ref stL_IP);
                                        //
                                        Query.ToDo_UPDATE("t06usuario_accesos");
                                        Query.ToDo_SET("A06FECHA_LOGOUT", Utils.BringMeServerDate(ObDbInfo, true, true));
                                        //
                                        Query.ToDo_WHERE("A06ID", Convert.ToString(ObDbInfo.getUserApp_Login_ID()));
                                        Query.ToDo_AND("A06IDUSUARIO", Convert.ToString(lnL_Id_Usuario));
                                        Query.ToDo_AND("A06IDAPP", Convert.ToString(lnL_Id_App));
                                        Query.ToDo_AND("A06USUARIO_WIN", " '" + stL_UsuarioWin.Trim() + "' ");
                                        Query.ToDo_AND("A06DOMINIO_WIN", " '" + stL_DominioWin.Trim() + "' ");
                                        Query.ToDo_AND("A06MAQUINA", " '" + stL_NombreMaquina.Trim() + "' ");
                                        Query.ToDo_AND("A06IP", " '" + stL_IP.Trim() + "' ");
                                        //
                                        in_TipoServidor = ObDbInfo.getDataBaseEngine_Type();
                                        //
                                        switch (in_TipoServidor)
                                        { // inicio del switch (in_TipoServidor)
                                            case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                                                // SQl Server.
                                                //
                                                Query.ToDo_AND("ISNULL ( A06FECHA_LOGOUT , '' ) = '' ", "");
                                                break;
                                            case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                                                // MY SQL
                                                // 
                                                Query.ToDo_AND("IFNULL( A06FECHA_LOGOUT , '' ) = '' ", "");
                                                break;
                                            case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                                                // PostGreSQL
                                                Query.ToDo_AND("( A06FECHA_LOGOUT IS NULL ) ", "");
                                                break;
                                            default:
                                                //
                                                Query.ToDo_AND("A06FECHA_LOGOUT = NULL ", "");
                                                break;
                                        } // Fin inicio del switch (in_TipoServidor)
                                        //
                                        Query.ToDo_EXECUTE_SQL();
                                        // Si ejecuta bien el query, deja en cero, el ID en la clase
                                        // de la informacion de la base de datos.
                                        if (Query.getSuccessQueryExecution())
                                        {
                                            ObDbInfo.setUserApp_Login_ID(0);
                                        }
                                        Query.ToDo_CLOSE();
                                    } // Fin de if ( ObDbInfo.getID_Ingreso_Usuario_App() > 0 ) 
                                } // fin del if (lnL_Id_App > 0 && lnL_Id_Usuario > 0)
                            } // fin del if (stL_App.Length > 0)
                        } // fin del if ( Obj_Valid.Object_Exists("t06usuario_accesos"))
                    } // del if (stL_UsuarioApp.Length > 0)
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "IsAnActiveDir. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "IsAnActiveDir. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void BringMe_ModulePermInfo(CLNBTN_IQy ObDbInfo, String ConfFile, ref string[] Arr_Objs, ref Boolean[] Arr_Read, ref Boolean[] Arr_Write, ref Boolean[] Arr_Delete, ref Boolean[] Arr_Display)
        {
            /// 
            /// Encargado de Leer los permisos de las acciones de seguridad y devolver los arreglos con los permisos correspondientes.
            /// Las acciones de seguridad las lee del archivo de configuracion de la aplicacion.
            /// 
            /// <param name="ObDbInfo">Objeto con la informacion de la base de datos.</param>
            /// <param name="ConfFile">Ruta y Path del archivo de configuracion de la aplicacion</param>
            /// <param name="Arr_Objs">Devuelve Arreglo con los objetos de la seguridad</param>
            /// <param name="Arr_Read">Devuelve Arreglo, correspondiente a los objetos con el permiso de READ</param>
            /// <param name="Arr_Write">Devuelve Arreglo, correspondiente a los objetos con el permiso de WRITE</param>
            /// <param name="Arr_Delete">Devuelve Arreglo, correspondiente a los objetos con el permiso de DELETE</param>
            /// <param name="Arr_Display">Devuelve Arreglo, correspondiente a los objetos con el permiso de DISPLAY</param>
            String stL_AppSeguridad = "";
            String stL_Objeto = "";
            //
            Boolean blL_Read = false;
            Boolean blL_Write = false;
            Boolean blL_Delete = false;
            Boolean blL_Display = false;
            //
            CLNBTN_Qy Query = null;
            //CLNBTN_Qy Query_Permisos = null;
            String stL_Sql = "";
            // Define DataTable, para los Datos del Query
            DataTable DatObjetos = null;
            //DataTable DatObjetos_Permisos = null;
            //
            //String stL_IdObjeto = "0";
            //
            String stL_Objeto_Ant = "";
            //
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    CLNBTN_Cg ObjL_ConfigApp = new CLNBTN_Cg(ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    stL_AppSeguridad = ObjL_ConfigApp.ReadAKeyFromSection(SECCION_ID_APP, "ID");
                    //
                    for (int i = 0; i < Arr_Objs.Length; i++)
                    { // Inicio del for (int i = 0; i < Arr_Objs.Length; i++)
                        Arr_Objs[i] = "";
                        Arr_Read[i] = false;
                        Arr_Write[i] = false;
                        Arr_Delete[i] = false;
                        Arr_Display[i] = false;
                        //
                        // Asigna nombre de los objetos al arreglo.
                        stL_Objeto = ObjL_ConfigApp.ReadAKeyFromSection(SECCION_OBJETOS_APP, i.ToString());
                        if (stL_Objeto.Length > 0)
                        { // Inicio del if (stL_Objeto.Length > 0)
                            Arr_Objs[i] = stL_Objeto;
                        }
                    } // Fin del for (int i = 0; i < Arr_Objs.Length; i++)
                    // ------------------------------------
                    // Lee todos los permisos de la aplicacion
                    // ------------------------------------
                    Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    Query.setDataBaseInfo(ObDbInfo);
                    //
                    //Query_Permisos = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    //Query_Permisos.setDataBaseInfo(ObDbInfo);
                    //
                    // ==============================================================
                    // ---------------------- I N I C I O  --------------------------
                    // ==============================================================
                    // --------------------------------------------------------------
                    // Selecciona de la tabla de permisos
                    stL_Sql = " * FROM t05app_objt_permission , t04app_objts WHERE ";
                    stL_Sql += " a05idapp = a04idapp ";
                    stL_Sql += " AND a05idobjeto = a04idobjeto ";
                    // Para los registros de la aplicacion 
                    stL_Sql += " AND A05IDAPP IN ( SELECT DISTINCT A03IDAPP FROM t03apps WHERE A03CODIGO_APP = '" + stL_AppSeguridad.Trim() + "' ) ";
                    // PAra los grupos en los cuales esta el usuario.
                    stL_Sql += " AND A05IDGRUPO IN ( SELECT DISTINCT A02IDGRUPO FROM t02usuario_grupos , t00usuarios ";
                    stL_Sql += " WHERE A02IDUSUARIO = A00IDUSUARIO ";
                    stL_Sql += " AND A00USUARIOWIN = '" + ObDbInfo.getUser() + "' )  ";
                    stL_Sql += " ORDER BY a05idobjeto , A05IDGRUPO ";
                    //
                    // Init flags values
                    blL_Read = false;
                    blL_Write = false;
                    blL_Delete = false;
                    blL_Display = false;
                    //
                    Query.ToDo_SELECT(stL_Sql);
                    Query.ToDo_EXECUTE_SQL(ref DatObjetos);
                    Query.ToDo_CLOSE();
                    //
                    if (DatObjetos != null)
                    { // del if (DatObjetos != null)
                        for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                        { // del for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                            //
                            // Toma la informacion de la fila
                            DataRow Info_Fila_0 = DatObjetos.Rows[inL_Row];
                            String stL_Aux = "";
                            // -----------------------------
                            // LEE EL DATO DEL OBJETO
                            // -----------------------------
                            stL_Objeto = "";
                            //-->>if (!DBNull.Value.Equals(Info_Fila_0["a04objeto_app"].ToString())) stL_Objeto = Info_Fila_0["a04objeto_app"].ToString();
                            if (Info_Fila_0["a04objeto_app"].ToString().Length > 0) stL_Objeto = Info_Fila_0["a04objeto_app"].ToString();
                            if (inL_Row == 0)
                            { // Inicio del if (inL_Row == 0)
                                // La primera vez carga el dato del objeto en el objeto anterior.
                                stL_Objeto_Ant = stL_Objeto;
                            } // Fin del if (inL_Row == 0)
                            //
                            // ---------------------------------------------
                            // Valida si cambia de Objeto
                            // ---------------------------------------------
                            if (stL_Objeto != stL_Objeto_Ant)
                            { // Inicio del if (stL_IdObjeto == stL_IdObjeto_Ant)
                                // 
                                // Se Cambio de objeto
                                //
                                // ///////////////////////////////////////////////
                                // Asigna el Permiso al objeto correspondiente en el Arreglo.
                                // Trabaja con el Objeto Anterior.
                                // ///////////////////////////////////////////////
                                if (stL_Objeto_Ant.Length > 0)
                                { // Inicio del if (stL_Objeto_Ant.Length > 0)
                                    for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                                    { // inicio del for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                                        if (stL_Objeto_Ant == Arr_Objs[inL_Indx])
                                        { // Inicio de if (stL_Objeto_Ant == Arr_Objs[inL_Indx])
                                            Arr_Read[inL_Indx] = blL_Read;
                                            Arr_Write[inL_Indx] = blL_Write;
                                            Arr_Delete[inL_Indx] = blL_Delete;
                                            Arr_Display[inL_Indx] = blL_Display;
                                            //
                                            break;
                                            //
                                        } // Fin del if (stL_Objeto_Ant == Arr_Objs[inL_Indx])
                                    } // Fin del for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                                } // Fin del if (stL_Objeto_Ant.Length > 0)
                                //
                                // Init flags values
                                blL_Read = false;
                                blL_Write = false;
                                blL_Delete = false;
                                blL_Display = false;
                                // Asigna al objeto anterior el objeto que ha liedo.
                                stL_Objeto_Ant = stL_Objeto;
                            } // Fin del ELSE del if (stL_IdObjeto == stL_IdObjeto_Ant)
                            // -----------------------------
                            // LEE DATOS DE PERMISOS
                            // -----------------------------
                            if (!blL_Read)
                            {
                                stL_Aux = "";
                                //if (!DBNull.Value.Equals(Info_Fila_0["A05READ"].ToString())) stL_Aux = Info_Fila_0["A05READ"].ToString();
                                if (Info_Fila_0["A05READ"].ToString().Length > 0) stL_Aux = Info_Fila_0["A05READ"].ToString();
                                blL_Read = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                            }
                            if (!blL_Write)
                            {
                                stL_Aux = "";
                                //if (!DBNull.Value.Equals(Info_Fila_0["A05WRITE"].ToString())) stL_Aux = Info_Fila_0["A05WRITE"].ToString();
                                if (Info_Fila_0["A05WRITE"].ToString().Length > 0) stL_Aux = Info_Fila_0["A05WRITE"].ToString();
                                blL_Write = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                            }
                            if (!blL_Delete)
                            {
                                stL_Aux = "";
                                //if (!DBNull.Value.Equals(Info_Fila_0["A05DELETE"].ToString())) stL_Aux = Info_Fila_0["A05DELETE"].ToString();
                                if (Info_Fila_0["A05DELETE"].ToString().Length > 0) stL_Aux = Info_Fila_0["A05DELETE"].ToString();
                                blL_Delete = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                            }
                            if (!blL_Display)
                            {
                                stL_Aux = "";
                                //if (!DBNull.Value.Equals(Info_Fila_0["A05DISPLAY"].ToString())) stL_Aux = Info_Fila_0["A05DISPLAY"].ToString();
                                if (Info_Fila_0["A05DISPLAY"].ToString().Length > 0) stL_Aux = Info_Fila_0["A05DISPLAY"].ToString();
                                blL_Display = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                            }
                        } // Fin del // del for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                        // ----------------------------
                        // IMPORTANTE: Procesa la informacion del ultimo registro.
                        // ----------------------------
                        // ///////////////////////////////////////////////
                        // Asigna el Permiso al objeto correspondiente en el Arreglo.
                        // Trabaja con el Objeto que leyo de ultimas.
                        // ///////////////////////////////////////////////
                        if (stL_Objeto.Length > 0)
                        { // Inicio del if (stL_Objeto.Length > 0)
                            for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                            { // inicio del for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                                if (stL_Objeto == Arr_Objs[inL_Indx])
                                { // Inicio de if (stL_Objeto == Arr_Objs[inL_Indx])
                                    Arr_Read[inL_Indx] = blL_Read;
                                    Arr_Write[inL_Indx] = blL_Write;
                                    Arr_Delete[inL_Indx] = blL_Delete;
                                    Arr_Display[inL_Indx] = blL_Display;
                                    //
                                    break;
                                    //
                                } // Fin del if (stL_Objeto == Arr_Objs[inL_Indx])
                            } // Fin del for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                        } // Fin del if (stL_Objeto_Ant.Length > 0)
                    } // Fin del // del if (DatObjetos != null)
                    //-->>Query.ToDo_CLOSE();
                    // ==============================================================
                    // ----------------------------- F I N --------------------------
                    // ==============================================================

                    //// Selecciona de la tabla de permisos
                    //stL_Sql = " distinct a05idobjeto , a04objeto_app FROM t05app_objt_permission , t04app_objts WHERE ";
                    //stL_Sql += " a05idapp = a04idapp ";
                    //stL_Sql += " AND a05idobjeto = a04idobjeto ";
                    //// Para los registros de la aplicacion 
                    //stL_Sql += " AND A05IDAPP IN ( SELECT DISTINCT A03IDAPP FROM t03apps WHERE A03CODIGO_APP = '" + stL_AppSeguridad.Trim() + "' ) ";
                    //// PAra los grupos en los cuales esta el usuario.
                    //stL_Sql += " AND A05IDGRUPO IN ( SELECT DISTINCT A02IDGRUPO FROM t02usuario_grupos , t00usuarios ";
                    //stL_Sql += " WHERE A02IDUSUARIO = A00IDUSUARIO ";
                    //stL_Sql += " AND A00USUARIOWIN = '" + ObDbInfo.getUser() + "' )  ";
                    ////
                    //Query.ToDo_SELECT(stL_Sql);
                    //Query.ToDo_EXECUTE_SQL(ref DatObjetos);
                    ////
                    //if (DatObjetos != null)
                    //{ // del if (DatObjetos != null)
                    //    for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                    //    { // del for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                    //        //
                    //        // Toma la informacion de la fila
                    //        DataRow Info_Fila_0 = DatObjetos.Rows[inL_Row];
                    //        //
                    //        stL_IdObjeto = "";
                    //        if (!DBNull.Value.Equals(Info_Fila_0["a05idobjeto"].ToString())) stL_IdObjeto = Info_Fila_0["a05idobjeto"].ToString();
                    //        if (stL_IdObjeto.Length == 0)
                    //        {
                    //            stL_IdObjeto = "0";
                    //        }
                    //        // Lee los permisos para el Objeto
                    //        stL_Objeto = "";
                    //        if (!DBNull.Value.Equals(Info_Fila_0["a04objeto_app"].ToString())) stL_Objeto = Info_Fila_0["a04objeto_app"].ToString();
                    //        //
                    //        // Selecciona de la tabla de permisos
                    //        stL_Sql = "";
                    //        stL_Sql = " * FROM t05app_objt_permission , t04app_objts WHERE ";
                    //        stL_Sql += " a05idapp = a04idapp ";
                    //        stL_Sql += " AND a05idobjeto = a04idobjeto ";
                    //        // Para los registros de la aplicacion 
                    //        stL_Sql += " AND A05IDAPP IN ( SELECT DISTINCT A03IDAPP FROM t03apps WHERE A03CODIGO_APP = '" + stL_AppSeguridad.Trim() + "' ) ";
                    //        // PAra los grupos en los cuales esta el usuario.
                    //        stL_Sql += " AND A05IDGRUPO IN ( SELECT DISTINCT A02IDGRUPO FROM t02usuario_grupos , t00usuarios ";
                    //        stL_Sql += " WHERE A02IDUSUARIO = A00IDUSUARIO ";
                    //        stL_Sql += " AND A00USUARIOWIN = '" + ObDbInfo.getUser() + "' )  ";
                    //        // Para el Objecto que se esta trabajando.
                    //        stL_Sql += " AND a05idobjeto = " + stL_IdObjeto;
                    //        //
                    //        Query_Permisos.ToDo_SELECT(stL_Sql);
                    //        Query_Permisos.ToDo_EXECUTE_SQL(ref DatObjetos_Permisos);
                    //        //
                    //        // Init flags values
                    //        blL_Read = false;
                    //        blL_Write = false;
                    //        blL_Delete = false;
                    //        blL_Display = false;
                    //        //
                    //        if (DatObjetos_Permisos != null)
                    //        { // del if (DatObjetos_Permisos != null)
                    //            for (int inL_Row_Per = 0; inL_Row_Per < DatObjetos_Permisos.Rows.Count; inL_Row_Per++)
                    //            { // del for (int inL_Row_Per = 0; inL_Row_Per < DatObjetos_Permisos.Rows.Count; inL_Row_Per++)
                    //                //
                    //                String stL_Aux = "";
                    //                // Toma la informacion de la fila
                    //                DataRow Info_Fila = DatObjetos_Permisos.Rows[inL_Row_Per];
                    //                //
                    //                stL_Objeto = "";
                    //                if (!DBNull.Value.Equals(Info_Fila["a04objeto_app"].ToString())) stL_Objeto = Info_Fila["a04objeto_app"].ToString();
                    //                // 
                    //                if (!blL_Read)
                    //                {
                    //                    stL_Aux = "";
                    //                    if (!DBNull.Value.Equals(Info_Fila["A05READ"].ToString())) stL_Aux = Info_Fila["A05READ"].ToString();
                    //                    blL_Read = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                    //                }
                    //                if (!blL_Write)
                    //                {
                    //                    stL_Aux = "";
                    //                    if (!DBNull.Value.Equals(Info_Fila["A05WRITE"].ToString())) stL_Aux = Info_Fila["A05WRITE"].ToString();
                    //                    blL_Write = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                    //                }
                    //                if (!blL_Delete)
                    //                {
                    //                    stL_Aux = "";
                    //                    if (!DBNull.Value.Equals(Info_Fila["A05DELETE"].ToString())) stL_Aux = Info_Fila["A05DELETE"].ToString();
                    //                    blL_Delete = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                    //                }
                    //                if (!blL_Display)
                    //                {
                    //                    stL_Aux = "";
                    //                    if (!DBNull.Value.Equals(Info_Fila["A05DISPLAY"].ToString())) stL_Aux = Info_Fila["A05DISPLAY"].ToString();
                    //                    blL_Display = (stL_Aux.Equals(Convert.ToString(ESTADO_ACTIVO)));
                    //                }
                    //                //
                    //            } // FIN del  for (int inL_Row_Per = 0; inL_Row_Per < DatObjetos_Permisos.Rows.Count; inL_Row_Per++)
                    //        } // Fin del del if (DatObjetos_Permisos != null)
                    //        Query_Permisos.ToDo_CLOSE();
                    //        // 
                    //        // ///////////////////////////////////////////////
                    //        // Asigna el Permiso al objeto correspondiente en el Arreglo.
                    //        // ///////////////////////////////////////////////
                    //        if (stL_Objeto.Length > 0)
                    //        { // Inicio del if (stL_Objeto.Length > 0)
                    //            for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                    //            { // inicio del for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                    //                if (stL_Objeto == Arr_Objs[inL_Indx])
                    //                { // Inicio de if (stL_Objeto == Arr_Objs[inL_Indx])
                    //                    Arr_Read[inL_Indx] = blL_Read;
                    //                    Arr_Write[inL_Indx] = blL_Write;
                    //                    Arr_Delete[inL_Indx] = blL_Delete;
                    //                    Arr_Display[inL_Indx] = blL_Display;
                    //                    //
                    //                    break;
                    //                    //
                    //                } // Fin del if (stL_Objeto == Arr_Objs[inL_Indx])
                    //            } // Fin del for (int inL_Indx = 0; inL_Indx < Arr_Objs.Length; inL_Indx++)
                    //        } // Fin del if (stL_Objeto.Length > 0)
                    //        //
                    //    } // Fin del for (int inL_Row = 0; inL_Row < DatObjetos.Rows.Count; inL_Row++)
                    //} // del if (DatObjetos != null)
                    //Query.ToDo_CLOSE();
                    // ------------------------------------
                    // FIN Lee todos los permisos de la aplicacion
                    // ------------------------------------
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ModulePermInfo. System.AccessViolationException", "", ex_0.Message.ToString());
                if (DatObjetos != null)
                {
                    if (Query != null)
                    {
                        Query.ToDo_CLOSE();
                    }
                }
                //if (DatObjetos_Permisos != null)
                //{
                //    if (Query_Permisos != null)
                //    {
                //        Query_Permisos.ToDo_CLOSE();
                //    }
                //}
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ModulePermInfo. Exception", "", ex.Message.ToString());
                if (DatObjetos != null)
                {
                    if (Query != null)
                    {
                        Query.ToDo_CLOSE();
                    }
                }
                //if (DatObjetos_Permisos != null)
                //{
                //    if (Query_Permisos != null)
                //    {
                //        Query_Permisos.ToDo_CLOSE();
                //    }
                //}
            }
        }








    }
}
