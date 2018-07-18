using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
// Para PostGreSQL
using Npgsql;
using NpgsqlTypes;
//using Npgsql.Design;
using System.IO;
//
using System.Text.RegularExpressions;
//
using System.Runtime.ExceptionServices;



namespace NBToolsNet
{
    public class CLNBTN_Qy
    {
        // Clase equivalente : ClasX_DBQuery			
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        private string _st_User = "CLNBTN_Qy";
        private string _st_FileLog = "C:\\Windows\\CLNBTN_Qy.log";
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Qy";


        //////////////////////////////////////////////////////////////////
        ///  Clase Para manejo de los queries y acceso a la(s) base(s) de datos
        /// Autor : Alvaro S. Quimbaya C.
        /// Fecha : Agosto 27 2012.
        /// Empresa : Strail SAS
        /// --------------------------------------------------------------------------
        /// CAMBIO IMPORTANTE: ( ASQC Julio 3 2.013 ) 
        /// --------------------------------------------------------------------------
        /// Se quito el maneo de OLEDB, para manejar conexion y queries
        /// con las dlls de cada motor, ejemplo:
        /// ------------------------------
        /// Para SQL SERVER:
        /// Se utilizan : 
        /// SqlConnection 
        /// SqlCommand 
        /// SqlDataReader
        /// ------------------------------
        /// Para MYSQL:
        /// Se utilizan :
        /// MySqlConnection 
        /// MySqlCommand 
        /// MySqlDataReader
        /// ------------------------------
        /// Para PostGreSQL
        /// NpgsqlConnection
        /// NpgsqlCommand
        /// NpgsqlDataReader
        /// ------------------------------------------------------------------------------------------------------------
        /// Y PARA LOS DEMAS MOTORES se deberan implementar sus respectivos conectores, comandos y lectores
        /// ------------------------------------------------------------------------------------------------------------
        /// Ult Mod : Alvaro S. Quimbaya C. Julio 18-19 2013
        /// Motivo  : Implementacion de validaciones para PostGreSQL
        /// 

        private Boolean _bl_Is_Connected = false;
        private String _st_ConnString = "";
        private String _st_ConnString4Grid = "";
        //
        private String stPr_Select = "";
        private String stPr_From = "";
        private String stPr_Where = "";
        private String stPr_And = "";
        private String stPr_Funcion = "";
        private String stPr_Update = "";
        private String stPr_Set = "";
        private String stPr_Campos = "";
        private String stPr_Delete = "";
        private String stPr_Insert = "";
        private String _st_Query = "";
        //
        private String stPr_NombreServidor = ""; // Nombre del servidor SQL
        private String stPr_NombreBdSql = ""; // Nombre de la base de datos
        //
        private String stPr_RutaBd = ""; // Ruta de la base de datos.
        private String stPr_NombreArchivoAccess = ""; // Nombre del archivo de Access.
        private String stPr_IdUsuario_BD = ""; // Codigo del usuario
        private String stPr_ClaveUsuario_BD = ""; // Password del usuario
        private String stPr_URL = ""; // URL de la base de datos. del usuario
        private String stPr_IPAdress = ""; // Direccion IP de la base de datos.f
        //
        private CLNBTN_IQy.inDB_Types _in_DataBaseEngine_Type = 0; // Tipo de motor de Base de Datos
        private CLNBTN_IQy.inConnect_Type _in_DataBaseConn_Type = 0; // Tipo de Conexion
        //
        private CLNBTN_IQy ObjPr_BDInfo = null; //  Variable del tipo ClasX_DbInfo, con la informacion de la base de datos.
        //
        private String _st_Statement_Sql = ""; // Comando SQL que se va a ejecutar
        //
        private Boolean _bl_WriteOutSql_Stmt = true; // Para saber si debe grabar las instrucciones SQL, en el archivo de Log cuando las ejecuta
        //////////////////////////////////////
        // Mensajes utilizados en la clase
        private const String MENSAJE_1 = "Tipo de Motor de base de datos no defino";

        //
        // Objetos de Conexion.
        private SqlConnection ConnDB_Pr_SQLSERVER = null;
        private MySqlConnection ConnDB_Pr_MYSQL = null;
        private NpgsqlConnection ConnDB_Pr_PostGreSQL = null;
        // Objetos para Comandos
        private SqlCommand CmdBD_Pr_SQLSERVER = null;
        private MySqlCommand CmdBD_Pr_MYSQL = null;
        private NpgsqlCommand CmdBD_Pr_PostGreSQL = null;
        // Objetos READER
        private SqlDataReader Rrd_Pr_SQLSERVER = null;
        private MySqlDataReader Rrd_Pr_MYSQL = null;
        private NpgsqlDataReader Rrd_Pr_PostGreSQL = null;
        // Para saber si se ejecuto bien o no el query
        private Boolean _bl_SuccessQueryExecution = false;
        ////////////////////////////////////////////////////
        private long lnPr_PuertoBD = 0; // Puerto de conexion a la base de datos.
        //
        private String _st_ServerMonitorPathTemp = ""; // Directorio temporal para conexiones via ConectoSQL de Pandora.
        private const int IN_CARACTER_CAMBIA = 22;
        //
        private const String TO_DO_INQUERY = "CONSULTA";
        private const String TO_DO_MODI = "MODIFICACION";
        private const String TO_DO_ERASE = "ELIMINAR";
        private const String TO_DO_LET_IN = "INGRESAR";
        private const String TO_DO_SP = "PROCEDURE";
        //
        private const String NEW_LINE = "\r\n"; // Caracteres para nueva linea
        //
        private int _in_CommandTimeout = 0; // Time out for sql statements
        private int _in_ConnectionTimeout = 0 ; // Time out for database connection

        //

        public CLNBTN_Qy(String LicName)
        {
            CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
        }

