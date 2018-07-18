using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ExceptionServices;


namespace _C_ProgRes
{
    public class ClasX_caDatosTemplate
    {
        private ClasX_EventLog objPr_LogEvent = new ClasX_EventLog();
        private ClasX_caTemplate[] template = new ClasX_caTemplate[0];
        /// <summary>
        /// por defecto se describe como HUELLA.
        /// Los posibles valores son HUELLA, ROSTRO, IRIS
        /// </summary>
        private String stPr_tipotemplate = "HUELLA";//por defecto se describe como HUELLA. Los posibles valores son HUELLA, ROSTRO, IRIS
        private int inPr_veltransmision = 1024;//tamño del template
        private String[] stPr_cedulas = new String[0];
        private String stPr_userApp = "";
        private String stPr_pathLog = "ca_DatosTemplate.log";
        /// <summary>
        /// Tipo de peticion.
        /// 1-IDENTIFICACION, 2-VERIFICACION
        /// </summary>
        private int inPr_tipopeticion = 1;//1-IDENTIFICACION, 2-VERIFICACION, 3-INSERCION
        /// <summary>
        /// Constructor vac{io de la clase
        /// </summary>
        public ClasX_caDatosTemplate()
        {
            //constructor vacío de la clase
        }
        /// <summary>
        /// Sobrecarga del constructor.
        /// Asigna el usuario actual de la aplicacion y la ruta de almacenamiento del log
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="PathLog"></param>
        public ClasX_caDatosTemplate(String usuario, String PathLog)
        {
            stPr_userApp = usuario;
            stPr_pathLog = PathLog;
            objPr_LogEvent.setUser(stPr_userApp);
            objPr_LogEvent.setPathArchivoLogErr(stPr_pathLog);
        }
        /// <summary>
        /// Método para asignar un arreglo de templates al arreglo de la clase
        /// </summary>
        /// <param name="dato"></param>
        public void setTemplate(ClasX_caTemplate[] dato)
        {
            template = dato;
        }
        /// <summary>
        /// Método para obtener el arreglo de templates de la clase
        /// </summary>
        /// <returns></returns>
        public ClasX_caTemplate[] getTemplate()
        {
            return template;
        }
        /// <summary>
        /// Método para obtener un template especifico del arreglo de la clase
        /// </summary>
        /// <param name="cual"></param>
        /// <returns></returns>
        public ClasX_caTemplate getTemplate(int cual)
        {
            return template[cual];
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Método para asignar un único template al arreglo de la clase
        /// </summary>
        /// <param name="dato"></param>
        public void agregaTemplate(ClasX_caTemplate dato)
        {
            try
            {
                ClasX_caTemplate[] stmps = new ClasX_caTemplate[template.Length];
                System.Array.Copy(template, 0, stmps, 0, template.Length);
                template = new ClasX_caTemplate[template.Length + 1];
                System.Array.Copy(stmps, 0, template, 0, stmps.Length);
                template[template.Length - 1] = dato;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "System.AccessViolationException: " + ex_0, ""
                    , "");
            }
            catch (ArgumentNullException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "ArgumentNullException: " + e, ""
                    , "");
            }
            catch (RankException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "RankException: " + e, ""
                    , "");
            }
            catch (ArrayTypeMismatchException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "ArrayTypeMismatchException: " + e, ""
                    , "");
            }
            catch (InvalidCastException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "InvalidCastException: " + e, ""
                    , "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "ArgumentOutOfRangeException: " + e, ""
                    , "");
            }
            catch (Exception ex_1)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "Exception: " + ex_1, ""
                    , "");
            }
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Sobrecarga del metodo para insertar un único Template,
        /// adicionalmente asigna el valor del identificador del template
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="datosbyte"></param>
        public void agregaTemplate(int identificador, byte[] datosbyte)
        {
            try
            {
                ClasX_caTemplate obj_templatetmp = new ClasX_caTemplate();
                obj_templatetmp.setIdentificador(identificador);
                obj_templatetmp.setTemplate(datosbyte);
                agregaTemplate(obj_templatetmp);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate(2)", "", "System.AccessViolationException: " + ex_0, ""
                    , "");
            }
            catch (Exception ex_1)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate(2)", "", "Exception: " + ex_1, ""
                    , "");
            }
        }


        /// <summary>
        /// Asigna el valor del tipo de template
        /// HUELLA, ROSTRO, IRIRS
        /// </summary>
        /// <param name="stR_tipoTemplate"></param>
        public void setTipo(String stR_tipoTemplate)
        {
            stPr_tipotemplate = stR_tipoTemplate;
        }
        /// <summary>
        /// Devuelve el tipo de Template
        /// </summary>
        /// <returns></returns>
        public String getTipo()
        {
            return stPr_tipotemplate;
        }
        /// <summary>
        /// Devuelve un entero con el tamaño de la coleccion de Templates,
        /// equivalente a el número de templates cargados
        /// </summary>
        /// <returns></returns>
        public int getNumTemplates()
        {
            return template.Length;
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Devuelve el tamaño de un Template específico dentro de la colección
        /// </summary>
        /// <param name="cualtemplate"></param>
        /// <returns></returns>
        public long getTamano(int cualtemplate)
        {
            try
            {
                long tamano = template.Length;
                //Strail 15dic2011 return template[cualtemplate].getTemplate().length;
                return template[cualtemplate].tamano;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "getTamano", "", "System.AccessViolationException: " + ex_0, ""
                    , "");
                return 0;
            }
            catch (Exception ex_1)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "getTamano", "", "Exception: " + ex_1, ""
                    , "");
                return 0;
            }
        }


        /// <summary>
        /// Asigna el valor de la variable para establecer la velocidad de transmision
        /// </summary>
        /// <param name="inR_velTransmit"></param>
        public void setVelTrasnmit(int inR_velTransmit)
        {
            inPr_veltransmision = inR_velTransmit;
        }
        /// <summary>
        /// Obtiene el valor actual de la velocidad de transmision en Bytes
        /// </summary>
        /// <returns></returns>
        public int getVelTrasnmit()
        {
            return inPr_veltransmision;
        }

        public void setCedulas(String[] stR_cedulas)
        {
            stPr_cedulas = stR_cedulas;
        }

        public String[] getCedulas()
        {
            return stPr_cedulas;
        }


        [HandleProcessCorruptedStateExceptions]
        public void agregeCedula(String stR_cedula)
        {
            try
            {
                String[] stmps = new String[stPr_cedulas.Length];
                System.Array.Copy(stPr_cedulas, 0, stmps, 0, stPr_cedulas.Length);
                stPr_cedulas = new String[stPr_cedulas.Length+1];
                System.Array.Copy(stmps, 0, stPr_cedulas, 0, stmps.Length);
                stPr_cedulas[stPr_cedulas.Length - 1] = stR_cedula;
                String prueba = stPr_cedulas[stPr_cedulas.Length - 1];
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "System.AccessViolationException: " + ex_0, ""
                    , "");
            }
            catch (ArgumentNullException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregeCedula", "", "ArgumentNullException: " + e, ""
                    , "");
            }
            catch (RankException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregeCedula", "", "RankException: " + e, ""
                    , "");
            }
            catch (ArrayTypeMismatchException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregeCedula", "", "ArrayTypeMismatchException: " + e, ""
                    , "");
            }
            catch (InvalidCastException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "InvalidCastException: " + e, ""
                    , "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "ArgumentOutOfRangeException: " + e, ""
                    , "");
            }
            catch (Exception ex_1)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "agregaTemplate", "", "Exception: " + ex_1, ""
                    , "");
            }
        }


        public void setTipoPeticion(int inR_tipoPeticion)
        {
            inPr_tipopeticion = inR_tipoPeticion;
        }

        public int getTipoPeticion()
        {
            return inPr_tipopeticion;
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Convierte una cadena protocolo en datos para un objeto de tipo caDatosTemplate
        /// en formatos:
        /// TIPO BIOMETRIA|CANT TEMPLATES|TEM1|TEM2..n|tamaTransmit(1024)|CANT CEDUL|CED1|CED2..n
        /// </summary>
        /// <param name="scadena"></param>
        public void ConvertCadena(String scadena)
        {
            try
            {
                int inL_tamano = 0;
                //int inL_posdato = -1;
                Char[] delimitadores = { '|' };
                String[] stL_datosmensage = scadena.Split(delimitadores);

                if (stL_datosmensage.Length > 0)
                {
                    stPr_tipotemplate = stL_datosmensage[0];
                    int tamaño;
                    Int32.TryParse(stL_datosmensage[1], out tamaño);
                    inL_tamano = tamaño;
                    template = new ClasX_caTemplate[inL_tamano];
                    int posgen = 0;
                    for (int i = 0; i < template.Length; i++)
                    {
                        posgen = i + 2;
                        template[i] = new ClasX_caTemplate();
                        //template[i].tamano = Long.parseLong(sdatosmensage[posgen]);
                        long tempTamaño;
                        long.TryParse(stL_datosmensage[posgen], out tempTamaño);
                        template[i].tamano = tempTamaño;
                    }
                    posgen++;
                    int veltransmision;
                    Int32.TryParse(stL_datosmensage[posgen], out veltransmision);
                    inPr_veltransmision = veltransmision;
                    posgen++;
                    int datosmensaje;
                    Int32.TryParse(stL_datosmensage[posgen], out datosmensaje);
                    stPr_cedulas = new String[datosmensaje];
                    for (int c = 0; c < stPr_cedulas.Length; c++)
                    {
                        posgen++;
                        stPr_cedulas[c] = stL_datosmensage[posgen];
                    }

                }
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "ConvertCadena", "", "System.AccessViolationException: " + ex_0, ""
                    , "");
            }
            catch (Exception e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "ConvertCadena", "", "Exception: " + e, ""
                    , "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String aCadena()
        {
            try
            {
                int inL_cantidad = template.Length;
                String stL_tamanos = "";
                for (int i = 0; i < template.Length; i++)
                {
                    stL_tamanos += template[i].getTemplate().Length + "|";
                }

                int inL_cantidadCed = stPr_cedulas.Length;
                String stL_cedulas = "";
                for (int c = 0; c < stPr_cedulas.Length; c++)
                {
                    stL_cedulas += "|" + stPr_cedulas[c];
                }

                String trama = stPr_tipotemplate +
                    "|" + Convert.ToString(inL_cantidad) +
                    "|" + stL_tamanos + Convert.ToString(inPr_veltransmision) +
                    "|" + Convert.ToString(inL_cantidadCed) +
                    "" + stL_cedulas;

                return stPr_tipotemplate +
                    "|" + Convert.ToString(inL_cantidad) +
                    "|" + stL_tamanos + Convert.ToString(inPr_veltransmision) +
                    "|" + Convert.ToString(inL_cantidadCed) +
                    "" + stL_cedulas;
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "aCadena", "", "System.AccessViolationException: " + ex_0, ""
                    , "");
                return null;
            }
            catch (Exception e)
            {
                objPr_LogEvent.setSalLog(true);
                objPr_LogEvent.outMensajError("_C_ProgRes.DLL",
                    "ClasX_caDatosTemplate", "aCadena", "", "Exception: " + e, ""
                    , "");
                return null;
            }
        }



    }
}
