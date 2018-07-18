using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Gui;
using Neurotec.Devices;
using Neurotec.Images;
using Neurotec.IO;
using Neurotec.Licensing;
using Neurotec.Drawing;
using Neurotec.Media;
using Neurotec.IO;
using Neurotec.Collections;
using _C_ProgRes;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;
using _C_Devices_N_4_3.Properties;


namespace _C_Devices_N_4_3
{
    /// <summary>
    /// función delegada para anunciar la terminación de la captura de la imagen de huella.
    /// Entrega a la aplicación una vista de tipo "NFView" de Neurotechnology para ser presentada en un panel
    /// </summary>
    /// <param name="vista"></param>
    public delegate void avisaFin(NFView vista);
    /// <summary>
    /// función delegada para anunciar que tanto la imagen como el template están disponibles para ser almacenados y manipulados desde la aplicación
    /// </summary>
    public delegate void ListoParaGrabar(int cuenta);
    /// <summary>
    /// delegado para generar eventos de vista previa mientras el scanner está trabajando
    /// </summary>
    /// <param name="activo"></param>
    public delegate void preview(bool activo);

    public class ClasX_FingerPrint_Device
    {
        #region constantes
        /// <summary>
        /// Dirección del servidor de licencias
        /// </summary>
        const string Address = "/local";
        /// <summary>
        /// Puerto  TCP para el servidor de licencias
        /// </summary>
        const string Port = "5000";
        /// <summary>
        /// Componentes a validar para activar el servicio
        /// </summary>
        const string Components = "Biometrics.FingerDetection,Devices.FingerScanners";
        string[] components ={"Biometrics.FingerDetection","Devices.FingerScanners",};
        #endregion

        #region campos privados
        /// <summary>
        /// ListBox para almacenar la lista de dispositivos encontrados
        /// </summary>
        private System.Windows.Forms.ListBox scannersListBox;
        /// <summary>
        /// BackgroundWorker para realizar las tareas de la captura en segundo plano
        /// </summary>
        private System.ComponentModel.BackgroundWorker scanWorker;
        /// <summary>
        /// Objeto de tipo "NFingerScanner" en el que se almacena el escaner en uso
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NFingerScanner objPr_currentScanner;
        /// <summary>
        /// Objeto de tipo "NDeviceManager" en el que se carga una colección de Dispositivos conectados a la máquina
        /// De este se puede obtener el tipo de dispositivo requerido para usar en la captura de huellas
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NDeviceManager objPr_deviceMan;
        /// <summary>
        /// Objeto de tipo "NFView" este contiene una vista de la captura del dispositivo ESCANER de huellas
        /// A partir de esta vista se obtienen, la imagen, el Template de la huella y se presenta la vista previa de la captura en las aplicaciones
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NFView objPr_nfView = null; //Strail = new NFView();
        /// <summary>
        /// Objeto de tipo "NBuffer" este contiene el Template una vez ha sido generado
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NBuffer objPr_template;
        /// <summary>
        /// Objeto de tipo "NFExtractor" realiza la operacion de extraccion del Template de una imagen
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NFExtractor objPr_extractor;
        /// <summary>
        /// Objeto de tipo "NGrayscaleImage" almacena una imagen en escala de grises de la captura realizada
        /// Este tipo de datos es propio de Neurotechnology
        /// </summary>
        private NGrayscaleImage objPr_imageShow;
        /// <summary>
        /// Objeto "ClasX_EventLog" para el manejo de log de los eventos de error de la clase
        /// </summary>
        private ClasX_EventLog objPr_log = null;
        /// <summary>
        /// Contiene el usuario actual de la aplicación
        /// </summary>
        private String stPr_userApp = "";
        /// <summary>
        /// Contiene la ruta en la cual será almacenado el archivo de log de la aplcación
        /// </summary>
        private String stPr_PathLog = "FingerPrint_Device.log";
        /// <summary>
        /// Contiene el estado actual del servicio de obtención de licenias
        /// True = activación correcta; False = activación incorrecta
        /// </summary>
        private bool blPr_licStatus = false;
        /// <summary>
        /// Estado del dispositivo, se pudo cargar si o no?
        /// </summary>
        private bool blPr_DevStatus = true;
        /// <summary>
        /// controla si un proceso ha sido cancelado
        /// </summary>
        private bool cancelProc = false;
        /// <summary>
        /// Contiene el numero máximo de capturas que se deben hacer en cada acción de escaneo
        /// </summary>
        private int inPr_NumCapturas = 0;
        /// <summary>
        /// Lleva la cuenta del numero de capturas realizadas
        /// </summary>
        private int inPr_contador = 1;
        private string sentenciaSQL = "";

