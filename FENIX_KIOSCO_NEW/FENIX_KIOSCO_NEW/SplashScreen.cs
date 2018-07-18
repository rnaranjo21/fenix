using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FENIX_KIOSCO
{
    public partial class SplashScreen : Form
    {

        private String stPr_VersionApp = Application.ProductVersion; // Version de la aplicacion
        private const int AW_HOR_POSITIVE = 0x1; //Animates the window from left to right. This flag can be used with roll or slide animation.
        private const int AW_HOR_NEGATIVE = 0x2; //Animates the window from right to left. This flag can be used with roll or slide animation.
        private const int AW_VER_POSITIVE = 0x4; //Animates the window from top to bottom. This flag can be used with roll or slide animation.
        private const int AW_VER_NEGATIVE = 0x8; //Animates the window from bottom to top. This flag can be used with roll or slide animation.
        private const int AW_CENTER = 0x10; //Makes the window appear to collapse inward if AW_HIDE is used or expand outward if the AW_HIDE is not used.
        private const int AW_HIDE = 0x10000; //Hides the window. By default, the window is shown.
        private const int AW_ACTIVATE = 0x20000; //Activates the window.
        private const int AW_SLIDE = 0x40000; //Uses slide animation. By default, roll animation is used.
        private const int AW_BLEND = 0x80000; //Uses a fade effect. This flag can be used only if hwnd is a top-level window.

        //[DllImport("user32")]
        //private static extern void AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        //public string ConectaFenix { get; set; }
        //public string ConectaBDFenix { get; set; }
        //public string ConectaGA2 { get; set; }
        //public string ConectaDigi { get; set; }
        //public Action ActionToExecute { get; set; }
        //public virtual Color BackColor { get; set; }


        public SplashScreen()
        {
            InitializeComponent();
            //this.Shown += fSplashScreen_Shown;
        }


        private void SplashScreen_Load(object sender, EventArgs e)
        {
            label1.Text = stPr_VersionApp;
        }

        //private void TmSplash_Tick(object sender, EventArgs e)
        //{
        //    //AnimateWindow(this.Handle, 200, AW_CENTER | AW_HIDE);
        //    this.progressBar1.Increment(1);

        //    if (progressBar1.Value == 100)
        //    {
        //        this.TmSplash.Stop();
        //    }
        //}

        //void fSplashScreen_Shown(object sender, EventArgs e)
        //{
        //    //label1.Text = ConectaFenix;
        //    //label1.BackColor = BackColor;
        //    //label2.Text = ConectaBDFenix;
        //    //label3.Text = ConectaGA2;
        //    //label4.Text = ConectaDigi;
        //    Task.Factory.StartNew(ActionToExecute).ContinueWith((t) => taskCompleted());
        //}

        //private void taskCompleted()
        //{
        //    if (InvokeRequired)
        //    {
        //        this.Invoke((MethodInvoker)(() =>
        //        {
        //            Close();
        //            DialogResult = DialogResult.OK;
        //        }));
        //    }
        //}
    }
}


        