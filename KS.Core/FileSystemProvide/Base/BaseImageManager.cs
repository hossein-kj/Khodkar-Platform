using System.Drawing;

namespace KS.Core.FileSystemProvide.Base
{
    public abstract class BaseImageManager
    {
        public virtual Image CreateThumbnail(Image imgPhoto, int width = 0, int height = 0, int compression = 0)
        {
           Bitmap bmpOut = null;

                var format = imgPhoto.RawFormat;

                decimal ratio;
                var newWidth = 0;
                var newHeight = 0;

                //*** If the image is smaller than a thumbnail just return it
                if (imgPhoto.Width < width && imgPhoto.Height < height)
                    return imgPhoto;

                if (imgPhoto.Width > imgPhoto.Height)
                {
                    ratio = (decimal)width / imgPhoto.Width;
                    newWidth = width;
                    decimal lnTemp = imgPhoto.Height * ratio;
                    newHeight = (int)lnTemp;
                }
                else
                {
                    ratio = (decimal)height / imgPhoto.Height;
                    newHeight = height;
                    decimal lnTemp = imgPhoto.Width * ratio;
                    newWidth = (int)lnTemp;
                }
                bmpOut = new Bitmap(newWidth, newHeight);
                var g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);
                g.DrawImage(imgPhoto, 0, 0, newWidth, newHeight);

                imgPhoto.Dispose();


            return bmpOut;
        }

        public virtual Image CreateExteraSmall(Image imgPhoto, int width = 0, int height = 0, int compression = 0)
        {
            throw new System.NotImplementedException();
        }

        public virtual Image CreateSmall(Image imgPhoto, int width = 0, int height = 0, int compression = 0)
        {
            throw new System.NotImplementedException();
        }

        public virtual Image CreateMedium(Image imgPhoto, int width = 0, int height = 0, int compression = 0)
        {
            throw new System.NotImplementedException();
        }

        public virtual Image CreateExteraLarg(Image imgPhoto, int width = 0, int height = 0, int compression = 0)
        {
            throw new System.NotImplementedException();
        }

        public virtual Image AddWaterMarkFromImage(Image imgPhoto, Image imgWaterMark, int width = 0, int height = 0, int x = 0, int y = 0)
        {
            throw new System.NotImplementedException();
        }
        public virtual Image AddWaterMarkFromText(Image imgPhoto, string waterMarkText, int width = 0, int height = 0, int x = 0, int y = 0)
        {
            throw new System.NotImplementedException();
        }

        public virtual Image Resize(Image imgPhoto, int width=0, int height=0, int percent = 0)
        {
            throw new System.NotImplementedException();
        }
    }
}
