#region usings
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;
//
#endregion

namespace _C_ProgRes
{
    /// <summary>
    /// Clase de conexiones TCP, permite conectarse con un servidor catai 
    /// Hereda de la clase IDisposable, para implementar el método Dispose
    /// </summary>
    public class ClasX_ClienteTCP : IDisposable
    {
        #region campos privados
        /// <summary>
        /// Campo de tipo IPAddress para ser utilizado al momento de construir el socket
        /// </summary>
        private IPAddress IpPr_IPServidor = null;
        /// <summary>
        /// Direccion IP del servidor remoto
        /// </summary>
        private String stPr_IPServidor = "127.0.0.1";
        /// <summary>
        /// Usuario actual de la aplicación, para ser grabado en el Log de la misma
        /// </summary>
        private String stPr_Usuario = "Fenix";
        /// <summary>
        /// ruta donde se escribe el log de la aplicación
        /// </summary>
        private String stPr_pathLog = "ConTCP.log";
        private String stPr_RespuestaGral = "";
        /// <summary>
        /// Puerto TCP al cual se conecta el cliente, se utiliza para construir el socket
        /// </summary>
        private int inPr_puerto = 0;
        /// <summary>
        /// Variable que representa las respuestas obtenidas desde el servidor al ejecutar cada comando
        /// </summary>
        private int inPr_respuestaSolBot = -9;
        /// <summary>
        /// objeto "ClasX_EventLog" para llevar el log de eventos de la palicacion
        /// </summary>
        private ClasX_EventLog objPr_LogEvent;
        /// <summary>
        /// variable para conocer el estado actula de la conexion con el servidor
        /// </summary>
        //private bool blPr_conectado = false;
        /// <summary>
        /// Variable para el usuario de Pandora
        /// </summary>
        private String stPr_Usuario_Pandora = "";
        /// <summary>
        /// Variable para la clave del usuario de Pandora.
        /// </summary>
        private String stPr_Clave_Usuario_Pandora = "";
        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_CliPro ObjPr_Self = null;
        private String stPr_TrxFile_Error = "";
        #endregion

