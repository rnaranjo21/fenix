﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _C_ProgRes;

namespace _Kiosko_WS
{
    public class ClasX_Web_Service
    {
        // Manejo de Servicios Web, para el Kiosko
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        private String stPr_ArchivoConf = ""; // Nombre el Archivo de Configuracion.
        //

        /// <summary>
        /// Constructor de la clase sin parametros
        /// </summary>
        public ClasX_Web_Service()
        {

        }

        /// <summary>
        /// Constructor de la clase 
        /// con parametros de usuario y path, nombre de archivo de log, Nombre archivo de configuraciones
        /// </summary>
        /// <param name="st_UsuarioApp">Codigo de usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Path y nombre del archivo log</param>
        /// <param name="st_ArchivoConf">Path y nombre del archivo de onfiguracion</param>
        public ClasX_Web_Service(String st_UsuarioApp, String st_ArchivoLog, String st_ArchivoConf)
        {
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            stPr_ArchivoConf = st_ArchivoConf;
        }


        /// <summary>
        /// Propiedad : getUsuarioApp
        /// Devuelve el codigo del usuario de la aplicacion.
        /// </summary>
        /// <returns>stPr_UsuarioAPP</returns>
        public String getUsuarioApp()
        {
            return stPr_UsuarioAPP;
        }


        /// <summary>
        /// Propiedad : getArchivoLog
        /// Devuelve el nombre del archivo de log de la aplicacion.
        /// </summary>
        /// <returns>stPr_ArchivoLog</returns>
        public String getArchivoLog()
        {
            return stPr_ArchivoLog;
        }


        /// <summary>
        /// Propiedad : getArchivoConf
        /// Devuelve el nombre del archivo de configuracion de la aplicacion.
        /// </summary>
        /// <returns>stPr_ArchivoConf</returns>
        public String getArchivoConf()
        {
            return stPr_ArchivoConf;
        }


        /// <summary>
        /// Metodo : set_UsuarioApp
        /// Cambia el codigo del usuario de la aplicacion.
        /// stPr_UsuarioAPP
        /// </summary>
        /// <param name="stDato">Codigo del usuario de la aplicacion</param>
        public void set_UsuarioApp(String stDato)
        {
            stPr_UsuarioAPP = stDato;
        }


        /// <summary>
        /// Metodo : set_ArchivoLog
        /// Cambia el nombre del archivo log de la  aplicacion.
        /// stPr_ArchivoLog
        /// 
        /// </summary>
        /// <param name="stDato">nuevo nombre del log de la aplicacion</param>
        public void set_ArchivoLog(String stDato)
        {
            stPr_ArchivoLog = stDato;
        }


        /// <summary>
        /// Metodo : set_ArchivoConf
        /// Cambia el nombre del archivo de configuracion, con el cual trabaja la clase
        /// stPr_ArchivoConf
        /// 
        /// </summary>
        /// <param name="stDato">nuevo nombre del archivo de configuracion con el cual trabaja la clase</param>
        public void set_ArchivoConf(String stDato)
        {
            stPr_ArchivoConf = stDato;
        }