        public CLNBTN_Qy(String UserName, String LogFile, String LicName)
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
            }
        }

        public CLNBTN_Qy(String UserName, String LogFile, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
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
                _bl_OutFileLog = OutFileLog;
                _bl_OutLineConsole = OutLineConsole;
                _bl_OutWindow = OutWindow;
            }
        }

        public CLNBTN_Qy(String UserName, String LogFile, Boolean WriteOutSql_Stmt, String LicName)
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
                _bl_WriteOutSql_Stmt = WriteOutSql_Stmt;
            }
        }


        public CLNBTN_Qy(String UserName, String LogFile, Boolean WriteOutSql_Stmt, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
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
                _bl_WriteOutSql_Stmt = WriteOutSql_Stmt;
                //
                _bl_OutFileLog = OutFileLog;
                _bl_OutLineConsole = OutLineConsole;
                _bl_OutWindow = OutWindow; 
            }
        }


        public CLNBTN_Qy(String UserName, String LogFile, Boolean WriteOutSql_Stmt, bool OutLineConsole, bool OutFileLog, bool OutWindow, String ServerMonitorPathTemp, String LicName)
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
                _bl_WriteOutSql_Stmt = WriteOutSql_Stmt;
                //
                _bl_OutFileLog = OutFileLog;
                _bl_OutLineConsole = OutLineConsole;
                _bl_OutWindow = OutWindow;
                _st_ServerMonitorPathTemp = ServerMonitorPathTemp;
            }
        }

        public CLNBTN_IQy.inDB_Types getDataBaseEngine_Type()
        {
            return _in_DataBaseEngine_Type;
        }

        public CLNBTN_IQy.inConnect_Type get_DataBaseConn_Type()
        {
            return _in_DataBaseConn_Type;
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


        public Boolean getIs_Connected()
        {
            return _bl_Is_Connected;
        }

        public String getConnString()
        {
            return _st_ConnString;
        }

        public String getConnString4Grid()
        {
            return _st_ConnString4Grid;
        }

        public String getst_Statement_Sql()
        {
            _st_Statement_Sql = _st_Query;
            return _st_Statement_Sql;
        }

        public Boolean getWriteOutSql_Stmt()
        {
            return _bl_WriteOutSql_Stmt;
        }

        public bool getSuccessQueryExecution()
        {
            return _bl_SuccessQueryExecution;
        }


        public String getServerMonitorPathTemp()
        {
            return _st_ServerMonitorPathTemp;
        }

        public int get_in_CommandTimeout()
        {
            return _in_CommandTimeout;
        }


        public int get_in_ConnectionTimeout()
        {
            return _in_ConnectionTimeout;
        }


        public void setWriteOutSql_Stmt(Boolean Datum)
        {
            _bl_WriteOutSql_Stmt = Datum;
        }


        public void setDataBaseEngine_Type(CLNBTN_IQy.inDB_Types Datum)
        {
            _in_DataBaseEngine_Type = Datum;
        }

        public void setDataBaseConn_Type(CLNBTN_IQy.inConnect_Type Datum)
        {
            _in_DataBaseConn_Type = Datum;
        }


        [HandleProcessCorruptedStateExceptions]
        public void setDataBaseInfo(CLNBTN_IQy ObDbInfo)
        {
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ObjPr_BDInfo = ObDbInfo;
                    if (ObDbInfo != null)
                    {
                        // URL Referencia : http://ants.dif.um.es/~felixgm/pub/others/C_Sharp.pdf
                        // URL Referencia : http://social.msdn.microsoft.com/Forums/es/vcses/thread/8aae89c7-41c0-41ce-84fa-f8cde639f0b4
                        // Asigna informacion de :
                        // Servidor
                        stPr_NombreServidor = ObjPr_BDInfo.getServerName();
                        // Nombre base de datos.
                        stPr_NombreBdSql = ObjPr_BDInfo.getDataBaseName();
                        // Ruta de la base de datos
                        stPr_RutaBd = ObjPr_BDInfo.getDataBasePath();
                        // Nombre archvivo access
                        stPr_NombreArchivoAccess = ObjPr_BDInfo.getFileName_Access();
                        // Codigo de usuario de la base de datos
                        stPr_IdUsuario_BD = ObjPr_BDInfo.getDataBase_UserID();
                        // Clave del usuario de la base de datos.
                        stPr_ClaveUsuario_BD = ObjPr_BDInfo.getDataBase_UserPWD();
                        // URL 
                        stPr_URL = ObjPr_BDInfo.getServer_URL();
                        // IP Address
                        stPr_IPAdress = ObjPr_BDInfo.getServer_IP_Address();
                        // El tipo de motor de la base de datos
                        _in_DataBaseEngine_Type = ObjPr_BDInfo.getDataBaseEngine_Type();
                        // Tipo de conexion
                        _in_DataBaseConn_Type = ObjPr_BDInfo.get_DataBaseConn_Type();
                        // Codigo del usuario de la aplicacion
                        _st_User = ObjPr_BDInfo.getUser();
                        // Nombre el Archivo Log.
                        _st_FileLog = ObjPr_BDInfo.getFileLog();
                        // El puerto de la base de datos
                        lnPr_PuertoBD = ObjPr_BDInfo.getServer_Port();
                        //
                        _in_CommandTimeout = ObjPr_BDInfo.get_in_CommandTimeout();
                        if (_in_CommandTimeout < 0)
                        {
                            _in_CommandTimeout = 0;
                        }
                        _in_ConnectionTimeout = ObjPr_BDInfo.get_in_ConnectionTimeout();
                        if (_in_ConnectionTimeout < 0)
                        {
                            _in_ConnectionTimeout = 0;
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "setDataBaseInfo. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "setDataBaseInfo. Exception", "", ex.Message.ToString());
            }
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


        public void setSuccessQueryExecution(bool Datum)
        {
            _bl_SuccessQueryExecution = Datum;
        }


        public void setServerMonitorPathTemp(String Datum)
        {
            _st_ServerMonitorPathTemp = Datum;
        }


        public void set_in_CommandTimeout(int Datum)
        {
            _in_CommandTimeout = Datum;
            if (_in_CommandTimeout < 0)
            {
                _in_CommandTimeout = 0;
            }
        }

        public void set_in_ConnectionTimeout(int Datum)
        {
            _in_ConnectionTimeout = Datum;
            if (_in_ConnectionTimeout < 0)
            {
                _in_ConnectionTimeout = 0;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public void ConnectDataBase()
        {
            /// <summary>
            /// Metodo : ConectarBD
            /// Conecta a la base de datos.
            /// </summary>
            /// <param name="stObjInfoBd"></param>
            String stL_Sql = "";
            String stL_Aux = "";
            String stL_Sql_XGrid = "";
            int inL_Pos = 0;
            //->>ClasX_Utils ObjL_Utils = null;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    _st_ConnString = "";
                    _st_ConnString4Grid = "";
                    _bl_Is_Connected = true;
                    //
                    if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                    { // Inicio del  if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                        ///////////////////////////////////////
                        // Valida si el servidor de pandota esta en linea.
                        ///////////////////////////////////////
                        String stL_ServidorPandora = ObjPr_BDInfo.getServer_Monitor();
                        int inL_Puerto = ObjPr_BDInfo.getServer_Monitor_Port();
                        //
                        if (stL_ServidorPandora.Length > 0 && inL_Puerto > 0)
                        { // Inicio del  if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                            CLNBTN_CliPro ObjL_TCP = new CLNBTN_CliPro(stL_ServidorPandora, inL_Puerto, _st_User, _st_FileLog, _st_Lic);
                            // Prueba si hay conexion, con el conector SQL.
                            if (!ObjL_TCP.Connection_OK())
                            {
                                _bl_Is_Connected = false;
                            }
                        } //  FIn del  if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                        else // del if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                        { // Inicio del else del if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                            _bl_Is_Connected = false;
                        } // Fin del else del if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                        //
                        ////////////////////////////////////////////////////////////////////////
                    } // Fin del  if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                    else // del  if ( _in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN ) 
                    { // Inicio del else del  if ( _in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN ) 

                        switch (_in_DataBaseEngine_Type)
                        { // del switch (_in_DataBaseEngine_Type)
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                                ///////////////////
                                // SQl Server
                                //////////////////
                                // Arma la conexion para el grid.
                                // Provider 
                                stL_Sql_XGrid = "Provider=SQLOLEDB";
                                // Servidor
                                stL_Sql_XGrid += ";Server=" + stPr_NombreServidor;
                                // Base de datos
                                stL_Sql_XGrid += ";Database=" + stPr_NombreBdSql;
                                // Usuario
                                stL_Sql_XGrid += ";UID=" + stPr_IdUsuario_BD;
                                // Clave
                                stL_Sql_XGrid += ";PWD=" + stPr_ClaveUsuario_BD;
                                if (lnPr_PuertoBD > 0)
                                {
                                    stL_Sql_XGrid = stL_Sql_XGrid + ";Port=" + lnPr_PuertoBD;
                                }
                                if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN)
                                {
                                    // Indica que se conecta con el usuario de windows
                                    stL_Sql_XGrid += ";Integrated Security=SSPI";
                                }
                                // Arma String de conexion para el SQlConnection
                                stL_Sql = "Data Source=" + stPr_NombreServidor;
                                stL_Sql += ";Initial Catalog=" + stPr_NombreBdSql;
                                stL_Sql += ";UId=" + stPr_IdUsuario_BD;
                                stL_Sql += ";Pwd=" + stPr_ClaveUsuario_BD;
                                if (lnPr_PuertoBD > 0)
                                {
                                    stL_Sql = stL_Sql + ";Port=" + lnPr_PuertoBD;
                                }
                                if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN)
                                {
                                    // Indica que se conecta con el usuario de windows
                                    stL_Sql += ";Integrated Security=SSPI";
                                }
                                //
                                //stL_Sql += " ;Connect Timeout=" + this._in_ConnectionTimeout;
                                stL_Sql += ";Connection Timeout=" + this._in_ConnectionTimeout;
                                ConnDB_Pr_SQLSERVER = new SqlConnection(stL_Sql);
                                break;
                            //
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS:
                                ///////////
                                //
                                stL_Sql = "";
                                stL_Sql = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=False;Data Source=" + stPr_NombreArchivoAccess;
                                // Asigna a esta variable, para generar la conexion para el grid.
                                stL_Sql_XGrid = stL_Sql;
                                //
                                //--Cn.Open(stL_Sql);
                                break;
                            //
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE:
                                // Oracle
                                // Provider
                                stL_Sql = "Provider=OraOLEDB.Oracle.1";
                                //
                                stL_Sql = stL_Sql + "Persist Security Info=False";
                                // Servidor
                                stL_Sql = stL_Sql + ";Server=" + stPr_NombreServidor;
                                // Base de datos
                                stL_Sql = stL_Sql + ";Data Source=" + stPr_NombreBdSql;
                                // Usuario
                                stL_Sql = stL_Sql + ";User Id=" + stPr_IdUsuario_BD;
                                // Clave
                                stL_Sql = stL_Sql + ";Password=" + stPr_ClaveUsuario_BD;
                                //
                                // Asigna a esta variable, para generar la conexion para el grid.
                                stL_Sql_XGrid = stL_Sql;
                                //
                                //--Cn.Open(stL_Sql);
                                break;
                            //
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                                // Tomado de : http://www.recursosvisualbasic.com.ar/htm/trucos-codigofuente-visual-basic/296-connectionstring-ado.htm
                                // My SQL
                                // Provider
                                // Arma el String para el Grid.
                                stL_Sql_XGrid = "Provider=MySQLProv";
                                //
                                // Servidor
                                stL_Sql_XGrid += ";SERVER=" + stPr_NombreServidor;
                                if (lnPr_PuertoBD > 0)
                                {
                                    stL_Sql_XGrid = stL_Sql_XGrid + ";Port=" + lnPr_PuertoBD;
                                }
                                // Base de datos
                                stL_Sql_XGrid += ";DATABASE=" + stPr_NombreBdSql;
                                // Usuario
                                stL_Sql_XGrid += ";UID=" + stPr_IdUsuario_BD;
                                // Clave
                                stL_Sql_XGrid += ";PASSWORD=" + stPr_ClaveUsuario_BD;
                                //
                                // Arma el string y hace la conexion con MySqlConnection
                                //
                                stL_Sql = "Server=" + stPr_NombreServidor;
                                if (lnPr_PuertoBD > 0)
                                {
                                    stL_Sql = stL_Sql + ";Port=" + lnPr_PuertoBD;
                                }
                                stL_Sql += ";DataBase=" + stPr_NombreBdSql;
                                stL_Sql += ";UId=" + stPr_IdUsuario_BD;
                                stL_Sql += ";Pwd=" + stPr_ClaveUsuario_BD;
                                //
                                ConnDB_Pr_MYSQL = new MySqlConnection(stL_Sql);
                                //
                                break;
                            //
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE:
                                // Sybase
                                // Tomado de : http://www.connectionstrings.com/sybase-adaptive
                                // Provider=Sybase.ASEOLEDBProvider;Srvr=myASEserver,5000;Catalog=myDataBase;User Id=myUsername;Password=myPassword;
                                // Provider
                                stL_Sql = "Provider=Sybase.ASEOLEDBProvider";
                                //
                                // Servidor
                                stL_Sql = stL_Sql + ";Srvr=" + stPr_NombreServidor + ",5000";
                                // Base de datos
                                stL_Sql = stL_Sql + ";Catalog=" + stPr_NombreBdSql;
                                // Usuario
                                stL_Sql = stL_Sql + ";User Id=" + stPr_IdUsuario_BD;
                                // Clave
                                stL_Sql = stL_Sql + ";Password=" + stPr_ClaveUsuario_BD;
                                //
                                // Asigna a esta variable, para generar la conexion para el grid.
                                stL_Sql_XGrid = stL_Sql;
                                //
                                //--Cn.Open(stL_Sql);
                                break;
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                                // PostGreSQL
                                // TOmado de : http://www.connectionstrings.com/postgre-sql
                                // Tomado de : http://geeks.ms/blogs/rduarte/archive/2008/02/29/comenzando.aspx
                                // User ID=root;Password=myPassword;Host=localhost;Port=5432;Database=myDataBase; Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;
                                // Provider
                                //stL_Sql = "Provider=Sybase.ASEOLEDBProvider";
                                //
                                // Servidor
                                stL_Sql = stL_Sql + ";Server=" + stPr_NombreServidor;
                                if (lnPr_PuertoBD > 0)
                                {
                                    stL_Sql = stL_Sql + ";Port=" + lnPr_PuertoBD;
                                }
                                // Base de datos
                                stL_Sql = stL_Sql + ";Database=" + stPr_NombreBdSql;
                                // Usuario
                                stL_Sql = stL_Sql + ";User ID=" + stPr_IdUsuario_BD;
                                // Clave
                                stL_Sql = stL_Sql + ";Password=" + stPr_ClaveUsuario_BD;
                                //stL_Sql = stL_Sql + ";Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
                                //
                                // Asigna a esta variable, para generar la conexion para el grid.
                                stL_Sql_XGrid = stL_Sql;
                                //
                                ConnDB_Pr_PostGreSQL = new NpgsqlConnection(stL_Sql);
                                ConnDB_Pr_PostGreSQL.Open();
                                ConnDB_Pr_PostGreSQL.Close();
                                break;
                            //
                            default:
                                _bl_Is_Connected = false;
                                //ObjL_Utils = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                                //ObjL_Utils.ShowMessage("", MENSAJE_1, "");
                                //
                                MessageBox.Show(MENSAJE_1);
                                break;
                        } // fin del switch (_in_DataBaseEngine_Type)
                        ///////////////////
                        _st_ConnString = stL_Sql;
                        _st_ConnString4Grid = "";
                        // trabaja con la variable : stL_Sql_XGrid
                        //
                        stL_Aux = stL_Sql_XGrid;
                        inL_Pos = stL_Aux.IndexOf("Provider");
                        if (inL_Pos == -1)
                        {
                            // Si no tiene la palabra Provider
                            _st_ConnString4Grid = stL_Sql_XGrid;
                        }
                        else
                        {
                            // Tiene la palabra Provider
                            stL_Aux = stL_Aux.Substring(inL_Pos, stL_Aux.Length);
                            inL_Pos = stL_Aux.IndexOf(";");
                            if (inL_Pos == -1)
                            {
                                _st_ConnString4Grid = stL_Sql_XGrid;
                            }
                            else
                            {
                                _st_ConnString4Grid = stL_Aux.Substring(inL_Pos + 1, stL_Aux.Length - inL_Pos - 1);
                            }
                        }
                    } // Fin del else del  if ( _in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN ) 
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _bl_Is_Connected = false;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ConnectDataBase. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                _bl_Is_Connected = false;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ConnectDataBase. Exception", "", ex.Message.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void Build_QUERY()
        { // Inicio del public void Build_QUERY(
            try
            {
                /// <summary>
                /// Metodo : Arma_QUERY
                /// Encargado de armar el query a ejecutar
                /// </summary> 
                /////////////////////////////////////////////////
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    _st_Query = "";
                    switch (stPr_Funcion)
                    { // del switch (stPr_Funcion)
                        case TO_DO_INQUERY:
                            //
                            _st_Query = stPr_Select + stPr_From + stPr_Where + stPr_And;
                            break;
                        case TO_DO_MODI:
                            //
                            _st_Query = stPr_Update + stPr_Set + stPr_Where + stPr_And;
                            break;
                        case TO_DO_ERASE:
                            //
                            _st_Query = stPr_Delete + stPr_Where + stPr_And;
                            break;
                        case TO_DO_LET_IN:
                            //
                            //'stPr_Campos = Mid(stPr_Campos, 1, Len(stPr_Campos) - 1)
                            _st_Query = stPr_Insert + stPr_Campos + ") VALUES (" + stPr_Set + ")";
                            break;
                        default:
                            break;
                    } // del switch (stPr_Funcion)
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Build_QUERY. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Build_QUERY. Exception", "", ex.Message.ToString());
            }
        } // fin del public void Arma_QUERY(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_UPDATE(String Update_Stmt)
        { // Inicio del public void ToDo_UPDATE(
            try
            {
                /// <summary>
                /// Metodo : UPDATE
                /// Arma la frase UPDATE
                /// </summary>
                /// <param name="st_Update"></param>
                /////////////////////////////////////////////////
                // Antes de Iniciar la Instruccion hace limpieza de las variables privadas
                // con las cuales se arman las instrucciones SQL
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    this.ToDo_CLEAN();
                    //
                    stPr_Update = "UPDATE " + Update_Stmt;
                    if (Update_Stmt.Length > 0)
                    {
                        stPr_Funcion = TO_DO_MODI;
                    }
                    this.Build_QUERY();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_UPDATE. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_UPDATE. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_UPDATE(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_SET(String FieldName, String FieldValue)
        { // Inicio del public void ToDo_SET(
            try
            {
                /// <summary>
                /// Metodo : SET
                /// Arma parte de a instruccion SET 
                /// </summary>
                /// <param name="st_Campo">Campo</param>
                /// <param name="st_Valor">Valor del Campo</param>
                ///
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    switch (stPr_Funcion)
                    { // inicio del switch ( stPr_Funcion)
                        case TO_DO_MODI:
                            //
                            if (stPr_Set.Length == 0)
                            {
                                stPr_Set = " SET " + FieldName + " = '" + FieldValue + "' ";
                            }
                            else
                            {
                                stPr_Set = stPr_Set + " , " + FieldName + " = '" + FieldValue + "' ";
                            }
                            break;
                        case TO_DO_LET_IN:
                            //
                            if (stPr_Campos.Length == 0)
                            {
                                stPr_Campos = FieldName;
                            }
                            else
                            {
                                stPr_Campos = stPr_Campos + " , " + FieldName;
                            }
                            //
                            if (stPr_Set.Length == 0)
                            {
                                stPr_Set = " '" + FieldValue + "' ";
                            }
                            else
                            {
                                stPr_Set = stPr_Set + " , '" + FieldValue + "' ";
                            }
                            break;
                        default:
                            break;
                    } // fin del switch ( stPr_Funcion)
                    //
                    this.Build_QUERY();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_SET. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_SET. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_SET(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_SELECT(String Select_Stmt)
        { // Inicio del public void ToDo_SELECT(
            try
            {
                /// <summary>
                /// Metodo : SELECT
                /// Inicia la instruccion SELECT 
                /// </summary>
                /// <param name="st_Select"></param>
                // Antes de Iniciar la Instruccion hace limpieza de las variables privadas
                // con las cuales se arman las instrucciones SQL
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    this.ToDo_CLEAN();
                    //
                    stPr_Select = "SELECT " + Select_Stmt;
                    if (Select_Stmt.Length > 0)
                    {
                        stPr_Funcion = TO_DO_INQUERY;
                    }
                    //
                    this.Build_QUERY();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_SELECT. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_SELECT. Exception", "", ex.Message.ToString());
            }
        } // fin del public


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_FROM(String From_Stmt)
        { // Inicio del public void ToDo_FROM(
            /// <summary>
            /// Metodo : FROM
            /// arma parte de la instruccion FROM 
            /// </summary>
            /// <param name="st_From">Nombre Tabla para el FROM</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    stPr_From = " FROM " + From_Stmt;
                    //
                    this.Build_QUERY();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_FROM. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_FROM. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_FROM(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_WHERE(String Where_Stmt, String CondVlue = "")
        { // Inicio del public void ToDo_WHERE(
            /// <summary>
            /// Metodo : WHERE
            /// Arma el WHERE de la instruccion SQL
            /// </summary>
            /// <param name="st_Where">Campo para el where</param>
            /// <param name="st_Valor">Valor del campo</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (Where_Stmt.Length > 0)
                    {
                        if (CondVlue.Length > 0)
                        {
                            stPr_Where = " WHERE " + Where_Stmt + " = " + CondVlue;
                        }
                        else
                        {
                            stPr_Where = " WHERE " + Where_Stmt;
                        }

                    }
                    else
                    {
                        stPr_Where = " WHERE " + Where_Stmt;
                    }
                    //
                    this.Build_QUERY();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_WHERE. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_WHERE. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_WHERE(

        [HandleProcessCorruptedStateExceptions]
        public void ToDo_AND(String And_Stmt, String And_Value)
        { // Inicio del public void ToDo_AND(
            /// <summary>
            /// Metodo : AND
            /// Arma el AND de la sentencia SQL
            /// </summary>
            /// <param name="st_And">Campo para el AND</param>
            /// <param name="st_Valor">Valor del Campo</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (And_Value.Length > 0)
                    {
                        stPr_And = stPr_And + " AND " + And_Stmt + " = " + And_Value;
                    }
                    else
                    {
                        stPr_And = stPr_And + " AND " + And_Stmt;
                    }
                    //
                    this.Build_QUERY();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_AND. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_AND. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_AND(

        [HandleProcessCorruptedStateExceptions]
        public void ToDo_ORDERBY(String OrderBy_Stmt)
        { // Inicio del public void ORDERBY(
            /// <summary>
            /// Metodo : ORDERBY
            /// Arma el ORDERBY de la instruccion SQL
            /// </summary>
            /// <param name="st_Orderby">Parte de la instruccion para el ORDERBY</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (OrderBy_Stmt.Length > 0)
                    {
                        _st_Query = _st_Query + " ORDER BY " + OrderBy_Stmt;
                    }
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_ORDERBY. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_ORDERBY. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ORDERBY(

        [HandleProcessCorruptedStateExceptions]
        public void ToDo_LIMIT(String Limit_Stmt)
        { // Inicio del public void ToDo_LIMIT(String st_Limit)
            /// <summary>
            /// Metodo : LIMIT
            /// Arma el LIMIT de la instruccion SQL
            /// </summary>
            /// <param name="st_Limit">Parte de la instruccion para el LIMIT</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (Limit_Stmt.Length > 0)
                    {
                        _st_Query = _st_Query + " LIMIT " + Limit_Stmt;
                    }
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_LIMIT. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_LIMIT. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_LIMIT(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_OFFSET(String OffSet_Stmt)
        { // Inicio del public void ToDo_OFFSET(String st_OffSet)
            /// <summary>
            /// Metodo : OFFSET
            /// Arma el OFFSET de la instruccion SQL
            /// </summary>
            /// <param name="st_OffSet">Parte de la instruccion para el OFFSET</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (OffSet_Stmt.Length > 0)
                    {
                        _st_Query = _st_Query + " OFFSET " + OffSet_Stmt;
                    }
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_OFFSET. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_OFFSET. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_OFFSET(       


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_AT_END_OF_STMT(String End_Stmt)
        { // Inicio del public void ToDo_AT_END_OF_STMT(String st_ParteFinal)
            /// <summary>
            /// Metodo : PARTE_FINAL_QUERY
            /// Arma el string que entra como parametro en la parte final de la instruccion SQL
            /// </summary>
            /// <param name="st_ParteFinal">Parte final de la instruccion para el OFFSET</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (End_Stmt.Length > 0)
                    {
                        _st_Query = _st_Query + " " + End_Stmt;
                        //
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_AT_END_OF_STMT. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_AT_END_OF_STMT. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_AT_END_OF_STMT(       


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_DELETE(String Delete_Stmt)
        { // Inicio del public void ToDo_DELETE(
            /// <summary>
            /// Metodo : DELETE
            /// Inicia la parte del DELETE de la instruccion SQL
            /// </summary>
            /// <param name="st_Delete">Nombre de la Tabla para el DELETE</param>
            try
            {
                // Antes de Iniciar la Instruccion hace limpieza de las variables privadas
                // con las cuales se arman las instrucciones SQL
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    this.ToDo_CLEAN();
                    //
                    stPr_Delete = " DELETE FROM " + Delete_Stmt;
                    if (Delete_Stmt.Length > 0)
                    {
                        stPr_Funcion = TO_DO_ERASE;
                    }
                    //
                    this.Build_QUERY();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_DELETE. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_DELETE. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_DELETE(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_INSERT(String Insert_Stmt)
        { // Inicio del public void ToDo_INSERT(
            /// <summary>
            /// Metodo : INSERT
            /// Inicia la parte del INSERT de la instruccion SQL
            /// </summary>
            /// <param name="st_Insert">Nombre de la tabla para el INSERT</param>
            try
            {
                // Antes de Iniciar la Instruccion hace limpieza de las variables privadas
                // con las cuales se arman las instrucciones SQL
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    this.ToDo_CLEAN();
                    //
                    stPr_Insert = " INSERT INTO " + Insert_Stmt + "(";
                    if (Insert_Stmt.Length > 0)
                    {
                        stPr_Funcion = TO_DO_LET_IN;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_INSERT. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_INSERT. Exception", "", ex.Message.ToString());
            }
        } // fin del public void INSERT(



        [HandleProcessCorruptedStateExceptions]
        public void ToDo_SP_NON_SELECT(String SP_Stmt)
        { // Inicio del public void ToDo_SP_NON_SELECT(
            /// <summary>
            /// Metodo : SP_NON_SELECT
            /// Utilizado para ejecutar SPS que no seleccionan informacion
            /// o que devuelven mensajes , como los sps de fenix.
            /// </summary>
            /// <param name="st_SP"></param>
            try
            {
                // Antes de Iniciar la Instruccion hace limpieza de las variables privadas
                // con las cuales se arman las instrucciones SQL
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    this.ToDo_CLEAN();
                    //
                    _st_Query = SP_Stmt;
                    if (SP_Stmt.Length > 0)
                    {
                        stPr_Funcion = TO_DO_SP;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_SP_NON_SELECT. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_SP_NON_SELECT. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_SP_NON_SELECT(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_SP_SELECT(String SP_Select_Stmt)
        { // Inicio del public void ToDo_SP_SELECT(
            /// <summary>
            /// Metodo : SP_SELECT
            /// Utilizado para llamar procedimientos almacenados que devuelven datos
            /// </summary>
            /// <param name="stRSP">Instruccion SQL que tiene el Sp y los parametros.</param>
            try
            {
                // Antes de Iniciar la Instruccion hace limpieza de las variables privadas
                // con las cuales se arman las instrucciones SQL
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    this.ToDo_CLEAN();
                    //
                    _st_Query = SP_Select_Stmt;
                    if (SP_Select_Stmt.Length > 0)
                    {
                        stPr_Funcion = TO_DO_INQUERY;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_SP_SELECT. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_SP_SELECT. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_SP_SELECT(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_CLOSE()
        { // Inicio del public void ToDo_CLOSE(
            /// <summary>
            /// Metodo : CLOSE
            /// Cierra la conexion y limpia variables privadas
            /// </summary>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Cierra y destruye Conexiones
                    switch (_in_DataBaseEngine_Type)
                    { // del switch (_in_DataBaseEngine_Type)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            //
                            if (ConnDB_Pr_SQLSERVER != null)
                            {
                                if (ConnDB_Pr_SQLSERVER.State == System.Data.ConnectionState.Open)
                                {
                                    ConnDB_Pr_SQLSERVER.Dispose();
                                    ConnDB_Pr_SQLSERVER.Close();
                                }
                            }
                            break;
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS:
                            // Access
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE:
                            // Oracle
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                            // My SQL
                            if (ConnDB_Pr_MYSQL != null)
                            {
                                if (ConnDB_Pr_MYSQL.State == System.Data.ConnectionState.Open)
                                {
                                    ConnDB_Pr_MYSQL.Dispose();
                                    ConnDB_Pr_MYSQL.Close();
                                }
                            }
                            //
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE:
                            // Sybase
                            break;
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                            // PostGreSQL
                            if (ConnDB_Pr_PostGreSQL != null)
                            {
                                if (ConnDB_Pr_PostGreSQL.State == System.Data.ConnectionState.Open)
                                {
                                    ConnDB_Pr_PostGreSQL.ClearPool();
                                    ConnDB_Pr_PostGreSQL.Dispose();
                                    ConnDB_Pr_PostGreSQL.Close();
                                    ConnDB_Pr_PostGreSQL = null;
                                }
                            }
                            break;
                        default:
                            // NADA
                            break;
                    } // Fin del switch (_in_DataBaseEngine_Type)
                    //
                    // Limpia variables
                    this.ToDo_CLEAN();
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_CLOSE. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_CLOSE. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_CLOSE(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_CLEAN()
        { // Inicio del public void ToDo_CLEAN(
            /// <summary>
            /// Metodo : CLEAN
            /// Limpia variables privadas para iniciar un nuevo query
            /// </summary>
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    _bl_SuccessQueryExecution = false;
                    stPr_And = "";
                    stPr_Delete = "";
                    stPr_Select = "";
                    stPr_Set = "";
                    stPr_Insert = "";
                    stPr_Where = "";
                    stPr_Campos = "";
                    stPr_Funcion = "";
                    _st_Query = "";
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_CLEAN. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_CLEAN. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_CLEAN(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_FUNCTION(String Func_Stmt)
        { // Inicio del public void ToDo_FUNCTION(
            /// <summary>
            /// Metodo : FUNCTION
            /// Define la funcio a ejecutar
            /// </summary>
            /// <param name="st_Funcion">La funcion a ejecutar</param>
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    stPr_Funcion = Func_Stmt;
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_FUNCTION. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_FUNCTION. Exception", "", ex.Message.ToString());
            }
        } // fin del public void ToDo_FUNCTION(


        [HandleProcessCorruptedStateExceptions]
        private void Write2Log_Sql_Stmt(String Sql_Stmt)
        {
            /// <summary>
            /// Metodo : CreaLog_Instruccion_SQL
            /// Se encarga de grabar en el archivo de log de la aplicacion
            /// la instruccion SQL que se va ejecutando en los metodos EXECUTE de esta clase.
            /// </summary>
            /// <param name="st_InstruccionSQL">La instruccion SQL que se esta ejecutando en la clase en el metodo EXECUTE</param>
            // Crea el log de la instruccion SQL, que se ejecuta.
            String stL_TextoLog = "";
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Lleva el Log de las instrucciones SQL
                    CLNBTN_Lg objL_LogSQL = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                    //
                    stL_TextoLog = "SQL STATEMENT      : " + _st_Query;
                    stL_TextoLog += NEW_LINE + "DATABASE NAME      : " + stPr_NombreBdSql;
                    stL_TextoLog += NEW_LINE + "APP USER NAME      : " + _st_User;
                    stL_TextoLog += NEW_LINE + "DATABASE USER NAME : " + stPr_IdUsuario_BD;
                    if (stPr_NombreServidor.Length > 0)
                    {
                        stL_TextoLog += NEW_LINE + "SERVER NAME        : " + stPr_NombreServidor;
                    }
                    //
                    objL_LogSQL.WriteTextInLog(stL_TextoLog);
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Write2Log_Sql_Stmt. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Write2Log_Sql_Stmt. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_EXECUTE_SQL(ref DataTable TableData, String Sql_Stmt = "")
        { // Inicio del public void ToDo_EXECUTE_SQL
            /// <summary>
            /// Metodo : EXECUTE_SQL
            /// Sobre Carga 1
            /// Ejecuta Instrucciones Tipo SELECT o SPs que Devuelven Datos
            /// Se crea un Comando con la conexion actual a la base de datos
            /// Se crea un Reader y se devuelve un DataTable
            /// </summary>
            /// <param name="TableData">Se devuelve DataTable con los datos de la consulta.</param>
            /// <param name="st_CadenaSql">Cadena Opcional, para ejecutar esa instruccion sobre la base de datos.</param>
            String stL_InstruccionSQL = "";
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Deja la tabla en NULL
                    TableData = null;
                    //
                    switch (stPr_Funcion)
                    { // Inicio de switch ( stPr_Funcion )
                        //
                        case TO_DO_INQUERY:
                            // Es SELECT o SP que devuelve datos.
                            // Valida si el parameto 'st_CadenaSql', no sta vacio para ejecutarla
                            // o ejecutar la instruccion que ya viene armada.
                            if (Sql_Stmt.Length == 0)
                            {
                                stL_InstruccionSQL = _st_Query;
                            }
                            else
                            {
                                stL_InstruccionSQL = Sql_Stmt;
                            }
                            if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                            { // Inicio del  if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                                ///////////////////////////////////////
                                // Envia comando via Pandora BOX
                                ///////////////////////////////////////
                                TableData = new DataTable();
                                this.ToDo_ExecQuery_ByMonitorSQL(stL_InstruccionSQL, ref TableData);
                                ////////////////////////////////////////////////////////////////////////
                            } // Fin del  if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                            else // del  if ( _in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN ) 
                            { // Inicio del else del  if ( _in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN ) 
                                // Crea el Command
                                switch (_in_DataBaseEngine_Type)
                                { // del switch (_in_DataBaseEngine_Type)
                                    case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                                        // SQL SERVER
                                        // Valida la conexion a la base de datos.
                                        if (ConnDB_Pr_SQLSERVER == null)
                                        {
                                            this.ConnectDataBase();
                                        }
                                        else
                                        {
                                            if (ConnDB_Pr_SQLSERVER.State == System.Data.ConnectionState.Closed)
                                            {
                                                this.ConnectDataBase();
                                            }
                                        }
                                        // Validate if the Sql Server is on-line
                                        if (SqlServerCanConnect())
                                        {
                                             // Crea el Comando
                                            CmdBD_Pr_SQLSERVER = new SqlCommand(stL_InstruccionSQL, ConnDB_Pr_SQLSERVER) ;
                                            CmdBD_Pr_SQLSERVER.CommandTimeout = this._in_CommandTimeout;
                                            ConnDB_Pr_SQLSERVER.Open();
                                            // Crea el Reader.
                                            Rrd_Pr_SQLSERVER = null;
                                            Rrd_Pr_SQLSERVER = CmdBD_Pr_SQLSERVER.ExecuteReader();
                                            // Pasa el DataTable
                                            TableData = new DataTable();
                                            TableData.Load(Rrd_Pr_SQLSERVER);
                                        }
                                        else
                                        {
                                            TableData = null;
                                        }
                                        //
                                        break;
                                    case CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS:
                                        // Access
                                        break;
                                    //
                                    case CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE:
                                        // Oracle
                                        break;
                                    //
                                    case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                                        // My SQL
                                        // Valida la conexion a la base de datos.
                                        if (ConnDB_Pr_MYSQL == null)
                                        {
                                            this.ConnectDataBase();
                                        }
                                        else
                                        {
                                            if (ConnDB_Pr_MYSQL.State == System.Data.ConnectionState.Closed)
                                            {
                                                this.ConnectDataBase();
                                            }
                                        }
                                        // Crea el comando
                                        CmdBD_Pr_MYSQL = new MySqlCommand(stL_InstruccionSQL, ConnDB_Pr_MYSQL);
                                        CmdBD_Pr_MYSQL.CommandTimeout = this._in_CommandTimeout;
                                        ConnDB_Pr_MYSQL.Open();
                                        // Crea el Reader.
                                        Rrd_Pr_MYSQL = null;
                                        Rrd_Pr_MYSQL = CmdBD_Pr_MYSQL.ExecuteReader();
                                        // Pasa el DataTable
                                        TableData = new DataTable();
                                        TableData.Load(Rrd_Pr_MYSQL);
                                        //
                                        break;
                                    //
                                    case CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE:
                                        // Sybase
                                        break;
                                    case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                                        // PostGreSQL
                                        // Valida la conexion a la base de datos.                               
                                        if (ConnDB_Pr_PostGreSQL == null)
                                        {
                                            this.ConnectDataBase();
                                        }
                                        else
                                        {
                                            if (ConnDB_Pr_PostGreSQL.State == System.Data.ConnectionState.Closed)
                                            {
                                                this.ConnectDataBase();
                                            }
                                        }
                                        // Crea el comando
                                        CmdBD_Pr_PostGreSQL = new NpgsqlCommand(stL_InstruccionSQL, ConnDB_Pr_PostGreSQL);
                                        CmdBD_Pr_PostGreSQL.CommandTimeout = this._in_CommandTimeout;
                                        ConnDB_Pr_PostGreSQL.Open();
                                        // Crea el Reader.
                                        Rrd_Pr_PostGreSQL = null;
                                        Rrd_Pr_PostGreSQL = CmdBD_Pr_PostGreSQL.ExecuteReader();
                                        // Pasa el DataTable
                                        TableData = new DataTable();
                                        TableData.Load(Rrd_Pr_PostGreSQL);
                                        //
                                        break;
                                    default:
                                        // NADA
                                        break;
                                } // Fin del switch (_in_DataBaseEngine_Type)
                            } // Fin del else del  if ( _in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN ) 
                            // Lleva el Log de las instrucciones SQL
                            if (_bl_WriteOutSql_Stmt)
                            {
                                this.Write2Log_Sql_Stmt(stL_InstruccionSQL);
                            }
                            //
                            break;
                        default:
                            break;
                    } // del switch ( stPr_Funcion )
                    /////////////////////
                }
            }
            //
            catch (System.AccessViolationException ex_0)
            {
                // Deja la tabla en NULL
                TableData = null;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_EXECUTE_SQL. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, stL_InstruccionSQL);
            }
            catch (Exception ex)
            {
                // Deja la tabla en NULL
                TableData = null;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_EXECUTE_SQL. Exception", "", ex.Message.ToString(), stPr_NombreBdSql, stL_InstruccionSQL);
            }
        } // fin del public void ToDo_EXECUTE_SQL(


        [HandleProcessCorruptedStateExceptions]
        public void ToDo_EXECUTE_SQL(String Sql_Stmt = "")
        { // Inicio del public void ToDo_EXECUTE_SQL
            /// <summary>
            /// Metodo : EXECUTE_SQL
            /// Sobre carga 2
            /// Ejecuta el tipo de instrucciones:
            /// INSERT, UPDATE , DELETE o Sps que no Devuelven Datos.
            /// Ejecuta la instruccion que se tiene actualmente en la clase
            /// o ejecuta la instruccion , que viene en el parametro 'st_CadenaSql'.
            /// </summary>
            /// <param name="st_CadenaSql">Instruccion SQL a ejecutar. Debe ser el tipo :  INSERT, UPDATE , DELETE o Sps que no Devuelven Datos.</param>
            Boolean bl_Ejecute = false;
            Boolean bl_Ejecute_SP = false;
            String stL_InstruccionSQL = "";
            int inL_rowsAffected = 0;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    //
                    switch (stPr_Funcion)
                    { // Inicio de switch ( stPr_Funcion )
                        //
                        case TO_DO_MODI:
                            //
                            bl_Ejecute = true;
                            break;
                        case TO_DO_LET_IN:
                            //
                            bl_Ejecute = true;
                            break;
                        case TO_DO_ERASE:
                            //
                            bl_Ejecute = true;
                            break;
                        case TO_DO_SP:
                            //
                            bl_Ejecute = true;
                            bl_Ejecute_SP = true;
                            break;
                        default:
                            bl_Ejecute = true;
                            break;
                    } // del switch ( stPr_Funcion )
                    /////////////////////
                    if (bl_Ejecute)
                    {
                        // Valida si el parameto 'st_CadenaSql', no sta vacio para ejecutarla
                        // o ejecutar la instruccion que ya viene armada.
                        if (Sql_Stmt.Length == 0)
                        {
                            stL_InstruccionSQL = _st_Query;
                        }
                        else
                        {
                            stL_InstruccionSQL = Sql_Stmt;
                        }
                        if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                        { // Inicio del  if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                            ///////////////////////////////////////
                            // Envia comando via Pandora BOX
                            ///////////////////////////////////////
                            _bl_SuccessQueryExecution = (this.ToDo_ExecQuery_ByMonitorSQL(stL_InstruccionSQL) > 0);
                            ////////////////////////////////////////////////////////////////////////
                        } // Fin del  if (_in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                        else // del  if ( _in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN )
                        {
                            // Crea el Command
                            switch (_in_DataBaseEngine_Type)
                            { // del switch (_in_DataBaseEngine_Type)
                                case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                                    // SQL SERVER
                                    // Valida la conexion a la base de datos.
                                    if (ConnDB_Pr_SQLSERVER == null)
                                    {
                                        this.ConnectDataBase();
                                    }
                                    else
                                    {
                                        if (ConnDB_Pr_SQLSERVER.State == System.Data.ConnectionState.Closed)
                                        {
                                            this.ConnectDataBase();
                                        }
                                    }
                                     // Validate if the Sql Server is on-line
                                    if (SqlServerCanConnect())
                                    {
                                        // Crea el Comando
                                        CmdBD_Pr_SQLSERVER = new SqlCommand(stL_InstruccionSQL, ConnDB_Pr_SQLSERVER);
                                        CmdBD_Pr_SQLSERVER.CommandTimeout = this._in_CommandTimeout;
                                        ConnDB_Pr_SQLSERVER.Open();
                                        // Ejecuta el Comando
                                        inL_rowsAffected = CmdBD_Pr_SQLSERVER.ExecuteNonQuery();
                                        if (bl_Ejecute_SP)
                                        {
                                            _bl_SuccessQueryExecution = true;
                                        }
                                        else
                                        {
                                            _bl_SuccessQueryExecution = (inL_rowsAffected > 0);
                                        }
                                        ConnDB_Pr_SQLSERVER.Close();
                                    }
                                    else
                                    {
                                        _bl_SuccessQueryExecution = false;
                                    }
                                    break;
                                case CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS:
                                    // Access
                                    break;
                                //
                                case CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE:
                                    // Oracle
                                    break;
                                //
                                case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                                    // My SQL
                                    // Valida la conexion a la base de datos.
                                    if (ConnDB_Pr_MYSQL == null)
                                    {
                                        this.ConnectDataBase();
                                    }
                                    else
                                    {
                                        if (ConnDB_Pr_MYSQL.State == System.Data.ConnectionState.Closed)
                                        {
                                            this.ConnectDataBase();
                                        }
                                    }
                                    // Crea el comando
                                    CmdBD_Pr_MYSQL = new MySqlCommand(stL_InstruccionSQL, ConnDB_Pr_MYSQL);
                                    CmdBD_Pr_MYSQL.CommandTimeout = this._in_CommandTimeout;
                                    ConnDB_Pr_MYSQL.Open();
                                    // Ejecuta el Comando
                                    inL_rowsAffected = CmdBD_Pr_MYSQL.ExecuteNonQuery();
                                    if (bl_Ejecute_SP)
                                    {
                                        _bl_SuccessQueryExecution = true;
                                    }
                                    else
                                    {
                                        _bl_SuccessQueryExecution = (inL_rowsAffected > 0);
                                    }
                                    ConnDB_Pr_MYSQL.Close();
                                    //
                                    break;
                                //
                                case CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE:
                                    // Sybase
                                    break;
                                case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                                    // PostGreSQL
                                    // Valida la conexion a la base de datos.                               
                                    if (ConnDB_Pr_PostGreSQL == null)
                                    {
                                        this.ConnectDataBase();
                                    }
                                    else
                                    {
                                        if (ConnDB_Pr_PostGreSQL.State == System.Data.ConnectionState.Closed)
                                        {
                                            this.ConnectDataBase();
                                        }
                                    }
                                    //
                                    // Crea el comando
                                    CmdBD_Pr_PostGreSQL = new NpgsqlCommand(stL_InstruccionSQL, ConnDB_Pr_PostGreSQL);
                                    CmdBD_Pr_PostGreSQL.CommandTimeout = this._in_CommandTimeout;
                                    ConnDB_Pr_PostGreSQL.Open();
                                    // Ejecuta el Comando
                                    inL_rowsAffected = CmdBD_Pr_PostGreSQL.ExecuteNonQuery();
                                    if (bl_Ejecute_SP)
                                    {
                                        _bl_SuccessQueryExecution = true;
                                    }
                                    else
                                    {
                                        _bl_SuccessQueryExecution = (inL_rowsAffected > 0);
                                    }
                                    ConnDB_Pr_PostGreSQL.Close();
                                    //
                                    break;
                                default:
                                    // NADA
                                    break;
                            } // Fin del switch (_in_DataBaseEngine_Type)
                        } // Fin del else del if ( _in_DataBaseConn_Type == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN )
                        ////////////////////////////////////////////
                        // Lleva el Log de las instrucciones SQL
                        if (_bl_WriteOutSql_Stmt)
                        {
                            this.Write2Log_Sql_Stmt(stL_InstruccionSQL);
                        }
                        //
                    }
                }
            }
            //
            catch (System.AccessViolationException ex_0)
            {
                _bl_SuccessQueryExecution = false;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_EXECUTE_SQL(2). System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, stL_InstruccionSQL);
            }
            catch (Exception ex)
            {
                _bl_SuccessQueryExecution = false;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_EXECUTE_SQL(2). Exception", "", ex.Message.ToString(), stPr_NombreBdSql, stL_InstruccionSQL);
            }
        } // fin del public void ToDo_EXECUTE_SQL(


        [HandleProcessCorruptedStateExceptions]
        private Boolean ToDo_ExecQuery_ByMonitorSQL(String Sql_Stmt, ref DataTable TableData)
        {
            /// <summary>
            /// Metodo : EjecutaQuery_ViaConectorSQL
            /// Ejecuta un query tipo SELECT, via ConectoSQL.
            /// </summary>
            /// <param name="_st_Query">Queyy a ejecutar</param>
            /// <param name="TableData">Devuelve un DataTable, con los datos del SELECT</param>
            /// <returns></returns>
            Boolean blL_EjecutoOk = false;
            String stL_Protocolo = "";
            String stL_AuxSQL = "";
            String stL_Resultados_Query = "";
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    //////////////////////////////////////////////////////////////////////
                    // CNTR|SQL|
                    // Tipo_Servidor|Indicador Encriptacion Datos Usuario(1/0)
                    // |Nombre_Servidor|Puerto_Servidor|Nombre_BD|Usuario_Bd|Clave_UsuarioBd
                    // |Tipo_Instruccion(S/X)|Instruccion_SQL|TRNX:No_Transaccion
                    // Ejemplo:
                    // El comando original viene:
                    // CNTR|SQL|3|0|localhost|0|Fenix_Local|userpostgres_fenix|entrada-fenix-2013|S|SElect * from t00usuarios|TRNX:Id_trx1
                    // CNTR|SQL|3|0|localhost|0|Fenix_Local|userpostgres_fenix|entrada-fenix-2013|X|update t00usuarios set a00sexo = null|TRNX:Id_trx1
                    // La clase recibe, despues del CNTR|SQL| :
                    // 3|0|localhost|0|Fenix_Local|userpostgres_fenix|entrada-fenix-2013|S|SElect * from t00usuarios|TRNX:Id_trx1
                    // 3|0|localhost|0|Fenix_Local|userpostgres_fenix|entrada-fenix-2013|X|update t00usuarios set a00sexo = null|TRNX:Id_trx1
                    //
                    /////////////////////////////////////////////////////////////////////////////
                    stL_Protocolo = "CNTR|SQL";
                    // Tipo de Motor de base de datos.
                    stL_Protocolo += "|" + Convert.ToInt16(_in_DataBaseEngine_Type);
                    // Indicador de Encriptacion
                    stL_Protocolo += "|" + "1";
                    // Nombre del servidor
                    stL_Protocolo += "|" + stPr_NombreServidor;
                    // Puerto del servidor
                    if (lnPr_PuertoBD > 0)
                    {
                        stL_Protocolo += "|" + lnPr_PuertoBD.ToString();
                    }
                    else
                    {
                        stL_Protocolo += "|" + "0";
                    }
                    // Nombre base de datos
                    stL_Protocolo += "|" + stPr_NombreBdSql;
                    // Usuario de la BD
                    CLNBTN_Es obL_Enc = new CLNBTN_Es(_st_User, _st_FileLog, false, true, false, _st_Lic);
                    //-----------------------------------------------------
                    // Para el usuario encriptado Cambia el pipe (|) por el TAB
                    //-->>stL_Protocolo += "|" + obL_Enc.EncriptaInfo(stPr_IdUsuario_BD);
                    stL_AuxSQL = "";
                    stL_AuxSQL = obL_Enc.File2Es(stPr_IdUsuario_BD, "gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnns06srbyMg", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "QHT6kUHtr2zRbupap5KPu4jeO9GUVQ2SZO7UMnns06srbyE+gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aMg", _st_Lic);
                    if (stL_AuxSQL.Contains("|"))
                    {
                        stL_AuxSQL = stL_AuxSQL.Replace("|", Convert.ToChar(IN_CARACTER_CAMBIA).ToString());
                    }
                    stL_Protocolo += "|" + stL_AuxSQL;
                    //-----------------------------------------------------
                    //-----------------------------------------------------
                    // Para la clave encriptado Cambia el pipe (|) por el TAB
                    // Clave de la BD
                    //-->>stL_Protocolo += "|" +  obL_Enc.EncriptaInfo(stPr_ClaveUsuario_BD);
                    stL_AuxSQL = "";
                    stL_AuxSQL = obL_Enc.File2Es(stPr_ClaveUsuario_BD, "gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnns06srbyMg", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "QHT6kUHtr2zRbupap5KPu4jeO9GUVQ2SZO7UMnns06srbyE+gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aMg", _st_Lic);
                    //
                    if (stL_AuxSQL.Contains("|"))
                    {
                        stL_AuxSQL = stL_AuxSQL.Replace("|", Convert.ToChar(IN_CARACTER_CAMBIA).ToString());
                    }
                    stL_Protocolo += "|" + stL_AuxSQL;
                    //-----------------------------------------------------
                    // |Tipo_Instruccion(S/X)
                    stL_Protocolo += "|" + "S";
                    //-----------------------------------------------------
                    // |Instruccion_SQL
                    // Para la instruccion SQL Cambia el pipe (|) por el TAB
                    stL_AuxSQL = "";
                    stL_AuxSQL = Sql_Stmt;
                    if (stL_AuxSQL.Contains("|"))
                    {
                        stL_AuxSQL = stL_AuxSQL.Replace("|", Convert.ToChar(IN_CARACTER_CAMBIA).ToString());
                    }
                    stL_Protocolo += "|" + stL_AuxSQL;
                    //-----------------------------------------------------
                    // ESTE DATO NO SE ENVIA
                    // |TRNX:No_Transaccion
                    //////////////////////////////////////////////////////////////////////
                    // Hace la conexion con el servidor TCP.
                    //////////////////////////////////////////////////////////////////////
                    //
                    String stL_ServidorPandora = ObjPr_BDInfo.getServer_Monitor();
                    int inL_Puerto = ObjPr_BDInfo.getServer_Monitor_Port();
                    Boolean blL_LeyoTiulos = false;
                    //
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable("DataResulSet");
                    //
                    if (stL_ServidorPandora.Length > 0 && inL_Puerto > 0)
                    { // Inicio del  if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                        CLNBTN_CliPro ObjL_TCP = new CLNBTN_CliPro(stL_ServidorPandora, inL_Puerto, _st_User, _st_FileLog, _st_Lic);
                        // 
                        String stL_ArchivoDatos = "";
                        // Envia el usuario y la clave de Pandora.
                        Boolean blL_ErrorAccessFile = false;
                        String stL_Respuesta = ObjL_TCP.Recieve_Info_Conn(stL_Protocolo, ref stL_ArchivoDatos, ObjPr_BDInfo.getServer_Monitor_UID(), ObjPr_BDInfo.getServer_Monitor_UID_PWD(), ref blL_ErrorAccessFile, ref stL_Resultados_Query, _st_ServerMonitorPathTemp);
                        //
                        if (blL_ErrorAccessFile)
                        {
                            stL_Respuesta = ObjL_TCP.Recieve_Info_Conn(stL_Protocolo, ref stL_ArchivoDatos, ObjPr_BDInfo.getServer_Monitor_UID(), ObjPr_BDInfo.getServer_Monitor_UID_PWD(), ref blL_ErrorAccessFile, ref stL_Resultados_Query, _st_ServerMonitorPathTemp);
                        }
                        if (stL_ArchivoDatos.Length == 0)
                        { // Inicio del if (stL_ArchivoDatos.Length == 0)
                            //throw new Exception(stL_Respuesta);
                            CLNBTN_Lg objL_Log1 = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, false);
                            //
                            objL_Log1.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_ExecQuery_ByMonitorSQL(1)", "", stL_Respuesta, stPr_NombreBdSql, _st_Query);
                            //
                        } // Fin del if (stL_ArchivoDatos.Length == 0)
                        else // Del if (stL_ArchivoDatos.Length == 0)
                        { // Inicio del else del if (stL_ArchivoDatos.Length == 0)
                            // //////////////////////////////////////////////////
                            // Los resultados estan en el protocolo
                            // Los lee y los pasa a la tabladef
                            // /////////////////////////////////////////////////
                            if (stL_Resultados_Query.Length > 0)
                            { // Inicio del if (stL_Resultados_Query.Length > 0)
                                String stL_END_OF_LINE = "<EOL>";
                                String[] stL_Regs = Regex.Split(stL_Resultados_Query, stL_END_OF_LINE);
                                string[] stL_Titulos = new String[0];
                                //
                                foreach (string stL_Registro in stL_Regs)
                                {
                                    // Crea los campos con base en los titulos de cada campo.
                                    if (stL_Registro.Length > 0)
                                    { // Inicio del if ( stL_Linea.Length > 0 ) 
                                        string[] stL_Campos = stL_Registro.Split(';'); //Corta por los punto y coma
                                        if (blL_LeyoTiulos)
                                        { // Inicio del if (blL_LeyoTiulos)
                                            // Va armando cada fila con los datos.
                                            DataRow row = dt.NewRow();
                                            for (int inL_Col = 0; inL_Col < stL_Campos.Length; inL_Col++)
                                            {
                                                //
                                                // Valida si viene el valor NULL
                                                // Lo deja vacio
                                                if (stL_Campos[inL_Col].Trim().ToUpper() == "NULL")
                                                {
                                                    row[stL_Titulos[inL_Col]] = "";
                                                }
                                                else
                                                {
                                                    row[stL_Titulos[inL_Col]] = stL_Campos[inL_Col];
                                                }
                                                //
                                            }
                                            dt.Rows.Add(row);
                                        } // Fin del Inicio del if (blL_LeyoTiulos)
                                        else // del Inicio del if (blL_LeyoTiulos)
                                        { // Inicio del else del Inicio del if (blL_LeyoTiulos)
                                            blL_LeyoTiulos = true;
                                            stL_Titulos = stL_Registro.Split(';'); //Corta por los punto y coma
                                            for (int inL_Col = 0; inL_Col < stL_Titulos.Length; inL_Col++)
                                            {
                                                if (stL_Titulos[inL_Col].Length == 0)
                                                {
                                                    // Si no tiene nombre la columna, deja como
                                                    // "Col_0", "Col_1" , "Col_N"
                                                    //
                                                    dt.Columns.Add("Col_" + inL_Col.ToString(), typeof(string));
                                                }
                                                else
                                                {
                                                    dt.Columns.Add(stL_Titulos[inL_Col], typeof(string));
                                                }
                                            }
                                            ds.Tables.Add(dt);
                                        } // Fin del else del Inicio del if (blL_LeyoTiulos)
                                    } // Fin del if ( stL_Linea.Length > 0 ) 
                                }
                                // Devuelve true
                                blL_EjecutoOk = true;
                            } // Fin del if (stL_Resultados_Query.Length > 0)
                            else // del if (stL_Resultados_Query.Length > 0)
                            { // Inicio del else del if (stL_Resultados_Query.Length > 0)
                                // ///////////////////////////////////
                                // Se lee el archivo generado con los datos del query
                                // //////////////////////////////////
                                if (File.Exists(stL_ArchivoDatos))
                                { // Inicio del if (File.Exists(stL_ArchivoDatos))
                                    string[] stL_Titulos = new String[0];
                                    // Lee  el archivo.
                                    using (StreamReader sr = new StreamReader(stL_ArchivoDatos))
                                    { // Inicio del using (StreamReader 
                                        //
                                        string stL_Linea;
                                        while ((stL_Linea = sr.ReadLine()) != null)
                                        { // Inicio del  while ((stL_Linea=
                                            // Crea los campos con base en los titulos de cada campo.
                                            if (stL_Linea.Length > 0)
                                            { // Inicio del if ( stL_Linea.Length > 0 ) 
                                                string[] stL_Campos = stL_Linea.Split(';'); //Corta por los punto y coma
                                                if (blL_LeyoTiulos)
                                                { // Inicio del if (blL_LeyoTiulos)
                                                    // Va armando cada fila con los datos.
                                                    DataRow row = dt.NewRow();
                                                    for (int inL_Col = 0; inL_Col < stL_Campos.Length; inL_Col++)
                                                    {
                                                        //
                                                        // Valida si viene el valor NULL
                                                        // Lo deja vacio
                                                        if (stL_Campos[inL_Col].Trim().ToUpper() == "NULL")
                                                        {
                                                            row[stL_Titulos[inL_Col]] = "";
                                                        }
                                                        else
                                                        {
                                                            row[stL_Titulos[inL_Col]] = stL_Campos[inL_Col];
                                                        }
                                                        //
                                                    }
                                                    dt.Rows.Add(row);
                                                } // Fin del Inicio del if (blL_LeyoTiulos)
                                                else // del Inicio del if (blL_LeyoTiulos)
                                                { // Inicio del else del Inicio del if (blL_LeyoTiulos)
                                                    blL_LeyoTiulos = true;
                                                    stL_Titulos = stL_Linea.Split(';'); //Corta por los punto y coma
                                                    for (int inL_Col = 0; inL_Col < stL_Titulos.Length; inL_Col++)
                                                    {
                                                        if (stL_Titulos[inL_Col].Length == 0)
                                                        {
                                                            // Si no tiene nombre la columna, deja como
                                                            // "Col_0", "Col_1" , "Col_N"
                                                            //
                                                            dt.Columns.Add("Col_" + inL_Col.ToString(), typeof(string));
                                                        }
                                                        else
                                                        {
                                                            dt.Columns.Add(stL_Titulos[inL_Col], typeof(string));
                                                        }
                                                    }
                                                    ds.Tables.Add(dt);
                                                } // Fin del else del Inicio del if (blL_LeyoTiulos)
                                            } // Fin del if ( stL_Linea.Length > 0 ) 
                                        } // Fin del  while ((stL_Linea=
                                    } // Fin del using (StreamReader 
                                    // Elimina el archivo 
                                    File.Delete(stL_ArchivoDatos);
                                    // Devuelve true
                                    blL_EjecutoOk = true;
                                } // Fin de if (File.Exists(stL_ArchivoDatos))
                            } // Fin del else del if (stL_Resultados_Query.Length > 0)
                        } // Fin else del if (stL_ArchivoDatos.Length == 0)
                    } // Fin del  if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                    // Devuelve la tabla, con los datos.
                    TableData = dt;
                    //
                }
                return blL_EjecutoOk;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_ExecQuery_ByMonitorSQL(1). System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, _st_Query);
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_ExecQuery_ByMonitorSQL(1). Exception", "", ex.Message.ToString(), stPr_NombreBdSql, _st_Query);
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private long ToDo_ExecQuery_ByMonitorSQL(String Sql_Stmt)
        {
            /// <summary>
            /// Metodo : EjecutaQuery_ViaConectorSQL
            /// Sobre Carga 2
            /// Ejecuta queries tipo, INSERT, UPDATE, DELETE
            /// via ConectoSQL
            /// Devuelve el nuemero de filas afectadas por el query.
            /// </summary>
            /// <param name="_st_Query">Query a ejecutar</param>
            /// <returns>nuemero de filas afectadas por el query</returns>
            String stL_Protocolo = "";
            long lnL_CantidadFilasAfectadas = 0;
            Boolean blL_GeneroError = false;
            String stL_AuxSQL = "";
            try
            {
                //////////////////////////////////////////////////////////////////////
                // CNTR|SQL|
                // Tipo_Servidor|Indicador Encriptacion Datos Usuario(1/0)
                // |Nombre_Servidor|Puerto_Servidor|Nombre_BD|Usuario_Bd|Clave_UsuarioBd
                // |Tipo_Instruccion(S/X)|Instruccion_SQL|TRNX:No_Transaccion
                // Ejemplo:
                // El comando original viene:
                // CNTR|SQL|3|0|localhost|0|Fenix_Local|userpostgres_fenix|entrada-fenix-2013|S|SElect * from t00usuarios|TRNX:Id_trx1
                // CNTR|SQL|3|0|localhost|0|Fenix_Local|userpostgres_fenix|entrada-fenix-2013|X|update t00usuarios set a00sexo = null|TRNX:Id_trx1
                // La clase recibe, despues del CNTR|SQL| :
                // 3|0|localhost|0|Fenix_Local|userpostgres_fenix|entrada-fenix-2013|S|SElect * from t00usuarios|TRNX:Id_trx1
                // 3|0|localhost|0|Fenix_Local|userpostgres_fenix|entrada-fenix-2013|X|update t00usuarios set a00sexo = null|TRNX:Id_trx1
                //
                /////////////////////////////////////////////////////////////////////////////
                stL_Protocolo = "CNTR|SQL";
                // Tipo de Motor de base de datos.
                stL_Protocolo += "|" + Convert.ToInt16(_in_DataBaseEngine_Type);
                // Indicador de Encriptacion
                stL_Protocolo += "|" + "1";
                // Nombre del servidor
                stL_Protocolo += "|" + stPr_NombreServidor;
                // Puerto del servidor
                if (lnPr_PuertoBD > 0)
                {
                    stL_Protocolo += "|" + lnPr_PuertoBD.ToString();
                }
                else
                {
                    stL_Protocolo += "|" + "0";
                }
                // Nombre base de datos
                stL_Protocolo += "|" + stPr_NombreBdSql;
                // Usuario de la BD
                CLNBTN_Es obL_Enc = new CLNBTN_Es(_st_User, _st_FileLog, false, true, false, _st_Lic);
                //-----------------------------------------------------
                // Para el usuario encriptado Cambia el pipe (|) por el TAB
                //-->>>stL_Protocolo += "|" + obL_Enc.EncriptaInfo(stPr_IdUsuario_BD);
                stL_AuxSQL = "";
                stL_AuxSQL = obL_Enc.File2Es(stPr_IdUsuario_BD, "gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnns06srbyMg", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "QHT6kUHtr2zRbupap5KPu4jeO9GUVQ2SZO7UMnns06srbyE+gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aMg", _st_Lic);
                if (stL_AuxSQL.Contains("|"))
                {
                    stL_AuxSQL = stL_AuxSQL.Replace("|", Convert.ToChar(IN_CARACTER_CAMBIA).ToString());
                }
                stL_Protocolo += "|" + stL_AuxSQL;
                //-----------------------------------------------------
                //-----------------------------------------------------
                // Para la clave encriptada Cambia el pipe (|) por el TAB
                // Clave de la BD
                //-->>>stL_Protocolo += "|" + obL_Enc.EncriptaInfo(stPr_ClaveUsuario_BD);
                stL_AuxSQL = "";
                stL_AuxSQL = obL_Enc.File2Es(stPr_ClaveUsuario_BD, "gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnns06srbyMg", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "QHT6kUHtr2zRbupap5KPu4jeO9GUVQ2SZO7UMnns06srbyE+gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aMg", _st_Lic);
                if (stL_AuxSQL.Contains("|"))
                {
                    stL_AuxSQL = stL_AuxSQL.Replace("|", Convert.ToChar(IN_CARACTER_CAMBIA).ToString());
                }
                stL_Protocolo += "|" + stL_AuxSQL;
                //-----------------------------------------------------
                // |Tipo_Instruccion(S/X)
                stL_Protocolo += "|" + "X";
                //-----------------------------------------------------
                // |Instruccion_SQL
                // Para la instruccion SQL Cambia el pipe (|) por el TAB
                stL_AuxSQL = "";
                stL_AuxSQL = Sql_Stmt;
                if (stL_AuxSQL.Contains("|"))
                {
                    stL_AuxSQL = stL_AuxSQL.Replace("|", Convert.ToChar(IN_CARACTER_CAMBIA).ToString());
                }
                stL_Protocolo += "|" + stL_AuxSQL;
                //-----------------------------------------------------
                // ESTE DATO NO SE ENVIA
                // |TRNX:No_Transaccion
                //////////////////////////////////////////////////////////////////////
                // Hace la conexion con el servidor TCP.
                //////////////////////////////////////////////////////////////////////
                //
                String stL_ServidorPandora = ObjPr_BDInfo.getServer_Monitor();
                int inL_Puerto = ObjPr_BDInfo.getServer_Monitor_Port();
                //
                if (stL_ServidorPandora.Length > 0 && inL_Puerto > 0)
                { // Inicio del  if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                    CLNBTN_CliPro ObjL_TCP = new CLNBTN_CliPro(stL_ServidorPandora, inL_Puerto, _st_User, _st_FileLog, _st_Lic);
                    // 
                    String stL_Respuesta = ObjL_TCP.Execute_Qy_Conn(stL_Protocolo, ref lnL_CantidadFilasAfectadas, ref blL_GeneroError);
                    //
                    if (blL_GeneroError)
                    {
                        lnL_CantidadFilasAfectadas = 0;
                        //throw new Exception(stL_Respuesta);
                        CLNBTN_Lg objL_Log1 = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                        //
                        objL_Log1.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_ExecQuery_ByMonitorSQL(2)", "", stL_Respuesta, stPr_NombreBdSql, _st_Query);
                    }
                } // Fin del  if ( stL_ServidorPandora.Length > 0 && inL_Puerto > 0 ) 
                ///
                return lnL_CantidadFilasAfectadas;
                ///
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_ExecQuery_ByMonitorSQL(2). System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBdSql, _st_Query);
                return 0;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_ExecQuery_ByMonitorSQL(2). Exception", "", ex.Message.ToString(), stPr_NombreBdSql, _st_Query);
                return 0;
            }
        }

        private Boolean SqlServerCanConnect()
        {
            Boolean blLOK = false;
            try
            {
                String stL_Aux = stPr_NombreServidor; 
                //
                System.Net.Sockets.TcpClient tcp = new System.Net.Sockets.TcpClient(stL_Aux, 1433);
                if (tcp.Connected)
                {
                    blLOK = true;
                }
                //
                tcp.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error connecting to {0}: {1}", svrName, ex.Message);
            }
            return blLOK;
        }


    }
}
