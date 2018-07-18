using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.ExceptionServices;


namespace _C_ProgRes
{
    public class ClasX_DBQuery
    {
        private Boolean blPr_Conectado = false; 
        private String stPr_ConnectionString = "" ; 
        private String stPr_ConnectionString_XGrid = "" ; 
        //
        private String stPr_NombreServidor= "" ; // Nombre del servidor SQL
        private String stPr_NombreBdSql= "" ; // Nombre de la base de datos
        //
        private String stPr_RutaBd= "" ; // Ruta de la base de datos.
        private String stPr_NombreArchivoAccess= "" ; // Nombre del archivo de Access.
        private String stPr_IdUsuario_BD= "" ; // Codigo del usuario
        private String stPr_ClaveUsuario_BD= "" ; // Password del usuario
        private String stPr_URL= "" ; // URL de la base de datos. del usuario
        private String stPr_IPAdress= "" ; // Direccion IP de la base de datos.f
        //
        private ClasX_DBInfo.inDB_Types inPr_TipoMotorBd = 0; // Tipo de motor de Base de Datos
        private ClasX_DBInfo.inConnect_Type inPr_TipoConexion = 0; // Tipo de Conexion
        //
        private ClasX_DBInfo ObjPr_BDInfo = null ; //  Variable del tipo ClasX_DbInfo, con la informacion de la base de datos.
        //
        private String stPr_SqlCommand = ""; // Comando SQL que se va a ejecutar
        //
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_Qy ObjPr_Self = null;
        //
        private Boolean blPr_GrabaLog_SQL = true; // Para saber si debe grabar las instrucciones SQL, en el archivo de Log cuando las ejecuta
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo

        // Para saber si se ejecuto bien o no el query
        private Boolean blPr_EjecutoQueryOK = false;
        ////////////////////////////////////////////////////
        private long lnPr_PuertoBD = 0; // Puerto de conexion a la base de datos.
        private String stPr_DirTemporal_Pandora = ""; // Directorio temporal para conexiones via ConectoSQL de Pandora.


        /// <summary>
        /// constructor de la clase 
        /// sin parametros
        /// </summary>
        public ClasX_DBQuery()
        {
            ObjPr_Self = new NBToolsNet.CLNBTN_Qy(stPr_Info);
        }
        /// <summary>
        /// Constructor de la clase 
        /// con parametros de usuario y path y nombre d archivo de log,
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo de usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path y nombre del archivo log</param>
        public ClasX_DBQuery(String st_UsuarioApp, String st_ArchivoLog)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            ObjPr_Self = new NBToolsNet.CLNBTN_Qy(st_UsuarioApp, st_ArchivoLog, stPr_Info);
        }


