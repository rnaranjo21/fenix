using _C_ProgRes;
//using _Kiosko_WS.MCDIntegrationServices1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;


namespace _Kiosko_WS
{
    public class ClasX_Web_Services_Reportes
    {


        /* public int consulta_cuenta(String st_CedulaAfiliado)
         {
             int Cta_Numero=0;
           
             //try
            // {
             /*
                   MCDIntegrationServices.wsModeloCanonicoDatosSoapClient client = new wsModeloCanonicoDatosSoapClient();
                  var customerAcount = client.CustomerAccount(st_CedulaAfiliado);
                  customerAcount.Columns.Add("CTA_ID", typeof(string));
                 //customerAcount.Select("CTA_NUMERO");
                 Cta_Numero = Convert.ToInt32(customerAcount);
                 //_C_ProgRes.ClasX_EventLog objL_Log = new _C_ProgRes.ClasX_EventLog(customerAcount, stPr_ArchivoLog, false, true, false);
                

                 client.Close();
             //}
             //catch (Exception ex)
            // {

                // ClasX_EventLog objL_Log = new ClasX_EventLog(Cta_Numero, stPr_ArchivoLog, false, true, false);
                 //
                //objL_Log.outMensajError("_Kiosko_WS.Dll", "ClasX_Web_Services_Reportes", "consulta_cuenta", ex.ToString(), ex.Message.ToString(), "", ""); 
             //}
                 return Cta_Numero;
            
         }*/
        /* public byte[] Genera_reporte(String st_CedulaAfiliado, int cuenta_afiliado)
         {
             /*
             MCDIntegrationServices1.wsModeloCanonicoDatosSoapClient client = new wsModeloCanonicoDatosSoapClient();

             var a = client.GenerarReporte("DECLARACION_RENTA", st_CedulaAfiliado, cuenta_afiliado);

             client.Close();

            return a.Report;
           
         }*/

    }
}
