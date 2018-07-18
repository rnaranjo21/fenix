using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBToolsNet
{
    public class CLNBTN_IQy
    {
        // Clase Equivalente : ClasX_DBInfo			
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        private string _st_User = "CLNBTN_IQy";
        private string _st_FileLog = "C:\\Windows\\CLNBTN_IQy.log";
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_IQy";
        //
        /// Clase Para manejo de la inforamacion de base de datos
        /// Autor : Alvaro S. Quimbaya C.
        /// Fecha : Agosto 27 2012.
        /// Empresa : Strail SAS
        /// URL Referencia : http://msdn.microsoft.com/es-es/library/3707x96z(v=vs.80).aspx
        /// 
        ///  
        //

        public enum inDB_Types
        {
            DB_TYPE_SQLSERVER
              ,
            DB_TYPE_ORACLE
                ,
            DB_TYPE_SYBASE
                ,
            DB_TYPE_POSTGRESQL
                ,
            DB_TYPE_MYSQL
                ,
            DB_TYPE_ACCESS
                ,
            DB_TYPE_DBASE
                , BD_TYPE_FOXPRO
        }

        public enum inConnect_Type
        {
            TYPE_1_CONNECT_USER_SQL = 1 // Usuario SQL, un usuario y password en la dll o .conf.
            ,
            TYPE_2_CONNECT_USER_APP = 2 // Usuario de la aplicacion que es tambien usuario SQL Server.
                ,
            TYPE_3_CONNECT_USER_WIN = 3  // Usuario de windows, que es usuario de SQL, no se pide usuario ni clave.
                ,
            TYPE_4_CONNECT_USER_INFO_EXT = 4  // Informado por el usuario, caso de Fenix.
              , TYPE_5_CONNECT_MONITOR_TRAN = 5 // Conexion via Pandora
        }

        //////////////////////////////////////
        private String _st_ServerName = "ServerName"; // Nombre del servidor SQL
        //
        private String _st_DataBaseName = "DataBaseName"; //  Nombre de la base de datos.
        //
        private String _st_DataBasePath = "PathBD"; // Ruta de la base de datos.
        //
        private String _st_FileName_Access = "AccessFileName"; // Nombre del archivo de Access.
        //
        private String _st_DataBase_UserID = "BDUserID"; // Codigo de usuario de la base de datos.
        //
        private String _st_DataBase_UserPWD = "BDUserPass"; // Password del usuario de la base de datos.
        //
        private inDB_Types _in_DataBaseEngine_Type = 0; // Tipo Motor de base de datos
        //
        private inConnect_Type _in_DataBaseConn_Type = 0; // Tipo de Conexion
        //
        private String _st_Server_URL = "BDURL"; // URL para la conexion con la bas de datos.
        //
        private String _st_Server_IP_Address = "DBIPAdress"; // Direccion IP
        //
        private String _st_UserApp_PWD = ""; // La clave del usuario de la aplicacion.
        private String _st_UserApp_PWD_Enc = ""; // la clave encriptada del usuario de la aplicacion.
        //
        private Boolean _bl_Access_By_SSO = false; // Indica si ha hecho un acceso SSO
        private long _ln_UserApp_Login_ID = 0; // Id de la tabla, T06USUARIO_ACCESOS, cuando ingresa a la aplicacion.
        private long _ln_Server_Port = 0; // Puerto de conexion a la base de datos.
        private String _st_Server_Monitor = ""; // URL del Servidor Pandora
        private int _in_Server_Monitor_Port = 0; // Puerto de conexional Servidor Pandora
        private String _st_Server_Monitor_UID = "";
        private String _st_Server_Monitor_UID_PWD = "";
        //
        private int _in_CommandTimeout = 0; // Time out for sql statements
        private int _in_ConnectionTimeout = 0; // Time out for database connection


        public CLNBTN_IQy(String LicName )
        {
            CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
        }


        public CLNBTN_IQy(String UserName, String LogFile, String LicName)
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


        public CLNBTN_IQy(String UserName, String LogFile, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
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

        public String getServerName()
        {
            return _st_ServerName;
        }


        public String getDataBaseName()
        {
            return _st_DataBaseName;
        }

        public String getDataBasePath()
        {
            return _st_DataBasePath;
        }

        public String getFileName_Access()
        {
            return _st_FileName_Access;
        }

        public String getDataBase_UserID()
        {
            return _st_DataBase_UserID;
        }

        public String getDataBase_UserPWD()
        {
            return _st_DataBase_UserPWD;
        }

        public inDB_Types getDataBaseEngine_Type()
        {
            return _in_DataBaseEngine_Type;
        }

        public inConnect_Type get_DataBaseConn_Type()
        {
            return _in_DataBaseConn_Type;
        }

        public String getServer_URL()
        {
            return _st_Server_URL;
        }

        public String getServer_IP_Address()
        {
            return _st_Server_IP_Address;
        }


        public String getUserApp_PWD()
        {
            return _st_UserApp_PWD;
        }

        public String getUserApp_PWD_Enc()
        {
            return _st_UserApp_PWD_Enc;
        }

        public Boolean getAccess_By_SSO()
        {
            return _bl_Access_By_SSO;
        }


        public long getUserApp_Login_ID()
        {
            return _ln_UserApp_Login_ID;
        }

        public long getServer_Port()
        {
            return _ln_Server_Port;
        }

        public String getServer_Monitor()
        {
            return _st_Server_Monitor;
        }

        public int getServer_Monitor_Port()
        {
            return _in_Server_Monitor_Port;
        }

        public String getServer_Monitor_UID()
        {
            return _st_Server_Monitor_UID;
        }

        public String getServer_Monitor_UID_PWD()
        {
            return _st_Server_Monitor_UID_PWD;
        }


        public int get_in_CommandTimeout()
        {
            return _in_CommandTimeout;
        }

        public int get_in_ConnectionTimeout()
        {
            return _in_ConnectionTimeout;
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


        public void setServerName(String Datum)
        {
            _st_ServerName = Datum;
        }


        public void setDataBaseName(String Datum)
        {
            _st_DataBaseName = Datum;
        }


        public void setDataBasePath(String Datum)
        {
            _st_DataBasePath = Datum;
        }

        public void setFileName_Access(String Datum)
        {
            _st_FileName_Access = Datum;
        }

        public void setDataBase_UserID(String Datum)
        {
            _st_DataBase_UserID = Datum;
        }

        public void setDataBase_UserPWD(String Datum)
        {
            _st_DataBase_UserPWD = Datum;
        }

        public void setDataBaseEngine_Type(inDB_Types Datum)
        {
            _in_DataBaseEngine_Type = Datum;
        }

        public void setDataBaseConn_Type(inConnect_Type Datum)
        {
            _in_DataBaseConn_Type = Datum;
        }


        public void setServer_URL(String Datum)
        {
            _st_Server_URL = Datum;
        }

        public void setServer_IP_Address(String Datum)
        {
            _st_Server_IP_Address = Datum;
        }

        public void setUserApp_PWD(String Datum)
        {
            _st_UserApp_PWD = Datum;
        }

        public void setUserApp_PWD_Enc(String Datum)
        {
            _st_UserApp_PWD_Enc = Datum;
        }

        public void setAccess_By_SSO(Boolean Datum)
        {
            _bl_Access_By_SSO = Datum;
        }

        public void setUserApp_Login_ID(long UserApp_Login_ID)
        {
            _ln_UserApp_Login_ID = UserApp_Login_ID;
        }

        public void setServer_Port(long Datum)
        {
            _ln_Server_Port = Datum;
        }

        public void setServer_Monitor(String Datum)
        {
            _st_Server_Monitor = Datum;
        }


        public void setServer_Monitor_Port(int Datum)
        {
            _in_Server_Monitor_Port = Datum;
        }

        public void setServer_Monitor_UID(String Datum)
        {
            _st_Server_Monitor_UID = Datum;
        }

        public void setServer_Monitor_UID_PWD(String Datum)
        {
            _st_Server_Monitor_UID_PWD = Datum;
        }

        public void set_in_CommandTimeout(int Datum)
        {
            _in_CommandTimeout = Datum;
        }

        public void set_in_ConnectionTimeout(int Datum)
        {
            _in_ConnectionTimeout = Datum;
        }
    }
}
