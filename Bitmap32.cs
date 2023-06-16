
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace image_processor
{
    public class Bitmap32: IDisposable
    {
        // Provide public access to the picture's byte data.
        public byte[] ImageBytes;
        public int RowSizeBytes;
        public const int PixelDataSize = 32;

        // A reference to the Bitmap.
        public Bitmap Bitmap;

        // True when locked.
        private bool m_IsLocked = false;
        public bool IsLocked
        {
            get
            {
                return m_IsLocked;
            }
        }

        // Save a reference to the bitmap.
        public Bitmap32(Bitmap bm)
        {
            Bitmap = bm;
        }

        // Bitmap data.
        private BitmapData m_BitmapData;

        // Return the image's dimensions.
        public int Width
        {
            get
            {
                return Bitmap.Width;
            }
        }
        public int Height
        {
            get
            {
                return Bitmap.Height;
            }
        }

        // Provide easy access to the color values.
        public void GetPixel(int x, int y, out byte red, out byte green, out byte blue, out byte alpha)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            blue = ImageBytes[i++];
            green = ImageBytes[i++];
            red = ImageBytes[i++];
            alpha = ImageBytes[i];
        }
        public void SetPixel(int x, int y, byte red, byte green, byte blue, byte alpha)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            ImageBytes[i++] = blue;
            ImageBytes[i++] = green;
            ImageBytes[i++] = red;
            ImageBytes[i] = alpha;
        }
        public byte GetBlue(int x, int y)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            return ImageBytes[i];
        }
        public void SetBlue(int x, int y, byte blue)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            ImageBytes[i] = blue;
        }
        public byte GetGreen(int x, int y)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            return ImageBytes[i + 1];
        }
        public void SetGreen(int x, int y, byte green)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            ImageBytes[i + 1] = green;
        }
        public byte GetRed(int x, int y)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            return ImageBytes[i + 2];
        }
        public void SetRed(int x, int y, byte red)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            ImageBytes[i + 2] = red;
        }
        public byte GetAlpha(int x, int y)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            return ImageBytes[i + 3];
        }
        public void SetAlpha(int x, int y, byte alpha)
        {
            int i = y * m_BitmapData.Stride + x * 4;
            ImageBytes[i + 3] = alpha;
        }

        // Lock the bitmap's data.
        public void LockBitmap()
        {
            // If it's already locked, do nothing.
            if (IsLocked) return;

            // Lock the bitmap data.
            Rectangle bounds = new Rectangle(
                0, 0, Bitmap.Width, Bitmap.Height);
            m_BitmapData = Bitmap.LockBits(bounds,
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            RowSizeBytes = m_BitmapData.Stride;

            // Allocate room for the data.
            int total_size = m_BitmapData.Stride * m_BitmapData.Height;
            ImageBytes = new byte[total_size];

            // Copy the data into the ImageBytes array.
            Marshal.Copy(m_BitmapData.Scan0, ImageBytes, 0, total_size);

            // It is now locked.
            m_IsLocked = true;
        }

        // Copy the data back into the Bitmap
        // and release resources.
        public void UnlockBitmap()
        {
            // If it's already unlocked, do nothing.
            if (!IsLocked) return;

            // Copy the data back into the bitmap.
            int total_size = m_BitmapData.Stride * m_BitmapData.Height;
            Marshal.Copy(ImageBytes, 0, m_BitmapData.Scan0, total_size);

            // Unlock the bitmap.
            Bitmap.UnlockBits(m_BitmapData);

            // Release resources.
            ImageBytes = null;
            m_BitmapData = null;

            // It is now unlocked.
            m_IsLocked = false;
        }

        public void Dispose()
        {
            UnlockBitmap();
        }

        public static Bitmap32 operator -(Bitmap32 lhs, Bitmap32 rhs) =>
            op(lhs, rhs, (l, r) => l - r);

        public static Bitmap32 operator +(Bitmap32 lhs, Bitmap32 rhs) =>
            op(lhs, rhs, (l, r) => l + r);


        private static Bitmap32 op(Bitmap32 lhs, Bitmap32 rhs, Func<byte, byte, int> op)
        {
            int width = Math.Min(lhs.Width, rhs.Width);
            int height = Math.Min(lhs.Height, rhs.Height);

            Bitmap bm = new Bitmap(width, height);
            Bitmap32 target = new Bitmap32(bm);
            lhs.LockBitmap();
            rhs.LockBitmap();
            target.LockBitmap();

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    lhs.GetPixel(x, y, out byte r1, out byte g1, out byte b1, out byte a1);
                    rhs.GetPixel(x, y, out byte r2, out byte g2, out byte b2, out byte a2);
                    target.SetPixel(x, y,
                        op(r1, r2).ToByte(),
                        op(g1, g2).ToByte(),
                        op(b1, b2).ToByte(),
                        op(a1, a2).ToByte());
                }
            });

            lhs.UnlockBitmap();
            rhs.UnlockBitmap();
            target.UnlockBitmap();
            return target;
        }

        public static Bitmap32 operator *(float factor, Bitmap32 source) =>
            source * factor;

        public static Bitmap32 operator *(Bitmap32 source, float factor)
        {
            int width = source.Width;
            int height = source.Height;

            Bitmap bm = new Bitmap(width, height);
            Bitmap32 target = new Bitmap32(bm);
            source.LockBitmap();
            target.LockBitmap();

            Parallel.For(0, height, y =>
            {
                for (int x = 0; x < width; x++)
                {
                    source.GetPixel(x, y, out byte r, out byte g, out byte b, out byte a);
                    target.SetPixel(x, y,
                        (r * factor).ToByte(),
                        (g * factor).ToByte(),
                        (b * factor).ToByte(),
                        (b * factor).ToByte());
                }
            });

            source.UnlockBitmap();
            target.UnlockBitmap();
            return target;
        }
    }
}
