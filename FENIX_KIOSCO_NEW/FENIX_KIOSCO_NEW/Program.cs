using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using _C_ProgRes;
using System.Runtime.ExceptionServices;
using Neurotec.Licensing;

namespace FENIX_KIOSCO
{
    static class Program
    {
        static SplashScreen splashscreen;

        const string Address = "/local";
        /// <summary>
        /// Puerto  TCP para el servidor de licencias
        /// </summary>
        const string Port = "5000";
        /// <summary>
        /// Componentes a validar para activar el servicio
        /// </summary>
        const string Components = "Biometrics.FingerExtraction,Devices.FingerScanners";
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String stPr_VersionApp = Application.ProductVersion; // Version de la aplicacion
            ClasX_EventLog log = new ClasX_EventLog("FENIX", "C:\\FENIX_KIOSCO\\LOGMAIN.log", false, true, false);
            try
            {
                bool created;
                string name = Assembly.GetEntryAssembly().FullName;
                using (Mutex mtex = new Mutex(true, name, out created))
                {
                    if (created)
                    {
                        bool blPr_licStatus = NLicense.ObtainComponents(Address, Port, Components);
                        splashscreen = new SplashScreen();
                        splashscreen.Show();
                        if (blPr_licStatus)
                        {
                            // Creamos una instancia de MainForm y la asignamos dentro de los eventos mostrados y cerrados. 
                            Frm_Principal main = new Frm_Principal();
                            main.Shown += main_Shown;
                            Application.Run(main);
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            //
                            log.setTextErrLog("Main(),   INICIANDO APLICACION KIOSCO FENIX ...");
                            //
                            Application.Run(new FENIX_KIOSCO.Frm_Principal());
                            log.setTextErrLog("Main(), Iniciando Aplicacion Kiosco ...");
                        }
                        else
                        {
                            MessageBox.Show("No se encuentra licencia de huella, se iniciará la aplicación pero la verificación no será realizada", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Frm_Principal main = new Frm_Principal();
                            main.Shown += main_Shown;
                            Application.Run(main);
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            //
                            log.setTextErrLog("Main(),   INICIANDO APLICACION KIOSCO FENIX ...");
                            //

                            log.setTextErrLog("Main(), Iniciando Aplicacion Kiosco ...");
                        }
                    }
                    else
                    {
                        log.setTextErrLog("Main(), La Aplicacion Kiosco se esta Ejecutando ...");
                        //
                        MessageBox.Show("Fenix Kiosco " + stPr_VersionApp + " se encuentra en ejecución o utilizado en otra sesión de usuario de este equipo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Application.Exit();
                        Environment.Exit(1);
                    }
                }
                log.setTextErrLog("Main(), Terminando Aplicacion Kiosco ...");
                Environment.Exit(1);
            }
            catch(AccessViolationException ano)
            {
                log.outMensajError("FENIX_KIOSCO", "Program", "Main", "", ano.Message, "", "");
            }
            catch (Exception ex)
            {
                log.outMensajError("FENIX_KIOSCO","Program","Main","",ex.Message,"","");
            }
        }

        private static bool FirstInstance
        {
            get
            {
                bool created;
                string name = Assembly.GetEntryAssembly().FullName;
                // created will be True if the current thread creates and owns the mutex.
                // Otherwise created will be False if a previous instance already exists.
                Mutex mutex = new Mutex(true, name, out created);
                return created;
            
            }
        }

        static void main_Shown(object sender, EventArgs e)
        {
            // Escondemos la pantalla. 
            splashscreen.Hide();
        }
    }
}
