using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Windows.Forms;
//
using System.IO;
//
using System.Data;
using System.Data.SqlClient;
//
using System.Diagnostics;
//
using _C_ProgRes;
//
using System.Text.RegularExpressions;
//
using System.Runtime.InteropServices;
//
using System.Globalization;

// Para el E-Mail
using System.Net;
using System.Net.Mail;

using System.Collections;
//
using System.Drawing;
//
using System.Management;
//
using System.Net.Sockets;
//
using MySql.Data.MySqlClient;
// Para PostGreSQL
using Npgsql;
using NpgsqlTypes;

namespace _C_Fenix_Kiosko
{
    public class ClasX_AppKiosko
    {
        //
        private String stPr_UsuarioApp = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        private String stPr_ArchivoConfApp = ""; // Path y Nombre del Archivo de Configuracion de la aplicacion
        //
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una venta
        private string stPr_ExeName_Exe = "_C_Fenix_Kiosko.dll"; // el nombre de la dll y la extensión:
        private const String NOM_CLASE = "ClasX_AppKiosko";
        private const String ST_DIR_TEMPO_SERVIDOR = "C:\\kioskostmp\\";
        //
        private ClasX_Config ObjPr_ConfigApp = null;
        private const String MENSAJE_APP_05 = "CORRECTO.";
        private const String MENSAJE_APP_06 = "FALLIDO.";

        /// <summary>
        /// Contructor de la clase
        /// </summary>
        /// <param name="st_UsuarioApp">Usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Ruta y nombre del archivo de log</param>
        /// <param name="st_ArchivoConfApp">Ruta y Nombre del archivo de configuracion de la aplicacion</param>
        /// <param name="bl_SalConsole">Indica la salida a la consola para el log</param>
        /// <param name="blPr_SalLog">Indica la salida al archivo de log para el log</param>
        /// <param name="blPr_SalDialog">Indica la salida a la pantalla para el log</param>
        public ClasX_AppKiosko(String st_UsuarioApp, String st_ArchivoLog, String st_ArchivoConfApp, Boolean bl_SalConsole = false, Boolean bl_SalidaLog = true, Boolean bl_SalidaDialog = true)
        {
            stPr_UsuarioApp = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            blPr_SalConsole = bl_SalConsole;
            stPr_ArchivoConfApp = st_ArchivoConfApp;
            //
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialog;

        }


        /// <summary>
        /// Propiedad getUserApp
        /// devuelve el codigo del usuario de la aplicacion
        /// </summary>
        /// <returns>stPr_UsuarioApp</returns>
        public string getUserApp()
        {
            return stPr_UsuarioApp;
        }
        /// <summary>
        /// Propiedad : getArchivoLog
        /// Devuelve el path del archivo de log,
        /// </summary>
        /// <returns>stPr_ArchivoLog</returns>
        public string getArchivoLog()
        {
            return stPr_ArchivoLog;
        }
        /// <summary>
        /// Prppiedad : getArchivoConfApp
        /// Devuelve el path y nombre del archivo de configuracion
        /// </summary>
        /// <returns>stPr_ArchivoConfApp</returns>
        public string getArchivoConfApp()
        {
            return stPr_ArchivoConfApp;
        }

        /// <summary>
        /// Propiedad : getSalConsole
        /// Devuelve el valor de la salida a la consola en el  manejo del log.
        /// </summary>
        /// <returns>blPr_SalConsole = Salida a la consola en el manejo del log</returns>
        public bool getSalConsole()
        {
            return blPr_SalConsole;
        }

        /// <summary>
        /// Propiedad : getSalLog
        /// Devuelve el valor de la salida a archivo en el manejo del log.
        /// </summary>
        /// <returns>blPr_SalLog = Salida a archivo en el  manejo del log</returns>
        public bool getSalLog()
        {
            return blPr_SalLog;
        }

        /// <summary>
        /// Propiedad : getSalDialog
        /// Devuelve el valor de la salida a la pantalla en el manejo del log.
        /// </summary>
        /// <returns>blPr_SalDialog = Salida a la pantalla en el  manejo del log</returns>
        public bool getSalDialog()
        {
            return blPr_SalDialog;
        }

        /// <summary>
        /// Metodo : setUserAPP
        /// Permite definir el codigo del usuario de la aplicacion
        /// </summary>
        /// <param name="st_User">codigo del usuario de la aplicacion</param>
        public void setUserAPP(string st_User)
        {
            stPr_UsuarioApp = st_User;
        }
        /// <summary>
        /// Metodo : setArchivoLog
        /// Permite definir el path y nombre del archivo de log.
        /// </summary>
        /// <param name="st_ArchivoLog">path y nombre del archivo de log</param>
        public void setArchivoLog(string st_ArchivoLog)
        {
            stPr_ArchivoLog = st_ArchivoLog;
        }
        /// <summary>
        /// Metodo : setArchivoConfApp
        /// Permite cambiar el path y nombre del archivo de configuracion
        /// </summary>
        /// <param name="st_ArchivoConfApp">path y nombre del archivo de configuracion</param>
        public void setArchivoConfApp(string st_ArchivoConfApp)
        {
            stPr_ArchivoConfApp = st_ArchivoConfApp;
        }

        /// <summary>
        /// Metodo : setSalConsole
        /// Permite cambiar el valor de la salida a la consola en el  manejo del log.
        /// </summary>
        /// <param name="bl_SalConsole">Salida a la consola en el manejo del log.</param>
        public void setSalConsole(bool bl_SalConsole)
        {
            blPr_SalConsole = bl_SalConsole;
        }

