using System;
using System.Drawing;


namespace OnlineShop.WebUI.Helpers
{
    public static class ImageHelper
    {
        public static byte[] CreateThumbnail(Image image, int width = 200, int height = 200)
        {
            var size = ResizeKeepAspect(image.Size, width, height);
            var thumbnail = (Image) (new Bitmap(image, size));

            using (var thumbStream = new System.IO.MemoryStream())
            {
                thumbnail.Save(thumbStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return thumbStream.GetBuffer();
            }
        }

        public static Size ResizeKeepAspect(this Size src, int maxWidth, int maxHeight, bool enlarge = false)
        {
            maxWidth = enlarge ? maxWidth : Math.Min(maxWidth, src.Width);
            maxHeight = enlarge ? maxHeight : Math.Min(maxHeight, src.Height);

            decimal rnd = Math.Min(maxWidth / (decimal)src.Width, maxHeight / (decimal)src.Height);
            return new Size((int)Math.Round(src.Width * rnd), (int)Math.Round(src.Height * rnd));
        }
    }
}