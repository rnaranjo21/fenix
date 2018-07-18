using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Timers;
//
using System.Threading;
//
using System.Text.RegularExpressions;
//
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public class CLNBTN_CliPro : IDisposable
    {
        // Clase Equivalante : ClasX_ClienteTCP              
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = false;
        //
        private IPAddress _Ip_IPServer = null;
        private String _st_IPServer = "127.0.0.1";
        private String _st_User = "CLNBTN_Lg";
        private String _st_FileLog = "C:\\Windows\\CLNBTN_CliPro.log";      
        private String _st_GralResponse = "";
        private int _in_PortNumber = 0;
        private int _in_AnswerBot = -9;
        private CLNBTN_Lg _Obj_Log;
        private String _st_Monitor_UID = "";
        private String _st_Monitor_Pass = "";
        private String _st_Lic = "";
        private String _st_TrxFile_Error = "";
        //
        private const int Buffer_Size_To_Trx = 8192;
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_CliPro";

        public void Dispose()
        {

        }

        public CLNBTN_CliPro(String Server, int PortNumber, CLNBTN_Lg Object_Log, String LicName)
        {
            try
            {
                _Obj_Log = Object_Log;
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (!IPAddress.TryParse(Server, out _Ip_IPServer))
                    {
                        IPHostEntry heserver = Dns.GetHostEntry(Server);
                        // Loop on the AddressList
                        foreach (IPAddress curAdd in heserver.AddressList)
                        {
                            if (curAdd.AddressFamily.ToString() == AddressFamily.InterNetwork.ToString()) // IPv4
                            {
                                //-->>_st_IPServer = curAdd.AddressFamily.ToString();
                                _Ip_IPServer = curAdd;
                                break;
                            }
                        }
                        if (_Ip_IPServer == null)
                            throw new Exception(string.Format("{0} not Valid", Server));
                    }
                    _st_IPServer = Server;
                    _in_PortNumber = PortNumber;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(1)", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentNullException e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(1)", "", "ArgumentNullException: " + e.Message, "", "");
            }
            catch (FormatException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(1)", "", "FormatException: " + e.Message, "", "");
            }
            catch (Exception ex_1)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(1)", "", "Exception: " + ex_1.Message, "", "");
            }
        }


        public CLNBTN_CliPro(String Server, int PortNumber, String UserName, String LogFile, String LicName)
        {

            try
            {
                //
                _st_User = UserName;
                _st_FileLog = LogFile;
                _Obj_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, true);
                //
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (!IPAddress.TryParse(Server, out _Ip_IPServer))
                    {
                        IPHostEntry heserver = Dns.GetHostEntry(Server);
                        // Loop on the AddressList
                        foreach (IPAddress curAdd in heserver.AddressList)
                        {
                            if (curAdd.AddressFamily.ToString() == AddressFamily.InterNetwork.ToString()) // IPv4
                            {
                                //-->>_st_IPServer = curAdd.AddressFamily.ToString();
                                _Ip_IPServer = curAdd;
                                break;
                            }
                        }
                        if (_Ip_IPServer == null)
                            throw new Exception(string.Format("{0} Not Valid", Server));
                    }
                    _st_IPServer = Server;
                    _in_PortNumber = PortNumber;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(2)", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentNullException e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(2)", "", "ArgumentNullException: " + e.Message, "", "");
            }
            catch (FormatException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(2)", "", "FormatException: " + e.Message, "", "");
            }
            catch (Exception ex_1)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(2)", "", "Exception: " + ex_1.Message, "", "");
            }
        }


        public CLNBTN_CliPro(String Server, int PortNumber, String UserName, String LogFile, String User_Monitor, String Pass_User_Monitor, String LicName)
        {
            try
            {
                //
                _st_User = UserName;
                _st_FileLog = LogFile;
                //
                _st_Monitor_UID = User_Monitor;
                _st_Monitor_Pass = Pass_User_Monitor;
                //
                _Obj_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, true);
                //
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                 _st_Lic = ObL_Lic.getLicName();
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                     if (!IPAddress.TryParse(Server, out _Ip_IPServer))
                     {
                         IPHostEntry heserver = Dns.GetHostEntry(Server);
                         // Loop on the AddressList
                         foreach (IPAddress curAdd in heserver.AddressList)
                         {
                             if (curAdd.AddressFamily.ToString() == AddressFamily.InterNetwork.ToString()) // IPv4
                             {
                                 //-->>_st_IPServer = curAdd.AddressFamily.ToString();
                                 _Ip_IPServer = curAdd;
                                 break;
                             }
                         }
                         if (_Ip_IPServer == null)
                             throw new Exception(string.Format("{0} Not Valid", Server));
                     }
                     _st_IPServer = Server;
                     _in_PortNumber = PortNumber;
                 }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(3)", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentNullException e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(3)", "", "ArgumentNullException: " + e.Message, "", "");
            }
            catch (FormatException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(3)", "", "FormatException: " + e.Message, "", "");
            }
            catch (Exception ex_1)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CLNBTN_CliPro(3)", "", "Exception: " + ex_1.Message, "", "");
            }
        }

        public String getst_TrxFile_Error()
        {
            return _st_TrxFile_Error;
        }
        

        public String getUser()
        {
            return _st_User;
        }


        public IPAddress getIPServer()
        {
            return _Ip_IPServer;
        }


        [HandleProcessCorruptedStateExceptions]
        public String getMyIPAdress()
        {
            String stL_miIP = "";
            IPHostEntry host;
            try
            {
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        stL_miIP = ip.ToString();
                    }
                }
                return stL_miIP;
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "getMyIPAdress", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return stL_miIP;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "getMyIPAdress", "", "Exception: " + ex.Message, "", "");
                return stL_miIP;
            }
        }

        public int getPortNumber()
        {
            return _in_PortNumber;
        }
        

        public String getFileLog()
        {
            return _st_FileLog;
        }


        public String getUser_Monitor()
        {
            return _st_Monitor_UID;
        }

        public String getPass_User_Monitor()
        {
            return _st_Monitor_Pass;
        }

        public int getAnswerBot()
        {
            return _in_AnswerBot;
        }



        public void setUser(String User)
        {
            _st_User = User;
        }


        public void setFileLog(String FileLog)
        {
            _st_FileLog = FileLog;
        }

        public void setUser_Monitor(String User_Monitor)
        {
            _st_Monitor_UID = User_Monitor;
        }

        public void setPass_User_Monitor(String Pass_User_Monitor)
        {
            _st_Monitor_Pass = Pass_User_Monitor;
        }


        [HandleProcessCorruptedStateExceptions]
        public String CMD2Insert(CLNBTN_Dtp TemplaData)
        {
            try
            {
                return CMD2Request("FENIXInsertar", "FENIX-Insertar", TemplaData);
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Insert", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Insert", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String CMD2Verify(CLNBTN_Dtp TemplaData)
        {
            try
            {
                return CMD2Request("FENIXVerificar", "FENIX-Verificar", TemplaData);
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Verify", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Verify", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public String CMD2Identify(CLNBTN_Dtp TemplaData)
        {
            try
            {
                return CMD2Request("FENIXIdentificar", "FENIX-Identificar", TemplaData);
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Identify", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Identify", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private String CMD2Request(String Funcion, String protocolo, CLNBTN_Dtp TemplaData)
        {
            String stL_entrada = "";
            String stL_nrsalida = "";
            bool blL_envioOK = false;
            //
            Socket socket = null;
            //
            try
            {
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                     return "";
                 }
                 else
                 {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(_Ip_IPServer, _in_PortNumber);
                    //
                    if (socket.Connected)
                    {
                        //-->>_Obj_Log.WriteTextInLog("Conectado con servidor " + _Ip_IPServer.ToString() + ":" + _in_PortNumber.ToString());
                        //ajuste de buffer de recepción de 8k
                        socket.ReceiveBufferSize = Buffer_Size_To_Trx;
                        //Desactivar el Algoritmo de Naglr para el socket
                        socket.NoDelay = true;
                        //ajuste de buffer de envío de 8k
                        socket.SendBufferSize = Buffer_Size_To_Trx;
                        //
                        //-->>_Obj_Log.WriteTextInLog(" socket.ReceiveTimeoutr 30 * 1000");
                        //Establece timeout de recepción (5 Seg)
                        socket.ReceiveTimeout = 5000;
                        //Establece timeout de envío (5 Seg)
                        socket.SendTimeout = 5000;
                        //Establece TTL (Time To Live) a 42 saltos del enrutador
                        socket.Ttl = 42;
                        //
                        //-->>_Obj_Log.WriteTextInLog(" Antes de socket.ReadLine()");
                        try
                        { // Inicio del try
                            //
                            string respuesta = "";
                            respuesta = socket.ReadLine();
                            if (respuesta.Length > 0)
                            { // Inicio del if ( respuesta.Length > 0 ) 
                                //if (socket.Connected)
                                //{ // Inicio del if (socket.Connected)
                                int inL_Bytes_Dispo = socket.Available;
                                if (inL_Bytes_Dispo > 0)
                                {
                                    respuesta = socket.ReadLine();
                                }
                                //
                                //-->>_Obj_Log.WriteTextInLog(" Despues de socket.ReadLine()");
                                //-->>_Obj_Log.WriteTextInLog(" respuesta = " + respuesta);
                                //
                                if (!this.ValidAnswerState_ConnServer(respuesta))
                                {
                                    throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                                }
                                //
                                //-->>_Obj_Log.WriteTextInLog("Respuesta desde el servidor: " + respuesta);
                                //
                                //-->>_Obj_Log.WriteTextInLog(_Ip_IPServer.ToString() + ":" + _in_PortNumber.ToString() + " " + Funcion + "(): SOLICITUD PARA: " + TemplateData.aCadena());
                                //
                                Byte[] datos = Encoding.ASCII.GetBytes(protocolo + " " + TemplaData.IntoString() + "\n");
                                //
                                socket.Send(datos);
                                //} // Fin del if (socket.Connected)

                                // ---------------------------------------------
                                for (int i = 0; i < TemplaData.getNumTempla(); i++)
                                {
                                    _in_AnswerBot = -9;
                                    //stL_entrada = Respuestas();
                                    String respuestaesperada = protocolo + " OK";
                                    String respuestaobtenida = socket.ReadLine();
                                    //_Obj_Log.WriteTextInLog("respuesta desde el servidor: " + respuestaobtenida);
                                    if (respuestaobtenida.Contains(respuestaesperada))
                                    {
                                        //-->>_Obj_Log.WriteTextInLog(_Ip_IPServer.ToString() + ":" + _in_PortNumber.ToString() + " " + Funcion + "(): Servidor aDirector OK");

                                        //String stmp = "";
                                        int len = TemplaData.getTempla()[i].getInfoTp().Length;
                                        socket.Send(TemplaData.getTempla()[i].getInfoTp());
                                        //
                                        //
                                        _Obj_Log.WriteTextInLog(_Ip_IPServer.ToString() + ":" + _in_PortNumber.ToString() + " " + Funcion
                                            + "(): DATOS ENVIADOS :" + TemplaData.getTempla()[i].getInfoTp().Length);
                                        //
                                        stL_entrada = socket.ReadLine();
                                        if (stL_entrada.Contains(protocolo + " TRANSMISIONok"))
                                        {
                                            _Obj_Log.WriteTextInLog("Respuesta desde el servidor " + _Ip_IPServer.ToString() + " " + stL_entrada);
                                            blL_envioOK = true;
                                        }
                                        else
                                        {
                                            _Obj_Log.WriteTextInLog(_st_IPServer + ":" + _in_PortNumber + " " + Funcion
                                                + "(): ERROR Template no se ha enviado al servidor");
                                            //
                                            blL_envioOK = false;
                                        }
                                        //
                                    }
                                }
                            } //  if (respuesta.Length > 0) 
                            if (blL_envioOK)
                            {
                                stL_entrada = socket.ReadLine();
                                if (stL_entrada.Contains(protocolo + " RESPUESTA|"))
                                {
                                    //_Obj_Log.WriteTextInLog("Respuesta a solicitud desde " + _Ip_IPServer.ToString() + " = " + stL_entrada);
                                    stL_nrsalida = stL_entrada;
                                }
                                else
                                {
                                    //_Obj_Log.WriteTextInLog(_st_IPServer + ":" + _in_PortNumber + " " + Funcion + "():"
                                    //    + " ERROR EN EL SERVIDOR :" + stL_entrada);
                                    stL_nrsalida = "";
                                }
                            }
                        } // Fin del Try
                        catch (System.AccessViolationException ex_0)
                        {
                            _Obj_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "CMD2Request" , 
                                "codigo del error", _st_IPServer + ":" + _in_PortNumber + " " + Funcion
                                + "(): SocketException. System.AccessViolationException: " + ex_0.Message, "", "");
                            socket.Close();
                            _in_AnswerBot  = 0;
                            stL_nrsalida = "";
                            return "";
                        }
                        catch (System.IO.IOException ex_3)
                        {
                            _Obj_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "CMD2Request" , 
                                "codigo del error", _st_IPServer + ":" + _in_PortNumber + " " + Funcion
                                + "(): SocketException: " + ex_3.Message, "", "");
                            socket.Close();
                            _in_AnswerBot  = 0;
                            stL_nrsalida = "";
                            return "";
                        }
                        catch (SocketException e)
                        {
                            _Obj_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "CMD2Request" , 
                                "codigo del error", _st_IPServer + ":" + _in_PortNumber + " " + Funcion
                                + "(): SocketException: " + e.Message, "", "");
                            socket.Close();
                            _in_AnswerBot  = 0;
                            stL_nrsalida = "";
                            return "";
                        }
                        catch (Exception ex_1)
                        {
                            _Obj_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "CMD2Request" , 
                                "codigo del error", _st_IPServer + ":" + _in_PortNumber + " " + Funcion
                                + "(): Exception: " + ex_1.Message, "", "");
                            socket.Close();
                            _in_AnswerBot  = 0;
                            stL_nrsalida = "";
                            return "";
                        }
                        /////////////////////////////////////////////
                    }
                    else
                    {
                        return "UnEnabled To Connect";
                    }
                    //
                    socket.Close();
                    //
                    if (stL_nrsalida == "")
                    {
                        _in_AnswerBot = -1;
                        return "";
                    }
                    else
                    {
                        _in_AnswerBot = 1;
                        return stL_nrsalida;
                    }
                }
            }
            //
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "CMD2Request" , 
                    "codigo del error", _st_IPServer + ":" + _in_PortNumber + " " + Funcion
                    + "(): SocketException. System.AccessViolationException: " + ex_0.Message, "", "");
                socket.Close();
                _in_AnswerBot  = 0;
                stL_nrsalida = "";
                return "";
            }
            catch (System.IO.IOException ex_3)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "CMD2Request" , 
                    "codigo del error", _st_IPServer + ":" + _in_PortNumber + " " + Funcion
                    + "(): SocketException: " + ex_3.Message, "", "");
                socket.Close();
                _in_AnswerBot  = 0;
                stL_nrsalida = "";
                return "";
            }
            catch (SocketException e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "CMD2Request" , 
                    "codigo del error", _st_IPServer + ":" + _in_PortNumber + " " + Funcion
                    + "(): SocketException: " + e.Message, "", "");
                socket.Close();
                _in_AnswerBot  = 0;
                stL_nrsalida = "";
                return "";
            }
            catch (Exception ex_1)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "CMD2Request" , 
                    "codigo del error", _st_IPServer + ":" + _in_PortNumber + " " + Funcion
                    + "(): Exception: " + ex_1.Message, "", "");
                socket.Close();
                _in_AnswerBot  = 0;
                stL_nrsalida = "";
                return "";
            }
        }
     


        [HandleProcessCorruptedStateExceptions]
        public Boolean ValidAnswerState_ConnServer(String RespuestaServidor)
        {
            // Valida el estado de la respuiesta de conexion del servidor TCP
            Boolean blL_RespuestaOk = false;
            try
            {
                //
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                    if (RespuestaServidor.Contains("PANDORA-NoSql"))
                    {
                        blL_RespuestaOk = true;
                    }
                    else
                    {
                        if (RespuestaServidor.Contains("IFX-Servidor"))
                        {
                            blL_RespuestaOk = true;
                        }
                    }
                }
                //
                return blL_RespuestaOk;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "ValidAnswerState_ConnServer. System.AccessViolationException", "", ex_0.Message.ToString());
                return false;
            }
            catch (SocketException e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "ValidAnswerState_ConnServer. SocketException", "", e.Message.ToString());
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac ,  _st_RelacSon ,  "ValidAnswerState_ConnServer", "", ex.Message.ToString());
                return false;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void SendInfo_Ext(string NumCed, string PathBaseServer)
        {
            //
            CLNBTN_Fm fm = new CLNBTN_Fm(_st_User, _st_FileLog, _st_Lic);
            String stL_Origen;
            String stL_destino;
            String repo;
            String temp = "C:\\fnxtemp\\" + NumCed + "\\" + NumCed;
            try
            {
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                    int value = NumCed.Length - 2;
                    stL_Origen = "C:\\BioCaptura\\" + NumCed;
                    fm.CopyFiles(stL_Origen, temp, true, _st_Lic);
                    fm.File2Compress("C:\\fnxtemp\\" + NumCed, temp + ".zip", _st_Lic);
                    repo = NumCed.Substring(value, 2);
                    int numrepo = Convert.ToInt32(repo);
                    if (numrepo < 50) repo = "Repositorio1\\" + repo;
                    if (numrepo > 49) repo = "Repositorio2\\" + repo;
                    stL_destino = PathBaseServer + "\\Repositorios\\" + repo + "\\" + NumCed + ".zip";
                    SendFile(temp + ".zip", stL_destino, 512);
                    Directory.Delete("C:\\fnxtemp\\", true);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext", "", "ArgumentOutOfRangeException: " + ex.Message, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext", "", "DirectoryNotFoundException: " + ex.Message, "", "");
            }
            catch (PathTooLongException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext", "", "PathTooLongException: " + ex.Message, "", "");
            }
            catch (ArgumentException ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext", "", "ArgumentException: " + ex.Message, "", "");
            }
            catch (IOException ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext", "", "IOException: " + ex.Message, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext", "", "UnauthorizedAccessException: " + ex.Message, "", "");
            }
            catch (Exception ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext", "", "Exception: " + ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void SendInfo_Ext(string NumCed, string PathBaseServer, string RutaTemp)
        {
            //
            CLNBTN_Fm fm = new CLNBTN_Fm(_st_User, _st_FileLog, _st_Lic);
            String stL_Origen;
            String stL_destino;
            String repo;
            String temp = "C:\\fnxtemp\\" + NumCed + "\\" + NumCed;
            try
            {
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                    int value = NumCed.Length - 2;
                    stL_Origen = RutaTemp + NumCed;
                    fm.CopyFiles(stL_Origen, temp, true, _st_Lic);
                    fm.File2Compress("C:\\fnxtemp\\" + NumCed, temp + ".zip", _st_Lic);
                    repo = NumCed.Substring(value, 2);
                    int numrepo = Convert.ToInt32(repo);
                    if (numrepo < 50) repo = "Repositorio1\\" + repo;
                    if (numrepo > 49) repo = "Repositorio2\\" + repo;
                    stL_destino = PathBaseServer + "\\Repositorios\\" + repo + "\\" + NumCed + ".zip";
                    SendFile(temp + ".zip", stL_destino, 512);
                    Directory.Delete("C:\\fnxtemp\\", true);
                 }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext(2)", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext(2)", "", "ArgumentOutOfRangeException: " + ex.Message, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext(2)", "", "DirectoryNotFoundException: " + ex.Message, "", "");
            }
            catch (PathTooLongException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext(2)", "", "PathTooLongException: " + ex.Message, "", "");
            }
            catch (ArgumentException ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext(2)", "", "ArgumentException: " + ex.Message, "", "");
            }
            catch (IOException ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext(2)", "", "IOException: " + ex.Message, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext(2)", "", "UnauthorizedAccessException: " + ex.Message, "", "");
            }
            catch (Exception ex)
            {
                 _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Ext(2)", "", "Exception: " + ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public bool Connection_OK()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            bool isConnected = false;
            try
            {
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                     return false;
                 }
                 else
                 {
                    socket.Connect(_Ip_IPServer , _in_PortNumber );
                    isConnected = socket.Connected;
                    socket.Disconnect(false);
                    socket.Close();
                    return isConnected;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
               _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Connection_OK", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
               _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Connection_OK", "", "Exception: " + ex.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String GetInfo_Ext(string NumCed, string TargetPath, string PathBaseServer)
        {
            CLNBTN_Fm fm = new CLNBTN_Fm(_st_User, _st_FileLog, _st_Lic);
            String temp = "C:\\fnxtemp\\" + NumCed + ".zip";
            //-->>_Obj_Log.WriteTextInLog("archivo de copia temporal desde la clase CLNBTN_CliPro: " + temp);
            String temcopy = "C:\\fnxtemp\\";
            //-->>_Obj_Log.WriteTextInLog("archivo de descompresion temporal desde la clase CLNBTN_CliPro: " + temcopy);
            String repo;
            String stL_Origen;
            try
            {
                _st_GralResponse = "";
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                    if (!Directory.Exists(temcopy))
                    {
                        Directory.CreateDirectory(temcopy);
                    }
                    //
                    int value = NumCed.Length - 2;
                    repo = NumCed.Substring(value, 2);
                    int numrepo = Convert.ToInt32(repo);
                    if (numrepo < 50) repo = "Repositorio1\\" + repo;
                    if (numrepo > 49) repo = "Repositorio2\\" + repo;
                    //-->>_Obj_Log.WriteTextInLog("Repositorio: " + repo);
                    stL_Origen = PathBaseServer + "\\Repositorios\\" + repo + "\\" + NumCed + ".zip";
                    //-->>_Obj_Log.WriteTextInLog("Ruta origen: " + stL_Origen);
                    //-->>_Obj_Log.WriteTextInLog("entrando en método RecibirArchivo(" + stL_Origen + "," + temp + ")");
                    //
                    RecieveFile(stL_Origen, temp);
                    //
                    fm.File2UnCompress(temp, TargetPath, _st_Lic);
                    //
                    Directory.Delete("C:\\fnxtemp", true);
                    //
                 }
                return _st_GralResponse;
            }
            catch (System.AccessViolationException ex_0)
            {
                //-->>January_25_2015-->_Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetInfo_Ext", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                //-->>January_25_2015-->_Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetInfo_Ext", "", "Exception: " + ex.Message, "", "");
                if (_st_GralResponse.Contains("IFX-Get NOEXISTE"))
                {
                    try
                    {
                        if (Directory.Exists("C:\\fnxtemp")) Directory.Delete("C:\\fnxtemp", true);
                        return _st_GralResponse;
                    }
                    catch (IOException e)
                    {
                        //-->>January_25_2015-->_Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetInfo_Ext", "", "IOException: " + e.Message, "", "");
                        return "";
                    }
                }
                return _st_GralResponse;
            }
        }




        [HandleProcessCorruptedStateExceptions]
        public void SendFile(string NombreArchivoOrigen, string NombreArchivoRemoto, int sendBufferSize)
        {
            long Tamano = 0;
            FileInfo fi = new FileInfo(NombreArchivoOrigen);
            Tamano = fi.Length;
            int fileBufferSize = 512 * 1024; // 512K
            byte[] bufferFile = new byte[fileBufferSize];

            int tamanoLeido = 0;
            int offsetLeido = 0;
            int tamanoEnviado = 0;
            int size = 0;
            int totalSend = 0;

            Socket socket = null;
            try
            {
                _st_TrxFile_Error = "";
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                    FileStream reader = File.Open(NombreArchivoOrigen, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryReader bFile = new BinaryReader(reader);
                    //
                    DateTime t0 = DateTime.Now;
                    //
                    socket = CMD2Put(NombreArchivoRemoto, Tamano, sendBufferSize);
                    //
                    while (totalSend < Tamano)
                    { // Inicio del while (totalSend < Tamano)
                        // Lee todo lo que pueda hasta máximo el tamaño del buffer del lectura física
                        tamanoLeido = bFile.Read(bufferFile, 0, fileBufferSize);
                        //
                        // ahora hay que sacar del buffer y enviar a aDirector en pedazos de sendBufferSize
                        offsetLeido = 0;
                        while (offsetLeido < tamanoLeido - 1)
                        { // Inicio del while (offsetLeido < tamanoLeido - 1)
                            size = tamanoLeido - offsetLeido;
                            if (size > sendBufferSize)
                                size = sendBufferSize;

                            tamanoEnviado = socket.Send(bufferFile, offsetLeido, size, SocketFlags.None);
                            offsetLeido += tamanoEnviado;
                            totalSend += tamanoEnviado;
                        } // Fin del while (offsetLeido < tamanoLeido - 1)
                        ///////////////////////////////////////////////////////////////////
                        /////// Strail-ASQC Enero 20 2.0.14.
                        ///////////////////////////////////////////////////////////////////
                        // Se coloca esta condicion para que salga del Loop principal de transmision
                        // cuando la variable : tamanoLeido es igual a cero. 
                        // Es decir que ya termino de leer el archivo.
                        // Situacion Detectada en el proceso de copia de las biometrias del destanqueo via ADirector.
                        // Implementado en la version de la dll : 1.0.3.18 Enero 30 2.014.
                        if (tamanoLeido == 0)
                        {
                            break;
                        }
                        ///////////////////////////////////////////////////////////////////
                        /////// Fin Strail-ASQC Enero 20 2.0.14.
                        ///////////////////////////////////////////////////////////////////
                    } // Fin del while (totalSend < Tamano)
                    reader.Close();
                    String respuesta = socket.ReadLine();
                    //
                    if (respuesta.Contains("IFX-Put ERROR"))
                    {
                        //throw new Exception(string.Format("Error en la transmision del archivo. Repetir la operacion '{0}'", respuesta));
                        _st_TrxFile_Error = "Error en la transmision del archivo. Repetir la operacion " + respuesta;
                    } 
                 }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendFile", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                // Valida si sokect es diferente de null
                if (socket != null)
                {
                    if (socket.Connected)
                    {
                        socket.Send(ASCIIEncoding.ASCII.GetBytes("IFX-Get TERMENI\n"));
                        socket.Close(3000);
                    }
                }
            }
        }

         //
        [HandleProcessCorruptedStateExceptions]
        private Socket CMD2Put(string NombreArchivoRemoto, long Tamano, int bufferSize)
        {
            try
            {
                _st_TrxFile_Error = "";
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                     return null;
                 }
                 else
                 {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //
                    socket.Connect(_Ip_IPServer , _in_PortNumber );
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; // 2048;
                    socket.SendBufferSize = bufferSize;

                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; // 256;

                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();
                    //
                    //--Console.WriteLine("Respuesta desde el servidor: " + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                    }

                    string msg = string.Format("IFX-Put {0}|{1}|{2}|{3}\n", NombreArchivoRemoto, NombreArchivoRemoto, Tamano, bufferSize);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(msg);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLine();
                    //
                    if (!respuesta.Contains("IFX-Put"))
                    {
                        throw new Exception(string.Format("PUT Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    //
                    //estas 4 lineas se marcan como comentario para prueba 17/01/2013 11:14 am jonathan serna saenz
                    //if (respuesta.ToUpper().Contains("TOKEN NO VALIDO"))
                    //{
                    //    throw new Exception(respuesta);
                    //}
                    //socket.Send(ASCIIEncoding.ASCII.GetBytes("send please\n"));
                    return socket;
                 }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Put", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return null;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Put", "", "Exception: " + ex.Message, "", "");
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void RecieveFile(string RemoteFileName, string LocalFileName)
        {
            //
            int Tamano = 0;
            Socket socket = null;
            //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Creando archivo: " + LocalFileName);
            FileStream writer = File.Open(LocalFileName, FileMode.Create);
            //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + LocalFileName + "creado");
            try
            {
                 if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                 }
                 else
                 {
                    BinaryWriter bFile = new BinaryWriter(writer);
                    int offset = 0;

                    DateTime t0 = DateTime.Now;
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Entrando en método: CMDGet(" + RemoteFileName + ",out Tamano)");
                    //
                    socket = CMD2Get(RemoteFileName, out Tamano);
                    //
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Se obtuvo socket: "+ Convert.ToString(socket));
                    byte[] buffer = new byte[socket.ReceiveBufferSize];
                    //
                    while (offset < (Tamano - 1))
                    {
                        //
                        int tamanoRecibido = socket.Receive(buffer);
                        //
                        bFile.Write(buffer, 0, tamanoRecibido);
                        offset += tamanoRecibido;
                    }
                    writer.Close();
                 }
            }
            catch (System.AccessViolationException ex_0)
            {
               _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "RecieveFile", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                writer.Close();
                throw ex;
            }
            finally
            {
                // Valida si el sokect es diferente de null
                if (socket != null)
                {
                    if (socket.Connected)
                    {
                        //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " RecieveFile. Desconectando socket");
                        socket.Send(ASCIIEncoding.ASCII.GetBytes("IFX-Get TERMENI\n"));
                        socket.Close(3000);
                    }
                }
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String RecieveFile(string RemoteFileName, string LocalFileName, bool val)
        {

            int Tamano = 0;
            Socket socket = null;
            //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Creando archivo: " + LocalFileName);
            FileStream writer = File.Open(LocalFileName, FileMode.Create);
            //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + LocalFileName + "creado");
            try
            {
                if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                    return "";
                 }
                 else
                 {
                    BinaryWriter bFile = new BinaryWriter(writer);
                    int offset = 0;

                    DateTime t0 = DateTime.Now;
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Entrando en método: CMDGet(" + RemoteFileName + ",out Tamano)");
                    //
                    socket = CMD2Get(RemoteFileName, out Tamano);
                    //
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Se obtuvo socket: " + Convert.ToString(socket));
                    byte[] buffer = new byte[socket.ReceiveBufferSize];
                    //
                    while (offset < (Tamano - 1))
                    {
                        //_
                        int tamanoRecibido = socket.Receive(buffer);
                        //
                        bFile.Write(buffer, 0, tamanoRecibido);
                        offset += tamanoRecibido;
                    }
                    writer.Close();
                    return "OK";
                }
            }
            catch (System.AccessViolationException ex_0)
            {
               _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "RecieveFile(2)", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "ERROR";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "RecieveFile", "", "Exception: " + ex.Message, "", "");
                writer.Close();
                return "ERROR";
            }
            finally
            {
                if (socket.Connected)
                {
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Desconectando socket");
                    socket.Send(ASCIIEncoding.ASCII.GetBytes("IFX-Get TERMENI\n"));
                    socket.Close(30);
                }
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private Socket CMD2Get(string RemoteFileName, out int Tamano)
        {
            try
            {
                if (_st_Lic.Length == 0)
                 {
                     MessageBox.Show("Invalid Lic To work");
                     Tamano = 0;
                     return null;
                 }
                 else
                 {
                    Tamano = 0 ;
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    socket.Connect(_Ip_IPServer , _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; //  2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; // 256;

                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();
                    _st_GralResponse = respuesta;
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta desde el servidor=" + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta de conexión no válida: " + respuesta);
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    //
                    string msg = string.Format("IFX-Get {0}\n", RemoteFileName);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(msg);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLine();
                    _st_GralResponse = respuesta;
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta desde el servidor=" + respuesta);

                    if (!(respuesta.Contains("IFX-Get") && respuesta.Contains("|")))
                    {
                        //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " GET Respuesta de conexión no válida: " + respuesta);
                        throw new Exception(string.Format("GET Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    string[] s = respuesta.Split(new char[] { '|' });
                    s[0] = s[0].Substring(8);
                    Tamano = 0;
                    if (!int.TryParse(s[2], out Tamano))
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));

                    socket.Send(ASCIIEncoding.ASCII.GetBytes("send please\n"));
                    //
                    return socket;
                }
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Get", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                Tamano = 0;
                return null;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Get", "", "Exception: " + ex.Message, "", "");
                Tamano = 0;
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String CMD2Dir(String PathInServer)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    socket.Connect(_st_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; // 2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; //  256;
                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta desde el servidor: " + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    string stL_mensaje = string.Format("SA-DIR {0}\n", PathInServer);
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Enviando comando: " + stL_mensaje);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLineD().Replace("\r", "");
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta: " + respuesta);
                    socket.Close();
                    if (!respuesta.Contains("SA-DIR"))
                    {
                        throw new Exception(string.Format("Respuesta no válida '{0}'", respuesta));
                    }
                    string[] stL_s = respuesta.Split(new char[] { '\n' });
                    if (stL_s.Length < 2)
                    {
                        throw new Exception(string.Format("Tamaño de la respuesta no válid0 '{0}'", respuesta));
                    }
                    //if (stL_s[1] != "OK")
                    //{
                    //    string resp = "";
                    //    for (int i = 0; i < stL_s.Length; i++)
                    //    {
                    //        resp += stL_s[i];
                    //    }
                    //    return resp;
                    //}
                    return respuesta;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Dir", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Dir", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }

       
        [HandleProcessCorruptedStateExceptions]
        public String CMD2UnCompress(String stR_rutaOrigen, String RutaSalida, String token)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    socket.Connect(_st_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; // 2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; // 256;
                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta desde el servidor: " + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    string stL_mensaje = string.Format("SA-UNZIP {0}|{1}|{2}\n", stR_rutaOrigen, RutaSalida, token);
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " enviando comando: " + stL_mensaje);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLine().Replace("\r", "");
                    //respuesta = socket.ReadLine();
                    socket.Close();
                    if (!respuesta.Contains("SA-UNZIP"))
                    {
                        throw new Exception(string.Format("Respuesta de login no válida '{0}'", respuesta));
                    }
                    string[] stL_s = respuesta.Split(new char[] { ' ' });
                    if (stL_s.Length < 2)
                    {
                        throw new Exception(string.Format("Tamaño de la respuesta de login no válid0 '{0}'", respuesta));
                    }
                    if (stL_s[1] != "OK")
                    {
                        string resp = "";
                        for (int i = 0; i < stL_s.Length; i++)
                        {
                            resp += stL_s[i];
                        }
                        return resp;
                    }
                    return stL_s[1];
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2UnCompress", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2UnCompress", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }



        //
        [HandleProcessCorruptedStateExceptions]
        public String CMD2Login(String UserName, String UserPass)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    socket.Connect(_Ip_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; //  2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; //  256;

                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta desde el servidor: " + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                    }

                    string stL_mensaje = string.Format("SA-LOGIN {0}|{1}\n", UserName, UserPass);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLine().Replace("\r", "");
                    socket.Close();
                    if (!respuesta.Contains("SA-LOGIN"))
                    {
                        throw new Exception(string.Format("Respuesta de login no válida '{0}'", respuesta));
                    }
                    string[] stL_s = respuesta.Split(new char[] { ' ' });
                    if (stL_s.Length != 2)
                    {
                        throw new Exception(string.Format("Tamaño de la respuesta de login no válid0 '{0}'", respuesta));
                    }
                    if (stL_s[1] == "0")
                    {
                        throw new Exception(string.Format("Login inválido"));
                    }
                    return stL_s[1];
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Login", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Login", "", "System.AccessViolationException: " + ex.Message, "", "");
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void SendInfo_Improved(string NumCed, string ServerPathBase)
        {
            CLNBTN_Fm fm = new CLNBTN_Fm(_st_User, _st_FileLog, _st_Lic);
            String stL_Origen;
            String stL_destino;
            String repo;
            String temp = "C:\\fnxtemp\\" + NumCed + "\\" + NumCed;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                    int value = NumCed.Length - 2;
                    stL_Origen = "C:\\BioCaptura\\" + NumCed;
                    fm.CopyFiles(stL_Origen, temp, true, _st_Lic);
                    fm.File2Compress_Ext("C:\\fnxtemp\\" + NumCed, temp + ".ztr", _st_Lic);
                    repo = NumCed.Substring(value, 2);
                    int numrepo = Convert.ToInt32(repo);
                    if (numrepo < 50) repo = "Repositorio1\\" + repo;
                    if (numrepo > 49) repo = "Repositorio2\\" + repo;
                    stL_destino = ServerPathBase + "\\Repositorios\\" + repo + "\\" + NumCed + ".ztr";
                    SendFile(temp + ".ztr", stL_destino, 512);
                    Directory.Delete("C:\\fnxtemp\\", true);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved", "", "ArgumentOutOfRangeException: " + ex.Message, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved", "", "DirectoryNotFoundException: " + ex.Message, "", "");
            }
            catch (PathTooLongException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved", "", "PathTooLongException: " + ex.Message, "", "");
            }
            catch (ArgumentException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved", "", "ArgumentException: " + ex.Message, "", "");
            }
            catch (IOException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved", "", "IOException: " + ex.Message, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved", "", "UnauthorizedAccessException: " + ex.Message, "", "");
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved", "", "Exception: " + ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void SendInfo_Improved(string NumCed, string ServerPathBase, string PathTemp)
        {
            //
            CLNBTN_Fm fm = new CLNBTN_Fm(_st_User, _st_FileLog, _st_Lic);
            String stL_Origen;
            String stL_destino;
            String repo;
            // Sept 23 2015. Ver 1.0.0.2
            //-->> Sept 23 2015. String temp = "C:\\fnxtemp\\" + NumCed + "\\" + NumCed;
            String temp = "C:\\fnxtemp\\" + NumCed ;
            // Fin Sept 23 2015. Ver 1.0.0.2
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                    int value = NumCed.Length - 2;
                    stL_Origen = PathTemp + "\\" + NumCed;
                    fm.CopyFiles(stL_Origen, temp, true, _st_Lic);
                    fm.File2Compress_Ext("C:\\fnxtemp\\", temp + ".ztr", _st_Lic);
                    repo = NumCed.Substring(value, 2);
                    int numrepo = Convert.ToInt32(repo);
                    if (numrepo < 50) repo = "Repositorio1\\" + repo;
                    if (numrepo > 49) repo = "Repositorio2\\" + repo;
                    stL_destino = ServerPathBase + "\\Repositorios\\" + repo + "\\" + NumCed + ".ztr";
                    SendFile(temp + ".ztr", stL_destino, 512);
                    Directory.Delete("C:\\fnxtemp\\", true);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved(2)", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved(2)", "", "ArgumentOutOfRangeException: " + ex.Message, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved(2)", "", "DirectoryNotFoundException: " + ex.Message, "", "");
            }
            catch (PathTooLongException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved(2)", "", "PathTooLongException: " + ex.Message, "", "");
            }
            catch (ArgumentException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved(2)", "", "ArgumentException: " + ex.Message, "", "");
            }
            catch (IOException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved(2)", "", "IOException: " + ex.Message, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved(2)", "", "UnauthorizedAccessException: " + ex.Message, "", "");
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved(2)", "", "Exception: " + ex.Message, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void SendInfo_Improved_Rej(string NumCed, string ServerPathBase)
        {
            //
            CLNBTN_Fm fm = new CLNBTN_Fm(_st_User, _st_FileLog, _st_Lic);
            String stL_Origen;
            String stL_destino;
            String repo;
            String temp = "C:\\fnxtemp\\" + NumCed + "\\" + NumCed;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                    int value = NumCed.Length - 2;
                    stL_Origen = "C:\\BioCaptura\\" + NumCed;
                    fm.CopyFiles(stL_Origen, temp, true, _st_Lic);
                    fm.File2Compress_Ext("C:\\fnxtemp\\" + NumCed, temp + ".ztr", _st_Lic);
                    repo = NumCed.Substring(value, 2);
                    int numrepo = Convert.ToInt32(repo);
                    if (numrepo < 50) repo = "\\" + repo;
                    if (numrepo > 49) repo = "\\" + repo;
                    //
                    stL_destino = ServerPathBase + repo + "\\" + NumCed + ".ztr";
                    SendFile(temp + ".ztr", stL_destino, 512);
                    Directory.Delete("C:\\fnxtemp\\", true);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved_Rej", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved_Rej", "", "ArgumentOutOfRangeException: " + ex.Message, "", "");
            }
            catch (DirectoryNotFoundException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved_Rej", "", "DirectoryNotFoundException: " + ex.Message, "", "");
            }
            catch (PathTooLongException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved_Rej", "", "PathTooLongException: " + ex.Message, "", "");
            }
            catch (ArgumentException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved_Rej", "", "ArgumentException: " + ex.Message, "", "");
            }
            catch (IOException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved_Rej", "", "IOException: " + ex.Message, "", "");
            }
            catch (UnauthorizedAccessException ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved_Rej", "", "UnauthorizedAccessException: " + ex.Message, "", "");
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_Improved_Rej", "", "Exception: " + ex.Message, "", "");
            }
        }




        [HandleProcessCorruptedStateExceptions]
        public String GetInfo_Improved(string NumCed, string TargetPath, string ServerPathBase)
        {
            CLNBTN_Fm fm = new CLNBTN_Fm(_st_User, _st_FileLog, _st_Lic);
            String temp = "C:\\fnxtemp\\" + NumCed + ".ztr";
            //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + "  Archivo ( Ztr ) de copia temporal desde la clase ClienteTCP: " + temp);
            String temcopy = "C:\\fnxtemp\\";
            //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + "  Archivo ( Ztr ) de descompresion temporal desde la clase ClienteTCP: " + temcopy);
            String repo;
            String stL_Origen;
            try
            {
                _st_GralResponse = "";
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (!Directory.Exists(temcopy))
                    {
                        Directory.CreateDirectory(temcopy);
                    }

                    int value = NumCed.Length - 2;
                    repo = NumCed.Substring(value, 2);
                    int numrepo = Convert.ToInt32(repo);
                    if (numrepo < 50) repo = "Repositorio1\\" + repo;
                    if (numrepo > 49) repo = "Repositorio2\\" + repo;
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + "  Repositorio: " + repo);
                    stL_Origen = ServerPathBase + "\\Repositorios\\" + repo + "\\" + NumCed + ".ztr";
                    //-->>_Obj_Log.WriteTextInLog("Ruta origen: " + stL_Origen);
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + "  entrando en método RecibirArchivo(" + stL_Origen + "," + temp + ")");
                    RecieveFile(stL_Origen, temp);
                    //
                    fm.File2UnCompress_Ext(temp, TargetPath, _st_Lic);
                    //
                    Directory.Delete("C:\\fnxtemp", true);
                    //
                }
                return _st_GralResponse;
            }
            catch (System.AccessViolationException ex_0)
            {
                //-->>January_25_2015-->_Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetInfo_Improved", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                //
                return "";
            }
            catch (Exception ex)
            {
                //-->>January_25_2015-->_Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetInfo_Improved", "", "Exception: " + ex.Message, "", "");
                if (_st_GralResponse.Contains("IFX-Get NOEXISTE"))
                {
                    try
                    {
                        if (Directory.Exists("C:\\fnxtemp")) Directory.Delete("C:\\fnxtemp", true);
                        return _st_GralResponse;
                    }
                    catch (IOException e)
                    {
                        //-->>January_25_2015-->_Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetInfo_Improved", "", "System.AccessViolationException: " + e.Message, "", "");
                    }
                }
                return _st_GralResponse;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String Recieve_Info_Conn(String Info_Proto, ref String DataFile, String MonitorUser, String PassMonitorUser, ref Boolean ErrorAccessFile, ref String QueryResults, String TemporalDir = "")
        { // Inicio de private String Pandora_Conector_SQL
            // Hace la conexion via pandora para la base de datos
            // y se trae el archivo plano con los datos.
            //
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //
            String stL_MensajeSalida = "";
            String stL_DirTemp = "C:\\PandoraTemp\\";
            String stL_Origen = "";
            String stL_FileData = "";
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
                    QueryResults = "";
                    ErrorAccessFile = false;
                    Thread.Sleep(1);
                    //
                    if (TemporalDir.Length == 0)
                    {
                        stL_DirTemp = "C:\\PandoraTemp\\";
                    }
                    else
                    {
                        stL_DirTemp = TemporalDir + "\\";
                    }
                    //
                    DataFile = "";
                    if (!Directory.Exists(stL_DirTemp))
                    {
                        Directory.CreateDirectory(stL_DirTemp);
                    }
                    // /////////////////////////////////
                    // Envia Solicitud
                    // ////////////////////////////////
                    socket.Connect(_Ip_IPServer , _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; //  2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; // 256;
                    socket.ReceiveTimeout = 30 * 1000;
                    string stL_Respuesta = socket.ReadLine();
                    //
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Conector_SQL_Recibir_Archivo. Respuesta desde el servidor: " + stL_Respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(stL_Respuesta))
                    {
                        stL_MensajeSalida = string.Format("Respuesta de conexión no válida '{0}'", stL_Respuesta);
                    }
                    //
                    string stL_Mensaje = string.Format(Info_Proto + "\n");
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + "  Conector_SQL_Recibir_Archivo. Enviando comando: " + stL_Mensaje);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_Mensaje);
                    socket.Send(sendBuffer);
                    stL_Respuesta = socket.ReadLine().Replace("\r", "");
                    //
                    socket.Close();
                    if (!stL_Respuesta.Contains("CNTR|SQL"))
                    {
                        stL_MensajeSalida = string.Format("Respuesta de ConectorSQL no válida '{0}'", stL_Respuesta);
                    }
                    //
                    string[] stL_s = stL_Respuesta.Split(new char[] { '|' });
                    // El Arreglo cuando hay error qued asi:
                    //  [0]: "CNTR"
                    //  [1]: "SQL"
                    //  [2]: "TRNX:"
                    //  [3]: "ERROR: Ejecutando : SELECT * FROM ad_rol Exception : java.lang.NullPointerException  .ERROR: ClassNotFoundException ClasX_DBQuery.getConnection, conexion SqlServer, java.lang.ClassNotFoundException: "
                    //
                    if (stL_s[3].Contains("ERROR:"))
                    { // inicio del if (stL_s[3].Contains("ERROR:"))
                        stL_MensajeSalida = stL_s[3];
                    } // Fin de if (stL_s[3].Contains("ERROR:"))
                    else // del if (stL_s[3].Contains("ERROR:"))
                    { // Inicio del else de if (stL_s[3].Contains("ERROR:"))
                        // Cuando el servidor responde ok el arreglo queda:
                        // [1]: "SQL"
                        // [2]: "TRNX:"
                        // [3]: "OK: 18 Fila(s) Seleccionada(s)."
                        // [4]: "Archivo Generado :"
                        // [5]: "C:\\\\Temporal\\QueryOutData--11-201444.txt"
                        // Tambien puede responder asi:
                        // [0]: "CNTR"
                        // [1]: "SQL"
                        // [2]: "TRNX:9005373789"
                        // [3]: "OK: 1 Fila(s) Seleccionada(s)."
                        // [4]: "Datos en Protocolo :"
                        // [5]: "count<EOL>3<EOL>"
                        // En la posicion 4 : Indica que los datos vienen en el protocolo
                        // En la posicion 5 : Vienen los datos Cada registro separado por : <EOL>
                        // Esto indica <EOL> es : END OF LINE
                        Boolean blL_Datos_En_Protocolo = false;
                        //
                        blL_Datos_En_Protocolo = (stL_s[4] == "Datos en Protocolo :");
                        // Los datos vienen en el protocolo
                        // Y los devuelve en la variable : 
                        // QueryResults
                        // A la variable : 
                        // DataFile, le coloca cualquier valor
                        // para que en el metodo de la clase, que lo llama se pase la validacion.
                        if (blL_Datos_En_Protocolo)
                        { // Inicio del if (blL_Datos_En_Protocolo)
                            DataFile = "12345";
                            QueryResults = stL_s[5];
                        } // Fin del if (blL_Datos_En_Protocolo)
                        else // del if (blL_Datos_En_Protocolo)
                        { // Inicio del ELSE del if (blL_Datos_En_Protocolo)
                            // Toma el nombre del archivo que tiene los datos.
                            stL_Origen = stL_s[5];
                            //
                            String stL_Ano = DateTime.Now.Year.ToString(); // stL_FechaHora.Substring(6, 4);
                            String stL_Mes = DateTime.Now.Month.ToString(); // stL_FechaHora.Substring(3, 2);
                            String stL_Dia = DateTime.Now.Day.ToString(); //stL_FechaHora.Substring(0, 2);
                            //
                            if ((Convert.ToInt32(stL_Mes) <= 9) & (stL_Mes.Length == 1))
                            {
                                stL_Mes = "0" + stL_Mes;
                            }
                            if ((Convert.ToInt32(stL_Dia) <= 9) & (stL_Dia.Length == 1))
                            {
                                stL_Dia = "0" + stL_Dia;
                            }
                            //
                            String stL_FechaAux = stL_Ano + stL_Mes + stL_Dia;
                            stL_FechaAux = stL_FechaAux.Trim();
                            //
                            int inL_ConseFile = 1;
                            stL_FileData = stL_DirTemp + "\\QueryRes-" + inL_ConseFile.ToString() + "-" + stL_FechaAux + ".txt";
                            // ASigna el nombre del archivo temporal donde se van a copiar los datos.
                            if (this.GiveMeFileNameQyRsls(stL_DirTemp, stL_FechaAux, ref stL_FileData, ref inL_ConseFile) != true)
                            {
                                // Si hay error en la generacion entra en un LOOP hasta que se genere correctamente el nombre.
                                while (this.GiveMeFileNameQyRsls(stL_DirTemp, stL_FechaAux, ref stL_FileData, ref inL_ConseFile))
                                {
                                    //
                                    break;
                                }
                            }
                            //while (File.Exists(stL_FileData))
                            //{
                            //    inL_ConseFile++;
                            //    stL_FileData = stL_DirTemp + "\\QueryRes-" + inL_ConseFile.ToString() + "-" + stL_FechaAux + ".txt";
                            //}
                            //
                            // //////////////////////////////////////////////////////
                            // Recibe el Archivo via Pandora 
                            // //////////////////////////////////////////////////////
                            this.RecieveFile_CMD2Get(stL_Origen, stL_FileData, MonitorUser, PassMonitorUser);
                            // //////////////////////////////////////////////////////
                            //
                            if (File.Exists(stL_FileData))
                            {
                                DataFile = stL_FileData;
                            }
                            // //////////////////////////////
                            // Elimina el archivo en el servidor 
                            // //////////////////////////////
                            String stL_Respuesta_DelFile = this.CMD2DelServerFile(stL_Origen, MonitorUser, PassMonitorUser);
                            // 
                        } // Fin del ELSE en if (blL_Datos_En_Protocolo)
                    } // Fin del else del if (stL_s[3].Contains("ERROR:"))
                    //
                }
                return stL_MensajeSalida;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ErrorAccessFile = true;
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Recieve_Info_Conn", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return ex_0.Message;
            }
            catch (Exception ex)
            {
                ErrorAccessFile = true;
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Recieve_Info_Conn", "", "Exception: " + ex.Message, "", "");
                return ex.Message;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String Execute_Qy_Conn(String Info_Proto, ref long RowsAffected, ref Boolean ErrorGenerated)
        { // 
            // y se trae el archivo plano con los datos.
            //
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //
            String stL_MensajeSalida = "";
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
                    RowsAffected = 0;
                    ErrorGenerated = false;
                    // /////////////////////////////////
                    // Envia Solicitud
                    // ////////////////////////////////
                    socket.Connect(_st_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; // 2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; // 256;
                    socket.ReceiveTimeout = 30 * 1000;
                    string stL_Respuesta = socket.ReadLine();
                    //
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Conector_SQL_Ejecutar_Query. Respuesta desde el servidor: " + stL_Respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(stL_Respuesta))
                    {
                        stL_MensajeSalida = string.Format("Respuesta de conexión no válida '{0}'", stL_Respuesta);
                        ErrorGenerated = true;
                    }
                    //
                    string stL_Mensaje = string.Format(Info_Proto + "\n");
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Conector_SQL_Ejecutar_Query. Enviando comando: " + stL_Mensaje);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_Mensaje);
                    socket.Send(sendBuffer);
                    stL_Respuesta = socket.ReadLine().Replace("\r", "");
                    //
                    socket.Close();
                    if (!stL_Respuesta.Contains("CNTR|SQL"))
                    {
                        stL_MensajeSalida = string.Format("Respuesta de ConectorSQL no válida '{0}'", stL_Respuesta);
                    }
                    //
                    string[] stL_s = stL_Respuesta.Split(new char[] { '|' });
                    // El Arreglo cuando hay error queda asi:
                    // [0]: "CNTR"
                    // [1]: "SQL"
                    // [2]: "TRNX:"
                    // [3]: "ERROR: Ejecutando : INSERT INTO ad_rol(ro_rol , ro_descripcion , ro_creador) VALUES ( '12'  , 'rol 12'  , '0' ) Exception : com.microsoft.sqlserver.jdbc.SQLServerException: Cannot insert the value NULL into column 'ro_fecha_crea', table 'administracion.dbo.ad_rol'; column does not allow nulls. INSERT fails. "
                    //
                    if (stL_s[3].Contains("ERROR:"))
                    { // inicio del if (stL_s[3].Contains("ERROR:"))
                        stL_MensajeSalida = stL_s[3];
                        ErrorGenerated = true;
                    } // Fin de if (stL_s[3].Contains("ERROR:"))
                    else // del if (stL_s[3].Contains("ERROR:"))
                    { // Inicio del else de if (stL_s[3].Contains("ERROR:"))
                        // Cuando el servidor responde ok el arreglo queda:
                        //  [0]: "CNTR"
                        //  [1]: "SQL"
                        //  [2]: "TRNX:"
                        //  [3]: "OK: 1 Fila(s) Afectada(s)."
                        // [4]: "1"
                        ////
                        // Devuelve el numero de filas afecatadas
                        ////
                        String stL_Cantidad = stL_s[4];
                        //
                        if (stL_Cantidad.Length == 0)
                        {
                            stL_Cantidad = "0";
                        }
                        else
                        {
                            // Valida si es numerico
                            long lnL_number1 = 0;
                            bool canConvert = long.TryParse(stL_Cantidad, out lnL_number1);
                            if (!canConvert)
                            {
                                stL_Cantidad = "0";
                            }
                        }
                        //
                        RowsAffected = Convert.ToInt64(stL_Cantidad);
                        //
                    } // Fin del else del if (stL_s[3].Contains("ERROR:"))
                    //
                }
                return stL_MensajeSalida;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Execute_Qy_Conn", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                ErrorGenerated = true;
                return ex_0.Message;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Execute_Qy_Conn", "", "Exception: " + ex.Message, "", "");
                ErrorGenerated = true;
                return ex.Message;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String WSConsuming(String WSType, String WSName, String WSMethod, String WSValArray)
        {
            String stL_Respuesta = "";
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    socket.Connect(_Ip_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; //  2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; //  256;
                    socket.ReceiveTimeout = 30 * 1000;
                    stL_Respuesta = socket.ReadLine();
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta desde el servidor: " + stL_Respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(stL_Respuesta))
                    {
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", stL_Respuesta));
                    }
                    //
                    string stL_mensaje = string.Format("CNTR|WS|{0}|{1}|{2}|{3}\n", WSType, WSName, WSMethod, WSValArray);
                    //
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " enviando comando: " + stL_mensaje);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                    socket.Send(sendBuffer);
                    //
                    stL_Respuesta = socket.ReadLineD().Replace("\r", "");
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta: " + stL_Respuesta);
                    socket.Close();
                    //
                    if (!stL_Respuesta.Contains("CNTR|WS"))
                    {
                        throw new Exception(string.Format("Respuesta no válida '{0}'", stL_Respuesta));
                    }
                    //
                    string[] stL_s = stL_Respuesta.Split(new char[] { '|' });
                    if (stL_s[3].Contains("ERROR:"))
                    {
                        stL_Respuesta = stL_s[3];
                    }
                    else
                    {
                        stL_Respuesta = stL_s[4].Remove(0, 10);
                    }
                    //
                    // Al nombre del archivo le elimina los "\n", que vienen y hacen que se generen errores
                    // en las operaciones subsiguientes.
                    stL_Respuesta = stL_Respuesta.Replace("\n", "");
                    //
                }
                return stL_Respuesta;
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "WSConsuming", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "WSConsuming", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMeAnswerFile(String ResponsePath, String LocalPath)
        {
            //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Archivo de copia local desde la clase ClienteTCP: " + LocalPath);
            try
            {
                _st_GralResponse = "";
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    String stL_Rutanueva = "";
                    if (!Directory.Exists(LocalPath))
                    {
                        String[] ruta = LocalPath.Split(new char[] { '\\' });
                        foreach (string parte in ruta)
                        {
                            if (parte.Contains(".xml"))
                            {
                                continue;
                            }
                            if (stL_Rutanueva.Equals(""))
                            {
                                stL_Rutanueva = parte;
                            }
                            else
                            {
                                stL_Rutanueva += "\\" + parte;
                            }
                        }
                        Directory.CreateDirectory(stL_Rutanueva);
                    }
                    //
                }
                this.RecieveFile(ResponsePath, LocalPath);
                //
                return _st_GralResponse;
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMeAnswerFile", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return _st_GralResponse;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMeAnswerFile", "", "Exception: " + ex.Message, "", "");
                return _st_GralResponse;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void RecieveFile_CMD2Get(string RemoteFile, string LocalFile, String Monitor_UID, String Monitor_Pass)
        {
            // ///////////////////////////////////////////////////////////
            // Recibe el archivo con el comando SA-GET
            // Llama el metodo de la clase SA_GET, para recibir el archivo 
            // ///////////////////////////////////////////////////////////
            //
            int Tamano = 0;
            Socket socket = null;
            //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Creando archivo: " + LocalFile);
            FileStream writer = File.Open(LocalFile, FileMode.Create);
            //-->>_Obj_Log.WriteTextInLog(LocalFile + "creado");
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    BinaryWriter bFile = new BinaryWriter(writer);
                    int offset = 0;
                    //
                    DateTime t0 = DateTime.Now;
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Entrando en método: SAGET(" + RemoteFile + ",out Tamano)");
                    // /////////////////////////////////////
                    // Llama el metodo que trae el archivo desde el servidor.
                    // ////////////////////////////////////
                    //
                    // Hace El get.
                    socket = this.CMD2Get_Ext(RemoteFile, out Tamano, Monitor_UID, Monitor_Pass);
                    //
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Se obtuvo socket: " + Convert.ToString(socket));
                    byte[] buffer = new byte[socket.ReceiveBufferSize];
                    //
                    while (offset < (Tamano - 1))
                    {
                        //
                        int tamanoRecibido = socket.Receive(buffer);
                        //
                        bFile.Write(buffer, 0, tamanoRecibido);
                        offset += tamanoRecibido;
                    }
                    writer.Close();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                writer.Close();
                throw ex_0;
            }
            catch (Exception ex)
            {
                writer.Close();
                throw ex;
            }
            finally
            {
                // Valida si el sokect es diferente de null
                if (socket != null)
                {
                    if (socket.Connected)
                    {
                        //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Desconectando socket");
                        socket.Send(ASCIIEncoding.ASCII.GetBytes("SA-GET TERMINE\n"));
                        socket.Close(30);
                    }
                }
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private Socket CMD2Get_Ext(string RemoteFile, out int FileSize, String Monitor_UID, String Monitor_Pass)
        {
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    FileSize = 0;
                    return null;
                }
                else
                {
                    FileSize = 0;
                    // /////////////////////////////////////////////////
                    // Llama el metodo, SA-GET, para recibir un archivo.
                    // Halla el TOKEN.
                    // /////////////////////////////////////////////////
                    String stL_Usu_Pandora = Monitor_UID;
                    String stL_Clave_Usu_Pandora = Monitor_Pass;
                    //
                    // Si el parametro del usuario esta vacio, toma el que tiene definida la clase.
                    if (stL_Usu_Pandora.Length == 0)
                    {
                        stL_Usu_Pandora = this._st_Monitor_UID;
                    }
                    // Si el parametro de la clave del usuario, toma el que tiene definida la clase.
                    if (stL_Clave_Usu_Pandora.Length == 0)
                    {
                        stL_Clave_Usu_Pandora = this._st_Monitor_Pass;
                    }
                    // Halla el Token
                    String stL_Token = this.CMD2Login(stL_Usu_Pandora, stL_Clave_Usu_Pandora);
                    //
                    // Implementa el Comando SA-GET, en el servidor TCP
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //
                    socket.Connect(_Ip_IPServer , _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx; // 2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx; //  256;
                    //
                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();
                    _st_GralResponse = respuesta;
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta desde el servidor=" + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta de conexión no válida: " + respuesta);
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    //
                    string msg = string.Format("SA-GET {0}\n", RemoteFile + "|" + stL_Token); // IFX-Get c:\\alalalal.txt
                    //SA-GET c:jkhdkjhshf|token
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(msg);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLine();
                    _st_GralResponse = respuesta;
                    //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " Respuesta desde el servidor=" + respuesta);
                    //
                    if (!(respuesta.Contains("SA-GET") && respuesta.Contains("|")))
                    {
                        //-->>_Obj_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " SA-GET Respuesta de conexión no válida: " + respuesta);
                        throw new Exception(string.Format("SA-GET Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    string[] s = respuesta.Split(new char[] { '|' });
                    s[0] = s[0].Substring(8);
                    FileSize = 0;
                    if (!int.TryParse(s[2], out FileSize))
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));

                    socket.Send(ASCIIEncoding.ASCII.GetBytes("send please\n"));
                    return socket;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Get_Ext", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                FileSize = 0;
                return null;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Get_Ext", "", "Exception: " + ex.Message, "", "");
                FileSize = 0;
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void SendInfo_CMD2Put(string SourceFileName, string RemoteFileName, int sendBufferSize, String Monitor_UID, String Monitor_Pass)
        {
            long Tamano = 0;
            FileInfo fi = new FileInfo(SourceFileName);
            Tamano = fi.Length;
            //-->>int fileBufferSize = 512 * 1024; // 512K
            int fileBufferSize = Buffer_Size_To_Trx;
            byte[] bufferFile = new byte[fileBufferSize];

            int tamanoLeido = 0;
            int offsetLeido = 0;
            int tamanoEnviado = 0;
            int size = 0;
            int totalSend = 0;
            //
            Socket socket = null;
            try
            {
                _st_TrxFile_Error = "";
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    FileStream reader = File.Open(SourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryReader bFile = new BinaryReader(reader);
                    //
                    DateTime t0 = DateTime.Now;
                    //
                    sendBufferSize = Buffer_Size_To_Trx;
                    socket = this.CMD2Put_Ext(RemoteFileName, Tamano, sendBufferSize, Monitor_UID, Monitor_Pass);
                    //
                    while (totalSend < Tamano)
                    { // Inicio del while (totalSend < Tamano)
                        // Lee todo lo que pueda hasta máximo el tamaño del buffer del lectura física
                        tamanoLeido = bFile.Read(bufferFile, 0, fileBufferSize);
                        //
                        // ahora hay que sacar del buffer y enviar a aDirector en pedazos de sendBufferSize
                        offsetLeido = 0;
                        while (offsetLeido < tamanoLeido - 1)
                        { // Inicio del while (offsetLeido < tamanoLeido - 1)
                            size = tamanoLeido - offsetLeido;
                            if (size > sendBufferSize)
                                size = sendBufferSize;

                            tamanoEnviado = socket.Send(bufferFile, offsetLeido, size, SocketFlags.None);
                            offsetLeido += tamanoEnviado;
                            totalSend += tamanoEnviado;
                        } // Fin del while (offsetLeido < tamanoLeido - 1)
                        // Se coloca esta condicion para que salga del Loop principal de transmision
                        // cuando la variable : tamanoLeido es igual a cero. 
                        // Es decir que ya termino de leer el archivo.
                        // Situacion Detectada en el proceso de copia de las biometrias del destanqueo via ADirector.
                        // Implementado en la version de la dll : 1.0.3.18 Enero 30 2.014.
                        if (tamanoLeido == 0)
                        {
                            break;
                        }
                    } // Fin del while (totalSend < Tamano)
                    reader.Close();
                    String respuesta = socket.ReadLine();
                    //
                    if (respuesta.Contains("IFX-Put ERROR") || respuesta.Contains("SA-PUT ERROR"))
                    {
                        //throw new Exception(string.Format("Error en la transmision del archivo. Repetir la operacion '{0}'", respuesta));
                        _st_TrxFile_Error = "Error en la transmision del archivo. Repetir la operacion " + respuesta;
                    } 
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SendInfo_CMD2Put", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                //
               
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                // Valida si sokect es diferente de null
                if (socket != null)
                {
                    if (socket.Connected)
                    {
                        socket.Send(ASCIIEncoding.ASCII.GetBytes("SA-PUT TERMINE\n"));
                        socket.Close(3000);
                    }
                }
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private Socket CMD2Get_Ext(string RemoteFileName, long Trx_Size, int sendBufferSize, String Monitor_UID, String Monitor_Pass)
        {
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return null;
                }
                else
                {
                    // Ejecuta el comando SA-PUT
                    // Halla el Token para poderlo ejecutar.
                    String stL_Usu_Pandora = Monitor_UID;
                    String stL_Clave_Usu_Pandora = Monitor_Pass;
                    //
                    // Si el parametro del usuario esta vacio, toma el que tiene definida la clase.
                    if (stL_Usu_Pandora.Length == 0)
                    {
                        stL_Usu_Pandora = this._st_Monitor_UID;
                    }
                    // Si el parametro de la clave del usuario, toma el que tiene definida la clase.
                    if (stL_Clave_Usu_Pandora.Length == 0)
                    {
                        stL_Clave_Usu_Pandora = this._st_Monitor_Pass;
                    }
                    // Halla el Token
                    String stL_Token = this.CMD2Login(stL_Usu_Pandora, stL_Clave_Usu_Pandora);
                    //
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    socket.Connect(_Ip_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx;
                    //-->>socket.ReceiveBufferSize = 2048;

                    socket.SendBufferSize = sendBufferSize;

                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx;
                    //socket.SendBufferSize = 256;

                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();

                    Console.WriteLine("Respuesta desde el servidor: " + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    // Envia el Token, al final
                    string msg = string.Format("SA-PUT {0}|{1}|{2}|{3}|{4}\n", RemoteFileName, RemoteFileName, Trx_Size, sendBufferSize, stL_Token);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(msg);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLine();
                    //
                    if (!respuesta.Contains("SA-PUT"))
                    {
                        throw new Exception(string.Format("SA-PUT Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    //estas 4 lineas se marcan como comentario para prueba 17/01/2013 11:14 am jonathan serna saenz
                    //if (respuesta.ToUpper().Contains("TOKEN NO VALIDO"))
                    //{
                    //    throw new Exception(respuesta);
                    //}
                    //socket.Send(ASCIIEncoding.ASCII.GetBytes("send please\n"));
                    return socket;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Get_Ext", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return null;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Get_Ext", "", "Exception: " + ex.Message, "", "");
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String CMD2DelServerFile(String File_2_Off, String Monitor_UID, String Monitor_Pass)
        {
            // Elimina un archivo en el servidor. Ejecuta SA-DELETE, dependiendo del sistema operativo.
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    // Halla el Token para poderlo ejecutar.
                    String stL_Usu_Pandora = Monitor_UID;
                    String stL_Clave_Usu_Pandora = Monitor_Pass;
                    //
                    // Si el parametro del usuario esta vacio, toma el que tiene definida la clase.
                    if (stL_Usu_Pandora.Length == 0)
                    {
                        stL_Usu_Pandora = this._st_Monitor_UID;
                    }
                    // Si el parametro de la clave del usuario, toma el que tiene definida la clase.
                    if (stL_Clave_Usu_Pandora.Length == 0)
                    {
                        stL_Clave_Usu_Pandora = this._st_Monitor_Pass;
                    }
                    // Halla el Token
                    String stL_Token = this.CMD2Login(stL_Usu_Pandora, stL_Clave_Usu_Pandora);
                    //
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(_st_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx;
                    //socket.ReceiveBufferSize = 2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx;
                    //socket.SendBufferSize = 256;
                    socket.ReceiveTimeout = 30 * 1000;
                    string stL_Respuesta = socket.ReadLine();
                    //-->>_Obj_Log.WriteTextInLog("Respuesta desde el servidor: " + stL_Respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(stL_Respuesta))
                    {
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", stL_Respuesta));
                    }
                    //
                    string stL_mensaje = string.Format("SA-DELETE {0}|{1}\n", File_2_Off, stL_Token);
                    //-->>_Obj_Log.WriteTextInLog("enviando comando: " + stL_mensaje);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                    socket.Send(sendBuffer);
                    stL_Respuesta = socket.ReadLineD().Replace("\r", "");
                    //-->>_Obj_Log.WriteTextInLog("Respuesta: " + stL_Respuesta);
                    socket.Close();
                    if (!stL_Respuesta.Contains("SA-DELETE"))
                    {
                        throw new Exception(string.Format("Respuesta no válida '{0}'", stL_Respuesta));
                    }
                    string[] stL_s = stL_Respuesta.Split(new char[] { '\n' });
                    if (stL_s.Length < 2)
                    {
                        throw new Exception(string.Format("Tamaño de la respuesta no válid0 '{0}'", stL_Respuesta));
                    }
                    return stL_Respuesta;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2DelServerFile", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2DelServerFile", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String CMD2InfOS(String Monitor_UID, String Monitor_Pass, String TokenInf, ref Boolean Is_Win_OS)
        {
            // Ejecuta el comando SA-OSNAME
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    String stL_Token = "";
                    Is_Win_OS = false;
                    // Halla el Token para poderlo ejecutar.
                    String stL_Usu_Pandora = Monitor_UID;
                    String stL_Clave_Usu_Pandora = Monitor_Pass;
                    //
                    // Si el parametro del usuario esta vacio, toma el que tiene definida la clase.
                    if (stL_Usu_Pandora.Length == 0)
                    {
                        stL_Usu_Pandora = this._st_Monitor_UID;
                    }
                    // Si el parametro de la clave del usuario, toma el que tiene definida la clase.
                    if (stL_Clave_Usu_Pandora.Length == 0)
                    {
                        stL_Clave_Usu_Pandora = this._st_Monitor_Pass;
                    }
                    // ----------------------------------------
                    // Halla el Token, si el token como parametro viene vacio.
                    // ----------------------------------------
                    if (TokenInf.Length == 0)
                    {
                        stL_Token = this.CMD2Login(stL_Usu_Pandora, stL_Clave_Usu_Pandora);
                    }
                    else
                    {
                        stL_Token = TokenInf;
                    }
                    // ----------------------------------------
                    //
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(_Ip_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx;
                    //socket.ReceiveBufferSize = 2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx;
                    //socket.SendBufferSize = 256;
                    socket.ReceiveTimeout = 30 * 1000;
                    string stL_Respuesta = socket.ReadLine();
                    //-->>_Obj_Log.WriteTextInLog("Respuesta desde el servidor: " + stL_Respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(stL_Respuesta))
                    {
                        throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", stL_Respuesta));
                    }
                    //
                    string stL_mensaje = string.Format("SA-OSNAME {0}\n", stL_Token);
                    //-->>_Obj_Log.WriteTextInLog("enviando comando: " + stL_mensaje);
                    //
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                    socket.Send(sendBuffer);
                    stL_Respuesta = socket.ReadLineD().Replace("\r", "");
                    //-->>_Obj_Log.WriteTextInLog("Respuesta: " + stL_Respuesta);
                    socket.Close();
                    //if (!stL_Respuesta.Contains("SA-OSNAME"))
                    //{
                    //    throw new Exception(string.Format("Respuesta no válida '{0}'", stL_Respuesta));
                    //}
                    string[] stL_s = stL_Respuesta.Split(new char[] { '\n' });
                    if (stL_s.Length < 2)
                    {
                        throw new Exception(string.Format("Tamaño de la respuesta no válid0 '{0}'", stL_Respuesta));
                    }
                    if (stL_Respuesta.ToLower().Contains("windows"))
                    {
                        Is_Win_OS = true;
                    }
                    return stL_Respuesta;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2InfOS", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2InfOS", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private Boolean GiveMeFileNameQyRsls(String DirNameTemp, String DateInf, ref String DataFile, ref int FileRec)
        {
            // Halla el nombre del archivo donde se guarda informacion del resultado de los queries
            // Cuando se trabaja con el conector SQl de Pandora.
            Boolean blL_FileOk = true;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    DataFile = DirNameTemp + "\\QueryRes-" + FileRec.ToString() + "-" + DateInf + ".txt";
                    while (File.Exists(DataFile))
                    {
                        FileRec++;
                        DataFile = DirNameTemp + "\\QueryRes-" + FileRec.ToString() + "-" + DateInf + ".txt";
                    }
                }
                return blL_FileOk;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GiveMeFileNameQyRsls", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GiveMeFileNameQyRsls", "", "Exception: " + ex.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String CMD2UnCompress_Ext(String IdPess, String SourcePath, String DestinationPath, String Monitor_UID, String Monitor_Pass)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    // /////////////////////////////////////////////////
                    // Llama el metodo, SA-UNZTR, para descompactar un archivo .ZTR en el servidor.
                    // Halla el TOKEN.
                    // /////////////////////////////////////////////////
                    String stL_Usu_Pandora = Monitor_UID;
                    String stL_Clave_Usu_Pandora = Monitor_Pass;
                    //
                    // Si el parametro del usuario esta vacio, toma el que tiene definida la clase.
                    if (stL_Usu_Pandora.Length == 0)
                    {
                        stL_Usu_Pandora = this._st_Monitor_UID;
                    }
                    // Si el parametro de la clave del usuario, toma el que tiene definida la clase.
                    if (stL_Clave_Usu_Pandora.Length == 0)
                    {
                        stL_Clave_Usu_Pandora = this._st_Monitor_Pass;
                    }
                    // Halla el Token
                    String stL_Token = this.CMD2Login(stL_Usu_Pandora, stL_Clave_Usu_Pandora);
                    //
                    socket.Connect(_Ip_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx;
                    //socket.ReceiveBufferSize = 2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx;
                    //socket.SendBufferSize = 256;
                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();
                    //-->>_Obj_Log.WriteTextInLog("SAUnzip_Ztr : Respuesta desde el servidor: " + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        throw new Exception(string.Format("CMD2UnCompress_Ext : Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    string stL_mensaje = string.Format("SA-UNZTR {0}|{1}|{2}|{3}\n", IdPess, SourcePath, DestinationPath, stL_Token);
                    //-->>_Obj_Log.WriteTextInLog("SAUnzip_Ztr : enviando comando: " + stL_mensaje);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLine().Replace("\r", "");
                    //respuesta = socket.ReadLine();
                    socket.Close();
                    if (!respuesta.Contains("SA-UNZTR OK"))
                    {
                        throw new Exception(string.Format("CMD2UnCompress_Ext : Respuesta de login no válida '{0}'", respuesta));
                    }
                    string[] stL_s = respuesta.Split(new char[] { ' ' });
                    if (stL_s.Length < 2)
                    {
                        throw new Exception(string.Format("CMD2UnCompress_Ext : Tamaño de la respuesta de login no válid0 '{0}'", respuesta));
                    }
                    if (stL_s[1] != "OK")
                    {
                        string resp = "";
                        for (int i = 0; i < stL_s.Length; i++)
                        {
                            resp += stL_s[i];
                        }
                        return resp;
                    }
                    return stL_s[1];
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2UnCompress_Ext", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2UnCompress_Ext", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String CMD2Ksc(String InfoMess, String Monitor_UID, String Monitor_Pass)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    socket.Connect(_Ip_IPServer, _in_PortNumber);
                    socket.ReceiveBufferSize = Buffer_Size_To_Trx;
                    //socket.ReceiveBufferSize = 2048;
                    socket.NoDelay = true;
                    socket.SendBufferSize = Buffer_Size_To_Trx;
                    //socket.SendBufferSize = 256;
                    socket.ReceiveTimeout = 30 * 1000;
                    string respuesta = socket.ReadLine();
                    //-->>_Obj_Log.WriteTextInLog("CMD2Ksc : Respuesta desde el servidor: " + respuesta);
                    //
                    if (!this.ValidAnswerState_ConnServer(respuesta))
                    {
                        throw new Exception(string.Format("CMD2Ksc : Respuesta de conexión no válida '{0}'", respuesta));
                    }
                    // /////////////////////////////////////////////////
                    // Llama el metodo, SA-UNZTR, para descompactar un archivo .ZTR en el servidor.
                    // Halla el TOKEN.
                    // /////////////////////////////////////////////////
                    String stL_Usu_Pandora = Monitor_UID;
                    String stL_Clave_Usu_Pandora = Monitor_Pass;
                    //
                    // Si el parametro del usuario esta vacio, toma el que tiene definida la clase.
                    if (stL_Usu_Pandora.Length == 0)
                    {
                        stL_Usu_Pandora = this._st_Monitor_UID;
                    }
                    // Si el parametro de la clave del usuario, toma el que tiene definida la clase.
                    if (stL_Clave_Usu_Pandora.Length == 0)
                    {
                        stL_Clave_Usu_Pandora = this._st_Monitor_Pass;
                    }
                    // Halla el Token
                    String stL_Token = this.CMD2Login(stL_Usu_Pandora, stL_Clave_Usu_Pandora);
                    //
                    string stL_mensaje = string.Format("SA-KSC {0}|{1}\n", InfoMess, stL_Token);
                    //-->>_Obj_Log.WriteTextInLog("enviando comando: " + stL_mensaje);
                    byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                    socket.Send(sendBuffer);
                    respuesta = socket.ReadLineD().Replace("\r", "");
                    //-->>_Obj_Log.WriteTextInLog("CMD2Ksc : Respuesta: " + respuesta);
                    socket.Close();
                    //
                    string[] stL_s = respuesta.Split(new char[] { '\n' });
                    String stL_Respuesta = "";
                    //
                    for (int inL_Index = 0; inL_Index < stL_s.Length; inL_Index++)
                    {
                        if (inL_Index == 0)
                        {
                            stL_Respuesta = stL_s[inL_Index];
                        }
                    }
                    return stL_Respuesta;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Ksc", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Ksc", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String CMD2MkDir(String Dir2Crte, String Monitor_UID, String Monitor_Pass, String Token_2V)
        {
            string respuesta = "";
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    //if (Token_2V.Trim() == "CLNBTN_CliPro.CMD2MkDir")
                    //{ // Inicio del if (st_Token_ToView.Trim() 
                        //
                        // /////////////////////////////////////////////////
                        // Halla el TOKEN.
                        // /////////////////////////////////////////////////
                        String stL_Usu_Pandora = Monitor_UID;
                        String stL_Clave_Usu_Pandora = Monitor_Pass;
                        //
                        // Si el parametro del usuario esta vacio, toma el que tiene definida la clase.
                        if (stL_Usu_Pandora.Length == 0)
                        {
                            stL_Usu_Pandora = this._st_Monitor_UID;
                        }
                        // Si el parametro de la clave del usuario, toma el que tiene definida la clase.
                        if (stL_Clave_Usu_Pandora.Length == 0)
                        {
                            stL_Clave_Usu_Pandora = this._st_Monitor_Pass;
                        }
                        // Halla el Token
                        String stL_Token = this.CMD2Login(stL_Usu_Pandora, stL_Clave_Usu_Pandora);
                        // /////////////////////////////////////////////////
                        // 
                        socket.Connect(_Ip_IPServer, _in_PortNumber);
                        socket.ReceiveBufferSize = Buffer_Size_To_Trx;
                        //socket.ReceiveBufferSize = 2048;
                        socket.NoDelay = true;
                        socket.SendBufferSize = Buffer_Size_To_Trx;
                        //socket.SendBufferSize = 256;
                        socket.ReceiveTimeout = 30 * 1000;
                        //
                        respuesta = socket.ReadLine();
                        //-->>_Obj_Log.WriteTextInLog("Respuesta desde el servidor: " + respuesta);
                        //
                        if (!this.ValidAnswerState_ConnServer(respuesta))
                        {
                            throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                        }
                        // /////////////////////////////////////////////////////////////
                        // Envia el comando :
                        // 'SA-MKDIRS' + st_Dir_A_Crear_En_Servidor + '|' + stL_Token 
                        // /////////////////////////////////////////////////////////////
                        //
                        string stL_mensaje = string.Format("SA-MKDIRS {0}|{1}\n", Dir2Crte, stL_Token);
                        //
                        //-->>_Obj_Log.WriteTextInLog("enviando comando: " + stL_mensaje);
                        byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(stL_mensaje);
                        socket.Send(sendBuffer);
                        respuesta = socket.ReadLineD().Replace("\r", "");
                        //-->>_Obj_Log.WriteTextInLog("Respuesta: " + respuesta);
                        socket.Close();
                        //
                        if (!respuesta.Contains("SA-MKDIRS"))
                        {
                            throw new Exception(string.Format("Respuesta no válida '{0}'", respuesta));
                        }
                        string[] stL_s = respuesta.Split(new char[] { '\n' });
                        respuesta = stL_s[0];
                    //} // Fin del if (st_Token_ToView.Trim() 
                    return respuesta;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2MkDir", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return "";
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2MkDir", "", "Exception: " + ex.Message, "", "");
                return "";
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private Socket CMD2Put_Ext(string NombreArchivoRemoto, long Tamano, int bufferSize, String st_Usuario_Pandora, String st_Clave_Usuario_Pandora)
        {
            /// 
            /// Envia un archivo al servidor Pandora, ejecutando el comando SA-PUT
            /// Halla el Token para poder ejecutar el envio del archivo
            /// 
            /// <param name="NombreArchivoRemoto">Ruta y Nombre del archivo como va a quedar en el servidor</param>
            /// <param name="Tamano">Tamaño del archivo</param>
            /// <param name="bufferSize">Tamaño del Buffer</param>
            /// <param name="st_Usuario_Pandora">Usuario de Pandora, para poder hallar el token</param>
            /// <param name="st_Clave_Usuario_Pandora">Clave del usuario de Pandora, para poder hallar el token</param>
            /// <returns>El sokect de la operacion</returns>
            try
            {
                // Ejecuta el comando SA-PUT
                // Halla el Token para poderlo ejecutar.
                String stL_Usu_Pandora = st_Usuario_Pandora;
                String stL_Clave_Usu_Pandora = st_Clave_Usuario_Pandora;
                //
                // Si el parametro del usuario esta vacio, toma el que tiene definida la clase.
                if (stL_Usu_Pandora.Length == 0)
                {
                    stL_Usu_Pandora = this._st_Monitor_UID;
                }
                // Si el parametro de la clave del usuario, toma el que tiene definida la clase.
                if (stL_Clave_Usu_Pandora.Length == 0)
                {
                    stL_Clave_Usu_Pandora = this._st_Monitor_Pass;
                }
                // Halla el Token
                String stL_Token = this.CMD2Login(st_Usuario_Pandora, st_Clave_Usuario_Pandora);
                //
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(_Ip_IPServer, _in_PortNumber);
                socket.ReceiveBufferSize = Buffer_Size_To_Trx;  // 8192;
                //-->>socket.ReceiveBufferSize = 2048;

                socket.SendBufferSize = bufferSize;


                socket.NoDelay = true;
                socket.SendBufferSize = Buffer_Size_To_Trx; //  8192;
                //socket.SendBufferSize = 256;

                socket.ReceiveTimeout = 30 * 1000;
                string respuesta = socket.ReadLine();

                Console.WriteLine("Respuesta desde el servidor: " + respuesta);
                //
                if (!this.ValidAnswerState_ConnServer(respuesta))
                {
                    throw new Exception(string.Format("Respuesta de conexión no válida '{0}'", respuesta));
                }
                // Envia el Token, al final
                string msg = string.Format("SA-PUT {0}|{1}|{2}|{3}|{4}\n", NombreArchivoRemoto, NombreArchivoRemoto, Tamano, bufferSize, stL_Token);
                byte[] sendBuffer = ASCIIEncoding.ASCII.GetBytes(msg);
                socket.Send(sendBuffer);
                respuesta = socket.ReadLine();
                //
                if (!respuesta.Contains("SA-PUT"))
                {
                    throw new Exception(string.Format("SA-PUT Respuesta de conexión no válida '{0}'", respuesta));
                }
                //estas 4 lineas se marcan como comentario para prueba 17/01/2013 11:14 am jonathan serna saenz
                //if (respuesta.ToUpper().Contains("TOKEN NO VALIDO"))
                //{
                //    throw new Exception(respuesta);
                //}
                //socket.Send(ASCIIEncoding.ASCII.GetBytes("send please\n"));
                return socket;
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Put_Ext", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return null;
            }
            catch (Exception ex)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CMD2Put_Ext", "", "Exception: " + ex.Message, "", "");
                return null;
            }
        }

        static void timer1_Elapsed(object Sender,EventArgs e)
        {
            // NADA 
        }




    }

    public static class ExtSocket
    {
        public static string ReadLine(this Socket sock)
        {
            StringBuilder buff = new StringBuilder(sock.ReceiveBufferSize);
            byte[] b = new byte[1];
            b[0] = 0;
            while (b[0] != '\n')
            {
                sock.Receive(b);
                if (b[0] != '\n')
                    buff.Append((char)b[0]);
                if (buff.ToString().Contains("IFX-Get NOEXISTE"))
                {
                    break;
                }
                else
                {
                    if (buff.ToString().Contains("PANDORA-NoSql NOEXISTE"))
                    {
                        break;
                    }
                }
            }
            return buff.ToString();
        }
        public static string ReadLineD(this Socket sock)
        {
            StringBuilder buff = new StringBuilder(sock.ReceiveBufferSize);
            byte[] b = new byte[1];
            b[0] = 0;
            while (b[0] != '\n')
            {
                sock.Receive(b);
                if (b[0] != '\n')
                {
                    buff.Append((char)b[0]);
                }
                else
                {
                    sock.Receive(b);
                    if (b[0] != ' ')
                    {
                        byte saltoL = Convert.ToByte('\n');
                        buff.Append((char)saltoL);
                        buff.Append((char)b[0]);
                    }
                    else
                    {
                        b[0] = Convert.ToByte('\n');
                    }
                }
            }
            return buff.ToString();
        }
    }
    
}
