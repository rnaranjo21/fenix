using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//====================================================
//| Downloaded From                                  |
//| Visual C# Kicks - http://www.vcskicks.com/       |
//| License - http://www.vcskicks.com/license.html   |
//====================================================
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
//
using System.IO;
using System.Runtime.ExceptionServices;


namespace Fenix
{
    public partial class FrmDetalleImagen : Form
    {
        private Rectangle selectedArea;
        private Image loadedImage;
        private Image thumbnail;
        private Color selectionColor;
        private Boolean blPr_Activo = false;
        private String stPr_UsuarioApp = ""; // Codigo del usuario de la aplicacion
        private String stPr_ArchivoLog = ""; // Nombre el Archivo Log.
        private String stPr_ExeName_Exe = "_C_ProgRes.dll"; // El nombre del exe
        private const string NOM_CLASE = "FrmDetalleImagen";
        //
        // Propiedades para manejar la salida del log
        private bool blPr_SalConsole = false; //variable tipo Boolean para definir si el log se imprime en la consola
        private bool blPr_SalLog = true;      //variable tipo Boolean para definir si el log se imprime en archivo ".log"
        private bool blPr_SalDialog = false;   //variable tipo bbolean para definir si el error de aplicacion se muestra en una ventana dialogo
        //
        private int inPr_Forma_Height_Ori = 0;
        private int inPr_Forma_Width_Ori = 0;
        //
        private int inPr_PicZoom_Height_Ori = 0;
        private int inPr_PicZoom_Width_Ori = 0;
        //
        private int inPr_Difer_Height_Ori = 0;
        private int inPr_Difer_Width_Ori = 0;

        private const int inrPr_Proporcion = 30;


        public FrmDetalleImagen()
        {
            InitializeComponent();

            //the zoom area in the thumbnail
            selectedArea = new Rectangle(0, 0, ((picZoom.Width * inrPr_Proporcion) / 100), ((picZoom.Height * inrPr_Proporcion) / 100));
            LblZoom.Visible = true;
            //
            inPr_Forma_Height_Ori = this.Height ;
            inPr_Forma_Width_Ori = this.Width;
            //
            inPr_PicZoom_Height_Ori = picZoom.Height;
            inPr_PicZoom_Width_Ori = picZoom.Width;
            //
            inPr_Difer_Height_Ori = inPr_Forma_Height_Ori - inPr_PicZoom_Height_Ori;
            inPr_Difer_Width_Ori = inPr_Forma_Width_Ori - inPr_PicZoom_Width_Ori;
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Stretches out a selected zoom area of an image
        /// </summary>
        private Image ZoomImage(Image input, Rectangle zoomArea, Rectangle sourceArea)
        {
            try
            {
                if (sourceArea.Width <= 0)
                {
                    sourceArea.Width = 1;
                }
                if (sourceArea.Height <= 0)
                {
                    sourceArea.Height = 1;
                }
                Bitmap newBmp = new Bitmap(sourceArea.Width, sourceArea.Height);
                //
                using (Graphics g = Graphics.FromImage(newBmp))
                {
                    //high interpolation
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.DrawImage(input, sourceArea, zoomArea, GraphicsUnit.Pixel);
                }
                //
                return newBmp;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ZoomImage. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
                return null;
            }
            catch (Exception ex)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ZoomImage", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Draws the selection rectangle on an image
        /// </summary>
        private Image MarkImage(Image input, Rectangle selectedArea, Color selectColor)
        {
            try
            {
                Bitmap newImg = new Bitmap(input.Width, input.Height);

                using (Graphics g = Graphics.FromImage(newImg))
                {
                    //Prevent using images internal thumbnail
                    input.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    input.RotateFlip(RotateFlipType.Rotate180FlipNone);

                    g.DrawImage(input, 0, 0);

                    //Draw the selection rect
                    using (Pen p = new Pen(selectColor))
                        g.DrawRectangle(p, selectedArea);
                }
                return (Image)newImg;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "MarkImage. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
                return null;
            }
            catch (Exception ex)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "MarkImage", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Resizes an image
        /// </summary>
        private Image ResizeImage(Image input, Size newSize, InterpolationMode interpolation)
        {
            try
            {
                Bitmap newImg = new Bitmap(newSize.Width, newSize.Height);

                using (Graphics g = Graphics.FromImage(newImg))
                {
                    //Prevent using images internal thumbnail
                    input.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    input.RotateFlip(RotateFlipType.Rotate180FlipNone);

                    //Interpolation
                    g.InterpolationMode = interpolation;

                    //Draw the image with the new dimensions
                    g.DrawImage(input, 0, 0, newSize.Width, newSize.Height);
                }
                //
                return (Image)newImg;
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ResizeImage. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
                return null;
            }
            catch (Exception ex)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ResizeImage", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
                return null;
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Returns the dominant color of an image
        /// </summary>
        private Color GetDominantColor(Bitmap bmp, bool includeAlpha)
        {
            try
            {
                // GDI+ still lies to us - the return format is BGRA, NOT ARGB.
                BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                               ImageLockMode.ReadWrite,
                                               PixelFormat.Format32bppArgb);

                int stride = bmData.Stride;
                IntPtr Scan0 = bmData.Scan0;

                int r = 0;
                int g = 0;
                int b = 0;
                int a = 0;
                int total = 0;

                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;

                    int nOffset = stride - bmp.Width * 4;
                    int nWidth = bmp.Width;

                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < nWidth; x++)
                        {
                            r += p[0];
                            g += p[1];
                            b += p[2];
                            a += p[3];

                            total++;

                            p += 4;
                        }
                        p += nOffset;
                    }
                }

                bmp.UnlockBits(bmData);

                r /= total;
                g /= total;
                b /= total;
                a /= total;

                if (includeAlpha)
                    return Color.FromArgb(a, r, g, b);
                else
                    return Color.FromArgb(r, g, b);
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "GetDominantColor. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
                return Color.FromArgb(0, 0, 0);
            }
            catch (Exception ex)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "GetDominantColor", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
                return Color.FromArgb(0, 0, 0);
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Calculates the opposite color of a given color. 
        /// Source: http://dotnetpulse.blogspot.com/2007/01/function-to-calculate-opposite-color.html
        /// </summary>
        /// <param name="clr"></param>
        /// <returns></returns>
        private Color CalculateOppositeColor(Color clr)
        {
            try
            {
                return Color.FromArgb(255 - clr.R, 255 - clr.G, 255 - clr.B);
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "CalculateOppositeColor. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
                return Color.FromArgb(0, 0, 0);
            }
            catch (Exception ex)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "CalculateOppositeColor", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
                return Color.FromArgb(0, 0, 0);
            }
        }


