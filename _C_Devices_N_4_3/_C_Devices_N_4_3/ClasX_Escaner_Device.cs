using System;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Specialized;
using System.IO;
using Dll_ScannerDocument;
using _C_ProgRes;
using System.Runtime.ExceptionServices;


namespace _C_Devices_N_4_3
{
    public class ClasX_Escaner_Device
    {
        //private bool blpr_LicStatus = false;
        private NetScanW.ISLibEx objPr_SDKSLib;
        private NetScanWex.ISLibEx objPr_SDKSLibEx;
        private NetScanW.IIdData objPr_SDKIDData;
        private NetScanWex.IIdData objPr_SDKIDDataEx;
        private NetScanW.ICImage objPr_SDKImage;
        private NetScanWex.CImageClass objPr_SDKImageEx;
        private NetScanW.ICOcr objPr_SDKOcr;
        private _C_ProgRes.ClasX_EventLog objPr_ClasX_EventLog;
        private Metodos objPr_xet;
        private String StPr_Licencia = "WK19DYWA6WTK78BU";
        private String stPr_UsuarioApp;
        private String stPr_PathLog;
        private StringCollection _ignoreFields;
        //private string[] _imagePaths;
        private string stPr_tempPath = "escaneres\\";
        // -------------------------
        private short inPr_Resolution = 300;
        private short inPr_ColorScheme = -256;
        // -------------------------
  

        public ClasX_Escaner_Device(String stR_UsuarioApp, String StR_PathLog)
        {
            this.stPr_UsuarioApp = stR_UsuarioApp;
            this.stPr_PathLog = StR_PathLog;
            objPr_ClasX_EventLog = new ClasX_EventLog(stR_UsuarioApp, StR_PathLog, false, true, false, true);
            try
            {
                objPr_xet = new Metodos();
                CargarLibrerias();
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "ClasX_Escaner_Device(1). System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "ClasX_Escaner_Device(1)", "", ex.Message, "", "");
            }
        }

        public ClasX_Escaner_Device(String stR_UsuarioApp, String StR_PathLog, String PathTemporalImagenes)
        {
            this.stPr_UsuarioApp = stR_UsuarioApp;
            this.stPr_PathLog = StR_PathLog;
            objPr_ClasX_EventLog = new ClasX_EventLog(stR_UsuarioApp, StR_PathLog, false, true, false, true);
            this.stPr_tempPath = PathTemporalImagenes;
            try
            {
                objPr_xet = new Metodos();
                CargarLibrerias();
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "ClasX_Escaner_Device(2). System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "ClasX_Escaner_Device(2)", "", ex.Message, "", "");
            }
        }

        public ClasX_Escaner_Device(ClasX_EventLog ObjR_EventLog, String PathTemporalImagenes)
        {
            this.stPr_UsuarioApp = ObjR_EventLog.getUser();
            this.stPr_PathLog = ObjR_EventLog.getPathArchivoLogErr();
            this.objPr_ClasX_EventLog = ObjR_EventLog;
            this.stPr_tempPath = PathTemporalImagenes;
            try
            {
                objPr_xet = new Metodos();
                CargarLibrerias();
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "ClasX_Escaner_Device(3). System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "ClasX_Escaner_Device(3)", "", ex.Message, "", "");
            }
        }



        public ClasX_Escaner_Device(ClasX_EventLog ObjR_EventLog)
        {
            this.objPr_ClasX_EventLog = ObjR_EventLog;
            try
            {
                objPr_xet = new Metodos();
                CargarLibrerias();
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "ClasX_Escaner_Device(4). System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "ClasX_Escaner_Device(4)", "", ex.Message, "", "");
            }
        }


        public void set_Resolution(short in_Res)
        {
            inPr_Resolution = in_Res;
        }

        public void set_ColorScheme(short in_ColorScheme)
        {
            inPr_ColorScheme = in_ColorScheme;
        }


        public short get_Resolution()
        {
            return inPr_Resolution;
        }

        public short get_ColorScheme()
        {
            return inPr_ColorScheme;
        }

