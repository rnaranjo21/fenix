using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public class CLNBTN_Es
    {
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = false;
        private string _st_User = "CLNBTN_Es";
        private string _st_FileLog = "C:\\Windows\\CLNBTN_Es.log"; 
        //
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Es";
        //
        private const String CHAVE_ES = "OkLexTY9";
        private String _st_Lic = "";

        public CLNBTN_Es(String LicName)
        {
            CLNBTN_Gp ObL_Lic = new CLNBTN_Gp(LicName);
            _st_Lic = ObL_Lic.getLicName();
            if (_st_Lic.Length == 0)
            {
                MessageBox.Show("Invalid Lic To work");
            }
        }

        public CLNBTN_Es(String UserName, String LogFile, String LicName)
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

        public CLNBTN_Es(String UserName, String LogFile, bool OutLineConsole, bool OutFileLog, bool OutWindow, String LicName)
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


        public void setFileLog(string FileLog)
        {
            _st_FileLog = FileLog;
        }

        public string getFileLog()
        {
            return _st_FileLog;
        }

        [HandleProcessCorruptedStateExceptions]
        public String File2Es(String File2Es, String TieYMDown, String Chave2Olho, String KMAssH, String LicName)
        { //
            String stL_File2Es = "";
            try
            { // Inicio del Try
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (Chave2Olho.Trim() == "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=")
                    {
                        RijndaelManaged rijndaelCipher = new RijndaelManaged();
                        rijndaelCipher.Mode = CipherMode.CBC;
                        rijndaelCipher.Padding = PaddingMode.PKCS7;
                        //
                        rijndaelCipher.KeySize = 0x80;
                        rijndaelCipher.BlockSize = 0x80;
                        byte[] pwdBytes = Encoding.UTF8.GetBytes(CHAVE_ES);
                        byte[] keyBytes = new byte[0x10];
                        int len = pwdBytes.Length;
                        if (len > keyBytes.Length)
                        {
                            len = keyBytes.Length;
                        }
                        Array.Copy(pwdBytes, keyBytes, len);
                        rijndaelCipher.Key = keyBytes;
                        rijndaelCipher.IV = keyBytes;
                        ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
                        byte[] plainText = Encoding.UTF8.GetBytes(File2Es);
                        stL_File2Es = Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
                    }
                }
                //
                return stL_File2Es;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Es(1). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                return stL_File2Es;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Es(1)", "", ex.Message.ToString(), "", "");
                return stL_File2Es;
            }
        }


               
        [HandleProcessCorruptedStateExceptions]
        public String File2Es(String File2Es, String TieYMDown, String Chave2Olho, String KMAssH, String LicName, String File2Es3)
        { //
            String stL_File2Es = "";
            try
            { // Inicio del Try
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                //
                return stL_File2Es;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Es(2). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                return stL_File2Es;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Es(2)", "", ex.Message.ToString(), "", "");
                return stL_File2Es;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String File2Es(String File2Es, String TieYMDown, String Chave2Olho, String KMAssH, String LicName, String File2Es3, String File2Es4)
        { //
            String stL_File2Es = "";
            try
            { // Inicio del Try
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                //
                return stL_File2Es;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Es(3). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                return stL_File2Es;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Es(3)", "", ex.Message.ToString(), "", "");
                return stL_File2Es;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String File2Es(String File2Es, String TieYMDown, String Chave2Olho, String KMAssH, String LicName, String File2Es3, String File2Es4, int MagicNumber)
        { //
            String stL_File2Es = "";
            try
            { // Inicio del Try
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                //
                return stL_File2Es;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Es(4). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                return stL_File2Es;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Es(4)", "", ex.Message.ToString(), "", "");
                return stL_File2Es;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String File2Des(String File2Des, String TieYMDown, String Chave2Olho, String KMAssH, String LicName)
        { //
            String stL_File2Des = "";
            try
            { // Inicio del Try
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    if (Chave2Olho.Trim() == "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=")
                    {
                        RijndaelManaged rijndaelCipher = new RijndaelManaged();
                        rijndaelCipher.Mode = CipherMode.CBC;
                        rijndaelCipher.Padding = PaddingMode.PKCS7;

                        rijndaelCipher.KeySize = 0x80;
                        rijndaelCipher.BlockSize = 0x80;
                        byte[] encryptedData = Convert.FromBase64String(File2Des);
                        byte[] pwdBytes = Encoding.UTF8.GetBytes(CHAVE_ES);
                        byte[] keyBytes = new byte[0x10];
                        int len = pwdBytes.Length;
                        if (len > keyBytes.Length)
                        {
                            len = keyBytes.Length;
                        }
                        Array.Copy(pwdBytes, keyBytes, len);
                        rijndaelCipher.Key = keyBytes;
                        rijndaelCipher.IV = keyBytes;
                        byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                        stL_File2Des = Encoding.UTF8.GetString(plainText);
                    }
                }
                return stL_File2Des;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Des(1). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                return stL_File2Des;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Des(1)", "", ex.Message.ToString(), "", "");
                return stL_File2Des;
            }
        }

        
        [HandleProcessCorruptedStateExceptions]
        public String File2Des(String File2Des, String TieYMDown, String Chave2Olho, String KMAssH, String LicName, String File2Es3)
        { //
            String stL_File2Des = "";
            try
            { // Inicio del Try
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                return stL_File2Des;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Des(2). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                return stL_File2Des;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Des(2)", "", ex.Message.ToString(), "", "");
                return stL_File2Des;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String File2Des(String File2Des, String TieYMDown, String Chave2Olho, String KMAssH, String LicName, String File2Es3, String File2Es4)
        { //
            String stL_File2Des = "";
            try
            { // Inicio del Try
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                return stL_File2Des;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Des(3). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                return stL_File2Des;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Des(3)", "", ex.Message.ToString(), "", "");
                return stL_File2Des;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public String File2Des(String File2Des, String TieYMDown, String Chave2Olho, String KMAssH, String LicName, String File2Es3, String File2Es4, int MagicNumber)
        { //
            String stL_File2Des = "";
            try
            { // Inicio del Try
                //
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                return stL_File2Des;
            } // Fin del Try
            catch (System.AccessViolationException ex_0)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Des(4). System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                return stL_File2Des;
            }
            catch (Exception ex)
            {
                CLNBTN_Lg objL_Log = new CLNBTN_Lg(_st_User, _st_FileLog, false, true, false);
                //
                objL_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Des(4)", "", ex.Message.ToString(), "", "");
                return stL_File2Des;
            }
        }



    }
}
