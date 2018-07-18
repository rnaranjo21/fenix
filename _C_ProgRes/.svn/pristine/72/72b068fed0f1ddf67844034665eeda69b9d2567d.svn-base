using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ExceptionServices;
//

namespace _C_ProgRes
{
    public class ClassX_DatosArchivo
    {
        private String stPr_nombrearchivoO = "";
        private String stPr_nombrearchivoD = "";
        private String stPr_pathorigen = "";
        private long lnPr_tamnnoarchivo = 0;
        private String stPr_pathdestino = "";
        private int inPr_tamtransmit = 1024;
        private String stPr_origen = "NULL";
        private String stPr_destino = "NULL";
        private String stPr_UsuarioApp = "_C_ProgRes";
        private String stPr_PathLog = "conCliente.log";
        ClasX_EventLog obj_Log = new ClasX_EventLog();
        ClasX_Utils obj_utls = new ClasX_Utils();
        /// <summary>
        /// Constructor de la clase. Sin parámetros
        /// </summary>
        public ClassX_DatosArchivo()
        {
            //constructor vacio
        }


        [HandleProcessCorruptedStateExceptions]
        public void ConvCad(String scadena)
        {
            stPr_UsuarioApp = obj_utls.getUserApp();
            stPr_PathLog = obj_utls.getArchivoLog();
            try
            {
                String[] sdatosmensage = new String[4];
                char[] delimitador = { '|' };
                sdatosmensage = scadena.Split(delimitador);// nombre origen|destino|tamaño|tamaTransmit (1024)

                if (!sdatosmensage[0].Equals("NULL"))
                {
                    stPr_origen = sdatosmensage[0];
                }
                if (!sdatosmensage[1].Equals("NULL"))
                {
                    stPr_destino = sdatosmensage[1];
                }
                    
                long lnL_datosmensaje2;
                long.TryParse(sdatosmensage[2], out lnL_datosmensaje2);
                if (lnL_datosmensaje2 > 0)
                {
                    long tamnnoarchivo;
                    long.TryParse(sdatosmensage[2], out tamnnoarchivo);
                    lnPr_tamnnoarchivo = tamnnoarchivo;
                }
                    
               
                int inL_datosmensage3;
                Int32.TryParse(sdatosmensage[3], out inL_datosmensage3);

                if (inL_datosmensage3 > 0)
                {
                    int tamtransmit;
                    Int32.TryParse(sdatosmensage[3], out tamtransmit);
                    inPr_tamtransmit = tamtransmit;
                }
                     

            }
            //
            catch (System.AccessViolationException ex_0)
            {
                obj_Log.setSalConsole(false);
                obj_Log.setSalDialog(false);
                obj_Log.setSalLog(true);
                obj_Log.outMensajError("_C_ProgRes.DLL", "ClassX_DatosArchivo",
                    "ConvCad. System.AccessViolationException", "codigo del error",
                    "Excepcion: " + ex_0, "", "");
            }
            catch (Exception e)
            {
                obj_Log.setSalConsole(false);
                obj_Log.setSalDialog(false);
                obj_Log.setSalLog(true);
                obj_Log.outMensajError("_C_ProgRes.DLL", "ClassX_DatosArchivo",
                    "ConvCad", "codigo del error",
                    "Excepcion: " + e, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String aCadena()
        {
            try
            {
                return stPr_origen +
                    "|" + stPr_destino +
                    "|" + Convert.ToString(lnPr_tamnnoarchivo) +
                    "|" + Convert.ToString(inPr_tamtransmit);
            }
            catch (System.AccessViolationException ex_0)
            {
                obj_Log.setSalConsole(false);
                obj_Log.setSalDialog(false);
                obj_Log.setSalLog(true);
                obj_Log.outMensajError("_C_ProgRes.DLL", "ClassX_DatosArchivo",
                    "aCadena", "codigo del error",
                    "Excepcion: " + ex_0, "", "");
            }
            catch (Exception e)
            {
                obj_Log.setSalConsole(false);
                obj_Log.setSalDialog(false);
                obj_Log.setSalLog(true);
                obj_Log.outMensajError("_C_ProgRes.DLL", "ClassX_DatosArchivo",
                    "aCadena. System.AccessViolationException", "codigo del error",
                    "Excepcion: " + e, "", "");
            }
            return "";
        }

        public String retNombreArch(String sdato)
        {
            return sdato.Substring(sdato.LastIndexOf("/") + 1);
        }

        public String retPath(String sdato)
        {
            return sdato.Substring(0, sdato.LastIndexOf("/") + 1);
        }

        public String getNomArchOrigen()
        {
            return stPr_nombrearchivoO;
        }


        public String getNomArchDestino()
        {
            return stPr_nombrearchivoD;
        }


        public String getPathDestino()
        {
            return stPr_pathdestino;
        }


        public String getPathOrigen()
        {
            return stPr_pathorigen;
        }


        public long getTamanoArchivo()
        {
            return lnPr_tamnnoarchivo;
        }


        public void setTamanoArchivo(long ndato)
        {
            lnPr_tamnnoarchivo = ndato;
        }


        public int getTamanoTransmit()
        {
            return inPr_tamtransmit;
        }


        public void setTamanoTransmit(int ndato)
        {
            inPr_tamtransmit = ndato;
        }


        public String getOrigen()
        {
            return stPr_origen;
        }


        public String getDestino()
        {
            return stPr_destino;
        }

        public void setOrigen(String sdato)
        {
            stPr_origen = sdato;
            stPr_nombrearchivoO = retNombreArch(sdato);
            stPr_pathorigen = retPath(sdato);
        }


        public void setDestino(String sdato)
        {
            stPr_destino = sdato;
            stPr_nombrearchivoD = retNombreArch(sdato);
            stPr_pathdestino = retPath(sdato);

        }
    }
}
