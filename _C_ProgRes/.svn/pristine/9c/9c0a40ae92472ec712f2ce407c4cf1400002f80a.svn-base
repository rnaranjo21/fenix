using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Runtime.ExceptionServices;

namespace _C_ProgRes
{
    public class ClasX_DBValidations
    { // Inicio del class ClasX_DBValidations
        //
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        //
        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_QyV ObjPr_Self = null;
        //
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo
       


        /// <summary>
        /// Constructor de la clase ClasX_DBValidations
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre del archivo de log</param>
        /// <param name="Obj_InfoBD">Objeto con la informacion de la base de datos de la tabla a trabajar</param>
        public ClasX_DBValidations(String st_UsuarioApp, String st_ArchivoLog, ClasX_DBInfo Obj_InfoBD)
        { // Inicio del public ClasX_DBValidations(
            ClasX_Utils ObjAux = new ClasX_Utils(st_UsuarioApp, st_ArchivoLog);
            NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
            ObjAux.ConvertirTipoInfoBd(Obj_InfoBD, ref O_Aux);
            ObjPr_Self = new NBToolsNet.CLNBTN_QyV(st_UsuarioApp, st_ArchivoLog, O_Aux, stPr_Info);
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog; 
            //
        } // Fin del public ClasX_DBValidations(

        /// <summary>
        /// Constructor de la clase ClasX_DBValidations
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre del archivo de log</param>
        /// <param name="Obj_InfoBD">Objeto con la informacion de la base de datos de la tabla a trabajar</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_DBValidations(String st_UsuarioApp, String st_ArchivoLog, ClasX_DBInfo Obj_InfoBD, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        { // Inicio del public ClasX_DBValidations(
            ClasX_Utils ObjAux = new ClasX_Utils(st_UsuarioApp, st_ArchivoLog);
            NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
            ObjAux.ConvertirTipoInfoBd(Obj_InfoBD, ref O_Aux);
            ObjPr_Self = new NBToolsNet.CLNBTN_QyV(st_UsuarioApp, st_ArchivoLog, O_Aux, bl_SalidaConsola,  bl_SalidaLog,  bl_SalidaDialogo , stPr_Info);
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog; 
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo; 
        } // Fin del public ClasX_DBValidations(