        [HandleProcessCorruptedStateExceptions]
        public bool iniciarEscaner()
        {
            try
            {
                objPr_SDKSLibEx.Duplex = (short)(true ? 1 : 0);
                if (!Directory.Exists(stPr_tempPath))
                {
                    Directory.CreateDirectory(stPr_tempPath);
                }
                // ----------------------------
                // Set Res info
                if (inPr_Resolution <= 0)
                {
                    inPr_Resolution = 300;
                }
                objPr_SDKSLib.Resolution = inPr_Resolution;
                // Set ColorScheme info
                if (inPr_ColorScheme == 0)
                {
                    inPr_ColorScheme = -256;
                }
                objPr_SDKSLib.ScannerColorScheme = inPr_ColorScheme;
                // ----------------------------
                objPr_SDKSLib.ScanToFile(stPr_tempPath+"Documento.jpg");
                objPr_SDKIDData.RefreshData();
                return true;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "iniciarEscaner", "ClasX_Escaner_Device. System.AccessViolationException", "", ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "iniciarEscaner", "ClasX_Escaner_Device", "", ex.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public bool GuardarImagen(String stR_DestinoImagenes)
        {
            _C_ProgRes.ClasX_FileManager objL_FileManager = new ClasX_FileManager(stPr_UsuarioApp, stPr_PathLog);
            try
            {
                // Dic 11 2014
                if (!Directory.Exists(stPr_tempPath))
                {
                    Directory.CreateDirectory(stPr_tempPath);
                }
                // Fin Dic 11 2014
                objL_FileManager.CopiarArchivos(stR_DestinoImagenes, stPr_tempPath, true);
                if (File.Exists(stR_DestinoImagenes + "\\Documento.jpg"))
                {
                    if (!File.Exists(stR_DestinoImagenes + "\\Documentos1.jpg"))
                    {
                        File.Move(stR_DestinoImagenes + "\\Documento.jpg", stR_DestinoImagenes + "\\Documentos1.jpg");
                        if (!File.Exists(stR_DestinoImagenes + "\\Documentos2.jpg"))
                        {
                            if (File.Exists(stR_DestinoImagenes + "\\Documento-back.jpg"))
                            {
                                File.Move(stR_DestinoImagenes + "\\Documento-back.jpg", stR_DestinoImagenes + "\\Documentos2.jpg");
                                Directory.Delete(stPr_tempPath, true);
                                return true;
                            }
                            else return false;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;

            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "GuardarImagen. System.AccessViolationException", "", ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "GuardarImagen", "", ex.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void CargarLibrerias()
        {
            int v = 1;
            try
            {
                bool blL_valorx = objPr_xet.IsInitialized(v);
                StPr_Licencia = objPr_xet.license_x;
                objPr_SDKSLib = new NetScanW.SLibExClass();
                objPr_SDKSLibEx = new NetScanWex.SLibExClass();
                objPr_SDKIDData = new NetScanW.IdDataClass();
                objPr_SDKIDDataEx = new NetScanWex.IdDataClass();
                objPr_SDKImage = new NetScanW.CImageClass();
                objPr_SDKImageEx = new NetScanWex.CImageClass();
                objPr_SDKOcr = new NetScanW.COcrClass();
                Process proc = Process.GetCurrentProcess();
                objPr_SDKSLibEx.DefaultScanner = 15;
                if (objPr_xet.IsInitialized(objPr_SDKSLibEx.InitLibraryEx(StPr_Licencia, proc.Id, proc.ProcessName)))
                {
                    if (objPr_xet.IsInitialized(objPr_SDKOcr.InitLibrary(StPr_Licencia)))
                    {
                        if (objPr_xet.IsInitialized(objPr_SDKIDData.InitLibrary(StPr_Licencia)))
                        {
                            objPr_SDKIDDataEx.WideCharacters = 1;
                            objPr_SDKIDDataEx.WideCharacters = 1;
                            DateTime now = DateTime.Now;
                            _ignoreFields = new StringCollection();
                            _ignoreFields.AddRange("DebugFlag,IssueDateAuthentication,DobDateAuthentication,LicenseAuthentication,StateAuthentication,IoCtl,DatesFormat,WideCharacters,Version,GeneralWaterMark,LastStateIndex".Split(','));
                            //string tempPath = @"C://escaneres//";
                            //_imagePaths = new string[4];
                            //_imagePaths[ImageTypeIndex.FRONT] = Path.Combine(tempPath, "Frente.jpg");
                            //_imagePaths[ImageTypeIndex.FACE] = Path.Combine(tempPath, "face.jpg");
                            //_imagePaths[ImageTypeIndex.SIGNATURE] = Path.Combine(tempPath, "sig.jpg");
                            //_imagePaths[ImageTypeIndex.BACK] = Path.Combine(tempPath, "Trasera.jpg");
                            objPr_SDKIDDataEx.RegionSetDetectionSequence(
                                SDKRegionId.EUROPE,
                                SDKRegionId.USA,
                                SDKRegionId.CANADA,
                                SDKRegionId.AUSTRALIA,
                                SDKRegionId.AMERICA,
                                SDKRegionId.ASIA,
                                SDKRegionId.SOUTH_AFRICA);
                            bool UICheckBoxDuplex_x = (objPr_SDKSLibEx.IsDuplexScanSupported > 0);
                            objPr_SDKSLib.ScanHeight = -1;
                            objPr_SDKSLib.ScanWidth = -1;
                            return;
                        }
                    }
                }
                SDKUnloadLibraries();
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "CargarLibrerias. System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "CargarLibrerias", "", ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void SDKUnloadLibraries()
        {
            try
            {
                if (objPr_SDKSLibEx != null)
                {
                    objPr_SDKSLibEx.UnInitEx(objPr_SDKSLibEx.OwnerId);
                }
                objPr_SDKIDData = null;
                objPr_SDKIDDataEx = null;
                objPr_SDKImage = null;
                objPr_SDKImageEx = null;
                objPr_SDKOcr = null;
                objPr_SDKSLib = null;
                objPr_SDKSLibEx = null;
                _ignoreFields = null;
                //_imagePaths = null;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "SDKUnloadLibraries. System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_Escaner_Device", "SDKUnloadLibraries", "", ex.Message, "", "");
            }
        }

    }


    #region constantes
    internal static class ImageTypeIndex
    {
        internal const int FRONT = 0;
        internal const int FACE = 1;
        internal const int SIGNATURE = 2;
        internal const int BACK = 3;
    }

    internal static class SDKImageType
    {
        internal const int FRONT = 0;
        internal const int FRONT_BARCODE = 1;
        internal const int BACK = 2;
        internal const int BACK_BARCODE = 3;
    }


    internal static class SDKDateFormat
    {
        internal const short DEFAULT = 0;
        internal const short MDY = 1;
        internal const short DMY = 2;
        internal const short YMD = 3;
        internal const short YDM = 4;
    }

    internal static class SDKRotationAngle
    {
        //image rotation angle definitions
        internal const int ANGLE_0 = 0;
        internal const int ANGLE_90 = 1;
        internal const int ANGLE_180 = 2;
        internal const int ANGLE_270 = 3;
    }

    internal static class SDKRegionId
    {
        internal const int NONE = -1;
        internal const int USA = 0;
        internal const int CANADA = 1;
        internal const int AMERICA = 2;
        internal const int EUROPE = 3;
        internal const int AUSTRALIA = 4;
        internal const int ASIA = 5;
        internal const int GENERAL_DOC = 6;
        internal const int SOUTH_AFRICA = 7;
    }

    internal static class SDKSaveType
    {
        //image saving target definition
        internal const int SAVE_TO_FILE = 0;
        internal const int SAVE_TO_CLIPBOARD = 1;
        internal const int SAVE_TO_IR = 2;
        internal const int SAVE_TO_UV = 3;
    }

    internal static class SDKScannerType
    {
        internal const short CSSN_NONE = 0;
        internal const short CSSN_600 = 1;
        internal const short CSSN_800 = 2;
        internal const short CSSN_800N = 3; // NR
        internal const short CSSN_1000 = 4;
        internal const short CSSN_2000 = 5;
        internal const short CSSN_2000N = 6;
        internal const short CSSN_800E = 7;
        internal const short CSSN_800EN = 8;
        internal const short CSSN_3000DX = 9;  // Duplex scanner
        internal const short CSSN_4000 = 10;  // Fujitsu fi-60F
        internal const short CSSN_TWAIN = 11;
        internal const short CSSN_5000 = 12;
        internal const short CSSN_IDR = 13;  // SnapShell - OCR only
        internal const short CSSN_800DX = 14;  // Small Duplex scanner
        internal const short CSSN_800DXN = 15;  // scanner without ocr
        internal const short CSSN_FDA = 16;  // SnapShell - OCR + Digimark watermark verification
        internal const short CSSN_WMD = 17;  // SnapShell - Digimark watermar verification only
        internal const short CSSN_TWN = 18;  // SnapShell - General twain camera
        internal const short CSSN_PASS = 19;  // SnapShell - Passport camera
        internal const short CSSN_RTE8K = 20;
        internal const short CSSN_TWAIN_N = 21;
        internal const short CSSN_MAGTEK_STX = 22;
        internal const short CSSN_CLBS = 23;
        internal const short CSSN_IP = 24;
        internal const short CSSN_1000N = 25;
        internal const short CSSN_3000DN = 26;
        internal const short CSSN_ICSCAN = 27;
        internal const short CSSN_RTE9K = 28;  // SnapShell - Passport camera (3M 9000)
    }
    internal static class SDKReturn
    {
        // License related return values
        internal const int LICENSE_VALID = 1;
        internal const int LICENSE_EXPIRED = -20;
        internal const int LICENSE_INVALID = -21;
        internal const int LICENSE_DOES_NOT_MATCH_LIBRARY = -22;

        // SLib general return values
        internal const int SLIB_ERR_SCANNER_NOT_FOUND = -4;
        internal const int SLIB_ERR_INVALID_SCANNER = -1;
        internal const int SLIB_LIBRARY_ALREADY_INITIALIZED = -13;
        internal const int SLIB_ERR_DRIVER_NOT_FOUND = -14;
        internal const int GENERAL_ERR_PLUG_NOT_FOUND = -200;

        // IdData return values
        internal const int ID_STATE_AUTODETECT = -1;
        internal const int ID_TRUE = 1;
        internal const int ID_FALSE = 0;
        internal const int ID_ERR_NONE = 1;
        internal const int ID_ERR_STATE_NOT_SUPORTED = -2;
        internal const int ID_ERR_BAD_PARAM = -3;
        internal const int ID_ERR_NO_MATCH = -4;
        internal const int ID_ERR_FILE_OPEN = -5;
        internal const int ID_BAD_DESTINATION_FILE = -6;
        internal const int ID_ERR_FEATURE_NOT_SUPPORTED = -7;
        internal const int ID_ERR_COUNTRY_NOT_INIT = -8;
        internal const int ID_ERR_NO_NEXT_COUNTRY = -9;
        internal const int ID_ERR_STATE_NOT_INIT = -10;
        internal const int ID_ERR_NO_NEXT_STATE = -11;
        internal const int ID_ERR_CANNOT_DELETE_DESTINATION_IMAGE = -12;
        internal const int ID_ERR_CANNOT_COPY_TO_DESTONATION = -13;
        internal const int ID_ERR_FACE_IMAGE_NOT_FOUND = -14;
        internal const int ID_ERR_STATE_NOT_RECOGNIZED = -15;
        internal const int ID_ERR_USA_TEMPLATES_NOT_FOUND = -16;
        internal const int ID_ERR_WRONG_TEMPLATE_FILE = -17;

        // CImage return values
        internal const int IMG_ERR_SUCCESS = 0;
        internal const int IMG_ERR_FILE_OPEN = -100;
        internal const int IMG_ERR_BAD_ANGLE_0 = -101;
        internal const int IMG_ERR_BAD_ANGLE_1 = -102;
        internal const int IMG_ERR_BAD_DESTINATION = -103;
        internal const int IMG_ERR_FILE_SAVE_TO_FILE = -104;
        internal const int IMG_ERR_FILE_SAVE_TO_CLIPBOARD = -105;
        internal const int IMG_ERR_FILE_OPEN_FIRST = -106;
        internal const int IMG_ERR_FILE_OPEN_SECOND = -107;
        internal const int IMG_ERR_COMB_TYPE = -108;

        internal const int IMG_ERR_BAD_COLOR = -130;
        internal const int IMG_ERR_BAD_DPI = -131;
        internal const int INVALID_INTERNAL_IMAGE = -132;

        internal static string ErrorDescriptionForInit(int returnValue)
        {
            switch (returnValue)
            {
                case LICENSE_VALID:
                case SLIB_LIBRARY_ALREADY_INITIALIZED:
                    return "Librerias cargadas exitosamente";
                case LICENSE_EXPIRED:
                    return "ERROR: Su licencia a espirado, todol los dispositivos estan Abajo!";
                case LICENSE_INVALID:
                    return "ERROR: libreria no a sido inicializada";
                case LICENSE_DOES_NOT_MATCH_LIBRARY:
                    return "Error: License Invalid for ID Library (IdData)!";
                case SLIB_ERR_SCANNER_NOT_FOUND:
                case SLIB_ERR_INVALID_SCANNER:
                    return "ERROR: No attached scanner was found!";
                case GENERAL_ERR_PLUG_NOT_FOUND:
                    return "ERROR: Plug (scanner) not found!";
                case SLIB_ERR_DRIVER_NOT_FOUND:
                    return "Error: Driver de scanner no encontrados";
                default:
                    return "ERROR: Failed initializing SDK libraries!";
            }
        }


        internal static string ErrorDescriptionForIDDetection(int stateId)
        {
            switch (stateId)
            {
                case LICENSE_INVALID:
                    return "ERROR: The license is invalid. All scanner operations are disabled!";
                case ID_ERR_USA_TEMPLATES_NOT_FOUND:
                    return "ERROR: Debe insertar un documento para proceso Scanner";
                case ID_ERR_STATE_NOT_RECOGNIZED:
                case ID_ERR_STATE_NOT_SUPORTED:
                    return "ERROR: The license image does not match any state template!";
                case INVALID_INTERNAL_IMAGE:
                    return "ERROR: No internal image is loaded. Please scan an image first!";
                case GENERAL_ERR_PLUG_NOT_FOUND:
                    return "ERROR: Plug (scanner) not found!";
                default:
                    return string.Format("ERROR: Failed detecting card (state Id:{0})", stateId);
            }
        }


        internal static string ErrorDescriptionForIDProcess(int returnValue)
        {
            switch (returnValue)
            {
                case ID_TRUE:
                    return "DOCUMENTO DETECTADO SATISFACTORIAMENTE";
                case LICENSE_EXPIRED:
                    return "ERROR: The license has expired. All scanner operations are disabled!";
                case LICENSE_INVALID:
                    return "ERROR: Library was not initialized with proper license!";
                case SLIB_ERR_SCANNER_NOT_FOUND:
                case SLIB_ERR_INVALID_SCANNER:
                    return "ERROR: No attached scanner was found!";
                case ID_ERR_STATE_NOT_SUPORTED:
                    return "ERROR: The requested state id is not supported!";
                case INVALID_INTERNAL_IMAGE:
                    return "ERROR: No internal image is loaded. Please scan an image first!";
                case GENERAL_ERR_PLUG_NOT_FOUND:
                    return "ERROR: Plug (scanner) not found!";
                default:
                    return "ERROR: Failed processing card!";
            }
        }
    }
    #endregion


    class Metodos
    {
        public string license_x = "WK19DYWA6WTK78BU";

        public bool IsInitialized(int returnValue)
        {
            return (returnValue == SDKReturn.LICENSE_VALID || returnValue == SDKReturn.SLIB_LIBRARY_ALREADY_INITIALIZED);
        }
    }
}
