using System;
using System.IO;
using System.Runtime.ExceptionServices;
//
namespace _C_ProgRes
{
    public class ClasX_Config
    {   /// <summary>
        /// Clase para la generacion de Un texto plano con la configuración de aplicaciones y usuarios
        /// </summary>
        /// <remarks> OBTIENE LOS VALORES PROPORCIONADOS PARA INSERTAR EN LAS LLAVES 
        /// DEL ARCHIVO .CONFIG</remarks>

        private String stPr_path = "Configuracion.cfg"; //Nombre del archivo de configuracion y ruta de acceso
        private String[] stPr_parametros = new String[0];            //Los parametros obtenidos del archivo
        private Boolean blPr_paramOK = false;                        //variable tipo boolean para definir si el error de aplicacion se muestra en una ventana dialogo

        private String stPr_user = "Fenix";
        private String stPr_patLog = "_C_ProgRes_conf.log";

        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo
        ////////////////////////////////////////////////////
        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_Cg ObjPr_Self = null;


        ///////////////////////////
        /// <summary>
        /// Constructor de la clase ClasX_Config
        /// </summary>
        public ClasX_Config()
        { 
            ObjPr_Self = new NBToolsNet.CLNBTN_Cg(stPr_Info);
        }

        /// <summary>
        /// Constructor que recibe los parametros de usuario y archivo log
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path completo del archivo log</param>
        public ClasX_Config(String st_ArchivoConfig, String st_UsuarioApp, String st_ArchivoLog)
        {
            stPr_path = st_ArchivoConfig; 
            stPr_user = st_UsuarioApp;
            stPr_patLog = st_ArchivoLog; 
            //
            ObjPr_Self = new NBToolsNet.CLNBTN_Cg(st_ArchivoConfig, st_UsuarioApp, st_ArchivoLog, stPr_Info);
        }

        /// <summary>
        /// Constructor que recibe los parametros para el tratamiento del log
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path completo del archivo log</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_Config(String st_ArchivoConfig, String st_UsuarioApp, String st_ArchivoLog, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            stPr_path = st_ArchivoConfig;
            stPr_user = st_UsuarioApp;
            stPr_patLog = st_ArchivoLog;
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo; 
            //
            ObjPr_Self = new NBToolsNet.CLNBTN_Cg(st_ArchivoConfig, st_UsuarioApp, st_ArchivoLog, bl_SalidaConsola,  bl_SalidaLog,  bl_SalidaDialogo, stPr_Info);
        }


        /// <summary>
        /// Gets y Sets para los parametros PathConf</param>
        /// </summary>
        public void setPathConf(String stR_ruta)
        {
            stPr_path = stR_ruta;
            ObjPr_Self.setPathConf(stPr_path);
        }

        private String getPathConf()
        {
            stPr_path = ObjPr_Self.getPathConf();
            return stPr_path;
        }
        /// <summary>
        /// Gets y Sets para los parametros setUser</param>
        /// </summary>
        public void setUser(String stR_Usuario)
        {
            stPr_user = stR_Usuario;
            ObjPr_Self.setUser(stPr_user);
        }

        public String getUser()
        {
            stPr_user = ObjPr_Self.getUser();
            return stPr_user;
        }
        /// <summary>
        /// Gets y Sets para los parametros PathLog</param>
        /// </summary>
        public void setPathLog(String stR_PathLog)
        {
            stPr_patLog = stR_PathLog;
            ObjPr_Self.setPathErrLog(stPr_patLog);
        }

        public String getPathLog()
        {
            stPr_patLog = ObjPr_Self.getPathErrLog();
            return stPr_patLog;
        }

        /// <summary>
        /// Gets y Sets para los parametros PathArchivo</param>
        /// </summary>
        public String getPathArchivo()
        {
            stPr_path = ObjPr_Self.getPathConf();
            return stPr_path;
        }
        public void setPathArchivo(String sdato)
        {
            stPr_path = sdato;
            ObjPr_Self.setPathConf(stPr_path);
        }
        //

