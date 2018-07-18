using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public class CLNBTN_FuncKrl
    {
        //Respuestas
        IfazX_RespuestasTCP ifaz_respuestatcp;
        String s_ipcliente = "";
        //
        private String mensajeError = "";
        CLNBTN_Srv_TCP servidortcp = null;
        private Socket socketCliente = null;
        NetworkStream networkStream = null;
        StreamWriter streamWriter = null;
        StreamReader streamReader = null;
        private int mi_llave;
        private int timeout = 20000;
        //
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = false;
        //
        private String _st_User = "CLNBTN_FuncKrl";
        private String _st_FileLog = "C:\\Windows\\CLNBTN_FuncKrl.log";
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_FuncKrl";
        public void setObjetoParaSalida(IfazX_RespuestasTCP objeto)
        {
            ifaz_respuestatcp = objeto;
        }

            

        public CLNBTN_FuncKrl(String LicName, String UserName, String LogFile , CLNBTN_Srv_TCP servidor, Socket socket, int llave)
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
                servidortcp = servidor;
                socketCliente = socket;
                mi_llave = llave;
                //ifaz_respuestatcp = servidortcp;
            }
        }


       
       
        public void setTimeout(int milisegundos) {
            timeout = milisegundos;
            socketCliente.ReceiveTimeout = timeout;

        }

        public String quitarPalabra(String sfrase, String spalabra)
        {
            String ssalida = "";
            int nstart = sfrase.IndexOf(spalabra) + spalabra.Length;
            ssalida = sfrase.Substring(nstart);
            return ssalida;

        }

         [HandleProcessCorruptedStateExceptions]
        public CLNBTN_Protocol procesarES(String sentrada)
        {
            try
            {
                CLNBTN_Protocol sretorno = new CLNBTN_Protocol();
                if (sentrada.Contains("SA-SALIR"))
                {
                    sretorno.comando = "SA-SALIR";
                    sretorno.setParametros(quitarPalabra(sentrada, sretorno.comando));
                }
                else if (sentrada.Contains("SA-CONSULTA"))
                {
                    sretorno.comando = "SA-CONSULTA";
                    sretorno.setParametros(quitarPalabra(sentrada, sretorno.comando));
                }
                else if (sentrada.Contains("SA-LOGIN"))
                {
                    sretorno.comando = "SA-LOGIN";
                    sretorno.setParametros(quitarPalabra(sentrada, sretorno.comando));
                }
                return sretorno;

            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "procesarES. System.AccessViolationException", "", ex_0.Message.ToString());
                return null;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "procesarES. Exception", "", ex.Message.ToString());
                return null;
            }
        }


         [HandleProcessCorruptedStateExceptions]
        private void nucleoFunciones() {
            try
            {
                //Crear buffer de lectura y escritura
                networkStream = new NetworkStream(socketCliente);
                streamWriter = new StreamWriter(networkStream);
                streamReader = new StreamReader(networkStream);
                String cadenaEntrada = "";
                //
                CLNBTN_Protocol protocolo = new CLNBTN_Protocol();
                s_ipcliente = socketCliente.RemoteEndPoint.ToString();
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " FuncKernel. SRV-PANDORA Index[ "+mi_llave.ToString()+" ] IP[ " +s_ipcliente+ " ] ");
                //-->>streamWriter.WriteLine("SRV-PANDORA Index[ "+mi_llave.ToString()+" ] IP[ " +s_ipcliente+ " ] ");
                streamWriter.Flush();
                //
                while (true)
                {
                    cadenaEntrada = streamReader.ReadLine();
                    objL_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " FuncKernel. Input Command From : " + s_ipcliente + " : " + cadenaEntrada);
                    Console.WriteLine("Input Command From : " + s_ipcliente + " : " + cadenaEntrada);
                    protocolo = procesarES(cadenaEntrada);

                    if (protocolo.comando.Contains("SA-SALIR"))
                    {
                        protocolo.respuesta = "SA-SALIR " + mi_llave.ToString();
                        break;
                    }
                    else if (protocolo.comando.Contains("SA-CONSULTA"))
                    {
                        try
                        {
                            if (protocolo.listaparametros.Length > 1)
                            {
                                String cedula = protocolo.listaparametros[0].Trim();

                                String[] datos = new String[1];
                                datos[0] = cedula;
                                ifaz_respuestatcp.x_respuestastcp("SA-CONSULTA", datos);
                                protocolo.respuesta = "SA-CONSULTA " + cedula;
                            }
                            else
                            {
                                protocolo.respuesta = "SA-CONSULTA ERROR " + "Los parametros no son correctos";
                            }
                        }catch(Exception e){
                            protocolo.respuesta = "SA-CONSULTA ERROR " + e.ToString();
                        }
                        
                    }
                    else if (protocolo.comando.Contains("SA-LOGIN"))
                    {
                        try
                        {
                            if (protocolo.listaparametros.Length > 1)
                            {
                                String usuario = protocolo.listaparametros[0];
                                String clave = protocolo.listaparametros[1];
                                protocolo.respuesta = "SA-LOGIN " + "90053737892015";
                                String[] datos = new String[3];
                                datos[0] = "90053737892015";
                                datos[1] = usuario;
                                datos[2] = clave;
                                ifaz_respuestatcp.x_respuestastcp("SA-LOGIN", datos);
                            }
                            else
                            {
                                protocolo.respuesta = "SA-LOGIN ERROR " + "Los parametros no son correctos";
                            }

                        }
                        catch (Exception e)
                        {
                            protocolo.respuesta = "SA-LOGIN ERROR " + e.ToString();
                        }

                    }
                    else { protocolo.respuesta = "SRV-PANDORA Terminando Conexion ..."; }
                    //Escribiendo la salida
                    if (protocolo.respuesta.Length > 0)
                    {
                        objL_Log.WriteTextInLog(_st_Relac + " " + _st_RelacSon + " FuncKernel. Sending response to : " + s_ipcliente + " : " + protocolo.respuesta);
                        //-->>Console.Write("Enviando respuesta a : " + s_ipcliente + " : " + protocolo.respuesta);
                        streamWriter.WriteLine(protocolo.respuesta);
                        streamWriter.Flush();
                        break;
                    }
                }
                Desconectar();
                servidortcp.Want2Exit(this);
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "nucleoFunciones. System.AccessViolationException", "", ex_0.Message.ToString());
                mensajeError = ex_0.ToString();
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "nucleoFunciones. Exception", "", ex.Message.ToString());
                mensajeError = ex.ToString();
            }
        }

         [HandleProcessCorruptedStateExceptions]
        public void Desconectar()
        {
            try
            {
                if (socketCliente != null) { 
                    socketCliente.Close();
                    socketCliente = null;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Desconectar. System.AccessViolationException", "", ex_0.Message.ToString());
                mensajeError = ex_0.ToString();
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Desconectar. Exception", "", ex.Message.ToString());
                mensajeError = ex.ToString();
            }
        }

         [HandleProcessCorruptedStateExceptions]
        public void start() {
            try
            {
                Thread t = new Thread(new ThreadStart(nucleoFunciones));
                t.Start();
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "start. System.AccessViolationException", "", ex_0.Message.ToString());
                mensajeError = ex_0.ToString();
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "start. Exception", "", ex.Message.ToString());
                mensajeError = ex.ToString();
            }
        }



    }
}
