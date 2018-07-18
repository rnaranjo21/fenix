using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public class CLNBTN_Ta
    {
        // Clase Equivalente : ClasX_AccesoTabla		
        /// 
        /// ClasX_AccesoTabla : Clase encaragada de accesar una tabla
        /// Para poder hacer insert, update y delete sobre una tabla determinada.
        /// Autor : Alvaro S. Quimbaya C.
        /// Fecha : Septiembre 7 2012.
        /// Empresa : Strail SAS
        ///
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        private string _st_User = "CLNBTN_Ta";
        private string _st_FileLog = "C:\\Windows\\CLNBTN_Ta.log";
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Ta";
        //
        private String _st_TableName = ""; //  Define el nombre de la tabla, con la cual se va a trabajar.
        private CLNBTN_IQy ObjPr_InfoBD_Tabla = null; // Define el objeto de la informacion de la base de datos con la cual va a trabajar.
        private CLNBTN_Qy ObjPr_Query = null; // Define el objeto para hacer los queries

        //
        const int IN_MAX_CAMPOS = 500; // Maximo numero de campos a trabajar.
        const String SEM_DADO = "SEM_DADO1_XDFR"; // Indica que no tiene dato el elemento de los arreglos.
        private String[] stPr_Campos_Tabla; // nombres de los campos de las tabla a trabajar
        private Boolean[] blPr_Llaves_Tabla; // campos que son llave en tabla.
        private String[] stPr_Tipo_Dato_Campo; // los tipos de datos de los campos de la llave.
        private String[] stPr_Tag_Campo; //  Tags, o datos adicionales de cada campo de la tabla.
        private String[] stPr_Contenido_Campo; //  Tags, o datos adicionales de cada campo de la tabla.
        private Boolean[] blPr_Actualiza_En_Modificacion; // Para saber si Actualiza en modificacion el campo.
        private Boolean[] blPr_Auto_Incrementable; // Para saber si el campo es AutoIncrementable, para no colocarlo en la instruccion Insert.
        private Boolean[] blPr_GrabaSolo_En_Modificacion; // Para saber que campos se graban unicamente en Modificacion.
        //
        private int _in_Quantity_Fields = 0; // Cantidad de Campos que se han adicionado.
        //
        private CLNBTN_IQy.inDB_Types inPr_TipoServidor = CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER; // Tipo de Motor de la base de datos.
        //
        //
        private const String CLASX_TIPO_DATO_STRING = "S";
        private const String CLASX_TIPO_DATO_NUMERIC = "N";
        private const String CLASX_TIPO_DATO_DATE = "D";
        private const String CLASX_TIPO_DATO_BOOLEAN = "B";
        private const String CLASX_TIPO_DATO_DOUBLE = "U";
        //
        private const String SYSTEM_DATETIME = "SYSTEM_DATE_TIME";
        //



        public CLNBTN_Ta(String UserName, String LogFile, CLNBTN_IQy ObDbInfo, String LicName)
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
                    // Redimenciona arreglos.
                    stPr_Campos_Tabla = new String[IN_MAX_CAMPOS];
                    blPr_Llaves_Tabla = new Boolean[IN_MAX_CAMPOS];
                    stPr_Tipo_Dato_Campo = new String[IN_MAX_CAMPOS];
                    stPr_Tag_Campo = new String[IN_MAX_CAMPOS];
                    stPr_Contenido_Campo = new String[IN_MAX_CAMPOS];
                    blPr_Actualiza_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                    blPr_Auto_Incrementable = new Boolean[IN_MAX_CAMPOS];
                    //
                    blPr_GrabaSolo_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                    //
                    _in_Quantity_Fields = 0;
                    //
                    _st_User = UserName;
                    _st_FileLog = LogFile;
                    // Set Info DB
                    this.setDataBaseInfo(ObDbInfo);
                    //
                    this.ToDo_Clear_Arrays();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Ta(1). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Ta(1). Exception", "", ex.Message.ToString());
            }
        }


         public CLNBTN_Ta(String UserName, String LogFile, CLNBTN_IQy ObDbInfo, String Table_Name , String LicName)
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
                    // Nombre de la tala
                    _st_TableName = Table_Name;
                    // Redimenciona arreglos.
                    stPr_Campos_Tabla = new String[IN_MAX_CAMPOS];
                    blPr_Llaves_Tabla = new Boolean[IN_MAX_CAMPOS];
                    stPr_Tipo_Dato_Campo = new String[IN_MAX_CAMPOS];
                    stPr_Tag_Campo = new String[IN_MAX_CAMPOS];
                    stPr_Contenido_Campo = new String[IN_MAX_CAMPOS];
                    blPr_Actualiza_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                    blPr_Auto_Incrementable = new Boolean[IN_MAX_CAMPOS];
                    //
                    blPr_GrabaSolo_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                    //
                    _in_Quantity_Fields = 0;
                    //
                    _st_User = UserName;
                    _st_FileLog = LogFile;
                    // Set Info DB
                    this.setDataBaseInfo(ObDbInfo);
                    //
                    this.ToDo_Clear_Arrays();
                }
            }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Ta(2). System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Ta(2). Exception", "", ex.Message.ToString());
             }
        }


         public CLNBTN_Ta(String UserName, String LogFile, CLNBTN_IQy ObDbInfo, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
        {
            try
            {
                //
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    _bl_OutFileLog = OutFileLog;
                    _bl_OutLineConsole = OutLineConsole;
                    _bl_OutWindow = OutWindow; 
                    //
                    // Redimenciona arreglos.
                    stPr_Campos_Tabla = new String[IN_MAX_CAMPOS];
                    blPr_Llaves_Tabla = new Boolean[IN_MAX_CAMPOS];
                    stPr_Tipo_Dato_Campo = new String[IN_MAX_CAMPOS];
                    stPr_Tag_Campo = new String[IN_MAX_CAMPOS];
                    stPr_Contenido_Campo = new String[IN_MAX_CAMPOS];
                    blPr_Actualiza_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                    blPr_Auto_Incrementable = new Boolean[IN_MAX_CAMPOS];
                    //
                    blPr_GrabaSolo_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                    //
                    _in_Quantity_Fields = 0;
                    //
                    _st_User = UserName;
                    _st_FileLog = LogFile;
                    // Set Info DB
                    this.setDataBaseInfo(ObDbInfo);
                    //
                    this.ToDo_Clear_Arrays();
                }
            }
            catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Ta(3). System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Ta(3). Exception", "", ex.Message.ToString());
             }
        }


         public CLNBTN_Ta(String UserName, String LogFile, CLNBTN_IQy ObDbInfo, String Table_Name, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
        {
            try
            {
                //
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    _bl_OutFileLog = OutFileLog;
                    _bl_OutLineConsole = OutLineConsole;
                    _bl_OutWindow = OutWindow; 
                    //
                    // Nombre de la tala
                    _st_TableName = Table_Name;
                    // Redimenciona arreglos.
                    stPr_Campos_Tabla = new String[IN_MAX_CAMPOS];
                    blPr_Llaves_Tabla = new Boolean[IN_MAX_CAMPOS];
                    stPr_Tipo_Dato_Campo = new String[IN_MAX_CAMPOS];
                    stPr_Tag_Campo = new String[IN_MAX_CAMPOS];
                    stPr_Contenido_Campo = new String[IN_MAX_CAMPOS];
                    blPr_Actualiza_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                    blPr_Auto_Incrementable = new Boolean[IN_MAX_CAMPOS];
                    //
                    blPr_GrabaSolo_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                    //
                    _in_Quantity_Fields = 0;
                    //
                    _st_User = UserName;
                    _st_FileLog = LogFile;
                    // Set Info DB
                    this.setDataBaseInfo(ObDbInfo);
                    //
                    this.ToDo_Clear_Arrays();
                }
            }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Ta(4). System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_Ta(4). Exception", "", ex.Message.ToString());
             }
        }

         [HandleProcessCorruptedStateExceptions]
         public void ToDo_Clear()
         { // Inicio del public void ToDo_Clear(
             /// <summary>
             /// Metodo : Clear
             /// Limpia Variables redimensiona arreglos de los campos.
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
                     // Redimensiona arreglos.
                     stPr_Campos_Tabla = new String[IN_MAX_CAMPOS];
                     blPr_Llaves_Tabla = new Boolean[IN_MAX_CAMPOS];
                     stPr_Tipo_Dato_Campo = new String[IN_MAX_CAMPOS];
                     stPr_Tag_Campo = new String[IN_MAX_CAMPOS];
                     stPr_Contenido_Campo = new String[IN_MAX_CAMPOS];
                     blPr_Actualiza_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                     blPr_Auto_Incrementable = new Boolean[IN_MAX_CAMPOS];
                     //
                     blPr_GrabaSolo_En_Modificacion = new Boolean[IN_MAX_CAMPOS];
                     //
                     _in_Quantity_Fields = 0;
                     //
                     this.ToDo_Clear_Arrays();
                     //
                 }
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Clear. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Clear. Exception", "", ex.Message.ToString());
             }
         }


         [HandleProcessCorruptedStateExceptions]
         public void ToDo_Clear_Info_Only()
         { // Inicio del public void ToDo_Clear_Info_Only(
             /// <summary>
             /// Metodo : Clear_Info_Only
             /// Limpia informacion de los campos unicamente
             /// </summary>
             int inL_Index = 0;
             try
             {
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     // Limpia Informacion de los campos unicamente.
                     if (_in_Quantity_Fields > 0)
                     {
                         for (inL_Index = 0; inL_Index <= _in_Quantity_Fields; inL_Index++)
                         {
                             stPr_Contenido_Campo[inL_Index] = SEM_DADO;
                         }
                     }
                     //
                 }
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Clear_Info_Only. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Clear_Info_Only. Exception", "", ex.Message.ToString());
             }
         }



         [HandleProcessCorruptedStateExceptions]
         private void ToDo_Clear_Arrays()
         { // Inicio del public void ToDo_Clear_Arrays(
             /// <summary>
             /// Metodo : Clear_Arrays
             /// Limpia los arreglos que se utilizan
             /// </summary>
             // Limpia todos los Arreglos.
             int inL_Index = 0;
             try
             {
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     for (inL_Index = 0; inL_Index < IN_MAX_CAMPOS; inL_Index++)
                     {
                         stPr_Campos_Tabla[inL_Index] = SEM_DADO;
                         stPr_Contenido_Campo[inL_Index] = SEM_DADO;
                         blPr_Llaves_Tabla[inL_Index] = false;
                         stPr_Tipo_Dato_Campo[inL_Index] = SEM_DADO;
                         stPr_Tag_Campo[inL_Index] = SEM_DADO;
                         stPr_Contenido_Campo[inL_Index] = SEM_DADO;
                         // Por defecto se graba en la modificacion 
                         blPr_Actualiza_En_Modificacion[inL_Index] = true;
                         // Por defecto No es campo AutoIncrementable
                         blPr_Auto_Incrementable[inL_Index] = false;
                         // Por Defecto se graban todos los campos cuando se crea el registro.
                         blPr_GrabaSolo_En_Modificacion[inL_Index] = false;
                     }
                 }
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Clear_Arrays. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Clear_Arrays. Exception", "", ex.Message.ToString());
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


         public string getTableName()
         {
             return _st_TableName;
         }

         public int getQuantity_Fields()
         {
             return _in_Quantity_Fields;
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

         public void setTableName(string TableName)
         {
             _st_TableName = TableName;
         }


         public void setDataBaseInfo( CLNBTN_IQy ObDbInfo)
         {
             try
             {
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     
                     // La informacion de la base de datos para manejar la tabla
                     ObjPr_InfoBD_Tabla = new CLNBTN_IQy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                     ObjPr_InfoBD_Tabla = ObDbInfo;
                     //
                     ObjPr_Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                     ObjPr_Query.setDataBaseInfo(ObjPr_InfoBD_Tabla);
                     //
                     // Toma el tipo de motor de la base de datos.
                     inPr_TipoServidor = ObjPr_InfoBD_Tabla.getDataBaseEngine_Type();
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




         [HandleProcessCorruptedStateExceptions]
         public void ToDo_AddField(String Table_Field, Boolean Is_Key_Field, String DataType_Field, String Tag_Info, Boolean ToSave_IF_Exists, Boolean Is_AutoIncre = false, Boolean ToSave_Only_Exists = false)
         { // Inicio del public void ToDo_AddField
             /// <summary>
             /// Metodo : AddField
             /// Adiciona un campo para trabajar con la tabla
             /// </summary>
             /// <param name="st_Campo_Tabla">Nombre del campo</param>
             /// <param name="bl_Campo_Llave">true = campo llave de la tabla</param>
             /// <param name="st_TipoDatoCampo">Tipo de Dato, String, Numerico o Date</param>
             /// <param name="st_Tag">Informacion Adicional, para el campo</param>
             try
             {
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     // Incrementa en 1 el numero de campos
                     _in_Quantity_Fields = _in_Quantity_Fields + 1;
                     // Asigna a los arreglos la informacion
                     stPr_Campos_Tabla[_in_Quantity_Fields] = Table_Field.Trim();
                     blPr_Llaves_Tabla[_in_Quantity_Fields] = Is_Key_Field;
                     stPr_Tipo_Dato_Campo[_in_Quantity_Fields] = DataType_Field.Trim();
                     stPr_Tag_Campo[_in_Quantity_Fields] = Tag_Info.Trim();
                     blPr_Actualiza_En_Modificacion[_in_Quantity_Fields] = ToSave_IF_Exists;
                     blPr_Auto_Incrementable[_in_Quantity_Fields] = Is_AutoIncre;
                     //
                     blPr_GrabaSolo_En_Modificacion[_in_Quantity_Fields] = ToSave_Only_Exists;
                     //
                 }
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_AddField. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_AddField. Exception", "", ex.Message.ToString());
             }
         }

         [HandleProcessCorruptedStateExceptions]
         public Boolean Is_An_Existing_Field(String Table_Field, ref int Index_Table_Field)
         { // Inicio del public void Is_An_Existing_Field
             /// <summary>
             /// Metodo : ExisteCampo
             /// Valida si un campo existe.
             /// </summary>
             /// <param name="st_Campo">Nombre del Campo</param>
             /// <param name="inR_IndexCampo">Devuelve el indice del campo en el arreglo de campos</param>
             /// <returns></returns>
             int inL_Index = 0;
             Boolean blL_Existe = false;
             try
             {
                 //
                 Index_Table_Field = 0;
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     {
                         for (inL_Index = 0; inL_Index <= _in_Quantity_Fields; inL_Index++)
                         {
                             if (stPr_Campos_Tabla[inL_Index].Trim().ToUpper() == Table_Field.Trim().ToUpper())
                             {
                                 Index_Table_Field = inL_Index;
                                 blL_Existe = true;
                                 break;
                             }
                         }
                     }
                 }
                 return blL_Existe;
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_An_Existing_Field. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_An_Existing_Field. Exception", "", ex.Message.ToString());
             }
             return blL_Existe;
         }



         [HandleProcessCorruptedStateExceptions]
         private void BringMe_PrimaryKey4Query(ref CLNBTN_Qy SQl)
         { // Inicio del private void BringMe_PrimaryKey4Query
             /// <summary>
             /// Metodo : HallaPrimaryKeyQuery
             /// Arma la condicion para la llave primaria de la tabla.
             /// </summary>
             /// <param name="SQl">Objeto para hacer queries, del tipo ClasX_DBQuery</param>
             int inL_Index = 0;
             String stL_Campo = "";
             String stL_Fecha = "";
             String stL_Hora = "";
             CLNBTN_Ul ObjL_Utils = null;
             String stL_NombreCampo = "";
             String stL_ContenidoCampo = "";
             Boolean blL_TieneLlavePrimaria = false;
             try
             {
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     { // del  if (_in_Quantity_Fields > 0)
                         //
                         ObjL_Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                         //
                         for (inL_Index = 0; inL_Index <= _in_Quantity_Fields; inL_Index++)
                         { // del for (inL_Index = 0;
                             //
                             stL_NombreCampo = stPr_Campos_Tabla[inL_Index];
                             stL_ContenidoCampo = stPr_Contenido_Campo[inL_Index];
                             // Valida si el campo es AutoIncrementable, para colocarlo en la condicion de la llave.
                             if (blPr_Auto_Incrementable[inL_Index])
                             { // del if (blPr_Auto_Incrementable[inL_Index])
                                 blL_TieneLlavePrimaria = true;
                                 if (stL_ContenidoCampo.Trim().ToUpper() == SEM_DADO)
                                 {
                                     SQl.ToDo_AND(stL_NombreCampo, "0");
                                 }
                                 else
                                 {
                                     SQl.ToDo_AND(stL_NombreCampo, stL_ContenidoCampo);
                                 }
                             } // del if (blPr_Auto_Incrementable[inL_Index])
                             else // del if (blPr_Auto_Incrementable[inL_Index])
                             { // inicio del else if (blPr_Auto_Incrementable[inL_Index])
                                 if ((stL_NombreCampo.Trim().ToUpper() != SEM_DADO) && (stL_ContenidoCampo.Trim().ToUpper() != SEM_DADO))
                                 { // del if ( ( (stL_NombreCampo.Trim().ToUpper()
                                     // Valida si el campo es llave en la tabla 
                                     if (blPr_Llaves_Tabla[inL_Index])
                                     { // Inicio del if ( blPr_Llaves_Tabla[inL_Index]) 
                                         blL_TieneLlavePrimaria = true;
                                         switch (stPr_Tipo_Dato_Campo[inL_Index])
                                         { // Inicio  switch (stPr_Tipo_Dato_Campo[inL_Index])
                                             case CLASX_TIPO_DATO_STRING:
                                                 // Campo tipo texto
                                                 SQl.ToDo_AND(stL_NombreCampo, "'" + stL_ContenidoCampo + "'");
                                                 //
                                                 break;
                                             case CLASX_TIPO_DATO_NUMERIC:
                                                 // Campo Numerico
                                                 SQl.ToDo_AND(stL_NombreCampo, stL_ContenidoCampo);
                                                 break;
                                             case CLASX_TIPO_DATO_DOUBLE:
                                                 // Campo con decimales
                                                 SQl.ToDo_AND(stL_NombreCampo, stL_ContenidoCampo);
                                                 break;
                                             case CLASX_TIPO_DATO_DATE:
                                                 // Campo Fecha 
                                                 stL_Campo = "";
                                                 stL_Campo = stL_ContenidoCampo;
                                                 //
                                                 stL_Fecha = "";
                                                 stL_Fecha = stL_Campo.Substring(0, 9);
                                                 SQl.ToDo_AND(stL_NombreCampo, "'" + ObjL_Utils.ConvertDate2Query(inPr_TipoServidor, stL_Fecha) + "'");
                                                 //
                                                 stL_Hora = "";
                                                 //
                                                 if (stL_Campo.Length > 10)
                                                 {
                                                     stL_Hora = stL_Campo.Substring(10, 8);
                                                     SQl.ToDo_AND(stL_NombreCampo, "'" + ObjL_Utils.ConvertDate2Query(inPr_TipoServidor, stL_Fecha) + " " + stL_Hora + "'");
                                                 }
                                                 break;
                                             case CLASX_TIPO_DATO_BOOLEAN:
                                                 // Tipo Boolean
                                                 SQl.ToDo_AND(stL_NombreCampo, stL_ContenidoCampo);
                                                 break;
                                             default:
                                                 break;
                                         } // Fin  switch (stPr_Tipo_Dato_Campo[inL_Index])
                                     } // Fin de if ( blPr_Llaves_Tabla[inL_Index]) 
                                 } // del if ( ( (stL_NombreCampo.Trim().ToUpper()
                             } // Fin del else if (blPr_Auto_Incrementable[inL_Index])
                         } // fin del for (inL_Index = 0;
                         ///////////////////////////////////////////////////////////////////////
                         // Valida si pudo definir campos para la llave primaria:
                         ///////////////////////////////////////////////////////////////////////
                         if (!blL_TieneLlavePrimaria)
                         { // del if (!blL_TieneLlavePrimaria)
                             // Define una condicion para que no exista el registro
                             // para no dañar los datos de TODA la tabla.
                             SQl.ToDo_AND(" 7 = 5 ", "");
                         } // fin del if (!blL_TieneLlavePrimaria)
                         ///////////////////////////////////////////////////////////////////////
                         //
                     } // Fin del  if (_in_Quantity_Fields > 0)
                     //
                 }
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_PrimaryKey4Query. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_PrimaryKey4Query. Exception", "", ex.Message.ToString());
             }
         }



         [HandleProcessCorruptedStateExceptions]
         private void BringMe_Fields2Query(ref CLNBTN_Qy SQl, Boolean RecordExists)
         { // Inicio del private void Arma_Campos_X_Query
             /// <summary>
             /// Metodo : Arma_Campos_X_Query
             /// Asigna los campos al objeto, SQl, para ir armando la frase SQL
             /// </summary>
             /// <param name="SQl">Objeto para hacer los queries del tipo ClasX_DBQuery</param>
             /// <param name="bl_ExisteReg">true=Indica que existe el registro</param>
             int inL_Index = 0;
             Boolean blL_ProcesaCampo = false;
             String stL_Campo = "";
             String stL_Fecha = "";
             String stL_Hora = "";
             CLNBTN_Ul ObjL_Utils = null;
             //
             String stL_NombreCampo = "";
             String stL_ContenidoCampo = "";
             //
             int inL_Pos = 0;
             int inL_Hora = 0;
             String stL_HoraDeLaFecha = "";
             //
             String stL_ParteDelaHora = "";
             int inL_number1 = 0;
             int inL_ContaDosPuntos = 0;
             String stL_Minutos = "";
             String stL_Segundos = "";
             try
             {
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     { // del  if (_in_Quantity_Fields > 0)
                         //
                         ObjL_Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                         //
                         for (inL_Index = 0; inL_Index <= _in_Quantity_Fields; inL_Index++)
                         { // del for (inL_Index = 0;
                             blL_ProcesaCampo = true;
                             //
                             stL_NombreCampo = stPr_Campos_Tabla[inL_Index];
                             stL_ContenidoCampo = stPr_Contenido_Campo[inL_Index];
                             //
                             // Si esta actualizando y es parte de llave, no lo coloca como campo
                             // a ser actualizado
                             if (RecordExists)
                             { // del  if (bl_ExisteReg)
                                 // Si el campo es parte de la llave no lo actualiza.
                                 if (blPr_Llaves_Tabla[inL_Index])
                                 {
                                     blL_ProcesaCampo = false;
                                 }
                                 else
                                 {
                                     // Si el campo esta marcado para que grabe en la modificacion, lo procesa
                                     blL_ProcesaCampo = blPr_Actualiza_En_Modificacion[inL_Index];
                                     // Si debe procesar el campo, valida si es un campo AutoIncrementable
                                     // para no incluir en la instruccion update
                                     if (blL_ProcesaCampo)
                                     {
                                         if (blPr_Auto_Incrementable[inL_Index])
                                         {
                                             // Si es campo AutoIncrementable, no lo debe procesar.
                                             blL_ProcesaCampo = false;
                                         }
                                     }
                                 }
                             } // del  if (bl_ExisteReg)
                             else // del del  if (bl_ExisteReg)
                             { // del else del  if (bl_ExisteReg)
                                 // Si va a hacer un insert, y el campo es autoincrementable, no lo debe incluir en la instruccion insert
                                 if (blPr_Auto_Incrementable[inL_Index])
                                 {
                                     // Si es campo AutoIncrementable, no lo debe procesar.
                                     blL_ProcesaCampo = false;
                                 }
                                 // Valida si el campo se actualiza solo en Modificacion.
                                 if (blPr_GrabaSolo_En_Modificacion[inL_Index])
                                 {
                                     // Si es campo Solo se Actualiza en Modificacion, no lo debe procesar.
                                     blL_ProcesaCampo = false;
                                 }
                             } // el else del  if (bl_ExisteReg)
                             /////////////////////////////////////////////////////////////////////
                             // Si el campo esta vacio no lo procesa.
                             if (blL_ProcesaCampo)
                             {
                                 if (stPr_Contenido_Campo[inL_Index].Length == 0)
                                 {
                                     blL_ProcesaCampo = false;
                                 }
                                 else
                                 {
                                     if ((stL_NombreCampo.Trim().ToUpper() == SEM_DADO) || (stL_ContenidoCampo.Trim().ToUpper() == SEM_DADO))
                                     {
                                         blL_ProcesaCampo = false;
                                     }
                                 }
                             }
                             // Procesa el campo
                             if (blL_ProcesaCampo)
                             { // Inicio del if (blL_ProcesaCampo)
                                 // Asigna los campos y su contenido.
                                 switch (stPr_Tipo_Dato_Campo[inL_Index])
                                 { // Inicio  switch (stPr_Tipo_Dato_Campo[inL_Index])
                                     case CLASX_TIPO_DATO_STRING:
                                         // Campo tipo texto
                                         SQl.ToDo_SET(stL_NombreCampo, stL_ContenidoCampo);
                                         //
                                         break;
                                     case CLASX_TIPO_DATO_NUMERIC:
                                         // Campo Numerico
                                         SQl.ToDo_SET(stL_NombreCampo, stL_ContenidoCampo);
                                         break;
                                     case CLASX_TIPO_DATO_DOUBLE:
                                         // Campo con Decimales
                                         SQl.ToDo_SET(stL_NombreCampo, stL_ContenidoCampo);
                                         break;
                                     case CLASX_TIPO_DATO_DATE:
                                         // Campo Fecha 
                                         stL_Campo = "";
                                         stL_Campo = stL_ContenidoCampo;
                                         // Valida si debe asignarle la fecha del sistema
                                         if (stL_Campo == SYSTEM_DATETIME)
                                         {
                                             SQl.ToDo_SET(stL_NombreCampo, ObjL_Utils.BringMeServerDate(ObjPr_InfoBD_Tabla, true, true));
                                         }
                                         else // del if ( stL_Campo == ClasX_Constans.SYSTEM_DATETIME )
                                         { // inicio del ELSE DEL if ( stL_Campo == ClasX_Constans.SYSTEM_DATETIME )
                                             // Toma la parte de la fecha
                                             stL_Fecha = "";
                                             stL_Fecha = stL_Campo.Substring(0, 10);
                                             //
                                             inL_ContaDosPuntos = 0;
                                             stL_Hora = "";
                                             stL_Minutos = "";
                                             stL_Segundos = "";
                                             stL_HoraDeLaFecha = "";
                                             //
                                             if (stL_Campo.Length == 10)
                                             {
                                                 SQl.ToDo_SET(stL_NombreCampo, ObjL_Utils.ConvertDate2Query(inPr_TipoServidor, stL_Fecha));
                                             }
                                             else // del if (stL_Campo.Length == 10)
                                             { // Incio del ELSE if (stL_Campo.Length == 10)
                                                 ///////////////////////////////////////////////////
                                                 // ASQC Marzo 14-18 2013.
                                                 ///////////////////////////////////////////////////
                                                 // Tratamiento de la hora.
                                                 /////////////////////////////////////////////////////////////////
                                                 // Recorre el string que tiene la hora y la separa en Horas, Minutos y Segundos
                                                 // Y hace la validacion para colocar la hora en formato Militar.
                                                 // Halla la hora
                                                 // Recorre el string de la hora y separa horas, minutos y segundos.
                                                 stL_ParteDelaHora = stL_Campo.Substring(11, stL_Campo.Length - 12);
                                                 for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                                                 { // Inicio del for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                                                     // Toma cada caracter y valida si es numero, para asi separarlos
                                                     bool canConvert = int.TryParse(stL_ParteDelaHora.Substring(inL_Index1, 1), out inL_number1);
                                                     if (canConvert == true)
                                                     { // Inicio del if (canConvert == true
                                                         switch (inL_ContaDosPuntos)
                                                         {
                                                             case 0:
                                                                 // La hora
                                                                 stL_Hora += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                                                 break;
                                                             case 1:
                                                                 // los minutos
                                                                 stL_Minutos += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                                                 break;
                                                             case 2:
                                                                 // los segundos
                                                                 stL_Segundos += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                                                 break;
                                                         }
                                                     } // fin del if (canConvert == true
                                                     else // del if (canConvert == true
                                                     { // del else del if (canConvert == true
                                                         // incrementa el contador de los caracteres que no son numericos, como los dos puntos.
                                                         inL_ContaDosPuntos = inL_ContaDosPuntos + 1;
                                                     } // fin del else del if (canConvert == true
                                                 } // Fin de for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                                                 ///////////////////////////////////////////
                                                 inL_Pos = stL_Campo.IndexOf("p.m.");
                                                 if (inL_Pos == -1)
                                                 {
                                                     stL_HoraDeLaFecha = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                                                 }
                                                 else
                                                 {
                                                     inL_Hora = 0;
                                                     if (stL_Hora.Length > 0)
                                                     {
                                                         inL_Hora = Convert.ToInt32(stL_Hora);
                                                     }
                                                     // si la hora es menor a 12
                                                     // Indica que es por la tarde
                                                     // a 12 le suma la hora que se tiene
                                                     // por ejemplo:
                                                     // hora 03. deberia ser 12 + 3 = 15 las 15 Horas
                                                     if (inL_Hora < 12)
                                                     {
                                                         inL_Hora = 12 + inL_Hora;
                                                         stL_Hora = Convert.ToString(inL_Hora);
                                                         stL_HoraDeLaFecha = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                                                     }
                                                     else
                                                     {
                                                         stL_HoraDeLaFecha = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                                                     }
                                                 }
                                                 ////////////////////////////////////////////////////
                                                 // Fin Marzo 14-18 2013.
                                                 ////////////////////////////////////////////////////
                                                 SQl.ToDo_SET(stL_NombreCampo, ObjL_Utils.ConvertDate2Query(inPr_TipoServidor, stL_Fecha) + " " + stL_HoraDeLaFecha);
                                             } // Fin del ELSE del if (stL_Campo.Length == 10)
                                         } // Fin del ELSE del if ( stL_Campo == ClasX_Constans.SYSTEM_DATETIME )
                                         break;
                                     case CLASX_TIPO_DATO_BOOLEAN:
                                         // Tipo Boolean
                                         SQl.ToDo_SET(stL_NombreCampo, stL_ContenidoCampo);
                                         break;
                                     default:
                                         break;
                                 } // Fin  switch (stPr_Tipo_Dato_Campo[inL_Index])
                             } // Fin de if (blL_ProcesaCampo)
                         } // fin del for (inL_Index = 0;
                     } // Fin del  if (_in_Quantity_Fields > 0)
                     //
                 }
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_Fields2Query. System.AccessViolationException", "", ex_0.Message.ToString() + " Field " + stL_NombreCampo + " FieldContents " + stL_ContenidoCampo);
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_Fields2Query. Exception", "", ex.Message.ToString() + " Field " + stL_NombreCampo + " FieldContents " + stL_ContenidoCampo);
             }
         }



         [HandleProcessCorruptedStateExceptions]
         public Boolean Let_Datum_4_Field(String Table_Field, String FieldContents)
         { // Inicio del public Boolean AsignaDato_X_Campo
             /// <summary>
             /// Metodo : AsignaDato_X_Campo
             /// permite asignar el contenido a un campo en la clase.
             /// Devuelve True, si pudo asignar el contenido el campo
             /// </summary>
             /// <param name="st_Campo">Nombre del Campo</param>
             /// <param name="st_ContenidoCampo">Contenido del Campo</param>
             /// <returns></returns>
             int inL_Index = 0;
             Boolean blL_Existe = false;
             try
             {
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     {
                         for (inL_Index = 0; inL_Index <= _in_Quantity_Fields; inL_Index++)
                         {
                             if (stPr_Campos_Tabla[inL_Index].Trim().ToUpper() == Table_Field.Trim().ToUpper())
                             {
                                 stPr_Contenido_Campo[inL_Index] = FieldContents;
                                 blL_Existe = true;
                                 break;
                             }
                         }
                     }
                 }
                 return blL_Existe;
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Datum_4_Field. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Datum_4_Field. Exception", "", ex.Message.ToString());
             }
             return blL_Existe;
         }



         [HandleProcessCorruptedStateExceptions]
         public Boolean BringMe_FieldContents(String Table_Field, ref String FieldContents)
         { // Inicio del public Boolean DevuelveDato_X_Campo
             /// <summary>
             /// Metodo : DevuelveDato_X_Campo
             /// Devuelve el contenido de un campo dentro de la clase
             /// Devuelve true, si encontro el campo en la clase.
             /// </summary>
             /// <param name="st_Campo">Nombre del Campo</param>
             /// <param name="st_ContenidoCampo">Devuelve el contenido del campo</param>
             /// <returns></returns>
             int inL_Index = 0;
             Boolean blL_Existe = false;
             try
             {
                 //
                 FieldContents = "";
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     {
                         for (inL_Index = 0; inL_Index <= _in_Quantity_Fields; inL_Index++)
                         {
                             if (stPr_Campos_Tabla[inL_Index].Trim().ToUpper() == Table_Field.Trim().ToUpper())
                             {
                                 // Si el contenido del arreglo es diferente del indicativo que no tiene dato.
                                 if (stPr_Contenido_Campo[inL_Index].Trim().ToUpper() != SEM_DADO)
                                 {
                                     FieldContents = stPr_Contenido_Campo[inL_Index];
                                     blL_Existe = true;
                                     break;
                                 }
                             }
                         }
                     }
                 }
                 return blL_Existe;
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_FieldContents. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_FieldContents. Exception", "", ex.Message.ToString());
             }
             return blL_Existe;
         }


         [HandleProcessCorruptedStateExceptions]
         public Boolean Is_An_Existing_Record(String SqlStmt = "", Boolean LoadInfoFromTable = false, Boolean Is_A_SP_Stmt = false)
         { // Inicio del public Boolean Existe_Registro
             /// <summary>
             /// Metodo : Existe_Registro
             /// Encargado de validar si existe o no el registro, dependiendo
             /// de los datos que se tengan cargados en la clase.
             /// </summary>
             /// <param name="st_Sql">Instruccion SQL Adicional, por ejemplo si se valida la existencia del registro con un SP, se envia la instruccion del SP</param>
             /// <param name="bl_LeeTabla">true=Indica que carga los datos de la tabla en la estructura de la clase</param>
             /// <returns></returns>
             int inL_IndexCampoClase = 0;
             Boolean blL_Existe = false;
             String stL_NombreCampo = "";
             String stL_ContenidoCampo = "";
             // Define DataTable, para los Datos del Query
             DataTable DatTable = null;
             double dlL_Valor = 0;
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
                     if (_in_Quantity_Fields > 0)
                     { // Inicio del if (_in_Quantity_Fields > 0)
                         //ObjL_Utils = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                         if (SqlStmt.Length == 0)
                         {
                             ObjPr_Query.ToDo_SELECT("*");
                             ObjPr_Query.ToDo_FROM(_st_TableName);
                             ObjPr_Query.ToDo_WHERE("0 = 0");
                             // Halla la llave primaria de la tabla
                             this.BringMe_PrimaryKey4Query(ref ObjPr_Query);
                             // MessageBox.Show("SQL " +  ObjPr_Query.getSqlCommand());
                             //
                             ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable);
                             ObjPr_Query.ToDo_CLOSE();
                         }
                         else
                         {
                             if (Is_A_SP_Stmt)
                             {
                                 // Indica que es un SP el que VA a ejecutar
                                 ObjPr_Query.ToDo_SP_SELECT(SqlStmt);
                                 // Ejecuta el SP 
                                 ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable, SqlStmt);
                                 ObjPr_Query.ToDo_CLOSE();
                             }
                             else
                             {
                                 // Ejecuta la instruccion SQL que viene
                                 ObjPr_Query.ToDo_EXECUTE_SQL(ref DatTable, SqlStmt);
                                 ObjPr_Query.ToDo_CLOSE();
                             }
                         }
                         //
                         if (DatTable != null)
                         { // del if ( ObjPr_Query.Rs.State != 0 ) 
                             for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                             { // inicio del for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                 // Toma la informacion de la fila
                                 DataRow Info_Fila = DatTable.Rows[inL_Row];
                                 blL_Existe = true;
                                 // Si debe leer la tabla, carga los datos
                                 if (LoadInfoFromTable)
                                 { // del if ( bl_LeeTabla ) 
                                     for (int inL_Index = 0; inL_Index < DatTable.Columns.Count; inL_Index++)
                                     { // Inicio del for (inL_Index = 0
                                         //
                                         stL_NombreCampo = DatTable.Columns[inL_Index].Caption;
                                         stL_ContenidoCampo = "0";
                                         //if (!DBNull.Value.Equals(Info_Fila[inL_Index].ToString()))
                                         //{ // del if (!DBNull.Value.Equals(Info_Fila[inL_Index].ToString()))
                                             if (Info_Fila[inL_Index].ToString().Length > 0)
                                             { // del if (Info_Fila[inL_Index].ToString().Length > 0)
                                                 // Si el campo es numerico lo lee tal y como viene
                                                 if (this.Is_An_Existing_Field(stL_NombreCampo, ref inL_IndexCampoClase))
                                                 { // del if (ExisteCampo(stL_NombreCampo, ref inL_IndexCampoClase))
                                                     if (stPr_Tipo_Dato_Campo[inL_IndexCampoClase] == CLASX_TIPO_DATO_DOUBLE)
                                                     {
                                                         // Valida si es numerico
                                                         stL_ContenidoCampo = "";
                                                         stL_ContenidoCampo = Info_Fila[inL_Index].ToString();
                                                         Double dlL_NumberAux = 0;
                                                         bool canConvert = Double.TryParse(stL_ContenidoCampo, out dlL_NumberAux);
                                                         if (!canConvert)
                                                         {
                                                             stL_ContenidoCampo = "0";
                                                         }
                                                         //-->dlL_Valor = Convert.ToDouble(Info_Fila[inL_Index]);
                                                         dlL_Valor = Convert.ToDouble(stL_ContenidoCampo);
                                                         stL_ContenidoCampo = Convert.ToString(dlL_Valor);
                                                     }
                                                     else
                                                     {
                                                         stL_ContenidoCampo = Info_Fila[inL_Index].ToString();
                                                     }
                                                     if (Let_Datum_4_Field(stL_NombreCampo, stL_ContenidoCampo))
                                                     {
                                                         // Nada 
                                                     }
                                                 } // del if (ExisteCampo(stL_NombreCampo, ref inL_IndexCampoClase))
                                             } // del if (Info_Fila[inL_Index].ToString().Length > 0)
                                         //} // del if (!DBNull.Value.Equals(Info_Fila[inL_Index].ToString()))
                                         //
                                     } // Fin del for (inL_Index = 0
                                 } // del if ( bl_LeeTabla ) 
                             } // fin de for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                         } // del if (DatTable != null )
                         //_->>ObjPr_Query.ToDo_CLOSE();
                     } // Fin del if (_in_Quantity_Fields > 0)
                 }
                 return blL_Existe;
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 if (stL_NombreCampo.Length > 0)
                 {
                     objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_An_Existing_Record. System.AccessViolationException", "", ex_0.Message.ToString() + " FieldName: " + stL_NombreCampo.Trim() + " FieldContents: " + stL_ContenidoCampo.Trim());
                     ObjPr_Query.ToDo_CLOSE();
                     if (DatTable != null)
                     {
                         DatTable = null;
                     }
                 }
                 else
                 {
                     objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_An_Existing_Record. System.AccessViolationException", "", ex_0.Message.ToString());
                 }
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 if (stL_NombreCampo.Length > 0)
                 {
                     objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_An_Existing_Record. Exception", "", ex.Message.ToString() + " FieldName: " + stL_NombreCampo.Trim() + " FieldContents: " + stL_ContenidoCampo.Trim());
                     ObjPr_Query.ToDo_CLOSE();
                     if (DatTable != null)
                     {
                         DatTable = null;
                     }
                }
                 else
                 {
                     objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_An_Existing_Record. Exception", "", ex.Message.ToString());
                 }
             }
             return blL_Existe;
         }


         [HandleProcessCorruptedStateExceptions]
         public Boolean ToDo_Delete_Record()
         { // Inicio del public Boolean ToDo_Delete_Record
             /// <summary>
             /// Metodo : Delete_Registro
             /// Se encarga de eliminar un registro de la tabla
             /// con base en la informacion de las llaves primarias definidas en la clase
             /// </summary>
             /// <returns>true=Elimino el Registro</returns>
             Boolean blL_Delete = false;
             try
             {
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     { // Inicio del if (_in_Quantity_Fields > 0)
                         //
                         CLNBTN_Qy Obj_Query_Delete = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                         Obj_Query_Delete.setDataBaseInfo(ObjPr_InfoBD_Tabla);
                         //
                         Obj_Query_Delete.ToDo_DELETE(_st_TableName);
                         Obj_Query_Delete.ToDo_WHERE("0 = 0");
                         // Halla la llave primaria de la tabla
                         this.BringMe_PrimaryKey4Query(ref Obj_Query_Delete);
                         //
                         // MessageBox.Show("SQL " +  Obj_Query_Delete.getSqlCommand());
                         Obj_Query_Delete.ToDo_EXECUTE_SQL();
                         // Si ejecute bien la instruccion
                         blL_Delete = Obj_Query_Delete.getSuccessQueryExecution();
                         Obj_Query_Delete.ToDo_CLOSE();
                     } // Fin del if (_in_Quantity_Fields > 0)
                 }
                 return blL_Delete;
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Delete_Record. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Delete_Record. Exception", "", ex.Message.ToString());
             }
             return blL_Delete;
         }


         [HandleProcessCorruptedStateExceptions]
         public Boolean ToDo_Insert_Record()
         { // Inicio del public Boolean ToDo_Insert_Record
             /// <summary>
             /// Metodo : Insert_Registro
             /// Encargado de hacer el insert en la tabla de la clase
             /// </summary>
             /// <returns>true=Si hizo el insert</returns>
             Boolean blL_Insert = false;
             try
             {
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     { // Inicio del if (_in_Quantity_Fields > 0)
                         //
                         CLNBTN_Qy Obj_Query_Insert = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                         Obj_Query_Insert.setDataBaseInfo(ObjPr_InfoBD_Tabla);
                         //
                         Obj_Query_Insert.ToDo_INSERT(_st_TableName);
                         // Arma SETS de los campos
                         this.BringMe_Fields2Query(ref Obj_Query_Insert, false);
                         // MessageBox.Show("SQL " +  Obj_Query_Insert.getSqlCommand());
                         Obj_Query_Insert.ToDo_EXECUTE_SQL();
                         // Si ejecute bien la instruccion
                         blL_Insert = Obj_Query_Insert.getSuccessQueryExecution();
                         Obj_Query_Insert.ToDo_CLOSE();
                     } // Fin del if (_in_Quantity_Fields > 0)
                 }
                 return blL_Insert;
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Insert_Record. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Insert_Record. Exception", "", ex.Message.ToString());
             }
             return blL_Insert;
         }


         [HandleProcessCorruptedStateExceptions]
         public Boolean ToDo_Update_Record()
         { // Inicio del public Boolean ToDo_Update_Record
             /// <summary>
             /// Metodo : Update_Registro
             /// Actualiza la informacion de la tabla en la base de datos.
             /// </summary>
             /// <returns>true=Si actualizo la informacion</returns>
             Boolean blL_Update = false;
             try
             {
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     { // Inicio del if (_in_Quantity_Fields > 0)
                         //
                         CLNBTN_Qy Obj_Query_Update = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                         Obj_Query_Update.setDataBaseInfo(ObjPr_InfoBD_Tabla);
                         //
                         Obj_Query_Update.ToDo_UPDATE(_st_TableName);
                         Obj_Query_Update.ToDo_WHERE("0 = 0");
                         // Halla la llave primaria de la tabla
                         this.BringMe_PrimaryKey4Query(ref Obj_Query_Update);
                         // Arma SETS de los campos
                         this.BringMe_Fields2Query(ref Obj_Query_Update, true);
                         // MessageBox.Show("SQL " +  Obj_Query_Update.getSqlCommand());
                         Obj_Query_Update.ToDo_EXECUTE_SQL();
                         // Si ejecute bien la instruccion
                         blL_Update = Obj_Query_Update.getSuccessQueryExecution();
                         Obj_Query_Update.ToDo_CLOSE();
                     } // Fin del if (_in_Quantity_Fields > 0)
                 }
                 return blL_Update;
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Update_Record. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Update_Record. Exception", "", ex.Message.ToString());
             }
             return blL_Update;
         }


         [HandleProcessCorruptedStateExceptions]
         public Boolean ToDo_Save_Record(String SqlStmt = "", Boolean Is_A_SP_Stmt = false)
         {
             // Encargado de hacer el insert o el update del registro
             /// <summary>
             /// Gtraba un registro. Hace insert o update dependiendo si existe o no en la tabla.
             /// </summary>
             /// <param name="st_Sql">Opcional. Instruccion SQL Para lectura del registro en la tabla</param>
             /// <param name="bl_InstruccionSP">True Si es una instruccion con SP</param>
             /// <returns>TRUE Si graba el registro con exito en la bd</returns>
             // 
             Boolean blL_Grabar = false;
             try
             {
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (_in_Quantity_Fields > 0)
                     { // Inicio del if (_in_Quantity_Fields > 0)
                         //
                         // Valida si existe el registro.
                         if (this.Is_An_Existing_Record(SqlStmt, false, Is_A_SP_Stmt))
                         {
                             // Hace el UPDATE
                             blL_Grabar = this.ToDo_Update_Record();
                         }
                         else
                         {
                             // Hace el INSERT
                             blL_Grabar = this.ToDo_Insert_Record();
                         }
                     } // Fin del if (_in_Quantity_Fields > 0)
                 }
                 return blL_Grabar;
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Save_Record. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ToDo_Save_Record. Exception", "", ex.Message.ToString());
             }
             return blL_Grabar;
         }


         [HandleProcessCorruptedStateExceptions]
         public Boolean Let_Values_2_Interface(Control WinForm, String TableName)
         { // Inicio del  public Boolean Let_Values_2_Interface(
             /// <summary>
             /// Metodo ; Valores_To_Interface
             /// Devuelve los valores a los controles de la forma o ventana
             /// que entra como parametro.
             /// </summary>
             /// <param name="Forma">El objeto de la forma</param>
             /// <param name="TableName">Nombre de la tabla</param>
             /// <returns></returns>

             // URL Referenci : http://www.devtroce.com/2010/09/01/recorrer-los-controles-de-un-formulario-con-c-vb-net/
             // En la propiedad TAG, debe venir :
             // ! Nombre Tabla, Propiedad, Campo
             // Ejemplo: 
             // TxtCodigo.TAG = ! ad_rol, Text, ro_rol
             // TxtDescripcion.TAG = ! ad_rol, Text, ro_descripcion
             //
             //Control ControlForma = null;
             //Control ControlHijo = null;
             //
             Boolean blL_DatosOk = false;
             String stL_NombreControl = "";
             String stL_TagControl = "";
             String stL_TablaControl = "";
             String stL_PropiedadControl = "";
             String stL_CampoControl = "";
             String stL_ContenidoCampo = "";
             Boolean blL_DatosCampoOk = false;
             try
             { // Inicio del Try
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     foreach (Control ControlForma in WinForm.Controls)
                     { // del foreach (Control ControlForma
                         if (ControlForma.HasChildren)
                         { // del if (ControlHijo.HasChildren)
                             foreach (Control ControlHijo in ControlForma.Controls)
                             { // del foreach (Control ControlHijo
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 if (ControlHijo is TextBox)
                                 {
                                     stL_NombreControl = ControlHijo.Name;
                                     stL_TagControl = Convert.ToString(ControlHijo.Tag);
                                 }
                                 else
                                 {
                                     if (ControlHijo is Label)
                                     {
                                         stL_NombreControl = ControlHijo.Name;
                                         stL_TagControl = Convert.ToString(ControlHijo.Tag);
                                     }
                                 }
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Devuelve el contenido del campo
                                         if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                         {
                                             ControlHijo.Text = stL_ContenidoCampo;
                                         }
                                     }
                                 } // del  if ( blL_DatosCampoOk ) 
                             } // del foreach (Control ControlHijo
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los combobox
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (ComboBox ItemCombo in ControlForma.Controls.OfType<ComboBox>())
                             { // inicio del  foreach (  ComboBox ItemCombo 
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemCombo.Name;
                                 stL_TagControl = Convert.ToString(ItemCombo.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Devuelve el contenido del campo
                                         if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                         {
                                             if (stL_ContenidoCampo.Length > 0)
                                             {
                                                 ItemCombo.SelectedValue = stL_ContenidoCampo;
                                             }
                                         }
                                     }
                                 }
                             } // Fin del  foreach (  ComboBox ItemCombo 
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los combobox
                             ///////////////////////////////////////////////////////////////////////////////
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los campos fecha
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (DateTimePicker ItemFecha in ControlForma.Controls.OfType<DateTimePicker>())
                             { // inicio del  foreach (  ComboBox ItemFecha 
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemFecha.Name;
                                 stL_TagControl = Convert.ToString(ItemFecha.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Devuelve el contenido del campo
                                         if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                         {
                                             if (stL_ContenidoCampo.Length > 0)
                                             {
                                                 ItemFecha.Value = Convert.ToDateTime(stL_ContenidoCampo);
                                             }
                                         }
                                     }
                                 }
                             } // Fin del  foreach (  ComboBox ItemFecha 
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los campos fecha
                             ///////////////////////////////////////////////////////////////////////////////
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los campos NumericUpDown
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (NumericUpDown ItemNumeric in ControlForma.Controls.OfType<NumericUpDown>())
                             { // inicio del  foreach (  NumericUpDown ItemNumeric
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemNumeric.Name;
                                 stL_TagControl = Convert.ToString(ItemNumeric.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     { // inicio del if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                         // Devuelve el contenido del campo
                                         if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                         { // Inicio del if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                             ItemNumeric.Value = 0;
                                             if (stL_ContenidoCampo.Length > 0)
                                             { // Inicio del if (stL_ContenidoCampo.Length > 0)
                                                 // Valida si es decimal
                                                 decimal dlL_number1 = 0;
                                                 bool canConvert = decimal.TryParse(stL_ContenidoCampo, out dlL_number1);
                                                 if (canConvert)
                                                 { // inicio del if (canConvert)
                                                     ItemNumeric.Value = dlL_number1;
                                                 } // Fin del if (canConvert)
                                             } // Fin del if (stL_ContenidoCampo.Length > 0)
                                         } // Fin del if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                     } // Fin del if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                 } // Inicio del if (blL_DatosCampoOk)
                             } // Fin del  foreach (  NumericUpDown ItemNumeric
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los campos NumericUpDown
                             ///////////////////////////////////////////////////////////////////////////////
                         } // del if (ControlHijo.HasChildren)
                         else // del if (ControlForma.HasChildren)
                         { // del else del if (ControlForma.HasChildren)
                             stL_NombreControl = "";
                             stL_TagControl = "";
                             if (ControlForma is TextBox)
                             {
                                 stL_NombreControl = ControlForma.Name;
                                 stL_TagControl = Convert.ToString(ControlForma.Tag);
                             }
                             else
                             {
                                 if (ControlForma is Label)
                                 {
                                     stL_NombreControl = ControlForma.Name;
                                     stL_TagControl = Convert.ToString(ControlForma.Tag);
                                 }
                             }
                             // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                             this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                             if (blL_DatosCampoOk)
                             { // del  if ( blL_DatosCampoOk ) 
                                 if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                 {
                                     // Devuelve el contenido del campo
                                     if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                     {
                                         ControlForma.Text = stL_ContenidoCampo;
                                     }
                                 }
                             } // del  if ( blL_DatosCampoOk ) 
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los combobox
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (ComboBox ItemCombo in ControlForma.Controls.OfType<ComboBox>())
                             { // inicio del  foreach (  ComboBox ItemCombo 
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemCombo.Name;
                                 stL_TagControl = Convert.ToString(ItemCombo.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Devuelve el contenido del campo
                                         if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                         {
                                             if (stL_ContenidoCampo.Length > 0)
                                             {
                                                 ItemCombo.SelectedValue = stL_ContenidoCampo;
                                             }
                                         }
                                     }
                                 }
                             } // Fin  del  foreach (  ComboBox ItemCombo 
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los combobox
                             ///////////////////////////////////////////////////////////////////////////////
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los campos Fecha
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (DateTimePicker ItemFecha in ControlForma.Controls.OfType<DateTimePicker>())
                             { // inicio del  foreach ( DateTimePicker ItemFecha
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemFecha.Name;
                                 stL_TagControl = Convert.ToString(ItemFecha.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Devuelve el contenido del campo
                                         if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                         {
                                             if (stL_ContenidoCampo.Length > 0)
                                             {
                                                 ItemFecha.Value = Convert.ToDateTime(stL_ContenidoCampo);
                                             }
                                         }
                                     }
                                 }
                             } // Fin  del  foreach (  DateTimePicker ItemFecha
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los campos Fecha
                             ///////////////////////////////////////////////////////////////////////////////
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los campos NumericUpDown
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (NumericUpDown ItemNumeric in ControlForma.Controls.OfType<NumericUpDown>())
                             { // inicio del  foreach (  NumericUpDown ItemNumeric
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemNumeric.Name;
                                 stL_TagControl = Convert.ToString(ItemNumeric.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     { // inicio del if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                         // Devuelve el contenido del campo
                                         if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                         { // Inicio del if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                             ItemNumeric.Value = 0;
                                             if (stL_ContenidoCampo.Length > 0)
                                             { // Inicio del if (stL_ContenidoCampo.Length > 0)
                                                 // Valida si es decimal
                                                 decimal dlL_number1 = 0;
                                                 bool canConvert = decimal.TryParse(stL_ContenidoCampo, out dlL_number1);
                                                 if (canConvert)
                                                 { // inicio del if (canConvert)
                                                     ItemNumeric.Value = dlL_number1;
                                                 } // Fin del if (canConvert)
                                             } // Fin del if (stL_ContenidoCampo.Length > 0)
                                         } // Fin del if (this.BringMe_FieldContents(stL_CampoControl, ref stL_ContenidoCampo))
                                     } // Fin del if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                 } // Inicio del if (blL_DatosCampoOk)
                             } // Fin del  foreach (  NumericUpDown ItemNumeric
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los campos NumericUpDown
                             ///////////////////////////////////////////////////////////////////////////////
                         } // del else del if (ControlForma.HasChildren)
                     } // de foreach (Control ControlForma
                 }
                 return blL_DatosOk;
             } // Fin del Try
             catch (System.AccessViolationException ex_0)
             {
                 blL_DatosOk = false;
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Values_2_Interface. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 blL_DatosOk = false;
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Values_2_Interface. Exception", "", ex.Message.ToString());
             }
             return blL_DatosOk;
         }


         [HandleProcessCorruptedStateExceptions]
         public void BringMe_ControlParamInfo(String Ctrl_Name, String Ctrl_TagInfo, ref String Ctrl_TableName, ref String Ctrl_ProperName, ref String Ctrl_FieldName, ref Boolean Is_Ctrl_Datum_OK)
         {
             // Funcion que se encarga de pasar los valores del registro a los controles de la interface
             // que tienen asociado, en la propiedad TAG, el campo del objeto.
             //
             int inL_Pos = 0;
             String stL_Aux = "";
             //
             // En la propiedad TAG, debe venir :
             // ! Nombre Tabla, Propiedad, Campo
             // Ejemplo: 
             // TxtCodigo.TAG = ! ad_rol, Text, ro_rol
             // TxtDescripcion.TAG = ! ad_rol, Text, ro_descripcion
             // Deviuelve por separado los datos de :
             // Tabla     = ad_rol
             // Propiedad = Text
             // Campo     = ro_descripcion
             //
             try
             { // Inicio del Try
                 //
                 Ctrl_TableName = "";
                 Ctrl_ProperName = "";
                 Ctrl_FieldName = "";
                 Is_Ctrl_Datum_OK = false;
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if ((Ctrl_Name.Length > 0) && (Ctrl_TagInfo.Length > 0))
                     { // del if (( Ctrl_Name.Length > 0 )  && ( Ctrl_TagInfo.Length > 0 ) )
                         stL_Aux = Ctrl_TagInfo;
                         inL_Pos = stL_Aux.IndexOf("!");
                         if (inL_Pos >= 0)
                         {
                             inL_Pos = stL_Aux.IndexOf(",");
                             if (inL_Pos >= 0)
                             { // del if ( inL_Pos >= 0 ) 
                                 // Toma el Nombre de la tabla 
                                 // TxtCodigo.TAG      = ! ad_rol, Text, ro_rol
                                 // Nombre de la tabla = ad_rol
                                 Ctrl_TableName = stL_Aux.Substring(1, inL_Pos - 1);
                                 // Toma del String la parte de :  Text, ro_rol
                                 stL_Aux = stL_Aux.Substring(inL_Pos + 1, stL_Aux.Length - inL_Pos - 1);
                                 inL_Pos = stL_Aux.IndexOf(",");
                                 if (inL_Pos >= 0)
                                 { // del if (inL_Pos >= 0)
                                     // Toma la propiedad del control
                                     // TxtCodigo.TAG = ! ad_rol, Text, ro_rol
                                     // Propiedad     = Text 
                                     Ctrl_ProperName = stL_Aux.Substring(0, inL_Pos - 1);
                                     // Toma el nombre del campo de la tabla 
                                     // TxtCodigo.TAG = ! ad_rol, Text, ro_rol
                                     // Campo         = ro_rol 
                                     Ctrl_FieldName = stL_Aux.Substring(inL_Pos + 1, stL_Aux.Length - inL_Pos - 1);
                                     //
                                     Ctrl_TableName = Ctrl_TableName.Trim();
                                     Ctrl_ProperName = Ctrl_ProperName.Trim();
                                     Ctrl_FieldName = Ctrl_FieldName.Trim();
                                 } // del if (inL_Pos >= 0)
                             } // del if ( inL_Pos >= 0 ) 
                         } // del if ( inL_Pos >= 0 )
                     } // del if (( Ctrl_Name.Length > 0 )  && ( Ctrl_TagInfo.Length > 0 ) )
                     Is_Ctrl_Datum_OK = ((Ctrl_TableName.Length > 0) && (Ctrl_ProperName.Length > 0) && (Ctrl_FieldName.Length > 0));
                 }
             } // fin del Try
             //
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ControlParamInfo. System.AccessViolationException", "", ex_0.Message.ToString() + " Crtl Name " + Ctrl_Name);
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ControlParamInfo. Exception", "", ex.Message.ToString() + " Crtl Name " + Ctrl_Name);
             }
         }


         [HandleProcessCorruptedStateExceptions]
         public Boolean Let_Interface_2_Values(Control WinForm, String TableName)
         { // Inicio del  public Boolean Interface_To_Valores(
             // Se encarga de pasar el contenido de los campos de la interface a los valores de la clase.
             // URL Referenci : http://www.devtroce.com/2010/09/01/recorrer-los-controles-de-un-formulario-con-c-vb-net/
             // En la propiedad TAG, debe venir :
             // ! Nombre Tabla, Propiedad, Campo
             // Ejemplo: 
             // TxtCodigo.TAG = ! ad_rol, Text, ro_rol
             // TxtDescripcion.TAG = ! ad_rol, Text, ro_descripcion
             //
             //
             Boolean blL_DatosOk = false;
             String stL_NombreControl = "";
             String stL_TagControl = "";
             String stL_TablaControl = "";
             String stL_PropiedadControl = "";
             String stL_CampoControl = "";
             String stL_ContenidoCampo = "";
             Boolean blL_DatosCampoOk = false;
             try
             { // Inicio del Try
                 //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     foreach (Control ControlForma in WinForm.Controls)
                     { // del foreach (Control ControlForma
                         if (ControlForma.HasChildren)
                         { // del if (ControlHijo.HasChildren)
                             foreach (Control ControlHijo in ControlForma.Controls)
                             { // del foreach (Control ControlHijo
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 if (ControlHijo is TextBox)
                                 {
                                     stL_NombreControl = ControlHijo.Name;
                                     stL_TagControl = Convert.ToString(ControlHijo.Tag);
                                 }
                                 else
                                 {
                                     if (ControlHijo is Label)
                                     {
                                         stL_NombreControl = ControlHijo.Name;
                                         stL_TagControl = Convert.ToString(ControlHijo.Tag);
                                     }
                                 }
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Toma el contenido del campo 
                                         stL_ContenidoCampo = Convert.ToString(ControlHijo.Text).Trim();
                                         // Lo asigna a la clase
                                         if (this.Let_Datum_4_Field(stL_CampoControl, stL_ContenidoCampo))
                                         {
                                             // Nada
                                         }
                                     }
                                 } // del  if ( blL_DatosCampoOk ) 
                             } // del foreach (Control ControlHijo
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los combobox
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (ComboBox ItemCombo in ControlForma.Controls.OfType<ComboBox>())
                             { // inicio del  foreach (  ComboBox ItemCombo 
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemCombo.Name;
                                 stL_TagControl = Convert.ToString(ItemCombo.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Toma el contenido del campo 
                                         stL_ContenidoCampo = Convert.ToString(ItemCombo.SelectedValue).Trim();
                                         // Lo asigna a la clase
                                         if (this.Let_Datum_4_Field(stL_CampoControl, stL_ContenidoCampo))
                                         {
                                             // Nada
                                         }
                                     }
                                 } // del  if ( blL_DatosCampoOk )
                             } // Fin  del  foreach (  ComboBox ItemCombo 
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los combobox
                             ///////////////////////////////////////////////////////////////////////////////
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los campos fecha
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (DateTimePicker ItemFecha in ControlForma.Controls.OfType<DateTimePicker>())
                             { // inicio del  foreach (  DateTimePicker ItemFecha
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemFecha.Name;
                                 stL_TagControl = Convert.ToString(ItemFecha.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Toma el contenido del campo 
                                         stL_ContenidoCampo = Convert.ToString(ItemFecha.Value).Trim();
                                         // Lo asigna a la clase
                                         if (this.Let_Datum_4_Field(stL_CampoControl, stL_ContenidoCampo))
                                         {
                                             // Nada
                                         }
                                     }
                                 } // del  if ( blL_DatosCampoOk )
                             } // Fin  del  foreach (  DateTimePicker ItemFecha
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los campos fecha
                             ///////////////////////////////////////////////////////////////////////////////
                             //
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los campos NumericUpDown
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (NumericUpDown ItemNumeric in ControlForma.Controls.OfType<NumericUpDown>())
                             { // inicio del  foreach (  NumericUpDown ItemNumeric
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemNumeric.Name;
                                 stL_TagControl = Convert.ToString(ItemNumeric.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Toma el contenido del campo 
                                         stL_ContenidoCampo = ItemNumeric.Value.ToString();
                                         // Lo asigna a la clase
                                         if (this.Let_Datum_4_Field(stL_CampoControl, stL_ContenidoCampo))
                                         {
                                             // Nada
                                         }
                                     }
                                 } // del  if ( blL_DatosCampoOk )
                             } // Fin  del  foreach (  NumericUpDown ItemNumeric
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los campos NumericUpDown 
                             ///////////////////////////////////////////////////////////////////////////////
                         } // del if (ControlHijo.HasChildren)
                         else // del if (ControlForma.HasChildren)
                         { // del else del if (ControlForma.HasChildren)
                             stL_NombreControl = "";
                             stL_TagControl = "";
                             if (ControlForma is TextBox)
                             {
                                 stL_NombreControl = ControlForma.Name;
                                 stL_TagControl = Convert.ToString(ControlForma.Tag);
                             }
                             else
                             {
                                 if (ControlForma is Label)
                                 {
                                     stL_NombreControl = ControlForma.Name;
                                     stL_TagControl = Convert.ToString(ControlForma.Tag);
                                 }
                             }
                             // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                             this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                             if (blL_DatosCampoOk)
                             { // del  if ( blL_DatosCampoOk ) 
                                 if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                 {
                                     // Toma el contenido del campo 
                                     stL_ContenidoCampo = Convert.ToString(ControlForma.Text).Trim();
                                     // Lo asigna a la clase
                                     if (this.Let_Datum_4_Field(stL_CampoControl, stL_ContenidoCampo))
                                     {
                                         // Nada
                                     }
                                 }
                             } // del  if ( blL_DatosCampoOk ) 
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los combobox
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (ComboBox ItemCombo in ControlForma.Controls.OfType<ComboBox>())
                             { // inicio del  foreach (  ComboBox ItemCombo 
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemCombo.Name;
                                 stL_TagControl = Convert.ToString(ItemCombo.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Toma el contenido del campo 
                                         stL_ContenidoCampo = Convert.ToString(ItemCombo.SelectedValue).Trim();
                                         // Lo asigna a la clase
                                         if (this.Let_Datum_4_Field(stL_CampoControl, stL_ContenidoCampo))
                                         {
                                             // Nada
                                         }
                                     }
                                 } // del  if ( blL_DatosCampoOk )
                             } // Fin  del  foreach (  ComboBox ItemCombo 
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los combobox
                             ///////////////////////////////////////////////////////////////////////////////
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los campos fecha
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (DateTimePicker ItemFecha in ControlForma.Controls.OfType<DateTimePicker>())
                             { // inicio del  foreach (  DateTimePicker ItemFecha
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemFecha.Name;
                                 stL_TagControl = Convert.ToString(ItemFecha.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Toma el contenido del campo 
                                         stL_ContenidoCampo = Convert.ToString(ItemFecha.Value).Trim();
                                         // Lo asigna a la clase
                                         if (this.Let_Datum_4_Field(stL_CampoControl, stL_ContenidoCampo))
                                         {
                                             // Nada
                                         }
                                     }
                                 } // del  if ( blL_DatosCampoOk )
                             } // Fin  del  foreach (  DateTimePicker ItemFecha
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los campo fecha
                             ///////////////////////////////////////////////////////////////////////////////
                             //
                             ///////////////////////////////////////////////////////////////////////////////
                             // Para los campos NumericUpDown
                             ///////////////////////////////////////////////////////////////////////////////
                             foreach (NumericUpDown ItemNumeric in ControlForma.Controls.OfType<NumericUpDown>())
                             { // inicio del  foreach (  NumericUpDown ItemNumeric
                                 stL_NombreControl = "";
                                 stL_TagControl = "";
                                 //
                                 stL_NombreControl = ItemNumeric.Name;
                                 stL_TagControl = Convert.ToString(ItemNumeric.Tag);
                                 // Halla tabla, campo y propiedad de la propiedad TAG del Campo
                                 this.BringMe_ControlParamInfo(stL_NombreControl, stL_TagControl, ref stL_TablaControl, ref stL_PropiedadControl, ref stL_CampoControl, ref blL_DatosCampoOk);
                                 if (blL_DatosCampoOk)
                                 { // del  if ( blL_DatosCampoOk ) 
                                     if (TableName.ToUpper() == stL_TablaControl.ToUpper())
                                     {
                                         // Toma el contenido del campo 
                                         stL_ContenidoCampo = ItemNumeric.Value.ToString();
                                         // Lo asigna a la clase
                                         if (this.Let_Datum_4_Field(stL_CampoControl, stL_ContenidoCampo))
                                         {
                                             // Nada
                                         }
                                     }
                                 } // del  if ( blL_DatosCampoOk )
                             } // Fin  del  foreach (  NumericUpDown ItemNumeric
                             ///////////////////////////////////////////////////////////////////////////////
                             // Fin Para los campo NumericUpDown
                             ///////////////////////////////////////////////////////////////////////////////
                         } // del else del if (ControlForma.HasChildren)
                     } // de foreach (Control ControlForma
                 }
                 return blL_DatosOk;
             } // Fin del Try
             catch (System.AccessViolationException ex_0)
             {
                 blL_DatosOk = false;
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Interface_2_Values. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 blL_DatosOk = false;
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Interface_2_Values. Exception", "", ex.Message.ToString());
             }
             return blL_DatosOk;
         }


         [HandleProcessCorruptedStateExceptions]
         public void Let_Change_DBInfo(CLNBTN_IQy Obj_InfoBD)
         {
             /// <summary>
             /// Metodo : CambiaInfo_BD
             /// Permite cambiar la informacion de la base de datos con la cual
             /// trabaja la clase de acceso
             /// </summary>
             /// <param name="Obj_InfoBD">Objeto con la informacion de la base de datos</param>
             // Nada
             try
             {
                 // Set Info DB
                 this.setDataBaseInfo(Obj_InfoBD);
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Change_DBInfo. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Change_DBInfo. Exception", "", ex.Message.ToString());
             }
         }




    }
}
