using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.ExceptionServices;


namespace _C_ProgRes
{
    public class ClasX_Security
    { // Inicio del public class ClasX_Security
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        //
        private String stPr_ArchivoConfigApp = ""; // Ruta y nombre del archivo de configuracion de la aplicacion.
        private String stPr_Archivo_InfoBds = ""; // Ruta y nombde del archivo de informacion de las bases de datos, para hacer conexion.
        //
        private String stPr_Nombre_App = ""; // Nombre de la aplicacion.
        private String stPr_Version_App = ""; // Version de la aplicacion.
        private String stPr_NombreEmpresa_App = ""; // Nombre de la empresa donde esta instalado la aplicacion.
        private String stPr_NombreBd_XTrabajo = ""; // Nombre de la base de datos, de trabajo. Si viene definida, no la deja cambiar en la ventana donde pide la informacion del servidor, bd, usauario y clave.
        //
        private String stPr_CodAPP_Seguridad = ""; // Codigo de la aplicacion en el esquema de seguridad.
        //
        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_Sg ObjPr_Self = null;
        private string stPr_ExeName_Exe = "_C_ProgRes.dll"; // el nombre de la dll y la extensión:
        private const String NOM_CLASE = "ClasX_Security";
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = true;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo

        //////////////////////////////////////////////////////////////
        // Constructores
        /// <summary>
        /// Constructor de la clase ClasX_Security, sin parametros.
        /// </summary>
        public ClasX_Security()
        {
            ObjPr_Self = new NBToolsNet.CLNBTN_Sg(stPr_Info);
        }

        /// <summary>
        /// Contructor de la clase, ClasX_Security
        /// Con parametros
        /// </summary>
        /// <param name="st_UsuarioApp">Usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre Archivo de log</param>
        /// <param name="st_ArchivoConfigApp">Ruta y Nombre Archivo de configutracion de la aplicacion</param>
        /// <param name="st_Nombre_App">Nombre de la aplicacion</param>
        /// <param name="st_Version">Version de la aplicacion</param>
        /// <param name="stPr_NombreEmpresa">Nombre de la empresa donde esta la aplicacion</param>
        public ClasX_Security(String st_UsuarioApp, String st_ArchivoLog, String st_ArchivoConfigApp, String st_Nombre_App, String st_Version, String st_NombreEmpresa, String st_Archivo_InfoBds, String st_NombreBd_XTrabajo)
        {
            ObjPr_Self = new NBToolsNet.CLNBTN_Sg(st_UsuarioApp,  st_ArchivoLog,  st_ArchivoConfigApp,  st_Nombre_App,  st_Version,  st_NombreEmpresa,  st_Archivo_InfoBds,  st_NombreBd_XTrabajo,stPr_Info);
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            stPr_ArchivoConfigApp = st_ArchivoConfigApp;
            //
            stPr_Nombre_App = st_Nombre_App;
            stPr_Version_App = st_Version ;
            stPr_NombreEmpresa_App = st_NombreEmpresa;
            //
            stPr_Archivo_InfoBds = st_Archivo_InfoBds;
            stPr_NombreBd_XTrabajo = st_NombreBd_XTrabajo;
        }

        /// <summary>
        /// Contructor de la clase, ClasX_Security
        /// Con parametros, para manejo del log.
        /// </summary>
        /// <param name="st_UsuarioApp">Usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre Archivo de log</param>
        /// <param name="st_ArchivoConfigApp">Ruta y Nombre Archivo de configutracion de la aplicacion</param>
        /// <param name="st_Nombre_App">Nombre de la aplicacion</param>
        /// <param name="st_Version">Version de la aplicacion</param>
        /// <param name="stPr_NombreEmpresa">Nombre de la empresa donde esta la aplicacion</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_Security(String st_UsuarioApp, String st_ArchivoLog, String st_ArchivoConfigApp, String st_Nombre_App, String st_Version, String st_NombreEmpresa, String st_Archivo_InfoBds, String st_NombreBd_XTrabajo, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            ObjPr_Self = new NBToolsNet.CLNBTN_Sg(st_UsuarioApp,  st_ArchivoLog,  st_ArchivoConfigApp,  st_Nombre_App,  st_Version,  st_NombreEmpresa,  st_Archivo_InfoBds,  st_NombreBd_XTrabajo,  bl_SalidaConsola,  bl_SalidaLog,  bl_SalidaDialogo, stPr_Info);
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            stPr_ArchivoConfigApp = st_ArchivoConfigApp;
            //
            stPr_Nombre_App = st_Nombre_App;
            stPr_Version_App = st_Version;
            stPr_NombreEmpresa_App = st_NombreEmpresa;
            //
            stPr_Archivo_InfoBds = st_Archivo_InfoBds;
            stPr_NombreBd_XTrabajo = st_NombreBd_XTrabajo;
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo; 
        }


