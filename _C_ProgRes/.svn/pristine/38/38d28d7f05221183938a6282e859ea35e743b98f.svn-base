using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
//
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
//

namespace _C_ProgRes
{
    //////////////////////////////////////////////////////////////////
    // Clase Para manejo de las operaciones comunes a varias aplicaciones
    //////////////////////////////////////////////////////////////////
    public class ClasX_Utils
    {
        // APIS para hallar ventanas y cerrarlas.
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //
        /// <summary>
        /// Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        //
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        //
        const UInt32 WM_CLOSE = 0x0010;
        // ///////////////////////////////////////////////
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        //
        //////////////////////////////////////////////////////////////////////////////////////
        // Los Colores
        //////////////////////////////////////////////////////////////////////////////////////
        // Colores para los botones habilitados y no habilitados.
        private System.Drawing.Color RED_Color_Panel_Principal = System.Drawing.Color.Brown;
        private System.Drawing.Color RED_Color_Boton_Habilitado = System.Drawing.Color.IndianRed;
        private System.Drawing.Color RED_Color_Boton_DesHabilitado = System.Drawing.Color.DarkSalmon;
        private System.Drawing.Color RED_Color_Label_Error = System.Drawing.Color.Red;
        //
        private System.Drawing.Color BLUE_Color_Panel_Principal = System.Drawing.Color.DodgerBlue;
        private System.Drawing.Color BLUE_Color_Boton_Habilitado = System.Drawing.Color.SteelBlue;
        private System.Drawing.Color BLUE_Color_Boton_DesHabilitado = System.Drawing.Color.LightSteelBlue;
        private System.Drawing.Color BLUE_Color_Label_Error = System.Drawing.Color.Blue;
        //
        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_Ul ObjPr_Self = null;
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo

