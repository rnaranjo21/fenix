using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using Topaz;
using _C_ProgRes;
using System.Runtime.ExceptionServices;
using Neurotec.Devices;
using System.Windows.Forms;

namespace _C_Devices_N_4_3
{
    public class ClasX_FirmaDigital_Device : SigPlusNET
    {
        private ClasX_EventLog objPr_ClasX_EventLog;
        private int Ancho = 600;
        private int Alto = 200;
        private NDeviceManager objPr_deviceMan;
        private System.Windows.Forms.ListBox scannersListBox;
        private Boolean blPr_ExistePad = false;


        public ClasX_FirmaDigital_Device(ClasX_EventLog Obj_ClasX_EventLog)
        {
            objPr_ClasX_EventLog = Obj_ClasX_EventLog;
            try
            {
                BackColor = System.Drawing.Color.White;
                ForeColor = System.Drawing.Color.Black;
                Name = "sigPlusNET1";
                Size = new System.Drawing.Size(600,200);
                TabIndex = 11;
                Text = "sigPlusNET1";
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "ClasX_FirmaDigital_Device. System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "ClasX_FirmaDigital_Device", "", ex.Message, "", "");
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public void setTamano(int Ancho, int Alto)
        {
            try
            {
                this.Ancho = Ancho;
                this.Alto = Alto;
                Size = new System.Drawing.Size(Ancho, Alto);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "setTamano. System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "setTamano", "", ex.Message, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void iniciarFirma()
        {
            try
            {
                SetTabletState(1);                
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "iniciarFirma. System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "iniciarFirma", "", ex.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void detenerFirma()
        {
            try
            {
                SetTabletState(0);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "detenerFirma. System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "detenerFirma", "", ex.Message, "", "");
            }
        }




        [HandleProcessCorruptedStateExceptions]
        public void limpiarFirma()
        {
            try
            {
                ClearTablet();
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "limpiarFirma. System.AccessViolationException", "", ex_0.Message, "", "");
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "limpiarFirma", "", ex.Message, "", "");
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public bool GuardarImagen(String rutaArchivo, String nombreArchivo)
        {
            if (!Directory.Exists(rutaArchivo))
            {
                try
                {
                    Directory.CreateDirectory(rutaArchivo);
                }
                catch (System.AccessViolationException ex_0)
                {
                    objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "GuardarImagen. System.AccessViolationException", "", ex_0.Message, "", "");
                }
                catch (Exception ex)
                {
                    objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "GuardarImagen", "", ex.Message, "", "");
                }
            }
            //
            int stado = GetTabletState();
            if (GetTabletState() == 1)
            {
                SetTabletState(0);
            }
            long receipt = GetSigReceipt();
            if (receipt > 0)
            {
                SetImageXSize(Ancho);
                SetImageYSize(Alto);
                SetJustifyMode(5);
                Image ObjL_Imagen = GetSigImage();
                ObjL_Imagen.Save(rutaArchivo+nombreArchivo, System.Drawing.Imaging.ImageFormat.Bmp);
                SetJustifyMode(0);
                return true;
            }
            else
            {
                //throw new Exception("El contenedor se encuentra vacío");
                return true;
            }
        }





        //AGR 11-ABRIEL-2017 Se incluye validación de conexión de PAD DE FIRMA
        public bool PadConectado()
        {
            try
            {
                if (TabletConnectQuery())
                {
                    SetTabletState(1);
                    return true;
                }
                else
                {
                    objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "PadConectado()", "", "No se detecta PAD de Firma", "", "");
                    return false;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "PadConectado(). System.AccessViolationException", "", ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                objPr_ClasX_EventLog.outMensajError("_C_Devices_N_4_3.dll", "ClasX_FirmaDigital_Device", "PadConectado()", "", ex.Message, "", "");
                return false;
            }

            
        }







        ////AGR 10-ABRIL-2017 Validar existencia de pad de firma
        //[HandleProcessCorruptedStateExceptions]
        ///// <summary>
        ///// Actualiza la lista de dispositivos y selecciona uno disponible
        ///// </summary>
        //public void UpdateScannerList()
        //{
        //    try
        //    {
        //        blPr_ExistePad = false;
        //        objPr_deviceMan = new NDeviceManager();
        //        scannersListBox = new ListBox();
        //        int cont = 0;
        //        foreach (NDevice device in objPr_deviceMan.Devices)
        //        {
        //            scannersListBox.Items.Add(device);
        //            if (device.DisplayName.Equals("DigitalPersona, Inc. U.are.U® 4500 Fingerprint Reader") || device.DisplayName.Equals("Upek TCS1C #1") || device.DisplayName.Equals("Cross Match Fast Verifier 300LC2 Series") || device.DisplayName.Equals("Futronic FS88 #1"))
        //            {
        //                blPr_ExistePad = true;
        //                scannersListBox.SelectedIndex = cont;
        //                break;
        //            }

        //            cont++;
        //        }
        //    }
        //    catch (System.AccessViolationException ex_0)
        //    {

        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
        //}


        //public Boolean getExistePad()
        //{
        //    return blPr_ExistePad;
        //}



    }
}