        /// <summary>
        /// Constructor de la clase con los parametros de usuario y path archivo log y una bandera indicando si graba o no las instruciones SQL
        /// en el archivo de log, cada vez que se ejecute alguna instruccion sobre la base de datos.
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo de usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path y nombre del archivo log</param>
        /// <param name="bl_GrabaLog_InstruccionesSQL">Indica si graba o no las instruccion SQL, en el archivo de log, cada vez que se ejecutan</param>
        public ClasX_DBQuery(String st_UsuarioApp, String st_ArchivoLog, Boolean bl_GrabaLog_InstruccionesSQL)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            blPr_GrabaLog_SQL = bl_GrabaLog_InstruccionesSQL;
            ObjPr_Self = new NBToolsNet.CLNBTN_Qy(st_UsuarioApp, st_ArchivoLog,bl_GrabaLog_InstruccionesSQL ,  stPr_Info);
        }


        /// <summary>
        /// Constructor de la clase 
        /// con parametros de usuario y path y nombre d archivo de log,
        /// Y parametros para el tratamiento del log.
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo de usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path y nombre del archivo log</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_DBQuery(String st_UsuarioApp, String st_ArchivoLog, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo;
            ObjPr_Self = new NBToolsNet.CLNBTN_Qy(st_UsuarioApp, st_ArchivoLog, bl_SalidaConsola, bl_SalidaLog, bl_SalidaDialogo, stPr_Info);
        }

        /// <summary>
        /// Constructor de la clase con los parametros de usuario y path archivo log y una bandera indicando si graba o no las instruciones SQL
        /// en el archivo de log, cada vez que se ejecute alguna instruccion sobre la base de datos.
        /// Y para sber el tratamiento del log.
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo de usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path y nombre del archivo log</param>
        /// <param name="bl_GrabaLog_InstruccionesSQL">Indica si graba o no las instruccion SQL, en el archivo de log, cada vez que se ejecutan</param>
        ///  <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_DBQuery(String st_UsuarioApp, String st_ArchivoLog, Boolean bl_GrabaLog_InstruccionesSQL, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            blPr_GrabaLog_SQL = bl_GrabaLog_InstruccionesSQL;
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo;
            ObjPr_Self = new NBToolsNet.CLNBTN_Qy(st_UsuarioApp, st_ArchivoLog, bl_GrabaLog_InstruccionesSQL , bl_SalidaConsola, bl_SalidaLog, bl_SalidaDialogo, stPr_Info);
        }



        /// <summary>
        /// Constructor de la clase con los parametros de usuario y path archivo log y una bandera indicando si graba o no las instruciones SQL
        /// en el archivo de log, cada vez que se ejecute alguna instruccion sobre la base de datos.
        /// Y para sber el tratamiento del log.
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo de usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path y nombre del archivo log</param>
        /// <param name="bl_GrabaLog_InstruccionesSQL">Indica si graba o no las instruccion SQL, en el archivo de log, cada vez que se ejecutan</param>
        ///  <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        /// /// <param name="st_DirTemporal_Pandora">nombre del directorio que se utiliza de manera temporal para recibir los archivos del conecto de Pandora SQL. Ejemplo "C:\\PandoraTemporal"</param>
        public ClasX_DBQuery(String st_UsuarioApp, String st_ArchivoLog, Boolean bl_GrabaLog_InstruccionesSQL, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo, String st_DirTemporal_Pandora)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            blPr_GrabaLog_SQL = bl_GrabaLog_InstruccionesSQL;
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo;
            //
            stPr_DirTemporal_Pandora = st_DirTemporal_Pandora;
            ObjPr_Self = new NBToolsNet.CLNBTN_Qy(st_UsuarioApp, st_ArchivoLog, bl_GrabaLog_InstruccionesSQL, bl_SalidaConsola, bl_SalidaLog, bl_SalidaDialogo, st_DirTemporal_Pandora, stPr_Info);
        }


        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getConectado
        /// Devuelve si esta conectado o no.
        /// </summary>
        /// <returns>
        /// blPr_Conectado
        /// </returns>
        public Boolean getConectado()
        {
            blPr_Conectado = ObjPr_Self.getIs_Connected();
            return blPr_Conectado;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getConnectionString
        /// Devuelve el string de conexion a la base de datos.
        /// </summary>
        /// <returns>
        /// stPr_ConnectionString
        /// </returns>
        public String getConnectionString()
        {
            stPr_ConnectionString = ObjPr_Self.getConnString();
            return stPr_ConnectionString;

        }
         //////////////////////////////////////
        /// <summary>
        /// Propiedad : getConnectionStringXGrid
        /// Devuelve el string de conexion a la base de datos, para utilizar con un grid.
        /// </summary>
        /// <returns>
        /// stPr_ConnectionString_XGrid
        /// </returns>
        public String getConnectionStringXGrid()
        {
            stPr_ConnectionString_XGrid = ObjPr_Self.getConnString4Grid();
            return stPr_ConnectionString_XGrid;

        }
        //////////////////////////////////////
        /// <summary>
        /// Propiedad : getSqlCommand
        /// Devuelve la instruccion SQL que se esta ejecutando.
        /// </summary>
        /// <returns>
        /// stPr_SqlCommand
        /// </returns>
        public String getSqlCommand()
        { // Inicio del public String getSqlCommand(
            //
            stPr_SqlCommand = ObjPr_Self.getst_Statement_Sql();
            return stPr_SqlCommand;
            //
        } // fin del public String getSqlCommand(

        /// <summary>
        /// Propiedad : getGrabaLog_SQL
        /// Devuelve si debe o no grabar las instrucciones SQL, en el archivo de log.
        /// </summary>
        /// <returns>blPr_GrabaLog_SQL= Para saber si graba o no las instruciones SQL, en el archivo de log</returns>
        public Boolean getGrabaLog_SQL()
        {
            blPr_GrabaLog_SQL = ObjPr_Self.getWriteOutSql_Stmt();
            return blPr_GrabaLog_SQL;
        }

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
        /// Propiedad : getEjecutoQueryOK
        /// Devuele true o false, si el ulitmo query se ejecuto correctamente.
        /// </summary>
        /// <returns>blPr_EjecutoQueryOK = true o fakse del estado del ultimo query.</returns>
        public bool getEjecutoQueryOK()
        {
            blPr_EjecutoQueryOK = ObjPr_Self.getSuccessQueryExecution();
            return blPr_EjecutoQueryOK;
        }



        /// <summary>
        /// Propiedad : getDirTemporal_Pandora
        /// Devuele El nombre del directorio que se utiliza de manera temporal para recibir los archivos del conecto de Pandora SQL.
        /// </summary>
        /// <returns>stPr_DirTemporal_Pandora = Nombre del Directorio temporal para recibir los archivos de pandora.</returns>
        public String  getDirTemporal_Pandora()
        {
            stPr_DirTemporal_Pandora = ObjPr_Self.getServerMonitorPathTemp();
            return stPr_DirTemporal_Pandora;
        }
        



        /// <summary>
        /// Metodo : setGrabaLog_SQL
        /// Permite definir si graba o no las instrucciones SQL en el archivo de log.
        /// Cambia el valor de la variable privada : blPr_GrabaLog_SQL
        /// </summary>
        /// <param name="bl_GRabaLog_SQL">Indica si graba o no las instrucciones SQL en el archivo de log</param>
        public void setGrabaLog_SQL(Boolean bl_GRabaLog_SQL)
        {
            blPr_GrabaLog_SQL = bl_GRabaLog_SQL;
            ObjPr_Self.setWriteOutSql_Stmt(blPr_GrabaLog_SQL);
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : setNombreServidor
        /// Cambia el nombre del servidor
        /// stPr_NombreServidor
        /// </summary>
        /// <param name="stDato">nuevo nombre del servidor</param>
        public void setInfoBD(ClasX_DBInfo stObjInfoBd)
        {
            try
            {
                ObjPr_BDInfo = stObjInfoBd;
                if (stObjInfoBd != null)
                {
                    //stObjInfoBd
                    // Asigna informacion de :
                    // Servidor
                    stPr_NombreServidor = ObjPr_BDInfo.getNombreServidor();
                    // Nombre base de datos.
                    stPr_NombreBdSql = ObjPr_BDInfo.getNombreBaseDatos(); 
                    // Ruta de la base de datos
                    stPr_RutaBd = ObjPr_BDInfo.getRutaBD(); 
                    // Nombre archvivo access
                    stPr_NombreArchivoAccess = ObjPr_BDInfo.getNombreArchivoAccess(); 
                    // Codigo de usuario de la base de datos
                    stPr_IdUsuario_BD = ObjPr_BDInfo.getIdUsuario_BD(); 
                    // Clave del usuario de la base de datos.
                    stPr_ClaveUsuario_BD = ObjPr_BDInfo.getClaveUsuario_BD();
                    // URL 
                    stPr_URL = ObjPr_BDInfo.getURL_BD();
                    // IP Address
                    stPr_IPAdress = ObjPr_BDInfo.getIP_Address();
                    // El tipo de motor de la base de datos
                    inPr_TipoMotorBd = ObjPr_BDInfo.getTipoBD();
                    // Tipo de conexion
                    inPr_TipoConexion = ObjPr_BDInfo.getTipoConexion();
                    // Codigo del usuario de la aplicacion
                    stPr_UsuarioAPP = ObjPr_BDInfo.getUsuarioApp();
                    // Nombre el Archivo Log.
                    stPr_ArchivoLog = ObjPr_BDInfo.getArchivoLog();
                    // El puerto de la base de datos
                    lnPr_PuertoBD = ObjPr_BDInfo.getPuertoBD();
                    //
                    NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                    //
                    O_Aux.setServerName(stPr_NombreServidor);
                    O_Aux.setDataBaseName(stPr_NombreBdSql);
                    O_Aux.setDataBasePath(stPr_RutaBd);
                    O_Aux.setFileName_Access(stPr_NombreArchivoAccess);
                    O_Aux.setDataBase_UserID(stPr_IdUsuario_BD);
                    O_Aux.setDataBase_UserPWD(stPr_ClaveUsuario_BD);
                    O_Aux.setServer_URL(stPr_URL);
                    O_Aux.setServer_IP_Address(stPr_IPAdress);
                    //
                    O_Aux.setUser(stPr_UsuarioAPP);
                    O_Aux.setFileLog(stPr_ArchivoLog);
                    O_Aux.setServer_Port(lnPr_PuertoBD);
                    //
                    O_Aux.setDataBase_UserPWD(ObjPr_BDInfo.getClaveUsuario_BD());
                    O_Aux.setUserApp_PWD(ObjPr_BDInfo.getClave_UsuarioAPP());
                    O_Aux.setUserApp_PWD_Enc(ObjPr_BDInfo.getClave_Encriptada_UsuarioAPP());
                    O_Aux.setAccess_By_SSO(ObjPr_BDInfo.getAcceso_SSO());
                    O_Aux.setUserApp_Login_ID(ObjPr_BDInfo.getID_Ingreso_Usuario_App());
                    //
                    O_Aux.setServer_Monitor(ObjPr_BDInfo.getServidorPandora());
                    O_Aux.setServer_Monitor_Port(ObjPr_BDInfo.getPuertoPandora());
                    O_Aux.setServer_Monitor_UID(ObjPr_BDInfo.getUsuario_Pandora());
                    O_Aux.setServer_Monitor_UID_PWD(ObjPr_BDInfo.getClave_Usuario_Pandora());
                    //
                    NBToolsNet.CLNBTN_IQy.inDB_Types Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                    switch (inPr_TipoMotorBd)
                    {
                        case ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER:
                            Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                            break;
                        case ClasX_DBInfo.inDB_Types.BD_TYPE_ACCESS:
                            Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS;
                            break;
                        case ClasX_DBInfo.inDB_Types.BD_TYPE_ORACLE:
                            Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE;
                            break;
                        case ClasX_DBInfo.inDB_Types.BD_TYPE_MYSQL:
                            Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL;
                            break;
                        case ClasX_DBInfo.inDB_Types.BD_TYPE_SYBASE:
                            Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE;
                            break;
                        case ClasX_DBInfo.inDB_Types.BD_TYPE_POSTGRESQL:
                            Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL;
                            break;
                        default:
                            Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                            break;
                    }
                    O_Aux.setDataBaseEngine_Type(Aux);
                    //
                    NBToolsNet.CLNBTN_IQy.inConnect_Type Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                    switch (inPr_TipoConexion)
                    {
                        case ClasX_DBInfo.inConnect_Type.TYPE_1_CONNECT_USER_SQL:
                            Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                            break;
                        case ClasX_DBInfo.inConnect_Type.TYPE_2_CONNECT_USER_APP:
                            Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_2_CONNECT_USER_APP;
                            break;
                        case ClasX_DBInfo.inConnect_Type.TYPE_3_CONNECT_USER_WIN:
                            Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN;
                            break;
                        case ClasX_DBInfo.inConnect_Type.TYPE_4_CONNECT_USER_INFO_FENIX:
                            Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT;
                            break;
                        case ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_PANDORA_BOX:
                            Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN;
                            break;
                        default:
                            Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                            break;
                    }
                    O_Aux.setDataBaseConn_Type(Aux_1);
                    //
                    ObjPr_Self.setDataBaseInfo(O_Aux);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "setInfoBD. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "setInfoBD", "", ex.Message.ToString(), stPr_NombreBdSql,"");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
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
        /// Metodo : setEjecutoQueryOK
        /// Permite cambiar el valor del estado del ultimo query ejecutado. 
        /// Cambia la variable privada blPr_EjecutoQueryOK 
        /// </summary>
        /// <param name="bl_EjecutoQueryOK">Estado del ultimo query</param>
        public void setEjecutoQueryOK(bool bl_EjecutoQueryOK)
        {
            blPr_EjecutoQueryOK = bl_EjecutoQueryOK;
            ObjPr_Self.setSuccessQueryExecution(blPr_EjecutoQueryOK);
        }


        /// <summary>
        /// Metodo : setDirTemporal_Pandora
        /// Permite cambiar el valor del nomrbe del directorio temporal para conexiones via ConectoSQL de Pandora. 
        /// Cambia la variable privada stPr_DirTemporal_Pandora 
        /// </summary>
        /// <param name="st_DirTemporal_Pandora">Nombre del directorio temporal para conexiones via ConectoSQL de Pandora.</param>
        public void setDirTemporal_Pandora(String st_DirTemporal_Pandora)
        {
            stPr_DirTemporal_Pandora = st_DirTemporal_Pandora;
            ObjPr_Self.setServerMonitorPathTemp(stPr_DirTemporal_Pandora);
        }




        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ConectarBD
        /// Conecta a la base de datos.
        /// </summary>
        /// <param name="stObjInfoBd"></param>
        public void ConectarBD()
        {
            try
            {
                ObjPr_Self.ConnectDataBase();
                blPr_Conectado = ObjPr_Self.getIs_Connected();
                stPr_ConnectionString = ObjPr_Self.getConnString();
                stPr_ConnectionString_XGrid = ObjPr_Self.getConnString4Grid();
            }
            catch (System.AccessViolationException ex_0)
            {
                blPr_Conectado = false;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "ConectarBD. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                blPr_Conectado = false;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "ConectarBD", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        
        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : UPDATE
        /// Arma la frase UPDATE
        /// </summary>
        /// <param name="st_Update"></param>
        /////////////////////////////////////////////////
        public void UPDATE(String st_Update)
        { // Inicio del public void UPDATE(
            try
            {
                ObjPr_Self.ToDo_UPDATE(st_Update);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "UPDATE. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "UPDATE", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void UPDATE(



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : SET
        /// Arma parte de a instruccion SET 
        /// </summary>
        /// <param name="st_Campo">Campo</param>
        /// <param name="st_Valor">Valor del Campo</param>
        public void SET(String st_Campo , String st_Valor)
        { // Inicio del public void SET(
            try
            {
                ObjPr_Self.ToDo_SET(st_Campo, st_Valor);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "SET. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "SET", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void SET(


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : SELECT
        /// Inicia la instruccion SELECT 
        /// </summary>
        /// <param name="st_Select"></param>
       public void SELECT(String st_Select)
        { // Inicio del public void SELECT(
            try
            {
                ObjPr_Self.ToDo_SELECT(st_Select);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "SELECT. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "SELECT", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void SELECT(


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : FROM
        /// arma parte de la instruccion FROM 
        /// </summary>
        /// <param name="st_From">Nombre Tabla para el FROM</param>
        public void FROM(String st_From)
        { // Inicio del public void FROM(
            try
            {
                ObjPr_Self.ToDo_FROM(st_From);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "FROM. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "FROM", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void FROM(



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : WHERE
        /// Arma el WHERE de la instruccion SQL
        /// </summary>
        /// <param name="st_Where">Campo para el where</param>
        /// <param name="st_Valor">Valor del campo</param>
        public void WHERE(String st_Where, String st_Valor = "")
        { // Inicio del public void WHERE(
            try
            {
                ObjPr_Self.ToDo_WHERE(st_Where, st_Valor);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "WHERE. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "WHERE", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void WHERE(


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : AND
        /// Arma el AND de la sentencia SQL
        /// </summary>
        /// <param name="st_And">Campo para el AND</param>
        /// <param name="st_Valor">Valor del Campo</param>
        public void AND(String st_And, String st_Valor)
        { // Inicio del public void AND(
            try
            {
                ObjPr_Self.ToDo_AND(st_And, st_Valor);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "AND. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "AND", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void AND(

       [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ORDERBY
        /// Arma el ORDERBY de la instruccion SQL
        /// </summary>
        /// <param name="st_Orderby">Parte de la instruccion para el ORDERBY</param>
        public void ORDERBY(String st_Orderby)
        { // Inicio del public void ORDERBY(
            try
            {
                ObjPr_Self.ToDo_ORDERBY(st_Orderby);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "ORDERBY. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "ORDERBY", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void ORDERBY(

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : LIMIT
        /// Arma el LIMIT de la instruccion SQL
        /// </summary>
        /// <param name="st_Limit">Parte de la instruccion para el LIMIT</param>
        public void LIMIT(String st_Limit)
        { // Inicio del public void LIMIT(String st_Limit)
            try
            {
                ObjPr_Self.ToDo_LIMIT(st_Limit);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "LIMIT. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "LIMIT", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void LIMIT(


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : OFFSET
        /// Arma el OFFSET de la instruccion SQL
        /// </summary>
        /// <param name="st_OffSet">Parte de la instruccion para el OFFSET</param>
        public void OFFSET(String st_OffSet)
        { // Inicio del public void OFFSET(String st_OffSet)
            try
            {
                ObjPr_Self.ToDo_OFFSET(st_OffSet);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "OFFSET. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "OFFSET", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void OFFSET(       

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : PARTE_FINAL_QUERY
        /// Arma el string que entra como parametro en la parte final de la instruccion SQL
        /// </summary>
        /// <param name="st_ParteFinal">Parte final de la instruccion para el OFFSET</param>
        public void PARTE_FINAL_QUERY(String st_ParteFinal)
        { // Inicio del public void PARTE_FINAL_QUERY(String st_ParteFinal)
            try
            {
                ObjPr_Self.ToDo_AT_END_OF_STMT(st_ParteFinal);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "PARTE_FINAL_QUERY. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "PARTE_FINAL_QUERY", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void PARTE_FINAL_QUERY(       


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : DELETE
        /// Inicia la parte del DELETE de la instruccion SQL
        /// </summary>
        /// <param name="st_Delete">Nombre de la Tabla para el DELETE</param>
        public void DELETE(String st_Delete)
        { // Inicio del public void DELETE(
            try
            {
                ObjPr_Self.ToDo_DELETE(st_Delete);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "DELETE. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "DELETE", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void DELETE(


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : INSERT
        /// Inicia la parte del INSERT de la instruccion SQL
        /// </summary>
        /// <param name="st_Insert">Nombre de la tabla para el INSERT</param>
        public void INSERT(String st_Insert)
        { // Inicio del public void INSERT(
            try
            {
                ObjPr_Self.ToDo_INSERT(st_Insert);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "INSERT. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "INSERT", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void INSERT(


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : SP_NON_SELECT
        /// Utilizado para ejecutar SPS que no seleccionan informacion
        /// o que devuelven mensajes , como los sps de fenix.
        /// </summary>
        /// <param name="st_SP"></param>
        public void SP_NON_SELECT(String st_SP)
        { // Inicio del public void SP_NON_SELECT(
            try
            {
                ObjPr_Self.ToDo_SP_NON_SELECT(st_SP);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "SP_NON_SELECT. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "SP_NON_SELECT", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void SP_NON_SELECT(

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : SP_SELECT
        /// Utilizado para llamar procedimientos almacenados que devuelven datos
        /// </summary>
        /// <param name="stRSP">Instruccion SQL que tiene el Sp y los parametros.</param>
        public void SP_SELECT(String stRSP)
        { // Inicio del public void SP_SELECT(
            try
            {
                ObjPr_Self.ToDo_SP_SELECT(stRSP);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "SP_SELECT. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "SP_SELECT", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void SP_SELECT(



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : CLOSE
        /// Cierra la conexion y limpia variables privadas
        /// </summary>
        public void CLOSE()
        { // Inicio del public void CLOSE(
            try
            {
                ObjPr_Self.ToDo_CLOSE();
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "CLOSE. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "CLOSE", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void CLOSE(



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : CLEAN
        /// Limpia variables privadas para iniciar un nuevo query
        /// </summary>
        public void CLEAN()
        { // Inicio del public void CLEAN(
            try
            {
                ObjPr_Self.ToDo_CLEAN();
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "CLEAN. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "CLEAN", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void CLEAN(



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : FUNCTION
        /// Define la funcio a ejecutar
        /// </summary>
        /// <param name="st_Funcion">La funcion a ejecutar</param>
        public void FUNCTION(String st_Funcion)
        { // Inicio del public void FUNCTION(
            try
            {
                ObjPr_Self.ToDo_FUNCTION(st_Funcion);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "FUNCION. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "FUNCION", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void FUNCTION(
        /////////////////////////////////////////////////

        

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : EXECUTE_SQL
        /// Sobre Carga 1
        /// Ejecuta Instrucciones Tipo SELECT o SPs que Devuelven Datos
        /// Se crea un Comando con la conexion actual a la base de datos
        /// Se crea un Reader y se devuelve un DataTable
        /// </summary>
        /// <param name="TableData">Se devuelve DataTable con los datos de la consulta.</param>
        /// <param name="st_CadenaSql">Cadena Opcional, para ejecutar esa instruccion sobre la base de datos.</param>
        public void EXECUTE_SQL(ref DataTable TableData , String st_CadenaSql = "")
        { // Inicio del public void EXECUTE_SQL
            String stL_InstruccionSQL = "";
            //
            try
            {
                ObjPr_Self.ToDo_EXECUTE_SQL(ref TableData, st_CadenaSql);
            }
            //
            catch (System.AccessViolationException ex_0)
            {
                // Deja la tabla en NULL
                TableData = null;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "EXECUTE_SQL(1). System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, stL_InstruccionSQL);
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                // Deja la tabla en NULL
                TableData = null;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "EXECUTE_SQL(1)", "", ex.Message.ToString(), stPr_NombreBdSql, stL_InstruccionSQL);
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void EXECUTE_SQL(

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : EXECUTE_SQL
        /// Sobre carga 2
        /// Ejecuta el tipo de instrucciones:
        /// INSERT, UPDATE , DELETE o Sps que no Devuelven Datos.
        /// Ejecuta la instruccion que se tiene actualmente en la clase
        /// o ejecuta la instruccion , que viene en el parametro 'st_CadenaSql'.
        /// </summary>
        /// <param name="st_CadenaSql">Instruccion SQL a ejecutar. Debe ser el tipo :  INSERT, UPDATE , DELETE o Sps que no Devuelven Datos.</param>
        public void EXECUTE_SQL(String st_CadenaSql = "")
        { // Inicio del public void EXECUTE_SQL
            //
            try
            {
                ObjPr_Self.ToDo_EXECUTE_SQL(st_CadenaSql);
            }
            //
            catch (System.AccessViolationException ex_0)
            {
                blPr_EjecutoQueryOK = false;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "EXECUTE_SQL(2). System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                blPr_EjecutoQueryOK = false;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBQuery", "EXECUTE_SQL(2)", "", ex.Message.ToString(), stPr_NombreBdSql, "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void EXECUTE_SQL(

        

       
    } // fin de class ClasX_DBQuery
}
