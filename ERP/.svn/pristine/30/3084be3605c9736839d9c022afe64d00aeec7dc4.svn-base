using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace MCC.Helpers
{
    public class ImageHelper
    {
        public System.Drawing.Image ImageReSize(string _imageUrl, int MaxHeight, string pathSave)
        {
            try
            {
                //uploadImageFile.PostedFile.InputSteam
                System.Drawing.Image oImg = System.Drawing.Image.FromFile(_imageUrl);

                int width;
                int height;

                if (oImg.Width > oImg.Height)
                {
                    width = MaxHeight;
                    height = MaxHeight * oImg.Height / oImg.Width;
                }
                else
                {
                    height = MaxHeight;
                    width = MaxHeight * oImg.Width / oImg.Height;
                }

                Size PictureThumbSize = new Size();
                PictureThumbSize.Height = height;
                PictureThumbSize.Width = width;

                System.Drawing.Image oThumbNail = new Bitmap(PictureThumbSize.Width, PictureThumbSize.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                Graphics oGraphic = Graphics.FromImage(oThumbNail);

                oGraphic.CompositingQuality = CompositingQuality.HighQuality;

                oGraphic.SmoothingMode = SmoothingMode.HighQuality;

                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                Rectangle oRectangle = new Rectangle(0, 0, PictureThumbSize.Width, PictureThumbSize.Height);

                oGraphic.DrawImage(oImg, oRectangle);
                oImg.Dispose();
                //oThumbNail.Save(sPhysicalPath + @"\" + newFileName, ImageFormat.Jpeg);
                var output = System.IO.File.Create(pathSave);
                oThumbNail.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg);
                oThumbNail.Dispose();
                return oThumbNail;

            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public static Bitmap Resize(int Maxwidth, int Maxheight, HttpPostedFileBase file, string pathfile)
        {
            var input = new Bitmap(file.InputStream);
            int width;
            int height;

            if (input.Width > input.Height)
            {
                width = Maxwidth;
                height = Maxheight * input.Height / input.Width;
            }
            else
            {
                height = Maxheight;
                width = Maxwidth * input.Width / input.Height;
            }
            var thumb = new Bitmap(width, height);
            var graphic = Graphics.FromImage(thumb);
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphic.DrawImage(input, 0, 0, width, height);
            var output = System.IO.File.Create(pathfile);
            thumb.Save(output, System.Drawing.Imaging.ImageFormat.Jpeg);
            graphic.Dispose();
            thumb.Dispose();
            output.Close();
            return input;
        }
    }
}