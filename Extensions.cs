using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (Bitmap32 bm32 = bm.UseLockedData())
            {
                int height = bm.Height;
                int width = bm.Width;
                Parallel.For(0, height, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        bm32.GetPixel(x, y, out byte r, out byte g, out byte b, out byte a);
                        op(ref r, ref g, ref b, ref a);
                        bm32.SetPixel(x, y, r, g, b, a);
                    }
                });
            }

            stopwatch.Stop();
            Debug.WriteLine($"PointOperation done in {stopwatch.ElapsedMilliseconds} ms");
        }

        public static void ApplyPointMatrix(this Bitmap bm, float[] m)
        {
            bm.ApplyPointOp((ref byte r, ref byte g, ref byte b, ref byte a) =>
            {
                float newR = r * m[0] + g * m[1] + b * m[2];
                float newG = r * m[3] + g * m[4] + b * m[5];
                float newB = r * m[6] + g * m[7] + b * m[8];
                r = (byte)Math.Min(255, (int)newR);
                g = (byte)Math.Min(255, (int)newG);
                b = (byte)Math.Min(255, (int)newB);
            });
        }

        public static byte ToByte(this float f)
        {
            if (f < 0) return 0;
            if (f > 255) return 255;
            return (byte)Math.Round(f);
        }

        public static byte ToByte(this int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return (byte)i;
        }

        #endregion

        #region Kernel operations

        public static Bitmap ApplyKernel(this Bitmap bm, float[,] kernel, float weight, float offset)
        {
            int height = bm.Height;
            int width = bm.Width;
            int xRadius = kernel.GetLength(0) / 2;
            int yRadius = kernel.GetLength(1) / 2;

            Bitmap32 source = new Bitmap32(bm);

            byte weigthPixel(float value)
            {
                return (value / weight + offset).ToByte();
            }

            void getPixel(int x, int y, out byte r, out byte g, out byte b)
            {
                int px = x, py = y;

                if (px < 0) px = 0;
                if (px >= width) px = width - 1;

                if (py < 0) py = 0;
                if (py >= height) py = height - 1;

                source.GetPixel(px, py, out r, out g, out b, out byte _);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            source.LockBitmap();
            Bitmap result = new Bitmap(width, height);
            
            using (Bitmap32 target = result.UseLockedData())
            {
                Parallel.For(0, height, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        float sumR = 0, sumG = 0, sumB = 0;
                        source.GetPixel(x, y, out _, out _, out _, out byte a);

                        for (int ky = -yRadius; ky <= yRadius; ky++)
                        {
                            for (int kx = -xRadius; kx <= xRadius; kx++)
                            {
                                getPixel(x + kx, y + ky, out byte r, out byte g, out byte b);

                                float scale = kernel[kx + xRadius, ky + xRadius];
                                sumR += r * scale;
                                sumG += g * scale;
                                sumB += b * scale;
                            }
                        }

                        target.SetPixel(x, y,
                            weigthPixel(sumR),
                            weigthPixel(sumG),
                            weigthPixel(sumB),
                            a);
                    }
                });
            }

            source.UnlockBitmap();

            stopwatch.Stop();
            Debug.WriteLine($"KernelOperation done in {stopwatch.ElapsedMilliseconds} ms");
            return result;
        }

        public static float[,] OnesArray(int radius)
        {
            int width = 2 * radius + 1;
            float[,] result = new float[width, width];
            for (int y = 0; y < width; y++)
                for (int x = 0; x < width; x++)
                    result[x, y] = 1f;

            return result;
        }

        public static Bitmap BoxBlur(this Bitmap bm, int radius)
        {
            var kernel = OnesArray(radius);
            return bm.ApplyKernel(kernel, kernel.Length, 0);
        }

        // Unsharp masking via original + (original - blurred) * amount
        public static Bitmap UnsharpMask(this Bitmap bm, int radius, float amount)
        {
            Bitmap32 original = new Bitmap32(bm);
            Bitmap32 blurred = new Bitmap32(bm.BoxBlur(radius));

            Bitmap32 result = original + (original - blurred) * amount;

            return result.Bitmap;
        }

        public static Bitmap RankFilter(this Bitmap bm, int xRadius, int yRadius, int rank)
        {
            int height = bm.Height;
            int width = bm.Width;
            Bitmap32 source = new Bitmap32(bm);

            IEnumerable<PixelData> WindowFunction(int x, int y)
            {
                for (int ky = y - yRadius; ky <= y + yRadius ; ky++)
                {
                    for (int kx = x - xRadius; kx <= x + xRadius; kx++)
                    {
                        int px = kx;
                        int py = ky;

                        if (px < 0) px = 0;
                        if (px >= width) px = width - 1;

                        if (py < 0) py = 0;
                        if (py >= height) py = height - 1;
                        source.GetPixel(px, py, out byte r, out byte g, out byte b, out byte a);
                        yield return new PixelData(r, g, b, a);
                    }
                }
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            source.LockBitmap();

            Bitmap result = new Bitmap(width, height);
            using (Bitmap32 target = result.UseLockedData())
            {
                Parallel.For(0, height, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = 
                            WindowFunction(x, y)
                            .OrderBy(pd => pd.Brightness)
                            .ElementAt(rank);

                        target.SetPixel(x, y, pixel.R, pixel.G, pixel.B, pixel.A);
                    }
                });

            }

            source.UnlockBitmap();

            stopwatch.Stop();
            Debug.WriteLine($"RankOperation done in {stopwatch.ElapsedMilliseconds} ms");
            return result;
        }

        #endregion

        public static float AdjustValue(this float value, float factor)
        {
            if (factor < 1) return value * factor;

            return 1 - (1 - value) * (2f - factor);
        }

        private static Bitmap32 UseLockedData(this Bitmap bm)
        {
            Bitmap32 bm32 = new Bitmap32(bm);
            bm32.LockBitmap();
            return bm32;
        }
    }

}