        /// <summary>
        /// Constructor de la clase, ClasX_Utils
        /// Sin parametros
        /// </summary>
        public ClasX_Utils ()
        {
           //
            try
            {
                //
                ObjPr_Self = new NBToolsNet.CLNBTN_Ul(stPr_Info);
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ClasX_Utils(1). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ClasX_Utils(1)", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
        }


        /// <summary>
        /// Contructor de la clase, ClasX_Utils
        /// Con parametros
        /// </summary>
        /// <param name="st_UsuarioApp">Usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre Archivo de log</param>
        public ClasX_Utils (String st_UsuarioApp, String st_ArchivoLog)
        {
            try
            {
                stPr_UsuarioAPP = st_UsuarioApp;
                stPr_ArchivoLog = st_ArchivoLog;
                ObjPr_Self = new NBToolsNet.CLNBTN_Ul(stPr_UsuarioAPP, stPr_ArchivoLog, stPr_Info);
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ClasX_Utils(2). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ClasX_Utils(2)", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
        }

        /// <summary>
        /// Constructor que recibe los parametros para el tratamiento del log
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo del usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path completo del archivo log</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_Utils(String st_UsuarioApp, String st_ArchivoLog, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            try
            {
                stPr_UsuarioAPP = st_UsuarioApp;
                stPr_ArchivoLog = st_ArchivoLog;
                //
                blPr_SalConsole = bl_SalidaConsola;
                blPr_SalLog = bl_SalidaLog;
                blPr_SalDialog = bl_SalidaDialogo;
                ObjPr_Self = new NBToolsNet.CLNBTN_Ul(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, bl_SalidaLog, bl_SalidaDialogo, stPr_Info);
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ClasX_Utils(3). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ClasX_Utils(3)", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
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
        /// Metodo : Convertir_Null
        /// Si el valor C es null, le asigna el valor de D
        /// De lo contrario le deja el mismo valor de C
        /// </summary>
        /// <param name="c">Objeto que entra el cual va a ser convenrtido en el objeto D si es null</param>
        /// <param name="d">Objeto a asignar si el objeto C es NULL</param>
        public object Convertir_Null(object c, object d) 
        {
            try
            {
                return ObjPr_Self.ConvertNull(c, d);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Convertir_Null System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return d;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Convertir_Null", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return d;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void ConvertirTipoInfoBd(ClasX_DBInfo r_Obj_InfoBD, ref NBToolsNet.CLNBTN_IQy O_Aux)
        {
            try
            {
                O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                //
                O_Aux.setServerName(r_Obj_InfoBD.getNombreServidor());
                O_Aux.setDataBaseName(r_Obj_InfoBD.getNombreBaseDatos());
                O_Aux.setDataBasePath(r_Obj_InfoBD.getRutaBD());
                O_Aux.setFileName_Access(r_Obj_InfoBD.getNombreArchivoAccess());
                O_Aux.setDataBase_UserID(r_Obj_InfoBD.getIdUsuario_BD());
                O_Aux.setDataBase_UserPWD(r_Obj_InfoBD.getClaveUsuario_BD());
                O_Aux.setServer_URL(r_Obj_InfoBD.getURL_BD());
                O_Aux.setServer_IP_Address(r_Obj_InfoBD.getIP_Address());
                //
                O_Aux.setUser(r_Obj_InfoBD.getUsuarioApp());
                O_Aux.setFileLog(r_Obj_InfoBD.getArchivoLog());
                O_Aux.setServer_Port(r_Obj_InfoBD.getPuertoBD());
                //
                O_Aux.setDataBase_UserPWD(r_Obj_InfoBD.getClaveUsuario_BD());
                O_Aux.setUserApp_PWD(r_Obj_InfoBD.getClave_UsuarioAPP());
                O_Aux.setUserApp_PWD_Enc(r_Obj_InfoBD.getClave_Encriptada_UsuarioAPP());
                O_Aux.setAccess_By_SSO(r_Obj_InfoBD.getAcceso_SSO());
                O_Aux.setUserApp_Login_ID(r_Obj_InfoBD.getID_Ingreso_Usuario_App());
                //
                O_Aux.setServer_Monitor(r_Obj_InfoBD.getServidorPandora());
                O_Aux.setServer_Monitor_Port(r_Obj_InfoBD.getPuertoPandora());
                O_Aux.setServer_Monitor_UID(r_Obj_InfoBD.getUsuario_Pandora());
                O_Aux.setServer_Monitor_UID_PWD(r_Obj_InfoBD.getClave_Usuario_Pandora());
                //
                NBToolsNet.CLNBTN_IQy.inDB_Types Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                switch (r_Obj_InfoBD.getTipoBD())
                {
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_ACCESS:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_ORACLE:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_MYSQL:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_SYBASE:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_POSTGRESQL:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL;
                        break;
                    default:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                        break;
                }
                O_Aux.setDataBaseEngine_Type(Aux);
                //
                NBToolsNet.CLNBTN_IQy.inConnect_Type Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                switch (r_Obj_InfoBD.getTipoConexion())
                {
                    case ClasX_DBInfo.inConnect_Type.TYPE_1_CONNECT_USER_SQL:
                        Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                        break;
                    case ClasX_DBInfo.inConnect_Type.TYPE_2_CONNECT_USER_APP:
                        Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_2_CONNECT_USER_APP;
                        break;
                    case ClasX_DBInfo.inConnect_Type.TYPE_3_CONNECT_USER_WIN:
                        Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN;
                        break;
                    case ClasX_DBInfo.inConnect_Type.TYPE_4_CONNECT_USER_INFO_FENIX:
                        Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT;
                        break;
                    case ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_PANDORA_BOX:
                        Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN;
                        break;
                    default:
                        Aux_1 = NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                        break;
                }
                O_Aux.setDataBaseConn_Type(Aux_1);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ConvertirTipoInfoBd System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ConvertirTipoInfoBd", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public void ConvertirTipoInfoBd(NBToolsNet.CLNBTN_IQy O_Aux , ref ClasX_DBInfo r_Obj_InfoBD )
        {
            try
            {
                r_Obj_InfoBD = new ClasX_DBInfo();
                //
                r_Obj_InfoBD.setNombreServidor(O_Aux.getServerName());
                r_Obj_InfoBD.setNombreBDSql(O_Aux.getDataBaseName());
                r_Obj_InfoBD.setRutaBD(O_Aux.getServerName());
                r_Obj_InfoBD.setNombreArchivoAccess(O_Aux.getFileName_Access());
                r_Obj_InfoBD.setIDUsuario_BD(O_Aux.getDataBase_UserID());
                r_Obj_InfoBD.setClaveUsuario_BD(O_Aux.getDataBase_UserPWD());
                r_Obj_InfoBD.setURL_BD(O_Aux.getServer_URL());
                r_Obj_InfoBD.setIP_Address(O_Aux.getServer_IP_Address());
                r_Obj_InfoBD.set_UsuarioApp(O_Aux.getUser());
                r_Obj_InfoBD.set_ArchivoLog(O_Aux.getFileLog());
                r_Obj_InfoBD.setPuertoBD(O_Aux.getServer_Port());
                //
                r_Obj_InfoBD.setClaveUsuario_BD(O_Aux.getDataBase_UserPWD());
                //
                r_Obj_InfoBD.set_Clave_UsuarioAPP(O_Aux.getUserApp_PWD());
                //
                r_Obj_InfoBD.set_Clave_Encriptada_UsuarioAPP(O_Aux.getUserApp_PWD_Enc());
                //
                r_Obj_InfoBD.set_Acceso_SSO(O_Aux.getAccess_By_SSO());
                r_Obj_InfoBD.setID_Ingreso_Usuario_App(O_Aux.getUserApp_Login_ID());
                //
                r_Obj_InfoBD.setServidorPandora(O_Aux.getServer_Monitor());
                r_Obj_InfoBD.setPuertoPandora(O_Aux.getServer_Monitor_Port());
                r_Obj_InfoBD.setUsuario_Pandora(O_Aux.getServer_Monitor_UID());
                r_Obj_InfoBD.setClave_Usuario_Pandora(O_Aux.getServer_Monitor_UID_PWD());
                //
                ClasX_DBInfo.inDB_Types Aux = ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER;
                switch (O_Aux.getDataBaseEngine_Type())
                {
                    case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                        Aux = ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS:
                        Aux = ClasX_DBInfo.inDB_Types.BD_TYPE_ACCESS;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE :
                        Aux = ClasX_DBInfo.inDB_Types.BD_TYPE_ORACLE;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL :
                        Aux = ClasX_DBInfo.inDB_Types.BD_TYPE_MYSQL;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE:
                        Aux = ClasX_DBInfo.inDB_Types.BD_TYPE_SYBASE;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL :
                        Aux = ClasX_DBInfo.inDB_Types.BD_TYPE_POSTGRESQL ;
                        break;
                    default:
                        Aux = ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER;
                        break;
                }
                r_Obj_InfoBD.setTipoMotorBD(Aux);
                //
                ClasX_DBInfo.inConnect_Type Aux_1 = ClasX_DBInfo.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                switch (O_Aux.get_DataBaseConn_Type())
                {
                    case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL:
                        Aux_1 = ClasX_DBInfo.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_2_CONNECT_USER_APP :
                        Aux_1 = ClasX_DBInfo.inConnect_Type.TYPE_2_CONNECT_USER_APP;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_3_CONNECT_USER_WIN:
                        Aux_1 = ClasX_DBInfo.inConnect_Type.TYPE_3_CONNECT_USER_WIN;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT :
                        Aux_1 = ClasX_DBInfo.inConnect_Type.TYPE_4_CONNECT_USER_INFO_FENIX;
                        break;
                    case NBToolsNet.CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN :
                        Aux_1 = ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_PANDORA_BOX;
                        break;
                    default:
                        Aux_1 = ClasX_DBInfo.inConnect_Type.TYPE_1_CONNECT_USER_SQL;
                        break;
                }
                r_Obj_InfoBD.setTipoConexion(Aux_1);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ConvertirTipoInfoBd(2) System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ConvertirTipoInfoBd(2)", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        ///  <summary>
        /// Metodo : st_Date_4_Query
        /// Devuelve la fecha para el formato de los queries
        /// Devuelve la Fecha en Formato AAAAMMDD para SQL Server
        /// para los demas tipos de servidores la devuelve AAAA-MM-DD
        /// </summary>
        /// <param name="in_TipoServidor">Tipo de motor de la base de datos</param>
        /// <param name="stRFechaEntra">Fecha que se necesita convertir</param>
        /// <param name="inRFormatoFecha">Formato de la fecha que se maneja en la aplicacion</param>
        /// <returns>La en el formato que la entiene el motor de la base de datos para ejecutar los queries</returns>
        public String st_Date_4_Query(ClasX_DBInfo.inDB_Types in_TipoServidor, String stRFechaEntra, ClasX_Constans.inDB_DateFormats inRFormatoFecha = ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_DD_MM_AAAA )
        {
            try
            {
                NBToolsNet.CLNBTN_IQy.inDB_Types Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                switch (in_TipoServidor)
                {
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_ACCESS:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_ORACLE:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_MYSQL:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_SYBASE:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_POSTGRESQL:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL;
                        break;
                    default:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                        break;
                }
                int Aux2 = 0 ;
                switch(inRFormatoFecha)
                {
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_DD_MM_AAAA:
                        Aux2 = 0 ;
                        break;
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_AAAA_MM_DD :
                        Aux2 = 1 ;
                        break;
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_MM_DD_AAAA:
                        Aux2 = 2 ;
                        break;
                    default:
                        Aux2 = 0 ;
                        break;
                }
                return ObjPr_Self.ConvertDate2Query(Aux,stRFechaEntra, Aux2);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_Date_4_Query. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            { 
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_Date_4_Query", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }
        //

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : stFechaServidor
        /// Devuelve la fecha del servidor de la base de datos
        /// </summary>
        /// <param name="r_Obj_InfoBD"></param>
        /// <param name="bRFormatoYYYYMMDD"></param>
        /// <param name="bRConHora"></param>
        /// <returns></returns>
        public String stFechaServidor(ClasX_DBInfo r_Obj_InfoBD, bool bRFormatoYYYYMMDD = false , bool bRConHora = false)
        { // Inicio del public String stFechaServidor(C
            try
            {
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                this.ConvertirTipoInfoBd(r_Obj_InfoBD, ref O_Aux);
                //
                return ObjPr_Self.BringMeServerDate(O_Aux, bRFormatoYYYYMMDD, bRConHora);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "stFechaServidor. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            { 
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "stFechaServidor", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        } // fin del public String stFechaServidor(C



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ShowMessage
        /// Presenta una ventana con el error presentado.
        /// </summary>
        /// <param name="st_Titulo">Titulo para la ventana</param>
        /// <param name="st_Message">Mensaje a presentar en la ventana.</param>
        /// <param name="stMensaje2">Mensaje adicional a presentar en la ventana.</param>
        public void ShowMessage(string st_Titulo, string st_Message, String stMensaje2)
        {
            //
            try
            {
                MessageBox.Show(st_Message + stMensaje2, st_Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowMessage. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowMessage", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ShowMessageError
        /// Presenta una ventana con el mensaje de error presentado. 
        /// Generalmente generado por la clase de manejo de log.
        /// </summary>
        /// <param name="st_Titulo">Titulo de la ventana </param>
        /// <param name="stMensaje2">Mensaje adicional  </param>
        /// <param name="st_Componente">Componente o ejecutable que genera el error.</param>
        /// <param name="st_ClaseMod">Modulo o clase donde se genera el error.</param>
        /// <param name="st_Metodo">Método o propiedad donde se genera el error.</param>
        /// <param name="st_CodigoErr">Código de error generado.</param>
        /// <param name="st_MessaDesc">Descripción del error generado.</param>
        /// <param name="st_BD">Nombre de la base de datos, donde se genera el error, cuando se ha ejecutado alguna operación sobre la base de datos.</param>
        /// <param name="st_InstSQL">Instrucción SQL que genero el error.</param>
        public void ShowMessageError(String st_Titulo, String stMensaje2, String st_Componente, String st_ClaseMod, String st_Metodo, String st_CodigoErr, String st_MessaDesc, String st_BD = "", String st_InstSQL = "")
        {
            FrmMensajeError Forma = new FrmMensajeError();
            try
            {
                Forma.TomaParametros(stPr_UsuarioAPP , stPr_ArchivoLog , st_Titulo,  stMensaje2,  st_Componente,  st_ClaseMod,  st_Metodo,  st_CodigoErr,  st_MessaDesc,  st_BD ,  st_InstSQL);
                Forma.ShowDialog();
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowMessageError. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowMessageError", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Propiedad : get_WindowsUserName
        /// Devuelve el usuario de windows
        /// </summary>
        /// <returns>stL_WinUser = SystemInformation.UserName;</returns>
        public String get_WindowsUserName() 
        { // inicio del public void get_WindowsUserName() 
            String stL_WinUser = "";
            try
            {
                stL_WinUser = ObjPr_Self.BringMeWinUserName();
                return stL_WinUser;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                //
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "get_WindowsUserName. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                //
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "get_WindowsUserName", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return stL_WinUser;
        } // fin del public void get_WindowsUserName() 
        //


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : inDiferencia_Fechas
        /// Halla la diferencia en dias, meses o años, entre dos fechas,
        /// </summary>
        /// <param name="st_Fecha1">Fecha 1 A hallar diferencia</param>
        /// <param name="st_Fecha2">Fecha 2 A hallar diferencia</param>
        /// <param name="st_TipoDiferencia">Tipo de Diferencia a hallar: "D" = Dias, "M" = Meses, "A"= Años</param>
        /// <param name="inRFormatoFecha">Formato de la fecha, por defecto = 0 MM/DD/AAAA</param>
        /// <returns></returns>
        public int inDiferencia_Fechas(String st_Fecha1, String st_Fecha2, String st_TipoDiferencia, ClasX_Constans.inDB_DateFormats inRFormatoFecha = ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_DD_MM_AAAA)
        {
            int inL_Diferencia = 0;
            //
            try
            {
                int Aux2 = 0 ;
                switch(inRFormatoFecha)
                {
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_DD_MM_AAAA:
                        Aux2 = 0 ;
                        break;
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_AAAA_MM_DD :
                        Aux2 = 1 ;
                        break;
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_MM_DD_AAAA:
                        Aux2 = 2 ;
                        break;
                    default:
                        Aux2 = 0 ;
                        break;
                }
                inL_Diferencia = ObjPr_Self.BringMe_DifDates(st_Fecha1, st_Fecha2, st_TipoDiferencia, Aux2);
                //
                return inL_Diferencia;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                inL_Diferencia = 0;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "inDiferencia_Fechas. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                inL_Diferencia = 0;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "inDiferencia_Fechas", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return inL_Diferencia;
        }
        //


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : setColor_Boton_Habilitado
        /// Cambia el color de un boton de comando cuando esta habilitadpo
        /// dependiendo del esquema de color.
        /// </summary>
        /// <param name="CtrlBoton">Boton de Comando a cambiar el backcolor</param>
        /// <param name="inEsquemaColor">Esquema de color, Rojo, Azul, etc...</param>
        public void setColor_Boton_Habilitado(Button CtrlBoton, ClasX_Constans.inEsquema_Colores inEsquemaColor = ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO)
        {
            try
            {
                switch ( inEsquemaColor ) 
                {
                    case ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO:
                        //
                        CtrlBoton.BackColor = RED_Color_Boton_Habilitado;
                        break;
                    case ClasX_Constans.inEsquema_Colores.ESQUEMA_COLOR_AZUL:
                        //
                        CtrlBoton.BackColor = BLUE_Color_Boton_Habilitado;
                        break;
                    default:
                        // Por Defecto el rojo
                        CtrlBoton.BackColor = RED_Color_Boton_Habilitado;
                        break;
                }

            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Boton_Habilitado. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Boton_Habilitado", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : setColor_Boton_DesHabilitado
        /// Cambia el backcolor de un  boton de comando cuando esta deshabilitado.
        /// </summary>
        /// <param name="CtrlBoton">Boton de comando a cambiar el backcolor</param>
        /// <param name="inEsquemaColor">Esquema de Colors, Rojo, Azul, etc...</param>
        public void setColor_Boton_DesHabilitado(Button CtrlBoton, ClasX_Constans.inEsquema_Colores inEsquemaColor = ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO)
        {
            try
            {
                switch (inEsquemaColor)
                {
                    case ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO:
                        //
                        CtrlBoton.BackColor = RED_Color_Boton_DesHabilitado;
                        break;
                    case ClasX_Constans.inEsquema_Colores.ESQUEMA_COLOR_AZUL:
                        //
                        CtrlBoton.BackColor = BLUE_Color_Boton_DesHabilitado;
                        break;
                    default:
                        // Por Defecto el rojo
                        CtrlBoton.BackColor = RED_Color_Boton_DesHabilitado;
                        break;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Boton_DesHabilitado. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Boton_DesHabilitado", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : setColor_Boton_XEstado
        /// Cambia el color del boton de acuerdo al estado del boton,si esta habilitado o no.
        /// </summary>
        /// <param name="CtrlBoton">Boton de comando a cambiar el backcolor</param>
        /// <param name="inEsquemaColor">Esquema de Colors, Rojo, Azul, etc...</param>
        public void setColor_Boton_XEstado(Button CtrlBoton, ClasX_Constans.inEsquema_Colores inEsquemaColor = ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO)
        {
            // Cambia el Color del boton de acuerdo al estado del mismo boton
            try
            {
                if (CtrlBoton.Enabled)
                {
                    //
                    switch (inEsquemaColor)
                    {
                        case ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO:
                            //
                            CtrlBoton.BackColor = RED_Color_Boton_Habilitado;
                            break;
                        case ClasX_Constans.inEsquema_Colores.ESQUEMA_COLOR_AZUL:
                            //
                            CtrlBoton.BackColor = BLUE_Color_Boton_Habilitado;
                            break;
                        default:
                            // Por Defecto el rojo
                            CtrlBoton.BackColor = RED_Color_Boton_Habilitado;
                            break;
                    }
                }
                else
                {
                    switch (inEsquemaColor)
                    {
                        case ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO:
                            //
                            CtrlBoton.BackColor = RED_Color_Boton_DesHabilitado;
                            break;
                        case ClasX_Constans.inEsquema_Colores.ESQUEMA_COLOR_AZUL:
                            //
                            CtrlBoton.BackColor = BLUE_Color_Boton_DesHabilitado;
                            break;
                        default:
                            // Por Defecto el rojo
                            CtrlBoton.BackColor = RED_Color_Boton_DesHabilitado;
                            break;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Boton_XEstado. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Boton_XEstado", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : setColor_Panel_PPal
        /// cambia el ForeColor de un panel de acuerdo al patron de colores que esta trabajando.
        /// </summary>
        /// <param name="CtrlPanel">El pabel a camiar el ForeColor</param>
        /// <param name="inEsquemaColor">Esquema de Colors, Rojo, Azul, etc...</param>
        public void setColor_Panel_PPal(Panel CtrlPanel, ClasX_Constans.inEsquema_Colores inEsquemaColor = ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO)
        {
            try
            {
                
                switch (inEsquemaColor)
                {
                    case ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO:
                        //
                        CtrlPanel.BackColor = RED_Color_Panel_Principal;
                        break;
                    case ClasX_Constans.inEsquema_Colores.ESQUEMA_COLOR_AZUL:
                        //
                        CtrlPanel.BackColor = BLUE_Color_Panel_Principal;
                        break;
                    default:
                        // Por Defecto el rojo
                        CtrlPanel.BackColor = RED_Color_Panel_Principal;
                        break;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Panel_PPal. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Panel_PPal", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : setColor_Label_Error
        /// Cambia el backcolor de un label, para presentar mensajes de error
        /// deopendiendo del esquema del color
        /// </summary>
        /// <param name="CtrlLabel">Label a Cambiar el backcolor</param>
        /// <param name="inEsquemaColor">Esquema de color, Rojo, Azul, etc...</param>
        public void setColor_Label_Error(Label CtrlLabel, ClasX_Constans.inEsquema_Colores inEsquemaColor = ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO)
        {
            try
            {

                switch (inEsquemaColor)
                {
                    case ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO:
                        //
                        CtrlLabel.ForeColor = RED_Color_Label_Error;
                        break;
                    case ClasX_Constans.inEsquema_Colores.ESQUEMA_COLOR_AZUL:
                        //
                        CtrlLabel.ForeColor = BLUE_Color_Label_Error;
                        break;
                    default:
                        // Por Defecto el rojo
                        CtrlLabel.ForeColor = RED_Color_Label_Error;
                        break;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Label_Error. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "setColor_Label_Error", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : QuitaEspaciosAdicionales
        /// Quita Espacios adicionales de la cadena que entra como parametro
        /// </summary>
        /// <param name="st_Cadena">Cadena a la cual se le va a quitar los espacios adicionales</param>
        /// <returns>Devuelve la cadena sin los espacios adicionales</returns>
        public String QuitaEspaciosAdicionales(String st_Cadena)
        {
            // quita espacios adicionales
            String stL_CadenaFuera = "";
            try
            {
                stL_CadenaFuera = ObjPr_Self.GetOff_SpacesAdic(st_Cadena);
                return stL_CadenaFuera;
            }
            catch (System.AccessViolationException ex_0)
            {
                stL_CadenaFuera = "";
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "QuitaEspaciosAdicionales. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                stL_CadenaFuera = "";
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "QuitaEspaciosAdicionales", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return stL_CadenaFuera;
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : QuitaCaracterDeLaCadena
        /// Quita un caracter de una cadena.
        /// Quita todas las apariciones de la cadena que entra como parametro, st_CaracterFuera.
        /// </summary>
        /// <param name="st_Cadena">String sobre la cual se va a eliminar el caracter</param>
        /// <param name="st_CaracterFuera">El caracter que se debe eliminar.</param>
        /// <returns>Devuelve la cadena sin el caracter</returns>
        public String QuitaCaracterDeLaCadena(String st_Cadena, String st_CaracterFuera)
        {
            // quita espacios adicionales
            String stL_CadenaFuera = "";
            try
            {
                stL_CadenaFuera = ObjPr_Self.GetOff_CharFromString(st_Cadena, st_CaracterFuera);
                return stL_CadenaFuera;
            }
            catch (System.AccessViolationException ex_0)
            {
                stL_CadenaFuera = "";
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "QuitaCaracterDeLaCadena. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                stL_CadenaFuera = "";
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "QuitaCaracterDeLaCadena", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return stL_CadenaFuera;
        }
        ///////////////////////////////////////////////////////////////


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : stFormatea_A_Moneda
        /// Da formato de numero o moneda  un string con un valor.
        /// Ejemplo : 
        /// st_Valor = "14180765.38"
        /// Se le cambia el punto por la coma :
        /// st_Valor = "14180765,38"
        /// Se convierte a Decimal :
        /// dlL_repNumerica = 14180765.38
        /// Se formatea Con signo Dollar:
        /// stL_SalidaMoneda = "$14,180,765.38"
        /// Se formatea Sin Signo Dollar:
        /// stL_SalidaMoneda = "14,180,765.38"
        /// </summary>
        /// <param name="st_Valor">Valor a formatear</param>
        /// <param name="bl_RConSimboloDollar">True = Le coloca signo dollar ( $ ) al string de salida.</param>
        /// <returns></returns>
        public String stFormatea_A_Moneda(String st_Valor, Boolean bl_RConSimboloDollar = false)
        {
            try
            {
                return  ObjPr_Self.BringMe_CurrencyFormat(st_Valor, bl_RConSimboloDollar);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "stFormatea_A_Moneda. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "stFormatea_A_Moneda", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : st_aMoneda
        /// Formatea un string a Moneda.
        /// </summary>
        /// <param name="st_CadenaNumero">String a Formatear</param>
        /// <returns> Devuelve el campo formateado en Moneda.</returns>
        public String st_aMoneda(String st_CadenaNumero)
        {
            try
            {
                return ObjPr_Self.BringMe_CoinFormat(st_CadenaNumero);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_aMoneda. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_aMoneda", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : st_completaTexto
        /// Sobre Carga 1
        /// justificar hacia la derecha un texto
        ///  Ejemplo:
        ///  Texto de Entrada : xxdffuou880:80 
        ///  Texto de Salida  : xxdffuou880:                           80
        /// </summary>
        /// <param name="st_texto">Texto a completar</param>
        /// <param name="in_MaxLongitud">Longitud Maxima con la cual va a trabajar. Por defecto 40</param>
        /// <param name="st_CaracterSeparacion">Caracter de separacion. Por defecto ":"</param>
        /// <returns>Devuelve el texto en el formato requerido</returns>
        public String st_completaTexto(String st_texto, int in_MaxLongitud = 40, String st_CaracterSeparacion = ":")
        {
            try
            {
                return ObjPr_Self.BringMe_CompleteText(st_texto,in_MaxLongitud,st_CaracterSeparacion);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_completaTexto_1. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return null;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_completaTexto_1", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : st_completaTexto
        /// Sobre Carga 2.
        /// justificar hacia la derecha dos textos
        /// Si la longitud del texto 2, lo manda en una nueva linea.
        /// Ejemplo :
        /// st_texto1 : xxdffuou880:80
        /// st_texto2 : cxvcxvjsfjsldfjsldskjflsdjf99
        /// Salida : 
        /// xxdffuou880:80
        /// cxvcxvjsfjsldfjsldskjflsdjf99
        /// </summary>
        /// <param name="st_texto1">Primera Parte del Texto</param>
        /// <param name="st_texto2">Segunda Parte del texto</param>
        /// <param name="in_MaxLongitud">Longitud Maxima con la cual va a trabajar. Por defecto 40</param>
        /// <returns>Devuelve el string de acuerdo al formato requerido</returns>
        public String st_completaTexto(String st_texto1, String st_texto2, int in_MaxLongitud = 40)
        {
            String textosalida = st_texto1 + " " + st_texto2;
            try
            {
                return ObjPr_Self.BringMe_CompleteText(st_texto1, st_texto2, in_MaxLongitud);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_completaTexto_2. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return null;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_completaTexto_2", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : st_ajustarLongitud
        /// Ajusta un texto a una longitud dada.
        /// da salto de linea si el texto es mas largo que la longitud
        /// </summary>
        /// <param name="st_texto">Texto a Ajustar</param>
        /// <param name="in_MaxLongitud">Longitud Maxima con la cual va a trabajar. Por defecto 40</param>
        /// <returns>Devuelve el string en el formato requerido</returns>
        public String st_ajustarLongitud(String st_texto, int in_MaxLongitud = 40)
        {
            try
            {
                return ObjPr_Self.BringMe_LenghAjusted(st_texto, in_MaxLongitud);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_ajustarLongitud. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_ajustarLongitud", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : st_EliminaAcento
        /// Elimina los acentos de la cadena.
        /// </summary>
        /// <param name="st_cadena">Cadena a la cual se eliminan los acentos</param>
        /// <returns>Devuelve la cadena sin acentos</returns>
        public String st_EliminaAcento(String st_cadena)
        {
            String stL_retorno = st_cadena;
            try
            {
                stL_retorno = ObjPr_Self.GetOff_Acent(st_cadena);
                return stL_retorno;
            }
            catch (System.AccessViolationException ex_0)
            {
                stL_retorno = st_cadena;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_EliminaAcento. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return stL_retorno;
            }
            catch (Exception ex)
            {
                stL_retorno = st_cadena;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_EliminaAcento", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return stL_retorno;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Halla_Nombre_IP_Maquina
        /// Halla el nombre y la direccion IP de la maquina.
        /// </summary>
        /// <param name="st_Maquina">Devuelve el nombre de la maquina</param>
        /// <param name="st_IPAdress">Devuelve la Ip de la maquina</param>
        public void Halla_Nombre_IP_Maquina(ref String st_Maquina, ref String st_IPAdress)
        { // inicio del public void Halla_Nombre_IP_Maquina(
            // Halla el nombre de la maquina y la IP de la maquina.
            try
            {
                st_Maquina = "";
                st_IPAdress = "";
                //
                ObjPr_Self.BringMe_MachineName_IpAdd(ref st_Maquina, ref st_IPAdress);
            
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Halla_Nombre_IP_Maquina. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Halla_Nombre_IP_Maquina", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // fin del public void Halla_Nombre_IP_Maquina(
        ///////////////////////////////////////////////////////////////


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Config_ComboBox
        /// Encargado de Configurar un ComboBox, para trabajar con la base de datos
        /// Dependiendo de la tabla y query que se envia como parametro.
        /// </summary>
        /// <param name="ControlComboBox">Control Combox a Configurar</param>
        /// <param name="ObjPr_Info_BD">Objeto con la informacion de la base de datos con la cual va a trabajar.</param>
        /// <param name="st_ConnectionString">String de Conexion para el ComboBox</param>
        /// <param name="st_Tabla">Nombre de la tabla sobre la cual va a hacer la seccion</param>
        /// <param name="st_CampoCod1">Campo Codigo para hacer la seleccion</param>
        /// <param name="st_CampoDesc1">Campo Descripcion a presentar en el combo</param>
        /// <param name="st_CampoCod2">Campo Codigo 2 para hacer la seleccion</param>
        /// <param name="st_CampoDesc2">Campo Descripcion 2 a presentar en el combo</param>
        /// <param name="stL_Condicion">Condicion para la seleccion de los datos</param>
        /// <param name="st_InsturccionSQL">Instruccion SQL que se ejecuta directamente</param>
        public void Config_ComboBox(ComboBox ControlComboBox,ClasX_DBInfo ObjPr_Info_BD, ref String st_ConnectionString, String st_Tabla, String st_CampoCod1, String st_CampoDesc1, String st_CampoCod2 = "", String st_CampoDesc2 = "", String stL_Condicion = "", String st_InsturccionSQL = "")
        {
            try
            {
                //
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                this.ConvertirTipoInfoBd(ObjPr_Info_BD, ref O_Aux);
                //
                ObjPr_Self.SetComboBox_Conf(ControlComboBox, O_Aux, ref  st_ConnectionString, st_Tabla, st_CampoCod1, st_CampoDesc1, st_CampoCod2, st_CampoDesc2, stL_Condicion, st_InsturccionSQL);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Config_ComboBox(1). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Config_ComboBox(1)", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Config_ComboBox
        /// Sobre Carga 2 
        /// Encargado de Configurar un ComboBox, para trabajar con la base de datos
        /// Dependiendo de la tabla y query que se envia como parametro.
        /// </summary>
        /// <param name="DatTable">Devuelve el Data Table,DataTable , con los datos del query</param>
        /// <param name="ControlComboBox">Control Combox a Configurar</param>
        /// <param name="ObjPr_Info_BD">Objeto con la informacion de la base de datos con la cual va a trabajar.</param>
        /// <param name="st_ConnectionString">String de Conexion para el ComboBox</param>
        /// <param name="st_Tabla">Nombre de la tabla sobre la cual va a hacer la seccion</param>
        /// <param name="st_CampoCod1">Campo Codigo para hacer la seleccion</param>
        /// <param name="st_CampoDesc1">Campo Descripcion a presentar en el combo</param>
        /// <param name="st_CampoCod2">Campo Codigo 2 para hacer la seleccion</param>
        /// <param name="st_CampoDesc2">Campo Descripcion 2 a presentar en el combo</param>
        /// <param name="stL_Condicion">Condicion para la seleccion de los datos</param>
        /// <param name="st_InsturccionSQL">Instruccion SQL que se ejecuta directamente</param>
        public void Config_ComboBox(ref DataTable DatTable, ComboBox ControlComboBox,ClasX_DBInfo ObjPr_Info_BD, ref String st_ConnectionString, String st_Tabla, String st_CampoCod1, String st_CampoDesc1, String st_CampoCod2 = "", String st_CampoDesc2 = "", String stL_Condicion = "", String st_InsturccionSQL = "")
        {
            try
            {
                //
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                this.ConvertirTipoInfoBd(ObjPr_Info_BD, ref O_Aux);
                //
                ObjPr_Self.SetComboBox_Conf(ref DatTable , ControlComboBox, O_Aux, ref  st_ConnectionString, st_Tabla, st_CampoCod1, st_CampoDesc1, st_CampoCod2, st_CampoDesc2, stL_Condicion, st_InsturccionSQL);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Config_ComboBox(2). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Config_ComboBox(2)", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Limpia_Controles_Forma
        ///  Limpia el contenido de los controles de una forma
        /// Aplica para controles tipo:
        /// TexBox       : TextBox
        /// CombosBox    : ComboBox
        /// Campos Fecha : DateTimePicker
        /// </summary>
        /// <param name="Forma">Forma o Control Contenedor a los cuales se les va a limpiar los controles</param>
        /// <param name="bl_LimpiaCombos">True = Limpia los ComboBox</param>
        /// <param name="bl_LimpiaCamposFecha">True = Limpia los Campos Fecha</param>
        /// <param name="st_CampoFecha">Valor a asignar a los campos fecha</param>
        /// <param name="st_CamposExcluidos">Array con el nombre de los controles a los cuales no se les va a limpiar el valor</param>
        public void Limpia_Controles_Forma(Control Forma, Boolean bl_LimpiaCombos, Boolean bl_LimpiaCamposFecha, String st_CampoFecha, string[] st_CamposExcluidos)
        {
            try
            {
                ObjPr_Self.SetCleanValuesControlForm(Forma,  bl_LimpiaCombos,  bl_LimpiaCamposFecha,  st_CampoFecha,  st_CamposExcluidos);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Limpia_Controles_Forma. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Limpia_Controles_Forma", "", ex.Message.ToString() , "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : stFormatea_A_Entero
        /// Formatea un numero entero con comas, asi:
        /// Entrada = "2345679086543"
        /// Salida  = "2,345,679,086,543"
        /// </summary>
        /// <param name="st_Valor">Numero a dar el formato</param>
        /// <returns></returns>
        public String stFormatea_A_Entero(String st_Valor)
        {
            try
            {
                return ObjPr_Self.BringMe_An_Int_Value(st_Valor);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "stFormatea_A_Entero. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "stFormatea_A_Entero", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }



        [HandleProcessCorruptedStateExceptions]
        ///  <summary>
        /// Metodo : st_DateTime_4_Query
        /// Devuelve la fecha para el formato de los queries
        /// Devuelve la Fecha en Formato AAAAMMDD para SQL Server
        /// para los demas tipos de servidores la devuelve AAAA-MM-DD
        /// Con la HORA
        /// </summary>
        /// <param name="in_TipoServidor">Tipo de motor de la base de datos</param>
        /// <param name="stRFechaEntra">Fecha que se necesita convertir</param>
        /// <param name="inRFormatoFecha">Formato de la fecha que se maneja en la aplicacion</param>
        /// <param name="bl_TomaHora = false">Parametro opcional. si viene en TRUE, se toma la hora de la fecha.</param>
        /// <returns>La fecha y hora en el formato que la entiene el motor de la base de datos para ejecutar los queries</returns>
        public String st_DateTime_4_Query(ClasX_DBInfo.inDB_Types in_TipoServidor, String stRFechaEntra, ClasX_Constans.inDB_DateFormats inRFormatoFecha = ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_DD_MM_AAAA)
        {
            try
            {
                NBToolsNet.CLNBTN_IQy.inDB_Types Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                switch (in_TipoServidor)
                {
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_ACCESS:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ACCESS;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_ORACLE:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_ORACLE;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_MYSQL:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_SYBASE:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SYBASE;
                        break;
                    case ClasX_DBInfo.inDB_Types.BD_TYPE_POSTGRESQL:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL;
                        break;
                    default:
                        Aux = NBToolsNet.CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
                        break;
                }
                int Aux2 = 0 ;
                switch(inRFormatoFecha)
                {
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_DD_MM_AAAA:
                        Aux2 = 0 ;
                        break;
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_AAAA_MM_DD :
                        Aux2 = 1 ;
                        break;
                    case ClasX_Constans.inDB_DateFormats.BD_DATE_FORMAT_MM_DD_AAAA:
                        Aux2 = 2 ;
                        break;
                    default:
                        Aux2 = 0 ;
                        break;
                }
                return ObjPr_Self.ConvertDateTime2Query(Aux, stRFechaEntra, Aux2);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_DateTime_4_Query. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_DateTime_4_Query", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return "";
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Elimina_Directorio
        /// Encargado de eliminar un directorio.
        /// </summary>
        /// <param name="st_Directorio">Directorio a Eliminar.</param>
        /// <param name="bl_SalDialog">Indica si presenta la salida del error en la pantalla. Por defecto en False</param>
        /// <returns>True = Si elimino el directorio</returns>
        public Boolean Elimina_Directorio(String st_Directorio, Boolean bl_SalDialog = false)
        {
            try
            {
                return ObjPr_Self.Let_Delete_A_Dir(st_Directorio, bl_SalDialog);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                //
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Directorio. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Directorio. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                //
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, bl_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Directorio", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Directorio", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Copia_Directorio
        /// Encargado de copiar lo archivos del Directorio de Destino al Directorio de Destino
        /// </summary>
        /// <param name="st_DirectorioOrigen">Directorio de Origen</param>
        /// <param name="st_DirectorioDestino">Directorio de Destino</param>
        /// <param name="bl_EliminaDir_Origen">True = Indica que debe eliminar el directorio de Origen.</param>
        /// <param name="bl_SalDialog">Indica si presenta la salida del error en la pantalla. Por defecto en False</param>
        /// <returns></returns>
        public Boolean Copia_Directorio(String st_DirectorioOrigen, String st_DirectorioDestino, Boolean bl_EliminaDir_Origen = false, Boolean bl_SalDialog = false)
        {
            try
            {
                return ObjPr_Self.Let_Copy_Dir(st_DirectorioOrigen, st_DirectorioDestino, bl_EliminaDir_Origen, bl_SalDialog);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // 
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Copia_Directorio. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Copia_Directorio. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                // 
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, bl_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Copia_Directorio", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Copia_Directorio", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Elimina_Archivos_Directorio
        /// Encargado de eliminar los archivos de un directorio.
        /// Recibe un arreglo de tipo string, con el nombre de los archivos que no debe eliminar.
        /// </summary>
        /// <param name="st_DirectorioOrigen">Ruta completa del directorio de origen</param>
        /// <param name="st_ArchivosExcluidos">Arreglo de String con los nombres de los archivos que NO debe eliminar. Archivos Excluidos</param>
        /// <param name="bl_SalDialog"></param>
        /// <returns>Devuelve TRUE si pudo realizar la operacion</returns>
        public Boolean Elimina_Archivos_Directorio(String st_DirectorioOrigen, string[] st_ArchivosExcluidos , Boolean bl_SalDialog = false)
        {
            try
            {
                return ObjPr_Self.Let_Delete_DirFiles(st_DirectorioOrigen, st_ArchivosExcluidos, bl_SalDialog);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // 
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Archivos_Directorio", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Archivos_Directorio", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                // 
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, bl_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Archivos_Directorio", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Archivos_Directorio", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : LeerArchivoTexto
        /// Lee un archivo Texto y devuelve el contenido en un arreglo de bytes
        /// </summary>
        /// <param name="st_FileName_Texto">Ruta y nombe de archvo texto.</param>
        /// <returns></returns>
        public String LeerArchivoTexto(String st_FileName_Texto)
        {
            try
            {
                return ObjPr_Self.Let_ReadTextFile(st_FileName_Texto);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_FileName_Texto. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_FileName_Texto. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_FileName_Texto", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "st_FileName_Texto", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Crear_PDF_Basado_Texto
        /// Crea un archivo PDF basado en un archivo .TXT
        /// </summary>
        /// <param name="st_FileName_Texto">Ruta y nombre del archivo de texto.</param>
        /// <param name="st_FileName_Pdf">Ruta y nombre del archivo .PDF</param>
        /// <returns></returns>
        public Boolean Crear_PDF_Basado_Texto(String st_FileName_Texto, String st_FileName_Pdf)
        {
            try
            {
                return ObjPr_Self.Let_Create_PDF_From_TextFile(st_FileName_Texto, st_FileName_Pdf);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Crear_PDF_Basado_Texto. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Crear_PDF_Basado_Texto. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Crear_PDF_Basado_Texto", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Crear_PDF_Basado_Texto", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Valida_Conexion_Bd
        /// Valida si hay conexion con una base de datos ejecutnado un query
        /// </summary>
        /// <param name="ObjPr_Info_BD"> ClasX_DBInfo ObjPr_Info_BD : Informacion de la base de datos</param>
        /// <param name="st_NombreTabla">Nombre de la tabla sobre la cual se va a hacer el query. Si viene vacia se utiliza la tabla "T01GRUPOS".</param>
        /// <returns>true = si se pudo conectar y ejecutar a la base de datos</returns>
        public Boolean Valida_Conexion_Bd( ClasX_DBInfo ObjPr_Info_BD, String st_NombreTabla = "")
        {
            try
            {
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                this.ConvertirTipoInfoBd(ObjPr_Info_BD, ref O_Aux);
                //
                return ObjPr_Self.BringMe_DB_Conn_Status(O_Aux, st_NombreTabla);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // IMPORTANTE : // Crea la instancia de la clase y que no presente el error en la pantalla, para poderlo evaluar.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Valida_Conexion_Bd. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Valida_Conexion_Bd. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                // IMPORTANTE : // Crea la instancia de la clase y que no presente el error en la pantalla, para poderlo evaluar.
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, false);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Valida_Conexion_Bd", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Valida_Conexion_Bd", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : CierraVentana_X_Caption
        /// Cierra una ventana ubicando la por el caption
        /// Llama APIS para hacer esta operacion.
        /// </summary>
        /// <param name="st_Caption">Caption o titulo de ventana a cerrar.</param>
        public void CierraVentana_X_Caption(String st_Caption)
        {
            try
            {
                IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, st_Caption);
                //
                if (windowPtr == IntPtr.Zero)
                {
                    //Console.WriteLine("Window not found");
                    //return;
                }
                else
                {
                    //
                    SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "CierraVentana_X_Caption. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "CierraVentana_X_Caption. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
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
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "CierraVentana_X_Caption", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "CierraVentana_X_Caption", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : HallaVentana_X_Caption
        /// Encargado de "cerrar" una ventana ubicandola por el titulo o el caption de la forma
        /// Llama APIS para hacer esta operacion.
        /// </summary>
        /// <param name="st_Caption">Titulo o Caption d ela ventana a cerrar</param>
        /// <returns>TRUE Si pudo ubicar y cerrar la ventana</returns>
        public Boolean HallaVentana_X_Caption(String st_Caption)
        {
            Boolean blL_HalloVentana = false;
            try
            {
                IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, st_Caption);
                //
                if (windowPtr != IntPtr.Zero)
                {
                    blL_HalloVentana = true;
                }
                return blL_HalloVentana;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "HallaVentana_X_Caption. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "HallaVentana_X_Caption. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "HallaVentana_X_Caption", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "HallaVentana_X_Caption", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Habilita_DesHabilita_Controles_Forma
        /// Encargado de habilitar o deshabilitar los controles de uan forma o un frame.
        /// Cambiando el color dependiendo del estado.
        /// Solo trabaja con controles tipo:
        /// TextBox
        /// ComboBox
        /// DateTimePicker ( Campos de fechas ) 
        /// </summary>
        /// <param name="bl_Habilita">TRUE = Indica ue habilita los controles</param>
        /// <param name="Forma">Forma o frame que contiene los controles</param>
        /// <param name="bl_TrabajaCombos">TRUE = Indica si habilia o no los combos</param>
        /// <param name="bl_TrabajaCamposFecha">TRUE = Indica si habilita o no los campos fecha</param>
        /// <param name="st_CampoFecha">Fecha a asignar</param>
        /// <param name="st_CamposExcluidos">Arreglo con los campos excluidos.</param>
        public void Habilita_DesHabilita_Controles_Forma(Boolean bl_Habilita, Control Forma, Boolean bl_LimpiaCombos, Boolean bl_LimpiaCamposFecha, String st_CampoFecha, string[] st_CamposExcluidos)
        {
            try
            {
                ObjPr_Self.SetEnableDisableControlForm(bl_Habilita,  Forma,  bl_LimpiaCombos,  bl_LimpiaCamposFecha, st_CampoFecha,  st_CamposExcluidos);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
               ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Habilita_DesHabilita_Controles_Forma. System.AccessViolationException", "", ex_0.Message.ToString() , "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Habilita_DesHabilita_Controles_Forma. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString() , "", "");
                }
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
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Habilita_DesHabilita_Controles_Forma", "", ex.Message.ToString() , "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Habilita_DesHabilita_Controles_Forma", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString() , "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Valida que un email sea correcto
        /// </summary>
        /// <param name="st_EMail">Correo electronico a validar</param>
        /// <param name="st_MensajeError">Devuelve el mensaje de error de las validaciones del correo electronico</param>
        /// <returns>true = si el correo esta ok</returns>
        public Boolean Valida_EMail(String st_EMail, ref String st_MensajeError)
        {
            try
            {
                return ObjPr_Self.Is_A_Valid_EMail(st_EMail, ref st_MensajeError);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Valida_EMail. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Valida_EMail. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Valida_EMail", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Valida_EMail", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Send_E_Mail
        /// Encargado de enviar un correo electronico.
        /// Lee del archivo de configuracion de la seccion : st_Seccion_Config
        /// La cuenta, servidor y clave ( encriptada) de la cuenta de origen
        /// Las cuentas de destino.
        /// Ejemplo de la seccion:
        /// 
        /// [E-MAIL]
        /// Email_Origen =  
        /// Servidor_Origen =  
        /// Clave_Email_Origen =  
        /// Puerto =  
        /// TimeOut =  
        /// ;
        /// Email_Destino_1 =  
        /// Email_Destino_2 = 
        /// Email_Destino_3 = 
        /// Email_Destino_4 =  
        /// Email_Destino_5 =  
        /// Email_Destino_6 = 
        /// Email_Destino_7 = 
        /// Email_Destino_8 = 
        /// Email_Destino_9 = 
        /// Email_Destino_10 = 
        /// Email_Destino_11 = 
        /// Email_Destino_12 = 
        /// Email_Destino_13= 
        /// Email_Destino_14= 
        /// Email_Destino_15 = 
        /// Email_Destino_16 = 
        /// Email_Destino_17 = 
        /// Email_Destino_19 = 
        /// Email_Destino_20 = 
        /// ;
        /// ;
        /// [FIN SECCION E-MAIL]
        /// ; ==========================================================================
        /// ;
        /// </summary>
        /// <param name="st_ArchivoConfigApp">Ruta y nombre del archivo de configuracion de la aplicacion</param>
        /// <param name="st_Seccion_Config">Nombre de la seccion del archivo de configuracion de donde lee los parametros.</param>
        /// <param name="st_Asunto">Asunto del correo</param>
        /// <param name="st_CuerpoMensaje">Cuerpo del Mensaje</param>
        /// <param name="st_ArchivosAdjuntos">Arreglo con la Ruta y nombre de los archivos adjuntos.</param>
        /// <param name="bl_SalDialog">Indica si presenta la salida del error en la pantalla. Por defecto en False</param>
        /// <returns>true Si envio el E-Mail</returns>
        public Boolean Send_E_Mail(String st_ArchivoConfigApp, String st_Seccion_Config, String st_Asunto, String st_CuerpoMensaje, string[] st_ArchivosAdjuntos, Boolean bl_SalDialog = false)
        {
            try
            {
                return ObjPr_Self.Let_Send_An_EMail(st_ArchivoConfigApp, st_Seccion_Config, st_Asunto, st_CuerpoMensaje, st_ArchivosAdjuntos, bl_SalDialog);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Send_E_Mail. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Send_E_Mail. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, bl_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Send_E_Mail", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Send_E_Mail", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Encargado de devolver la ruta del reopsitorio tomando en cuenta la cedula y la ruta base del servidor.
        /// </summary>
        /// <param name="st_Cedula">Numero de cedula</param>
        /// <param name="st_RutaBaseServidor">Ruta base del servidor, ejemplo C:\\Biometrias\\</param>
        /// <returns>Devuelve la ruta de las biometrias para la cedula</returns>
        public String ArmaRuta_Repo_X_Cedula(String st_Cedula, String st_RutaBaseServidor)
        {
            try
            {
                return ObjPr_Self.BringMe_ID_Rep_Path(st_Cedula, st_RutaBaseServidor);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ArmaRuta_Repo_X_Cedula. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ArmaRuta_Repo_X_Cedula. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ArmaRuta_Repo_X_Cedula", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ArmaRuta_Repo_X_Cedula", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return "";
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Asigna la imagen de un archivo a un objeto tipo Imgae, via Streamreader
        /// </summary>
        /// <param name="st_RutaImagen">Ruta y nombre del archivo de imagen</param>
        /// <param name="ImagenControl">Objeto a asignar la imagen</param>
        public void Asigna_Imagen_Control_Stream(String st_RutaImagen, ref System.Drawing.Image ImagenControl)
        {
            try
            {
                ObjPr_Self.Put_Img_2_Ctr_Stream(st_RutaImagen, ref ImagenControl);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Asigna_Imagen_Control_Stream. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Asigna_Imagen_Control_Stream. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
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
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Asigna_Imagen_Control_Stream", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Asigna_Imagen_Control_Stream", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void Evaluate_Bio_Files(String st_ArchivoConfigNames , String[] stL_FileList, ref Boolean blR_Firma, ref Boolean blR_Documento, ref Boolean blR_Huella, ref Boolean blR_Rostro, ref Boolean blR_Iris, ref int inR_Cant_Archivos)
        {
            Boolean blL_Firma_Archivo = false;
            Boolean blL_Documento_Archivo = false;
            Boolean blL_Huella_Archivo = false;
            Boolean blL_Rostro_Archivo = false;
            Boolean blL_Iris_Archivo = false;
            try
            {
                //
                blR_Firma = false;
                blR_Documento = false;
                blR_Huella = false;
                blR_Rostro = false;
                blR_Iris = false;
                inR_Cant_Archivos = 0;
                //
                foreach (String stL_FileName in stL_FileList)
                { // Inicio del foreach (String stL_FileName in stL_FileList)
                    //
                    if (!(stL_FileName.Contains("SA-DIR") || stL_FileName.ToLower().Contains("xml") || stL_FileName.ToLower().Contains("sig") || stL_FileName.ToLower().Contains("bin") || stL_FileName.ToLower().Contains("txt") || stL_FileName.Contains("\n") || stL_FileName.Equals("")))
                    {
                        this.Validate_Bio_File_Name(st_ArchivoConfigNames, stL_FileName, ref  blL_Firma_Archivo, ref  blL_Documento_Archivo, ref  blL_Huella_Archivo, ref  blL_Rostro_Archivo, ref  blL_Iris_Archivo);
                        inR_Cant_Archivos++;
                        // Como esta evaluando, archivo por archivo, prende las banderas, cuando el metodo anterior ha encontrado algun archivo
                        if (blL_Firma_Archivo)
                        {
                            blR_Firma = true;
                        }
                        if (blL_Documento_Archivo)
                        {
                            blR_Documento = true;
                        }
                        if (blL_Huella_Archivo)
                        {
                            blR_Huella = true;
                        }
                        if (blL_Rostro_Archivo)
                        {
                            blR_Rostro = true;
                        }
                        if (blL_Iris_Archivo)
                        {
                            blR_Iris = true;
                        }
                        //
                    }
                } // Fin del foreach (String stL_FileName in stL_FileList)
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Evaluate_Bio_Files. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Evaluate_Bio_Files. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
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
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Evaluate_Bio_Files", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Evaluate_Bio_Files", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private Boolean Validate_Bio_File_Name(String st_ArchivoConfigNames, String st_FileName, ref Boolean blR_Firma, ref Boolean blR_Documento, ref Boolean blR_Huella, ref Boolean blR_Rostro, ref Boolean blR_Iris)
        {
            Boolean blL_DatoOk = false;
            try
            {
                //
                blR_Firma = false;
                blR_Documento = false;
                blR_Huella = false;
                blR_Rostro = false;
                blR_Iris = false;
                //
                blR_Firma = this.Find_Bio_File_Name(st_ArchivoConfigNames, st_FileName, "FIRMA");
                //
                if (!blR_Firma)
                {
                    blR_Documento = this.Find_Bio_File_Name(st_ArchivoConfigNames, st_FileName, "DOCUMENTO");
                }
                //
                if (!blR_Firma && !blR_Documento)
                {
                    blR_Huella = this.Find_Bio_File_Name(st_ArchivoConfigNames, st_FileName, "HUELLA");
                }
                //
                if (!blR_Firma && !blR_Documento && !blR_Huella)
                {
                    blR_Rostro = this.Find_Bio_File_Name(st_ArchivoConfigNames, st_FileName, "ROSTRO");
                }
                //
                if (!blR_Firma && !blR_Documento && !blR_Huella && !blR_Rostro)
                {
                    blR_Iris = this.Find_Bio_File_Name(st_ArchivoConfigNames, st_FileName, "IRIS");
                }
                //
                blL_DatoOk = (blR_Firma && blR_Documento && blR_Huella && blR_Rostro && blR_Iris);
                return blL_DatoOk;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Validate_Bio_File_Name. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Validate_Bio_File_Name. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Validate_Bio_File_Name", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Validate_Bio_File_Name", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private Boolean Find_Bio_File_Name(String st_ArchivoConfigNames, String st_FileName, String st_SectionName)
        {
            Boolean blL_DatoOK = false;
            try
            {
                //
                if (File.Exists(st_ArchivoConfigNames))
                {
                    ClasX_Config ObjL_ConfigApp = new ClasX_Config(st_ArchivoConfigNames, stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                    //
                    for (int inL_Index = 1; inL_Index <= 100; inL_Index++)
                    { // Inicio del for (int inL_Index = 1;
                        String stL_ValorArchivo = ObjL_ConfigApp.LeeLlave_Seccion(st_SectionName, inL_Index.ToString().ToLower());
                        if (stL_ValorArchivo.Length == 0)
                        {
                            break;
                        }
                        else
                        {
                            if (st_FileName.Trim().ToLower().Contains(stL_ValorArchivo.ToLower()))
                            {
                                blL_DatoOK = true;
                                break;
                            }
                        }
                    } // Fin del for (int inL_Index = 1;
                }
                else
                {
                    ClasX_EventLog objL_Log_1 = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
                    objL_Log_1.setTextErrLog("_C_ProgReg.Dll. ClasX_Utils. Find_Bio_File_Name. El archivo " + st_ArchivoConfigNames + "No existe. No es posible hacer la validacion de la biometria.");
                }
                //
                return blL_DatoOK;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Find_Bio_File_Name. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Find_Bio_File_Name. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
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
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Find_Bio_File_Name", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Find_Bio_File_Name", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean Elimina_Archivos_Bio_In_Server(String st_Cedula, ClasX_ClienteTCP Transbio, String st_UsuarioPandora = "",String st_ClavePandora = "")
        {
            try
            {
                return ObjPr_Self.Let_Server_To_DelBio(st_Cedula, Transbio.getst_IPServidor(), Transbio.getin_puerto(), st_UsuarioPandora, st_ClavePandora);
            }
            //
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Archivos_Bio_In_Server", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Archivos_Bio_In_Server", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
            catch (Exception es)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (es.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Archivos_Bio_In_Server", "", es.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "Elimina_Archivos_Bio_In_Server", "", es.Message.ToString() + " " + es.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ShowFrmDetalleImagen
        /// PResenta el detale de una imagen
        /// </summary>
        /// <param name="st_TituloForma">Titulo de la forma</param>
        /// <param name="st_UsuarioApp">Usuario Aplicacion</param>
        /// <param name="st_ArchivoLog">Ruta y nombre de archivo de log</param>
        /// <param name="stPr_ExeName_Exe">Nombre del EXE</param>
        /// <param name="ObjImagen">Imagen a Presetar</param>
        /// <param name="st_TokenToView">Token to View</param>
        public void ShowFrmDetalleImagen(String st_TituloForma, String st_UsuarioApp, String st_ArchivoLog, String stPr_ExeName_Exe, System.Drawing.Image ObjImagen, String st_FileNameImg, String st_TokenToView)
        { // Inicio del public void ShowFrmDetalleImagen(
            // Se encarga de presentar el detalle de una imagen.
            try
            {
                if (st_TokenToView.Trim() == "_C_ProgReg.Dll_unique_ShowFrmDetalleImagen")
                {
                    Fenix.FrmDetalleImagen Forma = new Fenix.FrmDetalleImagen();
                    Forma.TomaParametros(st_TituloForma, st_UsuarioApp, st_ArchivoLog, stPr_ExeName_Exe, ObjImagen, st_FileNameImg);
                    // Presenta la forma de manera MODAL
                    //--Forma.ShowDialog();
                    Forma.Show();
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowFrmDetalleImagen. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowFrmDetalleImagen", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowFrmDetalleImagen", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // Fin del public void ShowFrmDetalleImagen(



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ShowShowFrmImgZoom
        /// PResenta el detale de una imagen
        /// </summary>
        /// <param name="st_TituloForma">Titulo de la forma</param>
        /// <param name="st_UsuarioApp">Usuario Aplicacion</param>
        /// <param name="st_ArchivoLog">Ruta y nombre de archivo de log</param>
        /// <param name="stPr_ExeName_Exe">Nombre del EXE</param>
        /// <param name="ObjImagen">Imagen a Presetar</param>
        /// <param name="st_TokenToView">Token to View</param>
        public void ShowFrmImgZoom(String st_TituloForma, String st_UsuarioApp, String st_ArchivoLog, String stPr_ExeName_Exe, System.Drawing.Image ObjImagen, String st_FileNameImg, String st_TokenToView)
        { // Inicio del public void ShowFrmImgZoom(
            // Se encarga de presentar el detalle de una imagen.
            try
            {
                if (st_TokenToView.Trim() == "_C_ProgReg.Dll_unique_ShowFrmImgZoom")
                {
                    FrmImgZoom Forma = new FrmImgZoom();
                    Forma.TomaParametros(st_TituloForma, st_UsuarioApp, st_ArchivoLog, stPr_ExeName_Exe, ObjImagen, st_FileNameImg);
                    // Presenta la forma de manera MODAL
                    //--Forma.ShowDialog();
                    Forma.Show();
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowFrmImgZoom. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
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
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowFrmImgZoom", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError("_C_ProgRes.DLL", "ClasX_Utils", "ShowFrmImgZoom", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // Fin del public void ShowFrmImgZoom(




    } // fin del  public class ClasX_Utils
}
