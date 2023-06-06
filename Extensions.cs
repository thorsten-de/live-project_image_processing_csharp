using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace image_processor
{
    public static class Extensions
    {
        // Save the file with the appropriate format.
        public static void SaveImage(this Image image, string filename)
        {
            string extension = Path.GetExtension(filename);
            switch (extension.ToLower())
            {
                case ".bmp":
                    image.Save(filename, ImageFormat.Bmp);
                    break;
                case ".exif":
                    image.Save(filename, ImageFormat.Exif);
                    break;
                case ".gif":
                    image.Save(filename, ImageFormat.Gif);
                    break;
                case ".jpg":
                case ".jpeg":
                    image.Save(filename, ImageFormat.Jpeg);
                    break;
                case ".png":
                    image.Save(filename, ImageFormat.Png);
                    break;
                case ".tif":
                case ".tiff":
                    image.Save(filename, ImageFormat.Tiff);
                    break;
                default:
                    throw new NotSupportedException(
                        "Unknown file extension " + extension);
            }
        }

        public static Bitmap RotateAtCenter(this Bitmap bm, float angle,
            Color bgColor, InterpolationMode mode)
        {
            var result = new Bitmap(bm.Height, bm.Width);
            Graphics g = Graphics.FromImage(result);
            g.Clear(bgColor);
            g.InterpolationMode = mode;
            g.TranslateTransform(result.Width / 2f, result.Height / 2f);
            g.RotateTransform(angle);
            g.TranslateTransform(-result.Width / 2.0f, -result.Height / 2.0f);
            g.DrawImage(bm, 0,0);

            return result;
        }


    }
}
