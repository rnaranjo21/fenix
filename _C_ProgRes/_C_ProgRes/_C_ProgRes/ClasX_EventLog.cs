using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ExceptionServices;


namespace _C_ProgRes
{

    public class ClasX_EventLog
    { // Iniciio del public class ClasX_EventLog
        private bool blPr_SalLog = false;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalConsole = false;      //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalDialog = false;  //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo
        private string stPr_User = "_C_ProgRes";      //variable tipo String, utilizada para definir el usuario activo en una aplicacion o modulo en el momento de generar el log
        private string stPr_PathErrLog = "C:\\Windows\\_C_ProgRes.log";       //Ruta de acceso para almacenar el log de las apicaciones, por defecto se define: "C:\Windows\fenix.log"
        private NBToolsNet.CLNBTN_Lg ObjPr_Self = null;
        //////////////
        private const String MENSAJE_1 = "EXCEPCIÓN INESPERADA EN EL SISTEMA\n";
        private const String MENSAJE_2 = "Excepción en el sistema";
        private const String MENSAJE_3 = "MENSAJE OPCIONAL DEL LOG\n";
        private const String MENSAJE_4 = "Mensaje Opcional del Seguimiento";
        //
        private bool blPr_EscribeEnHilo = false;
        private int Cta_Numero;
        private string stPr_ArchivoLog;
        private bool p1;
        private bool p2;
        private bool p3;
        

        // constructores
        /// <summary>
        /// Constructor de la clase ClasX_EventLog
        /// Sin Parametros
        /// </summary>
        public ClasX_EventLog()
        {
            ObjPr_Self = new NBToolsNet.CLNBTN_Lg();
        }
        /// <summary>
        /// Constructor de la clase ClasX_EventLog
        /// con parametros de usuario de la aplicacion y el archivo log
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path completo del archivo log</param>
        public ClasX_EventLog(String st_UsuarioApp, String st_ArchivoLog)
        {
            stPr_User = st_UsuarioApp;
            stPr_PathErrLog = st_ArchivoLog;
            ObjPr_Self = new NBToolsNet.CLNBTN_Lg(st_UsuarioApp, st_ArchivoLog);
            //
        }


        /// <summary>
        /// Constructor de la clase ClasX_EventLog
        /// con parametros
        /// </summary>
        ///  <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path completo del archivo log</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_EventLog(String st_UsuarioApp, String st_ArchivoLog, bool bl_SalidaConsola, bool bl_SalidaLog,  bool bl_SalidaDialogo)
        {
            stPr_User = st_UsuarioApp;
            stPr_PathErrLog = st_ArchivoLog;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalDialog = bl_SalidaDialogo ; 
            //
            ObjPr_Self = new NBToolsNet.CLNBTN_Lg(st_UsuarioApp, st_ArchivoLog, bl_SalidaConsola, bl_SalidaLog, bl_SalidaDialogo);
        }


        /// <summary>
        /// Constructor de la clase ClasX_EventLog
        /// con parametros de usuario de la aplicacion y el archivo log
        /// y si escribe el archivo de log, utilizando un hilo o no
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path completo del archivo log</param>
        /// <param name="bl_EscribeEnHilo">Indica si escribe utilizando un hilo o no en el archivo log</param>
        public ClasX_EventLog(String st_UsuarioApp, String st_ArchivoLog, bool bl_EscribeEnHilo)
        {
            stPr_User = st_UsuarioApp;
            stPr_PathErrLog = st_ArchivoLog;
            blPr_EscribeEnHilo = bl_EscribeEnHilo;
            ObjPr_Self = new NBToolsNet.CLNBTN_Lg(st_UsuarioApp, st_ArchivoLog, bl_EscribeEnHilo);
            //
        }


        /// <summary>
        /// Constructor de la clase ClasX_EventLog
        /// con parametros
        /// y si escribe el archivo de log, utilizando un hilo o no
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path completo del archivo log</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        /// <param name="bl_EscribeEnHilo">Indica si escribe utilizando un hilo o no en el archivo log</param>
        public ClasX_EventLog(String st_UsuarioApp, String st_ArchivoLog, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo, bool bl_EscribeEnHilo)
        {
            stPr_User = st_UsuarioApp;
            stPr_PathErrLog = st_ArchivoLog;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalDialog = bl_SalidaDialogo;
            blPr_EscribeEnHilo = bl_EscribeEnHilo;
            //
            ObjPr_Self = new NBToolsNet.CLNBTN_Lg(st_UsuarioApp, st_ArchivoLog, bl_SalidaConsola,  bl_SalidaLog,  bl_SalidaDialogo , bl_EscribeEnHilo);
            //
        }