        //
        /// <summary>
        /// Propiedad getUserApp
        /// devuelve el codigo del usuario de la aplicacion
        /// </summary>
        /// <returns>stPr_UsuarioAPP</returns>
        public string getUserApp()
        {
            stPr_UsuarioAPP = ObjPr_Self.getUser();
            return stPr_UsuarioAPP;
        }
        /// <summary>
        /// Propiedad : getArchivoLog
        /// Devuelve el path del archivo de log,
        /// </summary>
        /// <returns>stPr_ArchivoLog</returns>
        public string getArchivoLog()
        {
            stPr_ArchivoLog = ObjPr_Self.getFileLog();
            return stPr_ArchivoLog;
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


        ////////////////////////////////////////
        /// <summary>
        /// Metodo : setUserAPP
        /// Permite definir el codigo del usuario de la aplicacion
        /// </summary>
        /// <param name="st_User">codigo del usuario de la aplicacion</param>
        public void setUserAPP(string st_User)
        {
            stPr_UsuarioAPP = st_User;
            ObjPr_Self.setUser(stPr_UsuarioAPP);
        }
        /// <summary>
        /// Metodo : setArchivoLog
        /// Permite definir el path y nombre del archivo de log.
        /// </summary>
        /// <param name="st_ArchivoLog">path y nombre del archivo de log</param>
        public void setArchivoLog(string st_ArchivoLog)
        {
            stPr_ArchivoLog = st_ArchivoLog;
            ObjPr_Self.setFileLog(stPr_ArchivoLog);
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


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Propiedad DataBase_Exists
        /// Valida si la base de datos existe.
        /// </summary>
        /// <param name="st_NombreBd">Nombre de la base de datos a Validar</param>
        /// <returns>true=Si la base de datos existe,</returns>
        public Boolean DataBase_Exists(String st_NombreBd)
        { // Inicio del  public Boolean DataBase_Exists
            // Valida si la base de datos existe.
            Boolean blL_ExisteBd = false;
            //
            try
            {
                blL_ExisteBd = ObjPr_Self.Is_An_Existing_DataBase(st_NombreBd);
                return blL_ExisteBd;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "DataBase_Exists. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "DataBase_Exists", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_ExisteBd;
        } // Fin del  public Boolean DataBase_Exists



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Propiedad : Object_Exists 
        /// Valida si un objecto existe en la base de datos.
        /// </summary>
        /// <param name="st_NombreObjeto">Nombre del Objeto a Validar</param>
        /// <returns>true=si existe el objeto</returns>
        public Boolean Object_Exists(String st_NombreObjeto)
        { // Inicio del public Boolean Object_Exists(
            // Valida si un objeto existe en una base de datos.
            Boolean blL_Existe = false;
            //
            try
            {
                blL_Existe = ObjPr_Self.Is_A_Valid_DBObject(st_NombreObjeto);
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Object_Exists. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Object_Exists", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Existe;
        } // Fin del public Boolean Object_Exists(


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Propiedad : Field_Table_Exists
        /// Valida si un campo para una tabla existe.
        /// </summary>
        /// <param name="st_NombreTabla">Nombre tabla</param>
        /// <param name="st_NombreCampo">Nombre Campo</param>
        /// <returns>true=si el campo existe en la tabla</returns>
        public Boolean Field_Table_Exists(String st_NombreTabla, String st_NombreCampo)
        { // Inicio del public Boolean Field_Table_Exists(
            // Valida si campo existe en una tabla.
            Boolean blL_Existe = false;
            //
            try
            {
                blL_Existe = ObjPr_Self.Is_A_Valid_DBTable_Field(st_NombreTabla, st_NombreCampo);
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Field_Table_Exists. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Field_Table_Exists", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Existe;
        } // Fin del public Boolean Field_Table_Exists


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Propiedad : Index_Table_Exists
        /// Valida si un indice existe para una tabla.
        /// </summary>
        /// <param name="st_NombreTabla">Nombre de la tabla</param>
        /// <param name="st_NombreIndice">Nombre del Indice</param>
        /// <returns>true=Si el indice existe en la tabla</returns>
        public Boolean Index_Table_Exists(String st_NombreTabla, String st_NombreIndice)
        { // Inicio del public Boolean Index_Table_Exists(
            // Valida si un indice existe en una tabla.
            Boolean blL_Existe = false;
            //
            try
            {
                blL_Existe = ObjPr_Self.Is_A_Valid_DBTable_Index(st_NombreTabla, st_NombreIndice);
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Index_Table_Exists. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Index_Table_Exists", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Existe;
        } // Fin del public Boolean Index_Table_Exists(
        //////////////////////////////////////////////


        [HandleProcessCorruptedStateExceptions]
        public int Lengh_Field(String st_NombreTabla, String st_NombreCampo)
        { // Inicio del public int Lengh_Field(
            // Devuelve la longitud de un campo.
            int inL_Long = 0;
            //
            try
            {
                inL_Long = ObjPr_Self.BringMe_DB_Table_FieldLengh(st_NombreTabla, st_NombreCampo);
                return inL_Long;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Lengh_Field. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Lengh_Field", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return inL_Long;
        } // Fin del public int Lengh_Field(



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Valid_Objects_BD
        /// Encargado de hacer las validaciones de la base de datos, con base en un archivo de configuracion
        /// definido en st_Archivo_BD_Conf, y para la seccion definida en st_Seccion_Conf
        /// Genera el archivo , st_Archivo_Salida , en formato texto, con la inconsistencias encontradas.
        /// </summary>
        /// <param name="st_Archivo_BD_Conf">Ruta y nombre del archivo de configuracion, con el cual se hacen las validaciones</param>
        /// <param name="st_Seccion_Conf">Nombre de la seccion de la cual se obtienen los objetos a validar</param>
        /// <param name="st_Archivo_Salida">Ruta y nombre del archivo de salida, con las inconsistencias encontradas</param>
        /// <returns></returns>
        public Boolean Valid_Objects_BD(String st_Archivo_BD_Conf, String st_Seccion_Conf, String st_Archivo_Salida)
        { // Inicio del public Boolean Valid_Objects_BD(
            // Realiza las validaciones de existencia de tablas, objetos, etc...
            // para una base de datos, con base un archivo .Conf
            Boolean blL_ValidacionesOK = false;
            //
            try
            {
                blL_ValidacionesOK = ObjPr_Self.Let_BDObjects2Validate(st_Archivo_BD_Conf,  st_Seccion_Conf,  st_Archivo_Salida);
                return blL_ValidacionesOK;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations. System.AccessViolationException", "Valid_Objects_BD", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_DBValidations", "Valid_Objects_BD", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_ValidacionesOK;
        } // Fin del public Boolean Valid_Objects_BD(



       

    } // Fin del class ClasX_DBValidations
}
