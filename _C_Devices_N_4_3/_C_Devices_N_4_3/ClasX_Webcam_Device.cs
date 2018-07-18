#region usings
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
//
using System.Runtime.ExceptionServices;
#endregion

namespace _C_Devices_N_4_3
{
    #region funciones delegadas
    public delegate void listoGrabar();
    public delegate void UpdateVi(Bitmap image, NleDetectionDetails[] details, NleExtractionStatus status);
    #endregion

    public class ClasX_Webcam_Device
    {
        #region campos constantes
        /// <summary>
        /// Componentes a validar para activar el servicio
        /// </summary>
        const string stC_Components = "Biometrics.FaceExtraction,Devices.Cameras";
        /// <summary>
        /// Puerto  TCP para el servidor de licencias
        /// </summary>
        const string stC_Port = "5000";
        /// <summary>
        /// Dirección del servidor de licencias
        /// </summary>
        const string stC_Address = "/local";
        #endregion

        #region campos estáticos
        /// <summary>
        /// Llave para establecer el estado de la captura (extracción o captura)
        /// </summary>
        static int key = 1;
        #endregion

        #region campos privados
        /// <summary>
        /// Objeto de tipo "NDeviceManager" en el que se carga una colección de Dispositivos conectados a la máquina
        /// De este se puede obtener el tipo de dispositivo requerido para usar en la captura de rostro
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NDeviceManager objPr_deviceManager;
        /// <summary>
        /// Objeto de tipo "NCamera" contiene el dispositivo en uso
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NCamera objPr_camera;
        /// <summary>
        /// Objeto de tipo "NBuffer" este contiene el Template una vez ha sido generado
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NBuffer objPr_template;
        /// <summary>
        /// contiene la captura de la imagen de mejor calidad obtenida durante la captura
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NImage _bestFrame;
        /// <summary>
        /// Objeto de tipo "NLExtractor" realiza la operacion de extraccion del Template de una imagen
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NLExtractor objPr_extractor;
        /// <summary>
        /// Objeto "ClasX_EventLog" para el manejo de log de los eventos de error de la clase
        /// </summary>
        private ClasX_EventLog objPr_log = null;
        /// <summary>
        /// Objeto de tipo "NLView" este contiene una vista de la captura del dispositivo ESCANER de huellas
        /// A partir de esta vista se obtienen, la imagen, el Template de la huella y se presenta la vista previa de la captura en las aplicaciones
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private Neurotec.Biometrics.Gui.NLView facesView;
        /// <summary>
        /// variable booleana, para determinar el tipo de operacion a realizar (captura simple o extraccion)
        /// </summary>
        private bool blPr_capture;
        /// <summary>
        /// Arreglo de bytes, contienen el template de rostro
        /// </summary>
        private byte[] btR_Template;
        /// <summary>
        /// Definicion del backgroundWorker para ejecutar las tareas en segundo plano
        /// </summary>
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        /// <summary>
        /// Lista de imagenes, esta se utiliza para almacenar las multiples capturas realizadas durante el proceso
        /// </summary>
        private readonly List<NImage> _capturedImages = new List<NImage>();
        /// <summary>
        /// ComboBox para almacenar la lista de dispositivos encontrados
        /// </summary>
        private System.Windows.Forms.ComboBox cbCamera;
        private Timer timer;


        private Boolean bl_Pr_En_Identificacion = false;
        //

        private Boolean blPr_ExisteCamara = false;

        private Boolean blPr_Cancelar_DoWorker = false;

        private Object ObjPr_ThisLock = new Object();
        //
        private String stPr_UsuarioApp = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Path y Nombre el Archivo Log.
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = false;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo
        //
        #endregion