        private Boolean blPr_ExisteHuellero = false;
        /// <summary>
        /// Indica si ay un error en el huellero, porque se bloqueo o porque la licencia esta caida
        /// </summary>
        private bool errorenhuellero = false;
        /// <summary>
        /// Indica el tiempo en milisegundos que el huellero va a esperar antes de desconectarse
        /// </summary>
        private int timeoutHuellero = 20000;
        private bool Bl_Errorcaptura = false;


        //NFingerScanner scanner;

        #endregion

        #region campos públicos
        /// <summary>
        /// Evento de tipo público para realizar tareas al terminar la captura de una imagen
        /// luego de la ejecución de este evento, el BackgroundWorker continúa llevando a cabo las tareas en segundo plano
        /// </summary>
        public event avisaFin avisarTermine;
        /// <summary>
        /// Evento de tipo público para realizar tareas al confirmar que hay una imagen y un Template válidos
        /// luego de la ejecución de este evento, el BackgroundWorker continúa llevando a cabo las tareas en segundo plano
        /// </summary>
        public event ListoParaGrabar AvisaGrabar;
        /// <summary>
        /// Evento público para generar vistas previas mientras el escaner trabaja
        /// </summary>
        //public event preview VistaPrevia;
        #endregion

        /// <summary>
        /// Constructor de la clase.
        /// En este se realiza la activación del servicio de extracción de Neurotechnology
        /// de esta manera una vez creado el objeto puede ser utilizado este servicio y una vez destruido el servicio es liberado
        /// </summary>
        /// <param name="stR_UsuarioApp">Usuario actual de la aplicacion</param>
        /// <param name="stPr_ArchivoLog">ruta de almacenamiento del log, incluido el nombre del archivo</param>
        //public ClasX_FingerPrint_Device(String stR_UsuarioApp, String stPr_ArchivoLog)
        //{
        //    stPr_userApp = stR_UsuarioApp;
        //    stPr_PathLog = stPr_ArchivoLog;
        //    objPr_log = new ClasX_EventLog(stPr_userApp, stPr_PathLog, false, true, false, true);
        //    objPr_log.setPathArchivoLogErr(stPr_PathLog);
        //    objPr_log.setUser(stPr_userApp);
        //    objPr_log.setSalConsole(false);
        //    objPr_log.setSalDialog(false);
        //    objPr_log.setSalLog(true);

        //    try
        //    {

        //        blPr_licStatus = NLicense.ObtainComponents(Address, Port, Components);

        //        if (!blPr_licStatus)
        //        {
        //            objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
        //             "ClasX_FingerPrint_Device:Constructor", "",
        //             "NO SE PUDO OBTENER LICENCIA DE EXTRACCION DE HUELLA", "", "");
        //        }
        //        else
        //        {
        //            objPr_nfView = new NFView();
        //            scanWorker = new BackgroundWorker();
        //            scanWorker.WorkerSupportsCancellation = true;
        //            this.scanWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ScanWorkerDoWork);
        //            this.scanWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ScanWorkerRunWorkerCompleted);
        //        }
        //    }
        //    catch (System.AccessViolationException ex_0)
        //    {
        //        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
        //            "ClasX_FingerPrint_Device:Constructor. System.AccessViolationException", "",
        //            "Problemas obteniendo las licencias \n LicenseException :" + ex_0.Message, "", "");
        //    }
        //    catch (LicenseException ex)
        //    {
        //        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
        //            "ClasX_FingerPrint_Device:Constructor", "",
        //            "Problemas obteniendo las licencias \n LicenseException :" + ex.Message, "", "");
        //    }
        //    catch (Exception ex)
        //    {
        //        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
        //            "ClasX_FingerPrint_Device:Constructor", "",
        //            "Problemas obteniendo las licencias Exception :" + ex.Message, "", "");
        //    }
        //}