        public ClasX_EventLog(int Cta_Numero, string stPr_ArchivoLog, bool p1, bool p2, bool p3)
        {
            // TODO: Complete member initialization
            this.Cta_Numero = Cta_Numero;
            this.stPr_ArchivoLog = stPr_ArchivoLog;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Permite Insertar comentarios Opcionales en el log de aplicación
        /// </summary>
        /// <param name="SLTextoOpcional"></param>
        public void setTextErrLog(String st_TextoOpcional) //Este metodo permite ingresar texto opcional al archvio de log de aplicación
        {
            try
            {

                ObjPr_Self.WriteTextInLog(st_TextoOpcional ) ;
                //
                if (blPr_SalDialog == true)
                {
                    ClasX_Utils ShowDialog = new ClasX_Utils();
                    ShowDialog.ShowMessage(MENSAJE_3, st_TextoOpcional, MENSAJE_4);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                NBToolsNet.CLNBTN_Lg objL_Log = new NBToolsNet.CLNBTN_Lg(stPr_User, stPr_PathErrLog, false, true, false);
                objL_Log.WriteOutErrorMessage("_C_ProgRes.DLL", "ClasX_EvenLog", "setTextErrLog. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
            }
            catch (Exception ex)
            {
                NBToolsNet.CLNBTN_Lg objL_Log = new NBToolsNet.CLNBTN_Lg(stPr_User, stPr_PathErrLog, false, true, false);
                objL_Log.WriteOutErrorMessage("_C_ProgRes.DLL", "ClasX_EvenLog", "setTextErrLog. Exception", "", ex.Message.ToString(), "", "");
                //
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// metodo para sacar al log de aplicacion el mensaje de error de aplicación,
        /// recibe los parametros necesarios para identificar los errores
        /// </summary>
        /// <param name="st_Componente">Nombre del Componente</param>
        /// <param name="st_ClaseMod">Nombre de la Clase</param>
        /// <param name="st_Metodo">Nombre del Metodo o Propiedad</param>
        /// <param name="st_CodigoErr">Codigo del Error</param>
        /// <param name="st_MessaDesc">Descripcion del Error</param>
        /// <param name="st_BD">Nombre Base de Datos</param>
        /// <param name="st_InstSQL">Instruccion SQL</param>
        public void outMensajError(String st_Componente, String st_ClaseMod, String st_Metodo, String st_CodigoErr, String st_MessaDesc, String st_BD = "", String st_InstSQL = "")
        {
            try
            { // inicio del try

                ObjPr_Self.WriteOutErrorMessage(st_Componente,  st_ClaseMod,  st_Metodo,  st_CodigoErr,  st_MessaDesc,  st_BD ,  st_InstSQL );
                //
                if (blPr_SalDialog == true)
                {
                    string stL_Message = ObjPr_Self.getMess2Window();
                    //
                    ClasX_Utils ShowDialog = new ClasX_Utils("","");
                    //ShowDialog.ShowMessage(MENSAJE_1, stL_Message, MENSAJE_2);
                    ShowDialog.ShowMessageError(MENSAJE_2, MENSAJE_1, st_Componente,   st_ClaseMod,  st_Metodo,  st_CodigoErr,  st_MessaDesc,  st_BD ,  st_InstSQL );
                }
             } // del Try
            catch (System.AccessViolationException ex_0)
            {
                NBToolsNet.CLNBTN_Lg objL_Log = new NBToolsNet.CLNBTN_Lg(stPr_User, stPr_PathErrLog, false, true, false);
                objL_Log.WriteOutErrorMessage("_C_ProgRes.DLL", "ClasX_EvenLog", "outMensajError. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
             }
            catch (Exception ex)
            {
                NBToolsNet.CLNBTN_Lg objL_Log = new NBToolsNet.CLNBTN_Lg(stPr_User, stPr_PathErrLog, false, true, false);
                objL_Log.WriteOutErrorMessage("_C_ProgRes.DLL", "ClasX_EvenLog", "outMensajError. Exception", "", ex.Message.ToString(), "", "");
            }
        }



        /// <summary>
        /// Propiedad para habilitar la salida del log a un archivo.
        /// Los valores true = se habilita la escritura del archivo y false = deshabilita la escritura del archivo
        /// sintaxis: blSetSalLog = valor; (Valor por defecto: true)
        /// </summary>
        public void setSalLog(bool blR_SalLog)
        {
            blPr_SalLog = blR_SalLog;
            ObjPr_Self.setOutFileLog(blPr_SalLog);
        }
        /// <summary>
        /// Obtiene el valor actual de la variable para habilitar escritura de Log
        /// Sintaxis: Variable = blGetSalLog
        /// </summary>
        private bool getSalLog()
        {
            blPr_SalLog = ObjPr_Self.getOutFileLog();
            return blPr_SalLog;
        }
        /// <summary>
        /// Propiedad para habilitar la salida del log a la consola del sistema.
        /// Los valores true = se habilita la escritura en consola y false = deshabilita la escritura en consola
        /// sintaxis: blSetSalConsole = valor; (Valor por defecto: false
        /// </summary>
        public void setSalConsole(bool blR_SalConsole)
        {

            blPr_SalConsole = blR_SalConsole;
            ObjPr_Self.setOutLineConsole(blPr_SalConsole);
        }
        /// <summary>
        /// Obtiene el valor actual de la variable para habilitar escritura en consola
        /// Sintaxis: Variable = blGetSalConsole;
        /// </summary>
        private bool getSalConsole()
        {
            blPr_SalConsole = ObjPr_Self.getOutLineConsole();
            return blPr_SalConsole;
        }

        /// <summary>
        /// Propiedad para habilitar la salida del log de error en un dialogo
        /// Los valores true = habilita mostrar el mensje y false = deshabilita mostrar el mensaje
        /// sintaxis: blSetSalDialog = valor; (Valor por defecto: true)
        /// </summary>
        public void setSalDialog(bool blR_SalDialog)
        {
            blPr_SalDialog = blR_SalDialog;
            ObjPr_Self.setOutWindow(blPr_SalDialog);
        }
        /// <summary>
        /// Obtiene el valor actual de la variable para habilitar la muestra de errores en dialogos
        /// Sintaxis: Variable = blPr_SalDialog;
        /// </summary>
        private bool getSalDialog()
        {
            blPr_SalDialog = ObjPr_Self.getOutWindow();
            return blPr_SalDialog;
        }
        /// <summary>
        /// Inserta el valor de usuario, para ser grabado en el Log
        /// este valor debe ser insertado una sola vez desde que se inicializa el log
        /// y sera insertado en el log para cada evento
        /// </summary>
        public void setUser(string stR_User)
        {
            stPr_User = stR_User;
            ObjPr_Self.setUser(stPr_User);
        }
        /// <summary>
        /// Devuelve el valor actual de Usuario
        /// </summary>
        public string getUser()
        {
            stPr_User = ObjPr_Self.getUser();
            return stPr_User;
        }


        /// <summary>
        /// Establece la ruta en la que va a ser almacenado el Log de aplicacion
        /// sintaxis: StSetPathArchivoLogErr = "C:\\dir1\\dir2\\nombrearchivo.log"
        /// si esta variable no se define, por defecto se creara en "C:\Windows\fenix.log"
        /// </summary>
        public void setPathArchivoLogErr(string stR_PathErrLog)
        {
            stPr_PathErrLog = stR_PathErrLog;
            ObjPr_Self.setPathErrLog(stPr_PathErrLog);
            //
        }
        /// <summary>
        /// Devuelve el valor actual de la ruta de almacenamiento de log de aplicacion
        /// </summary>
        public string getPathArchivoLogErr()
        {
            stPr_PathErrLog = ObjPr_Self.getPathErrLog();
            return stPr_PathErrLog;
        }


        /// <summary>
        /// Metodo : setEscribeEnHilo
        /// Permite cambiar el valor de la variable privada: 
        /// blPr_EscribeEnHilo
        /// Para saber si se escribe en un hilo o no , el archivo de log.
        /// </summary>
        /// <param name="bl_EscribeEnHilo">Para determinar si el archivo de log se escribe dentro de un hilo o no.</param>
        public void setEscribeEnHilo(bool bl_EscribeEnHilo)
        {
            blPr_EscribeEnHilo = bl_EscribeEnHilo;
            ObjPr_Self.setWrite2Thread(blPr_EscribeEnHilo);
        }


        /// <summary>
        /// Propiedad : getEscribeEnHilo
        /// Devuelve el valor de la variable privada:
        /// blPr_EscribeEnHilo
        /// Para saber si se escribe dentro de un hilo el archivo de log.
        /// </summary>
        /// <returns>blPr_EscribeEnHilo = Indica si se escribe dentro de un hilo el archivo de log. </returns>
        public bool getEscribeEnHilo()
        {
            blPr_EscribeEnHilo = ObjPr_Self.getWrite2Thread();
            return blPr_EscribeEnHilo;

        }


        

       



    } // Fin del public class ClasX_EventLog
}
