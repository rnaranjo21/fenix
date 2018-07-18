using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
//
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
// Para PostGreSQL
using Npgsql;
using NpgsqlTypes;
//using Npgsql.Design;
//
using System.Net;
using System.Net.NetworkInformation;
// Para Manejo de Archivos.
using System.IO;
// Para generacion del PDF
using iTextSharp.text;
using iTextSharp.text.pdf;
// Para llamar dlls
using System.Runtime.InteropServices;
// Para validacion del E-Mail
using System.Text.RegularExpressions;
// Para el E-Mail
using System.Net.Mail;
//
using System.Runtime.ExceptionServices;


namespace NBToolsNet
{
    public class CLNBTN_Ul
    {
        // Clase Equivalente : ClasX_Utils			
        //////////////////////////////////////////////////////////////////
        // Clase Para manejo de las operaciones comunes a varias aplicaciones
        // 
        // Autor : Alvaro S. Quimbaya C.
        // Fecha : Septiembre 3 2012.
        // Empresa : Strail SAS
        /// Ult Mod : Alvaro S. Quimbaya C. Julio 18-19 2013
        /// Motivo  : Implementacion de manejos para PostGreSQL
        //////////////////////////////////////////////////////////////////
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        //
        private String _st_User = "CLNBTN_Ul";
        private String _st_FileLog = "C:\\Windows\\CLNBTN_Ul.log";  
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Ul";
        //
        private const int BD_DATE_FORMAT_DD_MM_AAAA = 0;
        private const int BD_DATE_FORMAT_AAAA_MM_DD = 1;
        private const int BD_DATE_FORMAT_MM_DD_AAAA = 2;


        public CLNBTN_Ul(String LicName)
        {
            CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
        }


        public CLNBTN_Ul(String UserName, String LogFile, String LicName)
        {
            CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
            else
            {
                _st_User = UserName;
                _st_FileLog = LogFile;
            }
        }

        public CLNBTN_Ul(String UserName, String LogFile, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
        {
            CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
            else
            {
                _st_User = UserName;
                _st_FileLog = LogFile;
                _bl_OutFileLog = OutFileLog;
                _bl_OutLineConsole = OutLineConsole;
                _bl_OutWindow = OutWindow;
            }
        }

        public bool getOutFileLog()
        {
            return _bl_OutFileLog;
        }


        public bool getOutLineConsole()
        {
            return _bl_OutLineConsole;
        }


        public bool getOutWindow()
        {
            return _bl_OutWindow;
        }

        public string getUser()
        {
            return _st_User;
        }

        public string getFileLog()
        {
            return _st_FileLog;
        }

        public void setOutFileLog(bool OutFileLog)
        {
            _bl_OutFileLog = OutFileLog;
        }

        public void setOutLineConsole(bool OutLineConsole)
        {
            _bl_OutLineConsole = OutLineConsole;
        }

        public void setOutWindow(bool blR_SalDialog)
        {
            _bl_OutWindow = blR_SalDialog;
        }

        public void setUser(string stR_User)
        {
            _st_User = stR_User;
        }

        public void setFileLog(string FileLog)
        {
            _st_FileLog = FileLog;
        }



        [HandleProcessCorruptedStateExceptions]
        public object ConvertNull(object c, object d)
        {
            /// <summary>
            /// Metodo : Convertir_Null
            /// Si el valor C es null, le asigna el valor de D
            /// De lo contrario le deja el mismo valor de C
            /// </summary>
            /// <param name="c">Objeto que entra el cual va a ser convenrtido en el objeto D si es null</param>
            /// <param name="d">Objeto a asignar si el objeto C es NULL</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return d;
                }
                else
                {
                    if (System.Convert.IsDBNull(c))
                    {
                        return d;
                    }
                    else
                    {
                        return c;
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ConvertNull. System.AccessViolationException", "", ex_0.Message.ToString());
                return d;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ConvertNull. Exception", "", ex.Message.ToString());
                return d;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String ConvertDate2Query(CLNBTN_IQy.inDB_Types DataBaseEngine_Type, String DateToConvert, int FormatOfDate = BD_DATE_FORMAT_DD_MM_AAAA)
        {
            String stL_Ano = "";
            String stL_Mes = "";
            String stL_Dia = "";
            String stL_FechaAux = "";
            String stL_Separador = "";
            try
            {
                ///  <summary>
                /// Metodo : st_Date_4_Query
                /// Devuelve la fecha para el formato de los queries
                /// Devuelve la Fecha en Formato AAAAMMDD para SQL Server
                /// para los demas tipos de servidores la devuelve AAAA-MM-DD
                /// </summary>
                /// <param name="DataBaseEngine_Type">Tipo de motor de la base de datos</param>
                /// <param name="DateToConvert">Fecha que se necesita convertir</param>
                /// <param name="FormatOfDate">Formato de la fecha que se maneja en la aplicacion</param>
                /// <returns>La en el formato que la entiene el motor de la base de datos para ejecutar los queries</returns>
                /// 
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    switch (DataBaseEngine_Type)
                    { // inicio del switch (DataBaseEngine_Type)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            //
                            stL_Separador = "";
                            break;
                        default:
                            //
                            stL_Separador = "-";
                            break;
                    } // Fin  del switch (DataBaseEngine_Type)
                    /////////
                    switch (FormatOfDate)
                    {
                        case BD_DATE_FORMAT_DD_MM_AAAA:
                            // Viene DD/MM/AAAA   
                            // Valida si la fecha ya viene en el formato, correcto.
                            if (DataBaseEngine_Type != CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER)
                            {
                                if (DateToConvert.IndexOf("-") == 4)
                                {
                                    // viene en formato AAAA-MM-DD
                                    stL_Ano = DateToConvert.Substring(0, 4);
                                    stL_Mes = DateToConvert.Substring(5, 2);
                                    stL_Dia = DateToConvert.Substring(8, 2);
                                }
                                else
                                {
                                    stL_Ano = DateToConvert.Substring(6, 4);
                                    stL_Mes = DateToConvert.Substring(3, 2);
                                    stL_Dia = DateToConvert.Substring(0, 2);
                                }
                            }
                            else
                            {
                                stL_Ano = DateToConvert.Substring(6, 4);
                                stL_Mes = DateToConvert.Substring(3, 2);
                                stL_Dia = DateToConvert.Substring(0, 2);
                            }
                            break;
                        case BD_DATE_FORMAT_AAAA_MM_DD:
                            // Viene AAAA/MM/DD
                            stL_Ano = DateToConvert.Substring(0, 4);
                            stL_Mes = DateToConvert.Substring(5, 2);
                            stL_Dia = DateToConvert.Substring(8, 2);
                            break;
                        case BD_DATE_FORMAT_MM_DD_AAAA:
                            // MM/DD/AAAA
                            stL_Ano = DateToConvert.Substring(6, 4);
                            stL_Mes = DateToConvert.Substring(0, 2);
                            stL_Dia = DateToConvert.Substring(3, 2);
                            break;
                        default:
                            stL_Ano = DateToConvert.Substring(6, 4);
                            stL_Mes = DateToConvert.Substring(3, 2);
                            stL_Dia = DateToConvert.Substring(0, 2);
                            break;
                    }
                    //
                    if ((Convert.ToInt32(stL_Mes) < 9) & (stL_Mes.Length == 1))
                    {
                        stL_Mes = "0" + stL_Mes;
                    }
                    if ((Convert.ToInt32(stL_Dia) < 9) & (stL_Dia.Length == 1))
                    {
                        stL_Dia = "0" + stL_Dia;
                    }
                    //
                    // Devuelve la Fecha en Formato AAAAMMDD para SQL Server
                    // para los demas tipos de servidores la devuelve AAAA-MM-DD
                    stL_FechaAux = stL_Ano + stL_Separador + stL_Mes + stL_Separador + stL_Dia;
                    stL_FechaAux = stL_FechaAux.Trim();
                    //
                }
                return stL_FechaAux;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ConvertDate2Query. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ConvertDate2Query. Exception", "", ex.Message.ToString());
            }
            return stL_FechaAux;
        }



        [HandleProcessCorruptedStateExceptions]
        public String BringMeServerDate(CLNBTN_IQy Ob_BdInfo, bool Format2YYYYMMDD = false, bool GetHour = false)
        { // Inicio del public String BringMeServerDate(C
            /// <summary>
            /// Metodo : stFechaServidor
            /// Devuelve la fecha del servidor de la base de datos
            /// </summary>
            /// <param name="Ob_BdInfo"></param>
            /// <param name="bRFormatoYYYYMMDD"></param>
            /// <param name="bRConHora"></param>
            /// <returns></returns>
            CLNBTN_Qy Query = null;
            String stL_FechaSale = "";
            String stL_StringAux = "";
            String stL_FchServior = "";
            String stL_HoraServidor = "";
            int inL_Pos = 0;
            String stL_Hora = "";
            int inL_Hora = 0;
            CLNBTN_IQy.inDB_Types in_TipoServidor = CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER;
            //
            String stL_Ano = "";
            String stL_Mes = "";
            String stL_Dia = "";
            //
            String stL_Minutos = "";
            String stL_Segundos = "";
            int inL_number1 = 0;
            String stL_ParteDelaHora = "";
            int inL_ContaDosPuntos = 0;
            //
            int inL_FormatoFecha = BD_DATE_FORMAT_DD_MM_AAAA;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    Query = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    // Hace la conexion
                    Query.setDataBaseInfo(Ob_BdInfo);
                    //
                    in_TipoServidor = Ob_BdInfo.getDataBaseEngine_Type();
                    //
                    switch (in_TipoServidor)
                    { // inicio del switch (in_TipoServidor)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            // SQl Server.
                            // Define DataTable, para los Datos del Query
                            DataTable DatTable = null;
                            Query.ToDo_SELECT("getdate() AS FechaSys");
                            Query.ToDo_EXECUTE_SQL(ref DatTable);
                            if (DatTable != null)
                            { // Inicio del if ( DatTable != null ) 
                                for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                                {
                                    // Toma la informacion de la fila
                                    DataRow Info_Fila = DatTable.Rows[inL_Row];
                                    stL_StringAux = Info_Fila["FechaSys"].ToString();
                                    if (Ob_BdInfo.get_DataBaseConn_Type() == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                                    {
                                        if (stL_StringAux.IndexOf("-") >= 0)
                                        {
                                            // Cambia los "-", por "/"
                                            stL_StringAux = stL_StringAux.Replace("-", "/");
                                        }
                                        inL_FormatoFecha = BD_DATE_FORMAT_AAAA_MM_DD;
                                    }
                                    //
                                    stL_StringAux = String.Format(stL_StringAux, "yyyy/MM/dd HH:Mm:Ss");
                                }
                            } // Fin del if ( DatTable != null ) 
                            Query.ToDo_CLOSE();
                            /////////////////////////////////////////////////////
                            // ASQC Marzo 14-18 2013
                            ////////////////////////////////////////////////////
                            // 
                            stL_FchServior = stL_StringAux.Substring(0, 10);
                            // Valida si la hora tiene el formato : 05/09/2012 03:47:09 p.m.
                            // para devolver la hora en formato militar:
                            // 03:47:09 p.m. = 15:47:09
                            /////////////////////////////////////////////////////////////////
                            // Recorre el string que tiene la hora y la separa en Horas, Minutos y Segundos
                            // Y hace la validacion para colocar la hora en formato Militar.
                            // Halla la hora
                            // Recorre el string de la hora y separa horas, minutos y segundos.
                            stL_ParteDelaHora = stL_StringAux.Substring(11, stL_StringAux.Length - 12);
                            for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                            { // Inicio del for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                                // Toma cada caracter y valida si es numero, para asi separarlos
                                bool canConvert = int.TryParse(stL_ParteDelaHora.Substring(inL_Index1, 1), out inL_number1);
                                if (canConvert == true)
                                { // Inicio del if (canConvert == true
                                    switch (inL_ContaDosPuntos)
                                    {
                                        case 0:
                                            // La hora
                                            stL_Hora += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                            break;
                                        case 1:
                                            // los minutos
                                            stL_Minutos += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                            break;
                                        case 2:
                                            // los segundos
                                            stL_Segundos += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                            break;
                                    }
                                } // fin del if (canConvert == true
                                else // del if (canConvert == true
                                { // del else del if (canConvert == true
                                    // incrementa el contador de los caracteres que no son numericos, como los dos puntos.
                                    inL_ContaDosPuntos = inL_ContaDosPuntos + 1;
                                } // fin del else del if (canConvert == true
                            } // Fin de for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                            ///////////////////////////////////////////
                            inL_Pos = stL_StringAux.IndexOf("p.m.");
                            if (inL_Pos == -1)
                            {
                                stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                            }
                            else
                            {

                                inL_Hora = Convert.ToInt32(stL_Hora);
                                // si la hora es menor a 12
                                // Indica que es por la tarde
                                // a 12 le suma la hora que se tiene
                                // por ejemplo:
                                // hora 03. deberia ser 12 + 3 = 15 las 15 Horas
                                if (inL_Hora < 12)
                                {
                                    inL_Hora = 12 + inL_Hora;
                                    stL_Hora = Convert.ToString(inL_Hora);
                                    stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                                }
                                else
                                {
                                    stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                                }
                            }
                            ////////////////////////////////////////////////////
                            // Fin Marzo 14-18 2013.
                            ////////////////////////////////////////////////////
                            break;
                        ////////////////////////////
                        // ASQC Julio 18-29 2013 Para PostGreSQL
                        ///////////////////////////
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                            // PostGreSQL
                            // Define DataTable, para los Datos del Query
                            DataTable DatTable_1 = null;
                            Query.ToDo_SELECT("current_timestamp AS FechaSys");
                            Query.ToDo_EXECUTE_SQL(ref DatTable_1);
                            if (DatTable_1 != null)
                            { // Inicio del if ( DatTable != null ) 
                                for (int inL_Row = 0; inL_Row < DatTable_1.Rows.Count; inL_Row++)
                                {
                                    // Toma la informacion de la fila
                                    DataRow Info_Fila = DatTable_1.Rows[inL_Row];
                                    stL_StringAux = Info_Fila["FechaSys"].ToString();
                                    stL_StringAux = String.Format(stL_StringAux, "yyyy/MM/dd HH:Mm:Ss");
                                }
                            } // Fin del if ( DatTable != null ) 
                            Query.ToDo_CLOSE();
                            // 
                            stL_FchServior = stL_StringAux.Substring(0, 10);
                            // Valida si la hora tiene el formato : 05/09/2012 03:47:09 p.m.
                            // para devolver la hora en formato militar:
                            // 03:47:09 p.m. = 15:47:09
                            /////////////////////////////////////////////////////////////////
                            // Recorre el string que tiene la hora y la separa en Horas, Minutos y Segundos
                            // Y hace la validacion para colocar la hora en formato Militar.
                            // Halla la hora
                            // Recorre el string de la hora y separa horas, minutos y segundos.
                            stL_ParteDelaHora = stL_StringAux.Substring(11, stL_StringAux.Length - 12);
                            for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                            { // Inicio del for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                                // Toma cada caracter y valida si es numero, para asi separarlos
                                bool canConvert = int.TryParse(stL_ParteDelaHora.Substring(inL_Index1, 1), out inL_number1);
                                if (canConvert == true)
                                { // Inicio del if (canConvert == true
                                    switch (inL_ContaDosPuntos)
                                    {
                                        case 0:
                                            // La hora
                                            stL_Hora += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                            break;
                                        case 1:
                                            // los minutos
                                            stL_Minutos += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                            break;
                                        case 2:
                                            // los segundos
                                            stL_Segundos += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                            break;
                                    }
                                } // fin del if (canConvert == true
                                else // del if (canConvert == true
                                { // del else del if (canConvert == true
                                    // incrementa el contador de los caracteres que no son numericos, como los dos puntos.
                                    inL_ContaDosPuntos = inL_ContaDosPuntos + 1;
                                } // fin del else del if (canConvert == true
                            } // Fin de for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                            ///////////////////////////////////////////
                            inL_Pos = stL_StringAux.IndexOf("p.m.");
                            if (inL_Pos == -1)
                            {
                                stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                            }
                            else
                            {

                                inL_Hora = Convert.ToInt32(stL_Hora);
                                // si la hora es menor a 12
                                // Indica que es por la tarde
                                // a 12 le suma la hora que se tiene
                                // por ejemplo:
                                // hora 03. deberia ser 12 + 3 = 15 las 15 Horas
                                if (inL_Hora < 12)
                                {
                                    inL_Hora = 12 + inL_Hora;
                                    stL_Hora = Convert.ToString(inL_Hora);
                                    stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                                }
                                else
                                {
                                    stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                                }
                            }
                            break;
                        ////////////////////////////
                        // Fin ASQC Julio 18-29 2013 Para PostGreSQL
                        ///////////////////////////
                        default:
                            //
                            stL_StringAux = DateTime.Now.ToString();
                            ////////////////////////////////////////////////////////////////
                            // ASQC Marzo 14 2013.
                            // Toma Año, Mes, Dia, Horas, Minutos, Segundos por aparte, con als funciones del sistema.
                            ///////////////////////////////////////////////////////////////
                            // La fecha 
                            stL_Ano = DateTime.Now.Year.ToString();
                            stL_Mes = DateTime.Now.Month.ToString();
                            stL_Dia = DateTime.Now.Day.ToString();
                            //
                            if ((Convert.ToInt32(stL_Mes) < 9) & (stL_Mes.Length == 1))
                            {
                                stL_Mes = "0" + stL_Mes;
                            }
                            if ((Convert.ToInt32(stL_Dia) < 9) & (stL_Dia.Length == 1))
                            {
                                stL_Dia = "0" + stL_Dia;
                            }
                            //
                            stL_FchServior = stL_Dia + "/" + stL_Mes + "/" + stL_Ano;
                            // La Hora 
                            //
                            stL_Hora = DateTime.Now.Hour.ToString();
                            stL_Minutos = DateTime.Now.Minute.ToString();
                            stL_Segundos = DateTime.Now.Second.ToString();
                            //
                            if ((Convert.ToInt32(stL_Hora) < 9) & (stL_Hora.Length == 1))
                            {
                                stL_Hora = "0" + stL_Hora;
                            }
                            //
                            if ((Convert.ToInt32(stL_Minutos) < 9) & (stL_Minutos.Length == 1))
                            {
                                stL_Minutos = "0" + stL_Minutos;
                            }
                            //
                            if ((Convert.ToInt32(stL_Segundos) < 9) & (stL_Segundos.Length == 1))
                            {
                                stL_Segundos = "0" + stL_Segundos;
                            }
                            //
                            stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                            //
                            ///////////////////////////////////////////////////////////////
                            // Fin ASQC Marzo 14 2013
                            ///////////////////////////////////////////////////////////////
                            break;

                    } // fin del switch(in_TipoServidor)
                    // Devuelve la informacion.
                    if (stL_StringAux.Length > 0)
                    { // del if ( stL_StringAux.Length > 0 ) 
                        //
                        if (Format2YYYYMMDD)
                        {
                            stL_FechaSale = this.ConvertDate2Query(in_TipoServidor, stL_FchServior, inL_FormatoFecha);
                        }
                        else
                        {
                            stL_FechaSale = stL_FchServior;
                        }
                        if (GetHour)
                        {
                            stL_FechaSale = stL_FechaSale + " " + stL_HoraServidor;
                        }
                    } // del if ( stL_StringAux.Length > 0 ) 
                    //
                }
                return stL_FechaSale;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMeServerDate. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMeServerDate. Exception", "", ex.Message.ToString());
            }
            return stL_FechaSale;
        } // fin del public String BringMeServerDate(C




        [HandleProcessCorruptedStateExceptions]
        public void ShowMessage2User(string TitleWindow, string Mess2Show_1, String Mess2Show_2)
        {
            /// <summary>
            /// Metodo : ShowMessage
            /// Presenta una ventana con el error presentado.
            /// </summary>
            /// <param name="st_Titulo">Titulo para la ventana</param>
            /// <param name="st_Message">Mensaje a presentar en la ventana.</param>
            /// <param name="stMensaje2">Mensaje adicional a presentar en la ventana.</param>
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    MessageBox.Show(Mess2Show_1 + Mess2Show_2, TitleWindow, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ShowMessage2User. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ShowMessage2User. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMeWinUserName()
        { // inicio del public void BringMeWinUserName() 
            /// <summary>
            /// Propiedad : get_WindowsUserName
            /// Devuelve el usuario de windows
            /// </summary>
            /// <returns>stL_WinUser = SystemInformation.UserName;</returns>
            String stL_WinUser = "";
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    stL_WinUser = SystemInformation.UserName;
                }
                return stL_WinUser;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMeWinUserName. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMeWinUserName. Exception", "", ex.Message.ToString());
            }
            return stL_WinUser;
        } // fin del public void BringMeWinUserName() 


        [HandleProcessCorruptedStateExceptions]
        public int BringMe_DifDates(String Date2Compare1, String Date2Compare2, String st_TipoDiferencia, int inRFormatoFecha =BD_DATE_FORMAT_DD_MM_AAAA)
        {
            /// <summary>
            /// Metodo : inDiferencia_Fechas
            /// Halla la diferencia en dias, meses o años, entre dos fechas,
            /// </summary>
            /// <param name="Date2Compare1">Fecha 1 A hallar diferencia</param>
            /// <param name="Date2Compare2">Fecha 2 A hallar diferencia</param>
            /// <param name="st_TipoDiferencia">Tipo de Diferencia a hallar: "D" = Dias, "M" = Meses, "A"= Años</param>
            /// <param name="inRFormatoFecha">Formato de la fecha, por defecto = 0 MM/DD/AAAA</param>
            /// <returns></returns>
            // Halla la diferencia entre las dos fechas.
            // URL Referencia : http://msdn.microsoft.com/es-es/library/576yyx3t(v=vs.90).aspx
            // URL Referencia : http://valentingt.blogspot.com/2010/09/c-diferencia-de-fechas-en-anos-meses-y_30.html
            String stL_Ano1 = "";
            String stL_Mes1 = "";
            String stL_Dia1 = "";
            //
            String stL_Ano2 = "";
            String stL_Mes2 = "";
            String stL_Dia2 = "";
            //
            int inL_Diferencia = 0;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    /////////
                    switch (inRFormatoFecha)
                    {
                        case BD_DATE_FORMAT_DD_MM_AAAA:
                            // Viene DD/MM/AAAA    
                            stL_Ano1 = Date2Compare1.Substring(6, 4);
                            stL_Mes1 = Date2Compare1.Substring(3, 2);
                            stL_Dia1 = Date2Compare1.Substring(0, 2);
                            //
                            stL_Ano2 = Date2Compare2.Substring(6, 4);
                            stL_Mes2 = Date2Compare2.Substring(3, 2);
                            stL_Dia2 = Date2Compare2.Substring(0, 2);
                            break;
                        case BD_DATE_FORMAT_AAAA_MM_DD:
                            // Viene AAAA/MM/DD
                            stL_Ano1 = Date2Compare1.Substring(0, 4);
                            stL_Mes1 = Date2Compare1.Substring(5, 2);
                            stL_Dia1 = Date2Compare1.Substring(8, 2);
                            //
                            stL_Ano2 = Date2Compare2.Substring(0, 4);
                            stL_Mes2 = Date2Compare2.Substring(5, 2);
                            stL_Dia2 = Date2Compare2.Substring(8, 2);
                            break;
                        case BD_DATE_FORMAT_MM_DD_AAAA:
                            // MM/DD/AAAA
                            stL_Ano1 = Date2Compare1.Substring(6, 4);
                            stL_Mes1 = Date2Compare1.Substring(0, 2);
                            stL_Dia1 = Date2Compare1.Substring(3, 2);
                            //
                            stL_Ano2 = Date2Compare2.Substring(6, 4);
                            stL_Mes2 = Date2Compare2.Substring(0, 2);
                            stL_Dia2 = Date2Compare2.Substring(3, 2);
                            break;
                        default:
                            stL_Ano1 = Date2Compare1.Substring(6, 4);
                            stL_Mes1 = Date2Compare1.Substring(3, 2);
                            stL_Dia1 = Date2Compare1.Substring(0, 2);
                            break;
                    }
                    //
                    DateTime DateFecha1 = new DateTime(Convert.ToInt32(stL_Ano1), Convert.ToInt32(stL_Mes1), Convert.ToInt32(stL_Dia1));
                    DateTime DateFecha2 = new DateTime(Convert.ToInt32(stL_Ano2), Convert.ToInt32(stL_Mes2), Convert.ToInt32(stL_Dia2));
                    switch (st_TipoDiferencia)
                    {
                        case "D":
                            // Diferencia en dias
                            //inL_Diferencia = ( DateFecha1.Day - DateFecha2.Day) ;
                            // Difference in days, hours, and minutes.
                            TimeSpan ts = DateFecha1 - DateFecha2;
                            inL_Diferencia = ts.Days;
                            break;
                        case "M":
                            // diferencia en Meses
                            inL_Diferencia = (DateFecha1.Month - DateFecha2.Month);
                            if ((DateFecha1.Year - DateFecha2.Year) != 0)
                            {
                                inL_Diferencia = inL_Diferencia + 1;
                            }
                            break;
                        case "A":
                            // diferencia en Años
                            inL_Diferencia = (DateFecha1.Year - DateFecha2.Year);
                            break;
                        default:
                            // En dias
                            break;
                    }
                    //
                }
                return inL_Diferencia;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                inL_Diferencia = 0;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_DifDates. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                inL_Diferencia = 0;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_DifDates. Exception", "", ex.Message.ToString());
            }
            return inL_Diferencia;
        }


