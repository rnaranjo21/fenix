using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.ExceptionServices;

namespace NBToolsNet
{
    public class CLNBTN_Tp
    {

        public long _InfoSize = 0;
        private byte[] _InfoTp = new byte[0];
        private int _Identifier = -1; //-1- No usa, HUELLA #Dedos(01234,56789), IRIS(1,2)
        //
        public CLNBTN_Tp()
        {
            // NADA
        }

        public byte[] getInfoTp()
        {
            return _InfoTp;
        }

        public void setInfoTp(byte[] dato)
        {
            _InfoTp = dato;
        }

        public int getIdentifier()
        {
            return _Identifier;
        }

        public void setIdentifier(int dato)
        {
            _Identifier = dato;
        }








    }
}
