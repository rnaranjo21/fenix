using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Gui;
using Neurotec.Devices;
using Neurotec.Images;
using Neurotec.IO;
using Neurotec.Licensing;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using _C_ProgRes;
using System.Runtime.ExceptionServices;

namespace _C_Devices_N_4_3
{
    public class ClasX_IrisScaner_Device
    {
        const string stC_Components = "Biometrics.IrisExtraction,Devices.IrisScanners";
        const string stC_Port = "5000";
        const string stC_Address = "/local";
        public delegate void PuedoGuardar();
        private string posicion = "";
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private NDeviceManager objPr_deviceManager;
        private NIrisScanner objPr_IrisScanner;
        private NImage objPr_image;
        private NBuffer objPr_template;
        private NEExtractor objPr_extractor;
        private NMatcher objPr_matcher;
        private Neurotec.Biometrics.Gui.NEView neView;
        private _C_ProgRes.ClasX_EventLog ObjPr_ClasX_EventLog;
        private System.Windows.Forms.ComboBox cbScanner;
        private string stPr_UserApp;
        private string stPr_ArchivoLog;
        private byte[] btR_Templateder;
        private byte[] btR_TemplateIzq;
        public PuedoGuardar puedoGuardar;
        //
        private String stPr_MensajeExcepcion = "";
        private Boolean blPr_ValidaImagenIRIS = true;
        //


        public ClasX_IrisScaner_Device(String stR_UserApp, String stR_ArchivoLog)
        {
            stPr_UserApp = stR_UserApp;
            stPr_ArchivoLog = stR_ArchivoLog;
            try
            {
                ObjPr_ClasX_EventLog = new ClasX_EventLog(stPr_UserApp, stPr_ArchivoLog);
                bool licEstdo = NLicense.ObtainComponents(stC_Address, stC_Port, stC_Components);
                objPr_extractor = new NEExtractor();
                objPr_matcher = new NMatcher();
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                    "ClasX_IrisScaner_Device(1). System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                    "ClasX_IrisScaner_Device(1)", "", ex.Message, "", "");
            }
        }


        public ClasX_IrisScaner_Device(ClasX_EventLog ObjR_ClasX_EventLog)
        {
            ObjPr_ClasX_EventLog = ObjR_ClasX_EventLog;
            try
            {
                bool licEstdo = NLicense.ObtainComponents(stC_Address, stC_Port, stC_Components);
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                    "ClasX_IrisScaner_Device(2). System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                    "ClasX_IrisScaner_Device(2)", "", ex.Message, "", "");
            }
        }
      

         /// <summary>
        /// Propiedad getMensajeExcepcion
        /// devuelve el mensaje de excepcion
        /// </summary>
        /// <returns>stPr_MensajeExcepcion</returns>
        public string getMensajeExcepcion()
        {
            return stPr_MensajeExcepcion;
        }


        /// <summary>
        /// Propiedad getValidaImagenIRIS
        /// devuelve si encuentra o no imagen de IRIS
        /// </summary>
        /// <returns>blPr_ValidaImagenIRIS</returns>
        public Boolean getValidaImagenIRIS()
        {
            return blPr_ValidaImagenIRIS;
        }


        /// <summary>
        /// Metodo : setMensajeExcepcion
        /// Permite definir el mensaje de Excepcion
        /// </summary>
        /// <param name="st_MensajeExcepcion">Mensaje de Excepcion</param>
        public void setMensajeExcepcion(string st_MensajeExcepcion)
        {
            stPr_MensajeExcepcion = st_MensajeExcepcion;
        }

         /// <summary>
        /// Metodo : setValidaImagenIRIS
        /// Permite definir si capturo imagen de IRIS
        /// </summary>
        /// <param name="blPr_ValidaImagenIRIS">Mensaje de Excepcion</param>
        public void setValidaImagenIRIS(Boolean bl_ValidaImagenIRIS)
        {
            blPr_ValidaImagenIRIS = bl_ValidaImagenIRIS;
        }




