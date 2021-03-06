﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using _C_ProgRes;
using System.Data.SqlClient;

namespace _C_Fenix_Kiosko
{
    /// <summary>
    /// Clase para ejecutar SP's y Query en el servidor SQL SERVER
    /// </summary>
    /// <remarks> EJECUTA QUERYS EN SQL SERVER</remarks>
    public class ClasX_Consultas
    {
        #region campos privados
        /// <summary>
        /// Componente principal que se esta ejecutando
        /// </summary>
        private String stPr_Componente = "";
        /// <summary>
        /// Ruta de conexion a la BD local
        /// </summary>
        private String StPr_RutaBD = "";
        /// <summary>
        /// sentencia SQL a ejecutar
        /// </summary>
        private String stPr_Comando = "";
        private String[] stPr_ParametrosBD;
        /// <summary>
        /// Objeto de manejo de log
        /// </summary>
        private ClasX_EventLog objPr_EventLog = null;
        /// <summary>
        /// SQL Connection, para obtener conexiones a la BD SQL SERVER
        /// </summary>
        private SqlConnection ObjPr_conDatabase = null;
        /// <summary>
        /// SqlCommand, para construir sentencias para SQL SERVER
        /// </summary>
        SqlCommand ObjPr_cmdDatabase = null;
        /// <summary>
        /// SQL reader, para almacenar los resultados de las consultas
        /// </summary>
        SqlDataReader ObjPr_rdr = null;
        //

        private string stPr_ExeName_Exe = "_C_Fenix_Kiosko.dll"; // el nombre de la dll y la extensión:
        private const String NOM_CLASE = "ClasX_Consultas";
        //
        #endregion

        public ClasX_Consultas(String stL_Componente, ClasX_EventLog objR_EventLog, String[] parametros)
        {
            this.objPr_EventLog = objR_EventLog;
            this.stPr_ParametrosBD = parametros;
            this.stPr_Componente = stL_Componente;
            this.StPr_RutaBD = "SERVER=" + parametros[4] + ";" + "DATABASE=" + parametros[0] + ";" + "UID=" + parametros[7] + ";" + "PASSWORD=" + parametros[8] + ";" + "Connection timeout=30;";
       
        }

        public SqlDataReader EjecutaSP(String stR_NomSP, String stR_Parametro)
        {
            try
            {
                String stL_Sentencia = stR_NomSP + " " + stR_Parametro + " ";
                this.stPr_Comando = stL_Sentencia;
                ObjPr_conDatabase = new SqlConnection(StPr_RutaBD);
                ObjPr_conDatabase.Open();
                ObjPr_cmdDatabase = new SqlCommand(stPr_Comando, ObjPr_conDatabase);
                ObjPr_rdr = ObjPr_cmdDatabase.ExecuteReader();
                return ObjPr_rdr;
            }
            catch (Exception ex)
            {
                objPr_EventLog.outMensajError(stPr_ExeName_Exe , NOM_CLASE ,  "EjecutaSP", "", ex.Message, stPr_ParametrosBD[0], stPr_Comando);
                return null;
            }
        }
        public void CierraCOnexiones()
        {
            try
            {
                
                //Strail 21112014, Valida error que se presenta con objetos NULL
                if (ObjPr_rdr != null)
                {
                    ObjPr_rdr.Close();
                    ObjPr_rdr = null;
                }
                if (ObjPr_cmdDatabase != null)
                {
                    ObjPr_cmdDatabase.Dispose();
                    
                    ObjPr_cmdDatabase = null;
                }
                if (ObjPr_conDatabase != null)
                {
                    ObjPr_conDatabase.Close();
                    ObjPr_conDatabase = null; 
                }
            }
            catch (Exception ex)
            {
                objPr_EventLog.outMensajError(stPr_ExeName_Exe , NOM_CLASE ,  "CierraCOnexiones", "", ex.Message, "", "");
            }
        }
        public bool validarConexion()
        {
            try
            {
                String stL_ConectionString = StPr_RutaBD + "Connection Timeout = 30";
                ObjPr_conDatabase = new SqlConnection(stL_ConectionString);
                ObjPr_conDatabase.Open();
                ObjPr_conDatabase.Close();
                return true;
            }
            catch (Exception ex)
            {
                objPr_EventLog.outMensajError(stPr_ExeName_Exe , NOM_CLASE ,  "validarConexion", "", ex.Message, "ServerBD:" + stPr_ParametrosBD[4] + " NombreBD:" + stPr_ParametrosBD[0], "Abriendo conexion con Base de Datos");
                return false;
            }
        }
    }
}
