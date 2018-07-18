using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ExceptionServices;

namespace _C_ProgRes
{
    /// <summary>
    /// Clase Para manejo de la inforamacion de base de datos  
    /// </summary>
    public class ClasX_DBInfo
    {
        //////////////////////////////////////
        /// <summary>
        ///  Tipos de motores de base de datos que se trabajan
        /// </summary>
        public enum inDB_Types 
        { BD_TYPE_SQLSERVER
            , BD_TYPE_ORACLE
            , BD_TYPE_SYBASE
            , BD_TYPE_POSTGRESQL 
            , BD_TYPE_MYSQL
            , BD_TYPE_ACCESS 
            , BD_TYPE_DBASE 
            , BD_TYPE_FOXPRO
        } 
        ///////////////////////////////////////////////////////
        /// <summary>
        /// Define los tipos de conexion a la base de datos.
        /// </summary>
        public enum inConnect_Type
        {
        TYPE_1_CONNECT_USER_SQL = 1 // Usuario SQL, un usuario y password en la dll o .conf.
        , TYPE_2_CONNECT_USER_APP = 2 // Usuario de la aplicacion que es tambien usuario SQL Server.
        , TYPE_3_CONNECT_USER_WIN = 3  // Usuario de windows, que es usuario de SQL, no se pide usuario ni clave.
        , TYPE_4_CONNECT_USER_INFO_FENIX = 4  // Informado por el usuario, caso de Fenix.
            , TYPE_5_CONNECT_PANDORA_BOX = 5 // Conexion via Pandora
        }

        //////////////////////////////////////
        private String stPr_NombreServidor = "ServerName"; // Nombre del servidor SQL
        //
        private String stPr_NombreBdSql = "DataBaseName"; //  Nombre de la base de datos.
        //
        private String stPr_RutaBD = "PathBD"; // Ruta de la base de datos.
        //
        private String stPr_NombreArchivoAccess = "AccessFileName"; // Nombre del archivo de Access.
        //
        private String stPr_IdUsuario_BD = "BDUserID"; // Codigo de usuario de la base de datos.
        //
        private String stPr_ClaveUsuario_BD = "BDUserPass"; // Password del usuario de la base de datos.
        //
        private inDB_Types inPr_TipoMotorBD = 0; // Tipo Motor de base de datos
        //
        private inConnect_Type inPr_TipoConexion = 0; // Tipo de Conexion
        //
        private String stPr_URL_BD = "BDURL"; // URL para la conexion con la bas de datos.
        //
        private String stPr_IP_Address = "DBIPAdress"; // Direccion IP
        //
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        //
        private String stPr_Clave_UsuarioAPP = ""; // La clave del usuario de la aplicacion.
        private String stPr_Clave_Encriptada_UsuarioApp = ""; // la clave encriptada del usuario de la aplicacion.
        //
        private Boolean blPr_Acceso_SSO = false; // Indica si ha hecho un acceso SSO

        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo
        //
        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_IQy ObjPr_Self = null;
        //
        private long lnPr_ID_Ingreso_Usuario_App = 0; // Id de la tabla, T06USUARIO_ACCESOS, cuando ingresa a la aplicacion.
        private long lnPr_PuertoBD = 0; // Puerto de conexion a la base de datos.
        private String stPr_ServidorPandora = ""; // URL del Servidor Pandora
        private int inPr_PuertoPandora = 0; // Puerto de conexional Servidor Pandora
        private String stPr_Usuario_Pandora = "";
        private String stPr_Clave_Usuario_Pandora = "";


        //////////////////////////////
        // Constructores
        //////////////////////////////
        /// <summary>
        /// constructor de la clase 
        /// sin parametros
        /// </summary>
        public ClasX_DBInfo()
        {
            ObjPr_Self = new NBToolsNet.CLNBTN_IQy(stPr_Info);
        }
        /// <summary>
        /// Constructor de la clase 
        /// con parametros de usuario y path y nombre d archivo de log,
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo de usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path y nombre del archivo log</param>
        public ClasX_DBInfo(String st_UsuarioApp, String st_ArchivoLog)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            ObjPr_Self = new NBToolsNet.CLNBTN_IQy(st_UsuarioApp, st_ArchivoLog, stPr_Info);
        }