        #region campos públicos
        /// <summary>
        /// Evento de tipo público para realizar tareas al confirmar que hay una imagen y un Template válidos
        /// luego de la ejecución de este evento, el BackgroundWorker continúa llevando a cabo las tareas en segundo plano
        /// </summary>
        public event listoGrabar ListoParaGrabar;
        /// <summary>
        /// Evento de tipo público para presentar la vista previa en tiempo real del trabajo de la camara
        /// luego de la ejecución de este evento, el BackgroundWorker continúa llevando a cabo las tareas en segundo plano
        /// </summary>
        public event UpdateVi updateview;
        #endregion



        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="stR_UserApp"></param>
        /// <param name="stR_ArchivoLog"></param>
        public ClasX_Webcam_Device(String stR_UserApp, String stR_ArchivoLog)
        {
            stPr_UsuarioApp = stR_UserApp;
            stPr_ArchivoLog = stR_ArchivoLog; 
            //
            ClasX_EventLog ObjL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog, false);
            ObjL_Log.setTextErrLog("Entrando en _C_Devices_N_4_3.ClasX_Webcam_Device(). constructor 1");
            ObjL_Log = null;
            timer = new Timer();
            objPr_log = new ClasX_EventLog(stR_UserApp, stR_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog, false);
            //
            try
            {
                //
                facesView = new Neurotec.Biometrics.Gui.NLView();
                NLicense.ObtainComponents(stC_Address, stC_Port, stC_Components);
                ClasX_EventLog ObjL_Log1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog, false);
                ObjL_Log1.setTextErrLog("Saliendo en _C_Devices_N_4_3.ClasX_Webcam_Device(). constructor 1");
                ObjL_Log1 = null;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "ClasX_Webcam_Device-constructor 1 de la clase",
                    "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "ClasX_Webcam_Device-constructor 1 de la clase",
                    "", "Excepción: " + ex.Message, "", "");
            }
        }


        public ClasX_Webcam_Device(String stR_UserApp, String stR_ArchivoLog, Boolean bl_En_Identificacion)
        {
            stPr_UsuarioApp = stR_UserApp;
            stPr_ArchivoLog = stR_ArchivoLog; 
            //
            ClasX_EventLog ObjL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog, false);
            ObjL_Log.setTextErrLog("Entrando en _C_Devices_N_4_3.ClasX_Webcam_Device(). constructor 2");
            ObjL_Log = null;
            //
            bl_Pr_En_Identificacion = bl_En_Identificacion;
            timer = new Timer();
            objPr_log = new ClasX_EventLog(stR_UserApp, stR_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog, false);
            //
            try
            {
                facesView = new Neurotec.Biometrics.Gui.NLView();
                NLicense.ObtainComponents(stC_Address, stC_Port, stC_Components);
                ClasX_EventLog ObjL_Log1 = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog, false);
                ObjL_Log1.setTextErrLog("Saliendo en _C_Devices_N_4_3.ClasX_Webcam_Device(). constructor 2");
                ObjL_Log1 = null;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "ClasX_Webcam_Device-constructor 2 de la clase",
                    "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "ClasX_Webcam_Device-constructor 2 de la clase",
                    "", "Excepción: " + ex.Message, "", "");
            }
        }




        public Boolean getExisteCamara()
        {
            return blPr_ExisteCamara;
        }

        public void setExisteCamara(Boolean bl_ExisteCamara)
        {
            blPr_ExisteCamara = bl_ExisteCamara;
        }

        public Boolean getCancelar_DoWorker()
        {
            return blPr_Cancelar_DoWorker;
        }

        public void setCancelar_DoWorker(Boolean bl_Cancelar_DoWorker)
        {
            blPr_Cancelar_DoWorker = bl_Cancelar_DoWorker;
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Actualiza la lista de dispositivos de la máquina y carga una camara web si está conectada
        /// </summary>
        private void UpdateCameraList()
        {
            objPr_deviceManager = new NDeviceManager();
            cbCamera = new ComboBox();
            cbCamera.BeginUpdate();
            try
            {
                blPr_ExisteCamara = false;
                cbCamera.Items.Clear();
                objPr_deviceManager.Refresh();
                int cont = 0;
                foreach (NDevice device in objPr_deviceManager.Devices)
                {
                    cbCamera.Items.Add(device);
                    // compara por el nombre 
                    if (device.DisplayName.Equals("Logitech Webcam Pro 9000") || device.DisplayName.Equals("Logitech HD Pro Webcam C910") || device.DisplayName.Equals("Logitech HD Pro Webcam C920") || device.DisplayName.Equals("FaceTime HD Camera (Built-in)") || device.DisplayName.Equals("Integrated Webcam"))
                    //if ((device.DisplayName.Contains("Logitech") && device.DisplayName.Contains("Webcam")) || device.DisplayName.Equals("FaceTime HD Camera (Built-in)") || device.DisplayName.Equals("Integrated Webcam"))
                    {
                        cbCamera.SelectedIndex = cont;
                        blPr_ExisteCamara = true;
                        break;
                    }
                    cont++;
                }

                objPr_camera = cbCamera.SelectedItem as NCamera;
                if (objPr_camera != null && objPr_camera.IsDisposed) objPr_camera = null;

                if (objPr_camera == null && cbCamera.Items.Count > 0)
                {
                    cbCamera.SelectedIndex = 0;
                    return;
                }

                if (objPr_camera != null)
                {
                    cbCamera.SelectedIndex = cbCamera.Items.IndexOf(objPr_camera);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "UpdateCameraList",
                    "", "ArgumentNullException. System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentNullException ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "UpdateCameraList",
                    "", "ArgumentNullException: " + ex.Message, "", "");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "UpdateCameraList",
                    "", "ArgumentOutOfRangeException: " + ex.Message, "", "");
            }
            catch (Exception ex_1)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "UpdateCameraList",
                    "", "Exception: " + ex_1.Message, "", "");
            }
            finally
            {
                cbCamera.EndUpdate();
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Inicia el proceso de captura de imagen y extracción de caracteristicas
        /// </summary>
        public void StartCam()
        {
            try
            {
                UpdateCameraList();

                if (objPr_camera == null)
                {
                    objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "StartCam",
                    "", "No se encontró camara compatible conectada ", "", "");
                    return;
                }
                objPr_template = null;
                backgroundWorker = new BackgroundWorker();
                this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
                if (_bestFrame != null)
                {
                    _bestFrame.Dispose();
                    _bestFrame = null;
                }
                objPr_extractor = new NLExtractor();
                objPr_extractor.MaxStreamDurationInFrames = Convert.ToInt32(4);
                objPr_extractor.DetectAllFeaturePoints = false;
                backgroundWorker.RunWorkerAsync();
                key = 2;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "StartCam",
                    "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (InvalidOperationException ex_1)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "StartCam",
                    "", "InvalidOperationException: " + ex_1.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "StartCam",
                   "", "Exception: " + ex.Message, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Produce un nuevo evento de extracción
        /// </summary>
        public void extract()
        {
            try
            {
                if (key == 2) blPr_capture = true;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "extract",
                    "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "extract",
                    "", "Exception: " + ex.Message, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Método para enrolar un rostro desde un archivo de imagen
        /// </summary>
        /// <param name="RutaImagen"></param>
        /// <returns></returns>
        public String EnrolImage(String RutaImagen)
        {
            try
            {
                objPr_extractor = new NLExtractor();
                NImage objL_Foto = NImage.FromFile(RutaImagen);

                using (NGrayscaleImage grayscaleImage = objL_Foto.ToGrayscale())
                {
                    NleFace[] faces = objPr_extractor.DetectFaces(grayscaleImage);
                    objPr_extractor.ExtractStart();
                    NleDetectionDetails details;
                    NleExtractionStatus status;
                    NLTemplate template = objPr_extractor.Extract(grayscaleImage, out details, out status);
                    //_capturedImages.Add((NImage)objL_Foto.Clone());
                    if (status != NleExtractionStatus.None)
                    {
                        template = objPr_extractor.ExtractUsingDetails(grayscaleImage, details, out status);
                        if (status == NleExtractionStatus.TemplateCreated)
                        {
                            objPr_template = template.Save();
                            btR_Template = objPr_template.ToArray();
                            if (btR_Template.Length < 35000) return "IMAGEN DE BAJA CALIDAD";
                            template.Dispose();
                            return "TEMPLATE CREADO";
                        }
                        return "NO SE PUDO CREAR TEMPLATE";
                    }
                    return "NO SE PUDO CREAR TEMPLATE";
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "EnrolImage", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return ex_0.Message;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "EnrolImage", "", "Exception: " + ex.Message, "", "");
                return ex.Message;
            }
        }
        private bool crearImagen = false;

        [HandleProcessCorruptedStateExceptions]
        public void IniciaExtraccion()
        {
            try
            {
                if (!crearImagen)
                {
                    crearImagen = true;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "IniciaExtraccion", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                crearImagen = false;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "IniciaExtraccion", "", "Exception: " + ex.Message, "", "");
                crearImagen = false;
            }
        }



        bool extractStarted = false;

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Procedimiento en segundo plano, inicializa la camara y hace llamadas a las funciones de extraccion de caracteristicas
        /// y presentacion de imagenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            Boolean blL_break = false;
            ClasX_EventLog ObjL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog, false);
            ObjL_Log.setTextErrLog("Entrando en _C_Devices_N_4_3.BackgroundWorkerDoWork()");
            //
            lock (ObjPr_ThisLock)
            { // Inicio del lock (ObjPr_ThisLock)
                ObjL_Log.setTextErrLog("En el Lock de _C_Devices_N_4_3.BackgroundWorkerDoWork()");
                extractStarted = false;
                crearImagen = false;
                try
                { // Inicio del try
                    objPr_camera.StartCapturing();
                    blPr_capture = true;
                    //
                    while (true)
                    { // Inicio del while
                        if (blPr_Cancelar_DoWorker)
                        { // Inicio del if ( blPr_Cancelar_DoWorker ) 
                            //--return;
                            backgroundWorker.CancelAsync();
                            blL_break = true;
                            break;
                        } // Fin del if ( blPr_Cancelar_DoWorker ) 
                        else // del if ( blPr_Cancelar_DoWorker ) 
                        { // Inicio del if ( blPr_Cancelar_DoWorker ) 
                            using (NImage frame = objPr_camera.GetFrame())
                            {
                                if (frame == null || backgroundWorker.CancellationPending)
                                {
                                    updateview(null, null, NleExtractionStatus.None);
                                    return;
                                }
                                using (NGrayscaleImage grayscaleImage = frame.ToGrayscale())
                                {
                                    if (!blPr_capture)
                                    {
                                        NleFace[] faces = objPr_extractor.DetectFaces(grayscaleImage);
                                        NleDetectionDetails[] details = new NleDetectionDetails[faces.Length];
                                        for (int i = 0; i < details.Length; i++)
                                        {
                                            details[i] = objPr_extractor.DetectFacialFeatures(grayscaleImage, faces[i]);
                                        }
                                        updateview(frame.ToBitmap(), details, NleExtractionStatus.None);
                                    }
                                    else
                                    {

                                        if (!extractStarted)
                                        {
                                            objPr_extractor.ExtractStart();
                                            extractStarted = true;
                                        }
                                        NleDetectionDetails details;
                                        NleExtractionStatus status = objPr_extractor.ExtractNext(grayscaleImage, out details);
                                        _capturedImages.Add((NImage)frame.Clone());
                                        if (status != NleExtractionStatus.None)
                                        {
                                            int frameIndex;
                                            NLTemplate template = objPr_extractor.ExtractEnd(out frameIndex, out status);
                                            extractStarted = false;
                                            if (status == NleExtractionStatus.TemplateCreated)
                                            {
                                                objPr_template = template.Save();
                                                btR_Template = objPr_template.ToArray();
                                                if (!crearImagen) continue;
                                                if (btR_Template.Length < 35000) continue;
                                                template.Dispose();
                                                _bestFrame = (NImage)_capturedImages[frameIndex].Clone();
                                                updateview(_bestFrame.ToBitmap(), new NleDetectionDetails[] { details }, status);
                                                ClearCapturedImages();
                                                blPr_capture = false;
                                                // Dic 5 2014
                                                if (blPr_Cancelar_DoWorker)
                                                { // Inicio del if ( blPr_Cancelar_DoWorker ) 
                                                    //-->>return;
                                                    backgroundWorker.CancelAsync();
                                                    blL_break = true;
                                                    break;
                                                    
                                                } // Fin del if ( blPr_Cancelar_DoWorker ) 
                                                else // del if ( blPr_Cancelar_DoWorker ) 
                                                { // Inicio del if ( blPr_Cancelar_DoWorker ) 
                                                    ListoParaGrabar();
                                                    if (blPr_capture)
                                                    {
                                                        continue;
                                                    }
                                                    //--return;
                                                    blL_break = true;
                                                    break;
                                                    ClearCapturedImages();
                                                    updateview(frame.ToBitmap(), null, status);
                                                    continue;
                                                } // Fin del if ( blPr_Cancelar_DoWorker ) 
                                                // fin Dic 5 2014
                                            }
                                        }
                                        // Dic 5 2014
                                        if (!blPr_Cancelar_DoWorker)
                                        {
                                            updateview(frame.ToBitmap(), new NleDetectionDetails[] { details }, status);
                                        }
                                    }
                                }
                            }
                        } // Fin del else del if ( blPr_Cancelar_DoWorker ) 
                    } // Fin del while
                    //
                    if (!blL_break)
                    {
                        ObjL_Log.setTextErrLog("Saliendo del While en _C_Devices_N_4_3.BackgroundWorkerDoWork(). Antes de : if (extractStarted)");
                        if (extractStarted)
                        {
                            int baseFrameIndex;
                            NleExtractionStatus status;
                            objPr_extractor.ExtractEnd(out baseFrameIndex, out status);
                        }
                        if (objPr_camera != null) objPr_camera.StopCapturing();
                        ObjL_Log.setTextErrLog("Saliendo del While en _C_Devices_N_4_3.BackgroundWorkerDoWork(). Despues de :  if (objPr_camera != null) objPr_camera.StopCapturing();");
                        //
                        ObjL_Log = null;
                    }
                } // Fin del try
                catch (System.AccessViolationException ex_0)
                {
                    objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "BackgroundWorkerDoWork",
                        "", "System.AccessViolationException: " + ex_0.Message, "", "");
                }
                catch (Exception ex)
                {
                    objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "BackgroundWorkerDoWork",
                        "", "Exception: " + ex.Message, "", "");
                }
                //finally
                //{
                //    if (extractStarted)
                //    {
                //        int baseFrameIndex;
                //        NleExtractionStatus status;
                //        objPr_extractor.ExtractEnd(out baseFrameIndex, out status);
                //    }
                //    if (objPr_camera != null) objPr_camera.StopCapturing();
                //}
            } // Fin del lock (ObjPr_ThisLock)
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Guarda la imagen capturada en la ruta especificada
        /// </summary>
        /// <param name="ruta"></param>
        public void GuardarImagen(String ruta)
        {
            try
            {
                _bestFrame.Save(ruta);
                //-->>MessageBox.Show("Salvo");
                if (bl_Pr_En_Identificacion)
                {
                    this.StopCapturing();
                }

            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "GuardarImagen",
                    "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "GuardarImagen",
                    "", "Exception: " + ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// guarda el template en Base de Datos MS SQL SERVER, segun los datos entregados
        /// </summary>
        /// <param name="objR_BaseDeDatos"></param>
        /// <param name="stR_nombreTabla"></param>
        /// <param name="str_NombreCampoCedula"></param>
        /// <param name="str_NombreCampoTemplate"></param>
        /// <param name="str_Cedula"></param>
        /// <param name="maxid"></param>
        /// <returns></returns>
        public int guardartemplateMSSQL(ClasX_DBInfo objR_BaseDeDatos, String stR_nombreTabla, String str_NombreCampoCedula, String str_NombreCampoTemplate, String str_Cedula, long maxid)
        {
            String stL_Ruta = "";
            String stL_Sentencia = "";
            try
            {
                stL_Ruta = "SERVER=" + objR_BaseDeDatos.getNombreServidor() + ";" + "DATABASE=" + objR_BaseDeDatos.getNombreBaseDatos() + ";" + "UID=" + objR_BaseDeDatos.getIdUsuario_BD() + ";" + "PASSWORD=" + objR_BaseDeDatos.getClaveUsuario_BD() + ";";
                stL_Sentencia = "INSERT INTO " + stR_nombreTabla + " (Id, " + str_NombreCampoCedula + ", " + str_NombreCampoTemplate + ") VALUES (@maxid, @identificacion, @template)";
                //
                SqlConnection objL_Conexion = new SqlConnection(stL_Ruta);
                objL_Conexion.Open();
                SqlCommand objL_cmd = new SqlCommand(stL_Sentencia, objL_Conexion);
                SqlParameter objL_param = new SqlParameter("@template", SqlDbType.VarBinary);
                objL_param.Size = btR_Template.Length;
                objL_param.Value = btR_Template;
                objL_cmd.Parameters.Add(objL_param);
                long lnL_cedula = Convert.ToInt64(str_Cedula);
                objL_cmd.Parameters.Add(new SqlParameter("@maxid", maxid));
                objL_cmd.Parameters.Add(new SqlParameter("@identificacion", lnL_cedula));
                int inL_numRegAffected = objL_cmd.ExecuteNonQuery();
                objL_Conexion.Close();
                return inL_numRegAffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "guardartemplate", "", "System.AccessViolationException: Error al guardar el template" + ex_0.Message, objR_BaseDeDatos.getNombreBaseDatos(), stL_Sentencia);
                return -1;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "guardartemplate", "", "Excepcion: Error al guardar el template" + ex.Message, objR_BaseDeDatos.getNombreBaseDatos(), stL_Sentencia);
                return -1;
            }
        }

        private string sentenciaSQL = "";


        [HandleProcessCorruptedStateExceptions]
        public int guardartemplatePLPosSQL(ClasX_DBInfo ObjPr_BaseDeDatos, String str_nombreTabla, String str_NombreCampoCedula, String st_NombreCampoTemplate, String str_Cedula)
        {
            int inL_rowsAffected = -1;
            try
            {
                if (str_Cedula.Length > 0)
                {
                    long cedula = Convert.ToInt64(str_Cedula);
                    String ruta = "Server=" + ObjPr_BaseDeDatos.getNombreServidor() + ";" + "Database=" + ObjPr_BaseDeDatos.getNombreBaseDatos() + ";" + "User ID=" + ObjPr_BaseDeDatos.getIdUsuario_BD() + ";" + "Password=" + ObjPr_BaseDeDatos.getClaveUsuario_BD() + ";";
                    Npgsql.NpgsqlConnection conDB_PostGreSQL = new Npgsql.NpgsqlConnection(ruta);
                    String sentencia = "INSERT INTO " + str_nombreTabla + " (" + str_NombreCampoCedula + ", " + st_NombreCampoTemplate + ") VALUES (" + cedula + ", @template)";
                    sentenciaSQL = sentencia;
                    Npgsql.NpgsqlCommand cmdDB_PostGreSQL = new Npgsql.NpgsqlCommand(sentencia, conDB_PostGreSQL);
                    byte[] pic = btR_Template;
                    cmdDB_PostGreSQL.Parameters.AddWithValue("@template", btR_Template);
                    conDB_PostGreSQL.Open();
                    // Ejecuta el Comando
                    inL_rowsAffected = cmdDB_PostGreSQL.ExecuteNonQuery();
                    //
                    cmdDB_PostGreSQL.Dispose();
                    conDB_PostGreSQL.Close();
                }
                return inL_rowsAffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "guardartemplatePLPosSQL. System.AccessViolationException", "", ex_0.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), sentenciaSQL);
                return -1;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device", "guardartemplatePLPosSQL", "", ex.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), sentenciaSQL);
                return -1;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// guarda el template en Base de Datos MySQL, segun los datos entregados
        /// </summary>
        /// <param name="ObjPr_BaseDeDatos"></param>
        /// <param name="str_nombreTabla"></param>
        /// <param name="str_NombreCampoCedula"></param>
        /// <param name="st_NombreCampoTemplate"></param>
        /// <param name="str_Cedula"></param>
        /// <returns></returns>
        public int guardartemplateMySQL(ClasX_DBInfo ObjPr_BaseDeDatos, String str_nombreTabla, String str_NombreCampoCedula, String st_NombreCampoTemplate, String str_Cedula)
        {
            String stL_ruta = "";
            String stL_sentencia = "";
            //
            try
            {
                stL_ruta = "SERVER=" + ObjPr_BaseDeDatos.getNombreServidor() + ";" + "DATABASE=" + ObjPr_BaseDeDatos.getNombreBaseDatos() + ";" + "UID=" + ObjPr_BaseDeDatos.getIdUsuario_BD() + ";" + "PASSWORD=" + ObjPr_BaseDeDatos.getClaveUsuario_BD() + ";";
                stL_sentencia = "INSERT INTO " + str_nombreTabla + " (" + str_NombreCampoCedula + ", " + st_NombreCampoTemplate + ") VALUES (@identificacion, @template)";
                //
                MySqlConnection conexion = new MySqlConnection(stL_ruta);
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand(stL_sentencia, conexion);
                MySqlParameter param = new MySqlParameter("@template", MySqlDbType.Blob);
                param.Size = btR_Template.Length;
                param.Value = btR_Template;
                cmd.Parameters.Add(param);
                long cedula = Convert.ToInt64(str_Cedula);
                cmd.Parameters.Add(new MySqlParameter("@identificacion", cedula));
                int numregaffected = cmd.ExecuteNonQuery();
                conexion.Close();
                return numregaffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "guardartemplate", "", "System.AccessViolationException: Error al guardar el template" + ex_0.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), stL_sentencia);

                return -1;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "guardartemplate", "", "Excepcion: Error al guardar el template" + ex.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), stL_sentencia);

                return -1;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// limpia el arreglo de imagenes capturadas durante el proceso
        /// </summary>
        private void ClearCapturedImages()
        {
            try
            {
                for (int i = 0; i < _capturedImages.Count; i++)
                {
                    _capturedImages[i].Dispose();
                }
                _capturedImages.Clear();
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "ClearCapturedImages", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "ClearCapturedImages", "", "Exception: " + ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// devuelve un arreglo de bytes con el template
        /// </summary>
        /// <returns></returns>
        public byte[] getTemplate()
        {
            try
            {
                byte[] template = objPr_template.ToArray();
                return template;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "getTemplate", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return null;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "getTemplate", "", "Excepcion: " + ex.Message, "", "");
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void StopCapturing()
        {
            try
            {
                ClasX_EventLog ObjL_Log = new ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog, false);
                ObjL_Log.setTextErrLog("Entrando en _C_Devices_N_4_3.StopCapturing()");
                blPr_capture = false;
                if (objPr_camera != null)
                {
                    ObjL_Log.setTextErrLog("En _C_Devices_N_4_3.StopCapturing(). Antes de Ejecutar : objPr_camera.StopCapturing();");
                    objPr_camera.StopCapturing();
                    ObjL_Log.setTextErrLog("En _C_Devices_N_4_3.StopCapturing(). Despues de Ejecutar : objPr_camera.StopCapturing();");
                }
                ObjL_Log.setTextErrLog("Saliendo de _C_Devices_N_4_3.StopCapturing()");
                ObjL_Log = null;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "StopCapturing", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Webcam_Device",
                        "StopCapturing", "", "Excepcion: " + ex.Message, "", "");
            }
        }



    }
}
