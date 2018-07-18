using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public class CLNBTN_Lg
    {
        ////////////////////////////////////////////////////
        private bool _bl_OutFileLog = false;     
        private bool _bl_OutLineConsole = false;     
        private bool _bl_OutWindow = false;
        private string _st_User = "CLNBTN_Lg";     
        private string _st_PathErrLog = "C:\\Windows\\CLNBTN_Lg.log";      
        //////////////
        private const String MENSAJE_1 = "EXCEPCIÓN INESPERADA EN EL SISTEMA\n";
        private const String MENSAJE_2 = "Excepción en el sistema";
        private const String MENSAJE_3 = "MENSAJE OPCIONAL DEL LOG\n";
        private const String MENSAJE_4 = "Mensaje Opcional del Seguimiento";
        //
        private Boolean _bl_DateTime_Assigned = false; 
        // 
        private String _st_Line1 = "";
        private String _st_Line2 = "";
        private String _st_Line3 = "";
        private String _st_Line4 = "";
        private String _st_Line5 = "";
        private String _st_Line6 = "";
        private String _st_Line7 = "";
        //
        private String _st_Mess2Window = "";
        //
        Thread Hilo = null;
        private bool _bl_Write2Thread = false;
        //
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Lg";


        public CLNBTN_Lg()
        {
        }


       
        public CLNBTN_Lg(String UserName, String LogFile)
        {
            //
            _st_User = UserName;
            _st_PathErrLog = LogFile;
            this.FileLog_X_Date_Time();
        }


        public CLNBTN_Lg(String UserName, String LogFile, bool OutLineConsole, bool OutFileLog, bool OutWindow)
        {
           
            _st_User = UserName;
            _st_PathErrLog = LogFile;
            _bl_OutFileLog = OutFileLog;
            _bl_OutLineConsole = OutLineConsole;
            _bl_OutWindow = OutWindow;
            //
            this.FileLog_X_Date_Time();
        }

        public CLNBTN_Lg(String UserName, String LogFile, bool OutLineConsole, bool OutFileLog, bool OutWindow, bool Writo2Thread)
        {
           //
            _st_User = UserName;
            _st_PathErrLog = LogFile;
            _bl_OutFileLog = OutFileLog;
            _bl_OutLineConsole = OutLineConsole;
            _bl_OutWindow = OutWindow;
            _bl_Write2Thread = Writo2Thread;
            //
            this.FileLog_X_Date_Time();
        }


        public CLNBTN_Lg(String UserName, String OutLineConsole, bool Writo2Thread)
        {
           //
            _st_User = UserName;
            _st_PathErrLog = OutLineConsole;
            _bl_Write2Thread = Writo2Thread;
            this.FileLog_X_Date_Time();
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
            // Arma el nombre del archivo mas la fecha y la hora,
            // para evitar que se llene el archivo plano del log.
            this.FileLog_X_Date_Time();
            //
        }

        public string getPathErrLog()
        {
            return _st_PathErrLog;
        }


        public void setWrite2Thread(bool Write2Thread)
        {
            _bl_Write2Thread = Write2Thread;
        }

        public bool getWrite2Thread()
        {
            return _bl_Write2Thread;

        }

        public String getMess2Window()
        {
            return _st_Mess2Window;

        }
        


        private void FileLog_X_Date_Time()
        {
            // Cambia el nombre del archivo de log, para colocarle la fecha y la hora.
            String stL_FechaHora = "";
            String stL_File_Name = "";
            String stL_FechaAux = "";
            //
            String stL_Ano = "";
            String stL_Mes = "";
            String stL_Dia = "";
            //
            String stL_Hora = "";
            //
            try
            {
                // si no ha hecho la operacion, procede a cambiar el nombre del .log.
                if (!_bl_DateTime_Assigned)
                { // del if (!blPr_YaAsignoFecha_Hora)
                    _bl_DateTime_Assigned = true;
                    //
                    stL_FechaHora = DateTime.Now.ToString();
                    stL_File_Name = _st_PathErrLog;
                    if (stL_File_Name.Length > 0)
                    { // inicio del if ( stL_File_Name.Length > 0 ) 
                        // cambia la forma de hallar la fecha y la hora
                        // Viene DD/MM/AAAA    
                        stL_Ano = DateTime.Now.Year.ToString(); // stL_FechaHora.Substring(6, 4);
                        stL_Mes = DateTime.Now.Month.ToString(); // stL_FechaHora.Substring(3, 2);
                        stL_Dia = DateTime.Now.Day.ToString(); //stL_FechaHora.Substring(0, 2);
                        //
                        if ((Convert.ToInt32(stL_Mes) <= 9) & (stL_Mes.Length == 1))
                        {
                            stL_Mes = "0" + stL_Mes;
                        }
                        if ((Convert.ToInt32(stL_Dia) <= 9) & (stL_Dia.Length == 1))
                        {
                            stL_Dia = "0" + stL_Dia;
                        }
                        //
                        stL_FechaAux = stL_Ano + stL_Mes + stL_Dia;
                        stL_FechaAux = stL_FechaAux.Trim();
                        // //////////////////////////////////////////
                        // Arma el nuevo nombre del archivo de log
                        // Path y Nombre Archivo Log + _AAAAMMDD_HORA + "." + Extension Archivo 
                        stL_Hora = DateTime.Now.Hour.ToString();
                        if ((Convert.ToInt32(stL_Hora) <= 9) & (stL_Hora.Length == 1))
                        {
                            stL_Hora = "0" + stL_Hora;
                        }
                        /////////////////////////////////////////////////////
                        // Fin ASQC Marzo 14-18 2013
                        /////////////////////////////////////////////////////
                        stL_FechaAux = "_" + stL_FechaAux + "_" + stL_Hora;
                        //
                        stL_File_Name = Path.GetDirectoryName(stL_File_Name) + "\\" + Path.GetFileNameWithoutExtension(stL_File_Name) + stL_FechaAux + Path.GetExtension(stL_File_Name);
                        // Cambia el nombre del archivo de log
                        _st_PathErrLog = stL_File_Name;
                        //
                    } // Fin del if ( stL_File_Name.Length > 0 ) 
                } // del if (!blPr_YaAsignoFecha_Hora)
            } // del try
            catch (System.AccessViolationException ex_0)
            {
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false , true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FileLog_X_Date_Time. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
            }
            catch (Exception ex)
            {
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "FileLog_X_Date_Time", "", ex.Message.ToString(), "", "");
                //
            }
        }


        public void WriteOutErrorMessage(String st_Componente, String st_ClaseMod, String st_Metodo, String st_CodigoErr, String st_MessaDesc, String st_BD = "", String st_InstSQL = "")
        {
            _st_Line1 = "";
            _st_Line2 = "";
            _st_Line3 = "";
            _st_Line4 = "";
            _st_Line5 = "";
            _st_Line6 = "";
            _st_Line7 = "";
            //
            _st_Mess2Window = "";
            //
            try
            { // inicio del try
                ////////////////////////////////////////////////////////////
                // Arma las lineas que va a escribir o presentar
                ////////////////////////////////////////////////////////////
                _st_Line1 = "*******************************************************************************";
                _st_Line2 = DateTime.Now.ToString() + " USUARIO: " + _st_User;
                _st_Line3 = "COMPONENTE: " + st_Componente;
                _st_Line4 = "CLASE O MODULO: " + st_ClaseMod + " METODO: " + st_Metodo;
                if (st_CodigoErr.Length > 0)
                {
                    _st_Line5 = "CODIGO DE ERROR: " + st_CodigoErr;
                }
                //
                if (st_MessaDesc.Length > 0)
                {
                    if (_st_Line5.Length != 0)
                    {
                        _st_Line5 = _st_Line5 + "\n";
                    }
                    _st_Line5 = _st_Line5 + "\n" + "DESCRIPCION DEL ERROR: " + st_MessaDesc;
                }
                if (st_BD.Length > 0)
                {
                    _st_Line6 = "NOMBRE BD: " + st_BD;
                }
                if (st_InstSQL.Length != 0)
                {
                    _st_Line7 = "INSTRUCCION SQL: " + st_InstSQL;
                }
                ////////////////////////////////////////////////////////////
                // Fin de Arma las lineas que va a escribir o presentar
                ////////////////////////////////////////////////////////////
                if (_bl_OutFileLog  == true)
                {
                    // esta parte debe ser hecha con un thread. validando que el archivo este libre para poder escribirlo. 
                    if (_bl_Write2Thread)
                    {
                        Thread Hilo = new Thread(this.Write_2_Log);
                        Hilo.Start();
                    }
                    else
                    {
                        this.Write_2_Log();
                    }
                    //
                }
                if (_bl_OutLineConsole == true)
                {
                    // Escribe Lineas
                    Console.WriteLine(_st_Line1);
                    Console.WriteLine(_st_Line2);
                    Console.WriteLine(_st_Line3);
                    Console.WriteLine(_st_Line4);
                    Console.WriteLine(_st_Line5);
                    if (_st_Line6.Length > 0)
                    {
                        Console.WriteLine(_st_Line6);
                    }
                    if (_st_Line7.Length > 0)
                    {
                        Console.WriteLine(_st_Line7);
                    }
                    //
                    //Console.ReadLine();
                }
                if (_bl_OutWindow == true)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(_st_Line3);
                    sb.Append("\n" + _st_Line4);
                    sb.Append("\n" + _st_Line5);
                    if (_st_Line6.Length > 0)
                    {
                        sb.Append("\n" + _st_Line6);
                    }
                    if (_st_Line7.Length > 0)
                    {
                        sb.Append("\n" + _st_Line7);
                    }
                    string stL_Message = sb.ToString();
                    //
                    _st_Mess2Window = stL_Message;
                    //ClasX_Utils ShowDialog = new ClasX_Utils("", "");
                    //ShowDialog.ShowMessageError(MENSAJE_2, MENSAJE_1, st_Componente, st_ClaseMod, st_Metodo, st_CodigoErr, st_MessaDesc, st_BD, st_InstSQL);
                }
            } // del Try
            catch (System.AccessViolationException ex_0)
            {
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "WriteOutErrorMessage. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
            }
            catch (Exception ex)
            {
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "WriteOutErrorMessage", "", ex.Message.ToString(), "", "");
                //
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void WriteTextInLog(String InfoText) 
        {
            _st_Line1 = "";
            _st_Line2 = "";
            _st_Line3 = "";
            _st_Line4 = "";
            _st_Line5 = "";
            _st_Line6 = "";
            _st_Line7 = "";
            _st_Mess2Window = "";
            try
            {
                _st_Line1 = "*******************************************************************************";
                _st_Line2 = DateTime.Now.ToString();
                _st_Line3 = InfoText;
                //
                if (_bl_OutFileLog == true)
                {
                    // esta parte debe ser hecha con un thread. validando que el archivo este libre para poder escribirlo. 
                    if (_bl_Write2Thread)
                    {
                        Thread Hilo = new Thread(this.Write_2_Log);
                        Hilo.Start();
                    }
                    else
                    {
                        this.Write_2_Log();
                    }
                    //
                }
                //
                if (_bl_OutLineConsole == true)
                {
                    Console.WriteLine(_st_Line1);
                    Console.WriteLine(_st_Line2);
                    Console.WriteLine(_st_Line3);
                    //Console.ReadLine();
                }
                //
                if (_bl_OutWindow == true)
                {
                    _st_Mess2Window = InfoText;
                    //ClasX_Utils ShowDialog = new ClasX_Utils();
                    //ShowDialog.ShowMessage(MENSAJE_3, InfoText, MENSAJE_4);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "WriteTextInLog. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
            }
            catch (Exception ex)
            {
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "WriteTextInLog", "", ex.Message.ToString(), "", "");
                //
            }
        }



        private void Write_2_Log()
        {
            Boolean blL_Flag = false;
            Boolean blL_CanWrite = false;
            //
            try
            {
                if (_st_PathErrLog.Length > 0)
                {
                    do
                    {
                        while (!blL_CanWrite)
                        {
                            blL_CanWrite = this.CanSystemAccessFile();
                            Application.DoEvents();
                        }
                        //
                        if (blL_CanWrite)
                        {
                            //
                            StreamWriter StLEscritor = new StreamWriter(_st_PathErrLog, true, Encoding.ASCII);
                            string SLFecha = DateTime.Now.ToString();
                            StLEscritor.WriteLine(_st_Line1);
                            StLEscritor.WriteLine(_st_Line2);
                            StLEscritor.WriteLine(_st_Line3);
                            if (_st_Line4.Length > 0)
                            {
                                StLEscritor.WriteLine(_st_Line4);
                            }
                            if (_st_Line5.Length > 0)
                            {
                                StLEscritor.WriteLine(_st_Line5);
                            }
                            if (_st_Line6.Length > 0)
                            {
                                StLEscritor.WriteLine(_st_Line6);
                            }
                            if (_st_Line7.Length > 0)
                            {
                                StLEscritor.WriteLine(_st_Line7);
                            }
                            StLEscritor.Close();
                            StLEscritor.Dispose();
                            //
                            blL_Flag = true;
                            if (Hilo != null)
                            {
                                if (!Hilo.IsAlive)
                                {
                                    Hilo.Abort();
                                }
                            }
                        }
                    } while (!blL_Flag);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false, false, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Write_2_Log. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                //
            }
            catch (IOException ex)
            {
                //
                if (!IsSystemFileLocked(ex))
                {
                    CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false, false, false);
                    //
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Write_2_Log. IOException", "", ex.Message.ToString(), "", "");
                }
                //
            }
            catch (Exception ex_1)
            {
                //
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_PathErrLog, false, false, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Write_2_Log. Exception", "", ex_1.Message.ToString(), "", "");
                //
            }
        }


        private Boolean CanSystemAccessFile()
        {
            //////////////////////////////////////////
            var perm = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.Write | System.Security.Permissions.FileIOPermissionAccess.Read, _st_PathErrLog);
            //
            try
            {
                perm.Demand();
                return true;
            }
            catch (System.AccessViolationException ex_0)
            {
                return false;
            }
            catch (IOException e)
            {
                if (IsSystemFileLocked(e)) { return false; } else { return true; }
            }
            catch (Exception ex_1)
            {
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private static bool IsSystemFileLocked(IOException exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }


    }
}
