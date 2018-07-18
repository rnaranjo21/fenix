using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public partial class FrmCambiarClave : Form
    {
        private String _st_User = ""; // Codigo del usuario de la aplicacion
        private String _st_FileLog = ""; // Nombre el Archivo Log.
        //
        private String stPr_ArchivoConfigApp = ""; // Ruta y nombre del archivo de configuracion de la aplicacion,
        //
        private Boolean blPr_AceptoInformacion = false; // Indica si ingreso los datos correctos del usuario
        //
        private CLNBTN_Cg ObjPr_Conf = null; // Para manejar la configuracion de la aplicacion.
        //
        private CLNBTN_IQy ObjPr_InfoBD = null; // Define el objeto de la informacion de la base de datos con la cual va a trabajar.
        //
        private CLNBTN_Ul ObjPr_Utils = null; // Para utilizar las utilidades.
        //
        private const int MAX_INTENTOS = 3; // Maximo numero de intentos
        private int inPr_NumIntentos = 0; // Numero de intentos
        //
        private String stPr_Nombre_App = ""; // Nombre de la aplicacion.
        private String stPr_Version_App = ""; // Version de la aplicacion.
        private String stPr_NombreEmpresa_App = ""; // Nombre de la empresa donde esta instalado la aplicacion.
        //
        private String stPr_Clave_Ori = "";
        private String stPr_Clave1_Ori = "";
        private String stPr_Clave2_Ori = "";
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "FrmChgPwd";
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        //
        private const String ESTADO_ACTIVO = "-1";
        private const String ESTADO_NO_ACTIVO = "0";
        //
        private const String SECCION_BD_0 = "SYS_BD_ZERO"; // La seccion para leer los parametros de la base de datos.
        //private const String SECCION_BD_0_DEMO = "SYS_BD_ZERO_DEMO"; // La seccion para leer los parametros de conexion de la base de datos.
        private const String SECCION_BD_CONNECT_INFO = "BD_CONNECT_INFO"; // La seccion para leer los parametros de conexion con la base de datos, tipo de conexion Fenix.
        //
        private const String SECCION_BD_1 = "SYS_BD_ONE"; // La seccion para leer los parametros de la base de datos. Segunda Base de Datos.
        //
        private const String SECCION_ID_APP = "APP"; // Seccion donde esta el Id de la Aplicacion
        private const String SECCION_OBJETOS_APP = "OBJETOS"; // Seccion donde estan los objetos a registrar
        //
        private const String NEW_LINE = "\r\n"; // Caracteres para nueva linea
        //
        //////////////////////////////////////////////////////////////////////////////////////
        // Los Mensajes utilizados, en la parte del login.
        //////////////////////////////////////////////////////////////////////////////////////
        private const String MENSAJE_1 = "La información del la Base de Datos, no está definida en el archivo de configuración de la aplicación";
        private const String MENSAJE_2 = "El tipo de Conexión para la Base de Datos, está mal configurada o no está definida en el archivo de configuración de la aplicación";
        private const String MENSAJE_3 = "Acceso Negado";
        private const String MENSAJE_4 = "A continuación se debe definir la información de conexión de la Base de Datos, para poder trabajar con la aplicación";
        private const String MENSAJE_5 = "Atención";
        //
        private const String MENSAJE_6 = "¿ Está seguro de grabar esta información, para la conexión con la base de datos ?";
        private const String MENSAJE_7 = "Error al establecer la conexión";
        private const String MENSAJE_8 = "La conexión se estableció correctamente";
        private const String MENSAJE_9 = "Ingreso al Sistema";
        private const String MENSAJE_10 = "Ingreso Información de Conexión a la Base de Datos";
        //
        private const String MENSAJE_11 = "El código de Usuario:";
        private const String MENSAJE_12 = "No está registrado, como usuario válido en el sistema.";
        private const String MENSAJE_13 = "No está ACTIVO en el sistema.";
        private const String MENSAJE_14 = "No tiene contraseña asignada. Se presentará una ventana, para definir la contraseña del usuario.";
        private const String MENSAJE_15 = "Contraseña Inválida.";
        private const String MENSAJE_16 = "Los días de validez de la contraseña, han caducado. Se presentará una ventana, para definir la contraseña del usuario.";
        private const String MENSAJE_17 = "Cambio de Contraseña";
        private const String MENSAJE_18 = "La Contraseña actual no coincide, con la digitada.";
        private const String MENSAJE_19 = "Las nuevas Contraseñas, no son iguales.";
        private const String MENSAJE_20 = "La nueva Contraseña, debe ser diferente de la Contraseña actual.";
        private const String MENSAJE_21 = "Se ha cambiado la Contraseña del usuario.";
        private const String MENSAJE_22 = "Favor entrar en contacto con el Administrador del Sistema.";
        private const String MENSAJE_23 = "Hallando información de los servidores de Bases de Datos disponibles.";
        private const String MENSAJE_24 = "Por favor esperar unos segundos..............";
        private const String MENSAJE_25 = "Intentando establecer conexión con el servidor de Base de Datos.";
        //
        private const String MENSAJE_38 = "Error en la autenticación en el Dominio de Windows";
        //





        public FrmCambiarClave()
        {
            InitializeComponent();
        }

        [HandleProcessCorruptedStateExceptions]
        public void GetParam(String LicName, String WinTittle, String UserName, String LogFile, String ConfFile, String AppInfo_Name, String AppInfo_Ver, String AppInfo_Cia, ref CLNBTN_IQy Obj_BaseDeDatos)
        {
            // Toma los parametros.
            String stL_Aux = "";
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                // Guarda textos originales.
                stPr_Clave_Ori = TxtClave.Text;
                stPr_Clave1_Ori = TxtClave1.Text;
                stPr_Clave2_Ori = TxtClave2.Text;
                //
                inPr_NumIntentos = 0;
                LblMensaje.Text = "";
                //
                this.Text = WinTittle;
                _st_User = UserName;
                _st_FileLog = LogFile;
                stPr_ArchivoConfigApp = ConfFile;
                //
                stPr_Nombre_App = AppInfo_Name;
                stPr_Version_App = AppInfo_Ver;
                stPr_NombreEmpresa_App = AppInfo_Cia;
                // Coloca aplicacion y version
                this.LblModuloVersion.Text = stPr_Nombre_App + " " + stPr_Version_App;
                this.LblNombreCia.Text = stPr_NombreEmpresa_App;
                //
                // Crea instancia para la clase que maneja las configuraciones
                ObjPr_Conf = new CLNBTN_Cg(stPr_ArchivoConfigApp, _st_User, _st_FileLog, _st_Lic);
                //
                ObjPr_Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _st_Lic);
                //
                // Lee el esquema de color
                stL_Aux = "";
                stL_Aux = ObjPr_Conf.ReadAKeyFromSection(SECCION_ID_APP, "Color");
                if (stL_Aux.Length == 0)
                {
                    stL_Aux = "0";
                }
                // La informacion de la base de datos para manejar la conexion con la base de datos.
                ObjPr_InfoBD = new CLNBTN_IQy(_st_Lic);
                ObjPr_InfoBD = Obj_BaseDeDatos;
                //
                if (ObjPr_InfoBD.getUserApp_PWD().Length == 0)
                {
                    GrpClave.Visible = false;
                }
                //
                HabilitaBotones();
                CmdCancelar.Enabled = true;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetParam. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CmdCancelar.Enabled = true;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetParam. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void BringBackParam(ref Boolean InfoIsAccepted, ref CLNBTN_IQy ObDbInfo)
        {
            // Devuelve los parametros
            try
            {
                //
                InfoIsAccepted = blPr_AceptoInformacion;
                // Devuelve el objeto con la informacion de la base de datos.
                ObDbInfo = ObjPr_InfoBD;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringBackParam. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringBackParam. Exception", "", ex.Message.ToString());
            }
        }
        //

        [HandleProcessCorruptedStateExceptions]
        private void HabilitaBotones()
        {
            // Habilita los botones.
            try
            {
                //
                CmdAceptar.Enabled = false;
                //
                // Si los contenidos de los campos son diferentes a los textos originales.
                if (ObjPr_InfoBD.getUserApp_PWD().Length == 0)
                {
                    if ( TxtClave1.Text != stPr_Clave1_Ori && TxtClave2.Text != stPr_Clave2_Ori)
                    {
                        if (TxtClave1.Text.Length > 0 && TxtClave2.Text.Length > 0)
                        {
                            CmdAceptar.Enabled = true;
                        }
                    }
                }
                else
                {
                    if (TxtClave.Text != stPr_Clave_Ori && TxtClave1.Text != stPr_Clave1_Ori && TxtClave2.Text != stPr_Clave2_Ori)
                    {
                        if (TxtClave.Text.Length > 0 && TxtClave1.Text.Length > 0 && TxtClave2.Text.Length > 0)
                        {
                            CmdAceptar.Enabled = true;
                        }
                    }
                }
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(1). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(1). Exception", "", ex.Message.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            CLNBTN_Qy Query = null;
            CLNBTN_Es Encrip = new CLNBTN_Es(_st_User, _st_FileLog, _st_Lic);
            CLNBTN_Ul Utils = new CLNBTN_Ul(_st_User, _st_FileLog, _st_Lic);
            Boolean blL_ValidaClave = false;
            String stL_Encrip = "";
            int inL_DiasClave = 0;
            try
            {
                //
                LblMensaje.Text = "";
                inPr_NumIntentos = inPr_NumIntentos + 1;
                if (inPr_NumIntentos > MAX_INTENTOS)
                {
                    LblMensaje.Text = MENSAJE_3;
                }
                else
                {
                     //
                    switch (ObjPr_InfoBD.get_DataBaseConn_Type())
                    {
                        case CLNBTN_IQy.inConnect_Type.TYPE_1_CONNECT_USER_SQL:
                            //
                            blL_ValidaClave = true;
                            break;
                        case CLNBTN_IQy.inConnect_Type.TYPE_4_CONNECT_USER_INFO_EXT:
                            //
                            blL_ValidaClave = true;
                            break;
                    }
                    if ( blL_ValidaClave ) 
                    { // del if ( blL_ValidaClave ) 
                        // Asigna informacion
                        Query = new CLNBTN_Qy(_st_User, _st_FileLog, _st_Lic);
                        //
                        // Hace la conexion
                        Query.setDataBaseInfo(ObjPr_InfoBD);
                        Query.ConnectDataBase();
                        //
                        if (Query.getIs_Connected())
                        {
                            blL_ValidaClave = false;
                            if (ObjPr_InfoBD.getUserApp_PWD().Length == 0)
                            {
                                blL_ValidaClave = true;
                            }
                            else
                            {
                                if (ObjPr_InfoBD.getUserApp_PWD() == TxtClave.Text)
                                {
                                    blL_ValidaClave = true;
                                }
                                else
                                {
                                    LblMensaje.Text = MENSAJE_18;
                                }
                            }
                            if ( blL_ValidaClave ) 
                            {
                                blL_ValidaClave = false;
                                 if (TxtClave1.Text == TxtClave2.Text)
                                {
                                    if (TxtClave.Text == TxtClave1.Text)
                                    {
                                        LblMensaje.Text = MENSAJE_20;
                                    }
                                    else
                                    {
                                        blL_ValidaClave = true;
                                    }
                                }
                                else
                                {
                                    LblMensaje.Text = MENSAJE_19;
                                }
                            }
                            if (blL_ValidaClave)
                            {
                                // Define DataTable, para los Datos del Query
                                DataTable DatTable = null;
                                //
                                Query.ToDo_SELECT ("*");
                                Query.ToDo_FROM("t00usuarios");
                                Query.ToDo_WHERE("A00USUARIOWIN", "'" + ObjPr_InfoBD.getUser() + "'");
                                Query.ToDo_EXECUTE_SQL(ref DatTable);
                                if (DatTable != null)
                                { // del if (Query.Rs.State != 0)
                                    for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                    {
                                        // Toma la informacion de la fila
                                        DataRow Info_Fila = DatTable.Rows[inL_Row];
                                        inL_DiasClave = 0;
                                        //if (!DBNull.Value.Equals(Info_Fila["A00DIAS_CLAVE"].ToString()))
                                        //{
                                            if ( Info_Fila["A00DIAS_CLAVE"].ToString().Length > 0 ) inL_DiasClave = Convert.ToInt16(Info_Fila["A00DIAS_CLAVE"].ToString());
                                        //}   
                                    }
                                }
                                Query.ToDo_CLOSE();
                                //
                                stL_Encrip = Encrip.File2Es(TxtClave1.Text.Trim(), "", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "", _st_Lic);
                                Query.ToDo_UPDATE("t00usuarios");
                                Query.ToDo_SET("A00ESTADO", ESTADO_ACTIVO);
                                Query.ToDo_SET("A00CLAVE", stL_Encrip);
                                Query.ToDo_SET("A00FECHA_CLAVE", Utils.BringMeServerDate(ObjPr_InfoBD, true));
                                if (inL_DiasClave == 0)
                                {
                                    Query.ToDo_SET("A00DIAS_CLAVE", "30");
                                }
                                Query.ToDo_WHERE("A00USUARIOWIN", " '" + ObjPr_InfoBD.getUser() + "' ");
                                Query.ToDo_EXECUTE_SQL();
                                if (Query.getSuccessQueryExecution())
                                {
                                    ObjPr_InfoBD.setUserApp_PWD(TxtClave1.Text);
                                    ObjPr_InfoBD.setUserApp_PWD_Enc(stL_Encrip);
                                    MessageBox.Show(MENSAJE_21, MENSAJE_5);
                                }
                                Query.ToDo_CLOSE();
                                //
                                blPr_AceptoInformacion = true;
                                // Cierra la forma
                                this.Hide();
                            }
                        }
                    } // del if ( blL_ValidaClave ) 
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(2). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(2). Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void CmdCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                blPr_AceptoInformacion = false;
                // Cierra la forma
                this.Hide();
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(3). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(3). Exception", "", ex.Message.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void TxtClave_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(4). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(4). Exception", "", ex.Message.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void TxtClave1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(5). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(5). Exception", "", ex.Message.ToString());
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void TxtClave2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                HabilitaBotones();
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(6). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FrmChgPwd(6). Exception", "", ex.Message.ToString());
            }
        }





    }
}
