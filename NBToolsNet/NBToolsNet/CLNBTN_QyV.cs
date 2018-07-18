using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Runtime.ExceptionServices;


namespace NBToolsNet
{
    public class CLNBTN_QyV
    {
        // Clase equivalente : ClasX_DBValidations	
        /// ClasX_DBValidations : Validaciones para las bases de datos.
        /// Autor : Alvaro S. Quimbaya C.
        /// Fecha : Septiembre 10 2012.
        /// Empresa : Strail SAS
        /// Ult Mod : Alvaro S. Quimbaya C. Julio 18-19 2013
        /// Motivo  : Implementacion de validaciones para PostGreSQL
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        private string _st_User = "CLNBTN_QyV";
        private string _st_FileLog = "C:\\Windows\\CLNBTN_QyV.log";
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_QyV";

        private CLNBTN_IQy ObjPr_InfoBD_Clase = null; // Define el objeto de la informacion de la base de datos con la cual va a trabajar.
        private CLNBTN_Qy ObjPr_Query = null; // Define el objeto para hacer los queries
        //
        private String stPr_NombreBaseDatos = ""; // Nombre de la base de datos, sobre la cual se trabaja.
        //
        private const String TO_DO_INQUERY = "CONSULTA";
        private const String TO_DO_MODI = "MODIFICACION";
        private const String TO_DO_ERASE = "ELIMINAR";
        private const String TO_DO_LET_IN = "INGRESAR";
        private const String TO_DO_SP = "PROCEDURE";
        //
        private const String NEW_LINE = "\r\n"; // Caracteres para nueva linea
        //
        private const String MENSAJE_26 = "Se ha detectado que la base de datos:";
        private const String MENSAJE_27 = "Instalada en el servidor:";
        private const String MENSAJE_28 = "No está actualizada";
        private const String MENSAJE_29 = "Se deben realizar los siguientes cambios en la Base de Datos, para poder ejecutar la aplicación";
        private const String MENSAJE_30 = "Creación de Tablas:";
        private const String MENSAJE_31 = "Creación de Campos:";
        private const String MENSAJE_32 = "Creación de Procedimientos Almacenados:";
        private const String MENSAJE_33 = "Creación de Vistas ( Views ) :";
        private const String MENSAJE_34 = "Creación de Índices:";
        //
        private const String MENSAJE_35 = "Tabla :";
        private const String MENSAJE_36 = "Campo :";
        private const String MENSAJE_37 = "Índice :";