         public ClasX_FingerPrint_Device(String stR_UsuarioApp, String stPr_ArchivoLog)
        {
            stPr_userApp = stR_UsuarioApp;
            stPr_PathLog = stPr_ArchivoLog;
            objPr_log = new ClasX_EventLog(stPr_userApp, stPr_PathLog, false, true, false, true);
            objPr_log.setPathArchivoLogErr(stPr_PathLog);
            objPr_log.setUser(stPr_userApp);
            objPr_log.setSalConsole(false);
            objPr_log.setSalDialog(false);
            objPr_log.setSalLog(true);

            try
            {
                //bool anyObtained = false;
                foreach (string component in components)
                {
                    blPr_licStatus = NLicense.ObtainComponents(Address, Port, component);
                }
                if (!blPr_licStatus)
                {
                    MessageBox.Show(string.Format("Could not obtain licenses for any of components: {0}", components));
                    objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                     "ClasX_FingerPrint_Device:Constructor", "",
                     "NO SE PUDO OBTENER LICENCIA DE EXTRACCION DE HUELLA", "", "");
                    return;
                }
                else
                {
                    objPr_nfView = new NFView();
                    scanWorker = new BackgroundWorker();
                    scanWorker.WorkerSupportsCancellation = true;
                    this.scanWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ScanWorkerDoWork);
                    this.scanWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ScanWorkerRunWorkerCompleted);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                    "ClasX_FingerPrint_Device:Constructor. System.AccessViolationException", "",
                    "Problemas obteniendo las licencias \n LicenseException :" + ex_0.Message, "", "");
            }
            catch (LicenseException ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                    "ClasX_FingerPrint_Device:Constructor", "",
                    "Problemas obteniendo las licencias \n LicenseException :" + ex.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                    "ClasX_FingerPrint_Device:Constructor", "",
                    "Problemas obteniendo las licencias Exception :" + ex.Message, "", "");
            }
        }



         
               












        public void setTimeoutHuellero(int dato)
        {
            timeoutHuellero = dato;
        }

        public Boolean getExisteHuellero()
        {
            return blPr_ExisteHuellero;
        }

        public void setExisteHuellero(Boolean bl_ExisteHuellero)
        {
            blPr_ExisteHuellero = bl_ExisteHuellero;
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Inicializa el dispositivo y la captura de las caracteristicas
        /// </summary>
        /// <param name="inR_numCapturas">Cantidad de huellas a capturar, si recibe como parametro -1 captura huellas indefinidamente hasta cancelar el proceso</param>
        public void ScannAction(int inR_numCapturas)
        {
            try
            {
                inPr_NumCapturas = inR_numCapturas;
                if (inPr_NumCapturas != 0)
                { // Inicio del if ( inPr_NumCapturas != 0 ) 
                    //objPr_templateArray = new NBuffer[inR_numCapturas];
                    //objPr_imageShowArray = new NGrayscaleImage[inR_numCapturas];
                    UpdateScannerList();
                    NFingerScanner scanner = scannersListBox.SelectedItem as NFingerScanner;
                    objPr_currentScanner = scanner;
                    //Strail scanWorker = new BackgroundWorker();
                    if (scanner == null)
                    {
                        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ScannAction", "", "Escaner no detectado, verifique la conexión del dispositivo", "", "");
                        //AGR 29082016 blPr_DevStatus = false;
                        blPr_ExisteHuellero = false;
                        return;
                    }
                    else
                    {
                        blPr_ExisteHuellero = true;
                    }
                    blPr_DevStatus = true;
                    //if (scanWorker == null)
                    //{
                    //    return;
                    //}
                    if (scanWorker == null)
                    {
                        errorenhuellero = true;
                        return;
                        //objPr_nfView = new NFView();
                        //scanWorker = new BackgroundWorker();
                        //scanWorker.WorkerSupportsCancellation = true;
                        //this.scanWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ScanWorkerDoWork);
                        //this.scanWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ScanWorkerRunWorkerCompleted);
                    }
                    if (scanWorker.IsBusy)
                    {
                        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ScannAction", "", "El escaner se encuentra trabajando actualmente, ¡escaneo en proceso!", "", "");
                        return;
                    }
                    
                    //scanWorker.WorkerSupportsCancellation = true;
                    //this.scanWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ScanWorkerDoWork);
                    //this.scanWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ScanWorkerRunWorkerCompleted);
                    scanWorker.RunWorkerAsync(scanner);
                } // Fin del if ( inPr_NumCapturas != 0 ) 
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "ScannAction", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Actualiza la lista de dispositivos y selecciona uno disponible
        /// </summary>
        public void UpdateScannerList()
        {
            try
            {
                blPr_ExisteHuellero = false;
                objPr_deviceMan = new NDeviceManager();
                scannersListBox = new ListBox();
                int cont = 0;

                foreach (NDevice device in objPr_deviceMan.Devices)
                {
                    scannersListBox.Items.Add(device);
                    if (device.DisplayName.Equals("DigitalPersona, Inc. U.are.U® 4500 Fingerprint Reader") || device.DisplayName.Equals("Upek TCS1C #1") || device.DisplayName.Equals("Cross Match Fast Verifier 300LC2 Series") || device.DisplayName.Contains("Futronic FS88"))
                    {
                        blPr_ExisteHuellero = true;
                        scannersListBox.SelectedIndex = cont;
                        break;
                    }

                    cont++;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "UpdateScannerList", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "UpdateScannerList", "", "Excepcion: " + ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// cancela el proceso actual de captura de imagen
        /// </summary>
        /// <returns></returns>
        public bool CancelScanning()
        {
            try
            {
                if (scanWorker == null)
                { // Inicio del if ( scanWorker != null ) 
                    Thread.Sleep(10);
                    Application.DoEvents();
                    objPr_log.setTextErrLog("ClasX_FingerPrint_Device.CancelScanning, scanWorker == null");
                    return false;
                }
                else
                {
                    if (scanWorker.IsBusy)
                    {
                        scanWorker.CancelAsync();
                        if (objPr_currentScanner != null)
                        {
                            MethodInvoker invoker = new MethodInvoker(objPr_currentScanner.Cancel);
                            IAsyncResult deviceCancelResult = invoker.BeginInvoke(null, null);
                            while (!deviceCancelResult.IsCompleted)
                            {
                                Thread.Sleep(10);
                                Application.DoEvents();
                                objPr_log.setTextErrLog("ClasX_FingerPrint_Device.CancelScanning, esperando IsCompleted");
                            }
                            invoker.EndInvoke(deviceCancelResult);
                        }

                        while (scanWorker.IsBusy)
                        {
                            objPr_log.setTextErrLog("ClasX_FingerPrint_Device.CancelScanning, esperando IsBusy");
                            Thread.Sleep(10);
                            Application.DoEvents();
                        }
                        cancelProc = true;
                        return true;
                    }
                    else
                    {
                        scanWorker.CancelAsync();
                        cancelProc = true;
                        return true;
                    }
                } // Fin del if ( scanWorker != null ) 
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "CancelScanning", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "CancelScanning", "", "ArgumentOutOfRangeException: " + ex.Message, "", "");
                return false;
            }
            catch (Exception ex_1)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "CancelScanning", "", "Exception: " + ex_1.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// metodo para ejecutar la tarea del background worker (Tarea en segundo plano)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            

            //scanner.Preview += new EventHandler<NFScannerPreviewEventArgs>(ScannerPreview);

            try
            {
                

                NFingerScanner scanner = (NFingerScanner)e.Argument;
                objPr_currentScanner = scanner;
                //e.Result = scanner.Capture(5000);
                e.Result = scanner.Capture(timeoutHuellero);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "ScanWorkerDoWork", "", "System.AccessViolationException_1: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                //objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        //"ScanWorkerDoWork", "", "Excepcion_1: " + ex.Message, "", "");
            }
            finally
            {
                //scanner.Preview -= new EventHandler<NFScannerPreviewEventArgs>(ScannerPreview);
            }
            try
            {

                if (((BackgroundWorker)sender).CancellationPending)
                {
                    e.Cancel = true;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "ScanWorkerDoWork", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "ScanWorkerDoWork", "", "Excepcion: " + ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo para guardar la imagen capturada en una ruta especificada
        /// </summary>
        /// <param name="imagen_foto">ruta completa de almacenamiento de la imagen, incluido el nombre del archivo</param>
        public void GuardarImagen(string imagen_foto)
        {
            if (objPr_nfView.Image == null) return;
            try
            {
                objPr_imageShow.Save(imagen_foto);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "GuardarImagen", "", "System.AccessViolationException: Error al guardar el archivo\\imagen_foto" + ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "GuardarImagen", "", "Excepcion: Error al guardar el archivo\\imagen_foto" + ex.Message, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public int guardartemplateMSSQL(ClasX_DBInfo ObjPr_BaseDeDatos, String str_nombreTabla, String str_NombreCampoCedula, String st_NombreCampoTemplate, String str_Cedula, byte[] btr_template, long maxId)
        {
            //
            String ruta = "SERVER=" + ObjPr_BaseDeDatos.getNombreServidor() + ";" + "DATABASE=" + ObjPr_BaseDeDatos.getNombreBaseDatos() + ";" + "UID=" + ObjPr_BaseDeDatos.getIdUsuario_BD() + ";" + "PASSWORD=" + ObjPr_BaseDeDatos.getClaveUsuario_BD() + ";";
            String sentencia = "INSERT INTO " + str_nombreTabla + " (Id, " + str_NombreCampoCedula + ", " + st_NombreCampoTemplate + ") VALUES (@maxid, @identificacion, @template)";
            //
            try
            {
                SqlConnection conexion = new SqlConnection(ruta);
                conexion.Open();
                SqlCommand cmd = new SqlCommand(sentencia, conexion);
                SqlParameter param = new SqlParameter("@template", MySqlDbType.VarBinary);
                param.Size = btr_template.Length;
                param.Value = btr_template;
                cmd.Parameters.Add(param);
                long cedula = Convert.ToInt64(str_Cedula);
                cmd.Parameters.Add(new SqlParameter("@maxid", maxId));
                cmd.Parameters.Add(new SqlParameter("@identificacion", cedula));
                int numregaffected = cmd.ExecuteNonQuery();
                conexion.Close();
                return numregaffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "guardarTemplate", "", "System.AccessViolationException: Error al guardar el template" + ex_0.Message, "", "");

                return -1;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "guardarTemplate", "", "Excepcion: Error al guardar el template" + ex.Message, "", "");

                return -1;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// metodo para guardar el template en una base de datos POSGRESQL especificada
        /// </summary>
        /// <param name="ObjPr_BaseDeDatos"></param>
        /// <param name="str_nombreTabla"></param>
        /// <param name="str_NombreCampoCedula"></param>
        /// <param name="st_NombreCampoTemplate"></param>
        /// <param name="str_Cedula"></param>
        /// <param name="btr_template"></param>
        /// <returns></returns>
        public int guardartemplatePLPosSQL(ClasX_DBInfo ObjPr_BaseDeDatos, String str_nombreTabla, String str_NombreCampoCedula, String st_NombreCampoTemplate, String str_Cedula, byte[] btr_template)
        {
            try
            {
                long cedula = Convert.ToInt64(str_Cedula);
                //
                String ruta = "Server=" + ObjPr_BaseDeDatos.getNombreServidor() + ";" + "Database=" + ObjPr_BaseDeDatos.getNombreBaseDatos() + ";" + "User ID=" + ObjPr_BaseDeDatos.getIdUsuario_BD() + ";" + "Password=" + ObjPr_BaseDeDatos.getClaveUsuario_BD() + ";";
                Npgsql.NpgsqlConnection conDB_PostGreSQL = new Npgsql.NpgsqlConnection(ruta);
                String sentencia = "INSERT INTO " + str_nombreTabla + " (" + str_NombreCampoCedula + ", " + st_NombreCampoTemplate + ") VALUES (" + cedula + ", @template)";
                sentenciaSQL = sentencia;
                Npgsql.NpgsqlCommand cmdDB_PostGreSQL = new Npgsql.NpgsqlCommand(sentencia, conDB_PostGreSQL);
                byte[] pic = btr_template;
                cmdDB_PostGreSQL.Parameters.AddWithValue("@template", pic);
                conDB_PostGreSQL.Open();
                // Ejecuta el Comando
                int inL_rowsAffected = cmdDB_PostGreSQL.ExecuteNonQuery();
                //
                cmdDB_PostGreSQL.Dispose();
                conDB_PostGreSQL.Close();
                return inL_rowsAffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device", "guardartemplatePLPosSQL. System.AccessViolationException", "", ex_0.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), sentenciaSQL);
                return -1;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device", "guardartemplatePLPosSQL", "", ex.Message, ObjPr_BaseDeDatos.getNombreBaseDatos(), sentenciaSQL);
                return -1;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// metodo para guardar el template en una base de datos MySQL especificada
        /// </summary>
        /// <param name="ObjPr_BaseDeDatos">Objeto de tipo ClasX_DBInfo con la informacion de conexion a la BD</param>
        /// <param name="str_nombreTabla">Nombre de la taba donde se insertará el registro</param>
        /// <param name="str_NombreCampoCedula">Nombre del campo de la tabla en el que se almacena el numero de cedula a insertar</param>
        /// <param name="st_NombreCampoTemplate">Nombre del campo de la tabla en el que se almacena el template a insertar</param>
        /// <param name="str_Cedula">Numero de cedula del enrolamiento actual</param>
        /// <param name="btr_template">arreglo de bytes que contiene el template</param>
        /// <returns></returns>
        public int guardarTemplateMySQL(ClasX_DBInfo ObjPr_BaseDeDatos, String str_nombreTabla, String str_NombreCampoCedula, String st_NombreCampoTemplate, String str_Cedula, byte[] btr_template)
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
                param.Size = btr_template.Length;
                param.Value = btr_template;
                cmd.Parameters.Add(param);
                long cedula = Convert.ToInt64(str_Cedula);
                cmd.Parameters.Add(new MySqlParameter("@identificacion", cedula));
                int numregaffected = cmd.ExecuteNonQuery();
                conexion.Close();
                return numregaffected;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "guardarTemplate", "", "System.AccessViolationException: Error al guardar el template" + ex_0.Message, "", "");

                return -1;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "guardarTemplate", "", "Excepcion: Error al guardar el template" + ex.Message, "", "");

                return -1;
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
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "getTemplate", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return null;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                        "getTemplate", "", "Excepcion: " + ex.Message, "", "");
                return null;
            }
        }



        /// <summary>
        /// Devuelve el estado actual de las licencias
        /// true: disponibles (activas)
        /// false: no disponible ó inactiva
        /// </summary>
        /// <returns></returns>
        public bool getLicStatus()
        {
            return blPr_licStatus;
        }

        public bool getblPr_DevStatus()
        {
            return blPr_DevStatus;
        }

        /// <summary>
        /// devuelve una imagen en escala de grises del tipo NGrayscaleimage de Neurotechnology
        /// </summary>
        /// <returns></returns>
        public NGrayscaleImage GetImagen()
        {
            return objPr_imageShow;
        }

        /// <summary>
        /// devuelve la vista creada con la imagen y sus caracteristicas
        /// </summary>
        /// <returns></returns>
        public NFView getVista()
        {
            return objPr_nfView;
        }

        public bool getErrorenHuellero()
        {
            //if (errorenhuellero)
            //{
            //    blPr_licStatus = NLicense.ObtainComponents(Address, Port, Components);
            //    if (blPr_licStatus)
            //    {
            //        errorenhuellero = false;
            //    }
            //    else
            //    {
            //        errorenhuellero = true;
            //    }
            //}
           return errorenhuellero ;
        }


        public Boolean getErrorCaptura()
        {
            return Bl_Errorcaptura;
        }


        [HandleProcessCorruptedStateExceptions]
        public String EnrolImage(String rutaImagen)
        {
            String resultado = "";

            try
            {
                NImage image = null;
                
                //Crea un objeto Image a partir del archivo especificado
                if (File.Exists(rutaImagen))
                {
                    image = NImage.FromFile(rutaImagen);
                }
                //
                float horiResol = image.HorzResolution;
                float verResol = image.VertResolution;
                if (horiResol < 250.0f || verResol < 250.0f)
                {
                    image.HorzResolution = 500.0f;
                    image.VertResolution = 500.0f;
                }
                if (image == null) return "NO FUE POSIBLE CARGAR LA IMAGEN";

                objPr_nfView.Width = (int)image.Width;
                objPr_nfView.Height = (int)image.Height;
                objPr_nfView.Image = image.ToBitmap();
                objPr_nfView.Refresh();
                //objPr_imageShow = (NGrayscaleImage)image.Clone();
                NGrayscaleImage grayscaleImage;
                NRgbImage nrgbscaleimage;
                if (rutaImagen.Contains("RES"))
                {
                    nrgbscaleimage = (NRgbImage)image.Clone();
                    NFRecord record = null;
                    NfeExtractionStatus extractionStatus = new NfeExtractionStatus();
                    objPr_extractor = new NFExtractor();
                    NGrayscaleImage grayscale = nrgbscaleimage.ToGrayscale();
                    record = objPr_extractor.Extract(grayscale, NFPosition.Unknown, NFImpressionType.LiveScanPlain, out extractionStatus);
                    if (extractionStatus == NfeExtractionStatus.TemplateCreated)
                    {
                        objPr_template = record.Save();
                        objPr_nfView.Template = record;
                        return resultado = "TEMPLATE CREADO CORRECTAMENTE";

                    }
                    else
                    {
                        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ScanWorkerRunWorkerCompleted", "", "Imagen de baja calidad", "", "");
                        return resultado = "IMAGEN DE BAJA CALIDAD";
                    }
                }
                else
                {
                    try
                    {
                        grayscaleImage = (NGrayscaleImage)image.Clone();
                    }
                    catch (System.AccessViolationException ex_0)
                    {
                        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
    "EnrolImage", "", "System.AccessViolationException_1 : Extraction error " + ex_0.Message, "", "");
                        grayscaleImage = null;
                    }
                    catch (Exception ex)
                    {
                        grayscaleImage = image.ToGrayscale();
                    }
                    NFRecord record = null;
                    NfeExtractionStatus extractionStatus = new NfeExtractionStatus();
                    objPr_extractor = new NFExtractor();
                    record = objPr_extractor.Extract(grayscaleImage, NFPosition.Unknown, NFImpressionType.LiveScanPlain, out extractionStatus);
                    if (extractionStatus == NfeExtractionStatus.TemplateCreated)
                    {
                        objPr_template = record.Save();
                        objPr_nfView.Template = record;
                        return resultado = "TEMPLATE CREADO CORRECTAMENTE";

                    }
                    else
                    {
                        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ScanWorkerRunWorkerCompleted", "", "Imagen de baja calidad", "", "");
                        return resultado = "IMAGEN DE BAJA CALIDAD";
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                    "EnrolImage", "", "System.AccessViolationException: Extraction error " + ex_0.Message, "", "");
                return resultado = "ERROR DE EXTRACCION: " + ex_0.Message;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                    "EnrolImage", "", "Excepcion: Extraction error " + ex.Message, "", "");
                return resultado = "ERROR DE EXTRACCION: " + ex.Message;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Método para ejecutar en segundo plano una vez realizada la captura de una imagen y sus caracteristicas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (inPr_NumCapturas != 0)
                { // inicio del if ( inPr_NumCapturas != 0 ) 
                    if (inPr_NumCapturas == -1)
                    {
                        objPr_currentScanner = null;

                        if (objPr_nfView.Image != null)
                        {
                            objPr_nfView.Image.Dispose();
                            objPr_nfView.Image = null;
                        }
                        if (objPr_nfView.Template != null)
                        {
                            objPr_nfView.Template.Dispose();
                            objPr_nfView.Template = null;
                        }

                        if (e.Error != null)
                        {
                            objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                "ScanWorkerRunWorkerCompleted", "", "Error: " + e.ToString(), "", "");
                            return;
                        }

                        if (e.Cancelled)
                        {
                            objPr_log.setTextErrLog("ClasX_FingerPrint_Device.ScanWorkerRunWorkerCompleted : e.Cancelled");
                            return;
                        }

                        NImage image = e.Result as NImage;
                        if (image == null)
                        {
                             errorenhuellero = true;
                            Bl_Errorcaptura = true;
                            //objPr_log.setTextErrLog("ClasX_FingerPrint_Device.ScanWorkerRunWorkerCompleted : ERROR(0) : El huellero no entrega template, licencia caida o esta bloqueado. T.O.");
                            //MessageBox.Show("Error en la captura de imagen, por favor repita el procedimiento", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        else
                        {
                            Bl_Errorcaptura = false;
                            errorenhuellero = false;
                            objPr_nfView.Width = (int)image.Width;
                            objPr_nfView.Height = (int)image.Height;
                            objPr_nfView.Image = image.ToBitmap();
                            objPr_nfView.Refresh();
                            avisarTermine(objPr_nfView);

                            objPr_imageShow = (NGrayscaleImage)image.Clone();
                            NGrayscaleImage grayscaleImage = (NGrayscaleImage)image.Clone();
                            NFRecord record = null;
                            try
                            {
                                NfeExtractionStatus extractionStatus = new NfeExtractionStatus();
                                objPr_extractor = new NFExtractor();
                                record = objPr_extractor.Extract(grayscaleImage, NFPosition.Unknown, NFImpressionType.LiveScanPlain, out extractionStatus);
                                if (extractionStatus == NfeExtractionStatus.TemplateCreated)
                                {
                                    objPr_template = record.Save();
                                    objPr_nfView.Template = record;
                                    AvisaGrabar(inPr_contador);
                                    if (!cancelProc) ReiniciarProceso();
                                }
                                else
                                {
                                    objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                        "ScanWorkerRunWorkerCompleted", "", "Imagen de baja calidad", "", "");
                                    AvisaGrabar(-1);
                                    if (!cancelProc) ReiniciarProceso();
                                }

                            }
                            catch (Exception ex)
                            {
                                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                    "ScanWorkerRunWorkerCompleted", "", "Excepcion: Extraction error " + ex.Message, "", "");
                            }
                        }
                        
                        //errorenhuellero = false;
                        
                    }
                    else
                    {
                        if (inPr_contador <= inPr_NumCapturas)
                        {
                            objPr_currentScanner = null;

                            if (objPr_nfView.Image != null)
                            {
                                objPr_nfView.Image.Dispose();
                                objPr_nfView.Image = null;
                            }
                            if (objPr_nfView.Template != null)
                            {
                                objPr_nfView.Template.Dispose();
                                objPr_nfView.Template = null;
                            }

                            if (e.Error != null)
                            {
                                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                    "getTemplate", "", "Error: " + e.ToString(), "", "");
                                return;
                            }

                            if (e.Cancelled)
                            {
                                objPr_log.setTextErrLog("ClasX_FingerPrint_Device.ScanWorkerRunWorkerCompleted : e.Cancelled(1)");
                                return;
                            }

                            NImage image = e.Result as NImage;
                            if (image == null)
                            {
                                errorenhuellero = true;
                                objPr_log.setTextErrLog("ClasX_FingerPrint_Device.ScanWorkerRunWorkerCompleted : ERROR : El huellero no entrega template, la licencia debe estar caida o esta bloqueado");
                                MessageBox.Show("Error al crear template de huella, intente nuevamente. Si este mensaje persiste; Finalice e inicie la aplicación e informe a Centro de Servicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            errorenhuellero = false;
                            objPr_nfView.Width = (int)image.Width;
                            objPr_nfView.Height = (int)image.Height;
                            objPr_nfView.Image = image.ToBitmap();
                            objPr_nfView.Refresh();
                            avisarTermine(objPr_nfView);
                            objPr_imageShow = (NGrayscaleImage)image.Clone();
                            NGrayscaleImage grayscaleImage = (NGrayscaleImage)image.Clone();
                            NFRecord record = null;
                            try
                            {
                                NfeExtractionStatus extractionStatus = new NfeExtractionStatus();
                                objPr_extractor = new NFExtractor();
                                record = objPr_extractor.Extract(grayscaleImage, NFPosition.Unknown, NFImpressionType.LiveScanPlain, out extractionStatus);
                                if(record != null)
                                {
                                    if (extractionStatus == NfeExtractionStatus.TemplateCreated)
                                    {
                                        objPr_template = record.Save();
                                        objPr_nfView.Template = record;
                                        AvisaGrabar(inPr_contador);
                                        ReiniciarProceso();
                                    }
                                    else
                                    {
                                        objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                        "ScanWorkerRunWorkerCompleted", "", "Imagen de baja calidad", "", "");
                                        MessageBox.Show("Imagen de mala calidad, intente nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        Bl_Errorcaptura = true;
                                        ReiniciarProceso();
                                        Bl_Errorcaptura = false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Error al crear template de huella. '" + extractionStatus + "'. Reinicie el proceso de captura o finalice e inicie la aplicación.");
                                    objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device", "ScanWorkerRunWorkerCompleted", "", "Error al crear template de huella", "");
                                }
                                

                            }
                            catch (System.AccessViolationException ex_0)
                            {
                                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                    "ScanWorkerRunWorkerCompleted", "", "System.AccessViolationException: Extraction error " + ex_0.Message, "", "");
                            }
                            catch (Exception ex)
                            {
                                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                    "ScanWorkerRunWorkerCompleted", "", "Excepcion: Extraction error " + ex.Message, "", "");
                                MessageBox.Show("Error de licencia, el enrolamiento de huellas no puede continuar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                } // Fin del if ( inPr_NumCapturas != 0 ) 
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ScanWorkerRunWorkerCompleted", "", "System.AccessViolationException_2: " + ex_0.Message, "", "");
            }
            catch (InvalidOperationException ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ScanWorkerRunWorkerCompleted", "", "InvalidOperationException_2: " + ex.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ScanWorkerRunWorkerCompleted", "", "Excepcion_2: " + ex.Message, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// metodo para reiniciar los recursos y comenzar una nueva captura
        /// </summary>
        public void ReiniciarProceso()
        {
            try
            {
                cancelProc = false;
                if (inPr_NumCapturas != 0)
                { // Inicio del if ( inPr_NumCapturas != 0 ) 
                    if (inPr_NumCapturas == -1)
                    {
                        UpdateScannerList();
                        NFingerScanner scanner = scannersListBox.SelectedItem as NFingerScanner;

                        if (scanner == null)
                        {
                            objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                    "ReiniciarProceso", "", "No se encuentra un escáner seleccionado", "", "");
                            return;
                        }
                        if (scanWorker.IsBusy)
                        {
                            objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                    "ReiniciarProceso", "", "Escáner trabajando, escaneo en proceso", "", "");
                            return;
                        }
                        scanWorker.RunWorkerAsync(scanner);
                    }
                    else
                    {
                            UpdateScannerList();
                            NFingerScanner scanner = scannersListBox.SelectedItem as NFingerScanner;

                            if (scanner == null)
                            {
                                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                        "ReiniciarProceso", "", "No se encuentra un escáner seleccionado", "", "");
                                return;
                            }
                            if (scanWorker.IsBusy)
                            {
                                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                                        "ReiniciarProceso", "", "Escáner trabajando, escaneo en proceso", "", "");
                                return;
                            }
                            scanWorker.RunWorkerAsync(scanner);
                            if (Bl_Errorcaptura)
                            {
                                if (inPr_contador == 1)
                                {
                                    inPr_contador = inPr_contador -1;
                                }
                            }
                            else
                            {
                                inPr_contador++;
                            }
                        }
                    
                } // Fin del if ( inPr_NumCapturas != 0 ) 
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ReiniciarProceso", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (InvalidOperationException ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ReiniciarProceso", "", "InvalidOperationException: " + ex.Message, "", "");
            }
            catch (Exception ex_1)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device",
                            "ReiniciarProceso", "", "Exception: " + ex_1.Message, "", "");
            }

        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// detiene el funcionamiento del escaner de huella activo
        /// </summary>
        /// <returns></returns>
        public bool StopAction()
        {
            try
            {
                // Dic 11 2014. Se coloca el If
                //--Dic-11-2014--scanner.Dispose();
                //if (scanner != null)
                //{
                //    scanner.Dispose();
                //}
                // Fin Dic 11 2014. Se coloca el If
                return true;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device", "StopAction", "",
                    "System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                objPr_log.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FingerPrint_Device", "StopAction", "",
                    "Excepcion: " + ex.Message, "", "");
                return false;
            }
        }
        



    }
}
