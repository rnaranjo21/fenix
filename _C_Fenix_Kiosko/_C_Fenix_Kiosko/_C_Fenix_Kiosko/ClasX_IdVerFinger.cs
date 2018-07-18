using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Resources;
using _C_ProgRes;
//

namespace _C_Fenix_Kiosko
{
    /// <summary>
    /// Clase para la identificacion y verificacion de templates desde el form
    /// </summary>
    public class ClasX_IdVerFinger
    {
        //
        #region campos privados
        /// <summary>
        /// Usuario actual de la aplicacion
        /// </summary>
        private String UserApp = "FENIX";
        /// <summary>
        /// Ruta de almacenamiento del log de la aplicacion
        /// </summary>
        private String PathLog = "logterminaenrol.log";
        private String stPr_componente = "";
        /// <summary>
        /// Arreglo de bytes con el template
        /// </summary>
        private byte[] btPr_template;
        /// <summary>
        /// Objeto para manejo de log
        /// </summary>
        private ClasX_EventLog objPr_EventLog = null;
        /// <summary>
        /// Objeto con la informacion de conexion del cliente TCP
        /// </summary>
        private ClasX_ClienteTCP objPr_ClienteTCP = null;
        #endregion

        #region tipos de biometria
        public const String HUELLA = "HUELLA";
        public const String ROSTRO = "ROSTRO";
        public const String IRIS = "IRIS";
        #endregion

        /// <summary>
        /// constructor de la clase
        /// </summary>
        /// <param name="objR_EventLog"></param>
        public ClasX_IdVerFinger(ClasX_EventLog objR_EventLog, String componente)
        {
            this.objPr_EventLog = objR_EventLog;
            this.stPr_componente = componente;
            UserApp = objPr_EventLog.getUser();
            PathLog = objPr_EventLog.getPathArchivoLogErr();
        }

        #region metodos
        /// <summary>
        /// Prepara y envia el template al servidor para la identificacion.
        /// Devuelve como resultado un arreglo de String con los resultados de la identificacion
        /// </summary>
        /// <param name="ObjR_ClienteTCP">Objeto con la informacion de conexion del cliente TCP</param>
        /// <param name="template">Template como arreglo de bytes</param>
        /// <param name="TipoTemplate">tipo de template que se esta enviando- valores: HUELLAS, ROSTRO, IRIS</param>
        /// <returns></returns>
        public String[] SendTemplate(ClasX_ClienteTCP ObjR_ClienteTCP, byte[] template, String TipoTemplate)
        {
            this.btPr_template = template;
            this.objPr_ClienteTCP = ObjR_ClienteTCP;
            ClasX_caDatosTemplate ObjL_caDatosTemplate = new ClasX_caDatosTemplate();
            try
            {
                ObjL_caDatosTemplate.agregaTemplate(-1, btPr_template);

                if (TipoTemplate.Equals("HUELLA")) ObjL_caDatosTemplate.setTipo("HUELLA");
                if (TipoTemplate.Equals("ROSTRO")) ObjL_caDatosTemplate.setTipo("ROSTRO");
                if (TipoTemplate.Equals("IRIS")) ObjL_caDatosTemplate.setTipo("IRIS");

                String respuesta = objPr_ClienteTCP.FENIXIdentificar(ObjL_caDatosTemplate);
                char[] delimiterChars = { ' ', '|', '\r' };
                string[] resultados = respuesta.Split(delimiterChars);
                return resultados;
            }
            catch (Exception ex)
            {
                objPr_EventLog.outMensajError(stPr_componente, "ClasX_IdVerFinger", "SendTemplate.identificar", "", "Excepcion: " + ex,
                    "", "");
                return null;
            }
        }

        /// <summary>
        /// Sobrecarga del metodo, que recibe como parametro adicional el numero de identificacion de la cedula a buscar para verificacion
        /// </summary>
        /// <param name="ObjR_ClienteTCP">Objeto con la informacion de conexion del cliente TCP</param>
        /// <param name="template">Template como arreglo de bytes</param>
        /// <param name="Identificacion">Numero de identificacion del template a buscar</param>
        /// <param name="TipoTemplate">Tipo de template: "HUELLA", "ROSTRO" O "IRIS"</param>
        /// <returns></returns>
        public String[] SendTemplate(ClasX_ClienteTCP ObjR_ClienteTCP, byte[] template, String Identificacion, String TipoTemplate)
        {
            this.btPr_template = template;
            this.objPr_ClienteTCP = ObjR_ClienteTCP;
            ClasX_caDatosTemplate ObjL_caDatosTemplate = new ClasX_caDatosTemplate();
            try
            {
                ObjL_caDatosTemplate.agregaTemplate(-1, btPr_template);
                ObjL_caDatosTemplate.agregeCedula(Identificacion);

                if (TipoTemplate.Equals("HUELLA")) ObjL_caDatosTemplate.setTipo("HUELLA");
                if (TipoTemplate.Equals("ROSTRO")) ObjL_caDatosTemplate.setTipo("ROSTRO");
                if (TipoTemplate.Equals("IRIS")) ObjL_caDatosTemplate.setTipo("IRIS");

                String respuesta = objPr_ClienteTCP.FENIXVerificar(ObjL_caDatosTemplate);
                char[] delimiterChars = { ' ', '|', '\r' };
                string[] resultados = respuesta.Split(delimiterChars);
                return resultados;
            }
            catch (Exception ex)
            {
                objPr_EventLog.outMensajError("FENIX_301.EXE", "ClasX_Identificar", "SendTemplate.verificar", "", "Excepcion: " + ex,
                    "", "");
                return null;
            }
        }
        #endregion
        //
    }
}