        [HandleProcessCorruptedStateExceptions]
        public String GetOff_SpacesAdic(String String2Manipulate)
        {
            /// <summary>
            /// Metodo : QuitaEspaciosAdicionales
            /// Quita Espacios adicionales de la cadena que entra como parametro
            /// </summary>
            /// <param name="st_Cadena">Cadena a la cual se le va a quitar los espacios adicionales</param>
            /// <returns>Devuelve la cadena sin los espacios adicionales</returns>
            // quita espacios adicionales
            String stL_CadenaFuera = "";
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return stL_CadenaFuera;
                }
                else
                {
                    for (int inL_Index = 0; inL_Index < String2Manipulate.Length; inL_Index++)
                    {
                        if (String2Manipulate.Substring(inL_Index, 1) != " ")
                        {
                            stL_CadenaFuera = stL_CadenaFuera + String2Manipulate.Substring(inL_Index, 1);
                        }
                    }
                }
                return stL_CadenaFuera;
            }
            catch (System.AccessViolationException ex_0)
            {
                stL_CadenaFuera = "";
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetOff_SpacesAdic. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                stL_CadenaFuera = "";
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetOff_SpacesAdic. Exception", "", ex.Message.ToString());
            }
            return stL_CadenaFuera;
        }


        [HandleProcessCorruptedStateExceptions]
        public String GetOff_CharFromString(String String2Manipulate, String String2GetOff)
        {
            /// <summary>
            /// Metodo : QuitaCaracterDeLaCadena
            /// Quita un caracter de una cadena.
            /// Quita todas las apariciones de la cadena que entra como parametro, st_CaracterFuera.
            /// </summary>
            /// <param name="st_Cadena">String sobre la cual se va a eliminar el caracter</param>
            /// <param name="st_CaracterFuera">El caracter que se debe eliminar.</param>
            /// <returns>Devuelve la cadena sin el caracter</returns>
            // quita espacios adicionales
            String stL_CadenaFuera = "";
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return stL_CadenaFuera;
                }
                else
                {
                    for (int inL_Index = 0; inL_Index < String2Manipulate.Length; inL_Index++)
                    {
                        if (String2Manipulate.Substring(inL_Index, 1) != String2GetOff)
                        {
                            stL_CadenaFuera = stL_CadenaFuera + String2Manipulate.Substring(inL_Index, 1);
                        }
                    }
                }
                return stL_CadenaFuera;
            }
            catch (System.AccessViolationException ex_0)
            {
                stL_CadenaFuera = "";
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetOff_CharFromString. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                stL_CadenaFuera = "";
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "GetOff_CharFromString. Exception", "", ex.Message.ToString());
            }
            return stL_CadenaFuera;
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMe_CurrencyFormat(String String2Manipulate, Boolean WithDollarSymbol = false)
        {
            /// <summary>
            /// Metodo : stFormatea_A_Moneda
            /// Da formato de numero o moneda  un string con un valor.
            /// Ejemplo : 
            /// st_Valor = "14180765.38"
            /// Se le cambia el punto por la coma :
            /// st_Valor = "14180765,38"
            /// Se convierte a Decimal :
            /// dlL_repNumerica = 14180765.38
            /// Se formatea Con signo Dollar:
            /// stL_SalidaMoneda = "$14,180,765.38"
            /// Se formatea Sin Signo Dollar:
            /// stL_SalidaMoneda = "14,180,765.38"
            /// </summary>
            /// <param name="st_Valor">Valor a formatear</param>
            /// <param name="bl_RConSimboloDollar">True = Le coloca signo dollar ( $ ) al string de salida.</param>
            /// <returns></returns>
            Double dlL_repNumerica = 0;
            String stL_SalidaMoneda = "";
            bool blL_CanConvert = false;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ////////////////////////////////////////////
                    // Si el valor viene con puntos para el separador decimal, lo pasa a "," ( coma ) para 
                    // que la conversion a double, lo convierta con las posiciones decimales
                    // Ejemplo : 
                    // st_Valor = "14180765.38"
                    // Se le cambia el punto por la coma :
                    // st_Valor = "14180765,38"
                    // Se convierte a Decima :
                    // dlL_repNumerica = 14180765.38
                    // Se formatea Con signo Dollar:
                    // stL_SalidaMoneda = "$14,180,765.38"
                    // Se formatea Sin Signo Dollar:
                    // stL_SalidaMoneda = "14,180,765.38"
                    ////////////////////////////////////////////
                    if (String2Manipulate.Length > 0)
                    { // del if (st_Valor.Length > 0)
                        // URL Referencia : http://msdn.microsoft.com/es-es/library/bb384043.aspx
                        // Averigua si es un numero la cadena
                        blL_CanConvert = double.TryParse(String2Manipulate, out dlL_repNumerica);
                        if (blL_CanConvert)
                        {
                            String2Manipulate = String2Manipulate.Replace(".", ",");
                            dlL_repNumerica = Convert.ToDouble(String2Manipulate);
                            // Si tiene que colocar el simbolo de Pesos o Dollar, lo coloca.
                            if (WithDollarSymbol)
                            {
                                stL_SalidaMoneda = (dlL_repNumerica.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
                            }
                            else
                            {
                                stL_SalidaMoneda = (dlL_repNumerica.ToString("N", CultureInfo.CreateSpecificCulture("en-US")));
                            }

                        }
                    } // if (st_Valor.Length > 0)
                }
                return stL_SalidaMoneda;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_CurrencyFormat. System.AccessViolationException", "", ex_0.Message.ToString());
                return "";
            }
            catch (Exception ex)
            {

                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_CurrencyFormat. Exception", "", ex.Message.ToString());
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMe_CoinFormat(String String2Manipulate)
        {
            /// <summary>
            /// Metodo : st_aMoneda
            /// Formatea un string a Moneda.
            /// </summary>
            /// <param name="st_CadenaNumero">String a Formatear</param>
            /// <returns> Devuelve el campo formateado en Moneda.</returns>
            Double dlL_repNumerica = 0;
            String stL_SalidaMoneda = "";
            bool blL_CanConvert = false;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (String2Manipulate.Length > 0)
                    {
                        // URL Referencia : http://msdn.microsoft.com/es-es/library/bb384043.aspx
                        // Averigua si es un numero la cadena
                        blL_CanConvert = double.TryParse(String2Manipulate, out dlL_repNumerica);
                        if (blL_CanConvert)
                        {
                            String2Manipulate = String2Manipulate.Replace(".", ",");
                            dlL_repNumerica = Convert.ToDouble(String2Manipulate);
                            stL_SalidaMoneda = (dlL_repNumerica.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
                        }
                    }
                }
                return stL_SalidaMoneda;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_CurrencyFormat. System.AccessViolationException", "", ex_0.Message.ToString());
                return "";
            }
            catch (Exception ex)
            {

                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_CurrencyFormat. Exception", "", ex.Message.ToString());
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMe_CompleteText(String String2Manipulate, int MaxLengh = 40, String String2Separate = ":")
        {
            /// <summary>
            /// Metodo : st_completaTexto
            /// Sobre Carga 1
            /// justificar hacia la derecha un texto
            ///  Ejemplo:
            ///  Texto de Entrada : xxdffuou880:80 
            ///  Texto de Salida  : xxdffuou880:                           80
            /// </summary>
            /// <param name="st_texto">Texto a completar</param>
            /// <param name="in_MaxLongitud">Longitud Maxima con la cual va a trabajar. Por defecto 40</param>
            /// <param name="st_CaracterSeparacion">Caracter de separacion. Por defecto ":"</param>
            /// <returns>Devuelve el texto en el formato requerido</returns>
            String stL_textosalida = "";
            String[] stL_textoArreglo;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    stL_textoArreglo = String2Manipulate.Split(Convert.ToChar(String2Separate.Substring(0, 1)));
                    if (stL_textoArreglo.Length > 1)
                    {
                        int longitud = stL_textoArreglo[0].Length + stL_textoArreglo[1].Length;
                        if (longitud < MaxLengh)
                        {
                            String puntos = String2Separate;
                            for (int i = longitud; i < MaxLengh; i++)
                            {
                                puntos += " ";
                            }
                            stL_textosalida = stL_textoArreglo[0] + puntos + stL_textoArreglo[1];
                        }
                        else
                        {
                            stL_textosalida = stL_textoArreglo[0] + String2Separate + stL_textoArreglo[1];
                        }
                    }
                    else
                    {
                        stL_textosalida = String2Manipulate;
                    }
                }
                return stL_textosalida;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_CompleteText. System.AccessViolationException", "", ex_0.Message.ToString());
                return "";
            }
            catch (Exception ex)
            {

                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_CompleteText. Exception", "", ex.Message.ToString());
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMe_CompleteText(String String2Manipulate_1, String String2Manipulate_2, int MaxLengh = 40)
        {
            /// <summary>
            /// Metodo : st_completaTexto
            /// Sobre Carga 2.
            /// justificar hacia la derecha dos textos
            /// Si la longitud del texto 2, lo manda en una nueva linea.
            /// Ejemplo :
            /// st_texto1 : xxdffuou880:80
            /// st_texto2 : cxvcxvjsfjsldfjsldskjflsdjf99
            /// Salida : 
            /// xxdffuou880:80
            /// cxvcxvjsfjsldfjsldskjflsdjf99
            /// </summary>
            /// <param name="st_texto1">Primera Parte del Texto</param>
            /// <param name="st_texto2">Segunda Parte del texto</param>
            /// <param name="in_MaxLongitud">Longitud Maxima con la cual va a trabajar. Por defecto 40</param>
            /// <returns>Devuelve el string de acuerdo al formato requerido</returns>
            String textosalida = String2Manipulate_1 + " " + String2Manipulate_2;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    int longitud = (String2Manipulate_1 + " " + String2Manipulate_2).Length;
                    if ((String2Manipulate_1 + " " + String2Manipulate_2).Length < MaxLengh)
                    {
                        String puntos = "";
                        for (int i = longitud; i < MaxLengh; i++)
                        {
                            puntos += " ";
                        }
                        textosalida = String2Manipulate_1 + puntos + String2Manipulate_2;
                    }
                    if ((String2Manipulate_1 + " " + String2Manipulate_2).Length > MaxLengh)
                    {
                        return String2Manipulate_1 + "\n" + String2Manipulate_2;
                    }
                }
                return textosalida;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_CompleteText(2). System.AccessViolationException", "", ex_0.Message.ToString());
                return "";
            }
            catch (Exception ex)
            {

                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_CompleteText(2). Exception", "", ex.Message.ToString());
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMe_LenghAjusted(String String2Manipulate, int MaxLengh = 40)
        {
            /// <summary>
            /// Metodo : st_ajustarLongitud
            /// Ajusta un texto a una longitud dada.
            /// da salto de linea si el texto es mas largo que la longitud
            /// </summary>
            /// <param name="st_texto">Texto a Ajustar</param>
            /// <param name="in_MaxLongitud">Longitud Maxima con la cual va a trabajar. Por defecto 40</param>
            /// <returns>Devuelve el string en el formato requerido</returns>
            String stL_texto = String2Manipulate;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    String[] partes = stL_texto.Split(new char[] { ' ' });
                    stL_texto = partes[0];
                    int cont = 0;
                    for (int i = 1; i < partes.Length; i++)
                    {
                        cont = i + 1;
                        if ((stL_texto + partes[i]).Length < MaxLengh)
                        {
                            stL_texto += " " + partes[i];
                        }
                        else
                        {
                            stL_texto += "\n    " + partes[i];
                            break;
                        }
                    }
                    if (cont == partes.Length) return stL_texto;
                    for (int i = cont; i < partes.Length; i++)
                    {
                        stL_texto += " " + partes[i];
                    }
                }
                return stL_texto;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_LenghAjusted. System.AccessViolationException", "", ex_0.Message.ToString());
                return stL_texto;
            }
            catch (Exception ex)
            {

                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_LenghAjusted. Exception", "", ex.Message.ToString());
                return stL_texto;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String GetOff_Acent(String String2Manipulate)
        {
            /// <summary>
            /// Metodo : st_EliminaAcento
            /// Elimina los acentos de la cadena.
            /// </summary>
            /// <param name="st_cadena">Cadena a la cual se eliminan los acentos</param>
            /// <returns>Devuelve la cadena sin acentos</returns>
            String stL_retorno = String2Manipulate;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (String.IsNullOrEmpty(String2Manipulate)) return stL_retorno;
                    byte[] btL_tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(stL_retorno);
                    stL_retorno = System.Text.Encoding.UTF8.GetString(btL_tempBytes);
                }
                return stL_retorno;
            }
            catch (System.AccessViolationException ex_0)
            {
                stL_retorno = String2Manipulate;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_LenghAjusted. System.AccessViolationException", "", ex_0.Message.ToString());
                return stL_retorno;
            }
            catch (Exception ex)
            {
                stL_retorno = String2Manipulate;
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_LenghAjusted. Exception", "", ex.Message.ToString());
                return stL_retorno;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void BringMe_MachineName_IpAdd(ref String MachineName, ref String MachineNameIP)
        { // inicio del public void Halla_Nombre_IP_Maquina(
            /// <summary>
            /// Metodo : Halla_Nombre_IP_Maquina
            /// Halla el nombre y la direccion IP de la maquina.
            /// </summary>
            /// <param name="st_Maquina">Devuelve el nombre de la maquina</param>
            /// <param name="st_IPAdress">Devuelve la Ip de la maquina</param>
            // Halla el nombre de la maquina y la IP de la maquina.
            try
            {
                MachineName = "";
                MachineNameIP = "";
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    MachineName = Dns.GetHostName();
                    //
                    IPHostEntry host;
                    //
                    host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress ip in host.AddressList)
                    {
                        if (ip.AddressFamily.ToString() == "InterNetwork")
                        {
                            MachineNameIP = ip.ToString();
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_MachineName_IpAdd. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_MachineName_IpAdd. Exception", "", ex.Message.ToString());
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void SetComboBox_Conf(ComboBox ComboBoxControl, CLNBTN_IQy ObBd_Inf, ref String String2Conn, String Db_TableName, String TableFieldCod1, String TableFieldDesc1, String TableFieldCod2 = "", String TableFieldDesc2 = "", String Filter2Applicate = "", String Sql_Stmt = "")
        {
            /// <summary>
            /// Metodo : Config_ComboBox
            /// Encargado de Configurar un ComboBox, para trabajar con la base de datos
            /// Dependiendo de la tabla y query que se envia como parametro.
            /// </summary>
            /// <param name="ComboBoxControl">Control Combox a Configurar</param>
            /// <param name="ObBd_Inf">Objeto con la informacion de la base de datos con la cual va a trabajar.</param>
            /// <param name="String2Conn">String de Conexion para el ComboBox</param>
            /// <param name="Db_TableName">Nombre de la tabla sobre la cual va a hacer la seccion</param>
            /// <param name="TableFieldCod1">Campo Codigo para hacer la seleccion</param>
            /// <param name="TableFieldDesc1">Campo Descripcion a presentar en el combo</param>
            /// <param name="TableFieldCod2">Campo Codigo 2 para hacer la seleccion</param>
            /// <param name="TableFieldDesc2">Campo Descripcion 2 a presentar en el combo</param>
            /// <param name="Filter2Applicate">Condicion para la seleccion de los datos</param>
            /// <param name="Sql_Stmt">Instruccion SQL que se ejecuta directamente</param>
            // Se encarga de configurar un combobox
            // con la informacion de la base de datos, la tabla y los campos a presentar.
            // URL Referencia : http://solocodigo.com/42469/cargar-tabla-en-combobox/
            String stL_Sql = "";
            String stL_ConnectString = "";
            //
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    stL_Sql = "";
                    if (Sql_Stmt.Length == 0)
                    {
                        stL_Sql = "SELECT " + TableFieldCod1 + " , " + TableFieldDesc1;
                        if (TableFieldCod2.Length > 0)
                        {
                            stL_Sql += " , " + TableFieldCod2;
                        }
                        if (TableFieldDesc2.Length > 0)
                        {
                            stL_Sql += " , " + TableFieldDesc2;
                        }
                        stL_Sql += " FROM " + Db_TableName;
                        if (Filter2Applicate.Length > 0)
                        {
                            stL_Sql += " WHERE " + Filter2Applicate;
                        }
                        stL_Sql += " ORDER BY " + TableFieldDesc1;
                    }
                    else
                    {
                        stL_Sql = Sql_Stmt;
                    }
                    stL_ConnectString = "";
                    if (String2Conn.Length == 0)
                    {
                        CLNBTN_Qy Obj_Query = new CLNBTN_Qy(_st_User, _st_FileLog, _st_Lic);
                        //
                        Obj_Query.setDataBaseInfo(ObBd_Inf);
                        Obj_Query.ConnectDataBase();
                        //
                        stL_ConnectString = Obj_Query.getConnString4Grid();
                        // Devuelve el conection string, por si se necesita en otros combos en la misma forma.
                        String2Conn = stL_ConnectString;
                        //
                    }
                    else
                    {
                        stL_ConnectString = String2Conn;
                    }
                    // 
                    // Valida si la conexion es via ConectorSQL
                    if (ObBd_Inf.get_DataBaseConn_Type() == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                    { // Inicio del if (ObBd_Inf.getTipoConexion() == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                        //
                        CLNBTN_Qy Obj_Query1 = new CLNBTN_Qy(_st_User, _st_FileLog, _st_Lic);
                        //
                        Obj_Query1.setDataBaseInfo(ObBd_Inf);
                        DataTable DatTable = null;
                        //
                        Obj_Query1.ToDo_SELECT("*");
                        Obj_Query1.ToDo_EXECUTE_SQL(ref DatTable, stL_Sql);
                        if (DatTable != null)
                        {
                            //
                            ComboBoxControl.ValueMember = TableFieldCod1;
                            ComboBoxControl.DisplayMember = TableFieldDesc1;
                            ComboBoxControl.DataSource = DatTable;
                            //
                        }
                        //
                    } // Fin del Inicio del if (ObBd_Inf.getTipoConexion() == ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                    else // del Inicio del if (ObBd_Inf.getTipoConexion() == ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                    { // Inicio del ELSE del if (ObBd_Inf.getTipoConexion() == ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                        // Dependiendo del tipo de motor de la base de datos.
                        switch (ObBd_Inf.getDataBaseEngine_Type())
                        { // Inicio del switch (ObBd_Inf.getTipoBD())
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                                //
                                using (SqlConnection sc = new SqlConnection())
                                {
                                    sc.ConnectionString = stL_ConnectString;
                                    sc.Open();
                                    using (SqlDataAdapter sda = new SqlDataAdapter(stL_Sql, sc))
                                    {
                                        DataTable dt = new DataTable();
                                        sda.Fill(dt);
                                        ComboBoxControl.ValueMember = TableFieldCod1;
                                        ComboBoxControl.DisplayMember = TableFieldDesc1;
                                        ComboBoxControl.DataSource = dt;
                                    }
                                }
                                break;
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                                //
                                using (MySqlConnection sc = new MySqlConnection())
                                {
                                    sc.ConnectionString = stL_ConnectString;
                                    sc.Open();
                                    using (MySqlDataAdapter sda = new MySqlDataAdapter(stL_Sql, sc))
                                    {
                                        DataTable dt = new DataTable();
                                        sda.Fill(dt);
                                        ComboBoxControl.ValueMember = TableFieldCod1;
                                        ComboBoxControl.DisplayMember = TableFieldDesc1;
                                        ComboBoxControl.DataSource = dt;
                                    }
                                }
                                break;
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                                /// PostGreSQL
                                /// 
                                using (NpgsqlConnection sc = new NpgsqlConnection())
                                {
                                    sc.ConnectionString = stL_ConnectString;
                                    sc.Open();
                                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter(stL_Sql, sc))
                                    {
                                        DataTable dt = new DataTable();
                                        sda.Fill(dt);
                                        ComboBoxControl.ValueMember = TableFieldCod1;
                                        ComboBoxControl.DisplayMember = TableFieldDesc1;
                                        ComboBoxControl.DataSource = dt;
                                    }
                                }
                                break;
                            default:
                                //
                                break;
                        } // Fin del switch (ObBd_Inf.getTipoBD())
                    } // Fin del ELSE del if (ObBd_Inf.getTipoConexion() == ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetComboBox_Conf. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetComboBox_Conf. Exception", "", ex.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void SetComboBox_Conf(ref DataTable DatTable, ComboBox ComboBoxControl, CLNBTN_IQy ObBd_Inf, ref String String2Conn, String Db_TableName, String TableFieldCod1, String TableFieldDesc1, String TableFieldCod2 = "", String TableFieldDesc2 = "", String Filter2Applicate = "", String Sql_Stmt = "")
        {
            /// <summary>
            /// Metodo : Config_ComboBox
            /// Sobre Carga 2 
            /// Encargado de Configurar un ComboBox, para trabajar con la base de datos
            /// Dependiendo de la tabla y query que se envia como parametro.
            /// </summary>
            /// <param name="DatTable">Devuelve el Data Table,DataTable , con los datos del query</param>
            /// <param name="ComboBoxControl">Control Combox a Configurar</param>
            /// <param name="ObBd_Inf">Objeto con la informacion de la base de datos con la cual va a trabajar.</param>
            /// <param name="String2Conn">String de Conexion para el ComboBox</param>
            /// <param name="Db_TableName">Nombre de la tabla sobre la cual va a hacer la seccion</param>
            /// <param name="TableFieldCod1">Campo Codigo para hacer la seleccion</param>
            /// <param name="TableFieldDesc1">Campo Descripcion a presentar en el combo</param>
            /// <param name="TableFieldCod2">Campo Codigo 2 para hacer la seleccion</param>
            /// <param name="TableFieldDesc2">Campo Descripcion 2 a presentar en el combo</param>
            /// <param name="Filter2Applicate">Condicion para la seleccion de los datos</param>
            /// <param name="Sql_Stmt">Instruccion SQL que se ejecuta directamente</param>
            // Se encarga de configurar un combobox
            // con la informacion de la base de datos, la tabla y los campos a presentar.
            // URL Referencia : http://solocodigo.com/42469/cargar-tabla-en-combobox/
            String stL_Sql = "";
            String stL_ConnectString = "";
            //
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    stL_Sql = "";
                    if (Sql_Stmt.Length == 0)
                    {
                        stL_Sql = "SELECT " + TableFieldCod1 + " , " + TableFieldDesc1;
                        if (TableFieldCod2.Length > 0)
                        {
                            stL_Sql += " , " + TableFieldCod2;
                        }
                        if (TableFieldDesc2.Length > 0)
                        {
                            stL_Sql += " , " + TableFieldDesc2;
                        }
                        stL_Sql += " FROM " + Db_TableName;
                        if (Filter2Applicate.Length > 0)
                        {
                            stL_Sql += " WHERE " + Filter2Applicate;
                        }
                        stL_Sql += " ORDER BY " + TableFieldDesc1;
                    }
                    else
                    {
                        stL_Sql = Sql_Stmt;
                    }
                    stL_ConnectString = "";
                    if (String2Conn.Length == 0)
                    {
                        CLNBTN_Qy Obj_Query = new CLNBTN_Qy(_st_User, _st_FileLog, _st_Lic);
                        //
                        Obj_Query.setDataBaseInfo(ObBd_Inf);
                        Obj_Query.ConnectDataBase();
                        //
                        stL_ConnectString = Obj_Query.getConnString4Grid();
                        // Devuelve el conection string, por si se necesita en otros combos en la misma forma.
                        String2Conn = stL_ConnectString;
                        //
                    }
                    else
                    {
                        stL_ConnectString = String2Conn;
                    }
                    // 
                    // Valida si la conexion es via ConectorSQL
                    if (ObBd_Inf.get_DataBaseConn_Type() == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                    { // Inicio del if (ObBd_Inf.getTipoConexion() == CLNBTN_IQy.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                        //
                        CLNBTN_Qy Obj_Query1 = new CLNBTN_Qy(_st_User, _st_FileLog, _st_Lic);
                        Obj_Query1.setDataBaseInfo(ObBd_Inf);
                        DatTable = null;
                        //
                        Obj_Query1.ToDo_SELECT("*");
                        Obj_Query1.ToDo_EXECUTE_SQL(ref DatTable, stL_Sql);
                        if (DatTable != null)
                        {
                            //
                            ComboBoxControl.ValueMember = TableFieldCod1;
                            ComboBoxControl.DisplayMember = TableFieldDesc1;
                            ComboBoxControl.DataSource = DatTable;
                            //
                        }
                        //
                    } // Fin del Inicio del if (ObBd_Inf.getTipoConexion() == ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                    else // del Inicio del if (ObBd_Inf.getTipoConexion() == ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                    { // Inicio del ELSE del if (ObBd_Inf.getTipoConexion() == ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                        // Dependiendo del tipo de motor de la base de datos.
                        switch (ObBd_Inf.getDataBaseEngine_Type())
                        { // Inicio del switch (ObBd_Inf.getTipoBD())
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                                //
                                using (SqlConnection sc = new SqlConnection())
                                {
                                    sc.ConnectionString = stL_ConnectString;
                                    sc.Open();
                                    using (SqlDataAdapter sda = new SqlDataAdapter(stL_Sql, sc))
                                    {
                                        DataTable dt = new DataTable();
                                        sda.Fill(dt);
                                        ComboBoxControl.ValueMember = TableFieldCod1;
                                        ComboBoxControl.DisplayMember = TableFieldDesc1;
                                        ComboBoxControl.DataSource = dt;
                                        DatTable = dt;
                                    }
                                }
                                break;
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_MYSQL:
                                //
                                using (MySqlConnection sc = new MySqlConnection())
                                {
                                    sc.ConnectionString = stL_ConnectString;
                                    sc.Open();
                                    using (MySqlDataAdapter sda = new MySqlDataAdapter(stL_Sql, sc))
                                    {
                                        DataTable dt = new DataTable();
                                        sda.Fill(dt);
                                        ComboBoxControl.ValueMember = TableFieldCod1;
                                        ComboBoxControl.DisplayMember = TableFieldDesc1;
                                        ComboBoxControl.DataSource = dt;
                                        DatTable = dt;
                                    }
                                }
                                break;
                            case CLNBTN_IQy.inDB_Types.DB_TYPE_POSTGRESQL:
                                /// PostGreSQL
                                /// 
                                using (NpgsqlConnection sc = new NpgsqlConnection())
                                {
                                    sc.ConnectionString = stL_ConnectString;
                                    sc.Open();
                                    using (NpgsqlDataAdapter sda = new NpgsqlDataAdapter(stL_Sql, sc))
                                    {
                                        DataTable dt = new DataTable();
                                        sda.Fill(dt);
                                        ComboBoxControl.ValueMember = TableFieldCod1;
                                        ComboBoxControl.DisplayMember = TableFieldDesc1;
                                        ComboBoxControl.DataSource = dt;
                                        DatTable = dt;
                                    }
                                }
                                break;
                            default:
                                //
                                break;
                        } // Fin del switch (ObBd_Inf.getTipoBD())
                    } // Fin del ELSE del if (ObBd_Inf.getTipoConexion() == ClasX_DBInfo.inConnect_Type.TYPE_5_CONNECT_MONITOR_TRAN)
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetComboBox_Conf(2). System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetComboBox_Conf(2). Exception", "", ex.Message.ToString());
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public void SetCleanValuesControlForm(Control WindowForm, Boolean CleanComboBoxes, Boolean CleanDataFields, String ValueOfDate, string[] Array_Exc)
        {
            /// <summary>
            /// Metodo : Limpia_Controles_WindowForm
            ///  Limpia el contenido de los controles de una WindowForm
            /// Aplica para controles tipo:
            /// TexBox       : TextBox
            /// CombosBox    : ComboBox
            /// Campos Fecha : DateTimePicker
            /// </summary>
            /// <param name="WindowForm">WindowForm o Control Contenedor a los cuales se les va a limpiar los controles</param>
            /// <param name="CleanComboBoxes">True = Limpia los ComboBox</param>
            /// <param name="CleanDataFields">True = Limpia los Campos Fecha</param>
            /// <param name="ValueOfDate">Valor a asignar a los campos fecha</param>
            /// <param name="Array_Exc">Array con el nombre de los controles a los cuales no se les va a limpiar el valor</param>
            // Limpia el contenido de los controles de una WindowForm
            // Aplica para controles tipo:
            // TexBox       : TextBox
            // CombosBox    : ComboBox
            // Campos Fecha : DateTimePicker
            String stL_NombreControl = "";
            Boolean blL_CampoExcluido = false;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    foreach (Control ControlWindowForm in WindowForm.Controls)
                    { // del foreach (Control ControlWindowForm
                        if (ControlWindowForm.HasChildren)
                        { // del if (ControlHijo.HasChildren)
                            foreach (Control ControlHijo in ControlWindowForm.Controls)
                            { // del foreach (Control ControlHijo
                                stL_NombreControl = "";
                                stL_NombreControl = ControlHijo.Name;
                                if (ControlHijo is TextBox)
                                { // Inicio del if (ControlHijo is TextBox)
                                    // Valida si esta en los campos Excluidos.
                                    blL_CampoExcluido = false;
                                    for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                    { // Inicio del  for ( inL_Index = 0 ;
                                        if (Array_Exc[inL_Index].Length > 0)
                                        { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                            {
                                                blL_CampoExcluido = true;
                                                break;
                                            }
                                        } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        else
                                        {
                                            break;
                                        }
                                    } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                    if (!blL_CampoExcluido)
                                    {
                                        ControlHijo.Text = "";
                                    }
                                } // Fin de if (ControlHijo is TextBox)
                            } // del foreach (Control ControlHijo
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los combobox
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            if (CleanComboBoxes)
                            { // if (CleanComboBoxes)
                                foreach (ComboBox ItemCombo in ControlWindowForm.Controls.OfType<ComboBox>())
                                { // inicio del  foreach (  ComboBox ItemCombo 
                                    stL_NombreControl = "";
                                    stL_NombreControl = ItemCombo.Name;
                                    // Valida si esta en los campos Excluidos.
                                    blL_CampoExcluido = false;
                                    for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                    { // Inicio del  for ( inL_Index = 0 ;
                                        if (Array_Exc[inL_Index].Length > 0)
                                        { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                            {
                                                blL_CampoExcluido = true;
                                                break;
                                            }
                                        } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        else
                                        {
                                            break;
                                        }
                                    } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                    if (!blL_CampoExcluido)
                                    {
                                        // Deja el primer valor del combobox
                                        if (ItemCombo.Items.Count >= 1)
                                        {
                                            ItemCombo.SelectedIndex = 0;
                                        }
                                    }
                                } // Fin del  foreach (  ComboBox ItemCombo 
                            } // del if (CleanComboBoxes)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los campos fecha
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            if (CleanDataFields)
                            { // del if (CleanDataFields)
                                if (ValueOfDate.Length > 0)
                                { // del if (ValueOfDate.Length > 0)
                                    foreach (DateTimePicker ItemFecha in ControlWindowForm.Controls.OfType<DateTimePicker>())
                                    { // inicio del  foreach (  ComboBox ItemFecha
                                        stL_NombreControl = "";
                                        stL_NombreControl = ItemFecha.Name;
                                        // Valida si esta en los campos Excluidos.
                                        blL_CampoExcluido = false;
                                        for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                        { // Inicio del  for ( inL_Index = 0 ;
                                            if (Array_Exc[inL_Index].Length > 0)
                                            { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                                if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                                {
                                                    blL_CampoExcluido = true;
                                                    break;
                                                }
                                            } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            else
                                            {
                                                break;
                                            }
                                        } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                        if (!blL_CampoExcluido)
                                        {
                                            ItemFecha.Value = Convert.ToDateTime(ValueOfDate);
                                        }
                                    } // Fin del  foreach (  ComboBox ItemFecha 
                                } // del if (ValueOfDate.Length > 0)
                            } // del if (CleanDataFields)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los campos NumericUpDown
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            foreach (NumericUpDown ItemNumeric in ControlWindowForm.Controls.OfType<NumericUpDown>())
                            { // inicio del  foreach (  NumericUpDown ItemNumeric
                                //
                                stL_NombreControl = "";
                                stL_NombreControl = ItemNumeric.Name;
                                // Valida si esta en los campos Excluidos.
                                blL_CampoExcluido = false;
                                for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                { // Inicio del  for ( inL_Index = 0 ;
                                    if (Array_Exc[inL_Index].Length > 0)
                                    { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                        {
                                            blL_CampoExcluido = true;
                                            break;
                                        }
                                    } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                    else
                                    {
                                        break;
                                    }
                                } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                if (!blL_CampoExcluido)
                                {
                                    ItemNumeric.Value = 0;
                                }
                                //
                            }  // Fin del  foreach (  NumericUpDown ItemNumeric
                            //////////////////////////////////////////////////////////////
                        } // del if (ControlHijo.HasChildren)
                        else // del if (ControlWindowForm.HasChildren)
                        { // del else del if (ControlWindowForm.HasChildren)
                            stL_NombreControl = "";
                            stL_NombreControl = ControlWindowForm.Name;
                            if (ControlWindowForm is TextBox)
                            { //  if (ControlWindowForm is TextBox)
                                // Valida si esta en los campos Excluidos.
                                blL_CampoExcluido = false;
                                for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                { // Inicio del  for ( inL_Index = 0 ;
                                    if (Array_Exc[inL_Index].Length > 0)
                                    { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                        {
                                            blL_CampoExcluido = true;
                                            break;
                                        }
                                    } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                    else
                                    {
                                        break;
                                    }
                                } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                if (!blL_CampoExcluido)
                                {
                                    ControlWindowForm.Text = "";
                                }
                            } //  if (ControlWindowForm is TextBox)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los combobox
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            if (CleanComboBoxes)
                            { // if (CleanComboBoxes)
                                foreach (ComboBox ItemCombo in ControlWindowForm.Controls.OfType<ComboBox>())
                                { // inicio del  foreach (  ComboBox ItemCombo 
                                    stL_NombreControl = "";
                                    stL_NombreControl = ItemCombo.Name;
                                    // Valida si esta en los campos Excluidos.
                                    blL_CampoExcluido = false;
                                    for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                    { // Inicio del  for ( inL_Index = 0 ;
                                        if (Array_Exc[inL_Index].Length > 0)
                                        { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                            {
                                                blL_CampoExcluido = true;
                                                break;
                                            }
                                        } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        else
                                        {
                                            break;
                                        }
                                    } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                    if (!blL_CampoExcluido)
                                    {
                                        // Deja el primer valor del combobox
                                        if (ItemCombo.Items.Count >= 1)
                                        {
                                            ItemCombo.SelectedIndex = 0;
                                        }
                                    }
                                } // Fin  del  foreach (  ComboBox ItemCombo 
                            } // Fin if (CleanComboBoxes)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los campos Fecha
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            if (CleanDataFields)
                            { // del if (CleanDataFields)
                                if (ValueOfDate.Length > 0)
                                { // del if (ValueOfDate.Length > 0)
                                    foreach (DateTimePicker ItemFecha in ControlWindowForm.Controls.OfType<DateTimePicker>())
                                    { // inicio del  foreach ( DateTimePicker ItemFecha
                                        stL_NombreControl = "";
                                        stL_NombreControl = ItemFecha.Name;
                                        // Valida si esta en los campos Excluidos.
                                        blL_CampoExcluido = false;
                                        for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                        { // Inicio del  for ( inL_Index = 0 ;
                                            if (Array_Exc[inL_Index].Length > 0)
                                            { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                                if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                                {
                                                    blL_CampoExcluido = true;
                                                    break;
                                                }
                                            } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            else
                                            {
                                                break;
                                            }
                                        } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                        if (!blL_CampoExcluido)
                                        {
                                            ItemFecha.Value = Convert.ToDateTime(ValueOfDate);
                                        }
                                    } // Fin  del  foreach (  DateTimePicker ItemFecha
                                } // Fin del  del if (ValueOfDate.Length > 0)
                            } // Fin de // del if (CleanDataFields)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los campos NumericUpDown
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            foreach (NumericUpDown ItemNumeric in ControlWindowForm.Controls.OfType<NumericUpDown>())
                            { // inicio del  foreach (  NumericUpDown ItemNumeric
                                //
                                stL_NombreControl = "";
                                stL_NombreControl = ItemNumeric.Name;
                                // Valida si esta en los campos Excluidos.
                                blL_CampoExcluido = false;
                                for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                { // Inicio del  for ( inL_Index = 0 ;
                                    if (Array_Exc[inL_Index].Length > 0)
                                    { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                        {
                                            blL_CampoExcluido = true;
                                            break;
                                        }
                                    } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                    else
                                    {
                                        break;
                                    }
                                } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                if (!blL_CampoExcluido)
                                {
                                    ItemNumeric.Value = 0;
                                }
                                //
                            } // Fin del del  foreach (  NumericUpDown ItemNumeric
                            ///////////////////////////////////////////////////////
                        } // del else del if (ControlWindowForm.HasChildren)
                    } // de foreach (Control ControlWindowForm
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetCleanValuesControlForm. System.AccessViolationException", "", ex_0.Message.ToString() + "Control : " + stL_NombreControl);
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetCleanValuesControlForm. Exception", "", ex.Message.ToString() + "Control : " + stL_NombreControl);
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMe_An_Int_Value(String String2Manipulate)
        {
            /// <summary>
            /// Metodo : stFormatea_A_Entero
            /// Formatea un numero entero con comas, asi:
            /// Entrada = "2345679086543"
            /// Salida  = "2,345,679,086,543"
            /// </summary>
            /// <param name="st_Valor">Numero a dar el formato</param>
            /// <returns></returns>
            Int64 lnL_repNumerica = 0;
            String stL_SalidaNumero = "";
            bool blL_CanConvert = false;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ////////////////////////////////////////////
                    // Formatea un numero entero.
                    // Ejemplo : 
                    // st_Valor = "2345679086543"
                    // Se convierte a long :
                    // lnL_repNumerica = 2345679086543
                    // Se formatea Sin decimales Dollar:
                    // stL_SalidaNumero = "2,345,679,086,543"
                    ////////////////////////////////////////////
                    if (String2Manipulate.Length > 0)
                    { // del if (st_Valor.Length > 0)
                        // URL Referencia : http://msdn.microsoft.com/es-es/library/bb384043.aspx
                        // Averigua si es un numero la cadena
                        blL_CanConvert = long.TryParse(String2Manipulate, out lnL_repNumerica);
                        if (blL_CanConvert)
                        {
                            // Lo convierte a long
                            lnL_repNumerica = Convert.ToInt64(String2Manipulate);
                            //
                            stL_SalidaNumero = (lnL_repNumerica.ToString("N0", CultureInfo.CreateSpecificCulture("en-US")));
                            //
                        }
                    } // if (st_Valor.Length > 0)
                }
                return stL_SalidaNumero;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_An_Int_Value. System.AccessViolationException", "", ex_0.Message.ToString());
                return "";
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_An_Int_Value. Exception", "", ex.Message.ToString());
                return "";
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public String ConvertDateTime2Query(CLNBTN_IQy.inDB_Types DataBaseEngine_Type, String DateToConvert, int FormatOfDate = BD_DATE_FORMAT_DD_MM_AAAA)
        {
            ///  <summary>
            /// Metodo : st_DateTime_4_Query
            /// Devuelve la fecha para el formato de los queries
            /// Devuelve la Fecha en Formato AAAAMMDD para SQL Server
            /// para los demas tipos de servidores la devuelve AAAA-MM-DD
            /// Con la HORA
            /// </summary>
            /// <param name="DataBaseEngine_Type">Tipo de motor de la base de datos</param>
            /// <param name="DateToConvert">Fecha que se necesita convertir</param>
            /// <param name="int FormatOfDate">Formato de la fecha que se maneja en la aplicacion</param>
            /// <param name="bl_TomaHora = false">Parametro opcional. si viene en TRUE, se toma la hora de la fecha.</param>
            /// <returns>La fecha y hora en el formato que la entiene el motor de la base de datos para ejecutar los queries</returns>
            String stL_Ano = "";
            String stL_Mes = "";
            String stL_Dia = "";
            String stL_FechaAux = "";
            String stL_Separador = "";
            //
            String stL_ParteDelaHora = "";
            int inL_number1 = 0;
            int inL_ContaDosPuntos = 0;
            String stL_Hora = "";
            String stL_Minutos = "";
            String stL_Segundos = "";
            int inL_Pos = 0;
            String stL_HoraServidor = "";
            int inL_Hora = 0;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    switch (DataBaseEngine_Type)
                    { // inicio del switch (DataBaseEngine_Type)
                        case CLNBTN_IQy.inDB_Types.DB_TYPE_SQLSERVER:
                            //
                            stL_Separador = "";
                            break;
                        default:
                            //
                            stL_Separador = "-";
                            break;
                    } // Fin  del switch (DataBaseEngine_Type)
                    /////////
                    switch (FormatOfDate)
                    {
                        case BD_DATE_FORMAT_DD_MM_AAAA:
                            // Viene DD/MM/AAAA    
                            stL_Ano = DateToConvert.Substring(6, 4);
                            stL_Mes = DateToConvert.Substring(3, 2);
                            stL_Dia = DateToConvert.Substring(0, 2);
                            break;
                        case BD_DATE_FORMAT_AAAA_MM_DD:
                            // Viene AAAA/MM/DD
                            stL_Ano = DateToConvert.Substring(0, 4);
                            stL_Mes = DateToConvert.Substring(5, 2);
                            stL_Dia = DateToConvert.Substring(8, 2);
                            break;
                        case BD_DATE_FORMAT_MM_DD_AAAA:
                            // MM/DD/AAAA
                            stL_Ano = DateToConvert.Substring(6, 4);
                            stL_Mes = DateToConvert.Substring(0, 2);
                            stL_Dia = DateToConvert.Substring(3, 2);
                            break;
                        default:
                            stL_Ano = DateToConvert.Substring(6, 4);
                            stL_Mes = DateToConvert.Substring(3, 2);
                            stL_Dia = DateToConvert.Substring(0, 2);
                            break;
                    }
                    //
                    if ((Convert.ToInt32(stL_Mes) < 9) & (stL_Mes.Length == 1))
                    {
                        stL_Mes = "0" + stL_Mes;
                    }
                    if ((Convert.ToInt32(stL_Dia) < 9) & (stL_Dia.Length == 1))
                    {
                        stL_Dia = "0" + stL_Dia;
                    }
                    //
                    // Devuelve la Fecha en Formato AAAAMMDD para SQL Server
                    // para los demas tipos de servidores la devuelve AAAA-MM-DD
                    stL_FechaAux = stL_Ano + stL_Separador + stL_Mes + stL_Separador + stL_Dia;
                    ///////////////////////////
                    // Toma la hora
                    ///////////////////////////
                    //
                    // Valida si la hora tiene el formato : 05/09/2012 03:47:09 p.m.
                    // para devolver la hora en formato militar:
                    // 03:47:09 p.m. = 15:47:09
                    /////////////////////////////////////////////////////////////////
                    // Recorre el string que tiene la hora y la separa en Horas, Minutos y Segundos
                    // Y hace la validacion para colocar la hora en formato Militar.
                    // Halla la hora
                    // Recorre el string de la hora y separa horas, minutos y segundos.
                    stL_ParteDelaHora = DateToConvert.Substring(11, DateToConvert.Length - 11);
                    for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                    { // Inicio del for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                        // Toma cada caracter y valida si es numero, para asi separarlos
                        bool canConvert = int.TryParse(stL_ParteDelaHora.Substring(inL_Index1, 1), out inL_number1);
                        if (canConvert == true)
                        { // Inicio del if (canConvert == true
                            switch (inL_ContaDosPuntos)
                            {
                                case 0:
                                    // La hora
                                    stL_Hora += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                    break;
                                case 1:
                                    // los minutos
                                    stL_Minutos += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                    break;
                                case 2:
                                    // los segundos
                                    stL_Segundos += stL_ParteDelaHora.Substring(inL_Index1, 1);
                                    break;
                            }
                        } // fin del if (canConvert == true
                        else // del if (canConvert == true
                        { // del else del if (canConvert == true
                            // incrementa el contador de los caracteres que no son numericos, como los dos puntos.
                            inL_ContaDosPuntos = inL_ContaDosPuntos + 1;
                        } // fin del else del if (canConvert == true
                    } // Fin de for (int inL_Index1 = 0; inL_Index1 < stL_ParteDelaHora.Length; inL_Index1++)
                    ///////////////////////////////////////////
                    inL_Pos = DateToConvert.IndexOf("p.m.");
                    if (inL_Pos == -1)
                    {
                        stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                    }
                    else
                    {
                        inL_Hora = Convert.ToInt32(stL_Hora);
                        // si la hora es menor a 12
                        // Indica que es por la tarde
                        // a 12 le suma la hora que se tiene
                        // por ejemplo:
                        // hora 03. deberia ser 12 + 3 = 15 las 15 Horas
                        if (inL_Hora < 12)
                        {
                            inL_Hora = 12 + inL_Hora;
                            stL_Hora = Convert.ToString(inL_Hora);
                            stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                        }
                        else
                        {
                            stL_HoraServidor = stL_Hora + ":" + stL_Minutos + ":" + stL_Segundos;
                        }
                    }
                    //
                    if (stL_HoraServidor.Length > 0)
                    {
                        stL_FechaAux += " " + stL_HoraServidor;
                    }
                    //
                    stL_FechaAux = stL_FechaAux.Trim();
                    //
                }
                return stL_FechaAux;
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ConvertDateTime2Query. System.AccessViolationException", "", ex_0.Message.ToString());
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "ConvertDateTime2Query. Exception", "", ex.Message.ToString());
            }
            return stL_FechaAux;
        }



        [HandleProcessCorruptedStateExceptions]
        public Boolean Let_Delete_A_Dir(String Dir2Delete, Boolean OutWindow = false)
        {
            /// <summary>
            /// Metodo : Elimina_Directorio
            /// Encargado de eliminar un directorio.
            /// </summary>
            /// <param name="Dir2Delete">Directorio a Eliminar.</param>
            /// <param name="OutWindow">Indica si presenta la salida del error en la pantalla. Por defecto en False</param>
            /// <returns>True = Si elimino el directorio</returns>
            // Elimina un Directorio y todos sus archivos.
            // Url : http://codigo.klosions.com/2010/10/c-como-copiar-eliminar-y-mover-archivos-y-carpetas-net/
            Boolean blL_Elimino = false;
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (Directory.Exists(Dir2Delete))
                    {
                        // Recorre cada uno de los archivos del directorio y los elimina
                        // para luego eliminar el directorio.
                        string[] files = System.IO.Directory.GetFiles(Dir2Delete);
                        //
                        foreach (string Arquivo in files)
                        {
                            // 
                            String stL_FileName = System.IO.Path.GetFileName(Arquivo);
                            //
                            System.IO.File.Delete(Dir2Delete + "\\" + stL_FileName);
                            //
                        }
                        //
                        Directory.Delete(Dir2Delete);
                        blL_Elimino = true;
                    }
                    //
                }
                return blL_Elimino;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_A_Dir. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_A_Dir. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                 return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, OutWindow);
                if (ex.InnerException == null)
                {
                    //objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_A_Dir. Exception", "", ex.Message.ToString());
                }
                else
                {
                    //objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_A_Dir. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return false;
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public Boolean Let_Copy_Dir(String SourceDir, String TargetDir, Boolean Delete_SourceDir = false, Boolean OutWindow = false)
        {
            // Copia los archivos de un directorio a otro
            // Url : http://codigo.klosions.com/2010/10/c-como-copiar-eliminar-y-mover-archivos-y-carpetas-net/
            Boolean blL_CopiaDir = false;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Si el directorio de Origen Existe
                    if (Directory.Exists(SourceDir))
                    {
                        // Si el directorio de Destino no existe lo crea
                        if (!Directory.Exists(TargetDir))
                        {
                            Directory.CreateDirectory(TargetDir);
                        }
                        //
                        // Recorre cada uno de los archivos del Directorio de Origen
                        // Y los copia al directorio de Destino, haciendo OVERWRITE
                        string[] files = System.IO.Directory.GetFiles(SourceDir);
                        //
                        foreach (string Arquivo in files)
                        {
                            // Use static Path methods to extract only the file name from the path.
                            String stL_FileName = System.IO.Path.GetFileName(Arquivo);
                            //
                            String stL_DestFile = System.IO.Path.Combine(TargetDir, stL_FileName);
                            System.IO.File.Copy(Arquivo, stL_DestFile, true);
                        }
                        // Valida si debe eliminar el directorio de destino.
                        if (Delete_SourceDir)
                        {
                            blL_CopiaDir = this.Let_Delete_A_Dir(SourceDir, OutWindow);
                        }
                        else
                        {
                            blL_CopiaDir = true;
                        }
                    }
                    //
                }
                return blL_CopiaDir;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_A_Dir. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_A_Dir. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_A_Dir. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_A_Dir. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean Let_Delete_DirFiles(String SourceDir, string[] Array_Exc, Boolean OutWindow = false)
        {
            /// <summary>
            /// Metodo : Elimina_Archivos_Directorio
            /// Encargado de eliminar los archivos de un directorio.
            /// Recibe un arreglo de tipo string, con el nombre de los archivos que no debe eliminar.
            /// </summary>
            /// <param name="SourceDir">Ruta completa del directorio de origen</param>
            /// <param name="Array_Exc">Arreglo de String con los nombres de los archivos que NO debe eliminar. Archivos Excluidos</param>
            /// <param name="bl_SalDialog"></param>
            /// <returns>Devuelve TRUE si pudo realizar la operacion</returns>
            // Copia los archivos de un directorio a otro
            // Url : http://codigo.klosions.com/2010/10/c-como-copiar-eliminar-y-mover-archivos-y-carpetas-net/
            Boolean blL_EliminaArchivosDir = false;
            Boolean blL_Elimina_Archivo = false;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Si el directorio de Origen Existe
                    if (Directory.Exists(SourceDir))
                    {
                        //
                        // Recorre cada uno de los archivos del Directorio de Origen
                        // Y los copia al directorio de Destino, haciendo OVERWRITE
                        string[] files = System.IO.Directory.GetFiles(SourceDir);
                        //
                        foreach (string Arquivo in files)
                        {
                            // Use static Path methods to extract only the file name from the path.
                            String stL_FileName = System.IO.Path.GetFileName(Arquivo);
                            // Recorre el Arreglo para saber si hay archivos excluidos de la eliminacion
                            blL_Elimina_Archivo = true;
                            for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                            {
                                if (Array_Exc[inL_Index] != null)
                                {
                                    if (Array_Exc[inL_Index].Trim().Length > 0)
                                    {
                                        if (Array_Exc[inL_Index].Trim().ToLower() == stL_FileName.Trim().ToLower())
                                        {
                                            blL_Elimina_Archivo = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (blL_Elimina_Archivo)
                            {
                                File.Delete(SourceDir + "\\" + stL_FileName);
                            }
                        }
                        blL_EliminaArchivosDir = true;
                    }
                    //
                }
                return blL_EliminaArchivosDir;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_DirFiles. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_DirFiles. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_DirFiles. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Delete_DirFiles. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String Let_ReadTextFile(String TextFile2Read)
        {
            /// <summary>
            /// Metodo : LeerArchivoTexto
            /// Lee un archivo Texto y devuelve el contenido en un arreglo de bytes
            /// </summary>
            /// <param name="TextFile2Read">Ruta y nombe de archvo texto.</param>
            /// <returns></returns>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    if (File.Exists(TextFile2Read))
                    {
                        FileStream fs = new FileStream(TextFile2Read, FileMode.Open, FileAccess.Read, FileShare.Read);
                        byte[] abyt = new byte[Convert.ToInt32(fs.Length)];
                        fs.Read(abyt, 0, abyt.Length);
                        fs.Close();
                        return Encoding.UTF8.GetString(abyt);
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_ReadTextFile. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_ReadTextFile. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return "";
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_ReadTextFile. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_ReadTextFile. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return "";
            }
        }



        [HandleProcessCorruptedStateExceptions]
        public Boolean Let_Create_PDF_From_TextFile(String SourceTextFile, String TargetPdfFile)
        {
            /// <summary>
            /// Metodo : Crear_PDF_Basado_Texto
            /// Crea un archivo PDF basado en un archivo .TXT
            /// </summary>
            /// <param name="SourceTextFile">Ruta y nombre del archivo de texto.</param>
            /// <param name="TargetPdfFile">Ruta y nombre del archivo .PDF</param>
            /// <returns></returns>
            // Crea un archivo PDF con base en un archivo texto.
            // Url : http://mundocharp.blogspot.com/2006/05/pasando-un-archivo-de-texto-pdf.html
            Boolean blL_GeneroOk = false;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (SourceTextFile.Length > 0)
                    { // inicio del if (SourceTextFile.Length > 0)
                        // Valida si Existe el .Txt
                        if (File.Exists(SourceTextFile))
                        { // Inicio del if ( File.Exists (SourceTextFile ) )
                            // Valida si Existe el PDF. lo Elimina si existe
                            if (File.Exists(TargetPdfFile))
                            {
                                File.Delete(TargetPdfFile);
                            }
                            //////////////////////////////////////////////////////////
                            // Abre el archivo de texto para leerlo.
                            //////////////////////////////////////////////////////////
                            string stL_StringFile = this.Let_ReadTextFile(SourceTextFile);
                            // Crea el PDF
                            Document pdf = new Document();
                            PdfWriter.GetInstance(pdf, new FileStream(TargetPdfFile, FileMode.Create));
                            pdf.Open();
                            pdf.Add(new Paragraph(stL_StringFile));
                            pdf.Close();
                            blL_GeneroOk = true;
                        } // Fin del if ( File.Exists (SourceTextFile ) )
                    } // Fin del inicio del if (SourceTextFile.Length > 0)
                }
                return blL_GeneroOk;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Create_PDF_From_TextFile. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Create_PDF_From_TextFile. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Create_PDF_From_TextFile. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Create_PDF_From_TextFile. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean BringMe_DB_Conn_Status(CLNBTN_IQy Ob_DBInf, String DB_TableName = "")
        {
            /// <summary>
            /// Metodo : Valida_Conexion_Bd
            /// Valida si hay conexion con una base de datos ejecutnado un query
            /// </summary>
            /// <param name="Ob_DBInf">StrailSAS_C_ProgRes.ClasX_DBInfo Ob_DBInf : Informacion de la base de datos</param>
            /// <param name="DB_TableName">Nombre de la tabla sobre la cual se va a hacer el query. Si viene vacia se utiliza la tabla "T01GRUPOS".</param>
            /// <returns>true = si se pudo conectar y ejecutar a la base de datos</returns>
            // Encargada de validar la conexion con una base de datos, ejecutand una instruccion.
            Boolean blL_ConexionOk = false;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Cre la instancia de la clase y que no presente el error en la pantalla, para poderlo evaluar.
                    CLNBTN_Qy Obj_BD = new CLNBTN_Qy(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, false, _st_Lic);
                    //
                    Obj_BD.setDataBaseInfo(Ob_DBInf);
                    //
                    // Define DataTable, para los Datos del Query
                    DataTable DatTable = null;
                    //
                    Obj_BD.ToDo_SELECT("COUNT(*)");
                    if (DB_TableName.Length == 0)
                    {
                        Obj_BD.ToDo_FROM("T01GRUPOS");
                    }
                    else
                    {
                        Obj_BD.ToDo_FROM(DB_TableName.Trim());
                    }
                    Obj_BD.ToDo_EXECUTE_SQL(ref DatTable);
                    if (DatTable != null)
                    { // Inicio del if (DatTable != null)
                        for (int inL_Row = 0; inL_Row < DatTable.Rows.Count; inL_Row++)
                        { // Inicio del for (int inL_Row = 0; 
                            if (inL_Row >= 0)
                            {
                                blL_ConexionOk = true;
                                break;
                            }
                        }
                    }
                    Obj_BD.ToDo_CLOSE();
                    //
                }
                return blL_ConexionOk;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, false);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_DB_Conn_Status. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_DB_Conn_Status. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, false);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_DB_Conn_Status. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_DB_Conn_Status. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void SetEnableDisableControlForm(Boolean Set_Enbaled, Control WindowForm, Boolean CleanComboBoxes, Boolean CleanDataFields, String ValueOfDate, string[] Array_Exc)
        {
            /// <summary>
            /// Metodo : Habilita_DesHabilita_Controles_WindowForm
            /// Encargado de habilitar o deshabilitar los controles de uan WindowForm o un frame.
            /// Cambiando el color dependiendo del estado.
            /// Solo trabaja con controles tipo:
            /// TextBox
            /// ComboBox
            /// DateTimePicker ( Campos de fechas ) 
            /// </summary>
            /// <param name="Set_Enbaled">TRUE = Indica ue habilita los controles</param>
            /// <param name="WindowForm">WindowForm o frame que contiene los controles</param>
            /// <param name="bl_TrabajaCombos">TRUE = Indica si habilia o no los combos</param>
            /// <param name="bl_TrabajaCamposFecha">TRUE = Indica si habilita o no los campos fecha</param>
            /// <param name="ValueOfDate">Fecha a asignar</param>
            /// <param name="Array_Exc">Arreglo con los campos excluidos.</param>
            // Habilita o Deshabilita los controles de una WindowForm
            // Aplica para controles tipo:
            // TexBox       : TextBox
            // CombosBox    : ComboBox
            // Campos Fecha : DateTimePicker
            String stL_NombreControl = "";
            Boolean blL_CampoExcluido = false;
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    foreach (Control ControlWindowForm in WindowForm.Controls)
                    { // del foreach (Control ControlWindowForm
                        if (ControlWindowForm.HasChildren)
                        { // del if (ControlHijo.HasChildren)
                            foreach (Control ControlHijo in ControlWindowForm.Controls)
                            { // del foreach (Control ControlHijo
                                stL_NombreControl = "";
                                stL_NombreControl = ControlHijo.Name;
                                if (ControlHijo is TextBox)
                                { // Inicio del if (ControlHijo is TextBox)
                                    // Valida si esta en los campos Excluidos.
                                    blL_CampoExcluido = false;
                                    for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                    { // Inicio del  for ( inL_Index = 0 ;
                                        if (Array_Exc[inL_Index].Length > 0)
                                        { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                            {
                                                blL_CampoExcluido = true;
                                                break;
                                            }
                                        } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        else
                                        {
                                            break;
                                        }
                                    } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                    if (!blL_CampoExcluido)
                                    {
                                        ControlHijo.Enabled = Set_Enbaled;
                                        if (Set_Enbaled)
                                        {
                                            ControlHijo.BackColor = System.Drawing.Color.White;
                                        }
                                        else
                                        {
                                            ControlHijo.BackColor = System.Drawing.SystemColors.InactiveBorder;
                                        }
                                    }
                                } // Fin de if (ControlHijo is TextBox)
                            } // del foreach (Control ControlHijo
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los combobox
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            if (CleanComboBoxes)
                            { // if (CleanComboBoxes)
                                foreach (ComboBox ItemCombo in ControlWindowForm.Controls.OfType<ComboBox>())
                                { // inicio del  foreach (  ComboBox ItemCombo 
                                    stL_NombreControl = "";
                                    stL_NombreControl = ItemCombo.Name;
                                    // Valida si esta en los campos Excluidos.
                                    blL_CampoExcluido = false;
                                    for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                    { // Inicio del  for ( inL_Index = 0 ;
                                        if (Array_Exc[inL_Index].Length > 0)
                                        { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                            {
                                                blL_CampoExcluido = true;
                                                break;
                                            }
                                        } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        else
                                        {
                                            break;
                                        }
                                    } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                    if (!blL_CampoExcluido)
                                    {
                                        // Deja el primer valor del combobox
                                        //if (ItemCombo.Items.Count >= 1)
                                        //{
                                        //    ItemCombo.SelectedIndex = 0;
                                        //}
                                        ItemCombo.Enabled = Set_Enbaled;
                                        if (Set_Enbaled)
                                        {
                                            ItemCombo.BackColor = System.Drawing.SystemColors.Window;
                                        }
                                        else
                                        {
                                            ItemCombo.BackColor = System.Drawing.SystemColors.InactiveBorder;
                                        }
                                    }
                                } // Fin del  foreach (  ComboBox ItemCombo 
                            } // del if (CleanComboBoxes)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los campos fecha
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            if (CleanDataFields)
                            { // del if (CleanDataFields)
                                if (ValueOfDate.Length > 0)
                                { // del if (ValueOfDate.Length > 0)
                                    foreach (DateTimePicker ItemFecha in ControlWindowForm.Controls.OfType<DateTimePicker>())
                                    { // inicio del  foreach (  ComboBox ItemFecha
                                        stL_NombreControl = "";
                                        stL_NombreControl = ItemFecha.Name;
                                        // Valida si esta en los campos Excluidos.
                                        blL_CampoExcluido = false;
                                        for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                        { // Inicio del  for ( inL_Index = 0 ;
                                            if (Array_Exc[inL_Index].Length > 0)
                                            { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                                if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                                {
                                                    blL_CampoExcluido = true;
                                                    break;
                                                }
                                            } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            else
                                            {
                                                break;
                                            }
                                        } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                        if (!blL_CampoExcluido)
                                        {
                                            ItemFecha.Value = Convert.ToDateTime(ValueOfDate);
                                            ItemFecha.Enabled = Set_Enbaled;
                                        }
                                    } // Fin del  foreach (  ComboBox ItemFecha 
                                } // del if (ValueOfDate.Length > 0)
                            } // del if (CleanDataFields)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los campos de NumericUpDown
                            ///////////////////////////////////////////////////////////////////////////////
                            foreach (NumericUpDown ItemNumeric in ControlWindowForm.Controls.OfType<NumericUpDown>())
                            { // inicio del  foreach (  NumericUpDown ItemNumeric
                                stL_NombreControl = "";
                                stL_NombreControl = ItemNumeric.Name;
                                // Valida si esta en los campos Excluidos.
                                blL_CampoExcluido = false;
                                for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                { // Inicio del  for ( inL_Index = 0 ;
                                    if (Array_Exc[inL_Index].Length > 0)
                                    { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                        {
                                            blL_CampoExcluido = true;
                                            break;
                                        }
                                    } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                    else
                                    {
                                        break;
                                    }
                                } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                if (!blL_CampoExcluido)
                                {
                                    ItemNumeric.Enabled = Set_Enbaled;
                                    if (Set_Enbaled)
                                    {
                                        ItemNumeric.BackColor = System.Drawing.SystemColors.Window;
                                    }
                                    else
                                    {
                                        ItemNumeric.BackColor = System.Drawing.SystemColors.InactiveBorder;
                                    }
                                }
                            } // Fin del  foreach (  NumericUpDown ItemNumeric
                            ///////////////////////////////////////////////////////////////////////
                        } // del if (ControlHijo.HasChildren)
                        else // del if (ControlWindowForm.HasChildren)
                        { // del else del if (ControlWindowForm.HasChildren)
                            stL_NombreControl = "";
                            stL_NombreControl = ControlWindowForm.Name;
                            if (ControlWindowForm is TextBox)
                            { //  if (ControlWindowForm is TextBox)
                                // Valida si esta en los campos Excluidos.
                                blL_CampoExcluido = false;
                                for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                { // Inicio del  for ( inL_Index = 0 ;
                                    if (Array_Exc[inL_Index].Length > 0)
                                    { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                        {
                                            blL_CampoExcluido = true;
                                            break;
                                        }
                                    } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                    else
                                    {
                                        break;
                                    }
                                } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                if (!blL_CampoExcluido)
                                {
                                    //ControlWindowForm.Text = "";
                                    if (Set_Enbaled)
                                    {
                                        ControlWindowForm.BackColor = System.Drawing.Color.White;
                                    }
                                    else
                                    {
                                        ControlWindowForm.BackColor = System.Drawing.SystemColors.InactiveBorder;
                                    }
                                }
                            } //  if (ControlWindowForm is TextBox)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los combobox
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            if (CleanComboBoxes)
                            { // if (CleanComboBoxes)
                                foreach (ComboBox ItemCombo in ControlWindowForm.Controls.OfType<ComboBox>())
                                { // inicio del  foreach (  ComboBox ItemCombo 
                                    stL_NombreControl = "";
                                    stL_NombreControl = ItemCombo.Name;
                                    // Valida si esta en los campos Excluidos.
                                    blL_CampoExcluido = false;
                                    for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                    { // Inicio del  for ( inL_Index = 0 ;
                                        if (Array_Exc[inL_Index].Length > 0)
                                        { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                            {
                                                blL_CampoExcluido = true;
                                                break;
                                            }
                                        } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        else
                                        {
                                            break;
                                        }
                                    } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                    if (!blL_CampoExcluido)
                                    {
                                        // Deja el primer valor del combobox
                                        //if (ItemCombo.Items.Count >= 1)
                                        //{
                                        //    ItemCombo.SelectedIndex = 0;
                                        //}
                                        ItemCombo.Enabled = Set_Enbaled;
                                        if (Set_Enbaled)
                                        {
                                            ItemCombo.BackColor = System.Drawing.SystemColors.Window;
                                        }
                                        else
                                        {
                                            ItemCombo.BackColor = System.Drawing.SystemColors.InactiveBorder;
                                        }
                                    }
                                } // Fin  del  foreach (  ComboBox ItemCombo 
                            } // Fin if (CleanComboBoxes)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los campos Fecha
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            if (CleanDataFields)
                            { // del if (CleanDataFields)
                                if (ValueOfDate.Length > 0)
                                { // del if (ValueOfDate.Length > 0)
                                    foreach (DateTimePicker ItemFecha in ControlWindowForm.Controls.OfType<DateTimePicker>())
                                    { // inicio del  foreach ( DateTimePicker ItemFecha
                                        stL_NombreControl = "";
                                        stL_NombreControl = ItemFecha.Name;
                                        // Valida si esta en los campos Excluidos.
                                        blL_CampoExcluido = false;
                                        for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                        { // Inicio del  for ( inL_Index = 0 ;
                                            if (Array_Exc[inL_Index].Length > 0)
                                            { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                                if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                                {
                                                    blL_CampoExcluido = true;
                                                    break;
                                                }
                                            } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                            else
                                            {
                                                break;
                                            }
                                        } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                        if (!blL_CampoExcluido)
                                        {
                                            ItemFecha.Value = Convert.ToDateTime(ValueOfDate);
                                            ItemFecha.Enabled = Set_Enbaled;
                                        }
                                    } // Fin  del  foreach (  DateTimePicker ItemFecha
                                } // Fin del  del if (ValueOfDate.Length > 0)
                            } // Fin de // del if (CleanDataFields)
                            ///////////////////////////////////////////////////////////////////////////////
                            // Para los campos de NumericUpDown
                            ///////////////////////////////////////////////////////////////////////////////
                            stL_NombreControl = "";
                            foreach (NumericUpDown ItemNumeric in ControlWindowForm.Controls.OfType<NumericUpDown>())
                            { // inicio del  foreach (  NumericUpDown ItemNumeric
                                //
                                stL_NombreControl = "";
                                stL_NombreControl = ItemNumeric.Name;
                                // Valida si esta en los campos Excluidos.
                                blL_CampoExcluido = false;
                                for (int inL_Index = 0; inL_Index < Array_Exc.Length; inL_Index++)
                                { // Inicio del  for ( inL_Index = 0 ;
                                    if (Array_Exc[inL_Index].Length > 0)
                                    { // inicio del if (Array_Exc[inL_Index].Length > 0)
                                        if (stL_NombreControl.ToUpper().Trim() == Array_Exc[inL_Index].ToUpper().Trim())
                                        {
                                            blL_CampoExcluido = true;
                                            break;
                                        }
                                    } // inicio del if (Array_Exc[inL_Index].Length > 0)
                                    else
                                    {
                                        break;
                                    }
                                } // Fin // Inicio del  for ( int inL_Index = 0 ;
                                if (!blL_CampoExcluido)
                                {
                                    ItemNumeric.Enabled = Set_Enbaled;
                                    if (Set_Enbaled)
                                    {
                                        ItemNumeric.BackColor = System.Drawing.SystemColors.Window;
                                    }
                                    else
                                    {
                                        ItemNumeric.BackColor = System.Drawing.SystemColors.InactiveBorder;
                                    }
                                }
                                //
                            } // Fin del  foreach (  NumericUpDown ItemNumeric
                            ///////////////////////////////////////////////////////////////////////
                        } // del else del if (ControlWindowForm.HasChildren)
                    } // de foreach (Control ControlWindowForm
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetEnableDisableControlForm. System.AccessViolationException", "", ex_0.Message.ToString() + " Control : " + stL_NombreControl);
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetEnableDisableControlForm. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString() + " Control : " + stL_NombreControl);
                }
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetEnableDisableControlForm. Exception", "", ex.Message.ToString() + " Control : " + stL_NombreControl);
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "SetEnableDisableControlForm. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString() + " Control : " + stL_NombreControl);
                }
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean Is_A_Valid_EMail(String EMail2Evaluate, ref String ErrorMessage)
        {
            /// <summary>
            /// Metodo : Valida que un email sea correcto
            /// </summary>
            /// <param name="EMail2Evaluate">Correo electronico a validar</param>
            /// <param name="ErrorMessage">Devuelve el mensaje de error de las validaciones del correo electronico</param>
            /// <returns>true = si el correo esta ok</returns>
            // URL : http://www.webprogramacion.com/108/csharp/validacion-de-una-direccion-de-correo-electronico.aspx
            // Valida que un emial sea correcto.
            Boolean blL_InfoOK = true;
            String stL_StringAux = EMail2Evaluate;
            String stL_Expresion = "";
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    ErrorMessage = "El correo electrónico no tiene un formato válido.";
                    //
                    if (stL_StringAux.Length > 0)
                    { // Inicio del if (stL_StringAux.Length > 0)
                        stL_Expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                        if (Regex.IsMatch(stL_StringAux, stL_Expresion))
                        {
                            if (Regex.Replace(stL_StringAux, stL_Expresion, String.Empty).Length == 0)
                            {
                                blL_InfoOK = true;
                            }
                            else
                            {
                                blL_InfoOK = false;
                            }
                        }
                        else
                        {
                            blL_InfoOK = false;
                        }
                    } // FIn del if (stL_StringAux.Length > 0)
                    //
                }
                return blL_InfoOK;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_EMail. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_EMail. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_EMail. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Is_A_Valid_EMail. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean Let_Send_An_EMail(String ConfFile, String st_Seccion_Config, String st_Asunto, String st_CuerpoMensaje, string[] st_ArchivosAdjuntos, Boolean bl_SalDialog = false)
        {
            /// <summary>
            /// Metodo : Send_E_Mail
            /// Encargado de enviar un correo electronico.
            /// Lee del archivo de configuracion de la seccion : st_Seccion_Config
            /// La cuenta, servidor y clave ( encriptada) de la cuenta de origen
            /// Las cuentas de destino.
            /// Ejemplo de la seccion:
            /// 
            /// [E-MAIL]
            /// Email_Origen = alvaroquimbaya@strailsas.com
            /// Servidor_Origen = smtp.gmail.com
            /// Clave_Email_Origen = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            /// Puerto = 587
            /// TimeOut = 100
            /// ;
            /// Email_Destino_1 = asqcrf@hotmail.com
            /// Email_Destino_2 = strailaparicio@strailsas.com
            /// Email_Destino_3 = gabrielparra@strailsas.com
            /// Email_Destino_4 = andresgarcia@strailsas.com
            /// Email_Destino_5 = alvaroquimbaya@strailsas.com
            /// Email_Destino_6 = 
            /// Email_Destino_7 = 
            /// Email_Destino_8 = 
            /// Email_Destino_9 = 
            /// Email_Destino_10 = 
            /// Email_Destino_11 = 
            /// Email_Destino_12 = 
            /// Email_Destino_13= 
            /// Email_Destino_14= 
            /// Email_Destino_15 = 
            /// Email_Destino_16 = 
            /// Email_Destino_17 = 
            /// Email_Destino_19 = 
            /// Email_Destino_20 = 
            /// ;
            /// ;
            /// [FIN SECCION E-MAIL]
            /// ; ==========================================================================
            /// ;
            /// </summary>
            /// <param name="ConfFile">Ruta y nombre del archivo de configuracion de la aplicacion</param>
            /// <param name="st_Seccion_Config">Nombre de la seccion del archivo de configuracion de donde lee los parametros.</param>
            /// <param name="st_Asunto">Asunto del correo</param>
            /// <param name="st_CuerpoMensaje">Cuerpo del Mensaje</param>
            /// <param name="st_ArchivosAdjuntos">Arreglo con la Ruta y nombre de los archivos adjuntos.</param>
            /// <param name="bl_SalDialog">Indica si presenta la salida del error en la pantalla. Por defecto en False</param>
            /// <returns>true Si envio el E-Mail</returns>
            // Encargado de enviar un correo electronico
            // url : http://www.devjoker.com/contenidos/Articulos/291/Enviar-email-con-C.aspx
            // url : http://stackoverflow.com/questions/5034503/adding-an-attachment-to-email-using-c-sharp
            // Url : http://stackoverflow.com/questions/5132801/sending-mails-with-attachment-in-c-sharp
            // Url : http://stackoverflow.com/questions/13165396/the-operations-timed-out-when-using-smtpclient 
            Boolean blL_Enviado = false;
            //
            String stL_Email_Origen = "";
            String stL_Servidor_Origen = "";
            String stL_Clave_Emial_Origen = "";
            String stL_Email_Destino = "";
            String stL_Puerto = "";
            String stL_TimeOut = "";
            //
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    // Lee el archivo de configuracion y lee los parametros
                    CLNBTN_Cg Obj_Conf = new CLNBTN_Cg(ConfFile, _st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    // La clase de encriptacion
                    CLNBTN_Es ObjL_Encrip = new CLNBTN_Es(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow, _st_Lic);
                    //
                    ///////////////////////////////
                    // Lee parametros desde la seccion que viene como parametro
                    ///////////////////////////////
                    //
                    stL_Email_Origen = Obj_Conf.ReadAKeyFromSection(st_Seccion_Config, "Email_Origen");
                    stL_Servidor_Origen = Obj_Conf.ReadAKeyFromSection(st_Seccion_Config, "Servidor_Origen");
                    stL_Clave_Emial_Origen = Obj_Conf.ReadAKeyFromSection(st_Seccion_Config, "Clave_Email_Origen");
                    //
                    stL_Puerto = Obj_Conf.ReadAKeyFromSection(st_Seccion_Config, "Puerto");
                    if (stL_Puerto.Length == 0)
                    {
                        stL_Puerto = "587";
                    }
                    else
                    {
                        // Valida si es numerico
                        int inL_number1 = 0;
                        bool canConvert = int.TryParse(stL_Puerto, out inL_number1);
                        if (!canConvert)
                        {
                            stL_Puerto = "587";
                        }
                    }
                    stL_TimeOut = Obj_Conf.ReadAKeyFromSection(st_Seccion_Config, "TimeOut");
                    if (stL_TimeOut.Length == 0)
                    {
                        stL_TimeOut = "";
                    }
                    else
                    {
                        // Valida si es numerico
                        long lnL_number1 = 0;
                        bool canConvert = long.TryParse(stL_TimeOut, out lnL_number1);
                        if (!canConvert)
                        {
                            stL_TimeOut = "";
                        }
                    }
                    //
                    // DesEncripta la clave del correo
                    //
                    stL_Clave_Emial_Origen = ObjL_Encrip.File2Des(stL_Clave_Emial_Origen, "Frgtyhw", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "Oklijdiue", _st_Lic);
                    //
                    ///////////////////////////////
                    // Si esta definida la cuenta de origen y el servidor de origen y la clave
                    if (stL_Email_Origen.Length > 0 && stL_Servidor_Origen.Length > 0 && stL_Clave_Emial_Origen.Length > 0)
                    { // Inicio del if (stL_Email_Origen.Length > 0 && stL_Servidor_Origen.Length > 0 && stL_Clave_Emial_Origen.Length > 0 )
                        //
                        MailMessage msg = new MailMessage();
                        // Halla las cuentas de destino
                        for (int inL_Index = 1; inL_Index <= 50; inL_Index++)
                        { // inicio del for (int inL_Index = 1; inL_Index <= 50; inL_Index++)
                            stL_Email_Destino = "";
                            stL_Email_Destino = Obj_Conf.ReadAKeyFromSection(st_Seccion_Config, "Email_Destino_" + inL_Index.ToString());
                            //
                            if (stL_Email_Destino.Length > 0)
                            {
                                msg.To.Add(new MailAddress(stL_Email_Destino));
                            }
                            //
                        } // Fin de for (int inL_Index = 1; inL_Index <= 50; inL_Index++)
                        //
                        msg.From = new MailAddress(stL_Email_Origen.Trim());
                        //
                        msg.Subject = st_Asunto.Trim();
                        //
                        msg.Body = st_CuerpoMensaje.Trim();
                        // Carga los archivos adjuntos
                        for (int inL_Conta = 0; inL_Conta < st_ArchivosAdjuntos.Length; inL_Conta++)
                        { // Inicio del  for ( int inL_Conta = 0 ; in
                            if (st_ArchivosAdjuntos[inL_Conta] != null)
                            { // inicio del if (st_ArchivosAdjuntos[inL_Conta] != null)
                                if (st_ArchivosAdjuntos[inL_Conta].Length > 0)
                                {
                                    // Valida si existe el archivo, para incluirlo.
                                    if (File.Exists(st_ArchivosAdjuntos[inL_Conta]))
                                    {
                                        msg.Attachments.Add(new Attachment(st_ArchivosAdjuntos[inL_Conta]));
                                    }
                                }
                            } // Fin del if (st_ArchivosAdjuntos[inL_Conta] != null)
                        } // fin del  for ( int inL_Conta = 0 ; in
                        //
                        SmtpClient clienteSmtp = new SmtpClient(stL_Servidor_Origen.Trim());
                        // Puerto 465
                        clienteSmtp.Port = Convert.ToInt16(stL_Puerto);
                        //
                        clienteSmtp.EnableSsl = true;
                        // Si tiene definido el timeout lo asigna
                        if (stL_TimeOut.Length > 0)
                        {
                            clienteSmtp.Timeout = Convert.ToInt16(stL_TimeOut);
                        }
                        clienteSmtp.UseDefaultCredentials = false;
                        //
                        clienteSmtp.Credentials = new NetworkCredential(stL_Email_Origen.Trim(), stL_Clave_Emial_Origen);
                        //
                        //--Application.DoEvents();
                        clienteSmtp.Send(msg);
                        //--Application.DoEvents();
                        //
                        blL_Enviado = true;
                        //
                    } // Fin del if (stL_Email_Origen.Length > 0 && stL_Servidor_Origen.Length > 0 && stL_Clave_Emial_Origen.Length > 0 )
                    //
                }
                return blL_Enviado;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Send_An_EMail. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Send_An_EMail. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Send_An_EMail. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Send_An_EMail. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String BringMe_ID_Rep_Path(String User_ID, String ServerBasePath)
        {
            /// <summary>
            /// Encargado de devolver la ruta del reopsitorio tomando en cuenta la cedula y la ruta base del servidor.
            /// </summary>
            /// <param name="User_ID">Numero de cedula</param>
            /// <param name="ServerBasePath">Ruta base del servidor, ejemplo C:\\Biometrias\\</param>
            /// <returns>Devuelve la ruta de las biometrias para la cedula</returns>
            String stL_RutaRepo = "";
            String stL_Repo;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    int value = User_ID.Length - 2;
                    stL_Repo = User_ID.Substring(value, 2);
                    int numrepo = Convert.ToInt32(stL_Repo);
                    if (numrepo < 50) stL_Repo = "Repositorio1\\" + stL_Repo;
                    if (numrepo > 49) stL_Repo = "Repositorio2\\" + stL_Repo;
                    stL_RutaRepo = ServerBasePath + "\\Repositorios\\" + stL_Repo;
                }
                return stL_RutaRepo;
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ID_Rep_Path. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ID_Rep_Path. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return "";
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ID_Rep_Path. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "BringMe_ID_Rep_Path. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return "";
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void Put_Img_2_Ctr_Stream(String ImgPath, ref System.Drawing.Image Control2Manipulate)
        {
            /// <summary>
            /// Asigna la imagen de un archivo a un objeto tipo Imgae, via Streamreader
            /// </summary>
            /// <param name="ImgPath">Ruta y nombre del archivo de imagen</param>
            /// <param name="Control2Manipulate">Objeto a asignar la imagen</param>
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (File.Exists(ImgPath))
                    {
                        StreamReader StreamFoto = new StreamReader(ImgPath);
                        System.Drawing.Image Foto = System.Drawing.Image.FromStream(StreamFoto.BaseStream);
                        StreamFoto.Close();
                        //carga la imagen del stream en la imagen
                        Control2Manipulate = Foto;
                        //
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Put_Img_2_Ctr_Stream. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Put_Img_2_Ctr_Stream. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Put_Img_2_Ctr_Stream. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Put_Img_2_Ctr_Stream. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean Let_Server_To_DelBio(String User_ID,  String Server, int PortNumber, String User_Monitor = "", String Pass_User_Monitor = "")
        {
            Boolean blL_ProcesoOk = false;
            String ST_DIR_TEMPO_SERVIDOR = "C:\\kioskostmp\\";
            String stL_Usuario_Pandora = "mini";
            String stL_Clave_Usuario_Pandora = "cooper";
            try
            {
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    CLNBTN_CliPro TransBio = new CLNBTN_CliPro(Server, PortNumber, _st_User, _st_FileLog, _st_Lic);
                    if (TransBio.Connection_OK())
                    { // Inicio del if ( Transbio.testConexion())
                        if (User_Monitor.Length > 0)
                        {
                            stL_Usuario_Pandora = User_Monitor;
                        }
                        if (Pass_User_Monitor.Length > 0)
                        {
                            stL_Clave_Usuario_Pandora = Pass_User_Monitor;
                        }
                        //
                        String stL_Ruta_Temporal_Servidor = ST_DIR_TEMPO_SERVIDOR + User_ID + "\\";
                        //
                        string respu = TransBio.CMD2Dir(stL_Ruta_Temporal_Servidor);
                        //
                        String[] contenido = respu.Split(new char[] { '\n' });
                        Boolean blL_EvaluaOtroDir = false;
                        // Valida si hay archivos en el directorio
                        if (contenido.Length == 0)
                        {
                            blL_EvaluaOtroDir = true;
                        }
                        else
                        {
                            if (contenido[1] == "")
                            {
                                blL_EvaluaOtroDir = true;
                            }
                        }
                        if (blL_EvaluaOtroDir)
                        { // Inicio del if (blL_EvaluaOtroDir)
                            // Aqui le asigna la cedula nuevamente
                            stL_Ruta_Temporal_Servidor = stL_Ruta_Temporal_Servidor + User_ID + "\\";
                            //
                            respu = TransBio.CMD2Dir(stL_Ruta_Temporal_Servidor);
                            //
                            contenido = respu.Split(new char[] { '\n' });
                        } // Fin del if (blL_EvaluaOtroDir)
                        //
                        foreach (String archivo in contenido)
                        { // Inicio del foreach (String archivo in contenido)
                            blL_ProcesoOk = true;
                            if (!archivo.Contains("SA-DIR"))
                            {
                                String stL_Respuesta_DelFile = TransBio.CMD2DelServerFile(stL_Ruta_Temporal_Servidor + archivo.Trim(), stL_Usuario_Pandora, stL_Clave_Usuario_Pandora);
                            }
                        } // Fin del foreach (String archivo in contenido)
                    } // Fin del if ( Transbio.testConexion())
                    //
                }
                return blL_ProcesoOk;
            }
            //
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex_0.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Server_To_DelBio. System.AccessViolationException", "", ex_0.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Server_To_DelBio. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
                if (ex.InnerException == null)
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Server_To_DelBio. Exception", "", ex.Message.ToString());
                }
                else
                {
                    objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Let_Server_To_DelBio. Exception", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString());
                }
                return false;
            }
        }







    }
}
