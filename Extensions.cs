using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using ImageProcessor;
using System.Threading.Tasks;

namespace image_processor
{
    public delegate void PointOp(ref byte r, ref byte g, ref byte b, ref byte a);

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
            g.DrawImage(bm, 0, 0);

            return result;
        }

        public static Bitmap Scale(this Bitmap bm, float scale, InterpolationMode mode) =>
            Scale(bm, scale, scale, mode);

        public static Bitmap Scale(this Bitmap bm, float scaleX, float scaleY,
            InterpolationMode mode)
        {
            int width = (int)(scaleX * bm.Width);
            int height = (int)(scaleY * bm.Height);
            var result = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(result);
            g.InterpolationMode = mode;
            g.DrawImage(bm, new[] {
                new Point(0, 0),
                new Point(width, 0),
                new Point(0, height)});

            return result;
        }

        public static Bitmap Crop(this Image image, Rectangle rect, InterpolationMode mode)
        {
            var result = new Bitmap(rect.Width, rect.Height);
            var g = Graphics.FromImage(result);
            g.InterpolationMode = mode;

            g.DrawImage(image,
                destPoints: new[] {
                    new Point(0, 0),
                    new Point(rect.Width, 0),
                    new Point(0, rect.Height)
                },
                srcRect: rect,
                srcUnit: GraphicsUnit.Pixel);

            return result;
        }

        public static void DrawDashedRectangle(this Graphics g, Rectangle rect, Color color1, Color color2,
            float thickness, float dashSize)
        {
            using (Pen pen = new Pen(color1, thickness))
            {
                g.DrawRectangle(pen, rect);
                pen.DashPattern = new float[] { dashSize, dashSize };
                pen.Color = color2;
                g.DrawRectangle(pen, rect);
            }
        }

        public static Rectangle ToRectangle(this Point origin, Point other) =>
            new Rectangle(
            Math.Min(origin.X, other.X),
            Math.Min(origin.Y, other.Y),
            Math.Abs(other.X - origin.X),
            Math.Abs(other.Y - origin.Y));

        #region point operations


        public static void ApplyPointOp(this Bitmap bm, PointOp op)
        {
            Bitmap32 bm32 = new Bitmap32(bm);
            bm32.LockBitmap();

            int height = bm.Height;
            int width = bm.Width;
            Parallel.For(0, width, x =>
            {
                for (int y = 0; y < height; y++)
                {
                    bm32.GetPixel(x, y, out byte r, out byte b, out byte g, out byte a);
                    op(ref r, ref g, ref b, ref a);
                    bm32.SetPixel(x, y, r, g, b, a);
                }
            });

            bm32.UnlockBitmap();
        }

        #endregion
    }
}
