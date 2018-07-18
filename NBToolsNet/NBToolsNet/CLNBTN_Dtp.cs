using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.ExceptionServices;


namespace NBToolsNet
{
    public class CLNBTN_Dtp 
    {
        // Clase equivalente : ClasX_caDatosTemplate		
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = false;
        //
        private CLNBTN_Lg _Obj_Log = new CLNBTN_Lg();
        private CLNBTN_Tp[] _Templa = new CLNBTN_Tp[0];
        //
        private String _TemplaType = "HUELLA";//por defecto se describe como HUELLA. Los posibles valores son HUELLA, ROSTRO, IRIS
        private int _Size_Templa = 1024;//tamño del template
        private String[] _Id_Pess = new String[0];
        //
        private String _st_User = "CLNBTN_Lg";
        private String _st_FileLog = "C:\\Windows\\CLNBTN_Dtp.log"; 
        //
        private int _Request_Type = 1;//1-IDENTIFICACION, 2-VERIFICACION, 3-INSERCION
        //
        private String _st_Lic = "";
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Dtp";



        public CLNBTN_Dtp()
        {
            
        }

        public CLNBTN_Dtp(String usuario, String PathLog, String LicName)
        {
            CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
            else
            {
                _st_User = usuario;
                _st_FileLog = PathLog;
                _Obj_Log.setUser(_st_User);
                _Obj_Log.setPathErrLog(_st_FileLog);
            }
        }


        public void setTempla(CLNBTN_Tp[] dato)
        {
            // Método para asignar un arreglo de templates al arreglo de la clase
            _Templa = dato;
        }


        public CLNBTN_Tp[] getTempla()
        {
            // Método para obtener el arreglo de templates de la clase
            return _Templa;
        }


        public CLNBTN_Tp getTempla(int WhichOne)
        {
            // Método para obtener un template especifico del arreglo de la clase
            return _Templa[WhichOne];
        }



