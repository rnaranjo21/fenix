using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Runtime.ExceptionServices;

namespace _C_ProgRes
{
    /// <summary>
    /// ClasX_AccesoTabla : Clase encaragada de accesar una tabla
    /// </summary>
    public class ClasX_AccesoTabla
    { // Inicio del  class ClasX_AccesoTabla
        private String stPr_NombreTabla = ""; //  Define el nombre de la tabla, con la cual se va a trabajar.
        //
        private int inPr_CantCamposAdd = 0 ; // Cantidad de Campos que se han adicionado.
        //
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        //
        private String stPr_Info = "CAJA HONOR";
        private NBToolsNet.CLNBTN_Ta ObjPr_Self = null;
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo


        /// <summary>
        /// Constructor de la clase, ClasX_AccesoTabla
        /// Redimensiona Arreglos y variables para trabajar con una nueva tabla.
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre del archivo de log</param>
        /// <param name="Obj_InfoBD">Objeto con la informacion de la base de datos de la tabla a trabajar</param>
        /// <param name="st_NombreTabla">Nombre de la tabla con la cual va a trabajar</param>
        public ClasX_AccesoTabla(String st_UsuarioApp, String st_ArchivoLog, ClasX_DBInfo Obj_InfoBD)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(st_UsuarioApp, st_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_InfoBD, ref O_Aux);
                ObjPr_Self = new NBToolsNet.CLNBTN_Ta(st_UsuarioApp, st_ArchivoLog, O_Aux, stPr_Info);
                stPr_UsuarioAPP = st_UsuarioApp;
                stPr_ArchivoLog = st_ArchivoLog; 
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ClasX_AccesoTabla(1). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ClasX_AccesoTabla(1)", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
        }