        public Boolean getparamOK()
        {
            blPr_paramOK = ObjPr_Self.getInfoParamOk();
            return blPr_paramOK;
        }
        //

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
        /// Funcion que guarda la informacion del archivo que esta manejando
        /// Con esta funcion se toma la informacion que esta en el arreglo y se guarda  en el archivo que se indica en el destino
        /// Llama el metodo privado , GuardarArchivoConfig.
        /// </summary>
        /// <param name="destino">La ruta y el nombre del archivo de destino que se va a grabar</param>
        public void GuardarArchivo(String destino)
        {
            try
            {
                ObjPr_Self.SaveFile(destino);
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "GuardarArchivo(0). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "GuardarArchivo(0)", "",  ex.Message.ToString(), "", "");
                //
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo encargado de grabar el archivo de parametros, con base en el arreglo
        /// que tiene cargado en ese momento.
        /// Llama el metodo privado , GuardarArchivoConfig.
        /// </summary>
        public void GuardarArchivo()
        {
            try
            {
                ObjPr_Self.SaveFile(stPr_path);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "GuardarArchivo(0). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "GuardarArchivo(0)", "", ex.Message.ToString(), "", "");
                //
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        /// <summary>
        /// Gets de los parametros.
        /// Entrega la lista de parametros recuperada del archivo y cargada en el objeto
        /// </summary>
        /// Los parametros</param>
        /// </returns>
        public String[] getParametros()
        {
            stPr_parametros = ObjPr_Self.getInfoFile();
            return stPr_parametros;
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Busca el parametro, dentro del arreglo
        /// </summary>
        /// El indice en el arreglo del parametro que se busca.
        /// </returns>
        /// <param name='parametro'>Parametro a buscar dentro de un arreglo recorriendolo por completo hasta q lo busque</param>
        public int buscaParemetro(String parametro)
        {
            int inL_Indice = -1;
            try
            {
                inL_Indice = ObjPr_Self.LookUpParam(parametro);
            }
            catch (System.AccessViolationException ex_0)
            {
                inL_Indice = -1;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "buscaParemetro. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                inL_Indice = -1;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "buscaParemetro", "", ex.Message.ToString(), "", "");
                //
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return inL_Indice ;
        }


       

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Funcion que envia los parametros en un formato Parametro = Valor, recibe dos parametros de tipo String
        /// Con esta funcion se toma la informacion y se acopla al formato anteriormente dicho, y se da el valor a la respectiva llave
        /// </summary>
        /// <param name="parametro">Parametro a buscar en el arreglo actual</param>
        /// <param name="valor">Valor que se encuentra en la llave</param>
        public void setParametro(String parametro, String valor)
        {
            try
            {
                ObjPr_Self.setValueOfParam(parametro,  valor);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgReg.Dll", "ClasX_Config", "setParametro. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgReg.Dll", "ClasX_Config", "setParametro", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

        /// <summary>
        /// En caso de inicializar el objeto desde un arreglo armado se hace con la funcion setParametros</param>
        /// </summary>
        /// <param name="parametros">Es un arreglo de tipo String con la estructura Parametro = Valor</param>
        public void setParametros(String[] parametros)
        {
            stPr_parametros = parametros;
            ObjPr_Self.setInfoFile(stPr_parametros);
        }


        



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo que elimina el archivo que entra como parametro.
        /// </summary>
        /// <param name="stR_Destino">Ruta y nombre del archivo que se va a eliminar</param>
        public void eliminarArchivo(String stR_Destino)
        {
            try
            {
                ObjPr_Self.DeleteAFile(stR_Destino);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminarArchivo", "", "System.AccessViolationException: " + ex_0, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (ArgumentNullException e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminarArchivo", "", "ArgumentNullException: " + e, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (ArgumentException e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminarArchivo", "", "ArgumentException: " + e, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (DirectoryNotFoundException e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminarArchivo", "", "DirectoryNotFoundException: " + e, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (IOException e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminarArchivo", "", "IOException: " + e, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (NotSupportedException e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminarArchivo", "", "NotSupportedException: " + e, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (UnauthorizedAccessException e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminarArchivo", "", "UnauthorizedAccessException: " + e, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex_1)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminarArchivo", "", "Exception: " + ex_1, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo que halla la informacion de la seccion dentro del arreglo de parametros
        /// Halla la posicion Inical y la Final de la seccion dentro del arreglo de parametros
        /// y carga el arreglo 'cadena'.
        /// </summary>
        /// <param name="st_Seccion">Nombre de la seccion a buscar.</param>
        /// <returns></returns>
        public String[] seccion(String st_Seccion)
        {
            // Halla el indice de la seccion
            int Inicio = 0;
            int Final = 0;
            //
            String[] cadena = new String[Final - Inicio];
            try
            {
                cadena = ObjPr_Self.LookUpLoadSection(st_Seccion);
                return cadena;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "seccion.System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
                cadena = null;
                return cadena;
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "seccion", "", ex.Message.ToString() , "", "");
                //
                cadena = null;
                return cadena;
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Funcion que recibe tres parametros de tipo string llamados secc, llave y valor.
        /// ModificaLlave, recorre un arreglo refe en busca de una seccion especifica, despues busca una llave especifica y el valor correspondiente a esta para modificarlo.
        /// </summary>
        /// <param name="st_Secc">Seccion de la llave a modificar</param>
        /// <param name="st_LLave">LLave a modificar</param>
        /// <param name="st_Valor">Valor a dejar en la llava a modificar</param>
        public void ModificaLlave(String st_Secc, String st_LLave, String st_Valor)
        {
            try
            {
                ObjPr_Self.ModifyAKey(st_Secc,  st_LLave,  st_Valor);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "ModificaLlave", "", "System.AccessViolationException: " + ex_0, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "ModificaLlave", "", "Exception: " + e, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Funcion para eliminar un valor especifico en una llave de una seccion. Recibe dos parametros de tipo string, stR_seccion y stR_Llave, para iniciar la busqueda.
        /// Recorre el arreglo en busca de dichos parametros y cuando encuentre la seccion especifica busca la llave especifica dentro de dicha seccion para ser eliminada.
        /// </summary>
        /// <param name="String stR_seccion">Referencia a las secciones del texto plano</param>
        /// <param name="String param = "[" + stR_seccion + "]"">Parametro para buscar secciones dentro del archivo plano teniendo en cuenta "[ ]", para inicio y fin de seccion</param>
        /// <param name="String stR_Llave">Referencia a las llaves del texto plano</param>       
        /// <param name="String[] refe = seccion(stR_seccion);">Arreglo de tipo string para que me almacene la seccion que se busca</param>
        /// <param name="int pos = buscaParemetro(param);">Variable de tipo entero para buscar el parametro especifico</param>
        public void eliminaLlaveSeccion(String st_Secc, String st_LLave)
        {
            try
            {
                ObjPr_Self.DeleteAKeyFromSection(st_Secc, st_LLave);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminaLlaveSeccion. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "eliminaLlaveSeccion", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo que lee una llave de una seccion
        /// </summary>
        /// <param name="st_Seccion">Seccion en la cual esta la llave a buscar.</param>
        /// <param name="st_LLave">La Llave a Buscar.</param>
        /// <returns></returns>
        public String LeeLlave_Seccion(String st_Seccion , String st_LLave)
        {
            String stL_ValorLlave = "";
            try
            {
                stL_ValorLlave = ObjPr_Self.ReadAKeyFromSection(st_Seccion, st_LLave);
                return stL_ValorLlave;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "LeeLlave_Seccion. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
                return stL_ValorLlave;
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "LeeLlave_Seccion", "", ex.Message.ToString(), "", "");
                //
                return stL_ValorLlave;
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


       

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metedo : AgregarLineaAlFinal
        /// Adiciona una linea al final del arreglo con la informacion del archivo de configuracion.
        /// NO VALIDA SI LA SECCION O LLAVE EXISTE, SOLO ADICIONA LA LINEA AL FINAL DEL ARCHIVO
        /// </summary>
        /// <param name="slinea">La linea a adicionar al final del archivo</param>
        public void AgregarLineaAlFinal(String slinea)
        {
            try
            {
                ObjPr_Self.AddNewLine2End(slinea);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgReg.Dll", "ClasX_Config", "AgregarLineaAlFinal. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgReg.Dll", "ClasX_Config", "AgregarLineaAlFinal", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : SobreEscribe_ArchivoConfiguracion
        /// Sobre escribe el archivo de configuracion, con los datos que tiene cargados actualmente.
        /// GRABA LA INFORMACION QUE TIENE EL ARREGLO ( stPr_parametros ) EN ESTE MOMENTO
        /// NO SE VALIDA NADA EN EL MOMENTO DE GRABAR, PASA DEL ARREGLO ( stPr_parametros ) AL ARCHIVO DE CONFIGURACION
        /// </summary>
        public void SobreEscribe_ArchivoConfiguracion()
        {
            //
            try
            {
                ObjPr_Self.OverWriteConfFile();
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "SobreEscribe_ArchivoConfiguracion", "", "System.AccessViolationException: " + ex_0, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_user, stPr_patLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Config", "SobreEscribe_ArchivoConfiguracion", "", "Exception: " + e, "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }




    }
}
