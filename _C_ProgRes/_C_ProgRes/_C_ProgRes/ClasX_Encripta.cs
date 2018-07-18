using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ExceptionServices;

namespace _C_ProgRes
{

    public class ClasX_Encripta
    { // Inicio del CmdClasesAcceso_Click
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo
        //
        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_Es ObjPr_Self = null;

        // constructores 

        /// <summary>
        /// Constructor de la clase sin parametros
        /// </summary>
        public ClasX_Encripta()
        {
            ObjPr_Self = new NBToolsNet.CLNBTN_Es(stPr_Info);
        }
        /// <summary>
        /// Constructor de la clase con parametros de usuario y path-archivo log
        /// </summary>
        /// <param name="st_UsuarioApp"></param>
        /// <param name="st_ArchivoLog"></param>
        public ClasX_Encripta(String st_UsuarioApp, String st_ArchivoLog)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            ObjPr_Self = new NBToolsNet.CLNBTN_Es(stPr_UsuarioAPP, stPr_ArchivoLog, stPr_Info);
        }

        /// <summary>
        /// Constructor de la clase con parametros de usuario y path-archivo log
        /// Tambien con los parametros para el manejo del log.
        /// </summary>
        /// <param name="st_UsuarioApp"></param>
        /// <param name="st_ArchivoLog"></param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_Encripta(String st_UsuarioApp, String st_ArchivoLog, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo; 
            //
            ObjPr_Self = new NBToolsNet.CLNBTN_Es(st_UsuarioApp, st_ArchivoLog, bl_SalidaConsola, bl_SalidaLog, bl_SalidaDialogo, stPr_Info);
        }

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
        //
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
        /// Metodo :; EncriptaInfo
        /// Encripta informacion
        /// </summary>
        /// <param name="st_TextoToEncrip">Texto a Encriptar</param>
        /// <returns>Texto Encriptado</returns>
        public String EncriptaInfo(String st_TextoToEncrip, String st_KeyToSee)
        { // Inicio del EncriptaInfo
            String stL_TextoEncrip = "";
            try
            { // Inicio del Try
                //
                stL_TextoEncrip = ObjPr_Self.File2Es(st_TextoToEncrip, "gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnns06srbyMg", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "QHT6kUHtr2zRbupap5KPu4jeO9GUVQ2SZO7UMnns06srbyE+gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aMg", stPr_Info);
                return stL_TextoEncrip;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            { // Inicio del catch  (System.AccessViolationException ex_0)
                stL_TextoEncrip = "";
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Encripta", "EncriptaInfo. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            } // Fin del catch  (System.AccessViolationException ex_0)
            catch (Exception ex)
            { // Inicio del catch (Exception ex)
                stL_TextoEncrip = "";
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Encripta", "EncriptaInfo", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            } // Fin del catch (Exception ex)
            return stL_TextoEncrip;
        } // Fin del EncriptaInfo
        ///////////////////////////////////////////////////////////////


        [HandleProcessCorruptedStateExceptions]
        public String DesEncriptaInfo(String st_TextoADesencriptar, String st_KeyToSee)
        { // Inicio del DesEncriptaInfo
            String stL_TextoDesEncrip = "";
            try
            { // Inicio del Try
                //
                stL_TextoDesEncrip = ObjPr_Self.File2Des("/roixTlcaiyrEDjdINvoCg==", "gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnns06srbyMg", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "QHT6kUHtr2zRbupap5KPu4jeO9GUVQ2SZO7UMnns06srbyE+gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aMg", stPr_Info);
                stL_TextoDesEncrip = ObjPr_Self.File2Des(st_TextoADesencriptar, "gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnns06srbyMg", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "QHT6kUHtr2zRbupap5KPu4jeO9GUVQ2SZO7UMnns06srbyE+gYjcEY/ns2slXFrk==FT/yQYmiWTURobdSariTY=+-6aMg", stPr_Info);
                return stL_TextoDesEncrip ;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            { // Inicio del catch  (System.AccessViolationException ex_0)
                stL_TextoDesEncrip = "";
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Encripta", "DesEncriptaInfo. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            } // Fin del catch  (System.AccessViolationException ex_0)
            catch (Exception ex)
            { // Inicio del catch (Exception ex)
                stL_TextoDesEncrip = "";
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Encripta", "DesEncriptaInfo", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            } // Fin del catch (Exception ex)
            return stL_TextoDesEncrip;
        } // Fin del DesEncriptaInfo
        ///////////////////////////////////////////////////////////////



    } // Fin del CmdClasesAcceso_Click
}