        /// <summary>
        /// Metodo : setSalLog
        /// Permite cambiar el valor de la a archivo en el  manejo del log.
        /// </summary>
        /// <param name="blPr_SalLog">Salida a  archivo en el manejo del log.</param>
        public void setSalLog(bool bl_SalLog)
        {
            blPr_SalLog = bl_SalLog;
        }


        /// <summary>
        /// Metodo : setDialog
        /// Permite cambiar el valor de la salida a la pantalla en el  manejo del log.
        /// </summary>
        /// <param name="blPr_SalDialog">Salida a la pantalla en el manejo del log.</param>
        public void setDialog(bool bl_Dialog)
        {
            blPr_SalDialog = bl_Dialog;
        }


        /// <summary>
        /// Metodo : Halla_Fecha_Hora_X_Archivo
        /// Halla la fecha y hora de la maquina, para concatenarlo 
        /// </summary>
        /// <returns></returns>
        public String Halla_Fecha_Hora_X_Archivo()
        {
            // Halla la fecha y hora, minutos y segundos y lo devuelve en un string
            String stL_FechaHora = "";
            String stL_File_Name = "";
            String stL_FechaAux = "";
            //
            String stL_Ano = "";
            String stL_Mes = "";
            String stL_Dia = "";
            //
            //String stL_Hora = "";
            String stL_HoraMaquina = "";
            //int inL_Hora = 0;
            //int inL_Pos = 0;
            //
            try
            {
                // Toma Fecha y hora
                stL_FechaHora = DateTime.Now.ToString();
                stL_File_Name = "";
                // Viene DD/MM/AAAA    
                stL_Ano = DateTime.Now.Year.ToString(); // stL_FechaHora.Substring(6, 4);
                stL_Mes = DateTime.Now.Month.ToString();  //stL_FechaHora.Substring(3, 2);
                stL_Dia = DateTime.Now.Day.ToString();  //stL_FechaHora.Substring(0, 2);
                //
                if ((Convert.ToInt32(stL_Mes) < 9) & (stL_Mes.Length == 1))
                {
                    stL_Mes = "0" + stL_Mes;
                }
                if ((Convert.ToInt32(stL_Dia) < 9) & (stL_Dia.Length == 1))
                {
                    stL_Dia = "0" + stL_Dia;
                }
                //
                stL_FechaAux = stL_Ano + stL_Mes + stL_Dia;
                stL_FechaAux = stL_FechaAux.Trim();

                stL_HoraMaquina = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                stL_File_Name = stL_FechaAux + "_" + stL_HoraMaquina;
                //
                return stL_File_Name;
            } // del try
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Halla_Fecha_Hora_X_Archivo", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return stL_File_Name;
            }
        }