        [HandleProcessCorruptedStateExceptions]
        private void updateScannerList()
        {
            objPr_deviceManager = new NDeviceManager();
            cbScanner = new ComboBox();
            cbScanner.BeginUpdate();
            try
            {
                cbScanner.Items.Clear();
                objPr_deviceManager.Refresh();
                int cont = 0;
                foreach (NDevice device in objPr_deviceManager.Devices)
                {
                    cbScanner.Items.Add(device);
                    if (device.DisplayName.Contains("Cross Match I Scan"))
                    {
                        cbScanner.SelectedIndex = cont;
                        break;
                    }
                    cont++;
                }
                objPr_IrisScanner = cbScanner.SelectedItem as NIrisScanner;
                if (objPr_IrisScanner != null && objPr_IrisScanner.IsDisposed) objPr_IrisScanner = null;
                if (cbScanner == null && cbScanner.Items.Count > 0)
                {
                    cbScanner.SelectedIndex = 0;
                    return;
                }

                if (objPr_IrisScanner != null)
                {
                    cbScanner.SelectedIndex = cbScanner.Items.IndexOf(objPr_IrisScanner);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                    "updateScannerList. System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                    "updateScannerList", "", ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void StartScaner(String posicion)
        {
            try
            {
                this.posicion = posicion;
                updateScannerList();
                if (objPr_IrisScanner == null)
                {
                    ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "StartScaner",
                    "", "No se ha detectado un escáner de iris válido conectado", "", "");
                    /////se activa el popap
                    stPr_MensajeExcepcion = "No se ha detectado un escáner de iris válido conectado";
                    return;
                }
                else
                {
                    if (objPr_extractor == null)
                    {
                        blPr_ValidaImagenIRIS = false;
                        ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "StartScaner",
                    "", "Componente de extracc`´on no activado o no válido", "", "");
                        return;
                    }
                    if (objPr_template != null) objPr_template.Dispose();
                    objPr_template = null;
                    if (objPr_image != null) objPr_image.Dispose();
                    objPr_image = null;
                    neView = new NEView();
                    ReplateViewImage(null);
                    backgroundWorker = new BackgroundWorker();
                    this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
                    this.backgroundWorker.RunWorkerCompleted +=
                        new System.ComponentModel.RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
                    this.backgroundWorker.WorkerSupportsCancellation = true;

                    //objPr_IrisScanner.Preview += new EventHandler<NIrisScannerPreviewEventArgs>(OnPreview);
                    //objPr_IrisScanner.Preview +=new EventHandler<NIrisScannerPreviewEventArgs>(objPr_IrisScanner_Preview);

                    if (posicion.Equals("izq"))
                    {
                        NEPosition position = NEPosition.Left;
                        backgroundWorker.RunWorkerAsync(position);
                    }
                    if (posicion.Equals("der"))
                    {
                        NEPosition position = NEPosition.Right;
                        backgroundWorker.RunWorkerAsync(position);
                    }
                    if (posicion.Equals("") || posicion.Equals("amb"))
                    {
                        NEPosition position = NEPosition.Both;
                        backgroundWorker.RunWorkerAsync(position);
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "StartScaner. System.AccessViolationException",
                    "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "StartScaner",
                    "", ex.Message, "", "");
            }
        }


        public bool ScannerDetected()
        {
            updateScannerList();
            if (objPr_IrisScanner == null)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "StartScaner",
                "", "No se ha detectado un escáner de iris válido conectado", "", "");
                /////se activa el popap
                //stPr_MensajeExcepcion = "No se ha detectado un escáner de iris válido conectado";
                return false;
            }
            else
            {
                return true;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public void CancelScaning()
        {
            try
            {
                if (backgroundWorker.IsBusy && objPr_IrisScanner != null)
                {
                    MethodInvoker cancelAsync = new MethodInvoker(objPr_IrisScanner.Cancel);
                    cancelAsync.BeginInvoke(null, null);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "CancelScaning. System.AccessViolationException",
                    "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "CancelScaning",
                    "", ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void ReplateViewImage(Bitmap bitmap)
        {
            try
            {
                Bitmap old = neView.Image;
                neView.Image = bitmap;
                if (old != null) old.Dispose();
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "ReplateViewImage. System.AccessViolationException",
                    "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "ReplateViewImage",
                    "", ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void Preview(Bitmap image, NBiometricStatus status)
        {
            try
            {
                ReplateViewImage(image);
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "Preview. System.AccessViolationException",
                    "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "Preview",
                    "", ex.Message, "", "");
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            NEPosition position = (NEPosition)e.Argument;
            try
            {

                NEPosition[] supportedPositions = objPr_IrisScanner.GetSupportedPositions();
                bool supportsPositions = supportedPositions.Length > 1;
                objPr_image = objPr_IrisScanner.Capture(supportsPositions ? position : NEPosition.Unknown);
                if (objPr_image != null)
                {
                    using (NGrayscaleImage grayscaleImage = objPr_image.ToGrayscale())
                    {
                        NeeExtractionStatus extractionStatus;
                        NeeSegmentationDetails segmentaDetails;
                        NERecord record = objPr_extractor.Extract(grayscaleImage, position, out segmentaDetails,
                            out extractionStatus);
                        if (extractionStatus == NeeExtractionStatus.TemplateCreated)
                        {
                            objPr_template = record.Save();
                            e.Result = segmentaDetails;
                            if (posicion.Equals("der"))
                            {
                                btR_Templateder = objPr_template.ToArray();
                            }
                            else if (posicion.Equals("izq"))
                            {
                                btR_TemplateIzq = objPr_template.ToArray();
                            }
                            puedoGuardar();
                        }
                        else
                        {
                            //MessageBox.Show(string.Format("Failed to extract iris image: {0}", extractionStatus), "Extraction failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ObjPr_ClasX_EventLog.setTextErrLog("Erro extrayendo la imagen del iris:" + extractionStatus +
                                "Extracciòn fallida");
                            stPr_MensajeExcepcion = "Licencias No Activas";
                            return;

                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "BackgroundWorkerDoWork. System.AccessViolationException",
                        "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "BackgroundWorkerDoWork",
                        "", "Imagen vacía" + ex.Message, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                neView.SegmentationDetails = e.Result as NeeSegmentationDetails;
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "RunWorkerCompleted. System.AccessViolationException",
                        "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "RunWorkerCompleted",
                        "",  ex.Message, "", "");
            }

        }


        [HandleProcessCorruptedStateExceptions]
        public Bitmap getImagen()
        {
            try
            {
                return objPr_image.ToBitmap();
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "getImagen. System.AccessViolationException",
                        "", ex_0.Message, "", "");
                return null;
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "getImagen",
                        "", ex.Message, "", "");
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public bool guardaImagen(String ruta)
        {
            try
            {
                if (objPr_image != null)
                {
                    objPr_image.Save(ruta);
                    return true;
                }
                else
                {
                    ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "guardaImagen",
                        "", "Imagen vacía", "", "");
                    return false;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "guardaImagen. System.AccessViolationException", "", ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "guardaImagen", "", ex.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public byte[] getTemplate()
        {
            try
            {
                byte[] template = objPr_template.ToArray();
                return template;
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "getTemplate", "", ex_0.Message, "", "");
                return null;
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device", "getTemplate", "", ex.Message, "", "");
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public int guardartemplateMSSQL(ClasX_DBInfo objR_BaseDeDatos, String stR_nombreTabla, String str_NombreCampoCedula, String str_NombreCampoTemplate, String str_Cedula, long maxid)
        {
            //
            String stL_Ruta = "SERVER=" + objR_BaseDeDatos.getNombreServidor() + ";" + "DATABASE=" + objR_BaseDeDatos.getNombreBaseDatos() + ";" + "UID=" + objR_BaseDeDatos.getIdUsuario_BD() + ";" + "PASSWORD=" + objR_BaseDeDatos.getClaveUsuario_BD() + ";";
            String sentencia = "INSERT INTO " + stR_nombreTabla + " (Id, " + str_NombreCampoCedula + ", " + str_NombreCampoTemplate + ") VALUES (@maxid, @identificacion, @template)";
            try
            {
                SqlConnection objL_Conexion = new SqlConnection(stL_Ruta);
                objL_Conexion.Open();
                SqlCommand objL_cmd = new SqlCommand(sentencia, objL_Conexion);
                SqlParameter objL_param = new SqlParameter("@template", SqlDbType.VarBinary);
                objL_param.Size = btR_Templateder.Length;
                objL_param.Value = btR_Templateder;
                objL_cmd.Parameters.Add(objL_param);
                long lnL_cedula = Convert.ToInt64(str_Cedula);
                objL_cmd.Parameters.Add(new SqlParameter("@maxid", maxid));
                objL_cmd.Parameters.Add(new SqlParameter("@identificacion", lnL_cedula));
                int inL_numRegAffected = objL_cmd.ExecuteNonQuery();

                objL_cmd = new SqlCommand(sentencia, objL_Conexion);
                objL_param = new SqlParameter("@template", SqlDbType.VarBinary);
                objL_param.Size = btR_TemplateIzq.Length;
                objL_param.Value = btR_TemplateIzq;
                objL_cmd.Parameters.Add(objL_param);
                objL_cmd.Parameters.Add(new SqlParameter("@maxid", maxid));
                objL_cmd.Parameters.Add(new SqlParameter("@identificacion", lnL_cedula));
                inL_numRegAffected = objL_cmd.ExecuteNonQuery();
                objL_Conexion.Close();
                return inL_numRegAffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                        "guardartemplateMSSQL", "", "System.AccessViolationException: Error al guardar el template" + ex_0.Message, objR_BaseDeDatos.getNombreBaseDatos(), sentencia);
                return -1;
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                        "guardartemplateMSSQL", "", "Excepcion: Error al guardar el template" + ex.Message, objR_BaseDeDatos.getNombreBaseDatos(), sentencia);
                return -1;
            }
        }



        private string sentenciaSQL = "";

        [HandleProcessCorruptedStateExceptions]
        public int guardartemplatePLPosSQL(ClasX_DBInfo ObjPr_BaseDeDatos, String str_nombreTabla, String str_NombreCampoCedula, String st_NombreCampoTemplate, String str_Cedula, byte[] btr_template)
        {
            try
            {
                long cedula = Convert.ToInt64(str_Cedula);
                String ruta = "Server=" + ObjPr_BaseDeDatos.getNombreServidor() + ";" + "Database=" + ObjPr_BaseDeDatos.getNombreBaseDatos() + ";" + "User ID=" + ObjPr_BaseDeDatos.getIdUsuario_BD() + ";" + "Password=" + ObjPr_BaseDeDatos.getClaveUsuario_BD() + ";";
                Npgsql.NpgsqlConnection conDB_PostGreSQL = new Npgsql.NpgsqlConnection(ruta);
                String sentencia = "INSERT INTO " + str_nombreTabla + " (" + str_NombreCampoCedula + ", " + st_NombreCampoTemplate + ") VALUES (" + cedula + ", @template)";
                sentenciaSQL = sentencia;
                Npgsql.NpgsqlCommand cmdDB_PostGreSQL = new Npgsql.NpgsqlCommand(sentencia, conDB_PostGreSQL);
                byte[] pic = btR_Templateder;
                cmdDB_PostGreSQL.Parameters.AddWithValue("@template", btr_template);
                conDB_PostGreSQL.Open();
                // Ejecuta el Comando
                int inL_rowsAffected = cmdDB_PostGreSQL.ExecuteNonQuery();
                //
                cmdDB_PostGreSQL.Dispose();

                //cmdDB_PostGreSQL = new Npgsql.NpgsqlCommand(sentencia, conDB_PostGreSQL);
                //pic = btR_TemplateIzq;
                //cmdDB_PostGreSQL.Parameters.AddWithValue("@template", btR_TemplateIzq);
                //conDB_PostGreSQL.Open();
                //// Ejecuta el Comando
                //inL_rowsAffected = cmdDB_PostGreSQL.ExecuteNonQuery();
                //
                //cmdDB_PostGreSQL.Dispose();
                conDB_PostGreSQL.Close();
                return inL_rowsAffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                       "guardartemplatePLPosSQL", "", "System.AccessViolationException: Error al guardar el template" + ex_0.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), sentenciaSQL);
                return -1;
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                       "guardartemplatePLPosSQL", "", "Excepcion: Error al guardar el template" + ex.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), sentenciaSQL);
                return -1;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public int guardartemplateMySQL(ClasX_DBInfo ObjPr_BaseDeDatos, String str_nombreTabla, String str_NombreCampoCedula, String st_NombreCampoTemplate, String str_Cedula)
        {
            //
            String ruta = "SERVER=" + ObjPr_BaseDeDatos.getNombreServidor() + ";" + "DATABASE=" + ObjPr_BaseDeDatos.getNombreBaseDatos() + ";" + "UID=" + ObjPr_BaseDeDatos.getIdUsuario_BD() + ";" + "PASSWORD=" + ObjPr_BaseDeDatos.getClaveUsuario_BD() + ";";
            String sentencia = "INSERT INTO " + str_nombreTabla + " (" + str_NombreCampoCedula + ", " + st_NombreCampoTemplate + ") VALUES (@identificacion, @template)";
            //
            try
            {
                MySqlConnection conexion = new MySqlConnection(ruta);
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(sentencia, conexion);
                MySqlParameter param = new MySqlParameter("@template", MySqlDbType.Blob);
                param.Size = btR_Templateder.Length;
                param.Value = btR_Templateder;
                cmd.Parameters.Add(param);
                long cedula = Convert.ToInt64(str_Cedula);
                cmd.Parameters.Add(new MySqlParameter("@identificacion", cedula));
                int numregaffected = cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(sentencia, conexion);
                param = new MySqlParameter("@template", MySqlDbType.Blob);
                param.Size = btR_TemplateIzq.Length;
                param.Value = btR_TemplateIzq;
                cmd.Parameters.Add(param);
                cmd.Parameters.Add(new MySqlParameter("@identificacion", cedula));
                numregaffected = cmd.ExecuteNonQuery();

                conexion.Close();
                return numregaffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                       "guardartemplateMySQL", "", "System.AccessViolationException: Error al guardar el template" + ex_0.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), sentencia);
                return -1;
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_IrisScaner_Device",
                       "guardartemplateMySQL", "", "Excepcion: Error al guardar el template" + ex.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), sentencia);
                return -1;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public String  Desconectado (String stR_UserApp, String stR_ArchivoLog)
        {
            stPr_UserApp = stR_UserApp;
            stPr_ArchivoLog = stR_ArchivoLog;
            try
            {
                return ("No se ha detectado un escáner de iris válido conectado");
            }
            catch (System.AccessViolationException ex_0)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "Desconectado",
                    "Desconectado. System.AccessViolationException", "", ex_0.Message, "", "");
                return ("No se ha detectado un escáner de iris válido conectado");
            }
            catch (Exception ex)
            {
                ObjPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "Desconectado",
                    "Desconectado", "", ex.Message, "", "");
                return ("No se ha detectado un escáner de iris válido conectado");
            }
        }



    }
}
