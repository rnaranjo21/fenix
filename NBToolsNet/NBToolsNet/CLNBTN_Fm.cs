using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public class CLNBTN_Fm
    {
        // Clase Equivalante : ClasX_FileManager		
        private bool _bl_OutFileLog = true;
        private bool _bl_OutLineConsole = false;
        private bool _bl_OutWindow = true;
        private string _st_User = "CLNBTN_Fm";
        private string _st_FileLog = "C:\\Windows\\CLNBTN_Fm.log"; 
        //
        private String _st_srcFile;
        private String _st_destFile;
        private CLNBTN_Lg _Obj_Log;
        private String _st_Lic = "";
        //
        private const String _st_Relac = "NBToolsNet.dll";
        private const String _st_RelacSon = "CLNBTN_Fm";
        //

        public CLNBTN_Fm(String UserName, String LogFile, String LicName)
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
                //
                _Obj_Log = new CLNBTN_Lg(UserName, LogFile, _bl_OutLineConsole, _bl_OutFileLog, _bl_OutWindow);
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public Boolean File2Compress(String PathFile, String PathZipFile, String LicName)
        {
            _st_srcFile = PathFile;
            _st_destFile = PathZipFile;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return false;
                }
                else
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddDirectory(_st_srcFile);
                        zip.Save(_st_destFile);
                    }
                    return true;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Compress", "", "Error Compressing Files of " + _st_srcFile + " System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Compress", "", "Error Compressing Files of " + _st_srcFile + " Excepcion: " + e.Message, "", "");
                return false;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public Boolean File2UnCompress(String PathZipFile, String PathUnCompress, String LicName)
        {
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return false;
                }
                else
                {
                    using (ZipFile zip = ZipFile.Read(PathZipFile))
                    {
                        zip.ExtractAll(PathUnCompress);
                        zip.Dispose();
                    }
                    return true;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2UnCompress", "", "Error UnCompressing Files of " + PathZipFile + " System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2UnCompress", "", "Error UnCompressing Files of " + PathZipFile + " Excepcion: " + e.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void CopyFiles(String Source, String Target, bool ToCopySubDirs, String LicName)
        {
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(Source);
                    DirectoryInfo[] dirs = dir.GetDirectories();

                    if (!dir.Exists)
                    {
                        throw new DirectoryNotFoundException("Source Directory Not Found: " + Source);
                    }
                    if (!Directory.Exists(Target))
                    {
                        Directory.CreateDirectory(Target);
                    }
                    FileInfo[] files = dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        string temppath = Path.Combine(Target, file.Name);
                        file.CopyTo(temppath, false);
                    }
                    if (ToCopySubDirs)
                    {
                        foreach (DirectoryInfo subdir in dirs)
                        {
                            string temppath = Path.Combine(Target, subdir.Name);
                            CopyFiles(subdir.FullName, temppath, ToCopySubDirs, LicName);
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CopyFiles", "", "System.AccessViolationException: " + ex_0.Message, "", "");
            }
            catch (Exception e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "CopyFiles", "", "Excepcion: " + e.Message, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean File2Compress_Ext(String PathFile, String PathZipFile, String LicName)
        {
            // 
            _st_srcFile = PathFile;
            _st_destFile = PathZipFile;
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return false;
                }
                else
                {
                    String stL_Archivo_Sin_Extension = PathZipFile.Substring(PathZipFile.LastIndexOf("\\") + 1);
                    stL_Archivo_Sin_Extension = stL_Archivo_Sin_Extension.Substring(0, stL_Archivo_Sin_Extension.IndexOf("."));
                    //
                    CLNBTN_Es ObjL_Encrip = new CLNBTN_Es(_st_User, _st_FileLog, false, true, false, LicName);
                    String stL_Encriptado = ObjL_Encrip.File2Es("SGA-" + stL_Archivo_Sin_Extension + "-APQ", "Frgtyhw", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "Oklijdiue", LicName);
                    //
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.Password = stL_Encriptado;
                        zip.AddDirectory(_st_srcFile);
                        zip.Save(_st_destFile);
                    }
                    return true;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Compress_Ext", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2Compress_Ext", "", "Excepcion: " + e.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public Boolean File2UnCompress_Ext(String PathZipFile, String PathUnCompress, String LicName)
        {
            try
            {
                if (_st_Lic.Length == 0)
                {
                    MessageBox.Show("Invalid Lic To work");
                    return false;
                }
                else
                {
                    String stL_Archivo_Sin_Extension = PathZipFile.Substring(PathZipFile.LastIndexOf("\\") + 1);
                    stL_Archivo_Sin_Extension = stL_Archivo_Sin_Extension.Substring(0, stL_Archivo_Sin_Extension.IndexOf("."));
                    //
                    CLNBTN_Es ObjL_Encrip = new CLNBTN_Es(_st_User, _st_FileLog, false, true, false, LicName);
                    String stL_DesEncriptado = ObjL_Encrip.File2Es("SGA-" + stL_Archivo_Sin_Extension + "-APQ", "Frgtyhw", "FT/yQYmins06srbyMggYjcEY/ns2slWTURobdSariTY=+-6aUVQ2SZO7QHT6kUHtr2zRbupap5KPu4jeO9GE+UMnk=", "Oklijdiue", LicName);
                    //
                    using (ZipFile zip = ZipFile.Read(PathZipFile))
                    {
                        zip.Password = stL_DesEncriptado;
                        zip.ExtractAll(PathUnCompress);
                        zip.Dispose();
                    }
                    return true;
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2UnCompress_Ext", "", "System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception e)
            {
                _Obj_Log.WriteOutErrorMessage(_st_Relac, _st_RelacSon, "File2UnCompress_Ext", "", "Excepcion: " + e.Message, "", "");
                return false;
            }
        }




    }
}