        /// <summary>
        /// Contructor de la clase, ClasX_Security
        /// Con parametros, para manejo del log.
        /// </summary>
        /// <param name="st_UsuarioApp">Usuario de la aplicacion</param>
        /// <param name="st_ArchivoLog">Nombre Archivo de log</param>
        /// <param name="bl_SalidaConsola">true=Salida a la consola</param>
        /// <param name="bl_SalidaLog">true=Genera Log</param>
        /// <param name="bl_SalidaDialogo">true=Salida por pantalla</param>
        public ClasX_Security(String st_UsuarioApp, String st_ArchivoLog, bool bl_SalidaConsola, bool bl_SalidaLog, bool bl_SalidaDialogo)
        {
            ObjPr_Self = new NBToolsNet.CLNBTN_Sg(st_UsuarioApp, st_ArchivoLog,  bl_SalidaConsola, bl_SalidaLog, bl_SalidaDialogo, stPr_Info);
            stPr_UsuarioAPP = st_UsuarioApp;
            stPr_ArchivoLog = st_ArchivoLog;
            //
            blPr_SalConsole = bl_SalidaConsola;
            blPr_SalLog = bl_SalidaLog;
            blPr_SalDialog = bl_SalidaDialogo;
        }


        /// <summary>
        /// Propiedad getUserApp
        /// devuelve el codigo del usuario de la aplicacion
        /// </summary>
        /// <returns>stPr_UsuarioAPP</returns>
        public string getUserApp()
        {
            stPr_UsuarioAPP = ObjPr_Self.getUser();
            return stPr_UsuarioAPP;
        }
        /// <summary>
        /// Propiedad : getArchivoLog
        /// Devuelve el path del archivo de log,
        /// </summary>
        /// <returns>stPr_ArchivoLog</returns>
        public string getArchivoLog()
        {
            stPr_ArchivoLog = ObjPr_Self.getFileLog();
            return stPr_ArchivoLog;
        }
        /// <summary>
        /// Propiedad : getArchivoConfigApp
        /// Devuelve la ruta y nombre del archivo de configuracion de la app
        /// </summary>
        /// <returns>getArchivoConfigApp</returns>
        public string getArchivoConfigApp()
        {
            stPr_ArchivoConfigApp = ObjPr_Self.getConfFile();
            return stPr_ArchivoConfigApp;
        }
        /// <summary>
        /// Propiedad : getNombre_App
        /// Devuelve el nombre de la aplicacion
        /// </summary>
        /// <returns>stPr_Nombre_App</returns>
        public string getNombre_App()
        {
            stPr_Nombre_App = ObjPr_Self.getAppInfo_Name();
            return stPr_Nombre_App;
        }
        /// <summary>
        /// Propiedad : getVersion_App
        /// Devuelve la version de la aplicacion.
        /// </summary>
        /// <returns>stPr_Version_App</returns>
        public string getVersion_App()
        {
            stPr_Version_App = ObjPr_Self.getAppInfo_Ver();
            return stPr_Version_App;
        }
        /// <summary>
        /// Propiedad : getNombreEmpresa_App
        /// Devuelve el nombre de la empresa donde esta instalada la aplicacion,
        /// </summary>
        /// <returns>stPr_NombreEmpresa_app</returns>
        public string getNombreEmpresa_App()
        {
            stPr_NombreEmpresa_App = ObjPr_Self.getAppInfo_Cia();
            return stPr_NombreEmpresa_App;
        }
        //
        /// <summary>
        /// Propiedad : getArchivo_InfoBds
        /// Devuelve el valor del archivo de configuracion de la base de datos
        /// </summary>
        /// <returns>stPr_Archivo_InfoBds</returns>
        public string getArchivo_InfoBds()
        {
            stPr_Archivo_InfoBds = ObjPr_Self.getInfDBFile();
            return stPr_Archivo_InfoBds;
        }
        /////////////////////////////////////////////////
        /// <summary>
        /// Propiedad : getNombreBd_XTrabajo
        /// Devuelve el nombre de la base de datos, que se va a presentar en la ventana
        /// donde se pide el servidor, bd, usuario y clave
        /// </summary>
        /// <returns>stPr_NombreBd_XTrabajo= Nombre de la bd de trabajo</returns>
        public string getNombreBd_XTrabajo()
        {
            stPr_NombreBd_XTrabajo = ObjPr_Self.getDBName4Work();
            return stPr_NombreBd_XTrabajo;
        }
        /// <summary>
        /// Propiedad : getCodAPP_Seguridad
        /// Devuelve el codigo de la aplicacion en el esquema de seguridad.
        /// </summary>
        /// <returns>El codigo de la aplicacion en el esquema de seguridad</returns>
        public string getCodAPP_Seguridad()
        {
            stPr_CodAPP_Seguridad = ObjPr_Self.getAppInfo_CodSeg();
            return stPr_CodAPP_Seguridad;
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Propiedad : getCodAPP_Seguridad_XConfig
        /// Halla el codigo de la aplicacion definido en el arcivo de configuracion de la aplicacion
        /// Lee del archivo de configuracion, definido en la variable privada stPr_ArchivoConfigApp
        /// </summary>
        /// <returns>stPr_CodAPP_Seguridad = Codigo de la aplicacion definido en el archivo de configuracion</returns>
        public string getCodAPP_Seguridad_XConfig()
        {
            try
            {
                stPr_CodAPP_Seguridad = ObjPr_Self.getAppInfo_CodSeg();
                return stPr_CodAPP_Seguridad;
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "getCodAPP_Seguridad_XConfig. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "getCodAPP_Seguridad_XConfig", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            return stPr_CodAPP_Seguridad;
        }


        /// <summary>
        /// Propiedad : getSalConsole
        /// Devuelve el valor de la salida a la consola en el  manejo del log.
        /// </summary>
        /// <returns>blPr_SalConsole = Salida a la consola en el manejo del log</returns>
        public bool getSalConsole()
        {
            blPr_SalConsole = ObjPr_Self.getOutLineConsole();
            return blPr_SalConsole;
        }

        /// <summary>
        /// Propiedad : getSalLog
        /// Devuelve el valor de la salida a archivo en el manejo del log.
        /// </summary>
        /// <returns>blPr_SalLog = Salida a archivo en el  manejo del log</returns>
        public bool getSalLog()
        {
            blPr_SalLog = ObjPr_Self.getOutFileLog();
            return blPr_SalLog;
        }

        /// <summary>
        /// Propiedad : getSalDialog
        /// Devuelve el valor de la salida a la pantalla en el manejo del log.
        /// </summary>
        /// <returns>blPr_SalDialog = Salida a la pantalla en el  manejo del log</returns>
        public bool getSalDialog()
        {
            blPr_SalDialog = ObjPr_Self.getOutWindow();
            return blPr_SalDialog;
        }


        ////////////////////////////////////////////////
        /// <summary>
        /// Metodo : setUserAPP
        /// Permite definir el codigo del usuario de la aplicacion
        /// </summary>
        /// <param name="st_User">codigo del usuario de la aplicacion</param>
        public void setUserAPP(string st_User)
        {
            stPr_UsuarioAPP = st_User;
            ObjPr_Self.setUser(stPr_UsuarioAPP);
        }
        /// <summary>
        /// Metodo : setArchivoLog
        /// Permite definir el path y nombre del archivo de log.
        /// </summary>
        /// <param name="st_ArchivoLog">path y nombre del archivo de log</param>
        public void setArchivoLog(string st_ArchivoLog)
        {
            stPr_ArchivoLog = st_ArchivoLog;
            ObjPr_Self.setFileLog(stPr_ArchivoLog);
        }
        //
        /// <summary>
        /// Metodo : setArchivoConfigApp
        /// Permite definir la ruta y el nombre del archivo de configuracion de la app
        /// </summary>
        /// <param name="st_ArchivoConfigApp">la ruta y el nombre del archivo de configuracion de la app</param>
        public void setArchivoConfigApp(string st_ArchivoConfigApp)
        {
            stPr_ArchivoConfigApp = st_ArchivoConfigApp;
            ObjPr_Self.setConfFile(stPr_ArchivoConfigApp);
        }
        //
        /// <summary>
        /// Metodo : setNombre_App
        /// Permite definir el nombre de la aplicacion.
        /// </summary>
        /// <param name="st_Nombre_App">Nombre de la aplicacion</param>
        public void setNombre_App(string st_Nombre_App)
        {
            stPr_Nombre_App = st_Nombre_App;
            ObjPr_Self.setAppInfo_Name(stPr_Nombre_App);
        }
        //
        /// <summary>
        /// Metodo : setVersion_App
        /// Permite definir la version de la aplicacion.
        /// </summary>
        /// <param name="st_Version_App">version de la aplicacion</param>
        public void setVersion_App(string st_Version_App)
        {
            stPr_Version_App = st_Version_App;
            ObjPr_Self.setAppInfo_Ver(stPr_Version_App);
        }
        //
        /// <summary>
        /// Metodo : setNombreEmpresa_App
        /// Permite definir el nombre de la empresa donde esta instalada la aplicacion.
        /// </summary>
        /// <param name="st_NombreEmpresaApp">El nombre de la empresa donde esta instalada la aplicacion.</param>
        public void setNombreEmpresa_App(string st_NombreEmpresaApp)
        {
            stPr_NombreEmpresa_App = st_NombreEmpresaApp;
            ObjPr_Self.setAppInfo_Cia(stPr_NombreEmpresa_App);
        }
        //
        /// <summary>
        /// Metodo : setArchivo_InfoBds
        /// Para definir la ruta y nombre de archivo de configuracion de la informacion de la base de datos.
        /// </summary>
        /// <param name="st_NombreArchivo_InfoBds">Ruta y nombre del archivo de configuracion de las bases de datos.</param>
        public void setArchivo_InfoBds(string st_NombreArchivo_InfoBds)
        {
            stPr_Archivo_InfoBds = st_NombreArchivo_InfoBds;
            ObjPr_Self.setInfDBFile(stPr_Archivo_InfoBds);
        }
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Metodo : setNombreBd_XTrabajo
        /// Permite definir el valor de la base de datos de trabajo, para le ventana donde se pide, servidor, bd, usuario y clave
        /// </summary>
        /// <param name="st_NombreBd_XTrabajo">Cambia el valor de la bd de trabajo.</param>
        public void setNombreBd_XTrabajo(string st_NombreBd_XTrabajo)
        {
            stPr_NombreBd_XTrabajo = st_NombreBd_XTrabajo;
            ObjPr_Self.setDBName4Work(stPr_NombreBd_XTrabajo);
        }
        /// <summary>
        /// Metodo : setCodAPP_Seguridad
        /// Permite cambiar el codigo de la aplicacion para el esquema de seguridad,
        /// </summary>
        /// <param name="st_CodApp_Seguridad">Codigo de la aplicacion</param>
        public void setCodAPP_Seguridad(string st_CodApp_Seguridad)
        {
            stPr_CodAPP_Seguridad = st_CodApp_Seguridad;
            ObjPr_Self.setAppInfo_CodSeg(stPr_CodAPP_Seguridad);
        }

        /// <summary>
        /// Metodo : setSalConsole
        /// Permite cambiar el valor de la salida a la consola en el  manejo del log.
        /// </summary>
        /// <param name="bl_SalConsole">Salida a la consola en el manejo del log.</param>
        public void setSalConsole(bool bl_SalConsole)
        {
            blPr_SalConsole = bl_SalConsole;
            ObjPr_Self.setOutLineConsole(blPr_SalConsole);
        }

        /// <summary>
        /// Metodo : setSalLog
        /// Permite cambiar el valor de la a archivo en el  manejo del log.
        /// </summary>
        /// <param name="blPr_SalLog">Salida a  archivo en el manejo del log.</param>
        public void setSalLog(bool bl_SalLog)
        {
            blPr_SalLog = bl_SalLog;
            ObjPr_Self.setOutFileLog(blPr_SalLog);
        }


        /// <summary>
        /// Metodo : setDialog
        /// Permite cambiar el valor de la salida a la pantalla en el  manejo del log.
        /// </summary>
        /// <param name="blPr_SalDialog">Salida a la pantalla en el manejo del log.</param>
        public void setDialog(bool bl_Dialog)
        {
            blPr_SalDialog = bl_Dialog;
            ObjPr_Self.setOutWindow(blPr_SalDialog);
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Login
        /// Hace el login del usuario en la aplicacion, ya sea SSO o via Validacion del usuario en una  base de datos
        /// Devuelve los paramtros asi:
        /// blR_AceptoInfo = true, si se hizo login correctamente.
        /// Obj_BaseDeDatos = Informacion actualizada de la base de datos.
        /// </summary>
        /// <param name="blR_AceptoInfo">true, si se hizo login correctamente.</param>
        /// <param name="Obj_BaseDeDatos">Informacion actualizada de la base de datos.</param>
        /// <param name="bl_PresentaMensajeNoAutenticado">True = Presenta mensaje de error de autenticacion del usuario windows en el Directorio Activo del Dominio</param>
        public void Login(ref Boolean blR_AceptoInfo, ref ClasX_DBInfo Obj_BaseDeDatos, Boolean bl_PresentaMensajeNoAutenticado = false)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_BaseDeDatos, ref O_Aux);
                ObjPr_Self.Let_Login(ref blR_AceptoInfo, ref O_Aux, bl_PresentaMensajeNoAutenticado);
                ObjAux.ConvertirTipoInfoBd(O_Aux, ref Obj_BaseDeDatos);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Login. System.AccessViolationException", ex_0.ToString(), ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "Login", ex.ToString(), ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

              


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ValidaAcceso_UsuarioAPP
        /// Encargado de validar el acceso del usuario a la aplicacion
        /// validando el usuario contra las tablas de la base de datos
        /// donde esta la informacion de los usuarios validos para el sistema.
        /// Devuelve estos parametros :
        /// blR_InfoOk      = true, Si la informacion del usuario esta correcta.
        /// st_MensajeSalida  = Mensaje que indica que error se presento, durante la validacion
        /// blR_PideCambioClave = true, si se debe llamar la ventana de cambio de clave.
        /// Obj_BaseDeDatos = Informacion actualizada de la base de datos.
        /// </summary>
        /// <param name="blR_InfoOk">true, Si la informacion del usuario esta correcta.</param>
        /// <param name="st_MensajeSalida">Mensaje que indica que error se presento, durante la validacion</param>
        /// <param name="blR_PideCambioClave">true, si se debe llamar la ventana de cambio de clave.</param>
        /// <param name="Obj_BaseDeDatos">Informacion actualizada de la base de datos.</param>
        /// <param name="bl_DesdeCambioClave">true = Indica si se esta llamando desde una opcion o un boton de cambio de clave</param>
        /// <param name="bl_LoginFenix">True = Indica que es llamado desde el login de aplicaciones tipo fenix, y no valida la fecha de la clave</param>
        public void ValidaAcceso_UsuarioAPP(ref Boolean blR_InfoOk, ref String st_MensajeSalida, ref Boolean blR_PideCambioClave,  ref ClasX_DBInfo Obj_BaseDeDatos, Boolean bl_DesdeCambioClave, Boolean bl_LoginFenix = false)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_BaseDeDatos, ref O_Aux);
                ObjPr_Self.Is_A_Valid_User_Access(ref  blR_InfoOk, ref  st_MensajeSalida, ref  blR_PideCambioClave, ref  O_Aux, bl_DesdeCambioClave, bl_LoginFenix);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ValidaAcceso_UsuarioAPP. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "ValidaAcceso_UsuarioAPP", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }



        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : HallaPermisos_Objeto.
        /// Sobre Carga Uno.
        /// Encargado de hallar los permisos de :
        /// Read, Write, Delete y Display
        /// sobre un objeto de una aplicacion
        /// para los grupos del usuario de la aplicacion
        /// Devuelve los parametros :
        /// blR_Read = true, tiene permiso de lectura
        /// blR_Write = true tiene permiso de escritura
        /// blR_Delete = true tiene permiso de eliminacion
        /// blR_Display = true tiene permiso de despliegue
        /// Llama al metodo privado LeePermisosObjeto, para validar los permisos
        /// </summary>
        /// <param name="st_Aplicacion">Codigo de la Aplicacion, por ejemplo FENIX30</param>
        /// <param name="st_Objeto">Codigo del Objeto, por ejemplo FICHA_PERSONAL</param>
        /// <param name="Obj_BaseDeDatos">Informacion de la base de datos</param>
        /// <param name="blR_Read">true, tiene permiso de lectura</param>
        /// <param name="blR_Write">true tiene permiso de escritura</param>
        /// <param name="blR_Delete"> true tiene permiso de eliminacion</param>
        /// <param name="blR_Display">true tiene permiso de despliegue</param>
        public void HallaPermisos_Objeto(String st_Aplicacion, String st_Objeto, ClasX_DBInfo Obj_BaseDeDatos, ref Boolean blR_Read, ref Boolean blR_Write, ref Boolean blR_Delete, ref Boolean blR_Display)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_BaseDeDatos, ref O_Aux);
                ObjPr_Self.BringMe_ObjPermission( st_Aplicacion,  st_Objeto, O_Aux, ref  blR_Read, ref  blR_Write, ref  blR_Delete, ref  blR_Display);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "HallaPermisos_Objeto(1). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "HallaPermisos_Objeto(1)", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : HallaPermisos_Objeto.
        /// Sobre Carga Dos.
        /// Encargado de hallar el tipo de Permiso ya sea  :
        /// Read, Write, Delete y Display
        /// Dependiendo del tipo de permiso, definido en el parametro:
        /// st_TipoPermisoValidar = "R" = Leer
        /// st_TipoPermisoValidar = "W" = Escribir
        /// st_TipoPermisoValidar = "D" = Eliminar
        /// st_TipoPermisoValidar = "Y" = Display, Desplegar
        /// sobre un objeto de una aplicacion
        /// para los grupos del usuario de la aplicacion
        /// Devuelve los parametros :
        /// blR_PermisoSobreObjeto = true, si se tiene el permiso asignado, depeniendo del tipo de permiso.
        /// Llama al metodo privado LeePermisosObjeto, para validar los permisos
        /// </summary>
        /// <param name="st_Aplicacion">Codigo de la Aplicacion, por ejemplo FENIX30</param>
        /// <param name="st_Objeto">Codigo del Objeto, por ejemplo FICHA_PERSONAL</param>
        /// <param name="Obj_BaseDeDatos">Informacion de la base de datos</param>
        /// <param name="st_TipoPermisoValidar">Tipo de Permiso a validar</param>
        /// <param name="blR_PermisoSobreObjeto">true, si se tiene el permiso asignado, depeniendo del tipo de permiso.</param>
        public void HallaPermisos_Objeto(String st_Aplicacion, String st_Objeto, ClasX_DBInfo Obj_BaseDeDatos , String st_TipoPermisoValidar, ref Boolean blR_PermisoSobreObjeto)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_BaseDeDatos, ref O_Aux);
                ObjPr_Self.BringMe_ObjPermission(st_Aplicacion, st_Objeto, O_Aux,   st_TipoPermisoValidar, ref  blR_PermisoSobreObjeto);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "HallaPermisos_Objeto(2). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "HallaPermisos_Objeto(2)", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }





        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : RegistraObjetosAplicacion
        /// Graba la infomacion o registra los datos de la aplicacion y los objetos que maneja
        /// en la base de datos
        /// Actualiza la tablas, t03apps y t04app_objts
        /// Con base en el archivo .Conf de la aplicacion, definido en la variable local :
        /// stPr_ArchivoConfigApp
        /// </summary>
        /// <param name="Obj_BaseDeDatos">Informacion de la base de datos</param>
        public void RegistraObjetosAplicacion( ClasX_DBInfo Obj_BaseDeDatos)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_BaseDeDatos, ref O_Aux);
                ObjPr_Self.RegAppObjts( O_Aux);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "RegistraObjetosAplicacion. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "RegistraObjetosAplicacion", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }




        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo: InfDirectorioActivo
        /// Hace la validacion en el directorio Activo, para el usuario que ha hecho login en la maquina de Windows.
        /// </summary>
        /// <param name="bl_PresentaMensajeNoAutenticado">True = Si presenta mensaje de error de auntenticacion</param>
        /// <returns>True = La Autenticacion se pudo hacer. </returns>
        public bool InfDirectorioActivo(Boolean bl_PresentaMensajeNoAutenticado)
        {
            bool blL_Resultado = false;
            //
            try
            {
                blL_Resultado = ObjPr_Self.IsAnActiveDir(bl_PresentaMensajeNoAutenticado);
                return blL_Resultado;
            }
            catch (System.AccessViolationException ex_0)
            {
                blL_Resultado = false;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Importante: Manda el ultimo parametro el FALSE, para que no presente el error por la pantalla
                // por que es cuando se esta inicializando la aplicacion, y no hay nada ejecutando frente al usuario
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ValidaDirectorioActivo. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return blL_Resultado;
            }
            catch (Exception ex)
            {
                blL_Resultado = false;
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Importante: Manda el ultimo parametro el FALSE, para que no presente el error por la pantalla
                // por que es cuando se esta inicializando la aplicacion, y no hay nada ejecutando frente al usuario
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "ValidaDirectorioActivo", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return blL_Resultado;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo: ValidaAutenticacion
        /// realiza la validacion del paht ante el directorio activo
        /// Devuele True o False, si se pudo hacer o no la autenticacion.
        /// </summary>
        /// <param name="st_Path">Informacion a validar, la cual contiene LDA://SERVIDOR:PUERTO</param>
        /// <returns>True o False</returns>
        public bool ValidaAutenticacion(string st_Path)
        {
            try
            {
                return ObjPr_Self.IsAValidAuthentic(st_Path);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Importante: Manda el ultimo parametro el FALSE, para que no presente el error por la pantalla
                // por que es cuando se esta inicializando la aplicacion, y no hay nada ejecutando frente al usuario
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, false);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ValidaAutenticacion. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                // Importante: Manda el ultimo parametro el FALSE, para que no presente el error por la pantalla
                // por que es cuando se esta inicializando la aplicacion, y no hay nada ejecutando frente al usuario
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, false);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "ValidaAutenticacion", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
                return false;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Registra_Ingreso_Usuario_App
        /// Registra el ingreso del usuario a la aplicacion, grabando los datos en la tabla:
        /// t06usuario_accesos, en caso que exista en la base de datos con la cual esta trabajando.
        /// En el objeto de la informacion de la base de datos :
        /// Obj_BaseDeDatos
        /// Coloca el Id de la Tabla, t06usuario_accesos, que se genero.
        /// </summary>
        /// <param name="Obj_BaseDeDatos">Por Referencia. Informacion de la base de datos con la cual va a trabajar.</param>
        public void Registra_Ingreso_Usuario_App(ref ClasX_DBInfo Obj_BaseDeDatos)
        { // Inicio del Registra_Ingreso_Usuario_App
            try
            {
                //
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_BaseDeDatos, ref O_Aux);
                ObjPr_Self.Let_Write_UserAppAccess(ref O_Aux);
                ObjAux.ConvertirTipoInfoBd(O_Aux, ref Obj_BaseDeDatos);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Registra_Ingreso_Usuario_App. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "Registra_Ingreso_Usuario_App", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // Fin del Registra_Ingreso_Usuario_App


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : Registra_Salida_Usuario_App       
        /// Encargado de actualizar la fecha y hora de salida de la aplicacion
        /// Actualiza el campo : A06FECHA_LOGOUT
        /// De la tabla : t06usuario_accesos
        /// con el Id ( A06ID ) , que se genero cuando se ingreso a la aplicacion
        /// y que el Metodo : Registra_Ingreso_Usuario_App, lo grabado en la propiedad:
        /// Obj_BaseDeDatos.getID_Ingreso_Usuario_App()
        /// Al final llama el metodo:
        /// Obj_BaseDeDatos.setID_Ingreso_Usuario_App(0)
        /// Dejando el Id en cero ( 0 ) 
        /// De esta forma se graba la hora y fecha en la que el usuario sale de la aplicacion.
        /// </summary>
        /// <param name="Obj_BaseDeDatos">Por Referencia. Informacion de la base de datos con la cual va a trabajar.</param>
        public void Registra_Salida_Usuario_App(ref ClasX_DBInfo Obj_BaseDeDatos)
        { // Inicio del Registra_Salida_Usuario_App
            try
            {
                //
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_BaseDeDatos, ref O_Aux);
                ObjPr_Self.Let_Write_UserAppAccessOut(ref O_Aux);
                ObjAux.ConvertirTipoInfoBd(O_Aux, ref Obj_BaseDeDatos);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "Registra_Salida_Usuario_App. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe ,  NOM_CLASE , "Registra_Salida_Usuario_App", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        } // Fin del Registra_Salida_Usuario_App


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Encargado de Leer los permisos de las acciones de seguridad y devolver los arreglos con los permisos correspondientes.
        /// Las acciones de seguridad las lee del archivo de configuracion de la aplicacion.
        /// </summary>
        /// <param name="Obj_BaseDeDatos">Objeto con la informacion de la base de datos.</param>
        /// <param name="st_ArchivoConfApp">Ruta y Path del archivo de configuracion de la aplicacion</param>
        /// <param name="st_Objetos">Devuelve Arreglo con los objetos de la seguridad</param>
        /// <param name="bl_Read">Devuelve Arreglo, correspondiente a los objetos con el permiso de READ</param>
        /// <param name="bl_Write">Devuelve Arreglo, correspondiente a los objetos con el permiso de WRITE</param>
        /// <param name="bl_Delete">Devuelve Arreglo, correspondiente a los objetos con el permiso de DELETE</param>
        /// <param name="bl_Display">Devuelve Arreglo, correspondiente a los objetos con el permiso de DISPLAY</param>
        public void HallaInfPermisosModulo(ClasX_DBInfo Obj_BaseDeDatos, String st_ArchivoConfApp , ref string[] st_Objetos, ref Boolean[] bl_Read, ref Boolean[] bl_Write, ref Boolean[] bl_Delete, ref Boolean[] bl_Display)
        {
            try
            {
                ClasX_Utils ObjAux = new ClasX_Utils(stPr_UsuarioAPP, stPr_ArchivoLog);
                NBToolsNet.CLNBTN_IQy O_Aux = new NBToolsNet.CLNBTN_IQy(stPr_Info);
                ObjAux.ConvertirTipoInfoBd(Obj_BaseDeDatos, ref O_Aux);
                ObjPr_Self.BringMe_ModulePermInfo( O_Aux, st_ArchivoConfApp , ref st_Objetos, ref  bl_Read, ref  bl_Write, ref  bl_Delete, ref  bl_Display);
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                 ClasX_EventLog objL_Log = new  ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "HallaInfPermisosModulo. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                 ClasX_EventLog objL_Log = new  ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "HallaInfPermisosModulo", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }




    } // Fin del public class ClasX_Security
}