        [HandleProcessCorruptedStateExceptions]
        /// <summary>
        /// Constricts a set of given dimensions while keeping aspect ratio.
        /// </summary>
        private Size ShrinkToDimensions(int originalWidth, int originalHeight, int maxWidth, int maxHeight)
        {
            try
            {
                int newWidth = 0;
                int newHeight = 0;

                if (originalWidth >= originalHeight)
                {
                    //Match area width to max width
                    if (originalWidth <= maxWidth)
                    {
                        newWidth = originalWidth;
                        newHeight = originalHeight;
                    }
                    else
                    {
                        newWidth = maxWidth;
                        newHeight = originalHeight * maxWidth / originalWidth;
                    }
                }
                else
                {
                    //Match area height to max height
                    if (originalHeight <= maxHeight)
                    {
                        newWidth = originalWidth;
                        newHeight = originalHeight;
                    }
                    else
                    {
                        newWidth = originalWidth * maxHeight / originalHeight;
                        newHeight = maxHeight;
                    }
                }
                //
                return new Size(newWidth, newHeight);
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "resizePictureArea. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
                return new Size(1, 1);
            }
            catch (Exception ex)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "resizePictureArea", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
                return new Size(1, 1);
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private void resizePictureArea()
        {
            try
            {
                //Create a thumbnail image (maintaining aspect ratio)

                //Size newSize = ShrinkToDimensions(loadedImage.Width, loadedImage.Height, 160, 130);
                Size newSize = ShrinkToDimensions(loadedImage.Width, loadedImage.Height, 260, 211);
                //use low interpolation
                thumbnail = ResizeImage(loadedImage, new Size(newSize.Width, newSize.Height), InterpolationMode.Low);

                picSmall.Invalidate();
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "resizePictureArea. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "resizePictureArea", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void updateZoom()
        {
            try
            {
                if (loadedImage != null)
                {
                    //Map the area selected in the thumbail to the actual image size
                    Rectangle zoomArea = new Rectangle();
                    zoomArea.X = selectedArea.X * loadedImage.Width / thumbnail.Width;
                    zoomArea.Y = selectedArea.Y * loadedImage.Height / thumbnail.Height;
                    zoomArea.Width = selectedArea.Width * loadedImage.Width / thumbnail.Width;
                    zoomArea.Height = selectedArea.Height * loadedImage.Height / thumbnail.Height;

                    //Adjust the selected area to the current zoom value
                    zoomArea.Width /= tZoom.Value;
                    zoomArea.Height /= tZoom.Value;

                    picZoom.Image = ZoomImage(loadedImage, zoomArea, picZoom.ClientRectangle);
                    picZoom.Refresh();
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "updateZoom. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "updateZoom. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "updateZoom", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "updateZoom", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void picSmall_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (loadedImage != null)
                {
                    //Adjust the selected area to reflect the zoom value
                    Rectangle adjustedArea = new Rectangle();
                    adjustedArea.X = selectedArea.X;
                    adjustedArea.Y = selectedArea.Y;
                    adjustedArea.Width = selectedArea.Width / tZoom.Value;
                    adjustedArea.Height = selectedArea.Height / tZoom.Value;

                    //Draw the selected area on the thumbnail
                    picSmall.Image = MarkImage(thumbnail, adjustedArea, selectionColor);
                    if (!blPr_Activo)
                    {
                        blPr_Activo = true;
                        picZoom.Refresh();
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picSmall_Paint. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picSmall_Paint. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picSmall_Paint", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picSmall_Paint", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void picSmall_Click(object sender, EventArgs e)
        {
            try
            {
                //Update the selected area when the user clicks on the thumbnail
                Point mouseLoc = picSmall.PointToClient(Cursor.Position);

                selectedArea.X = mouseLoc.X - ((selectedArea.Width / tZoom.Value) / 2);
                selectedArea.Y = mouseLoc.Y - ((selectedArea.Height / tZoom.Value) / 2);
                //
                //selectedArea.X = mouseLoc.X ;
                //selectedArea.Y = mouseLoc.Y ;

                //Bound the box to the picture area bounds
                //if (selectedArea.Left < 0)
                //    selectedArea.X = 0;
                //else if (selectedArea.Right > picSmall.Width)
                //    selectedArea.X = picSmall.Width - selectedArea.Width - 1;

                //if (selectedArea.Top < 0)
                //    selectedArea.Y = 0;
                //else if (selectedArea.Bottom > picSmall.Height)
                //    selectedArea.Y = picSmall.Height - selectedArea.Height - 1;

                picSmall.Invalidate();
                updateZoom();
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picSmall_Click. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picSmall_Click. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picSmall_Click", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picSmall_Click", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
        }



        [HandleProcessCorruptedStateExceptions]
        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picSmall.Image = Image.FromFile(openFileDialog1.FileName);
                        loadedImage = Image.FromFile(openFileDialog1.FileName);

                        //Get a contrasting color for the image selection marker
                        using (Bitmap bmp = new Bitmap(loadedImage))
                        {
                            selectionColor = GetDominantColor(bmp, false);
                            selectionColor = CalculateOppositeColor(selectionColor);
                        }

                        tZoom.Value = 1;
                        //
                        resizePictureArea();
                        updateZoom();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid image");
                    }
                }
                //
                picSmall.Invalidate();
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "btnOpen_Click_1. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "btnOpen_Click_1. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "btnOpen_Click_1", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "btnOpen_Click_1", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void tZoom_Scroll(object sender, EventArgs e)
        {
            try
            {
                updateZoom();
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "tZoom_Scroll. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "tZoom_Scroll. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "tZoom_Scroll", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "tZoom_Scroll", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                // Fin Manejo de log
            }
        }


        [HandleProcessCorruptedStateExceptions]
        public void TomaParametros(String st_TituloForma, String st_UsuarioApp, String st_ArchivoLog, String st_ExeName_Exe, System.Drawing.Image ObjImagen, String st_FileNameImg)
        { // inicio del public void TomaParametros(
            try
            {
                this.Text = st_TituloForma;
                stPr_UsuarioApp = st_UsuarioApp;
                stPr_ArchivoLog = st_ArchivoLog;
                stPr_ExeName_Exe = st_ExeName_Exe;
                if (st_FileNameImg.Length == 0)
                {
                    this.picSmall.Image = ObjImagen;
                }
                else
                {
                    if (File.Exists(st_FileNameImg))
                    {
                        //this.picSmall.Image = Image.FromFile(st_FileNameImg);
                        picSmall.Image = Image.FromFile(st_FileNameImg);
                        loadedImage = Image.FromFile(st_FileNameImg);

                        //Get a contrasting color for the image selection marker
                        using (Bitmap bmp = new Bitmap(loadedImage))
                        {
                            selectionColor = GetDominantColor(bmp, false);
                            selectionColor = CalculateOppositeColor(selectionColor);
                        }
                        //
                        tZoom.Value = 1;
                        //
                        resizePictureArea();
                        selectedArea.X = -3;
                        selectedArea.Y = -2;
                        updateZoom();
                        Application.DoEvents();
                        picZoom.Refresh();
                        Application.DoEvents();
                    }
                    else
                    {
                        if (ObjImagen != null)
                        {
                            this.picSmall.Image = ObjImagen;
                            loadedImage = ObjImagen;

                            //Get a contrasting color for the image selection marker
                            using (Bitmap bmp = new Bitmap(loadedImage))
                            {
                                selectionColor = GetDominantColor(bmp, false);
                                selectionColor = CalculateOppositeColor(selectionColor);
                            }
                            //
                            tZoom.Value = 1;
                            //
                            resizePictureArea();
                            selectedArea.X = -3;
                            selectedArea.Y = -2;
                            updateZoom();
                            Application.DoEvents();
                            picZoom.Refresh();
                            Application.DoEvents();
                        }
                    }
                }
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ptbVista_Lateral_DoubleClick. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ptbVista_Lateral_DoubleClick. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ptbVista_Lateral_DoubleClick", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "ptbVista_Lateral_DoubleClick", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }

        [HandleProcessCorruptedStateExceptions]
        private void picZoom_Click(object sender, EventArgs e)
        {
            try
            {
                // NADA
            }
            catch (System.AccessViolationException ex_0)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picZoom_Click. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                // Fin Manejo de log
            }
            catch (Exception ex)
            {
                // Manejo de log
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "picZoom_Click", "", ex.Message.ToString(), "", "");
                // Fin Manejo de log
            }
        }


        [HandleProcessCorruptedStateExceptions]
        private void FrmDetalleImagen_Resize(object sender, EventArgs e)
        {
            try
            {
                //
                picZoom.Height = this.Height - inPr_Difer_Height_Ori ;
                picZoom.Width = this.Width - inPr_Difer_Width_Ori;
                //
                selectedArea.Width = (picZoom.Width * inrPr_Proporcion) / 100;
                selectedArea.Height = (picZoom.Height * inrPr_Proporcion) / 100;
                //
                this.updateZoom();
                //
            }
            catch (System.AccessViolationException ex_0)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex_0.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "FrmDetalleImagen_Resize. System.AccessViolationException", "", ex_0.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "FrmDetalleImagen_Resize. System.AccessViolationException", "", ex_0.Message.ToString() + " " + ex_0.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                ///////////////////////////////////////////////////////////////
                // Manejo de log
                ///////////////////////////////////////////////////////////////
                //
                _C_ProgRes.ClasX_EventLog objL_Log = new  _C_ProgRes.ClasX_EventLog(stPr_UsuarioApp, stPr_ArchivoLog, blPr_SalConsole, blPr_SalLog, blPr_SalDialog);
                //
                if (ex.InnerException == null)
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "FrmDetalleImagen_Resize", "", ex.Message.ToString(), "", "");
                }
                else
                {
                    objL_Log.outMensajError(stPr_ExeName_Exe, NOM_CLASE, "FrmDetalleImagen_Resize", "", ex.Message.ToString() + " " + ex.InnerException.Message.ToString(), "", "");
                }
                ///////////////////////////////////////////////////////////////
                // Fin Manejo de log
                ///////////////////////////////////////////////////////////////
            }
        }


        //private void CmdCerrar_Click(object sender, EventArgs e)
        //{
        //    // Cierra la forma
        //    //
        //    this.Close();
        //}

        //private void ZoomInOut(bool zoom)
        //{
        //    //Zoom ratio by which the images will be zoomed by default
        //    int zoomRatio = 10;
        //    //Set the zoomed width and height
        //    int widthZoom = picSmall.Width * zoomRatio / 100;
        //    int heightZoom = picSmall.Height * zoomRatio / 100;
        //    //zoom = true --> zoom in
        //    //zoom = false --> zoom out
        //    if (!zoom)
        //    {
        //        widthZoom *= -1;
        //        heightZoom *= -1;
        //    }
        //    //Add the width and height to the picture box dimensions
        //    picSmall.Width += widthZoom;
        //    picSmall.Height += heightZoom;
        //}

        //private void CmdMenos_Click(object sender, EventArgs e)
        //{
        //    picSmall.SizeMode = PictureBoxSizeMode.Zoom;
        //    this.ZoomInOut(false);
        //}

        //private void CmdMas_Click(object sender, EventArgs e)
        //{
        //    picSmall.SizeMode = PictureBoxSizeMode.Zoom;
        //    this.ZoomInOut(true);
        //}

        //private void CmdMas_Click_1(object sender, EventArgs e)
        //{
        //    this.CmdMas_Click( sender,  e);
        //}

        //private void CmdMenos_Click_1(object sender, EventArgs e)
        //{
        //    this.CmdMenos_Click( sender,  e);
        //}

             

        //private void FrmDetalleImagen_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar.ToString() == "+")
        //    {
        //        // Aumenta
        //        this.CmdMas_Click(sender, e);
        //    }
        //    else
        //    {
        //        if (e.KeyChar.ToString() == "-")
        //        {
        //            // Disminuye
        //            this.CmdMenos_Click(sender, e);
        //        }
        //    }
        //}   
    }
}
