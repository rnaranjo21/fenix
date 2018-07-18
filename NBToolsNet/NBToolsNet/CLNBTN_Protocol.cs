using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBToolsNet
{
    public class CLNBTN_Protocol
    {
        public String comando = "";
        private String parametros = "";
        public String[] listaparametros;
        public String respuesta;
        public void setParametros(String datos)
        {
            parametros = datos;
            listaparametros = parametros.Split('|');
        }
        public String getParametros()
        {
            return parametros;
        }


    }
}