         /// <summary>
        /// Constructor de la clase 
        /// con parametros de usuario y path y nombre d archivo de log,
        /// Y para el tratamiento del log
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo de usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path y nombre del archivo log</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_DBInfo(String st_UsuarioApp, String st_ArchivoLog,bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog; 
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo;
            ObjPr_Self = new NBToolsNet.CLNBTN_IQy(st_UsuarioApp, st_ArchivoLog,bl_SalidaConsola, bl_SalidaLog , bl_SalidaDialogo, stPr_Info);
        }


        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getNombreServidor
        /// Devuelve el nombre del servidor
        /// </summary>
        /// <returns>
        /// stPr_NombreServidor
        /// </returns>
        public String getNombreServidor()
        {
            stPr_NombreServidor = ObjPr_Self.getServerName();
            return stPr_NombreServidor;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getNombreBaseDatos
        /// Devuelve el nombre de la base de datos
        /// </summary>
        /// <returns>
        /// stPr_NombreBdSql
        /// </returns>
        public String getNombreBaseDatos()
        {
            stPr_NombreBdSql = ObjPr_Self.getDataBaseName();
            return stPr_NombreBdSql;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getRutaBD
        /// Devuelve la ruta de la base de datos, por ejemplo para access
        /// </summary>
        /// <returns>
        /// stPr_RutaBD
        /// </returns>
        public String getRutaBD()
        {
            stPr_RutaBD = ObjPr_Self.getDataBasePath();
            return stPr_RutaBD;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getNombreArchivoAccess
        /// Devuelve la ruta de la base de datos de access.
        /// </summary>
        /// <returns>
        /// stPr_NombreArchivoAccess
        /// </returns>
        public String getNombreArchivoAccess()
        {
            stPr_NombreArchivoAccess = ObjPr_Self.getFileName_Access();
            return stPr_NombreArchivoAccess;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getIdUsuario_BD
        /// Devuelve el codigo del usuario que se conecta a la base de datos.
        /// </summary>
        /// <returns>
        /// stPr_IdUsuario_BD
        /// </returns>
        public String getIdUsuario_BD()
        {
            stPr_IdUsuario_BD = ObjPr_Self.getDataBase_UserID();
            return stPr_IdUsuario_BD;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getClaveUsuario_BD
        /// Devuelve la clave del usuario que se conecta a la base de datos.
        /// </summary>
        /// <returns>
        /// stPr_ClaveUsuario_BD
        /// </returns>
        public String getClaveUsuario_BD()
        {
            stPr_ClaveUsuario_BD = ObjPr_Self.getDataBase_UserPWD();
            return stPr_ClaveUsuario_BD;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getTipoBD
        /// Devuelve el tipo de motor de la base de datos 
        /// </summary>
        /// <returns>
        /// inPr_TipoMotorBD
        /// </returns>
        public inDB_Types getTipoBD()
        {
            NBToolsNet.CLNBTN_IQy.inDB_Types Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
            Aux = ObjPr_Self.getDataBaseEngine_Type();
            switch (Aux)
            {
                case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                    inPr_TipoMotorBD = inDB_Types.BD_TYPE_SQLSERVER;
                    break;
                case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS:
                    inPr_TipoMotorBD = inDB_Types.BD_TYPE_ACCESS;
                    break;
                case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE:
                    inPr_TipoMotorBD = inDB_Types.BD_TYPE_ORACLE;
                    break;
                case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                    inPr_TipoMotorBD = inDB_Types.BD_TYPE_MYSQL;
                    break;
                case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE:
                    inPr_TipoMotorBD = inDB_Types.BD_TYPE_SYBASE;
                    break;
                case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                    inPr_TipoMotorBD = inDB_Types.BD_TYPE_POSTGRESQL;
                    break;
                default:
                    inPr_TipoMotorBD = inDB_Types.BD_TYPE_SQLSERVER;
                    break;
            }
            return inPr_TipoMotorBD;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : inPr_TipoConexion
        /// Devuelve el tipo de conexion a la base de datos 
        /// </summary>
        /// <returns>
        /// inPr_TipoConexion
        /// </returns>
        public inConnect_Type getTipoConexion()
        {
            NBToolsNet.CLNBTN_IQy.inConnect_Type Aux = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
            Aux = ObjPr_Self.get_DataBaseConn_Type();
            switch (Aux)
            {
                case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL:
                    inPr_TipoConexion = inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                    break;
                case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_2_CONNECT_USER_APP:
                    inPr_TipoConexion = inConnect_Type.TYPE_2_CONNECT_USER_APP;
                    break;
                case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN:
                    inPr_TipoConexion = inConnect_Type.TYPE_3_CONNECT_USER_WIN;
                    break;
                case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT:
                    inPr_TipoConexion = inConnect_Type.TYPE_4_CONNECT_USER_INFO_FENIX;
                    break;
                case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN:
                    inPr_TipoConexion = inConnect_Type.TYPE_5_CONNECT_PANDORA_BOX;
                    break;
                default:
                    inPr_TipoConexion = inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                    break;
            }
            return inPr_TipoConexion;
        }

        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getURL_BD
        /// Devuelve la URL,de la base de datos.
        /// </summary>
        /// <returns>
        /// stPr_URL_BD
        /// </returns>
        public String getURL_BD()
        {
            stPr_URL_BD = ObjPr_Self.getServer_URL();
            return stPr_URL_BD;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getIP_Address
        /// Devuelve la direccion IP, de la base de datos.
        /// </summary>
        /// <returns>
        /// stPr_IP_Address
        /// </returns>
        public String getIP_Address()
        {
            stPr_IP_Address = ObjPr_Self.getServer_IP_Address();
            return stPr_IP_Address;

        }
        /// <summary>
        /// Propiedad : getUsuarioApp
        /// Devuelve el codigo del usuario de la aplicacion.
        /// </summary>
        /// <returns>stPr_UsuarioAPP</returns>
        public String getUsuarioApp()
        {
            stPr_UsuarioAPP = ObjPr_Self.getUser();
            return stPr_UsuarioAPP;
        }
        /// <summary>
        /// Propiedad : getUsuarioApp
        /// Devuelve el nombre del archivo de log de la aplicacion.
        /// </summary>
        /// <returns>stPr_ArchivoLog</returns>
        public String getArchivoLog()
        {
            stPr_ArchivoLog = ObjPr_Self.getFileLog(); 
            return stPr_ArchivoLog;
        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getClave_UsuarioAPP
        /// Devuelve la clave del usurio de la aplicacion
        /// </summary>
        /// <returns>stPr_Clave_UsuarioAPP = La clave del usuario de la aplicacion</returns>
         public String getClave_UsuarioAPP()
        {
            stPr_Clave_UsuarioAPP = ObjPr_Self.getUserApp_PWD();
            return stPr_Clave_UsuarioAPP;
        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getClave_Encriptada_UsuarioAPP
        /// Devuelve la clave encriptada del usuario de la aplicacion.
        /// cuando esta grabada en la base de datos,
        /// </summary>
         /// <returns>stPr_Clave_Encriptada_UsuarioApp = Clave Encriptada</returns>
        public String getClave_Encriptada_UsuarioAPP()
        {
            stPr_Clave_Encriptada_UsuarioApp = ObjPr_Self.getUserApp_PWD_Enc();
            return stPr_Clave_Encriptada_UsuarioApp;
        }
        /// <summary>
        /// Propiedad : getAcceso_SSO
        /// Devuelve el contenido de la variable privada :
        /// blPr_Acceso_SSO, para saber si se ha hecho un acceso via SSO
        /// </summary>
        /// <returns>blPr_Acceso_SSO = True, se ha hecho acceso via SSO</returns>
        public Boolean getAcceso_SSO()
        {
            blPr_Acceso_SSO = ObjPr_Self.getAccess_By_SSO();
            return blPr_Acceso_SSO;
        }
        //////////////////////////////////////

        /// <summary>
        /// Propiedad : getSalConsole
        /// Devuelve el valor de la salida a la consola en el  manejo del log.
        /// </summary>
        /// <returns>blPr_SalConsole = Salida a la consola en el manejo del log</returns>
        public bool getSalConsole()
        {
            blPr_SalConsole = ObjPr_Self.getOutLineConsole();
            return blPr_SalConsole;
        }

        /// <summary>
        /// Propiedad : getSalLog
        /// Devuelve el valor de la salida a archivo en el manejo del log.
        /// </summary>
        /// <returns>blPr_SalLog = Salida a archivo en el  manejo del log</returns>
        public bool getSalLog()
        {
            blPr_SalLog = ObjPr_Self.getOutFileLog();
            return blPr_SalLog;
        }

        /// <summary>
        /// Propiedad : getSalDialog
        /// Devuelve el valor de la salida a la pantalla en el manejo del log.
        /// </summary>
        /// <returns>blPr_SalDialog = Salida a la pantalla en el  manejo del log</returns>
        public bool getSalDialog()
        {
            blPr_SalDialog = ObjPr_Self.getOutWindow();
            return blPr_SalDialog;
        }

        /// <summary>
        /// Propiedad : getID_Ingreso_Usuario_App
        /// Devuelve el valor de la variable privada:
        /// lnPr_ID_Ingreso_Usuario_App
        /// La cual coniene Id de la tabla, T06USUARIO_ACCESOS, cuando ingresa a la aplicacion.
        /// </summary>
        /// <returns>lnPr_ID_Ingreso_Usuario_App=  Id de la tabla, T06USUARIO_ACCESOS, cuando ingresa a la aplicacion.</returns>
        public long getID_Ingreso_Usuario_App()
        {
            lnPr_ID_Ingreso_Usuario_App = ObjPr_Self.getUserApp_Login_ID();
            return lnPr_ID_Ingreso_Usuario_App;
        }



        /// <summary>
        /// Propiedad : getPuertoBD
        /// Devuelve el puerto de conexion de la base de datos.
        /// </summary>
        /// <returns>lnPr_PuertoBD = Puerto de conexion de la base de datos</returns>
        public long getPuertoBD()
        {
            lnPr_PuertoBD = ObjPr_Self.getServer_Port();
            return lnPr_PuertoBD;
        }


        /// <summary>
        /// Propiedad : getServidorPandora
        /// Devuelve el servidor pandora
        /// </summary>
        /// <returns>stPr_ServidorPandora = Nombre del servidor Pandora</returns>
        public String getServidorPandora()
        {
            stPr_ServidorPandora = ObjPr_Self.getServer_Monitor();
            return stPr_ServidorPandora;
        }


        /// <summary>
        /// Propiedad : getPuertoPandora
        /// Devuelve el puerto de conexion del servidor Pandora
        /// </summary>
        /// <returns>inPr_PuertoPandora = Puerto de conexion del servidor Pandora</returns>
        public int getPuertoPandora()
        {
            inPr_PuertoPandora = ObjPr_Self.getServer_Monitor_Port();
            return inPr_PuertoPandora;
        }


        /// <summary>
        /// Devuelve el valor del usuario Pandora
        /// </summary>
        /// <returns>Devuelve el valor de la variable privada : stPr_Usuario_Pandora</returns>
        public String getUsuario_Pandora()
        {
            stPr_Usuario_Pandora = ObjPr_Self.getServer_Monitor_UID();
            return stPr_Usuario_Pandora;
        }

        /// <summary>
        /// Devuelve la clave del usuario de Pandora.
        /// </summary>
        /// <returns>Devuelve el valor de la variable privada : stPr_Clave_Usuario_Pandora</returns>
        public String getClave_Usuario_Pandora()
        {
            stPr_Clave_Usuario_Pandora = ObjPr_Self.getServer_Monitor_UID_PWD();
            return stPr_Clave_Usuario_Pandora;
        }

        

        //////////////////////////////////////
        /// <summary>
        /// Metodo : setNombreServidor
        /// Cambia el nombre del servidor
        /// stPr_NombreServidor
        /// </summary>
        /// <param name="stDato">nuevo nombre del servidor</param>
        public void setNombreServidor(String stDato)
        {
            stPr_NombreServidor = stDato;
            ObjPr_Self.setServerName(stPr_NombreServidor);
        }

        //////////////////////////////////////
        /// <summary>
        /// Metodo : setNombreBDSql
        /// Cambia el nombre de la base de datos. 
        /// stPr_NombreBdSql
        /// </summary>
        /// <param name="stDato">nuevo nombre de la base de datos</param>
        public void setNombreBDSql(String stDato)
        {
            stPr_NombreBdSql = stDato;
            ObjPr_Self.setDataBaseName(stPr_NombreBdSql);
        }
        //////////////////////////////////////
        /// <summary>
        /// Metodo : setRutaBD
        /// Cambia la ruta de la base de datos. 
        /// stPr_RutaBD
        /// </summary>
        /// <param name="stDato">nueva ruta base de datos</param>
        public void setRutaBD(String stDato)
        {
            stPr_RutaBD = stDato;
            ObjPr_Self.setDataBasePath(stPr_RutaBD);
        }
        //////////////////////////////////////
        /// <summary>
        /// Metodo : setNombreArchivoAccess
        /// Cambia el nombre del archivo de access.
        /// stPr_NombreArchivoAccess
        /// </summary>
        /// <param name="stDato">nuevo nombre archivo base de datos acess</param>
        public void setNombreArchivoAccess(String stDato)
        {
            stPr_NombreArchivoAccess = stDato;
            ObjPr_Self.setFileName_Access(stPr_NombreArchivoAccess);
        }
        //////////////////////////////////////
        /// <summary>
        /// Metodo : setIDUsuario_BD
        /// Cambia el codigo del usuario que se conecta a la base de datos.
        /// stPr_IdUsuario_BD
        /// </summary>
        /// <param name="stDato">nuevo codigo de uuario que se conecta a la base de datos</param>
        public void setIDUsuario_BD(String stDato)
        {
            stPr_IdUsuario_BD = stDato;
            ObjPr_Self.setDataBase_UserID(stPr_IdUsuario_BD);
        }
        //////////////////////////////////////
        /// <summary>
        /// Metodo : setClaveUsuario_BD
        /// Cambia la clave del usuario que se conecta a la base de datos.
        /// stPr_ClaveUsuario_BD
        /// </summary>
        /// <param name="stDato">nueva clave del usuario que se conecta a la base de datos</param>
        public void setClaveUsuario_BD(String stDato)
        {
            stPr_ClaveUsuario_BD = stDato;
            ObjPr_Self.setDataBase_UserPWD(stPr_ClaveUsuario_BD);
        }
        //////////////////////////////////////
        /// <summary>
        /// Metodo : setTipoMotorBD
        /// Cambia el tipo de motor de la base de datos.
        /// inPr_TipoMotorBD
        /// </summary>
        /// <param name="inDato">nuevo tipo de motor de la base de datos</param>
        public void setTipoMotorBD(inDB_Types inDato)
        {
            inPr_TipoMotorBD = inDato;
            //
            NBToolsNet.CLNBTN_IQy.inDB_Types Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
            switch (inPr_TipoMotorBD)
            {
                case inDB_Types.BD_TYPE_SQLSERVER:
                    Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                    break;
                case inDB_Types.BD_TYPE_ACCESS:
                    Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS;
                    break;
                case inDB_Types.BD_TYPE_ORACLE:
                    Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE;
                    break;
                case inDB_Types.BD_TYPE_MYSQL:
                    Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL;
                    break;
                case inDB_Types.BD_TYPE_SYBASE:
                    Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE;
                    break;
                case inDB_Types.BD_TYPE_POSTGRESQL:
                    Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL;
                    break;
                default:
                    Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                    break;
            }
            ObjPr_Self.setDataBaseEngine_Type(Aux);
        }
        //////////////////////////////////////
        /// <summary>
        /// Metodo : setTipoConexion
        /// Cambia el tipo de conexion a la base de datos.
        /// inPr_TipoConexion
        /// </summary>
        /// <param name="inDato">nuevo tipo de conexion a la base de datos</param>
        public void setTipoConexion(inConnect_Type inDato)
        {
            inPr_TipoConexion = inDato;
            NBToolsNet.CLNBTN_IQy.inConnect_Type Aux = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
            switch (inPr_TipoConexion)
            {
                case inConnect_Type.TYPE_1_CONNECT_USER_SQL:
                    Aux = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                    break;
                case inConnect_Type.TYPE_2_CONNECT_USER_APP:
                    Aux = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_2_CONNECT_USER_APP;
                    break;
                case inConnect_Type.TYPE_3_CONNECT_USER_WIN:
                    Aux = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN;
                    break;
                case inConnect_Type.TYPE_4_CONNECT_USER_INFO_FENIX:
                    Aux = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT;
                    break;
                case inConnect_Type.TYPE_5_CONNECT_PANDORA_BOX:
                    Aux = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN;
                    break;
                default:
                    Aux = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                    break;
            }
            ObjPr_Self.setDataBaseConn_Type(Aux);
        }
        //////////////////////////////////////
        /// <summary>
        /// Metodo : setURL_BD
        /// Cambia la URL de la base de datos.
        /// setURL_BD
        /// </summary>
        /// <param name="stDato">nuevo URL de la base de datos</param>
        public void setURL_BD(String stDato)
        {
            stPr_URL_BD = stDato;
            ObjPr_Self.setServer_URL(stPr_URL_BD);
        }
        //////////////////////////////////////
        /// <summary>
        /// Metodo : setIP_Address
        /// Cambia la IP de la base de datos.
        /// stPr_IP_Address
        /// </summary>
        /// <param name="stDato">nueva IP de la base de datos</param>
        public void setIP_Address(String stDato)
        {
            stPr_IP_Address = stDato;
            ObjPr_Self.setServer_IP_Address(stPr_IP_Address);
        }
        /// <summary>
        /// Metodo : set_UsuarioApp
        /// Cambia el codigo del usuario de la aplicacion.
        /// stPr_UsuarioAPP
        /// </summary>
        /// <param name="stDato">Codigo del usuario de la aplicacion</param>
        public void set_UsuarioApp(String stDato)
        {
            stPr_UsuarioAPP = stDato;
            ObjPr_Self.setUser(stPr_UsuarioAPP);
        }
        /// <summary>
        /// Metodo : set_ArchivoLog
        /// Cambia el nombre del archivo log de la  aplicacion.
        /// stPr_ArchivoLog
        /// 
        /// </summary>
        /// <param name="stDato">nuevo nombre del log de la aplicacion</param>
        public void set_ArchivoLog(String stDato)
        {
            stPr_ArchivoLog = stDato;
            ObjPr_Self.setFileLog(stPr_ArchivoLog);
        }
        /////////////////////////////////////////////////
        
        /// <summary>
        /// Metodo : set_Clave_UsuarioAPP
        /// permite definir la clave sin encriptar el usuario de la aplicacion
        /// </summary>
        /// <param name="stDato">La clave sin encriptar</param>
        public void set_Clave_UsuarioAPP(String stDato)
        {
            stPr_Clave_UsuarioAPP = stDato;
            ObjPr_Self.setUserApp_PWD(stPr_Clave_UsuarioAPP);
        }
        /// <summary>
        /// Metodo : set_Clave_Encriptada_UsuarioAPP
        /// permite defnir la clave encriptada el usuario de la aplicacion,
        /// </summary>
        /// <param name="stDato">Clave encriptada el usuario de la aplicacion</param>
        public void set_Clave_Encriptada_UsuarioAPP(String stDato)
        {
            stPr_Clave_Encriptada_UsuarioApp = stDato;
            ObjPr_Self.setUserApp_PWD_Enc(stPr_Clave_Encriptada_UsuarioApp);
        }
        /// <summary>
        /// Metodo : set_Acceso_SSO
        /// Permite cambiar la variable privada, blPr_Acceso_SSO
        /// para indicar si se hizo un acceso via SSO
        /// </summary>
        /// <param name="blDato">True o False</param>
        public void set_Acceso_SSO(Boolean blDato)
        {
            blPr_Acceso_SSO = blDato;
            ObjPr_Self.setAccess_By_SSO(blPr_Acceso_SSO);
        }

        /// <summary>
        /// Metodo : setSalConsole
        /// Permite cambiar el valor de la salida a la consola en el  manejo del log.
        /// </summary>
        /// <param name="bl_SalConsole">Salida a la consola en el manejo del log.</param>
        public void setSalConsole(bool bl_SalConsole)
        {
            blPr_SalConsole = bl_SalConsole;
            ObjPr_Self.setOutLineConsole(blPr_SalConsole);
        }

        /// <summary>
        /// Metodo : setSalLog
        /// Permite cambiar el valor de la a archivo en el  manejo del log.
        /// </summary>
        /// <param name="blPr_SalLog">Salida a  archivo en el manejo del log.</param>
        public void setSalLog(bool bl_SalLog)
        {
            blPr_SalLog = bl_SalLog;
            ObjPr_Self.setOutFileLog(blPr_SalLog);
        }


        /// <summary>
        /// Metodo : setDialog
        /// Permite cambiar el valor de la salida a la pantalla en el  manejo del log.
        /// </summary>
        /// <param name="blPr_SalDialog">Salida a la pantalla en el manejo del log.</param>
        public void setDialog(bool bl_Dialog)
        {
            blPr_SalDialog = bl_Dialog;
            ObjPr_Self.setOutWindow(blPr_SalDialog);
        }

        /// <summary>
        /// Metodo : setID_Ingreso_Usuario_App
        /// Permite cambiar el valor de la variable privada:
        /// lnPr_ID_Ingreso_Usuario_App
        /// La cual coniene Id de la tabla, T06USUARIO_ACCESOS, cuando ingresa a la aplicacion.
        /// </summary>
        /// <param name="ln_ID_Ingreso_Usuario_App">Valor del Id de la tabla T06USUARIO_ACCESOS, cuando ingresa a la aplicacion. </param>
        public void setID_Ingreso_Usuario_App(long ln_ID_Ingreso_Usuario_App)
        {
            lnPr_ID_Ingreso_Usuario_App = ln_ID_Ingreso_Usuario_App;
            ObjPr_Self.setUserApp_Login_ID(lnPr_ID_Ingreso_Usuario_App);
        }


        /// <summary>
        /// Metodo : setPuertoBD
        /// Permite cambiar la variable privada:
        /// lnPr_PuertoBD
        /// Puerto de Conexion de la base de datos.
        /// </summary>
        /// <param name="ln_PuertoBD">Puerto de Conexion de la base de datos.</param>
        public void setPuertoBD(long ln_PuertoBD)
        {
            lnPr_PuertoBD = ln_PuertoBD;
            ObjPr_Self.setServer_Port(lnPr_PuertoBD);
        }



        /// <summary>
        /// Metodo : setServidorPandora
        /// Permite cambiar la variable privada:
        /// stPr_ServidorPandora
        /// Nombre del servidor Pandora.
        /// </summary>
        /// <param name="st_ServidorPandora">Nombre del servidor Pandora.</param>
        public void setServidorPandora(String st_ServidorPandora)
        {
            stPr_ServidorPandora = st_ServidorPandora;
            ObjPr_Self.setServer_Monitor(stPr_ServidorPandora);
        }


        /// <summary>
        /// Metodo : setPuertoPandora
        /// Permite cambiar la variable privada:
        /// lnPr_PuertoPandora
        /// Puerto de Conexion del servidor Pandora
        /// </summary>
        /// <param name="in_PuertoPandora">Puerto de Conexion del servidor Pandora.</param>
        public void setPuertoPandora(int in_PuertoPandora)
        {
            inPr_PuertoPandora = in_PuertoPandora;
            ObjPr_Self.setServer_Monitor_Port(inPr_PuertoPandora);
        }


        /// <summary>
        /// Permite cambiar el valor de la variable privada, stPr_Usuario_Pandora, que tiene el usuario de Pandora.
        /// </summary>
        /// <param name="st_Usuario_Pandora">El usuario de pandora.</param>
        public void setUsuario_Pandora(String st_Usuario_Pandora)
        {
            stPr_Usuario_Pandora = st_Usuario_Pandora;
            ObjPr_Self.setServer_Monitor_UID(stPr_Usuario_Pandora);
        }


        /// <summary>
        /// Permite cambiar el valor de la variable privada, stPr_Clave_Usuario_Pandora, que tiene la clave deel usuario de Pandora.
        /// </summary>
        /// <param name="st_Usuario_Pandora">La clave del usuario de pandora.</param>
        public void setClave_Usuario_Pandora(String st_Clave_Usuario_Pandora)
        {
            stPr_Clave_Usuario_Pandora = st_Clave_Usuario_Pandora;
            ObjPr_Self.setServer_Monitor_UID_PWD(stPr_Clave_Usuario_Pandora);
        }




    }
}