        /// <summary>
        /// Metodo : blGeneraTurno_CIEL
        /// Metodo encargado de generar el Turno de CIEL, utilizando el servicio Web indicado por ellos.
        /// Devuelve TRUE = Si genero el turno
        /// Devuelve FALSE = Si no genero el turno o si el web service genero algun error.
        /// En el parametro : st_NoTurno, Devuelve el numero del turno generado por el web service
        /// En el parametro : st_MensajeErrorCIEL, Devuelve el mensaje de error generado por el web service
        /// </summary>
        /// <param name="st_CedulaAfiliado">Cedula del afiliado</param>
        /// <param name="st_Apellidos_Nombres_Afiliado">Apellidos y nombres del afiliado</param>
        /// <param name="st_ServicioXTurno">Tipo de Servicio para el Turno, por ejemplo Informacion General</param>
        /// <param name="st_NoTurno">Devuelve el numero del turno generado por el web service</param>
        /// <param name="st_MensajeErrorCIEL">Devuelve el mensaje de error generado por el web service</param>
        /// <param name="st_FEchaHoraTurno">Devuelve la fecha y hora del turno. Informacion devuelva por el web service</param>
        /// <param name="st_MensajeWeb_Service">Devuelve el mensaje de error generado por el web service, tal y como el web service lo genera</param>
        /// <param name="bl_GeneraDummy">True = Genera un turno Dummy, para iniciar la conexion. False = Genera el turno normalmente.</param>
        /// <returns></returns>
        public Boolean blGeneraTurno_CIEL(String st_CedulaAfiliado, String st_Apellidos_Nombres_Afiliado, String st_ServicioXTurno, ref String st_NoTurno, ref String st_MensajeErrorCIEL, ref String st_FEchaHoraTurno, ref String st_MensajeWeb_Service, Boolean bl_GeneraDummy = false)
        {
            // Metodo encargado de generar el Turno de CIEL, utilizando el servicio Web indicado por ellos.
            // Devuelve TRUE = Si genero el turno
            // Devuelve FALSE = Si no genero el turno o si el web service genero algun error.
            Boolean blL_GeneroTurno = false;
            String stL_MsgSegui = "";
            String stL_LLave_X_Cola = "";
            //
            String stL_URL = "";
            String stL_Oficinaconf = "";
            //
            String stL_UNIGUID = "";
            String stL_UNITURNO = "";
            Boolean bl_IMPRIMIR = false;
            String stL_HORASOLICITUD = "";
            String stL_SALA = "";
            String stL_USUARIOEMISOR = "";
            String stL_HARDWARERECEPTOR = "";
            String stL_OBSERVACIONES = "";
            String stL_SELECTOR = "";
            String stL_COLA = "";
            String stL_TURNO = "";
            String stL_CLIENTE = "";
            String stL_IDCLIENTE = "";
            String stL_NOMBRE = "";
            String stL_PRIORIDAD = "";
            String stL_TIPOID = "";
            String stL_ERROR = "";
            String stL_MODO = "2"; // Siempre 2 = Turno Normal
            String stL_AREA = "";
            String stL_TIPOCLIENTE = "";
            String stL_SERVICIO = "";
            String stL_SUBSERVICIO = "";
            String stL_USUARIORECEPTOR = "";
            String stL_TURNOUSUARIO = "";
            //
            _C_ProgRes.ClasX_Config ObjL_ConfigApp = null;
            //
            try
            {
                //
                stL_IDCLIENTE = st_CedulaAfiliado;
                stL_CLIENTE = st_CedulaAfiliado;
                stL_NOMBRE = st_Apellidos_Nombres_Afiliado; 
                //
                st_NoTurno = "";
                st_MensajeErrorCIEL = "";
                st_FEchaHoraTurno = "";
                st_MensajeWeb_Service = "";
                //
                //
                WebReferenceCIEL.IISelectorservice servicio = new _Kiosko_WS.WebReferenceCIEL.IISelectorservice();
                // DEfine estructura TSoapTURNO
                WebReferenceCIEL.TSoapTURNO InfoTurno = new WebReferenceCIEL.TSoapTURNO();
                /////////////////////////////////////////////////////////////////////
                // Se arman los parametros, con base en el .CONF
                /////////////////////////////////////////////////////////////////////
                ObjL_ConfigApp = new _C_ProgRes.ClasX_Config(stPr_ArchivoConf, stPr_ArchivoLog, stPr_ArchivoLog);
                ///////////////////////////////////////
                // Cambia la URL del servicio
                ///////////////////////////////////////
                stL_URL = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Url_Web_Ser_Digiturno");
                if (stL_URL.Length > 0)
                {
                    servicio.Url = stL_URL;
                }
                // Lee la informacion de la oficina
                stL_Oficinaconf = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
                // Con base en la oficina, se hallan los datos de la sala y selector
                stL_SALA = ObjL_ConfigApp.LeeLlave_Seccion(stL_Oficinaconf, "COD_SALA");
               // Si va a generar una prueba, cambia el codigo del selector
                if (bl_GeneraDummy)
                {
                    stL_SELECTOR = ObjL_ConfigApp.LeeLlave_Seccion(stL_Oficinaconf, "COD_SELECTOR") + "LIXFEWSUSFOIFDU";
                }
                else
                {
                    stL_SELECTOR = ObjL_ConfigApp.LeeLlave_Seccion(stL_Oficinaconf, "COD_SELECTOR");
                }
                // Usuario
                stL_USUARIOEMISOR = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Usuario_Digiturno");
                // La prioridad
                stL_PRIORIDAD = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Llave_Prioridad_Digiturno");
                // La Cola La determina con base en el servicio seleccionado 
                stL_LLave_X_Cola = st_ServicioXTurno;
                //
                if (stL_LLave_X_Cola.Length == 0) 
                {
                    // Si el servicio viene vacio lo genera para Informacion General
                    stL_LLave_X_Cola = "Informacion_General";
                }
                //
                stL_COLA = ObjL_ConfigApp.LeeLlave_Seccion("SERVICIOS_DIGITURNO", stL_LLave_X_Cola);
                if (stL_COLA.Length == 0)
                {
                    // Si la cola esta vacia toma el de la Informacion General
                    stL_LLave_X_Cola = "Informacion_General";
                    stL_COLA = ObjL_ConfigApp.LeeLlave_Seccion("SERVICIOS_DIGITURNO", stL_LLave_X_Cola);
                }
                //
                // Arma la estructura , como la espera el web service
                // 
                InfoTurno.UNIGUID = stL_UNIGUID;
                InfoTurno.UNITURNO = stL_UNITURNO;
                InfoTurno.IMPRIMIR = bl_IMPRIMIR;
                InfoTurno.HORASOLICITUD = stL_HORASOLICITUD;
                InfoTurno.SALA = stL_SALA;
                InfoTurno.USUARIOEMISOR = stL_USUARIOEMISOR;
                InfoTurno.HARDWARERECEPTOR = stL_HARDWARERECEPTOR;
                InfoTurno.OBSERVACIONES = stL_OBSERVACIONES;
                InfoTurno.SELECTOR = stL_SELECTOR;
                InfoTurno.COLA = stL_COLA;
                InfoTurno.TURNO = stL_TURNO;
                InfoTurno.CLIENTE = stL_CLIENTE;
                InfoTurno.IDCLIENTE = stL_IDCLIENTE;
                InfoTurno.NOMBRE = stL_NOMBRE;
                InfoTurno.PRIORIDAD = stL_PRIORIDAD;
                InfoTurno.TIPOID = stL_TIPOID;
                InfoTurno.ERROR = stL_ERROR;
                InfoTurno.MODO = stL_MODO;
                InfoTurno.AREA = stL_AREA;
                InfoTurno.TIPOCLIENTE = stL_TIPOCLIENTE;
                InfoTurno.SERVICIO = stL_SERVICIO;
                InfoTurno.SUBSERVICIO = stL_SUBSERVICIO;
                InfoTurno.USUARIORECEPTOR = stL_USUARIORECEPTOR;
                InfoTurno.TURNOUSUARIO = stL_TURNOUSUARIO;
                //
                //////////////////////////////////////////
                // Graba en el archivo de log, lo que esta haciendo.
                stL_MsgSegui = "Componente = Fenix_Kiosko_WS.Dll " ;
                stL_MsgSegui = stL_MsgSegui + " " + "ClassName = ClasX_Web_Service";
                stL_MsgSegui = stL_MsgSegui + " " + "Method    = blGeneraTurno_CIEL";
                stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Llamando el Web Service de CIEL GeneraTurno";
                stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Afiliado : " + st_CedulaAfiliado + " " + st_Apellidos_Nombres_Afiliado;
                //
                stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SALA : " + stL_SALA;
                stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SELECTOR : " + stL_SELECTOR;
                stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO PRIORIDAD : " + stL_PRIORIDAD;
                stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " COLA O SERVICIO : " + stL_COLA + " ( " + stL_LLave_X_Cola + " ) ";
                //
                _C_ProgRes.ClasX_EventLog objL_Log1 = new _C_ProgRes.ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
                objL_Log1.setTextErrLog(stL_MsgSegui);
                //////////////////////////////////////////
                //
                InfoTurno = servicio.GeneraTurno(InfoTurno);
                //
                if (InfoTurno.ERROR.Length == 0)
                {
                    // Graba en el archivo de log, lo que esta haciendo.
                    st_NoTurno = InfoTurno.TURNO;
                    // Devuelve la fecha y hora del turno
                    st_FEchaHoraTurno = InfoTurno.HORASOLICITUD;
                    blL_GeneroTurno = true;
                    //////////////////////////////////////////
                    // Graba en el archivo de log, lo que esta haciendo.
                    stL_MsgSegui = "Componente = Fenix_Kiosko_WS.Dll ";
                    stL_MsgSegui = stL_MsgSegui + " " + "ClassName = ClasX_Web_Service";
                    stL_MsgSegui = stL_MsgSegui + " " + "Method    = blGeneraTurno_CIEL";
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Se Genero el Turno : " + st_NoTurno;
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Afiliado : " + st_CedulaAfiliado + " " + st_Apellidos_Nombres_Afiliado;
                    //
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SALA : " + stL_SALA;
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SELECTOR : " + stL_SELECTOR;
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO PRIORIDAD : " + stL_PRIORIDAD;
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " COLA O SERVICIO : " + stL_COLA + " ( " + stL_LLave_X_Cola + " ) ";
                    //
                    _C_ProgRes.ClasX_EventLog objL_Log2 = new _C_ProgRes.ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
                    objL_Log2.setTextErrLog(stL_MsgSegui);
                    //////////////////////////////////////////
                }
                else
                {
                    // DEvuelve el mensaje exacto que devuelve el web service 
                    st_MensajeWeb_Service = InfoTurno.ERROR;
                    // Graba en el archivo de log, lo que esta haciendo.
                    st_MensajeErrorCIEL = "El Web Service Genero el mensaje de error : " + InfoTurno.ERROR;
                    //////////////////////////////////////////
                    // Graba en el archivo de log, lo que esta haciendo.
                    stL_MsgSegui = "Componente = Fenix_Kiosko_WS.Dll ";
                    stL_MsgSegui = stL_MsgSegui + " " + "ClassName = ClasX_Web_Service";
                    stL_MsgSegui = stL_MsgSegui + " " + "Method    = blGeneraTurno_CIEL";
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " " + st_MensajeErrorCIEL;
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Afiliado : " + st_CedulaAfiliado + " " + st_Apellidos_Nombres_Afiliado;
                    //
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SALA : " + stL_SALA;
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SELECTOR : " + stL_SELECTOR;
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO PRIORIDAD : " + stL_PRIORIDAD;
                    stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " COLA O SERVICIO : " + stL_COLA + " ( " + stL_LLave_X_Cola + " ) ";
                    //
                    _C_ProgRes.ClasX_EventLog objL_Log3 = new _C_ProgRes.ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
                    objL_Log3.setTextErrLog(stL_MsgSegui);
                    //////////////////////////////////////////
                }
                //
                return blL_GeneroTurno;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // no genera salida a pantalla debido a que esta en una dll y se ejecuta desde el Kiosko
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
                //
                objL_Log.outMensajError("_Kiosko_WS.Dll", "ClasX_Web_Service", "blGeneraTurno_CIEL", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return blL_GeneroTurno;
            }
        }
    









        //   public Boolean blGeneraTurno(String st_CedulaAfiliado, String st_Apellidos_Nombres_Afiliado, String st_ServicioXTurno, ref String st_NoTurno, ref String st_MensajeErrorCIEL, ref String st_FEchaHoraTurno, ref String st_MensajeWeb_Service, Boolean bl_GeneraDummy = false)
        //{
        //    // Metodo encargado de generar el Turno de CIEL, utilizando el servicio Web indicado por ellos.
        //    // Devuelve TRUE = Si genero el turno
        //    // Devuelve FALSE = Si no genero el turno o si el web service genero algun error.
        //    Boolean blL_GeneroTurno = false;
        //    String stL_MsgSegui = "";
        //    String stL_LLave_X_Cola = "";
        //    //
        //    String stL_URL = "";
        //    String stL_Oficinaconf = "";
        //    //
        //    String stL_UNIGUID = "";
        //    String stL_UNITURNO = "";
        //    Boolean bl_IMPRIMIR = false;
        //    String stL_HORASOLICITUD = "";
        //    String stL_SALA = "";
        //    String stL_USUARIOEMISOR = "";
        //    String stL_HARDWARERECEPTOR = "";
        //    String stL_OBSERVACIONES = "";
        //    String stL_SELECTOR = "";
        //    String stL_COLA = "";
        //    String stL_TURNO = "";
        //    String stL_CLIENTE = "";
        //    String stL_IDCLIENTE = "";
        //    String stL_NOMBRE = "";
        //    String stL_PRIORIDAD = "";
        //    String stL_TIPOID = "";
        //    String stL_ERROR = "";
        //    String stL_MODO = "2"; // Siempre 2 = Turno Normal
        //    String stL_AREA = "";
        //    String stL_TIPOCLIENTE = "";
        //    String stL_SERVICIO = "";
        //    String stL_SUBSERVICIO = "";
        //    String stL_USUARIORECEPTOR = "";
        //    String stL_TURNOUSUARIO = "";
        //    //
        //    _C_ProgRes.ClasX_Config ObjL_ConfigApp = null;
        //    //
        //    try
        //    {
        //        //
        //        stL_IDCLIENTE = st_CedulaAfiliado;
        //        stL_CLIENTE = st_CedulaAfiliado;
        //        stL_NOMBRE = st_Apellidos_Nombres_Afiliado; 
        //        //
        //        st_NoTurno = "";
        //        st_MensajeErrorCIEL = "";
        //        st_FEchaHoraTurno = "";
        //        st_MensajeWeb_Service = "";
        //        //
        //        //
        //        WebReferenceCIEL.IISelectorservice servicio = new _Kiosko_WS.WebReferenceCIEL.IISelectorservice();
        //        // DEfine estructura TSoapTURNO
        //        WebReferenceCIEL.TSoapTURNO InfoTurno = new WebReferenceCIEL.TSoapTURNO();
        //        /////////////////////////////////////////////////////////////////////
        //        // Se arman los parametros, con base en el .CONF
        //        /////////////////////////////////////////////////////////////////////
        //        ObjL_ConfigApp = new _C_ProgRes.ClasX_Config(stPr_ArchivoConf, stPr_ArchivoLog, stPr_ArchivoLog);
        //        ///////////////////////////////////////
        //        // Cambia la URL del servicio
        //        ///////////////////////////////////////
        //        stL_URL = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Url_Web_Ser_Digiturno");
        //        if (stL_URL.Length > 0)
        //        {
        //            servicio.Url = stL_URL;
        //        }
        //        // Lee la informacion de la oficina
        //        stL_Oficinaconf = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Oficina");
        //        // Con base en la oficina, se hallan los datos de la sala y selector
        //        stL_SALA = ObjL_ConfigApp.LeeLlave_Seccion(stL_Oficinaconf, "COD_SALA");
        //       // Si va a generar una prueba, cambia el codigo del selector
        //        if (bl_GeneraDummy)
        //        {
        //            stL_SELECTOR = ObjL_ConfigApp.LeeLlave_Seccion(stL_Oficinaconf, "COD_SELECTOR") + "LIXFEWSUSFOIFDU";
        //        }
        //        else
        //        {
        //            stL_SELECTOR = ObjL_ConfigApp.LeeLlave_Seccion(stL_Oficinaconf, "COD_SELECTOR");
        //        }
        //        // Usuario
        //        stL_USUARIOEMISOR = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Usuario_Digiturno");
        //        // La prioridad
        //        stL_PRIORIDAD = ObjL_ConfigApp.LeeLlave_Seccion("INFO_KIOSKO", "Llave_Prioridad_Digiturno");
        //        // La Cola La determina con base en el servicio seleccionado 
        //        stL_LLave_X_Cola = st_ServicioXTurno;
        //        //
        //        if (stL_LLave_X_Cola.Length == 0) 
        //        {
        //            // Si el servicio viene vacio lo genera para Informacion General
        //            stL_LLave_X_Cola = "Informacion_General";
        //        }
        //        //
        //        stL_COLA = ObjL_ConfigApp.LeeLlave_Seccion("SERVICIOS_DIGITURNO", stL_LLave_X_Cola);
        //        if (stL_COLA.Length == 0)
        //        {
        //            // Si la cola esta vacia toma el de la Informacion General
        //            stL_LLave_X_Cola = "Informacion_General";
        //            stL_COLA = ObjL_ConfigApp.LeeLlave_Seccion("SERVICIOS_DIGITURNO", stL_LLave_X_Cola);
        //        }
        //        //
        //        // Arma la estructura , como la espera el web service
        //        // 
        //        InfoTurno.UNIGUID = stL_UNIGUID;
        //        InfoTurno.UNITURNO = stL_UNITURNO;
        //        InfoTurno.IMPRIMIR = bl_IMPRIMIR;
        //        InfoTurno.HORASOLICITUD = stL_HORASOLICITUD;
        //        InfoTurno.SALA = stL_SALA;
        //        InfoTurno.USUARIOEMISOR = stL_USUARIOEMISOR;
        //        InfoTurno.HARDWARERECEPTOR = stL_HARDWARERECEPTOR;
        //        InfoTurno.OBSERVACIONES = stL_OBSERVACIONES;
        //        InfoTurno.SELECTOR = stL_SELECTOR;
        //        InfoTurno.COLA = stL_COLA;
        //        InfoTurno.TURNO = stL_TURNO;
        //        InfoTurno.CLIENTE = stL_CLIENTE;
        //        InfoTurno.IDCLIENTE = stL_IDCLIENTE;
        //        InfoTurno.NOMBRE = stL_NOMBRE;
        //        InfoTurno.PRIORIDAD = stL_PRIORIDAD;
        //        InfoTurno.TIPOID = stL_TIPOID;
        //        InfoTurno.ERROR = stL_ERROR;
        //        InfoTurno.MODO = stL_MODO;
        //        InfoTurno.AREA = stL_AREA;
        //        InfoTurno.TIPOCLIENTE = stL_TIPOCLIENTE;
        //        InfoTurno.SERVICIO = stL_SERVICIO;
        //        InfoTurno.SUBSERVICIO = stL_SUBSERVICIO;
        //        InfoTurno.USUARIORECEPTOR = stL_USUARIORECEPTOR;
        //        InfoTurno.TURNOUSUARIO = stL_TURNOUSUARIO;
        //        //
        //        //////////////////////////////////////////
        //        // Graba en el archivo de log, lo que esta haciendo.
        //        stL_MsgSegui = "Componente = Fenix_Kiosko_WS.Dll " ;
        //        stL_MsgSegui = stL_MsgSegui + " " + "ClassName = ClasX_Web_Service";
        //        stL_MsgSegui = stL_MsgSegui + " " + "Method    = blGeneraTurno_CIEL";
        //        stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Llamando el Web Service de CIEL GeneraTurno";
        //        stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Afiliado : " + st_CedulaAfiliado + " " + st_Apellidos_Nombres_Afiliado;
        //        //
        //        stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SALA : " + stL_SALA;
        //        stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SELECTOR : " + stL_SELECTOR;
        //        stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO PRIORIDAD : " + stL_PRIORIDAD;
        //        stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " COLA O SERVICIO : " + stL_COLA + " ( " + stL_LLave_X_Cola + " ) ";
        //        //
        //        _C_ProgRes.ClasX_EventLog objL_Log1 = new _C_ProgRes.ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
        //        objL_Log1.setTextErrLog(stL_MsgSegui);
        //        //////////////////////////////////////////
        //        //
        //        InfoTurno = servicio.GeneraTurno(InfoTurno);
        //        //
        //        if (InfoTurno.ERROR.Length == 0)
        //        {
        //            // Graba en el archivo de log, lo que esta haciendo.
        //            st_NoTurno = InfoTurno.TURNO;
        //            // Devuelve la fecha y hora del turno
        //            st_FEchaHoraTurno = InfoTurno.HORASOLICITUD;
        //            blL_GeneroTurno = true;
        //            //////////////////////////////////////////
        //            // Graba en el archivo de log, lo que esta haciendo.
        //            stL_MsgSegui = "Componente = Fenix_Kiosko_WS.Dll ";
        //            stL_MsgSegui = stL_MsgSegui + " " + "ClassName = ClasX_Web_Service";
        //            stL_MsgSegui = stL_MsgSegui + " " + "Method    = blGeneraTurno_CIEL";
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Se Genero el Turno : " + st_NoTurno;
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Afiliado : " + st_CedulaAfiliado + " " + st_Apellidos_Nombres_Afiliado;
        //            //
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SALA : " + stL_SALA;
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SELECTOR : " + stL_SELECTOR;
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO PRIORIDAD : " + stL_PRIORIDAD;
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " COLA O SERVICIO : " + stL_COLA + " ( " + stL_LLave_X_Cola + " ) ";
        //            //
        //            _C_ProgRes.ClasX_EventLog objL_Log2 = new _C_ProgRes.ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
        //            objL_Log2.setTextErrLog(stL_MsgSegui);
        //            //////////////////////////////////////////
        //        }
        //        else
        //        {
        //            // DEvuelve el mensaje exacto que devuelve el web service 
        //            st_MensajeWeb_Service = InfoTurno.ERROR;
        //            // Graba en el archivo de log, lo que esta haciendo.
        //            st_MensajeErrorCIEL = "El Web Service Genero el mensaje de error : " + InfoTurno.ERROR;
        //            //////////////////////////////////////////
        //            // Graba en el archivo de log, lo que esta haciendo.
        //            stL_MsgSegui = "Componente = Fenix_Kiosko_WS.Dll ";
        //            stL_MsgSegui = stL_MsgSegui + " " + "ClassName = ClasX_Web_Service";
        //            stL_MsgSegui = stL_MsgSegui + " " + "Method    = blGeneraTurno_CIEL";
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " " + st_MensajeErrorCIEL;
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " Afiliado : " + st_CedulaAfiliado + " " + st_Apellidos_Nombres_Afiliado;
        //            //
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SALA : " + stL_SALA;
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO SELECTOR : " + stL_SELECTOR;
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " CODIGO PRIORIDAD : " + stL_PRIORIDAD;
        //            stL_MsgSegui = stL_MsgSegui = stL_MsgSegui + " COLA O SERVICIO : " + stL_COLA + " ( " + stL_LLave_X_Cola + " ) ";
        //            //
        //            _C_ProgRes.ClasX_EventLog objL_Log3 = new _C_ProgRes.ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
        //            objL_Log3.setTextErrLog(stL_MsgSegui);
        //            //////////////////////////////////////////
        //        }
        //        //
        //        return blL_GeneroTurno;
        //    }
        //    catch (Exception ex)
        //    {
        //        ///////////////////////////////////////////////////////////////
        //        // Manejo de log
        //        ///////////////////////////////////////////////////////////////
        //        // no genera salida a pantalla debido a que esta en una dll y se ejecuta desde el Kiosko
        //        ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, false);
        //        //
        //        objL_Log.outMensajError("_Kiosko_WS.Dll", "ClasX_Web_Service", "blGeneraTurno_CIEL", ex.ToString(), ex.Message.ToString(), "", "");
        //        ///////////////////////////////////////////////////////////////
        //        // Fin Manejo de log
        //        ///////////////////////////////////////////////////////////////
        //        return blL_GeneroTurno;
        //    }
        //}
    







    
    
    }
}
