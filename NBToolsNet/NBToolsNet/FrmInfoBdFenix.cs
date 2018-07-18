using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.IO;
using System.Runtime.ExceptionServices;

namespace StrailSAS_C_ProgRes
{
    public partial class FrmInfoBdFenix : Form
    {
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        //
        private String stPr_ArchivoConfigApp = ""; // Ruta y nombre del archivo de configuracion de la aplicacion,
        private String stPr_Archivo_InfoBds = ""; // Ruta y nombde del archivo de informacion de las bases de datos, para hacer conexion.
        //
        private Boolean blPr_AceptoInformacion = false; // Indica si ingreso los datos correctos del usuario
        //
        private ClasX_Config ObjPr_Conf = null; // Para manejar la configuracion de la aplicacion.
        private ClasX_Config ObjPr_InfoBDS_Fenix = null;// Para manejar la configuracion de la conexion a la base de datos, tipo fenix.
        //
        private ClasX_DBInfo ObjPr_InfoBD = null; // Define el objeto de la informacion de la base de datos con la cual va a trabajar.
        //
        private ClasX_Utils ObjPr_Utils = null; // Para utilizar las utilidades.
        //
        private String stPr_NombreBd_XTrabajo = "";
        private Boolean blPr_YaActivada = false;
        //
        private String stPr_Nombre_App = ""; // Nombre de la aplicacion.
        private String stPr_Version_App = ""; // Version de la aplicacion.
        private String stPr_NombreEmpresa_App = ""; // Nombre de la empresa donde esta instalado la aplicacion.
        //
        private String stPr_Servidor_Ori = "";
        private String stPr_Bd_Ori = "";
        private String stPr_Usuario_Ori = "";
        private String stPr_Clave_Ori = "";
        //
        private ClasX_Constans.inEsquema_Colores PrIn_EsquemaColor = ClasX_Constans.inEsquema_Colores.ESQUEMA__COLOR_ROJO; // el esquema del color
        //
        public FrmInfoBdFenix()
        {
            InitializeComponent();
        }