        /// <summary>
        /// Metodo : Valida_Puerto_TCP_Abierto
        /// Encargado de validar si un puerto TCP esta abierto o no.
        /// </summary>
        /// <param name="st_IPServidor">Direccion IP del Servidor</param>
        /// <param name="In_puerto">Numero del puerto TCP</param>
        /// <returns>Devuelve TRUE si el puerto esta abierto</returns>
        public Boolean Valida_Puerto_TCP_Abierto(String st_IPServidor, int In_puerto)
        {
            Boolean blL_PuertoAbierto = false;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //
            try
            {
                //
                socket.Connect(st_IPServidor, In_puerto);
                if (socket.Connected)
                {
                    blL_PuertoAbierto = true;
                }
                //
                return blL_PuertoAbierto;
            }
            //
            catch (SocketException e)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (e.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Valida_Puerto_TCP_Abierto", "", e.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Valida_Puerto_TCP_Abierto", "", e.Message.ToString() + " " + e.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Valida_Puerto_TCP_Abierto", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Valida_Puerto_TCP_Abierto", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        public void Lee_Info_Basica_Archivo_Config(ref String stR_Serv, ref String stR_ServDig, ref String stR_inPuertoDig, ref String stR_Timeoff, ref String stR_cursor, ref String stR_modo, ref String stR_inPuerto, ref String stR_RutaBase, ref int inR_SP_TRAMITE, ref string[] stR_ParamBd, ref String stR_NombreEmpresaApp)
        {
            // Lee la informacion basica desde el archivo de configuracion
            ClasX_Config ObjL_ConfigApp = null;
            ClasX_Encripta ObjL_Encripta = null;
            try
            {
                //
                ObjL_ConfigApp = new ClasX_Config(stPr_ArchivoConfApp, stPr_ArchivoLog, stPr_ArchivoLog);
                //
                stR_Serv = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "ServeraD");
                stR_ServDig = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "ServerDig");
                stR_inPuertoDig = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "PuertoDig");
                stR_Timeoff = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "TOff");
                stR_cursor = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "cursor");
                stR_modo = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "MODO");
                //
                stR_inPuerto = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "PuertoaD");
                stR_RutaBase = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "RBase");
                inR_SP_TRAMITE = Convert.ToInt16(ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "SP"));
                //
                String stL_Seccion_BD = _C_ProgRes.ClasX_Constans.SECCION_BD_0;
                // Lee informacion de la base de datos.
                ObjL_Encripta = new ClasX_Encripta(stPr_UsuarioApp, stPr_ArchivoLog);
                String stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "Name");
                ClasX_DBInfo.inDB_Types inL_TipoBD = 0;
                ClasX_DBInfo.inConnect_Type inL_TipoConexion = 0;
                //
                stR_NombreEmpresaApp = ObjL_ConfigApp.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "cia");
                //
                stR_ParamBd[0] = stL_Aux;
                stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "Engine");
                inL_TipoBD = (ClasX_DBInfo.inDB_Types)(Convert.ToInt32(stL_Aux));
                stR_ParamBd[1] = stL_Aux;
                //
                stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "Security");
                inL_TipoConexion = (_C_ProgRes.ClasX_DBInfo.inConnect_Type)(Convert.ToInt32(stL_Aux));
                stR_ParamBd[2] = stL_Aux;
                //
                stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "URL");
                stR_ParamBd[3] = stL_Aux;
                //
                stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "Server");
                stR_ParamBd[4] = stL_Aux;
                //
                stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "IPAddress");
                stR_ParamBd[5] = stL_Aux;
                //
                stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "IPAddress");
                stR_ParamBd[6] = stL_Aux;

                stL_Seccion_BD = ClasX_Constans.SECCION_BD_CONNECT_INFO;
                stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "F");
                stL_Aux = ObjL_Encripta.DesEncriptaInfo(stL_Aux, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                stR_ParamBd[7] = stL_Aux;
                //
                stL_Aux = ObjL_ConfigApp.LeeLlave_Seccion(stL_Seccion_BD, "D");
                stL_Aux = ObjL_Encripta.DesEncriptaInfo(stL_Aux, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                stR_ParamBd[8] = stL_Aux;
                //
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Lee_Info_Basica_Archivo_Config", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Lee_Info_Basica_Archivo_Config", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        /// <summary>
        /// Test de conexión a aDirector, BD Fenix y GA2
        /// </summary>
        public void VerificaFenix_GA2( String st_Serv , String St_inPuerto , string[] st_ParamBd , ref ulong inR_A001NUM_IDENTIFICACION , ref String stR_MensajeError)
        {
            try
            {
                ClasX_ClienteTCP Obj_ConTCP = new ClasX_ClienteTCP(st_Serv, Convert.ToInt32(St_inPuerto), stPr_UsuarioApp, stPr_ArchivoLog);
                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (Obj_ConTCP.testConexion())
                {
                    objL_Log_1.setTextErrLog("VerificaFenix_GA2 : Conexion Fenix (" + st_Serv + "): " + MENSAJE_APP_05);
                    //Verifica conexión con BD Fenix
                    this.VerificaConexionBD(st_ParamBd, ref stR_MensajeError);
                    this.VerificaConexionGA2(st_ParamBd, ref inR_A001NUM_IDENTIFICACION , ref stR_MensajeError);
                }
                else
                {
                    objL_Log_1.setTextErrLog("VerificaFenix_GA2 : Conexion Fenix (" + st_Serv + "): " + MENSAJE_APP_06);
                    String stL_Men = "- No es posible establecer una conexión con el servidor FENIX.";
                    if (stR_MensajeError.Length == 0)
                    {
                        stR_MensajeError = "Atención: \n" + stL_Men;
                    }
                    else
                    {
                        stR_MensajeError += "\n" + stL_Men;
                    }
                    //MessageBox.Show("No es posible establecer una conexión con el servidor Fenix. Por favor verifique las conexiones de red e informe al administrador del sistema \n" + Convert.ToString(respuestaFenix.Status), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.DoEvents();
                }

            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "VerificaFenix_GA2", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "VerificaFenix_GA2", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        public void VerificaConexionBD(string[] st_ParamBd, ref String stR_MensajeError )
        {
            try
            {

                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                //Verifica conexión a BD
                ClasX_Consultas objL_Consultas = new ClasX_Consultas(stPr_ExeName_Exe, objL_Log_1, st_ParamBd);
                //
                if (objL_Consultas.validarConexion())
                {
                    //objL_Consultas.EjecutaSP("SP_021_ACT_CONSUL_KIOSKO", "-1");
                    //objL_Consultas.CierraCOnexiones();
                    objL_Log_1.setTextErrLog("VerificaConexionBD : Conexion a BD Fenix: " + MENSAJE_APP_05);
                }
                else
                {
                    objL_Log_1.setTextErrLog("VerificaConexionBD : Conexion a BD Fenix: " + MENSAJE_APP_06);
                    String stL_Men = "- No es posible establecer una conexión con la BD Fenix.";
                    if (stR_MensajeError.Length == 0)
                    {
                        stR_MensajeError = "Atención: \n" + stL_Men;
                    }
                    else
                    {
                        stR_MensajeError += "\n" + stL_Men;
                    }
                }
                objL_Consultas.CierraCOnexiones();
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "VerificaConexionBD", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "VerificaConexionBD", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        public void VerificaConexionGA2(string[] st_ParamBd , ref ulong inR_A001NUM_IDENTIFICACION , ref String stR_MensajeError)
        {
            try
            {

                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log_1.setTextErrLog("antes de VerificaConexionGA2.if (!this.Conecta_BD_Ga2('001', st_ParamBd, ref inR_A001NUM_IDENTIFICACION))");
                if (!this.Conecta_BD_Ga2("001", st_ParamBd, ref inR_A001NUM_IDENTIFICACION))
                {
                    objL_Log_1.setTextErrLog("VerificaConexionGA2 : Conexion_BD_Ga2: " + MENSAJE_APP_06);
                    String stL_Men = "- No es posible establecer una conexión con la BD GA2.";
                    if (stR_MensajeError.Length == 0)
                    {
                        stR_MensajeError = "Atención: \n" + stL_Men;
                    }
                    else
                    {
                        stR_MensajeError += "\n" + stL_Men;
                    }
                    objL_Log_1.setTextErrLog("saliendo de VerificaConexionGA2.if (!this.Conecta_BD_Ga2('001', st_ParamBd, ref inR_A001NUM_IDENTIFICACION))");
                }
                    
                else
                {
                    objL_Log_1.setTextErrLog("VerificaConexionGA2 : Conexion_BD_Ga2: " + MENSAJE_APP_05);
                }
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "VerificaConexionGA2", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "VerificaConexionGA2", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        public Boolean Conecta_BD_Ga2(String st_Cedula, string[] st_ParamBd, ref ulong inR_A001NUM_IDENTIFICACION)
        {
            // ejecuta un sp que accesa la bd de ga2, para saber si hay conexion
            Boolean blL_Ok = false;
            try
            {
                inR_A001NUM_IDENTIFICACION = 0;
                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                ClasX_Consultas objL_Consultas = new ClasX_Consultas(stPr_ExeName_Exe, objL_Log_1, st_ParamBd);
                //
                objL_Log_1.setTextErrLog("antes de Conecta_BD_Ga2.SqlDataReader rdr = objL_Consultas.EjecutaSP('SP_010_LEE_AFILIADO_GA2', st_Cedula);");
                SqlDataReader rdr = objL_Consultas.EjecutaSP("SP_010_LEE_AFILIADO_GA2", st_Cedula);
                objL_Log_1.setTextErrLog("despues de Conecta_BD_Ga2.SqlDataReader rdr = objL_Consultas.EjecutaSP('SP_010_LEE_AFILIADO_GA2', st_Cedula);");
                inR_A001NUM_IDENTIFICACION = Convert.ToUInt64(st_Cedula);
                objL_Log_1.setTextErrLog("antes de Conecta_BD_Ga2.if (rdr != null)");
                if (rdr != null)
                {
                    blL_Ok = true;
                }
                objL_Log_1.setTextErrLog("después de Conecta_BD_Ga2.Conecta_BD_Ga2.if (rdr != null)");
                objL_Log_1.setTextErrLog("antes de Conecta_BD_Ga2.rdr.Close()");
                rdr.Close();
                objL_Log_1.setTextErrLog("despues de Conecta_BD_Ga2.rdr.Close()");
                objL_Log_1.setTextErrLog("antes de Conecta_BD_Ga2.objL_Consultas.CierraCOnexiones();");
                objL_Consultas.CierraCOnexiones();
                objL_Log_1.setTextErrLog("despues de Conecta_BD_Ga2.objL_Consultas.CierraCOnexiones();");
                //
                return blL_Ok;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Conecta_BD_Ga2", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Conecta_BD_Ga2", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }



        /// <summary>
        /// Test de conexión a Digiturno
        /// </summary>
        public void TestDigiturno(String st_ServDigi, String St_InPuertoDig, ref String stR_MensajeError)
        {
            try
            {
                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                ClasX_ClienteTCP ObjL_ConTCPDig = new ClasX_ClienteTCP(st_ServDigi, Convert.ToInt32(St_InPuertoDig), objL_Log_1);
                if (ObjL_ConTCPDig.testConexion())
                {
                    objL_Log_1.setTextErrLog("TestDigiturno : Conexion Digiturno: " + MENSAJE_APP_05);
                }
                else
                {
                    String stL_Men = "- No es posible establecer una conexión con Servidor Digiturno.";
                    if (stR_MensajeError.Length == 0)
                    {
                        stR_MensajeError = "Atención: \n" + stL_Men;
                    }
                    else
                    {
                        stR_MensajeError += "\n" + stL_Men;
                    }
                    objL_Log_1.setTextErrLog("TestDigiturno : Conexion Digiturno: " + MENSAJE_APP_06);
                }
                ObjL_ConTCPDig.Dispose();
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "TestDigiturno", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "TestDigiturno", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        /// <summary>
        /// Esta funcion trae las Biometrias
        /// Ultima Modificacion: 05112014, cambio en la variable terminedetraer en el tipo int y asi: (-1 ERROR, -9 Ejecutando, 1 Ok termine)
        /// Quien: Strail Aparicio
        /// </summary>
        /// <param name="st_PathApp"></param>
        /// <param name="st_NoCedula"></param>
        /// <param name="inPr_A001NUM_IDENTIFICACION"></param>
        /// <param name="stPr_Serv"></param>
        /// <param name="StPr_InPuerto"></param>
        /// <param name="stPr_RutaBase"></param>
        /// <param name="terminedetraer"></param>
        /// <param name="blPr_HayBio"></param>
        public void TraeBios(String st_PathApp, String st_NoCedula, ulong inPr_A001NUM_IDENTIFICACION, String stPr_Serv,
           String StPr_InPuerto, String stPr_RutaBase, ref int terminedetraer, ref bool blPr_HayBio, ref String st_biofalta)
        {

            //Strail 05112014
            terminedetraer = -9;

            try
            {
                ClasX_EventLog ObjPr_EventLog = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  INICIANDO ... " + terminedetraer);
                
                //Strail 21112014
                Application.DoEvents();
                String strL_temp = "C:\\fnx";
                ObjPr_ConfigApp = new _C_ProgRes.ClasX_Config(stPr_ArchivoConfApp, stPr_ArchivoLog, stPr_ArchivoLog);
                String strL_Biocomp = ObjPr_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "BiomC");
                Application.DoEvents();
                ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  INFO_KIOSKO =" + strL_Biocomp);
                //Strail

                blPr_HayBio = false;
                //
                //ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (!Directory.Exists(strL_temp))
                {
                    Application.DoEvents();
                    //objL_Log_1.setTextErrLog("TraeBios : Creando directorio local" + strL_temp);
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Creando directorio local :" + strL_temp);
                    Directory.CreateDirectory(strL_temp);
                }
                ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Creando ClasX_ClienteTCP ");
                int inL_Puerto = Convert.ToInt16(StPr_InPuerto);
                ClasX_ClienteTCP ObjL_ClienteTCP = new ClasX_ClienteTCP(stPr_Serv, inL_Puerto, stPr_UsuarioApp, stPr_ArchivoLog);
                //
                // ----------------------------------------
                // NUEVO TRATAMIENTO DEL ARCHIVO DE LA BIOMETRIA
                // ----------------------------------------
                //-->>string respuesta = ObjL_ClienteTCP.SAUnzip(rutaOrigen, "C:\\kioskostmp\\" + txt_Identificacion.Text + "\\", StPr_TOKEN);
                //-->>string rutaOrigen = this.armaRepo(st_NoCedula, stPr_RutaBase);
                //-->> string[] cadena = obtieneToken(stPr_Serv, StPr_inPuerto);
                //-->>StPr_TOKEN = cadena[0];
                String stL_Usuario_Pandora = "";
                String stL_Clave_Usuario_Pandora = "";

                stL_Usuario_Pandora = this.Halla_Usuario_Pandora(stPr_Serv, inL_Puerto.ToString(), ref stL_Clave_Usuario_Pandora);
                ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Usuario en Pandora : " + stL_Usuario_Pandora);
                //
                ClasX_Utils ObjL_Utilis = new ClasX_Utils(stPr_UsuarioApp, stPr_ArchivoLog, false, true, false);
                //
                String stL_Ruta_Bio = ObjL_Utilis.ArmaRuta_Repo_X_Cedula(st_NoCedula, stPr_RutaBase);
                ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  stL_Ruta_Bio : " + stL_Ruta_Bio);
                //String stL_Ruta_Temporal_Servidor = "C:\\kioskostmp\\"; 
                String stL_Ruta_Temporal_Servidor = ST_DIR_TEMPO_SERVIDOR + st_NoCedula + "\\";
                String respuesta = ObjL_ClienteTCP.SAUnzip_Ztr(st_NoCedula, stL_Ruta_Bio, stL_Ruta_Temporal_Servidor, stL_Usuario_Pandora, stL_Clave_Usuario_Pandora);
                ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Respuesta SAUNZIP : " + respuesta);
                //String respuesta = ObjL_ClienteTCP.TraerBiometria_Ztr(st_NoCedula, stL_Ruta_Temporal_Servidor, stPr_RutaBase );
                //if (respuesta.Trim().ToUpper() == "IFX-GET NOEXISTE")
                //{
                //    // Trae la biometria encriptada si no existe la biometria sin encriptar
                //    respuesta = ObjL_ClienteTCP.TraerBiometria(st_NoCedula, stL_Ruta_Temporal_Servidor, stPr_RutaBase);
                //}
                //
                // ----------------------------------------
                // FIN NUEVO TRATAMIENTO DEL ARCHIVO DE LA BIOMETRIA
                // ----------------------------------------
                if (!respuesta.ToUpper().Contains("OK"))
                {
                    //objL_Log_1.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "TraeBios", "", respuesta, "", "");

                    //
                    // blPr_HayBio = false;
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  No Existen Biometrias en el servidor para : " + st_NoCedula);
                    if (Directory.Exists(strL_temp))
                    {
                        Directory.Delete(strL_temp, true);
                    }
                    if (Directory.Exists("C:\\fnxtemp"))
                    {
                        Directory.Delete("C:\\fnxtemp", true);
                    }
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Se eliminan directorios temporales de : " + st_NoCedula);
                    st_biofalta = "FALTA: FIRMA, DOCTO, HUELLA, ROSTRO, ";
                    terminedetraer = 1;
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Respuesta thread : " + terminedetraer);
                }
                else
                {
                    //-->>>string respu = ObjL_ClienteTCP.SADIR("C:\\kioskostmp\\" + st_NoCedula + "\\");
                    // Cambia la ruta en el servidor incluyendo las carpetas generadas por el UNZIP
                    stL_Ruta_Temporal_Servidor = ST_DIR_TEMPO_SERVIDOR + st_NoCedula + "\\";
                    string respu = ObjL_ClienteTCP.SADIR(stL_Ruta_Temporal_Servidor);
                    //objL_Log_1.setTextErrLog("TraeBios " + respu);
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Respuesta SADIR : " + respu);
                    String[] contenido = respu.Split(new char[] { '\n' });
                    //objL_Log_1.setTextErrLog(Convert.ToString(contenido.Length));
                    //
                    //
                    //
                    //
                    // =================================================
                    // Valida si hay archivos en el directorio
                    // =================================================
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Validando Archivos en el Servidor ... ");
                    Boolean blL_EvaluaOtroDir = false;
                    Boolean blL_EvaluaOtroDir2 = false;
                    //
                    if (contenido.Length == 0)
                    {
                        blL_EvaluaOtroDir = true;
                    }
                    else
                    {
                        if (contenido[1] == "")
                        {
                            blL_EvaluaOtroDir = true;
                        }
                    }
                    if (blL_EvaluaOtroDir)
                    { // Inicio del if (blL_EvaluaOtroDir)
                        // Aqui le asigna la cedula nuevamente
                        stL_Ruta_Temporal_Servidor = stL_Ruta_Temporal_Servidor + st_NoCedula + "\\";
                        //
                        respu = ObjL_ClienteTCP.SADIR(stL_Ruta_Temporal_Servidor);
                        ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Respuesta : " + respu);
                        //
                        contenido = respu.Split(new char[] { '\n' });
                        if (contenido.Length == 0)
                        {
                            blL_EvaluaOtroDir = true;
                        }
                        else
                        {
                            if (contenido[1] == "")
                            {
                                blL_EvaluaOtroDir2 = true;
                            }
                        }
                        if (blL_EvaluaOtroDir2)
                        {
                            stL_Ruta_Temporal_Servidor = stL_Ruta_Temporal_Servidor + st_NoCedula + "\\";
                            //
                            respu = ObjL_ClienteTCP.SADIR(stL_Ruta_Temporal_Servidor);
                            ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Respuesta : " + respu);
                            //
                            contenido = respu.Split(new char[] { '\n' });
                        }
                    } // Fin del if (blL_EvaluaOtroDir)
                    //
                    int count = 0;
                    //
                    //
                    //
                    //objL_Log_1.setTextErrLog("TraeBios : ---Revisando archivos---");
                    //
                    //
                    //antigua manera de revision de imágenes
                    //foreach (String archivo in contenido)
                    //{
                    //    if (!(archivo.Contains("SA-DIR") || archivo.ToLower().Contains("xml") || archivo.ToLower().Contains("sig") || archivo.ToLower().Contains("bin") || archivo.Contains(" ") || archivo.Contains("\n") || archivo.Equals("")))
                    //    {

                    //        count++;
                    //    }
                    //}
                    //Fin antigua manera de revision de imágenes
                    //
                    //
                    //
                    // Evalua de la nueva forma
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Evaluando biometrias ...");
                    String stL_ArchivoConfigNames = "";
                    ClasX_Utils ObJL_Utilis = new ClasX_Utils(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                    Boolean blL_Firma = false;
                    Boolean blL_Documento = false;
                    Boolean blL_Huella = false;
                    Boolean blL_Rostro = false;
                    Boolean blL_Iris = false;
                    stL_ArchivoConfigNames = st_PathApp + "\\_C_ProgRes.conf";
                    ObJL_Utilis.Evaluate_Bio_Files(stL_ArchivoConfigNames, contenido, ref blL_Firma, ref blL_Documento, ref blL_Huella, ref blL_Rostro, ref blL_Iris, ref count);

                    //AGR 18112014
                    st_biofalta = "";
                    if (!blL_Firma)
                    {
                        st_biofalta += "FIRMA, ";
                    }
                    if (!blL_Documento)
                    {
                        st_biofalta += "DOCTO, ";
                    }
                    if (!blL_Huella)
                    {
                        st_biofalta += "HUELLA, ";
                    }

                    ///AGR - 21 junio 2017
                    ///Se retira comprobación de imagen de rostro según requerimiento MA134811
                    //if (!blL_Rostro)
                    //{
                    //    st_biofalta += "ROSTRO, ";
                    //}
                    //if (!blL_Iris)
                    //{
                    //    //st_biofalta += "IRIS, ";
                    //}
                    if (st_biofalta.Length > 0)
                    {
                        st_biofalta = "FALTA: " + st_biofalta;
                    }
                    //AGR 18112014
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  st_biofalta : " + st_biofalta);
                    if (strL_Biocomp == "SI")
                    { // Inicio del if (strL_Biocomp == "SI")
                        //-->>if (count < 12)
                        if (count >= 20)
                        {
                            blPr_HayBio = true;
                        }
                    } // Fin del if (strL_Biocomp == "SI")
                    else // del if (strL_Biocomp == "SI")
                    { // Inicio del ELSE del if (strL_Biocomp == "SI")
                        //AGR 21 Junio 2017 se retira validación de rostro
                        //blPr_HayBio = (blL_Firma && blL_Documento && blL_Huella && blL_Rostro);
                        blPr_HayBio = (blL_Firma && blL_Documento && blL_Huella );
                    } // Fin del ELSE del if (strL_Biocomp == "SI")
                    // =============================================================================================0
                    //   
                    //
                    //
                    //
                    //objL_Log_1.setTextErrLog("TraeBios : ---Fin archivos explorados---");
                    //objL_Log_1.setTextErrLog(Convert.ToString(count) + " archivos encontrados en la biometria.");
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Archivos en la biometria se encontraron : " + count.ToString());
                    if (!Directory.Exists(strL_temp + "\\" + Convert.ToString(inPr_A001NUM_IDENTIFICACION) + "\\")) Directory.CreateDirectory(strL_temp + "\\" + Convert.ToString(inPr_A001NUM_IDENTIFICACION) + "\\");
                    //
                    //-->>respuesta = ObjL_ClienteTCP.RecibirArchivo("C:\\kioskostmp\\" + st_NoCedula + "\\Foto.jpg", strL_temp + "\\" + Convert.ToString(inPr_A001NUM_IDENTIFICACION) + "\\Foto.jpg", true);
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Inicia transmision de archivos ...");
                    //AGR 21 Junio 2017 - Se reemplaza la imagen a presentar en el Kiosco (Foto.jpg) por Documento de identidad
                    //respuesta = ObjL_ClienteTCP.RecibirArchivo(stL_Ruta_Temporal_Servidor + "\\Foto.jpg", strL_temp + "\\" + Convert.ToString(inPr_A001NUM_IDENTIFICACION) + "\\Foto.jpg", true);
                    //
                    respuesta = ObjL_ClienteTCP.RecibirArchivo(stL_Ruta_Temporal_Servidor + "\\DOCUMENTOSImagen1.jpg", strL_temp + "\\" + Convert.ToString(inPr_A001NUM_IDENTIFICACION) + "\\DOCUMENTOSImagen1.jpg", true);
                    if (!respuesta.ToUpper().Contains("OK"))
                    {
                        //Se intenta traer la otra imagen
                        respuesta = ObjL_ClienteTCP.RecibirArchivo(stL_Ruta_Temporal_Servidor + "\\Documento.jpg", strL_temp + "\\" + Convert.ToString(inPr_A001NUM_IDENTIFICACION) + "\\Documento.jpg", true);
                        if (!respuesta.ToUpper().Contains("OK"))
                        {//objL_Log_1.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "TraeBios", "", respuesta, "", "");
                            ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  ERROR Algun problema con la transmision o no hay archivos : " + respuesta);
                            //blPr_HayBio = false;
                            if (Directory.Exists(strL_temp)) Directory.Delete(strL_temp, true);
                            if (Directory.Exists("C:\\fnxtemp")) Directory.Delete("C:\\fnxtemp", true);
                            ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Se borraron archivos temporales");
                        }
                    }
                    else
                    {
                        Application.DoEvents();
                        //objL_Log_1.setTextErrLog("TraeBios : Termina de traer biometria para cc: " + Convert.ToString(inPr_A001NUM_IDENTIFICACION));
                        ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Termina transmision de archivos para : " + inPr_A001NUM_IDENTIFICACION.ToString());
                        //blPr_HayBio = true;
                    }
                    //if (count < 12)
                    //{
                    //    blPr_HayBio = false;
                    //}
                    terminedetraer = 1;
                    ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Respuesta 2 thread : " + terminedetraer);
                }
                ObjPr_EventLog.setTextErrLog("ClasX_AppKiosko.TraeBios(),  Termina Ok");
            }
            catch (Exception ex)
            {
                terminedetraer = -1; //Asignamos -1 para indicar que esta con error
                Application.DoEvents();
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "TraeBios", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "TraeBios", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            } 
        }

        public string armaRepo(string stR_NumBio, string stR_rutaBaseServidor, Boolean bl_XZtr = false)
        {
            string rutaRepo = "";
            String repo;
            try
            {
                int value = stR_NumBio.Length - 2;
                repo = stR_NumBio.Substring(value, 2);
                int numrepo = Convert.ToInt32(repo);
                if (numrepo < 50) repo = "Repositorio1\\" + repo;
                if (numrepo > 49) repo = "Repositorio2\\" + repo;
                //
                rutaRepo = stR_rutaBaseServidor + "\\Repositorios\\" + repo + "\\" + stR_NumBio;
                //
                if ( bl_XZtr ) 
                {
                    rutaRepo += ".ztr";
                }
                else
                {
                    rutaRepo += ".zip";
                }
                return rutaRepo;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "armaRepo", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "armaRepo", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return rutaRepo;
            }
        }


        public String[] obtieneToken(String st_Serv , String St_inPuerto )
        {
            String[] datos = new String[2];
            try
            {
                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                ClasX_Config ObjL_ConfigApp = new ClasX_Config(stPr_ArchivoConfApp, stPr_ArchivoLog, stPr_ArchivoLog);
                ClasX_ClienteTCP ObjL_ClienteTCP = new ClasX_ClienteTCP(st_Serv, Convert.ToInt32(St_inPuerto), stPr_UsuarioApp, stPr_ArchivoLog);
                //
                objL_Log_1.setTextErrLog("obtieneToken : entrando en metodo obtieneToken");
                //
                String stL_Usuario_Pandora = "";
                String stL_Clave_Usuario_Pandora = "";
                stL_Usuario_Pandora = this.Halla_Usuario_Pandora(st_Serv, St_inPuerto, ref stL_Clave_Usuario_Pandora);
                //
                objL_Log_1.setTextErrLog("obtieneToken : haciendo login en adirector");
                datos[0] = ObjL_ClienteTCP.SALogin(stL_Usuario_Pandora, stL_Clave_Usuario_Pandora);
                datos[1] = stL_Clave_Usuario_Pandora;
                objL_Log_1.setTextErrLog(datos[0] + datos[1]);
                objL_Log_1.setTextErrLog("obtieneToken : saliendo en metodo obtieneToken");
                //
                return datos;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "obtieneToken", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "obtieneToken", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return null;
            }
        }



        public Boolean Valida_Conexion_ADirector(String stPr_Serv, String StPr_inPuerto)
        {
            Boolean blL_ConexionOk = false;
            try
            {
                int inL_Puerto = Convert.ToInt16(StPr_inPuerto);
                ClasX_ClienteTCP ObjL_ClienteTCP = new ClasX_ClienteTCP(stPr_Serv, inL_Puerto, stPr_UsuarioApp, stPr_ArchivoLog);
                blL_ConexionOk = ObjL_ClienteTCP.testConexion();
                //
                return blL_ConexionOk;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Valida_Conexion_ADirector", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Valida_Conexion_ADirector", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        public String  Get_My_Ip(String stPr_Serv, String StPr_inPuerto)
        {
            String stL_My_IP = "";
            try
            {
                int inL_Puerto = Convert.ToInt16(StPr_inPuerto);
                ClasX_ClienteTCP ObjL_ClienteTCP = new ClasX_ClienteTCP(stPr_Serv, inL_Puerto, stPr_UsuarioApp, stPr_ArchivoLog);
                //
                stL_My_IP = ObjL_ClienteTCP.getMyIP();
                //
                return stL_My_IP;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Get_My_Ip", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Get_My_Ip", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }



        public String[] Manage_Template(String stL_Cedula, String stPr_Serv, String StPr_inPuerto, byte[] btL_template, ulong inPr_A001NUM_IDENTIFICACION)
        {
            try
            {
                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                ClasX_IdVerFinger ObjL_IdVerFinger = new ClasX_IdVerFinger(objL_Log_1, stPr_ExeName_Exe);
                //
                int inL_Puerto = Convert.ToInt16(StPr_inPuerto);
                ClasX_ClienteTCP ObjPr_ClienteTCP = new ClasX_ClienteTCP(stPr_Serv, inL_Puerto, stPr_UsuarioApp, stPr_ArchivoLog);
                //
                objL_Log_1.setTextErrLog("Manage_Template : enviando solicitud de verificacion para cc: " + Convert.ToString(inPr_A001NUM_IDENTIFICACION));
                //
                String[] stL_Respuesta = ObjL_IdVerFinger.SendTemplate(ObjPr_ClienteTCP, btL_template, stL_Cedula, ClasX_IdVerFinger.HUELLA);
                //
                Application.DoEvents();
                //
                return stL_Respuesta;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Get_My_Ip", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Get_My_Ip", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return null;
            }
        }


        public String  Write_Command_Srv(String stPr_Serv, String StPr_inPuerto, String st_Mensaje)
        {
            String stL_Mensaje_Srv = "";
            try
            {
                int inL_Puerto = Convert.ToInt16(StPr_inPuerto);
                ClasX_ClienteTCP ObjL_ClienteTCP = new ClasX_ClienteTCP(stPr_Serv, inL_Puerto, stPr_UsuarioApp, stPr_ArchivoLog);
                ClasX_Config ObjL_ConfigApp = new ClasX_Config(stPr_ArchivoConfApp, stPr_ArchivoLog, stPr_ArchivoLog);
                ClasX_Encripta objL_Encripta = new ClasX_Encripta(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                String temp = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
                String usrAdirector = ObjL_ConfigApp.LeeLlave_Seccion(temp, "U_ADIRECTOR");
                objL_Log_1.setTextErrLog("Write_Command_Srv : usuario: " + usrAdirector);
                //
                usrAdirector = objL_Encripta.DesEncriptaInfo(usrAdirector, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                objL_Log_1.setTextErrLog("Write_Command_Srv : usuario: " + usrAdirector);
                String pwAdirector = ObjL_ConfigApp.LeeLlave_Seccion(temp, "IO_ADIRECTOR");
                //
                objL_Log_1.setTextErrLog("Write_Command_Srv : pw: " + pwAdirector);
                pwAdirector = objL_Encripta.DesEncriptaInfo(pwAdirector, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                //
                stL_Mensaje_Srv = ObjL_ClienteTCP.SA_KSC(st_Mensaje, usrAdirector, pwAdirector);
                return stL_Mensaje_Srv;
                //
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Write_Command_Srv", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Write_Command_Srv", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }



        private String Halla_Usuario_Pandora(String st_Serv, String St_inPuerto, ref String stR_Clave_Usuario_Pandora)
        {
            String stL_Usuario = "";
            try
            {
                stR_Clave_Usuario_Pandora = "";
                ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                ClasX_Config ObjL_ConfigApp = new ClasX_Config(stPr_ArchivoConfApp, stPr_ArchivoLog, stPr_ArchivoLog);
                ClasX_Encripta objL_Encripta = new ClasX_Encripta(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log_1.setTextErrLog("Halla_Usuario_Pandora : entrando en metodo Halla_Usuario_Pandora");
                int inL_Puerto = Convert.ToInt16(St_inPuerto);
                ClasX_ClienteTCP ObjL_ClienteTCP = new ClasX_ClienteTCP(st_Serv, inL_Puerto, stPr_UsuarioApp, stPr_ArchivoLog);
                //
                String temp = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
                stL_Usuario = ObjL_ConfigApp.LeeLlave_Seccion(temp, "U_ADIRECTOR");
                //
                stL_Usuario = objL_Encripta.DesEncriptaInfo(stL_Usuario, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                //
                stR_Clave_Usuario_Pandora = ObjL_ConfigApp.LeeLlave_Seccion(temp, "IO_ADIRECTOR");
                stR_Clave_Usuario_Pandora = objL_Encripta.DesEncriptaInfo(stR_Clave_Usuario_Pandora, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                //
                objL_Log_1.setTextErrLog("Halla_Usuario_Pandora : saliendo en metodo Halla_Usuario_Pandora");
                return stL_Usuario;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Envia el ultimo parametro en FALSE, para que no salga por consola los errores.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Halla_Usuario_Pandora", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Halla_Usuario_Pandora", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }



    }
}
