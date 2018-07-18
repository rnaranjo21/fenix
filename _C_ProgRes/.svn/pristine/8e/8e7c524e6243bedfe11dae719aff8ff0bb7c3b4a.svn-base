using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;

namespace _C_ProgRes
{
    public partial class FrmMensajeError : Form
    {
        private String stPr_UsuarioAPP = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        //
        public FrmMensajeError()
        {
            InitializeComponent();
        }


        [HandleProcessCorruptedStateExceptions]
        public void TomaParametros(String st_UsuarioAPP , String st_ArchivoLog , String st_Titulo, String st_Mensaje2, String st_Componente, String st_ClaseMod, String st_Metodo, String st_CodigoErr, String st_MessaDesc, String st_BD = "", String st_InstSQL = "")
        {
            try
            {
                stPr_UsuarioAPP = st_UsuarioAPP;
                stPr_ArchivoLog = st_ArchivoLog;
                //
                this.Text = st_Titulo;
                LblTitulo.Text = st_Mensaje2;
                TxtComponente.Text = st_Componente;
                TxtClase.Text = st_ClaseMod;
                TxtMetodo.Text = st_Metodo;
                //
                TxtCodError.Text = st_CodigoErr;
                TxtCodError.Visible = (st_CodigoErr.Length != 0);
                LblCodError.Visible = (st_CodigoErr.Length != 0);
                //
                TxtDescError.Text = st_MessaDesc;
                TxtDescError.Visible = (st_MessaDesc.Length != 0);
                LblDescError.Visible = (st_MessaDesc.Length != 0);
                //
                TxtBD.Text = st_BD;
                TxtBD.Visible = (st_BD.Length != 0);
                LblBD.Visible = (st_BD.Length != 0);
                //
                TxtSQL.Text = st_InstSQL;
                TxtSQL.Visible = (st_InstSQL.Length != 0);
                LblSQL.Visible = (st_InstSQL.Length != 0);
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "FrmMensajeError", "TomaParametros. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "FrmMensajeError", "TomaParametros", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void CmdCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Cierra la forma
                //
                this.Close();
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "CmdCerrar_Click", "TomaParametros. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                ClasX_EventLog objL_Log = new ClasX_EventLog(stPr_UsuarioAPP, stPr_ArchivoLog, false, true, true);
                //
                objL_Log.outMensajError("_C_ProgRes.DLL", "CmdCerrar_Click", "TomaParametros", "", ex.Message.ToString(), "", "");
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

    }
}
