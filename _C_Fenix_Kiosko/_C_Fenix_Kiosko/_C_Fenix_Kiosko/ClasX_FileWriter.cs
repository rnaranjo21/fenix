using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.IO;
using _C_ProgRes;


namespace _C_Fenix_Kiosko
{
    public class ClasX_FileWriter
    {
        private ClasX_EventLog ObjPr_EventLog;
        private ClasX_FileManager ObjPr_FileManager;

        public ClasX_FileWriter(ClasX_EventLog ObjPr_EventLog)
        {
            this.ObjPr_EventLog = ObjPr_EventLog;
            ObjPr_FileManager = new ClasX_FileManager(this.ObjPr_EventLog.getUser(), this.ObjPr_EventLog.getPathArchivoLogErr());
        }
        public void EscribeArchivo(String nomArchivo)
        {
            String stL_LFecha = DateTime.Now.ToString();
            StreamWriter StLEscritor = new StreamWriter(nomArchivo, true, Encoding.ASCII);
            StLEscritor.WriteLine("Fecha Consulta: " + stL_LFecha);
            StLEscritor.Close();
        }
        public void gregaLinea(String nomArchivo, String linea)
        {
            StreamWriter StLEscritor = new StreamWriter(nomArchivo, true, Encoding.ASCII);
            StLEscritor.WriteLine(linea);
            StLEscritor.Close();
        }
    }
}