        [HandleProcessCorruptedStateExceptions]
        public void TomaParametros(String st_Titulo, String st_UsuarioAPP, String st_ArchivoLog, String st_ArchivoConfigApp, String st_Nombre_App, String st_Version, String st_NombreEmpresa, String st_Archivo_InfoBds, String st_NombreBd_XTrabajo , ref ClasX_DBInfo Obj_BaseDeDatos)
        {
            // Toma los parametros.
            String stL_Aux = "";
            try
            {
                // Guarda textos originales.
                stPr_Servidor_Ori = cmbServidores.Text;
                stPr_Bd_Ori = TxtBD.Text;
                stPr_Usuario_Ori = TxtUsuario.Text;
                stPr_Clave_Ori = TxtClave.Text;
                //
                LblMensaje.Text = "";
                //
                this.Text = st_Titulo;
                stPr_UsuarioAPP = st_UsuarioAPP;
                stPr_ArchivoLog = st_ArchivoLog;
                stPr_ArchivoConfigApp = st_ArchivoConfigApp;
                stPr_Archivo_InfoBds = st_Archivo_InfoBds;
                //
                stPr_NombreBd_XTrabajo = st_NombreBd_XTrabajo;
                //
                stPr_Nombre_App = st_Nombre_App;
                stPr_Version_App = st_Version;
                stPr_NombreEmpresa_App = st_NombreEmpresa;
                // Coloca aplicacion y version
                this.LblModuloVersion.Text = stPr_Nombre_App + " " + stPr_Version_App;
                this.LblNombreCia.Text = stPr_NombreEmpresa_App;
                //
                // Crea instancia para la clase que maneja las configuraciones
                ObjPr_Conf = new ClasX_Config(stPr_ArchivoConfigApp, stPr_UsuarioAPP, stPr_ArchivoLog);
                //
                ObjPr_Utils = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                // Lee el esquema de color
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.LeeLlave_Seccion(ClasX_Constans.SECCION_ID_APP, "Color");
                if (stL_Aux.Length == 0)
                {
                    stL_Aux = "0";
                }
                PrIn_EsquemaColor = (ClasX_Constans.inEsquema_Colores)Convert.ToInt32(stL_Aux);
                //
                // La informacion de la base de datos para manejar la conexion con la base de datos.
                ObjPr_InfoBD = new ClasX_DBInfo();
                ObjPr_InfoBD = Obj_BaseDeDatos;
                /////////////////////////////////////////////////////////
                // Dependiendo de tipo de conenexion habilita los controles de la forma
                /////////////////////////////////////////////////////////
                CmdAceptar.Enabled = false;
                ObjPr_Utils.setColor_Boton_DesHabilitado(CmdAceptar, PrIn_EsquemaColor);
                //
                CmdProbar.Enabled = true;
                ObjPr_Utils.setColor_Boton_Habilitado(CmdProbar, PrIn_EsquemaColor);
                //
                if (st_NombreBd_XTrabajo.Length > 0)
                {
                    TxtBD.Text = st_NombreBd_XTrabajo;
                    TxtBD.Enabled = false;
                }
                HabilitaBotones();
                ObjPr_Utils.setColor_Panel_PPal(PanelPpal, PrIn_EsquemaColor);
                ObjPr_Utils.setColor_Label_Error(LblMensaje, PrIn_EsquemaColor);
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "TomaParametros. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "TomaParametros", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void DevuelveParametros(ref Boolean blR_AceptoInfo, ref ClasX_DBInfo Obj_BaseDeDatos)
        {
            // Devuelve los parametros
            try
            {
                //
                blR_AceptoInfo = blPr_AceptoInformacion;
                // Devuelve el objeto con la informacion de la base de datos.
                Obj_BaseDeDatos = ObjPr_InfoBD;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "DevuelveParametros. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "DevuelveParametros", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void FrmInfoBdFenix_Shown(Object sender, EventArgs e)
        {
            try
            {
                /////////////////////////////////////////////////////////
                if (blPr_YaActivada == false)
                {
                    blPr_YaActivada = true;
                    HallaListaServidores();
                }
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "FrmInfoBdFenix_Shown. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "FrmInfoBdFenix_Shown", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }
        //

        [HandleProcessCorruptedStateExceptions]
        private void HabilitaBotones()
        {
            // Habilita los botones.
            try
            {
                //
                CmdAceptar.Enabled = false;
                ObjPr_Utils.setColor_Boton_DesHabilitado(CmdAceptar, PrIn_EsquemaColor);
                //
                CmdProbar.Enabled = false;
                ObjPr_Utils.setColor_Boton_DesHabilitado(CmdProbar, PrIn_EsquemaColor);
                // Si los contenidos de los campos son diferentes a los textos originales.
                if (cmbServidores.Text != stPr_Servidor_Ori && TxtBD.Text != stPr_Bd_Ori  && TxtUsuario.Text != stPr_Usuario_Ori && TxtClave.Text != stPr_Clave_Ori)
                {
                    if ((cmbServidores.Text.Length > 0 && TxtBD.Text.Length > 0 && TxtUsuario.Text.Length > 0 && TxtClave.Text.Length > 0))
                    {
                        CmdProbar.Enabled = true;
                        ObjPr_Utils.setColor_Boton_Habilitado(CmdProbar, PrIn_EsquemaColor);
                    }
                }
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "HabilitaBotones. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "HabilitaBotones", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }
        //


        [HandleProcessCorruptedStateExceptions]
        private void HallaListaServidores()
        {
            // Halla la lista de los servidores de SQL Server.
            // URL de consulta : http://programandoenpuntonet.blogspot.com/2009/01/obtener-instancias-de-sql-server-y.html
            // Creamos una lista para que sea el origen de datos del combobox
            List<String> LstServidores = new List<String>();
            Boolean blL_EstadoCombo = false;
            Boolean blL_EstadoUsuario = false;
            Boolean blL_EstacoClave = false;
            //
            try
            {
                // Guarda los estados de los controles
                blL_EstadoCombo = cmbServidores.Enabled;
                blL_EstadoUsuario = TxtUsuario.Enabled;
                blL_EstacoClave = TxtClave.Enabled;
                // Los Coloca en false, mientras halla la lista de los servidores
                cmbServidores.Enabled = false;
                TxtUsuario.Enabled = false;
                TxtClave.Enabled = false;
                CmdCancelar.Enabled = false;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                //
                LblMensaje.Text = ClasX_Constans.MENSAJE_23 + ClasX_Constans.NEW_LINE + ClasX_Constans.MENSAJE_24;
                Application.DoEvents();
                Application.DoEvents();
                //
                if (ObjPr_InfoBD.getTipoBD() == ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER)
                { // del if (ObjPr_InfoBD.getTipoBD() == ClasX_DBInfo.inDB_Types.BD_TYPE_SQLSERVER) 
                    //
                    SqlDataSourceEnumerator servidores;
                    System.Data.DataTable tablaServidores;
                    //String servidor;
                    //
                    servidores = SqlDataSourceEnumerator.Instance;
                    tablaServidores = new DataTable();
                    //
                    // Comprobamos que no se haya cargado ya el combobox
                    if (tablaServidores.Rows.Count == 0)
                    {
                        // Obtenemos un dataTable con la información sobre las instancias visibles
                        // de SQL Server 2000 y 2005
                        tablaServidores = servidores.GetDataSources();
                        // Recorremos el dataTable y añadimos un valor nuevo a la lista con cada fila
                        foreach (DataRow rowServidor in tablaServidores.Rows)
                        {
                            // La instancia de SQL Server puede tener nombre de instancia 
                            //o únicamente el nombre del servidor, comprobamos si hay 
                            //nombre de instancia para mostrarlo
                            if (String.IsNullOrEmpty(rowServidor["InstanceName"].ToString()))
                                LstServidores.Add(rowServidor["ServerName"].ToString());
                            else
                                LstServidores.Add(rowServidor["ServerName"] + "\\" + rowServidor["InstanceName"]);
                        }

                        // Asignamos al origen de datos del combobox la lista con 
                        // las instancias de servidores
                        if (LstServidores.Count == 0)
                        {
                            // Asigna el Servior, que tiene la BD
                            LstServidores.Add(ObjPr_InfoBD.getNombreServidor());
                        }
                        this.cmbServidores.DataSource = LstServidores;
                    }
                    //
                    //SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                    //System.Data.DataTable table = instance.GetDataSources();
                    //
                    //foreach (System.Data.DataRow row in table.Rows)
                    //{
                    //    foreach (System.Data.DataColumn col in table.Columns)
                    //    {
                    //        Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                    //    }
                    //}
                    //
                }
                else
                {
                    // Para los otros tipos de servidores.
                    // Asigna el Servior, que tiene la BD
                    LstServidores.Add(ObjPr_InfoBD.getNombreServidor());
                    // Asignamos la lista al combo 
                    this.cmbServidores.DataSource = LstServidores;
                    //
                }
                // Limpia mensaje y deja los controles con el estado que estaban originalmente
                LblMensaje.Text = "";
                //
                cmbServidores.Enabled = blL_EstadoCombo;
                TxtUsuario.Enabled = blL_EstadoUsuario;
                TxtClave.Enabled = blL_EstacoClave;
                //
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                //
                Application.DoEvents();
                Application.DoEvents();
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "HallaListaServidores. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "HallaListaServidores", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void cmbServidores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "cmbServidores_SelectedIndexChanged. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "cmbServidores_SelectedIndexChanged", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void TxtBD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "TxtBD_TextChanged. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "TxtBD_TextChanged", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "TxtUsuario_TextChanged. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "TxtUsuario_TextChanged", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void TxtClave_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "TxtClave_TextChanged. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                ObjPr_Utils.setColor_Boton_XEstado(CmdCancelar, PrIn_EsquemaColor);
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "TxtClave_TextChanged", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void CmdProbar_Click(object sender, EventArgs e)
        {
            // Prueba la conexion
            ClasX_DBQuery Query = null;
            //
            try
            {
                LblMensaje.Text = "";
                LblMensaje.Text = ClasX_Constans.MENSAJE_25 + ClasX_Constans.NEW_LINE + ClasX_Constans.MENSAJE_24;
                Application.DoEvents();
                //
                CmdAceptar.Enabled = false;
                ObjPr_Utils.setColor_Boton_DesHabilitado(CmdAceptar, PrIn_EsquemaColor);
                Application.DoEvents();
                //
                Query = new ClasX_DBQuery(stPr_UsuarioAPP, stPr_ArchivoLog);
                //
                ObjPr_InfoBD.setNombreServidor(cmbServidores.Text);
                ObjPr_InfoBD.setNombreBDSql(TxtBD.Text);
                ObjPr_InfoBD.setIDUsuario_BD(TxtUsuario.Text);
                ObjPr_InfoBD.setClaveUsuario_BD(TxtClave.Text);
                //
                // Hace la conexion
                Query.setInfoBD(ObjPr_InfoBD);
                Query.ConectarBD();
                //
                if (Query.getConectado())
                {
                    CmdAceptar.Enabled = true;
                    ObjPr_Utils.setColor_Boton_Habilitado(CmdAceptar, PrIn_EsquemaColor);
                    LblMensaje.Text = ClasX_Constans.MENSAJE_8;
                    Application.DoEvents();
                }
                else
                {
                    LblMensaje.Text = ClasX_Constans.MENSAJE_7;
                    Application.DoEvents();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "HallaListaServidores. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "HallaListaServidores", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            //
            ClasX_Encripta ObjL_Encrip = null;
            try
            {
                MessageBoxButtons Botones = MessageBoxButtons.YesNo;
                DialogResult Obj_result;
                // Displays the MessageBox.
                Obj_result = MessageBox.Show(ClasX_Constans.MENSAJE_6, ClasX_Constans.MENSAJE_5, Botones);
                if (Obj_result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (File.Exists(stPr_Archivo_InfoBds))
                    {
                        File.Delete(stPr_Archivo_InfoBds);
                    }
                    ObjPr_InfoBDS_Fenix = new ClasX_Config(stPr_Archivo_InfoBds, stPr_UsuarioAPP, stPr_ArchivoLog);
                    ObjL_Encrip = new ClasX_Encripta(stPr_UsuarioAPP, stPr_ArchivoLog);
                    //
                    String[] parametros = new String[8];
                    //
                    parametros[0] = "[" + ClasX_Constans.SECCION_BD_CONNECT_INFO + "]";
                    parametros[1] = ";@#";
                    parametros[2] = "ServerName = " + ObjL_Encrip.EncriptaInfo(cmbServidores.Text, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                    parametros[3] = "DBName = " + ObjL_Encrip.EncriptaInfo(TxtBD.Text, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                    parametros[4] = "UID = " + ObjL_Encrip.EncriptaInfo(TxtUsuario.Text, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                    parametros[5] = "PWDID = " + ObjL_Encrip.EncriptaInfo(TxtClave.Text, "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=");
                    parametros[6] = ";@#";
                    parametros[7] = "[Fin de seccion" + ClasX_Constans.SECCION_BD_CONNECT_INFO + "]";
                    //
                    ObjPr_InfoBDS_Fenix.setParametros(parametros);
                    ObjPr_InfoBDS_Fenix.GuardarArchivo();
                    //
                    // Cambia la informacion del servidor
                    //ObjPr_Conf.ModificaLlave( ClasX_Constans.SECCION_BD_0 , "Server", cmbServidores.Text);
                    //ObjPr_Conf.GuardarArchivo();
                    //
                    blPr_AceptoInformacion = true;
                    // Cierra la forma
                    this.Hide();
                }
                
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "CmdAceptar_Click. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "CmdAceptar_Click", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

        private void CmdCancelar_Click(object sender, EventArgs e)
        {
            //
            try
            {
                blPr_AceptoInformacion = false;
                // Cierra la forma
                this.Hide();
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("StrailSAS_C_ProgReg.Dll", "FrmInfoBdFenix", "CmdCancelar_Click", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }





    }
}
