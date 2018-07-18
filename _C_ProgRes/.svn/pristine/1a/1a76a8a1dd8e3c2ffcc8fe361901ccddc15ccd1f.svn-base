#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ExceptionServices;

#endregion

namespace _C_ProgRes
{
    /// <summary>
    /// Clase para el manejo de archivos, permite manipular archivos directamente
    /// </summary>
    public class ClasX_FileManager
    {
        #region campos privados
        /// <summary>
        /// ruta de origen a manipular
        /// </summary>
        private String StPr_srcFile;
        /// <summary>
        /// ruta destino, si llegan a haber movimientos en los mismos
        /// </summary>
        private String stPr_destFile;
        /// <summary>
        /// objeto para manejo de log
        /// </summary>
        private ClasX_EventLog objPr_Log;

        private String stPr_Info = "CaProVimpo";
        private NBToolsNet.CLNBTN_Fm ObjPr_Self = null;
        private String stPr_UsuarioApp = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.



        #endregion

        /// <summary>
        /// constructor de la clase
        /// </summary>
        /// <param name="stR_userApp">Usuario actual de la aplicacion</param>
        /// <param name="stR_pathLog">Ruta para almacenar el log de la clase</param>
        public ClasX_FileManager(String stR_userApp, String stR_pathLog)
        {
            stPr_UsuarioApp = stR_userApp;
            stPr_ArchivoLog = stR_pathLog;
            //
            objPr_Log = new ClasX_EventLog(stR_userApp, stR_pathLog, false, true, false);
            ObjPr_Self = new NBToolsNet.CLNBTN_Fm(stR_userApp, stR_pathLog, stPr_Info);
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Método para comprimir directorios o archivos solicita la ruta de origen, de donde se toma el directorio y el destino del archivo zip
        /// este último debe contener el nombre del archivo
        /// </summary>
        /// <param name="stR_StartPath">Ruta desde donde se toma el directorio a comprimir</param>
        /// <param name="stR_ZipPath">Destino del archivo comprimido, incluye el nombre del archivo y la extension</param>
        /// <returns>Retorna true si pudo realizar la operacion</returns>
        public Boolean ComprimirArchivo(String stR_StartPath, String stR_ZipPath)
        {
            StPr_srcFile = stR_StartPath;
            stPr_destFile = stR_ZipPath;
            try
            {
                return ObjPr_Self.File2Compress(stR_StartPath, stR_ZipPath, stPr_Info);
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "ComprimirArchivo", "",
                    "Error en la compresion del archivo " + stR_StartPath + " System.AccessViolationException: " + ex_0, "", "");
                return false;
            }
            catch (Exception e)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "ComprimirArchivo", "",
                    "Error en la compresion del archivo " + stR_StartPath + " Excepcion: " + e, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Método para descomprimir directorios, solicita la ruta de origen incluyendo el nombre del archivo ".zip" de donde se toma el directorio y el destino
        /// </summary>
        /// <param name="stR_ArchivoZIP">Ruta completa del archivo .zip</param>
        /// <param name="stR_RutaDescomp">Ruta final para almacenar el directorio descomprimido</param>
        /// <returns></returns>
        public Boolean DescomprimirArchivo(String stR_ArchivoZIP, String stR_RutaDescomp)
        {
            try
            {
                return ObjPr_Self.File2UnCompress(stR_ArchivoZIP, stR_RutaDescomp, stPr_Info);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "DescomprimirArchivo", "",
                    "Error en la descompresion del archivo " + stR_ArchivoZIP + " System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "DescomprimirArchivo", "",
                    "Error en la descompresion del archivo " + stR_ArchivoZIP + " Excepcion: " + ex.Message, "", "");
                return false;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// metodo para copiar directorios, recibe como parametro el directorio origen, el directorio destino 
        /// y una variable de tipo booleano que define si se copian tambien los subdirectorios.
        /// CopiarSubDirs = true copia todos los subdirectorios
        /// </summary>
        /// <param name="Origen">Ruta desde donde se toman el o los directorios a copiar</param>
        /// <param name="Destino">Ruta final donde se almacenaran el o los directorios originales</param>
        /// <param name="CopiarSubDirs">Define si se copian tambien los subdirectorios dentro del directorio principal a copiar</param>
        public void CopiarArchivos(String Origen, String Destino, bool CopiarSubDirs)
        {
            try
            {
                ObjPr_Self.CopyFiles(Origen, Destino, CopiarSubDirs, stPr_Info);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "CopiarArchivos. System.AccessViolationException",
                    "", "Problemas copiando los directorios. Exception: " + ex_0, "", "");
            }
            catch (Exception ex)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "CopiarArchivos",
                    "", "Problemas copiando los directorios. Exception: " + ex, "", "");
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : ComprimirArchivo_EncripBio
        /// Encargado de compactar un directorio en un archivo .zip, para biometrias
        /// </summary>
        /// <param name="stR_StartPath">Ruta completa del directorio a compactar</param>
        /// <param name="stR_ZipPath">Ruta y nombre del archivo .zip a generar</param>
        /// <returns>Devuelve TRUE si pudo realizar la operacion</returns>
        public Boolean ComprimirArchivo_EncripBio(String stR_StartPath, String stR_ZipPath)
        {
            // 
            StPr_srcFile = stR_StartPath;
            stPr_destFile = stR_ZipPath;
            try
            {
                return ObjPr_Self.File2Compress_Ext(stR_StartPath, stR_ZipPath, stPr_Info);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "ComprimirArchivo_EncripBio", "",
                    "Error en la compresion del archivo " + stR_StartPath + " System.AccessViolationException: " + ex_0, "", "");
                return false;
            }
            catch (Exception e)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "ComprimirArchivo_EncripBio", "",
                    "Error en la compresion del archivo " + stR_StartPath + " Excepcion: " + e, "", "");
                return false;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Metodo : DescomprimirArchivo_EncripBio
        /// Descompacta un archivo .zip en un directorio, para las biometrias.
        /// </summary>
        /// <param name="stR_ArchivoZIP">Ruta y nombre dek archivo a descompactar</param>
        /// <param name="stR_RutaDescomp">Ruta donbde se descompactan lo archivos</param>
        /// <returns>Devuelve TRUE si realiza la operacion</returns>
        public Boolean DescomprimirArchivo_EncripBio(String stR_ArchivoZIP, String stR_RutaDescomp)
        {
            try
            {
                return ObjPr_Self.File2UnCompress_Ext(stR_ArchivoZIP, stR_RutaDescomp, stPr_Info);
            }
            catch (System.AccessViolationException ex_0)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "DescomprimirArchivo_EncripBio", "",
                    "Error en la descompresion del archivo " + stR_ArchivoZIP + " System.AccessViolationException: " + ex_0.Message, "", "");
                return false;
            }
            catch (Exception ex)
            {
                objPr_Log.outMensajError("_C_ProgRes.DLL", "ClasX_FileManager", "DescomprimirArchivo_EncripBio", "",
                    "Error en la descompresion del archivo " + stR_ArchivoZIP + " Excepcion: " + ex.Message, "", "");
                return false;
            }
        }



    }
}
