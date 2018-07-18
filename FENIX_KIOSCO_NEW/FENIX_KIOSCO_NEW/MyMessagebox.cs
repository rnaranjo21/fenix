using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FENIX_KIOSCO
{
    public partial class MyMessageBox : Form
    {
        static MyMessageBox newMessageBox;
        public Timer timer1;
        static string Button_id;
        int disposeFormTimer; 
        public MyMessageBox()
        {
            InitializeComponent();
        }

        public static string ShowBox(string txtMessage)
        {
            newMessageBox = new MyMessageBox();
            newMessageBox.lblmensaje.Text = txtMessage;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        public static string ShowBox(string txtMessage, string txtTitle, string txtBoton1, string txtBoton2, bool verboton2, int boton1der)
        {
            //new System.Drawing.Font("Microsoft Sans Serif", 10F)
            newMessageBox = new MyMessageBox();
            newMessageBox.lblTitulo.Text = txtTitle;
            newMessageBox.lblmensaje.Text = txtMessage;
            newMessageBox.cmd_boton1.Text = txtBoton1;
            newMessageBox.cmd_boton2.Text = txtBoton2;
            newMessageBox.cmd_boton2.Visible = verboton2;
            newMessageBox.cmd_boton1.Left = boton1der;
            newMessageBox.ShowDialog();
            return Button_id;
        }

        private void MyMessageBox_Load(object sender, EventArgs e)
        {
            disposeFormTimer = 20;
            newMessageBox.lblTimer.Text = disposeFormTimer.ToString();
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += new System.EventHandler(this.timer_tick);
        }

        private void MyMessageBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            //Pen pen1 = new Pen(Color.FromArgb(96, 100, 173), 1);

            Rectangle Area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            //LinearGradientBrush LGB = new LinearGradientBrush(Area1, Color.FromArgb(96, 100, 173), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical);
            //mGraphics.FillRectangle(LGB, Area1);
            //mGraphics.DrawRectangle(pen1, Area1);
        }

        private void cmd_boton1_Click(object sender, EventArgs e)
        {
            newMessageBox.timer1.Stop();
            newMessageBox.timer1.Dispose();
            Button_id = "1";
            newMessageBox.Dispose();
        }

        private void cmd_boton2_Click(object sender, EventArgs e)
        {
            newMessageBox.timer1.Stop();
            newMessageBox.timer1.Dispose();
            Button_id = "2";
            newMessageBox.Dispose();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            disposeFormTimer--;

            if (disposeFormTimer >= 0)
            {
                newMessageBox.lblTimer.Text = disposeFormTimer.ToString();
            }
            else
            {
                newMessageBox.timer1.Stop();
                newMessageBox.timer1.Dispose();
                newMessageBox.Dispose();
            }
        }

        private void cmd_OK_Click(object sender, EventArgs e)
        {
            newMessageBox.timer1.Stop();
            newMessageBox.timer1.Dispose();
            Button_id = "1";
            newMessageBox.Dispose(); 
        }

        private void cmd_cancelar_Click(object sender, EventArgs e)
        {
            newMessageBox.timer1.Stop();
            newMessageBox.timer1.Dispose();
            Button_id = "2";
            newMessageBox.Dispose();
        }


    }
}
