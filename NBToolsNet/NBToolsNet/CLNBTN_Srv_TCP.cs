using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public class CLNBTN_Srv_TCP : IfazX_RespuestasTCP
    {
        List<CLNBTN_FuncKrl> clientesConectados = new List<CLNBTN_FuncKrl>();
        public String mensajeError = "";
        private String direccionIP = "127.0.0.1";
        private int puerto = 6116;
        private int timeout = 0;
        private IPAddress ipaddress = null;
        private TcpListener tcplistener = null;
        private Socket socketCliente = null;
        private Boolean conectado = true;
        //private IfazX_RespuestasTCP objetoSalida = null;
        private IfazX_mensajesSRV respuestaservidor = null;
        //
        private String _st_Cedula = "";
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = false;
        //
        private String _st_User = "CLNBTN_Srv_TCP";
        private String _st_FileLog = "C:\\Windows\\CLNBTN_Srv_TCP.log";
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Srv_TCP";
        public void setObjetoParaSalida(IfazX_mensajesSRV objeto)
        {
            respuestaservidor = objeto;
        }

        public CLNBTN_Srv_TCP(String LicName,String UserName, String LogFile)
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


        public CLNBTN_Srv_TCP(String LicName, String UserName, String LogFile, String st_IpAddress, int in_TCP_Port)
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
                this.direccionIP = st_IpAddress;
                this.puerto = in_TCP_Port;
                ipaddress = IPAddress.Parse(direccionIP);
                tcplistener = new TcpListener(ipaddress, puerto);
            }
        }


   


        public void DisConnect() { conectado = false; }

        public void setIpAddress(String dato){
            direccionIP = dato;

        }

        public void setPort(int dato)
        {
            puerto = dato;

        }

        public String get_st_Cedula()
        {
            return this._st_Cedula;
        }

        public void set_st_Cedula(String dato)
        {
            this._st_Cedula = dato;
        }

        [HandleProcessCorruptedStateExceptions]
        public int Wait4Client()
        {
            try {
 
                return 0;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Wait4Client. System.AccessViolationException", "", ex_0.Message.ToString());
                return -1;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Wait4Client. Exception", "", ex.Message.ToString());
                return -1;
            }
        }
        //

        [HandleProcessCorruptedStateExceptions]
        private void threadWait4Clients() { 
            try 
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " threadWait4Clients. Beginnig PANDORA Server On TCP Port :  [" + puerto.ToString() + "] ");
                Console.WriteLine("Beginnig PANDORA Server On TCP Port :  [" + puerto.ToString() + "] ");
                ipaddress = IPAddress.Parse(direccionIP);
                tcplistener = new TcpListener(ipaddress, puerto);
                //tcplistener = new TcpListener(puerto);
                tcplistener.Start();
                //
                while (conectado)
                {
                    objL_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " threadWait4Clients. Waiting for Client ...");
                    Console.WriteLine("Waiting for Client ...");
                    //
                    socketCliente = tcplistener.AcceptSocket();
                    //
                    //Entrando un cliente
                    objL_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " threadWait4Clients. Client Connected [" + socketCliente.RemoteEndPoint + "] ");
                    Console.WriteLine("Client Connected [" + socketCliente.RemoteEndPoint + "] ");
                    int llaveCliente = clientesConectados.Count;
                    objL_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " threadWait4Clients. Key for Client [" + llaveCliente.ToString() + "] ");
                    Console.WriteLine("Key for Client [" + llaveCliente.ToString() + "] ");
                    //CLNBTN_FuncKrl o_cliente = new CLNBTN_FuncKrl(this,socketCliente,(clientesConectados.Count));
                    CLNBTN_FuncKrl o_cliente = new CLNBTN_FuncKrl(_st_Lic, _st_User, _st_FileLog, this, socketCliente, (clientesConectados.Count));
                    o_cliente.setObjetoParaSalida(this);
                    clientesConectados.Add(o_cliente);
                    clientesConectados[(clientesConectados.Count - 1)].setTimeout(timeout);
                    clientesConectados[(clientesConectados.Count-1)].start();
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "threadWait4Clients. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "threadWait4Clients. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void start() {
            try
            {
                Thread t = new Thread(new ThreadStart(threadWait4Clients));
                // Deja esta propiedad en true, para que cuando se cierre el programa
                // Tambien se termine la tarea.
                t.IsBackground = true;
                t.Start();
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "start. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "start. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void Want2Exit(CLNBTN_FuncKrl cliente)
        {
            try
            {
                cliente.Desconectar();
                clientesConectados.Remove(cliente);
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Want2Exit(1). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Want2Exit(1). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void Want2Exit()
        {
            try
            {
                // Desconecta todos los clientes.
                foreach (CLNBTN_FuncKrl cliente in clientesConectados) 
                {
                    cliente.Desconectar();
                    clientesConectados.Remove(cliente);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Want2Exit(2). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Want2Exit(2). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void x_respuestastcp(String quien, String[] datos)
         {
             try
             {
                 this._st_Cedula = "";
                 if (quien.Equals("SA-SALIR"))
                 {

                 }
                 else if (quien.Equals("SA-CONSULTA"))
                 {
                     String cedula = datos[0];
                     if (respuestaservidor == null)
                     {
                         this._st_Cedula = cedula;
                     }
                     else
                     {
                         respuestaservidor.x_mensajesservidor(datos);
                     }

                 }
                 else if (quien.Equals("SA-LOGIN"))
                 {
                     String token = datos[0];

                 }
             }
             catch (System.AccessViolationException ex_0)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "x_respuestastcp. System.AccessViolationException", "", ex_0.Message.ToString());
             }
             catch (Exception ex)
             {
                 CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                 objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "x_respuestastcp. Exception", "", ex.Message.ToString());
             }
        }




    }
}