        public CLNBTN_QyV(String UserName, String LogFile, CLNBTN_IQy ObDbInfo, String LicName)
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
                // La informacion de la base de datos para manejar la tabla
                ObjPr_InfoBD_Clase = new CLNBTN_IQy(_st_Lic);
                ObjPr_InfoBD_Clase = ObDbInfo;
                //
                ObjPr_Query = new CLNBTN_Qy(UserName, LogFile, _st_Lic);
                ObjPr_Query.setDataBaseInfo(ObjPr_InfoBD_Clase);
                stPr_NombreBaseDatos = ObjPr_InfoBD_Clase.getDataBaseName();
            }
        }  

        public CLNBTN_QyV(String UserName, String LogFile, CLNBTN_IQy ObDbInfo, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
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
                // La informacion de la base de datos para manejar la tabla
                ObjPr_InfoBD_Clase = new CLNBTN_IQy(_st_Lic);
                ObjPr_InfoBD_Clase = ObDbInfo;
                //
                ObjPr_Query = new CLNBTN_Qy(UserName, LogFile, _st_Lic);
                ObjPr_Query.setDataBaseInfo(ObjPr_InfoBD_Clase);
                stPr_NombreBaseDatos = ObjPr_InfoBD_Clase.getDataBaseName();
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


        [HandleProcessCorruptedStateExceptions]
        public Boolean Is_An_Existing_DataBase(String DB2Validate)
        { // Inicio del  public Boolean Is_A_Existing_DataBase
            /// <summary>
            /// Propiedad DataBase_Exists
            /// Valida si la base de datos existe.
            /// </summary>
            /// <param name="st_NombreBd">Nombre de la base de datos a Validar</param>
            /// <returns>true=Si la base de datos existe,</returns>
            // Valida si la base de datos existe.
            Boolean blL_ExisteBd = false;
            String stL_SQl = "";
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ObjPr_Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    // Hace la conexion
                    ObjPr_Query.setDataBaseInfo(ObjPr_InfoBD_Clase);
                    // Define DataTable, para los Datos del Query
                    DataTable DatTable = null;
                    //
                    switch (ObjPr_InfoBD_Clase.getDataBaseEngine_Type())
                    { // del switch (inPr_TipoMotorBd)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            ///////////////////
                            // SQl Server
                            //////////////////
                            //
                            ObjPr_Query.ToDo_SELECT("*");
                            ObjPr_Query.ToDo_FROM("sys.databases");
                            ObjPr_Query.ToDo_WHERE("name", "'" + DB2Validate.Trim() + "'");
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                blL_ExisteBd = DatTable.Rows.Count > 0;
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                            //
                            stL_SQl = "show databases";
                            // Hace esto para poder ejecutar este tipo de comandos, como show database
                            ObjPr_Query.ToDo_FUNCTION(TO_DO_INQUERY);
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                {
                                    // Toma la informacion de la fila
                                    DataRow Info_Fila = DatTable.Rows[inL_Row];
                                    stL_SQl = "";
                                    stL_SQl = Info_Fila["Database"].ToString();
                                    if (DB2Validate.Trim().ToUpper() == stL_SQl.Trim().ToUpper())
                                    {
                                        blL_ExisteBd = true;
                                        break;
                                    }
                                }
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            break;
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                            // PostGreSQL
                            ObjPr_Query.ToDo_SELECT("*");
                            ObjPr_Query.ToDo_FROM("information_schema.information_schema_catalog_name");
                            ObjPr_Query.ToDo_WHERE("catalog_name", "'" + DB2Validate.Trim() + "'");
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                blL_ExisteBd = DatTable.Rows.Count > 0;
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            //
                            break;
                        default:
                            blL_ExisteBd = true;
                            break;
                    } // fin del switch (inPr_TipoMotorBd)
                    ///////////////////
                }
                return blL_ExisteBd;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_An_Existing_DataBase. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_An_Existing_DataBase. Exception", "", ex.Message.ToString());
            }
            return blL_ExisteBd;
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean Is_A_Valid_DBObject(String DBObjectName2Validate)
        { // Inicio del public Boolean Object_Exists(

            /// Propiedad : Object_Exists 
            /// Valida si un objecto existe en la base de datos.
            /// </summary>
            /// <param name="st_NombreObjeto">Nombre del Objeto a Validar</param>
            /// <returns>true=si existe el objeto</returns>
            /// 
            // Valida si un objeto existe en una base de datos.
            Boolean blL_Existe = false;
            String stL_SQl = "";
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ObjPr_Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    // Hace la conexion
                    ObjPr_Query.setDataBaseInfo(ObjPr_InfoBD_Clase);
                    //
                    // Define DataTable, para los Datos del Query
                    DataTable DatTable = null;
                    //
                    switch (ObjPr_InfoBD_Clase.getDataBaseEngine_Type())
                    { // del switch (inPr_TipoMotorBd)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            ///////////////////
                            // SQl Server
                            //////////////////
                            stL_SQl = " name FROM sysobjects WHERE ";
                            stL_SQl = stL_SQl + " sysobjects.name = '" + DBObjectName2Validate.Trim() + "' ";
                            //
                            ObjPr_Query.ToDo_SELECT(stL_SQl);
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                blL_Existe = DatTable.Rows.Count > 0;
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                            // Para MYSQL Solo hace el Show Tables
                            // Funciona para tablas y vistas
                            //
                            stL_SQl = "show tables";
                            // Hace esto para poder ejecutar este tipo de comandos, como show database, show tables, etc...
                            ObjPr_Query.ToDo_FUNCTION(TO_DO_INQUERY);
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable, stL_SQl);
                            if (DatTable != null)
                            {
                                for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                {
                                    // Toma la informacion de la fila
                                    DataRow Info_Fila = DatTable.Rows[inL_Row];
                                    stL_SQl = "";
                                    stL_SQl = Info_Fila[0].ToString();
                                    if (DBObjectName2Validate.Trim().ToUpper() == stL_SQl.Trim().ToUpper())
                                    {
                                        blL_Existe = true;
                                        break;
                                    }
                                }
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                            // PostGreSQL
                            // Hace UNION, para evaluar, Tablas, Vistas y SPS
                            stL_SQl = " table_name FROM information_schema.tables  WHERE ";
                            stL_SQl += " table_catalog = '" + ObjPr_InfoBD_Clase.getDataBaseName() + "' ";
                            stL_SQl += " AND UPPER( table_name )  = UPPER( '" + DBObjectName2Validate.Trim() + "' )";
                            stL_SQl += " AND table_type = 'BASE TABLE' ";
                            stL_SQl += " UNION ";
                            stL_SQl += " SELECT table_name FROM information_schema.tables  WHERE ";
                            stL_SQl += " table_catalog = '" + ObjPr_InfoBD_Clase.getDataBaseName() + "' ";
                            stL_SQl += " AND UPPER( table_name ) = UPPER( '" + DBObjectName2Validate.Trim() + "' )";
                            stL_SQl += " AND table_type = 'VIEW' ";
                            stL_SQl += " UNION ";
                            stL_SQl += " SELECT routine_name FROM information_schema.routines  WHERE ";
                            stL_SQl += " specific_catalog = '" + ObjPr_InfoBD_Clase.getDataBaseName() + "' ";
                            stL_SQl += " AND UPPER( routine_name )  = UPPER( '" + DBObjectName2Validate.Trim() + "' )";
                            //
                            ObjPr_Query.ToDo_SELECT(stL_SQl);
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                blL_Existe = DatTable.Rows.Count > 0;
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            //
                            break;
                        //
                        default:
                            blL_Existe = true;
                            break;
                    } // fin del switch (inPr_TipoMotorBd)
                    ///////////////////
                }
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_DBObject. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBaseDatos, stL_SQl);
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_DBObject. Exception", "", ex.Message.ToString(), stPr_NombreBaseDatos, stL_SQl);
            }
            return blL_Existe;
        }



        [HandleProcessCorruptedStateExceptions]
        public Boolean Is_A_Valid_DBTable_Field(String DBTableName, String DBFieldName2Validate)
        { // Inicio del public Boolean Is_A_Valid_DBTable_Field(
            /// Propiedad : Field_Table_Exists
            /// Valida si un campo para una tabla existe.
            /// </summary>
            /// <param name="st_NombreTabla">Nombre tabla</param>
            /// <param name="st_NombreCampo">Nombre Campo</param>
            /// <returns>true=si el campo existe en la tabla</returns>
            // Valida si campo existe en una tabla.
            Boolean blL_Existe = false;
            String stL_SQl = "";
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ObjPr_Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    // Hace la conexion
                    ObjPr_Query.setDataBaseInfo(ObjPr_InfoBD_Clase);
                    //
                    // Define DataTable, para los Datos del Query
                    DataTable DatTable = null;
                    //
                    switch (ObjPr_InfoBD_Clase.getDataBaseEngine_Type())
                    { // del switch (inPr_TipoMotorBd)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            ///////////////////
                            // SQl Server
                            //////////////////
                            //
                            stL_SQl = " syscolumns.name FROM sysobjects, syscolumns WHERE ";
                            stL_SQl = stL_SQl + " sysobjects.name = '" + DBTableName.Trim() + "' ";
                            stL_SQl = stL_SQl + " AND syscolumns.name = '" + DBFieldName2Validate.Trim() + "' ";
                            stL_SQl = stL_SQl + " AND  syscolumns.id = sysobjects.id   ";
                            //
                            ObjPr_Query.ToDo_SELECT(stL_SQl);
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            //
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                blL_Existe = DatTable.Rows.Count > 0;
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                            // Para MYSQL ejecuta el 'describe nombre_tabla'
                            // Valida si la tabla Existe
                            if (this.Is_A_Valid_DBObject(DBTableName.Trim()))
                            { // del if (this.Object_Exists(st_NombreTabla.Trim()))
                                stL_SQl = "describe " + DBTableName.Trim();
                                // Hace esto para poder ejecutar este tipo de comandos, como show database, show tables, describe table, etc...
                                ObjPr_Query.ToDo_FUNCTION(TO_DO_INQUERY);
                                ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable, stL_SQl);
                                if (DatTable != null)
                                {
                                    for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                    {
                                        // Toma la informacion de la fila
                                        DataRow Info_Fila = DatTable.Rows[inL_Row];
                                        stL_SQl = "";
                                        stL_SQl = Info_Fila[0].ToString();
                                        if (DBFieldName2Validate.Trim().ToUpper() == stL_SQl.Trim().ToUpper())
                                        {
                                            blL_Existe = true;
                                            break;
                                        }
                                    }
                                }
                                ObjPr_Query.ToDo_CLOSE();
                            } // del if (this.Object_Exists(st_NombreTabla.Trim()))
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                            // PostGreSQL
                            stL_SQl = " column_name FROM information_schema.columns WHERE ";
                            stL_SQl += " table_catalog = '" + ObjPr_InfoBD_Clase.getDataBaseName() + "' ";
                            stL_SQl += " AND UPPER( table_name )  = UPPER( '" + DBTableName.Trim() + "' ) ";
                            stL_SQl += " AND UPPER( column_name )  = UPPER( '" + DBFieldName2Validate.Trim() + "' ) ";
                            //
                            ObjPr_Query.ToDo_SELECT(stL_SQl);
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            //
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                blL_Existe = DatTable.Rows.Count > 0;
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            //
                            break;
                        //
                        default:
                            blL_Existe = true;
                            break;
                    } // fin del switch (inPr_TipoMotorBd)
                    ///////////////////
                }
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_DBTable_Field. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBaseDatos, stL_SQl);
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_DBTable_Field. Exception", "", ex.Message.ToString(), stPr_NombreBaseDatos, stL_SQl);
            }
            return blL_Existe;
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean Is_A_Valid_DBTable_Index(String DBTableName, String DBIndexName2Validate)
        { // Inicio del public Boolean Is_A_Valid_DBTable_Index(
            /// Propiedad : Index_Table_Exists
            /// Valida si un indice existe para una tabla.
            /// </summary>
            /// <param name="st_NombreTabla">Nombre de la tabla</param>
            /// <param name="st_NombreIndice">Nombre del Indice</param>
            /// <returns>true=Si el indice existe en la tabla</returns>
            // Valida si un indice existe en una tabla.
            Boolean blL_Existe = false;
            String stL_SQl = "";
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ObjPr_Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    // Hace la conexion
                    ObjPr_Query.setDataBaseInfo(ObjPr_InfoBD_Clase);
                    //
                    // Define DataTable, para los Datos del Query
                    DataTable DatTable = null;
                    //
                    switch (ObjPr_InfoBD_Clase.getDataBaseEngine_Type())
                    { // del switch (inPr_TipoMotorBd)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            ///////////////////
                            // SQl Server
                            //////////////////
                            //
                            stL_SQl = " sysobjects.name , sysindexes.name FROM sysobjects, sysindexes WHERE ";
                            stL_SQl = stL_SQl + " sysobjects.name = '" + DBTableName.Trim() + "' ";
                            stL_SQl = stL_SQl + " AND sysindexes.name = '" + DBIndexName2Validate.Trim() + "' ";
                            stL_SQl = stL_SQl + " AND sysobjects.id = sysindexes.id ";
                            //
                            ObjPr_Query.ToDo_SELECT(stL_SQl);
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            //
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                blL_Existe = DatTable.Rows.Count > 0;
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            break;
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                            // PostGreSQL
                            //
                            stL_SQl = " * FROM pg_catalog.pg_indexes WHERE 0 = 0 ";
                            //stL_SQl += " table_catalog = '" + ObjPr_InfoBD_Clase.getNombreBaseDatos() + "' ";
                            stL_SQl += " AND UPPER( tablename )  = UPPER( '" + DBTableName.Trim() + "' ) ";
                            stL_SQl += " AND UPPER( indexname )  = UPPER( '" + DBIndexName2Validate.Trim() + "' ) ";
                            //
                            ObjPr_Query.ToDo_SELECT(stL_SQl);
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            //
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                blL_Existe = DatTable.Rows.Count > 0;
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            //
                            break;
                        //
                        default:
                            blL_Existe = true;
                            break;
                    } // fin del switch (inPr_TipoMotorBd)
                    ///////////////////
                }
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_DBTable_Index. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBaseDatos, stL_SQl);
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_DBTable_Index. Exception", "", ex.Message.ToString(), stPr_NombreBaseDatos, stL_SQl);
            }
            return blL_Existe;
        }



        [HandleProcessCorruptedStateExceptions]
        public int BringMe_DB_Table_FieldLengh(String DBTableName, String DBFieldName2Validate)
        { // Inicio del public int BringMe_DB_Table_FieldLengh(
            // Devuelve la longitud de un campo.
            int inL_Long = 0;
            long lnL_IdTabla = 0;
            String stL_SQl = "";
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ObjPr_Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    // Hace la conexion
                    ObjPr_Query.setDataBaseInfo(ObjPr_InfoBD_Clase);
                    //
                    CLNBTN_Ul ObjL_Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    //
                    // Define DataTable, para los Datos del Query
                    DataTable DatTable = null;
                    //
                    switch (ObjPr_InfoBD_Clase.getDataBaseEngine_Type())
                    { // del switch (inPr_TipoMotorBd)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            ///////////////////
                            // SQl Server
                            //////////////////
                            // Halla el Id de la tabla
                            stL_SQl = " id  FROM sysobjects  WHERE ";
                            stL_SQl = stL_SQl + " sysobjects.name = '" + DBTableName.Trim() + "' ";
                            //
                            ObjPr_Query.ToDo_SELECT(stL_SQl);
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            //
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                {
                                    // Toma la informacion de la fila
                                    DataRow Info_Fila = DatTable.Rows[inL_Row];
                                    lnL_IdTabla = 0;
                                    //if (!DBNull.Value.Equals(Info_Fila[0].ToString()))
                                    //{
                                        if (Info_Fila[0].ToString().Length > 0) lnL_IdTabla = Convert.ToInt32(Info_Fila[0].ToString());
                                    //}
                                }
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            //
                            if (lnL_IdTabla > 0)
                            { // del if (lnL_IdTabla > 0)
                                stL_SQl = "";
                                stL_SQl = " length   FROM syscolumns  WHERE ";
                                stL_SQl = stL_SQl + " syscolumns.id = " + lnL_IdTabla;
                                stL_SQl = stL_SQl + " AND syscolumns.name = '" + DBFieldName2Validate.Trim() + "' ";
                                ObjPr_Query.ToDo_SELECT(stL_SQl);
                                // Toma la instruccion SQL que esta ejecutando.
                                //stL_SQl = ObjPr_Query.getSqlCommand();
                                //
                                ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                                if (DatTable != null)
                                {
                                    for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                    {
                                        // Toma la informacion de la fila
                                        DataRow Info_Fila = DatTable.Rows[inL_Row];
                                        inL_Long = 0;
                                        //if (!DBNull.Value.Equals(Info_Fila["length"].ToString()))
                                        //{
                                            if (Info_Fila["length"].ToString().Length > 0) inL_Long = Convert.ToInt16(Info_Fila["length"].ToString());
                                        //}
                                    }
                                }
                                ObjPr_Query.ToDo_CLOSE();
                            } // del if (lnL_IdTabla > 0)
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                            // PostGreSQL
                            //
                            stL_SQl = " character_maximum_length FROM information_schema.columns WHERE ";
                            stL_SQl += " table_catalog = '" + ObjPr_InfoBD_Clase.getDataBaseName() + "' ";
                            stL_SQl += " AND UPPER( table_name ) = UPPER( '" + DBTableName.Trim() + "' ) ";
                            stL_SQl += " AND UPPER( column_name ) = UPPER( '" + DBFieldName2Validate.Trim() + "' ) ";
                            ObjPr_Query.ToDo_SELECT(stL_SQl);
                            // Toma la instruccion SQL que esta ejecutando.
                            //stL_SQl = ObjPr_Query.getSqlCommand();
                            //
                            ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            {
                                for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                {
                                    // Toma la informacion de la fila
                                    DataRow Info_Fila = DatTable.Rows[inL_Row];
                                    inL_Long = 0;
                                    //if (!DBNull.Value.Equals(Info_Fila[0].ToString()))
                                    //{
                                        if (Info_Fila[0].ToString().Length > 0) inL_Long = Convert.ToInt16(Info_Fila[0].ToString());
                                    //}
                                }
                            }
                            ObjPr_Query.ToDo_CLOSE();
                            //
                            break;
                        //
                        default:
                            inL_Long = 0;
                            break;
                    } // fin del switch (inPr_TipoMotorBd)
                    ///////////////////
                }
                return inL_Long;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_DB_Table_FieldLengh. System.AccessViolationException", "", ex_0.Message.ToString(), stPr_NombreBaseDatos, stL_SQl);
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_DB_Table_FieldLengh. Exception", "", ex.Message.ToString(), stPr_NombreBaseDatos, stL_SQl);
            }
            return inL_Long;
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean Let_BDObjects2Validate(String ConfFile, String Section2Read, String OutFile)
        { // Inicio del public Boolean Valid_Objects_BD(
            /// Metodo : Valid_Objects_BD
            /// Encargado de hacer las validaciones de la base de datos, con base en un archivo de configuracion
            /// definido en ConfFile, y para la seccion definida en Section2Read
            /// Genera el archivo , OutFile , en formato texto, con la inconsistencias encontradas.
            /// </summary>
            /// <param name="ConfFile">Ruta y nombre del archivo de configuracion, con el cual se hacen las validaciones</param>
            /// <param name="Section2Read">Nombre de la seccion de la cual se obtienen los objetos a validar</param>
            /// <param name="OutFile">Ruta y nombre del archivo de salida, con las inconsistencias encontradas</param>
            /// <returns></returns>
            // Realiza las validaciones de existencia de tablas, objetos, etc...
            // para una base de datos, con base un archivo .Conf
            Boolean blL_ValidacionesOK = false;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    switch (ObjPr_InfoBD_Clase.getDataBaseEngine_Type())
                    { // del switch (inPr_TipoMotorBd)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            ///////////////////
                            // SQl Server
                            //////////////////
                            blL_ValidacionesOK = this.Let_BDObjects2Validate_DATA_BASE(ConfFile, Section2Read, OutFile);
                            break;
                        //
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                            /////////////
                            // MYSQL
                            ////////////
                            blL_ValidacionesOK = this.Let_BDObjects2Validate_DATA_BASE(ConfFile, Section2Read, OutFile);
                            break;
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                            /////////////
                            // PostGreSQL
                            ////////////
                            blL_ValidacionesOK = this.Let_BDObjects2Validate_DATA_BASE(ConfFile, Section2Read, OutFile);
                            break;
                        default:
                            blL_ValidacionesOK = true;
                            break;
                    } // fin del switch (inPr_TipoMotorBd)
                    ///////////////////
                }
                return blL_ValidacionesOK;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_BDObjects2Validate. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_BDObjects2Validate. Exception", "", ex.Message.ToString());
            }
            return blL_ValidacionesOK;
        }


        [HandleProcessCorruptedStateExceptions]
        private Boolean Let_BDObjects2Validate_DATA_BASE(String ConfFile, String Section2Read, String OutFile)
        { // Inicio del private Boolean Let_BDObjects2Validate_DATA_BASE(
            /// Metodo : Valid_Objects_DATA_BASE
            /// Metodo privado encargado de hacer las validaciones sobre la base de datos
            /// Valida :
            /// Existencia de tablas
            /// Existencia de Campos en las tablas
            /// Existencia de Vistas ( Para SQl Server y MYSQL)
            /// Existencia de Procedimientos Almacenados ( Para SQl Server ) 
            /// </summary>
            /// <param name="ConfFile">Ruta y nombre del archivo de configuracion, con el cual se hacen las validaciones</param>
            /// <param name="Section2Read">Nombre de la seccion de la cual se obtienen los objetos a validar</param>
            /// <param name="OutFile">Ruta y nombre del archivo de salida, con las inconsistencias encontradas</param>
            /// <returns></returns>
            // Realiza las validaciones de existencia de tablas, objetos, etc...
            // para una base de datos, con base un archivo .Conf
            // Rangos :
            // Tablas 20-999
            // Campos 1000-1999
            // Procedimientos Almacenados 2000-2999
            // Vistas 3000-3999
            // Indices 4000-4999
            Boolean blL_ValidacionesOK = false;
            String stL_Aux = "";
            int inL_Pos = 0;
            String stL_Tabla = "";
            String stL_ListaTablas = "";
            String stL_Campo = "";
            String stL_ListaCampos = "";
            String stL_ListaSps = "";
            String stL_ListaVistas = "";
            String stL_ListaIndices = "";
            String stL_Registro = "";
            //
            Boolean blL_TablasOk = true;
            Boolean blL_CamposOk = true;
            Boolean blL_SpsOK = true;
            Boolean blL_Vistas = true;
            Boolean blL_Indices = true;
            //
            int inL_TablaInicio = 0;
            int inL_TablaFin = 0;
            int inL_CampoInicio = 0;
            int inL_CampoFin = 0;
            int inL_SPInicio = 0;
            int inL_SPFin = 0;
            int inL_VistaInicio = 0;
            int inL_VistaFin = 0;
            int inL_IndiceInicio = 0;
            int inL_IndiceFin = 0;
            //
            int inL_Index = 0;
            //
            StreamWriter outt = null;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Para trabajar con el archivo de configuracion.
                    CLNBTN_Cg ObL_Conf = new CLNBTN_Cg(ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    //
                    // Define los rangos
                    inL_TablaInicio = 20;
                    inL_TablaFin = 999;
                    //
                    inL_CampoInicio = 1000;
                    inL_CampoFin = 1999;
                    //
                    inL_SPInicio = 2000;
                    inL_SPFin = 2999;
                    //
                    inL_VistaInicio = 3000;
                    inL_VistaFin = 3999;
                    //
                    inL_IndiceInicio = 4000;
                    inL_IndiceFin = 4999;
                    /////////////////////////////////////////////////////////////////////////////
                    // Dependiendo del tipo de motor de la base de datos
                    // deja en cero ( 0 ) , los rangos que no se desean validar.
                    /////////////////////////////////////////////////////////////////////////////
                    switch (ObjPr_InfoBD_Clase.getDataBaseEngine_Type())
                    { // del switch (inPr_TipoMotorBd)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                            //
                            inL_SPInicio = 0;
                            inL_SPFin = 0;
                            //
                            inL_IndiceInicio = 0;
                            inL_IndiceFin = 0;
                            break;
                        default:
                            //
                            break;
                    } // fin del // del switch (inPr_TipoMotorBd)
                    //////////////////////////////////////////////////////////////////
                    // Valida las Tablas
                    // Tablas 20-999
                    //////////////////////////////////////////////////////////////////
                    for (inL_Index = inL_TablaInicio; inL_Index <= inL_TablaFin; inL_Index++)
                    { // Inicio del for ( inL_Index = inL_TablaInicio;
                        stL_Tabla = "";
                        stL_Tabla = ObL_Conf.ReadAKeyFromSection(Section2Read, Convert.ToString(inL_Index));
                        //
                        if (stL_Tabla.Length == 0)
                        {
                            // Si la info viene vacia sale del for
                            break;
                        }
                        else
                        {
                            if (!this.Is_A_Valid_DBObject(stL_Tabla))
                            {
                                if (stL_ListaTablas.Length == 0)
                                {
                                    stL_ListaTablas = stL_Tabla;
                                }
                                else
                                {
                                    stL_ListaTablas = stL_ListaTablas + NEW_LINE + stL_Tabla;
                                }
                            }
                        }
                    } // Fin del for ( inL_Index = inL_TablaInicio;
                    //////////////////////////////////////////////////
                    // Valida los campos 
                    // Campos 1000-1999
                    //////////////////////////////////////////////////
                    for (inL_Index = inL_CampoInicio; inL_Index <= inL_CampoFin; inL_Index++)
                    { // Inicio del for ( inL_Index = inL_CampoInicio;
                        stL_Aux = "";
                        stL_Tabla = "";
                        stL_Campo = "";
                        //
                        stL_Aux = ObL_Conf.ReadAKeyFromSection(Section2Read, Convert.ToString(inL_Index));
                        inL_Pos = stL_Aux.IndexOf("*");
                        if (inL_Pos > -1)
                        {
                            // Tiene tabla y campo 
                            stL_Tabla = stL_Aux.Substring(0, inL_Pos);
                            stL_Campo = stL_Aux.Substring(inL_Pos + 1, stL_Aux.Length - inL_Pos - 1);
                        }
                        //
                        if (stL_Tabla.Length == 0 && stL_Campo.Length == 0)
                        {
                            // Si la info viene vacia sale del for
                            break;
                        }
                        else
                        {
                            if (!this.Is_A_Valid_DBTable_Field(stL_Tabla, stL_Campo))
                            {
                                if (stL_ListaCampos.Length == 0)
                                {
                                    stL_ListaCampos = MENSAJE_35 + " " + stL_Tabla + " " + MENSAJE_36 + " " + stL_Campo;
                                }
                                else
                                {
                                    stL_ListaCampos = stL_ListaCampos + MENSAJE_35 + " " + stL_Tabla + " " + MENSAJE_36 + " " + stL_Campo;
                                }
                            }
                        }
                    } // Fin del for ( inL_Index = inL_CampoInicio;
                    //////////////////////////////////////////////////////////////////
                    // Valida Sps 
                    // Procedimientos Almacenados 2000-2999
                    //////////////////////////////////////////////////////////////////
                    for (inL_Index = inL_SPInicio; inL_Index <= inL_SPFin; inL_Index++)
                    { // Inicio del for ( inL_Index = inL_SPInicio;
                        stL_Tabla = "";
                        stL_Tabla = ObL_Conf.ReadAKeyFromSection(Section2Read, Convert.ToString(inL_Index));
                        //
                        if (stL_Tabla.Length == 0)
                        {
                            // Si la info viene vacia sale del for
                            break;
                        }
                        else
                        {
                            if (!this.Is_A_Valid_DBObject(stL_Tabla))
                            {
                                if (stL_ListaSps.Length == 0)
                                {
                                    stL_ListaSps = stL_Tabla;
                                }
                                else
                                {
                                    stL_ListaSps = stL_ListaSps + NEW_LINE + stL_Tabla;
                                }
                            }
                        }
                    } // Fin del for ( inL_Index = inL_SPInicio;
                    //////////////////////////////////////////////////////////////////
                    // Valida Vistas ( Views )  
                    // Vistas 3000-3999
                    //////////////////////////////////////////////////////////////////
                    for (inL_Index = inL_VistaInicio; inL_Index <= inL_VistaFin; inL_Index++)
                    { // Inicio del for ( inL_Index = inL_VistaInicio;
                        stL_Tabla = "";
                        stL_Tabla = ObL_Conf.ReadAKeyFromSection(Section2Read, Convert.ToString(inL_Index));
                        //
                        if (stL_Tabla.Length == 0)
                        {
                            // Si la info viene vacia sale del for
                            break;
                        }
                        else
                        {
                            if (!this.Is_A_Valid_DBObject(stL_Tabla))
                            {
                                if (stL_ListaVistas.Length == 0)
                                {
                                    stL_ListaVistas = stL_Tabla;
                                }
                                else
                                {
                                    stL_ListaVistas = stL_ListaVistas + NEW_LINE + stL_Tabla;
                                }
                            }
                        }
                    } // Fin del for ( inL_Index = inL_VistaInicio;
                    //////////////////////////////////////////////////////////////////
                    // Valida Indices  
                    // Indices 4000-4999
                    //////////////////////////////////////////////////////////////////
                    for (inL_Index = inL_IndiceInicio; inL_Index <= inL_IndiceFin; inL_Index++)
                    { // Inicio del for ( inL_Index = inL_IndiceInicio;
                        stL_Aux = "";
                        stL_Tabla = "";
                        stL_Campo = "";
                        //
                        stL_Aux = ObL_Conf.ReadAKeyFromSection(Section2Read, Convert.ToString(inL_Index));
                        inL_Pos = stL_Aux.IndexOf("*");
                        if (inL_Pos > -1)
                        {
                            // Tiene tabla y Indice 
                            stL_Tabla = stL_Aux.Substring(0, inL_Pos);
                            stL_Campo = stL_Aux.Substring(inL_Pos + 1, stL_Aux.Length - inL_Pos - 1);
                        }
                        //
                        if (stL_Tabla.Length == 0 && stL_Campo.Length == 0)
                        {
                            // Si la info viene vacia sale del for
                            break;
                        }
                        else
                        {
                            if (!this.Is_A_Valid_DBTable_Index(stL_Tabla, stL_Campo))
                            {
                                if (stL_ListaIndices.Length == 0)
                                {
                                    stL_ListaIndices = MENSAJE_35 + " " + stL_Tabla + " " + MENSAJE_37 + " " + stL_Campo;
                                }
                                else
                                {
                                    stL_ListaIndices = stL_ListaIndices + MENSAJE_35 + " " + stL_Tabla + " " + MENSAJE_37 + " " + stL_Campo;
                                }
                            }
                        }
                    } // Fin del for ( inL_Index = inL_IndiceInicio;
                    //////////////////////////////////////////////////////////////////
                    // Arma el encabezado
                    //////////////////////////////////////////////////////////////////
                    blL_TablasOk = (stL_ListaTablas.Length == 0);
                    blL_CamposOk = (stL_ListaCampos.Length == 0);
                    blL_SpsOK = (stL_ListaSps.Length == 0);
                    blL_Vistas = (stL_ListaVistas.Length == 0);
                    blL_Indices = (stL_ListaIndices.Length == 0);
                    //
                    blL_ValidacionesOK = (blL_TablasOk && blL_CamposOk && blL_SpsOK && blL_Vistas && blL_Indices);
                    //
                    if (!blL_ValidacionesOK)
                    { // del if (!blL_ValidacionesOK)
                        //
                        stL_Registro = "";
                        stL_Registro = MENSAJE_26 + " " + ObjPr_InfoBD_Clase.getDataBaseName();
                        if (ObjPr_InfoBD_Clase.getServerName().Length > 0)
                        {
                            stL_Registro = stL_Registro + NEW_LINE + MENSAJE_27 + " " + ObjPr_InfoBD_Clase.getServerName(); ;
                        }
                        stL_Registro = stL_Registro + NEW_LINE + MENSAJE_28;
                        stL_Registro = stL_Registro + NEW_LINE + MENSAJE_29;
                        //
                        if (!blL_TablasOk)
                        {
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + NEW_LINE + MENSAJE_30;
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + stL_ListaTablas;
                        }
                        //
                        if (!blL_CamposOk)
                        {
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + NEW_LINE + MENSAJE_31;
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + stL_ListaCampos;
                        }
                        //
                        if (!blL_SpsOK)
                        {
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + NEW_LINE + MENSAJE_32;
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + stL_ListaSps;
                        }
                        //
                        if (!blL_Vistas)
                        {
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + NEW_LINE + MENSAJE_33;
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + stL_ListaVistas;
                        }
                        //
                        if (!blL_Indices)
                        {
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + NEW_LINE + MENSAJE_34;
                            stL_Registro = stL_Registro + NEW_LINE;
                            stL_Registro = stL_Registro + stL_ListaIndices;
                        }
                        // EScribe el archivo plano
                        if (File.Exists(OutFile))
                        {
                            File.Delete(OutFile);
                        }
                        //
                        outt = File.AppendText(OutFile);
                        //
                        outt.WriteLine(stL_Registro);//Escritura de los parametros linea a linea en el vector
                        outt.Flush();
                        outt.Close();//cierre del archivo
                        //
                    } // del if (!blL_ValidacionesOK)
                    //
                }
                return blL_ValidacionesOK;
            }
            catch (System.AccessViolationException ex_0)
            {
                blL_ValidacionesOK = false;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_BDObjects2Validate_DATA_BASE. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                blL_ValidacionesOK = false;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_BDObjects2Validate_DATA_BASE. Exception", "", ex.Message.ToString());
            }
            return blL_ValidacionesOK;
        } 







    }
}