        /// <summary>
        /// Libera los recursos de sus propiedades
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// Constructor de la clase, recibe como parámetro la direccion IP del servidor o su nombre de red y un puerto de escucha TCP
        /// </summary>
        /// <param name="servidor">Direccion IP o nombre del servidor</param>
        /// <param name="puerto">Puerto TCP Ej: 4114</param>
        /// <param name="stR_userApp">Usuario actual de la aplicacion</param>
        /// <param name="stR_PathLog">Ruta para almacenar el log de la clase</param>
        public ClasX_ClienteTCP(String servidor, int puerto, ClasX_EventLog objR_EventLog)
        {
            try
            {
                objPr_LogEvent = objR_EventLog;
                NBToolsNet.CLNBTN_Lg ObjL_Temp = new NBToolsNet.CLNBTN_Lg(objPr_LogEvent.getUser(), objPr_LogEvent.getPathArchivoLogErr());
                ObjPr_Self = new NBToolsNet.CLNBTN_CliPro(servidor, puerto, ObjL_Temp, stPr_Info);
                //
                stPr_IPServidor = ObjPr_Self.getMyIPAdress();
                IpPr_IPServidor = ObjPr_Self.getIPServer();
                //
                inPr_puerto = puerto;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                   "ClasX_ClienteTCP", "ClasX_ClienteTCP(1)", "", "System.AccessViolationException: " + ex_0, ""
                   , "");
            }
            catch (ArgumentNullException e)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(1)", "", "ArgumentNullException: " + e, ""
                    , "");
            }
            catch (FormatException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(1)", "", "FormatException: " + e, ""
                    , "");
            }
            catch (Exception ex_1)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(1)", "", "Exception: " + ex_1, ""
                    , "");
            }
        }


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servidor">Nombre o IP del Servidor TCP, ejemplo localhost.</param>
        /// <param name="puerto">Puerto del Servidor TCP, ejemplo : 4114</param>
        /// <param name="stR_userApp">Usuario de la aplicacion</param>
        /// <param name="stR_PathLog">Ruta y nombre del archivo de log</param>
        public ClasX_ClienteTCP(String servidor, int puerto, String stR_userApp, String stR_PathLog)
        {
           
            try
            {
                stPr_Usuario = stR_userApp;
                stPr_pathLog = stR_PathLog;
                objPr_LogEvent = new ClasX_EventLog(stPr_Usuario, stPr_pathLog, false, true, false, true);
                //
                ObjPr_Self = new NBToolsNet.CLNBTN_CliPro(servidor, puerto, stR_userApp, stR_PathLog, stPr_Info);
                //
                stPr_IPServidor = ObjPr_Self.getMyIPAdress();
                IpPr_IPServidor = ObjPr_Self.getIPServer();
                //
                inPr_puerto = puerto;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                   "ClasX_ClienteTCP", "ClasX_ClienteTCP(2)", "", "System.AccessViolationException: " + ex_0, ""
                   , "");
            }
            catch (ArgumentNullException e)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(2)", "", "ArgumentNullException: " + e, ""
                    , "");
            }
            catch (FormatException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(2)", "", "FormatException: " + e, ""
                    , "");
            }
            catch (Exception ex_1)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(2)", "", "FormatException: " + ex_1, ""
                    , "");
            }
        }


        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="servidor">Nombre o IP del Servidor TCP, ejemplo localhost.</param>
        /// <param name="puerto">Puerto del Servidor TCP, ejemplo : 4114</param>
        /// <param name="stR_userApp">Usuario de la aplicacion</param>
        /// <param name="stR_PathLog">Ruta y nombre del archivo de log</param>
        /// <param name="st_Usuario_Pandora">Usuario de Pandora, para poder hallar el token</param>
        /// <param name="st_Clave_Usuario_Pandora">Clave del usuario de Pandora, para poder hallar el token</param>
        public ClasX_ClienteTCP(String servidor, int puerto, String stR_userApp, String stR_PathLog, String st_Usuario_Pandora, String st_Clave_Usuario_Pandora)
        {
           
            try
            {
                stPr_Usuario = stR_userApp;
                stPr_pathLog = stR_PathLog;
                //
                stPr_Usuario_Pandora = st_Usuario_Pandora;
                stPr_Clave_Usuario_Pandora = st_Clave_Usuario_Pandora;
                //
                objPr_LogEvent = new ClasX_EventLog(stPr_Usuario, stPr_pathLog, false, true, false, true);
                //
                ObjPr_Self = new NBToolsNet.CLNBTN_CliPro(servidor, puerto, stR_userApp, stR_PathLog, st_Usuario_Pandora, st_Clave_Usuario_Pandora, stPr_Info);
                //
                stPr_IPServidor = ObjPr_Self.getMyIPAdress();
                IpPr_IPServidor = ObjPr_Self.getIPServer();
                //
                inPr_puerto = puerto;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                   "ClasX_ClienteTCP", "ClasX_ClienteTCP(3)", "", "System.AccessViolationException: " + ex_0, ""
                   , "");
            }
            catch (ArgumentNullException e)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(3)", "", "ArgumentNullException: " + e, ""
                    , "");
            }
            catch (FormatException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(3)", "", "FormatException: " + e, ""
                    , "");
            }
            catch (Exception ex_1)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_ClienteTCP", "ClasX_ClienteTCP(3)", "", "Exception: " + ex_1, ""
                    , "");
            }
        }


        /// <summary>
        /// Asigna el valor del usuario actual de la aplicación
        /// </summary>
        /// <param name="Usuario">Usuario actual de la aplicacion</param>
        public void setUser(String Usuario)
        {
            stPr_Usuario = Usuario;
            ObjPr_Self.setUser(stPr_Usuario);
        }



        [HandleProcessCorruptedStateExceptions]
        public String getMyIP()
        {
            String stL_miIP = "";
            try
            {
                stL_miIP = ObjPr_Self.getMyIPAdress();
                //
                return stL_miIP;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "getMyIP. System.AccessViolationExceptio", "", ex_0.Message, "", "");
                return stL_miIP;
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "getMyIP", "", ex.Message, "", "");
                return stL_miIP;
            }
        }


        public String getTrxFile_Error()
        {
            stPr_TrxFile_Error = ObjPr_Self.getst_TrxFile_Error();
            return stPr_TrxFile_Error;
        }


        /// <summary>
        /// Devuelve el valor actual del usuario de la aplicación
        /// </summary>
        /// <returns>Usuario actual registrado en la clase</returns>
        public String getUser()
        {
            stPr_Usuario = ObjPr_Self.getUser();
            return stPr_Usuario;
        }


        /// <summary>
        /// Devuelve el valor actual de la ruta para almacenar el log de la clase
        /// </summary>
        /// <returns>Ruta actual donde se almacena el log de la clase</returns>
        public String getPathLog()
        {
            stPr_pathLog = ObjPr_Self.getFileLog();
            return stPr_pathLog;
        }

        /// <summary>
        /// Devuelve el valor del usuario Pandora
        /// </summary>
        /// <returns>Devuelve el valor de la variable privada : stPr_Usuario_Pandora</returns>
        public String getUsuario_Pandora()
        {
            stPr_Usuario_Pandora = ObjPr_Self.getUser_Monitor();
            return stPr_Usuario_Pandora;
        }

        /// <summary>
        /// Devuelve la clave del usuario de Pandora.
        /// </summary>
        /// <returns>Devuelve el valor de la variable privada : stPr_Clave_Usuario_Pandora</returns>
        public String getClave_Usuario_Pandora()
        {
            stPr_Clave_Usuario_Pandora = ObjPr_Self.getPass_User_Monitor();
            return stPr_Clave_Usuario_Pandora;
        }

        /// <summary>
        /// Devuelve la direccion Ip del Servidor.
        /// </summary>
        /// <returns>Devuelve el valor de la variable privada : stPr_IPServidor</returns>
        public String getst_IPServidor()
        {
            stPr_IPServidor = ObjPr_Self.getMyIPAdress();
            return stPr_IPServidor;
        }

        /// <summary>
        /// Devuelve el puerto del Servidor.
        /// </summary>
        /// <returns>Devuelve el valor de la variable privada : inPr_puerto</returns>
        public int getin_puerto()
        {
            inPr_puerto = ObjPr_Self.getPortNumber();
            return inPr_puerto;
        }

        /// <summary>
        /// Asigna el valor de la ruta para almacenar el log de la clase
        /// </summary>
        /// <param name="path">Ruta para almacenar el log de la clase</param>
        public void setPathLog(String path)
        {
            stPr_pathLog = path;
            ObjPr_Self.setFileLog(stPr_pathLog);
        }

        /// <summary>
        /// Permite cambiar el valor de la variable privada, stPr_Usuario_Pandora, que tiene el usuario de Pandora.
        /// </summary>
        /// <param name="st_Usuario_Pandora">El usuario de pandora.</param>
        public void setUsuario_Pandora(String st_Usuario_Pandora)
        {
            stPr_Usuario_Pandora = st_Usuario_Pandora;
            ObjPr_Self.setUser_Monitor(stPr_Usuario_Pandora);
        }


        /// <summary>
        /// Permite cambiar el valor de la variable privada, stPr_Clave_Usuario_Pandora, que tiene la clave deel usuario de Pandora.
        /// </summary>
        /// <param name="st_Usuario_Pandora">La clave del usuario de pandora.</param>
        public void setClave_Usuario_Pandora(String st_Clave_Usuario_Pandora)
        {
            stPr_Clave_Usuario_Pandora = st_Clave_Usuario_Pandora;
            ObjPr_Self.setPass_User_Monitor(stPr_Clave_Usuario_Pandora);
        }




        [HandleProcessCorruptedStateExceptions]
        public String FENIXInsertar(ClasX_caDatosTemplate objR_datostemplate)
        {
            try
            {
                NBToolsNet.CLNBTN_Dtp ObjL_Temp = new NBToolsNet.CLNBTN_Dtp(stPr_Usuario, stPr_pathLog, stPr_Info);
                //
                ObjL_Temp.setId_Pess(objR_datostemplate.getCedulas());
                ObjL_Temp.setRequest_Type(objR_datostemplate.getTipoPeticion());
                ObjL_Temp.setSizeTempla(objR_datostemplate.getVelTrasnmit());
                //
                NBToolsNet.CLNBTN_Tp[] Tempo_new = new NBToolsNet.CLNBTN_Tp[objR_datostemplate.getTemplate().Length];
                //
                for (int i = 0; i < objR_datostemplate.getTemplate().Length; i++)
                {
                    Tempo_new[i] = new NBToolsNet.CLNBTN_Tp();
                }
                //
                for (int i = 0; i < objR_datostemplate.getTemplate().Length; i++)
                {
                    Tempo_new[i].setInfoTp(objR_datostemplate.getTemplate()[i].getTemplate());
                }
                //
                ObjL_Temp.setTempla(Tempo_new);
                //
                ObjL_Temp.setType(objR_datostemplate.getTipo());
                //
                return ObjPr_Self.CMD2Insert(ObjL_Temp);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "FENIXInsertar. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "FENIXInsertar", "", ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String FENIXVerificar(ClasX_caDatosTemplate objR_datostemplate)
        {
            try
            {
                NBToolsNet.CLNBTN_Dtp ObjL_Temp = new NBToolsNet.CLNBTN_Dtp(stPr_Usuario, stPr_pathLog, stPr_Info);
                //
                ObjL_Temp.setId_Pess(objR_datostemplate.getCedulas());
                ObjL_Temp.setRequest_Type(objR_datostemplate.getTipoPeticion());
                ObjL_Temp.setSizeTempla(objR_datostemplate.getVelTrasnmit());
                //
                NBToolsNet.CLNBTN_Tp[] Tempo_new = new NBToolsNet.CLNBTN_Tp[objR_datostemplate.getTemplate().Length];
                //
                for (int i = 0; i < objR_datostemplate.getTemplate().Length; i++)
                {
                    Tempo_new[i] = new NBToolsNet.CLNBTN_Tp();
                }
                //
                for (int i = 0; i < objR_datostemplate.getTemplate().Length; i++)
                {
                    Tempo_new[i].setInfoTp(objR_datostemplate.getTemplate()[i].getTemplate());
                }
                //
                ObjL_Temp.setTempla(Tempo_new);
                //
                ObjL_Temp.setType(objR_datostemplate.getTipo());
                //
                return ObjPr_Self.CMD2Verify(ObjL_Temp);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "FENIXVerificar. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "FENIXVerificar", "", ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String FENIXIdentificar(ClasX_caDatosTemplate objR_datostemplate)
        {
            try
            {
                NBToolsNet.CLNBTN_Dtp ObjL_Temp = new NBToolsNet.CLNBTN_Dtp(stPr_Usuario, stPr_pathLog, stPr_Info);
                //
                ObjL_Temp.setId_Pess(objR_datostemplate.getCedulas());
                ObjL_Temp.setRequest_Type(objR_datostemplate.getTipoPeticion());
                ObjL_Temp.setSizeTempla(objR_datostemplate.getVelTrasnmit());
                //
                NBToolsNet.CLNBTN_Tp[] Tempo_new = new NBToolsNet.CLNBTN_Tp[objR_datostemplate.getTemplate().Length];
                //
                for (int i = 0; i < objR_datostemplate.getTemplate().Length; i++)
                {
                    Tempo_new[i] = new NBToolsNet.CLNBTN_Tp();
                }
                //
                for (int i = 0; i < objR_datostemplate.getTemplate().Length; i++)
                {
                    Tempo_new[i].setInfoTp(objR_datostemplate.getTemplate()[i].getTemplate());
                }
                //
                ObjL_Temp.setTempla(Tempo_new);
                //
                ObjL_Temp.setType(objR_datostemplate.getTipo());
                //
                return ObjPr_Self.CMD2Identify(ObjL_Temp);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "FENIXIdentificar. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "FENIXIdentificar", "", ex.Message, "", "");
                return "";
            }
        }


       


        /// <summary>
        /// Devuelve un entero que representa la respuesta del servidor
        /// </summary>
        /// <returns></returns>
        public int getRespuestaSalBot()
        {
            inPr_respuestaSolBot = ObjPr_Self.getAnswerBot();
            return inPr_respuestaSolBot;
        }


        [HandleProcessCorruptedStateExceptions]
        public void EnviarBiometria(string stR_NumBio, string stR_rutaBaseServidor)
        {
            try
            {
                ObjPr_Self.SendInfo_Ext(stR_NumBio, stR_rutaBaseServidor);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "ArgumentOutOfRangeException. System.AccessViolationException:" + ex_0, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "ArgumentOutOfRangeException:" + ex, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "DirectoryNotFoundException:" + ex, "", "");
            }
            catch (PathTooLongException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "ArgumentException:" + ex, "", "");
            }
            catch (ArgumentException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "PathTooLongException:" + ex, "", "");
            }
            catch (IOException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "IOException:" + ex, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "UnauthorizedAccessException:" + ex, "", "");
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "Problemas transmitiendo la biometria. Excepción:" + ex, "", "");
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public void EnviarBiometria(string stR_NumBio, string stR_rutaBaseServidor, string stR_rutatemp)
        {
            try
            {
                ObjPr_Self.SendInfo_Ext(stR_NumBio, stR_rutaBaseServidor, stR_rutatemp);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "ArgumentOutOfRangeException. System.AccessViolationException:" + ex_0, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "ArgumentOutOfRangeException:" + ex, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "DirectoryNotFoundException:" + ex, "", "");
            }
            catch (PathTooLongException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "ArgumentException:" + ex, "", "");
            }
            catch (ArgumentException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "PathTooLongException:" + ex, "", "");
            }
            catch (IOException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "IOException:" + ex, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "UnauthorizedAccessException:" + ex, "", "");
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria", "",
                    "Problemas transmitiendo la biometria. Excepción:" + ex, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Método para consultar la disponibilidad del servidor
        /// </summary>
        /// <returns>True si pudo conectarse al servidor</returns>
        public bool testConexion()
        {
            try
            {
                return ObjPr_Self.Connection_OK();
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "testConexion", "",
                    "System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "testConexion", "",
                    "Excepcion: " + ex.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String TraerBiometria(string stR_NumBio, string stR_rutaDestino, string stR_rutaBaseServidor)
        {
            try
            {
                stPr_RespuestaGral = ObjPr_Self.GetInfo_Ext(stR_NumBio, stR_rutaDestino, stR_rutaBaseServidor);
                return stPr_RespuestaGral;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "TraerBiometria", "",
                    "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "TraerBiometria", "", "Excepcion: "
                    + ex.Message, "", "");
                return stPr_RespuestaGral;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void EnviarArchivo(string NombreArchivoOrigen, string NombreArchivoRemoto, int sendBufferSize)
        {
            try
            {
                ObjPr_Self.SendFile(NombreArchivoOrigen,  NombreArchivoRemoto,  sendBufferSize);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "EnviarArchivo", "",
                    "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void RecibirArchivo(string NombreArchivoRemoto, string NombreArchivoLocal)
        {
            try
            {
                ObjPr_Self.RecieveFile(NombreArchivoRemoto, NombreArchivoLocal);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "RecibirArchivo", "",
                    "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public String RecibirArchivo(string NombreArchivoRemoto, string NombreArchivoLocal, bool val)
        {
            try
            {
                return ObjPr_Self.RecieveFile(NombreArchivoRemoto, NombreArchivoLocal, val); ;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "RecibirArchivo(2)", "",
                    "System.AccessViolationException: " + ex_0.Message, "", "");
                return "ERROR";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes", "ClasX_ClienteTCP", "RecibirArchivo(2)", "", ex.Message, "", "");
                return "ERROR";
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public String SADIR(String rutaServidor)
        {
            try
            {
                return ObjPr_Self.CMD2Dir(rutaServidor);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SADIR. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SADIR", "", ex.Message, "", "");
                return "";
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public String SAUnzip(String stR_rutaOrigen, String RutaSalida, String token)
        {
            try
            {
                return ObjPr_Self.CMD2UnCompress(stR_rutaOrigen, RutaSalida, token);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SAUnzip. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SAUnzip", "", ex.Message, "", "");
                return "";
            }
        }


        //
        [HandleProcessCorruptedStateExceptions]
        public String SALogin(String stR_Usuario, String stR_Contraseña)
        {
            try
            {
                return ObjPr_Self.CMD2Login(stR_Usuario, stR_Contraseña);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SALogin. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SALogin", "", ex.Message, "", "");
                return null;
            }
        }


        

        [HandleProcessCorruptedStateExceptions]
        public void EnviarBiometria_Ztr(string stR_NumBio, string stR_rutaBaseServidor)
        {
            try
            {
                ObjPr_Self.SendInfo_Improved( stR_NumBio, stR_rutaBaseServidor);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr", "",
                    "System.AccessViolationException:" + ex_0, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr", "",
                    "ArgumentOutOfRangeException:" + ex, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr", "",
                    "DirectoryNotFoundException:" + ex, "", "");
            }
            catch (PathTooLongException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr", "",
                    "ArgumentException:" + ex, "", "");
            }
            catch (ArgumentException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr", "",
                    "PathTooLongException:" + ex, "", "");
            }
            catch (IOException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr", "",
                    "IOException:" + ex, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr", "",
                    "UnauthorizedAccessException:" + ex, "", "");
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr", "",
                    "Problemas transmitiendo la biometria. Excepción:" + ex, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void EnviarBiometria_Ztr(string stR_NumBio, string stR_rutaBaseServidor, string stR_rutatemp)
        {
            try
            {
                ObjPr_Self.SendInfo_Improved(stR_NumBio, stR_rutaBaseServidor, stR_rutatemp);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr(1)", "",
                    "System.AccessViolationException:" + ex_0, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr(1)", "",
                    "ArgumentOutOfRangeException:" + ex, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr(1)", "",
                    "DirectoryNotFoundException:" + ex, "", "");
            }
            catch (PathTooLongException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr(1)", "",
                    "ArgumentException:" + ex, "", "");
            }
            catch (ArgumentException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr(1)", "",
                    "PathTooLongException:" + ex, "", "");
            }
            catch (IOException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr(1)", "",
                    "IOException:" + ex, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr(1)", "",
                    "UnauthorizedAccessException:" + ex, "", "");
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr(1)", "",
                    "Problemas transmitiendo la biometria. Excepción:" + ex, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void EnviarBiometria_Ztr_Rechazados(string stR_NumBio, string stR_rutaBaseServidor)
        {
            try
            {
                ObjPr_Self.SendInfo_Improved_Rej(stR_NumBio, stR_rutaBaseServidor);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr_Rechazados", "",
                    "System.AccessViolationException:" + ex_0, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr_Rechazados", "",
                    "ArgumentOutOfRangeException:" + ex, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr_Rechazados", "",
                    "DirectoryNotFoundException:" + ex, "", "");
            }
            catch (PathTooLongException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr_Rechazados", "",
                    "ArgumentException:" + ex, "", "");
            }
            catch (ArgumentException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr_Rechazados", "",
                    "PathTooLongException:" + ex, "", "");
            }
            catch (IOException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr_Rechazados", "",
                    "IOException:" + ex, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr_Rechazados", "",
                    "UnauthorizedAccessException:" + ex, "", "");
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "EnviarBiometria_Ztr_Rechazados", "",
                    "Problemas transmitiendo la biometria. Excepción:" + ex, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public String TraerBiometria_Ztr(string stR_NumBio, string stR_rutaDestino, string stR_rutaBaseServidor)
        {
            try
            {
                stPr_RespuestaGral = ObjPr_Self.GetInfo_Improved(stR_NumBio, stR_rutaDestino, stR_rutaBaseServidor);
                return stPr_RespuestaGral;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "TraerBiometria_Ztr", "", "System.AccessViolationException: "
                    + ex_0.Message, "", "");
                //
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "TraerBiometria_Ztr", "", "Excepcion: "
                    + ex.Message, "", "");
                if (stPr_RespuestaGral.Contains("IFX-Get NOEXISTE"))
                {
                    try
                    {
                        if (Directory.Exists("C:\\fnxtemp")) Directory.Delete("C:\\fnxtemp", true);
                        return stPr_RespuestaGral;
                    }
                    catch (IOException e)
                    {
                        objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "TraerBiometria_Ztr", "", "Excepcion: "
                    + e.Message, "", "");
                    }
                }
                return stPr_RespuestaGral;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String Conector_SQL_Recibir_Archivo(String st_Protocolo, ref String stR_ArchivoDatos,  String st_Usuario_Pandora , String st_Clave_Usuario_Pandora , ref Boolean blR_ErrorAccessFile , ref String stR_Resultados_Query, String st_DirectorioTemporal = "")
        { 
            try
            {
                return ObjPr_Self.Recieve_Info_Conn(st_Protocolo, ref  stR_ArchivoDatos, st_Usuario_Pandora, st_Clave_Usuario_Pandora, ref  blR_ErrorAccessFile, ref  stR_Resultados_Query, st_DirectorioTemporal);
            }
            catch (System.AccessViolationException ex_0)
            {
                blR_ErrorAccessFile = true;
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "Conector_SQL_Recibir_Archivo. System.AccessViolationException", "", ex_0.Message, "", "");
                return ex_0.Message;
            }
            catch (Exception ex)
            {
                blR_ErrorAccessFile = true;
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "Conector_SQL_Recibir_Archivo", "", ex.Message, "", "");
                return ex.Message;
            }
        } 



        [HandleProcessCorruptedStateExceptions]
        public String Conector_SQL_Ejecutar_Query(String st_Protocolo, ref long ln_CantidadFilasAfectadas, ref Boolean bl_GeneroError)
        { 
            
            try
            {
                return ObjPr_Self.Execute_Qy_Conn(st_Protocolo, ref  ln_CantidadFilasAfectadas, ref  bl_GeneroError);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "Conector_SQL_Ejecutar_Query. System.AccessViolationException", "", ex_0.Message, "", "");
                bl_GeneroError = true;
                return ex_0.Message;
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "Conector_SQL_Ejecutar_Query", "", ex.Message, "", "");
                bl_GeneroError = true;
                return ex.Message;
            }
        } 



        [HandleProcessCorruptedStateExceptions]
        public String consumirWS(String st_tipo, String st_nombreWS, String st_metodo, String st_arregloValores)
        {
            try
            {
                return ObjPr_Self.WSConsuming(st_tipo,  st_nombreWS,  st_metodo,  st_arregloValores);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "consumirWS. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "consumirWS", "", ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String traerArchivoRespuesta(String st_RutaRespuesta, String st_RutaLocal)
        {
            try
            {
                stPr_RespuestaGral = ObjPr_Self.BringMeAnswerFile(st_RutaRespuesta, st_RutaLocal);
                return stPr_RespuestaGral;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "traerArchivoRespuesta", "", "System.AccessViolationException: "
                    + ex_0.Message, "", "");
                return stPr_RespuestaGral;
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "traerArchivoRespuesta", "", "Excepcion: "
                    + ex.Message, "", "");
                return stPr_RespuestaGral;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void RecibirArchivo_SA_GET(string NombreArchivoRemoto, string NombreArchivoLocal, String st_Usuario_Pandora , String st_Clave_Usuario_Pandora)
        {
            try
            {
                ObjPr_Self.RecieveFile_CMD2Get(NombreArchivoRemoto, NombreArchivoLocal, st_Usuario_Pandora, st_Clave_Usuario_Pandora);
            }
            catch (System.AccessViolationException ex_0)
            {
                throw ex_0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        [HandleProcessCorruptedStateExceptions]
        public void EnviarArchivo_SA_PUT(string NombreArchivoOrigen, string NombreArchivoRemoto, int sendBufferSize, String st_Usuario_Pandora, String st_Clave_Usuario_Pandora)
        {
            try
            {
                ObjPr_Self.SendInfo_CMD2Put( NombreArchivoOrigen,  NombreArchivoRemoto,  sendBufferSize,  st_Usuario_Pandora,  st_Clave_Usuario_Pandora);
            }
            catch (System.AccessViolationException ex_0)
            {
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_Usuario, stPr_pathLog, false, true, false);
                //
                objL_Log.outMensajError("_C_ProgReg.Dll", "ClasX_ClienteTCP", "EnviarArchivo_SA_PUT. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
            }
            catch (Exception)
            {
                throw;
            }
        }




        [HandleProcessCorruptedStateExceptions]
        public String SA_DELETE_FILE_IN_SERVER( String st_File_2_Delete, String st_Usuario_Pandora, String st_Clave_Usuario_Pandora)
        {
            try
            {
                //
                return ObjPr_Self.CMD2DelServerFile(st_File_2_Delete, st_Usuario_Pandora, st_Clave_Usuario_Pandora);
            }
            catch (System.AccessViolationException ex_0)
            {
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_Usuario, stPr_pathLog, false, true, false);
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SA_DELETE_FILE_IN_SERVER. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SA_DELETE_FILE_IN_SERVER", "", ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String SA_OSNAME(String st_Usuario_Pandora, String st_Clave_Usuario_Pandora, String st_Token , ref Boolean blR_Windows_OS)
        {
            try
            {
                return ObjPr_Self.CMD2InfOS(st_Usuario_Pandora, st_Clave_Usuario_Pandora, st_Token , ref blR_Windows_OS);
            }
            catch (System.AccessViolationException ex_0)
            {
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_Usuario, stPr_pathLog, false, true, false);
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SA_OSNAME. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SA_OSNAME", "", ex.Message, "", "");
                return "";
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public String SAUnzip_Ztr(String st_Cedula, String stR_rutaOrigen, String RutaSalida, String st_Usuario_Pandora, String st_Clave_Usuario_Pandora)
        {
            try
            {
                return ObjPr_Self.CMD2UnCompress_Ext(st_Cedula,  stR_rutaOrigen,  RutaSalida,  st_Usuario_Pandora,  st_Clave_Usuario_Pandora);
            }
            catch (System.AccessViolationException ex_0)
            {
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_Usuario, stPr_pathLog, false, true, false);
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SAUnzip_Ztr. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SAUnzip_Ztr", "", ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String SA_KSC(String st_Mensaje, String st_Usuario_Pandora, String st_Clave_Usuario_Pandora)
        {
            try
            {
                return ObjPr_Self.CMD2Ksc(st_Mensaje,  st_Usuario_Pandora,  st_Clave_Usuario_Pandora);
            }
            catch (System.AccessViolationException ex_0)
            {
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_Usuario, stPr_pathLog, false, true, false);
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SA_KSC. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SA_KSC", "", ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String SA_MKDIRS(String st_Dir_A_Crear_En_Servidor, String st_Usuario_Pandora , String st_Clave_Usuario_Pandora , String st_Token_ToView)
        {
            try
            {
                return ObjPr_Self.CMD2MkDir(st_Dir_A_Crear_En_Servidor,  st_Usuario_Pandora ,  st_Clave_Usuario_Pandora ,  st_Token_ToView);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SA_MKDIRS. System.AccessViolationException", "", ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL", "ClasX_ClienteTCP", "SA_MKDIRS", "", ex.Message, "", "");
                return "";
            }
        }



    }
   
}
