using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBToolsNet
{
    class CLNBTN_Gp
    {
        private String _st_LicName = "";
        private String[] _st_LicNames = new String[4] { "cero_cinto_coqeeq_", "CaProVimpo", "InfoGx_lkfodi09_s0s0d" , "GcA Tech" };

        public CLNBTN_Gp(String LicName)
        {
            for (int i = 0 ; i < _st_LicNames.Length;i++)
            {
                if (_st_LicNames[i] == LicName)
                {
                    _st_LicName = LicName;
                    break;
                }
            }
        }

        public String getLicName()
        {
            return _st_LicName;
        }
    }
}