        [HandleProcessCorruptedStateExceptions]
        public void AddTempla(CLNBTN_Tp Datum)
        {
            // Método para asignar un único template al arreglo de la clase
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    CLNBTN_Tp[] stmps = new CLNBTN_Tp[_Templa.Length];
                    System.Array.Copy(_Templa, 0, stmps, 0, _Templa.Length);
                    _Templa = new CLNBTN_Tp[_Templa.Length + 1];
                    System.Array.Copy(stmps, 0, _Templa, 0, stmps.Length);
                    _Templa[_Templa.Length - 1] = Datum;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentNullException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla", "", "ArgumentNullException: " + e.Message, "", "");
            }
            catch (RankException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla", "", "RankException: " + e.Message, "", "");
            }
            catch (ArrayTypeMismatchException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla", "", "ArrayTypeMismatchException: " + e.Message, "", "");
            }
            catch (InvalidCastException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla", "", "InvalidCastException: " + e.Message, "", "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla", "", "ArgumentOutOfRangeException: " + e.Message, "", "");
            }
            catch (Exception ex_1)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla", "", "ArgumentOutOfRangeException: " + ex_1.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void AddTempla(int Identifier, byte[] BytesData)
        {
            // Sobrecarga del metodo para insertar un único Template,
            // adicionalmente asigna el valor del identificador del template
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    CLNBTN_Tp obj_templatetmp = new CLNBTN_Tp();
                    obj_templatetmp.setIdentifier(Identifier);
                    obj_templatetmp.setInfoTp(BytesData);
                    this.AddTempla(obj_templatetmp);
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla(2)", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception ex_1)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddTempla(2)", "", "System.AccessViolationException: " + ex_1.Message, "", "");
            }
        }


        public void setType(String TemplaType)
        {
            // Asigna el valor del tipo de template
            // HUELLA, ROSTRO, IRIRS
            _TemplaType = TemplaType;
        }

        public String getType()
        {
            // Devuelve el tipo de Template
            return _TemplaType;
        }

        public int getNumTempla()
        {
            // Devuelve un entero con el tamaño de la coleccion de Templates,
            // equivalente a el número de templates cargados
            return _Templa.Length;
        }



        [HandleProcessCorruptedStateExceptions]
        public long getTemplaSize(int WhichOne)
        {
            // Devuelve el tamaño de un Template específico dentro de la colección
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return 0;
                }
                else
                {
                    long tamano = _Templa.Length;
                    //return template[cualtemplate].getTemplate().length;
                    return _Templa[WhichOne]._InfoSize;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "getTemplaSize", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return 0;
            }
            catch (Exception ex_1)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "getTemplaSize", "", "Exception: " + ex_1.Message, "", "");
                return 0;
            }
        }


        public void setSizeTempla(int SizeTempla)
        {
            // Asigna el valor de la variable para establecer la velocidad de transmision
            _Size_Templa = SizeTempla;
        }

        public int getSizeTempla()
        {
            /// Obtiene el valor actual de la velocidad de transmision en Bytes
            return _Size_Templa;
        }


        public void setId_Pess(String[] Array_Id_Pess)
        {
            _Id_Pess = Array_Id_Pess;
        }

        public String[] getId_Pess()
        {
            return _Id_Pess;
        }


        [HandleProcessCorruptedStateExceptions]
        public void AddId_Pess(String Id_Pess)
        {
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    String[] stmps = new String[_Id_Pess.Length];
                    System.Array.Copy(_Id_Pess, 0, stmps, 0, _Id_Pess.Length);
                    _Id_Pess = new String[_Id_Pess.Length + 1];
                    System.Array.Copy(stmps, 0, _Id_Pess, 0, stmps.Length);
                    _Id_Pess[_Id_Pess.Length - 1] = Id_Pess;
                    String prueba = _Id_Pess[_Id_Pess.Length - 1];
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddId_Pess", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (ArgumentNullException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddId_Pess", "", "ArgumentNullException: " + e.Message, "", "");
            }
            catch (RankException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddId_Pess", "", "RankException: " + e.Message, "", "");
            }
            catch (ArrayTypeMismatchException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddId_Pess", "", "ArrayTypeMismatchException: " + e.Message, "", "");
            }
            catch (InvalidCastException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddId_Pess", "", "InvalidCastException: " + e.Message, "", "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddId_Pess", "", "ArgumentOutOfRangeException: " + e.Message, "", "");
            }
            catch (Exception ex_1)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "AddId_Pess", "", "Exception: " + ex_1.Message, "", "");
            }
        }


        public void setRequest_Type(int Request_Type)
        {
            _Request_Type = Request_Type;
        }

        public int getRequest_Type()
        {
            return _Request_Type;
        }



        [HandleProcessCorruptedStateExceptions]
        public void Convert2String(String String2Convert)
        {
            /// Convierte una cadena protocolo en datos para un objeto de tipo caDatosTemplate
            /// en formatos:
            /// TIPO BIOMETRIA|CANT TEMPLATES|TEM1|TEM2..n|tamaTransmit(1024)|CANT CEDUL|CED1|CED2..n
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    int inL_tamano = 0;
                    //int inL_posdato = -1;
                    Char[] delimitadores = { '|' };
                    String[] stL_datosmensage = String2Convert.Split(delimitadores);

                    if (stL_datosmensage.Length > 0)
                    {
                        _TemplaType = stL_datosmensage[0];
                        int tamaño;
                        Int32.TryParse(stL_datosmensage[1], out tamaño);
                        inL_tamano = tamaño;
                        _Templa = new CLNBTN_Tp[inL_tamano];
                        int posgen = 0;
                        for (int i = 0; i < _Templa.Length; i++)
                        {
                            posgen = i + 2;
                            _Templa[i] = new CLNBTN_Tp();
                            //template[i].tamano = Long.parseLong(sdatosmensage[posgen]);
                            long tempTamaño;
                            long.TryParse(stL_datosmensage[posgen], out tempTamaño);
                            _Templa[i]._InfoSize = tempTamaño;
                        }
                        posgen++;
                        int veltransmision;
                        Int32.TryParse(stL_datosmensage[posgen], out veltransmision);
                        _Size_Templa = veltransmision;
                        posgen++;
                        int datosmensaje;
                        Int32.TryParse(stL_datosmensage[posgen], out datosmensaje);
                        _Id_Pess = new String[datosmensaje];
                        for (int c = 0; c < _Id_Pess.Length; c++)
                        {
                            posgen++;
                            _Id_Pess[c] = stL_datosmensage[posgen];
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Convert2String", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Convert2String", "", "Exception: " + e.Message, "", "");
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public String IntoString()
        {
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return "";
                }
                else
                {
                    int inL_cantidad = _Templa.Length;
                    String stL_tamanos = "";
                    for (int i = 0; i < _Templa.Length; i++)
                    {
                        stL_tamanos += _Templa[i].getInfoTp().Length + "|";
                    }

                    int inL_cantidadCed = _Id_Pess.Length;
                    String stL_cedulas = "";
                    for (int c = 0; c < _Id_Pess.Length; c++)
                    {
                        stL_cedulas += "|" + _Id_Pess[c];
                    }
                    //
                    String stL_Trama = _TemplaType + "|" + Convert.ToString(inL_cantidad) + "|" + stL_tamanos + Convert.ToString(_Size_Templa) + "|" + Convert.ToString(inL_cantidadCed) + "" + stL_cedulas;
                    //
                    return stL_Trama;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Convert2String", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return null;
            }
            catch (Exception e)
            {
                _Obj_Log.setOutFileLog(true);
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "Convert2String", "", "Exception: " + e.Message, "", "");
                return null;
            }
        }






    }
}
