﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FENIX_KIOSCO_1._0
{
    class encima
    {
        // Constantes para SetWindowsPos
        //   Valores de wFlags
        const int SWP_NOSIZE = 0x1;
        const int SWP_NOMOVE = 0x2;
        const int SWP_NOACTIVATE = 0x10;
        const int wFlags = SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE;
        //   Valores de hwndInsertAfter
        const int HWND_TOPMOST = -1;
        const int HWND_NOTOPMOST = -2;
        //
        /// <summary>
        /// Para mantener la ventana siempre visible
        /// </summary>
        /// <remarks>No utilizamos el valor devuelto</remarks>
        [DllImport("user32.DLL")]
        private extern static void SetWindowPos(
            int hWnd, int hWndInsertAfter,
            int X, int Y,
            int cx, int cy,
            int wFlags);

        public  void SiempreEncima(int handle)
        {
            SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, wFlags);
        }

        public  void NoSiempreEncima(int handle)
        {
            SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, 0, 0, wFlags);
        }
    }
}