        /// <summary>
        /// Constructor de la clase, ClasX_AccesoTabla
        /// Redimensiona Arreglos y variables para trabajar con una nueva tabla.
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre del archivo de log</param>
        /// <param name="Obj_InfoBD">Objeto con la informacion de la base de datos de la tabla a trabajar</param>
        /// <param name="st_NombreTabla">Nombre de la tabla con la cual va a trabajar</param>
        public ClasX_AccesoTabla(String st_UsuarioApp, String st_ArchivoLog, ClasX_DBInfo Obj_InfoBD, String st_NombreTabla)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(st_UsuarioApp, st_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_InfoBD, ref O_Aux);
                ObjPr_Self = new NBToolsNet.CLNBTN_Ta(st_UsuarioApp, st_ArchivoLog, O_Aux, st_NombreTabla, stPr_Info);
                stPr_UsuarioAPP = st_UsuarioApp;
                stPr_ArchivoLog = st_ArchivoLog;
                stPr_NombreTabla = st_NombreTabla;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ClasX_AccesoTabla(2). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ClasX_AccesoTabla(2)", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
        }


        /// <summary>
        /// Constructor de la clase, ClasX_AccesoTabla
        /// Redimensiona Arreglos y variables para trabajar con una nueva tabla.
        /// Y recibe parametros para manejo del log.
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre del archivo de log</param>
        /// <param name="Obj_InfoBD">Objeto con la informacion de la base de datos de la tabla a trabajar</param>
        /// <param name="st_NombreTabla">Nombre de la tabla con la cual va a trabajar</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_AccesoTabla(String st_UsuarioApp, String st_ArchivoLog, ClasX_DBInfo Obj_InfoBD, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(st_UsuarioApp, st_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_InfoBD, ref O_Aux);
                ObjPr_Self = new NBToolsNet.CLNBTN_Ta(st_UsuarioApp, st_ArchivoLog, O_Aux, bl_SalidaConsola, bl_SalidaLog, bl_SalidaDialogo, stPr_Info);
                stPr_UsuarioAPP = st_UsuarioApp;
                stPr_ArchivoLog = st_ArchivoLog;
                blPr_SalConsole = bl_SalidaConsola;
                blPr_SalLog = bl_SalidaLog;
                blPr_SalDialog = bl_SalidaDialogo;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ClasX_AccesoTabla(3). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ClasX_AccesoTabla(3)", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
        }



        /// <summary>
        /// Constructor de la clase, ClasX_AccesoTabla
        /// Redimensiona Arreglos y variables para trabajar con una nueva tabla.
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre del archivo de log</param>
        /// <param name="Obj_InfoBD">Objeto con la informacion de la base de datos de la tabla a trabajar</param>
        /// <param name="st_NombreTabla">Nombre de la tabla con la cual va a trabajar</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_AccesoTabla(String st_UsuarioApp, String st_ArchivoLog, ClasX_DBInfo Obj_InfoBD, String st_NombreTabla, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            try
            {
                //
                ClasX_Utils ObjAux = new ClasX_Utils(st_UsuarioApp, st_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_InfoBD, ref O_Aux);
                ObjPr_Self = new NBToolsNet.CLNBTN_Ta(st_UsuarioApp, st_ArchivoLog, O_Aux, st_NombreTabla, bl_SalidaConsola, bl_SalidaLog, bl_SalidaDialogo, stPr_Info);
                //
                stPr_UsuarioAPP = st_UsuarioApp;
                stPr_ArchivoLog = st_ArchivoLog;
                blPr_SalConsole = bl_SalidaConsola;
                blPr_SalLog = bl_SalidaLog;
                blPr_SalDialog = bl_SalidaDialogo;
                stPr_NombreTabla = st_NombreTabla;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ClasX_AccesoTabla(4). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ClasX_AccesoTabla(4)", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Clear
        /// Limpia Variables redimensiona arreglos de los campos.
        /// </summary>
        public void Clear()
        { 
            try
            {
                //
                ObjPr_Self.ToDo_Clear();
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Clear. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Clear", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } 


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Clear_Info_Only
        /// Limpia informacion de los campos unicamente
        /// </summary>
        public void Clear_Info_Only()
        { 
            try
            {
                ObjPr_Self.ToDo_Clear_Info_Only();
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Clear_Info_Only. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Clear_Info_Only", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void Clear_Info_Only(




        /// <summary>
        /// Propiedad : getNombreTabla
        /// Presenta el nombre de la tabla
        /// </summary>
        /// <returns>stPr_NombreTabla: Nombre de la tabla</returns>
        public String getNombreTabla()
        {
            stPr_NombreTabla = ObjPr_Self.getTableName();
            return stPr_NombreTabla;
        }


        /// <summary>
        /// Propiedad : getCantidadCampos
        /// Devuelve la cantidad de campos que se estan manejando en la clase.
        /// </summary>
        /// <returns>inPr_CantCamposAdd : La cantidad de campos</returns>
        public int getCantidadCampos()
        {
            inPr_CantCamposAdd = ObjPr_Self.getQuantity_Fields();
            return inPr_CantCamposAdd;
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
        /// Metodo : setNombreTabla
        /// Permite Asignar el nombre de la tabla
        /// </summary>
        /// <param name="stDato">Nombre de la tabla</param>
        public void setNombreTabla(String stDato)
        {
            stPr_NombreTabla = stDato;
            ObjPr_Self.setTableName(stPr_NombreTabla);
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : setInfoBD
        /// Utilizado para cambiar la informacion de la base de datos
        /// con la cual la clase de acceso va a trabajar.
        /// Esto permite que si la clase originalmente se crea para trabajar con por ejemplo MYSQL
        /// Luego sin perder los datos que tiene la clase, pueda trabajar con SQLSERVER u otro
        /// motor de base de datos y se sigan haciendo las operaciones de la clase, como grabar etc...
        /// </summary>
        /// <param name="Obj_InfoBD">Del tipo ClasX_DBInfo, el cual contiene la informacion de la base de datos con la cual la case va a trabajar.</param>
        public void setInfoBD(ClasX_DBInfo Obj_InfoBD)
        {
            try
            {
                //
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_InfoBD, ref O_Aux);
                ObjPr_Self.setDataBaseInfo(O_Aux);
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "setInfoBD. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "setInfoBD", "", ex.Message.ToString(), "", "");
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

        

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : AddField
        /// Adiciona un campo para trabajar con la tabla
        /// </summary>
        /// <param name="st_Campo_Tabla">Nombre del campo</param>
        /// <param name="bl_Campo_Llave">true = campo llave de la tabla</param>
        /// <param name="st_TipoDatoCampo">Tipo de Dato, String, Numerico o Date</param>
        /// <param name="st_Tag">Informacion Adicional, para el campo</param>
        public void AddField(String st_Campo_Tabla, Boolean bl_Campo_Llave, String st_TipoDatoCampo, String st_Tag, Boolean bl_GrabaEnModificacion, Boolean bl_AutoIncrementable = false, Boolean bl_GrabaSolo_En_Modificacion = false)
        { // Inicio del public void AddField
            try
            {
                ObjPr_Self.ToDo_AddField(st_Campo_Tabla,  bl_Campo_Llave,  st_TipoDatoCampo,  st_Tag,  bl_GrabaEnModificacion,  bl_AutoIncrementable ,  bl_GrabaSolo_En_Modificacion);
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "AddField. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "AddField", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // Fin del public void AddField


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ExisteCampo
        /// Valida si un campo existe.
        /// </summary>
        /// <param name="st_Campo">Nombre del Campo</param>
        /// <param name="inR_IndexCampo">Devuelve el indice del campo en el arreglo de campos</param>
        /// <returns></returns>
        public Boolean ExisteCampo(String st_Campo, ref int inR_IndexCampo)
        { // Inicio del public void ExisteCampo
            Boolean blL_Existe = false;
            try
            {
                //
                inR_IndexCampo = 0;
                blL_Existe = ObjPr_Self.Is_An_Existing_Field(st_Campo, ref inR_IndexCampo);
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ExisteCampo. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "ExisteCampo", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Existe;
        } // Fin del public void ExisteCampo
        /////////////////////////////////////////

        


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : AsignaDato_X_Campo
        /// permite asignar el contenido a un campo en la clase.
        /// Devuelve True, si pudo asignar el contenido el campo
        /// </summary>
        /// <param name="st_Campo">Nombre del Campo</param>
        /// <param name="st_ContenidoCampo">Contenido del Campo</param>
        /// <returns></returns>
        public Boolean AsignaDato_X_Campo(String st_Campo, String st_ContenidoCampo)
        { // Inicio del public Boolean AsignaDato_X_Campo
            Boolean blL_Existe = false;
            try
            {
                blL_Existe = ObjPr_Self.Let_Datum_4_Field(st_Campo, st_ContenidoCampo);
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "AsignaDato_X_Campo. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "AsignaDato_X_Campo", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Existe;
        } // Fin del public Boolean AsignaDato_X_Campo
        /////////////////////////////////////////

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : DevuelveDato_X_Campo
        /// Devuelve el contenido de un campo dentro de la clase
        /// Devuelve true, si encontro el campo en la clase.
        /// </summary>
        /// <param name="st_Campo">Nombre del Campo</param>
        /// <param name="st_ContenidoCampo">Devuelve el contenido del campo</param>
        /// <returns></returns>
        public Boolean DevuelveDato_X_Campo(String st_Campo, ref String st_ContenidoCampo)
        { // Inicio del public Boolean DevuelveDato_X_Campo
            Boolean blL_Existe = false;
            try
            {
                st_ContenidoCampo = "";
                blL_Existe = ObjPr_Self.BringMe_FieldContents(st_Campo, ref st_ContenidoCampo);
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "DevuelveDato_X_Campo. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "DevuelveDato_X_Campo", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Existe;
        } // Fin del public Boolean DevuelveDato_X_Campo


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Existe_Registro
        /// Encargado de validar si existe o no el registro, dependiendo
        /// de los datos que se tengan cargados en la clase.
        /// </summary>
        /// <param name="st_Sql">Instruccion SQL Adicional, por ejemplo si se valida la existencia del registro con un SP, se envia la instruccion del SP</param>
        /// <param name="bl_LeeTabla">true=Indica que carga los datos de la tabla en la estructura de la clase</param>
        /// <returns></returns>
        public Boolean Existe_Registro(String st_Sql = "", Boolean bl_LeeTabla = false, Boolean bl_InstruccionSP = false)
        { // Inicio del public Boolean Existe_Registro
            Boolean blL_Existe = false;
            //
            try
            {
                blL_Existe = ObjPr_Self.Is_An_Existing_Record(st_Sql, bl_LeeTabla, bl_InstruccionSP);
                return blL_Existe;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Existe_Registro. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Existe_Registro", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Existe;
        } // Fin del public Boolean Existe_Registro
        /////////////////////////////////////////

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Delete_Registro
        /// Se encarga de eliminar un registro de la tabla
        /// con base en la informacion de las llaves primarias definidas en la clase
        /// </summary>
        /// <returns>true=Elimino el Registro</returns>
        public Boolean Delete_Registro()
        { // Inicio del public Boolean Delete_Registro
            Boolean blL_Delete = false;
            try
            {
                blL_Delete = ObjPr_Self.ToDo_Delete_Record();
                return blL_Delete;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Delete_Registro. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Delete_Registro", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Delete;
        } // Fin del public Boolean Delete_Registro
        /////////////////////////////////////////

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Insert_Registro
        /// Encargado de hacer el insert en la tabla de la clase
        /// </summary>
        /// <returns>true=Si hizo el insert</returns>
        public Boolean Insert_Registro()
        { // Inicio del public Boolean Insert_Registro
            Boolean blL_Insert = false;
            try
            {
                blL_Insert = ObjPr_Self.ToDo_Insert_Record();
                return blL_Insert;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Insert_Registro. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Insert_Registro", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Insert;
        } // Fin del public Boolean Insert_Registro
        /////////////////////////////////////////

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Update_Registro
        /// Actualiza la informacion de la tabla en la base de datos.
        /// </summary>
        /// <returns>true=Si actualizo la informacion</returns>
        public Boolean Update_Registro()
        { // Inicio del public Boolean Update_Registro
            Boolean blL_Update = false;
            try
            {
                blL_Update = ObjPr_Self.ToDo_Update_Record();
                return blL_Update;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Update_Registro. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Update_Registro", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Update;
        } // Fin del public Boolean Update_Registro


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Gtraba un registro. Hace insert o update dependiendo si existe o no en la tabla.
        /// </summary>
        /// <param name="st_Sql">Opcional. Instruccion SQL Para lectura del registro en la tabla</param>
        /// <param name="bl_InstruccionSP">True Si es una instruccion con SP</param>
        /// <returns>TRUE Si graba el registro con exito en la bd</returns>
        public Boolean Grabar_Registro(String st_Sql = "",  Boolean bl_InstruccionSP = false)
        {
            // Encargado de hacer el insert o el update del registro
            // 
            Boolean blL_Grabar = false;
            try
            {
                blL_Grabar = ObjPr_Self.ToDo_Save_Record();
                return blL_Grabar;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Grabar_Registro. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Grabar_Registro", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_Grabar;
        }
        /////////////////////////////////////////

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo ; Valores_To_Interface
        /// Devuelve los valores a los controles de la forma o ventana
        /// que entra como parametro.
        /// </summary>
        /// <param name="Forma">El objeto de la forma</param>
        /// <param name="st_NombreTabla">Nombre de la tabla</param>
        /// <returns></returns>
        public Boolean Valores_To_Interface(Control Forma, String st_NombreTabla)
        { // Inicio del  public Boolean bo_Valores_To_Interface(
            //
            Boolean blL_DatosOk = false;
            try
            { // Inicio del Try
                blL_DatosOk = ObjPr_Self.Let_Values_2_Interface(Forma, st_NombreTabla);
                return blL_DatosOk;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                blL_DatosOk = false; 
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Valores_To_Interface. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                blL_DatosOk = false; 
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Valores_To_Interface", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_DatosOk;
        } // Fin del  public Boolean bo_Valores_To_Interface(
        /////////////////////////////////////////

        [HandleProcessCorruptedStateExceptions]
        public void Halla_Parametros_X_Control(String st_NombreControl, String st_TagControl, ref String stR_TablaControl, ref String stR_PropiedadControl, ref String stR_CampoControl, ref Boolean blR_DatosOk)
        {
            //
            try
            { // Inicio del Try
                //
                stR_TablaControl = "";
                stR_PropiedadControl = "";
                stR_CampoControl = "";
                blR_DatosOk = false;
                //
                ObjPr_Self.BringMe_ControlParamInfo(st_NombreControl,  st_TagControl, ref  stR_TablaControl, ref  stR_PropiedadControl, ref  stR_CampoControl, ref  blR_DatosOk);
            } // fin del Try
            //
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Valores_To_Interface. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Valores_To_Interface", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }
        /////////////////////////////////////////

        [HandleProcessCorruptedStateExceptions]
        public Boolean Interface_To_Valores(Control Forma, String st_NombreTabla)
        { // Inicio del  public Boolean Interface_To_Valores(
            Boolean blL_DatosOk = false;
            try
            { // Inicio del Try
                //
                blL_DatosOk = ObjPr_Self.Let_Interface_2_Values(Forma, st_NombreTabla);
                return blL_DatosOk;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                blL_DatosOk = false;
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Interface_To_Valores. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                blL_DatosOk = false;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "Interface_To_Valores", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return blL_DatosOk;
        } // Fin del  public Boolean Interface_To_Valores(


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : CambiaInfo_BD
        /// Permite cambiar la informacion de la base de datos con la cual
        /// trabaja la clase de acceso
        /// </summary>
        /// <param name="Obj_InfoBD">Objeto con la informacion de la base de datos</param>
        public void CambiaInfo_BD ( ClasX_DBInfo Obj_InfoBD )
        {
            // Nada
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_InfoBD, ref O_Aux);
                ObjPr_Self.Let_Change_DBInfo(O_Aux);
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "CambiaInfo_BD. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_AccesoTabla", "CambiaInfo_BD", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

    } // FIn del  class ClasX_AccesoTabla
}
