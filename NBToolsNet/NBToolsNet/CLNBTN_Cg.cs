using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace NBToolsNet
{
    public class CLNBTN_Cg
    {
        // Clase rquivalente : ClasX_Config
        private bool _bl_OutFileLog = false;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        private string _st_User = "CLNBTN_Cg";
        private string _st_PathErrLog = "C:\\Windows\\CLNBTN_Cg.log";  
        //
        private String _st_PathConf = "CLNBTN_Cg.Conf"; //Nombre del archivo de configuracion y ruta de acceso
        private String[] _stInfoFile = new String[0];            //Los parametros obtenidos del archivo
        private String[] _stInfoFileBkp;                         //Arreglo de backup que se usa como resplado a stPr_parametros
        private Boolean _InfoParamOk = false;                        //variable tipo boolean para definir si el error de aplicacion se muestra en u
        private String _st_Lic = "";
        //
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Cg";
        //

        public CLNBTN_Cg(String LicName)
        {
             CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
            else
            {
                _InfoParamOk = this.LoadParamFromFile();
            }
        }

        public CLNBTN_Cg(String ConfFile, String UserName, String LogFile, String LicName)
        {
             CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
            else
            {
                _st_PathConf = ConfFile;
                _st_User = UserName;
                _st_PathErrLog = LogFile;
                //
                _InfoParamOk = this.LoadParamFromFile();
            }
        }

        public CLNBTN_Cg(String ConfFile, String UserName, String LogFile, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
        {
             CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
            else
            {
                _st_PathConf = ConfFile;
                _st_User = UserName;
                _st_PathErrLog = LogFile;
                //
                _InfoParamOk = this.LoadParamFromFile();
                //
                _bl_OutLineConsole = OutLineConsole;
                _bl_OutFileLog = OutFileLog;
                _bl_OutWindow = OutWindow;
            }
        }




        public void setOutFileLog(bool OutFileLog)
        {
            _bl_OutFileLog = OutFileLog;
        }

        public bool getOutFileLog()
        {
            return _bl_OutFileLog;
        }

        public void setOutLineConsole(bool OutLineConsole)
        {
            _bl_OutLineConsole = OutLineConsole;
        }

        public bool getOutLineConsole()
        {
            return _bl_OutLineConsole;
        }


        public void setOutWindow(bool blR_SalDialog)
        {
            _bl_OutWindow = blR_SalDialog;
        }

        public bool getOutWindow()
        {
            return _bl_OutWindow;
        }

        public void setUser(string stR_User)
        {
            _st_User = stR_User;
        }

        public string getUser()
        {
            return _st_User;
        }


        public void setPathErrLog(string FileLog)
        {
            _st_PathErrLog = FileLog;
        }

        public string getPathErrLog()
        {
            return _st_PathErrLog;
        }


        public void setPathConf(string PathConf)
        {
            _st_PathConf = PathConf;
        }

        public string getPathConf()
        {
            return _st_PathConf;
        }

        public void setPathConf(Boolean InfoParamOk)
        {
            _InfoParamOk = InfoParamOk;
        }

        public Boolean getInfoParamOk()
        {
            return _InfoParamOk;
        }


        [HandleProcessCorruptedStateExceptions]
        private void SaveConfFile(String TargetFile)
        {
            /// Metodo que graba el archivo de configuracion, con base en el arreglo de los parametros
            /// que tiene en ese momento
            StreamWriter outt = null;
            int inL_pos = -1;
            //
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (_stInfoFileBkp != null)
                    {
                        inL_pos = this.IsValidParam(_stInfoFile[0]);
                    }
                    if (inL_pos == -1)
                    {
                        outt = File.AppendText(TargetFile);

                        for (int i = 0; i < _stInfoFile.Length; i++)
                        {
                            outt.WriteLine(_stInfoFile[i]);//Escritura de los parametros linea a linea en el vector
                            outt.Flush();
                        }
                        outt.Close();//cierre del archivo
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveConfFile(0). System.AccessViolationException", "", ex_0.Message.ToString());
                outt = null;
            }
            catch (FileNotFoundException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveConfFile(0). FileNotFoundException", "", ex.Message.ToString());
                outt = null;
            }
            catch (IOException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveConfFile(0). IOException", "", ex.Message.ToString());
                outt = null;
            }
            catch (ObjectDisposedException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveConfFile(0). ObjectDisposedException", "", ex.Message.ToString());
                outt = null;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveConfFile(0). Exception", "", ex.Message.ToString());
                outt = null;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void SaveFile(String TargetFile)
        {
            /// Funcion que guarda la informacion del archivo que esta manejando
            /// Con esta funcion se toma la informacion que esta en el arreglo y se guarda  en el archivo que se indica en el destino
            /// Llama el metodo privado , GuardarArchivoConfig.
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Llama el metodo que graba el archivo de configuracion
                    this.SaveConfFile(TargetFile);
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveFile. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveFile. System.AccessViolationException", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void SaveFile()
        {
            /// Metodo encargado de grabar el archivo de parametros, con base en el arreglo
            /// que tiene cargado en ese momento.
            /// Llama el metodo privado , GuardarArchivoConfig.
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Llama el metodo que graba el archivo de configuracion
                    this.SaveConfFile(_st_PathConf);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveFile(2). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveFile(2). System.AccessViolationException", "", ex.Message.ToString());
            }
        }


        public String[] getInfoFile()
        {
            /// Gets de los parametros.
            /// Entrega la lista de parametros recuperada del archivo y cargada en el objeto
            return _stInfoFile;
        }


        [HandleProcessCorruptedStateExceptions]
        public int LookUpParam(String InfoToSeek)
        {
            /// Busca el parametro, dentro del arreglo
            /// </summary>
            /// El indice en el arreglo del parametro que se busca.
            /// </returns>
            /// <param name='parametro'>Parametro a buscar dentro de un arreglo recorriendolo por completo hasta q lo busque</param>
            int inL_Indice = -1;
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    for (int i = 0; i < _stInfoFile.Length; i++)
                    {
                        // compara en mayusculas el contenido del arreglo contra el parametro.
                        // Tiene que ser igual.
                        if (_stInfoFile[i].ToUpper().Equals(InfoToSeek.ToUpper()))
                        {
                            inL_Indice = i;
                            break;
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                inL_Indice = -1;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LookUpParam. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                inL_Indice = -1;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LookUpParam. Exception", "", ex.Message.ToString());
            }
            return inL_Indice;
        }


        [HandleProcessCorruptedStateExceptions]
        private int IsValidParam(String InfoToSeek)
        {
            /// <summary>
            /// Método privado para validar parametro. Recibe un parametro de tipo string</param>
            /// Recorre el arreglo backup comparando el parametro que recibe con lo que contiene este arreglo</param>
            /// </summary>
            /// <param name="_stInfoFileBkp[i]">Es un arreglo de tipo String como backup del arreglo original</param>
            int inL_Indice = -1;
            int i;
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    for (i = 0; i < _stInfoFileBkp.Length; i++)
                    {
                        if (_stInfoFileBkp[i].Contains(InfoToSeek))
                        {
                            inL_Indice = i;
                            break;
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                inL_Indice = -1;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "IsValidParam. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                inL_Indice = -1;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "IsValidParam. System.AccessViolationException", "", ex.Message.ToString());
            }
            return inL_Indice;
        }



        [HandleProcessCorruptedStateExceptions]
        public void setValueOfParam(String InfoToSet, String ValueToSet)
        {
            /// <summary>
            /// Funcion que envia los parametros en un formato Parametro = Valor, recibe dos parametros de tipo String
            /// Con esta funcion se toma la informacion y se acopla al formato anteriormente dicho, y se da el valor a la respectiva llave
            /// </summary>
            /// <param name="parametro">Parametro a buscar en el arreglo actual</param>
            /// <param name="valor">Valor que se encuentra en la llave</param>
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    int i = this.LookUpParam(InfoToSet);
                    if (i != -1)
                    {
                        _stInfoFile[i] = InfoToSet + "=" + ValueToSet;
                    }
                    else
                    {
                        this.AddNewParam(InfoToSet + "=" + ValueToSet);
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "setValueOfParam. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "setValueOfParam. Exception", "", ex.Message.ToString());
            }
        }


        public void setInfoFile(String[] InfoFile)
        {
            /// <summary>
            /// En caso de inicializar el objeto desde un arreglo armado se hace con la funcion setParametros</param>
            /// </summary>
            /// <param name="parametros">Es un arreglo de tipo String con la estructura Parametro = Valor</param>
            _stInfoFile = InfoFile;
        }


        [HandleProcessCorruptedStateExceptions]
        private void AddNewParam(String InfoToAdd)
        {
            /// <summary>
            /// Funcion que agrega parametros linea a linea, recibe un parametro de tipo String "slinea"
            /// </summary>
            /// <param name="String slinea"> Parametro que recibe la funcion para inicializarla</param>
            /// <param name="String[] stmps = new String[_stInfoFile.Length]">Crea un arreglo de tipo String </param>
            /// <param name=" _stInfoFile.CopyTo(stmps, 0)"> Realiza la copia de los parametros cargados en el arreglo </param>
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    String[] stmps = new String[_stInfoFile.Length];
                    _stInfoFile.CopyTo(stmps, 0);
                    _stInfoFile = new String[_stInfoFile.Length + 1];
                    stmps.CopyTo(_stInfoFile, 0);

                    if ((_stInfoFile.Length - 1) < 0)
                    {
                        _stInfoFile[0] = InfoToAdd;
                    }
                    else
                    {
                        _stInfoFile[_stInfoFile.Length - 1] = InfoToAdd;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddNewParam. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddNewParam. System.AccessViolationException", "", ex.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private Boolean LoadParamFromFile()
        {
            /// <summary>
            /// Funcion de caracter booleano para leer el archivo que carga los parametros de la clase
            /// Realiza primero la validacion de que el archivo exista y que dicho valor este contenido sino agrega el parametro.
            /// </summary>
            /// 
            String slinep = "";
            // ASQC Dic 3 2012.
            // No genera el log cuando lee el archivo de configuracion
            // para no generar tantas lineas en el archivo de log.
            //ClasX_EventLog objL_Log1 = new ClasX_EventLog(stPr_user, stPr_patLog, false, true, false);
            //
            //objL_Log1.setPathArchivoLogErr(stPr_patLog);
            //objL_Log1.setUser(stPr_user);
            //objL_Log1.setTextErrLog("csaConfig.LoadParamFromFile: Cargando archivo configuracion: " + _st_PathConf);
            // Fin ASQC Dic 3 2012.
            //
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return false;
                }
                else
                {
                    if (File.Exists(_st_PathConf))
                    {
                        StreamReader brin = File.OpenText(_st_PathConf);
                        ///////////////////////////////////////////////////////////////////////
                        // Inicializa el arreglo de parametros
                        // para que no duplique la informacion
                        // cada vez que pasa por este metodo.
                        ///////////////////////////////////////////////////////////////////////
                        //
                        _stInfoFile = new String[0];
                        ///////////////////////////////////////////////////////////////////////
                        while ((slinep = brin.ReadLine()) != null)
                        {
                            if (slinep.Length > 0)
                            {
                                // Carga todo el archivo.
                                //if (!slinep.Contains("#"))
                                //{
                                this.AddNewParam(slinep);
                                //}
                            }
                        }
                        _stInfoFileBkp = new String[_stInfoFile.Length];
                        _stInfoFile.CopyTo(_stInfoFileBkp, 0);
                        brin.Close();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LoadParamFromFile. System.AccessViolationException", "", ex_0.Message.ToString());
                return false;
            }
            catch (FileNotFoundException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LoadParamFromFile. FileNotFoundException", "", ex.Message.ToString());
                return false;
            }
            catch (IOException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LoadParamFromFile. IOException", "", ex.Message.ToString());
                return false;
            }
            catch (System.UnauthorizedAccessException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LoadParamFromFile. System.UnauthorizedAccessException", "", ex.Message.ToString());
                return false;
            }
            catch (System.ArgumentException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LoadParamFromFile. System.ArgumentException", "", ex.Message.ToString());
                return false;
            }
            catch (System.OutOfMemoryException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LoadParamFromFile. System.OutOfMemoryException", "", ex.Message.ToString());
                return false;
            }
            catch (SystemException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LoadParamFromFile. SystemException", "", ex.Message.ToString());
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LoadParamFromFile. Exception", "", ex.Message.ToString());
                return false;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void DeleteAFile(String Target2Delete)
        {
            /// <summary>
            /// Metodo que elimina el archivo que entra como parametro.
            /// </summary>
            /// <param name="stR_Destino">Ruta y nombre del archivo que se va a eliminar</param>
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (File.Exists(Target2Delete))
                    {
                        File.Delete(Target2Delete);
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAFile. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (ArgumentNullException e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAFile. ArgumentNullException", "", e.Message.ToString());
            }
            catch (ArgumentException e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAFile. ArgumentException", "", e.Message.ToString());
            }
            catch (DirectoryNotFoundException e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAFile. DirectoryNotFoundException", "", e.Message.ToString());
            }
            catch (IOException e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAFile. IOException", "", e.Message.ToString());
            }
            catch (NotSupportedException e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAFile. NotSupportedException", "", e.Message.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAFile. UnauthorizedAccessException", "", e.Message.ToString());
            }
            catch (Exception ex_1)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAFile. Exception", "", ex_1.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public String[] LookUpLoadSection(String SectionToLookUp)
        {
            /// <summary>
            /// Metodo que halla la informacion de la seccion dentro del arreglo de parametros
            /// Halla la posicion Inical y la Final de la seccion dentro del arreglo de parametros
            /// y carga el arreglo 'cadena'.
            /// </summary>
            /// <param name="st_Seccion">Nombre de la seccion a buscar.</param>
            /// <returns></returns>
            // Halla el indice de la seccion
            int Inicio = 0;
            int Final = 0;
            //
            String[] cadena = new String[Final - Inicio];
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    Inicio = this.LookUpParam("[" + SectionToLookUp + "]");
                    if (Inicio >= 0)
                    {
                        Final = this.LookUpAKey(Inicio, "[");
                        cadena = new String[Final - Inicio];
                        //
                        int j = 0;
                        for (int i = Inicio; i < Final; i++)
                        {
                            cadena[j] = _stInfoFile[i];
                            j++;
                        }
                    }
                }
                return cadena;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LookUpLoadSection. System.AccessViolationException", "", ex_0.Message.ToString());
                cadena = null;
                return cadena;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LookUpLoadSection. Exception", "", ex.Message.ToString());
                cadena = null;
                return cadena;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private int LookUpAKey(int InitialPos, String KeyToLookUp)
        {
            /// <summary>
            /// Funcion de tipo entero que recibe un parametro del mismo tipo llamado posicioninicial y otro de tipo string llamado param
            /// int buscallave, recorre el arreglo en busca del parametro que recibe, y devuelve un entero que indica la posicion del parametro que se esta buscando.
            /// </summary>
            /// <param name="posicioninicial"></param>
            /// <param name="param"></param>
            int inL_Indice = -1;
            int i;
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    for (i = InitialPos + 1; i < _stInfoFile.Length; i++)
                    {
                        String cadena_actual = _stInfoFile[i];
                        if (cadena_actual.Contains(KeyToLookUp))
                        {
                            inL_Indice = i;
                            break;
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                inL_Indice = -1;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LookUpAKey. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (ArgumentNullException ex)
            {
                inL_Indice = -1;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LookUpAKey. ArgumentNullException", "", ex.Message.ToString());
            }
            catch (Exception ex_1)
            {
                inL_Indice = -1;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "LookUpAKey. ArgumentNullException", "", ex_1.Message.ToString());
            }
            return inL_Indice;
        }



        [HandleProcessCorruptedStateExceptions]
        public void ModifyAKey(String Section2LookUp, String Key2LookUp, String ValueToSet)
        {
            /// <summary>
            /// Funcion que recibe tres parametros de tipo string llamados secc, llave y valor.
            /// ModificaLlave, recorre un arreglo refe en busca de una seccion especifica, despues busca una llave especifica y el valor correspondiente a esta para modificarlo.
            /// </summary>
            /// <param name="st_Secc">Seccion de la llave a modificar</param>
            /// <param name="st_LLave">LLave a modificar</param>
            /// <param name="st_Valor">Valor a dejar en la llava a modificar</param>
            String param = "[" + Section2LookUp + "]";
            //
            String stL_Aux = "";
            String stL_ParteIzquierda = "";
            String stL_ParteDerecha = "";
            //
            Boolean blL_ExisteLlave = false;
            //
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    int pos = this.LookUpParam(param);
                    // Si la seccion existe.
                    if (pos >= 0)
                    {
                        String[] refe = this.LookUpLoadSection(Section2LookUp);
                        //
                        for (int i = pos; i < refe.Length + pos; i++)
                        {

                            stL_Aux = _stInfoFile[i];
                            // Separa Llave y Valor.
                            this.Separate_Key_Value(stL_Aux, ref stL_ParteIzquierda, ref stL_ParteDerecha);
                            //
                            if (stL_ParteIzquierda.ToUpper().Equals(Key2LookUp.ToUpper()))
                            {
                                _stInfoFile[i] = Key2LookUp + " = " + ValueToSet;
                                blL_ExisteLlave = true;
                                break;
                            }
                        }
                        if (blL_ExisteLlave != true)
                        {
                            // Crea la nueva llave.
                            //String[] st_Temporal = new String[0]; 
                            //for (int i = 0; i < pos ; i++)
                            //{
                            //    //
                            //   
                            //
                            //}

                            //
                        }
                        //
                        File.Delete(_st_PathConf);
                        this.SaveChanges();
                        // Carga nuevamente el arreglo con el archivo plano.
                        _InfoParamOk = this.LoadParamFromFile();
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ModifyAKey. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ModifyAKey. Exception", "", e.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private void SaveChanges()
        {
            /// <summary>
            /// Funcion para guardar las modificaciones que se realicen al archivo plano y ser cargadas en este.
            /// </summary>
            StreamWriter outt = null;
            try
            {
                CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    outt = File.AppendText(_st_PathConf);//Graba todo 
                    //outt = File.CreateText(_st_PathConf);//Graba todo
                    for (int i = 0; i < _stInfoFile.Length; i++)
                    {
                        // Si la linea viene vacia no la graba.
                        if (_stInfoFile[i].Length > 0)
                        {
                            outt.WriteLine(_stInfoFile[i]);
                            outt.Flush();
                        }
                    }
                    outt.Close();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveChanges. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (FileNotFoundException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveChanges. FileNotFoundException", "", ex.Message.ToString());
            }
            catch (IOException ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveChanges. IOException", "", ex.Message.ToString());
            }
            catch (Exception ex_1)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SaveChanges. Exception", "", ex_1.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void DeleteAKeyFromSection(String Section2LookUp, String Key2Delete)
        {
            /// <summary>
            /// Funcion para eliminar un valor especifico en una llave de una seccion. Recibe dos parametros de tipo string, stR_seccion y stR_Llave, para iniciar la busqueda.
            /// Recorre el arreglo en busca de dichos parametros y cuando encuentre la seccion especifica busca la llave especifica dentro de dicha seccion para ser eliminada.
            /// </summary>
            /// <param name="String stR_seccion">Referencia a las secciones del texto plano</param>
            /// <param name="String param = "[" + stR_seccion + "]"">Parametro para buscar secciones dentro del archivo plano teniendo en cuenta "[ ]", para inicio y fin de seccion</param>
            /// <param name="String stR_Llave">Referencia a las llaves del texto plano</param>       
            /// <param name="String[] refe = seccion(stR_seccion);">Arreglo de tipo string para que me almacene la seccion que se busca</param>
            /// <param name="int pos = LookUpParam(param);">Variable de tipo entero para buscar el parametro especifico</param>
            String param = "[" + Section2LookUp + "]";
            //
            String stL_Aux = "";
            String stL_ParteIzquierda = "";
            String stL_ParteDerecha = "";
            //
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    int pos = this.LookUpParam(param);
                    // Si la seccion existe.
                    if (pos >= 0)
                    {
                        String[] refe = this.LookUpLoadSection(Section2LookUp);
                        for (int i = pos; i < refe.Length + pos; i++)
                        {

                            stL_Aux = _stInfoFile[i];
                            // Separa Llave y Valor.
                            this.Separate_Key_Value(stL_Aux, ref stL_ParteIzquierda, ref stL_ParteDerecha);
                            //
                            if (stL_ParteIzquierda.ToUpper().Equals(Key2Delete.ToUpper()))
                            {
                                _stInfoFile[i] = "";
                                break;
                            }

                        }
                        File.Delete(_st_PathConf);
                        this.SaveChanges();
                        _InfoParamOk = this.LoadParamFromFile();
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAKeyFromSection. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "DeleteAKeyFromSection. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String ReadAKeyFromSection(String Section2LookUp, String Key2LookUp)
        {
            /// <summary>
            /// Metodo que lee una llave de una seccion
            /// </summary>
            /// <param name="st_Seccion">Seccion en la cual esta la llave a buscar.</param>
            /// <param name="st_LLave">La Llave a Buscar.</param>
            /// <returns></returns>
            // Lee el Valor de la llave en una seccion.
            String stL_ValorLlave = "";
            String stL_Aux = "";
            String stL_ParteIzquierda = "";
            String stL_ParteDerecha = "";
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Halla el arreglo con la seccion unicamente,
                    String[] imprimible = this.LookUpLoadSection(Section2LookUp);
                    //
                    for (int i = 0; i < imprimible.Length; i++)
                    {
                        stL_Aux = imprimible[i];
                        // Separa Llave y Valor.
                        this.Separate_Key_Value(stL_Aux, ref stL_ParteIzquierda, ref stL_ParteDerecha);
                        //
                        if (stL_ParteIzquierda.ToUpper().Equals(Key2LookUp.ToUpper()))
                        {
                            stL_ValorLlave = stL_ParteDerecha;
                            break;
                        }
                    }
                }
                return stL_ValorLlave;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ReadAKeyFromSection. System.AccessViolationException", "", ex_0.Message.ToString());
                return stL_ValorLlave;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ReadAKeyFromSection. Exception", "", ex.Message.ToString());
                return stL_ValorLlave;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private void Separate_Key_Value(String InputString, ref String KeyInfo, ref String ValueInfo, Boolean NoSpaces = true)
        {
            /// <summary>
            /// Metodo privado : Separate_Key_Value
            /// 
            /// Separa de un string, la llave y el valor.
            /// Por Ejemplo:
            /// " Name = Administracion "
            /// Devuelve : ( Sin Espacios ) , si el parametro : bl_EliminaEspacios = true
            /// Llave = "Name"
            /// Valor = "Administracion"
            /// Devuelve : ( Como esta en el archivo ) , si el parametro : bl_EliminaEspacios = false
            /// Llave = " Name "
            /// Valor = " Administracion "
            /// </summary>
            /// <param name="InputString">La cadena con la llave y el valor ej:  Name = Administracion</param>
            /// <param name="st_LLave">Devuelve la llave Ej: Name</param>
            /// <param name="st_Valor">Devuelve el valor Ej: Administracion </param>
            /// <param name="bl_EliminaEspacios">Indica si quita espacios adicionales. Por defecto true=Quita los espacios adicionales</param>
            // Separa de un string, la llave y el valor.
            // Por Ejemplo:
            // " Name = Administracion "
            // Devuelve : ( Sin Espacios ) , si el parametro : bl_EliminaEspacios = true
            // Llave = "Name"
            // Valor = "Administracion"
            // Devuelve : ( Como esta en el archivo ) , si el parametro : bl_EliminaEspacios = false
            // Llave = " Name "
            // Valor = " Administracion "
            //
            int inL_Pos = 0;
            String stL_ParteIzquierda = "";
            String stL_ParteDerecha = "";
            String stL_Aux = "";
            //
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    inL_Pos = InputString.IndexOf("=");
                    //
                    if (inL_Pos != -1)
                    { // del if (inL_Pos != -1)
                        stL_ParteIzquierda = InputString.Substring(0, inL_Pos);
                        //
                        stL_ParteDerecha = InputString.Substring(inL_Pos + 1, InputString.Length - inL_Pos - 1);
                        //
                        if (NoSpaces)
                        { // del if (NoSpaces)
                            // Le quita los espacios a la parte izquiera y a la parte derecha.
                            // Pero los que estan al inicio y al final de la cadena.
                            stL_Aux = "";
                            for (int i = 0; i < stL_ParteIzquierda.Length; i++)
                            {
                                if (stL_ParteIzquierda.Substring(i, 1) == " ")
                                {
                                    if ((i != 0) && (i != stL_ParteIzquierda.Length - 1))
                                    {
                                        stL_Aux = stL_Aux + stL_ParteIzquierda.Substring(i, 1);
                                    }
                                }
                                else
                                {
                                    stL_Aux = stL_Aux + stL_ParteIzquierda.Substring(i, 1);
                                }
                            }
                            stL_ParteIzquierda = stL_Aux;
                            //
                            stL_Aux = "";
                            for (int i = 0; i < stL_ParteDerecha.Length; i++)
                            {
                                if (stL_ParteDerecha.Substring(i, 1) == " ")
                                {
                                    if ((i != 0) && (i != stL_ParteDerecha.Length - 1))
                                    {
                                        stL_Aux = stL_Aux + stL_ParteDerecha.Substring(i, 1);
                                    }
                                }
                                else
                                {
                                    stL_Aux = stL_Aux + stL_ParteDerecha.Substring(i, 1);
                                }
                            }
                            stL_ParteDerecha = stL_Aux;
                        } // del if (NoSpaces)
                    } // del if (inL_Pos != -1)
                    //////////////////////////////////////////////////////////
                    // Devuelve la llave y su valor.
                    KeyInfo = stL_ParteIzquierda;
                    ValueInfo = stL_ParteDerecha;
                    //
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Separate_Key_Value. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Separate_Key_Value. Exception", "", ex.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void AddNewLine2End(String NewLineInfo)
        {
            /// <summary>
            /// Metedo : AgregarLineaAlFinal
            /// Adiciona una linea al final del arreglo con la informacion del archivo de configuracion.
            /// NO VALIDA SI LA SECCION O LLAVE EXISTE, SOLO ADICIONA LA LINEA AL FINAL DEL ARCHIVO
            /// </summary>
            /// <param name="slinea">La linea a adicionar al final del archivo</param>
            /// 
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    String[] stmps = new String[_stInfoFile.Length];
                    _stInfoFile.CopyTo(stmps, 0);
                    _stInfoFile = new String[_stInfoFile.Length + 1];
                    stmps.CopyTo(_stInfoFile, 0);

                    if ((_stInfoFile.Length - 1) < 0)
                    {
                        _stInfoFile[0] = NewLineInfo;
                    }
                    else
                    {
                        _stInfoFile[_stInfoFile.Length - 1] = NewLineInfo;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddNewLine2End. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddNewLine2End. Exception", "", ex.Message.ToString());
                ///////////////////////////////////////////////////////////////
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void OverWriteConfFile()
        {
            /// <summary>
            /// Metodo : SobreEscribe_ArchivoConfiguracion
            /// Sobre escribe el archivo de configuracion, con los datos que tiene cargados actualmente.
            /// GRABA LA INFORMACION QUE TIENE EL ARREGLO ( _stInfoFile ) EN ESTE MOMENTO
            /// NO SE VALIDA NADA EN EL MOMENTO DE GRABAR, PASA DEL ARREGLO ( _stInfoFile ) AL ARCHIVO DE CONFIGURACION
            /// </summary>
            //
            try
            {
                 CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(_st_Lic);
                _st_Lic = ObL_Lic.getLicName();
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (File.Exists(_st_PathConf))
                    {
                        File.Delete(_st_PathConf);
                        this.SaveChanges();
                        // Carga nuevamente el arreglo con el archivo plano.
                        _InfoParamOk = this.LoadParamFromFile();
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "OverWriteConfFile. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception e)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "OverWriteConfFile. Exception", "", e.Message.ToString());
            }
        }







    }
}
