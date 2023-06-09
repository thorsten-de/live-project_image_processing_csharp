using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor
{
    public class Bitmap32 : IDisposable
    {
        private readonly Bitmap source;
        private IntPtr iptr = IntPtr.Zero;
        private BitmapData bitmapData;

        private bool locked = false;
        private bool unlocked = false;
        private static readonly object lockObject = new object();
        private static readonly object unlockObject = new object();

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Bitmap32(Bitmap source)
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBitmap()
        {
            try
            {
                // double-checked lock
                if (locked)
                {
                    return;
                }

                lock (lockObject)
                {
                    if (locked)
                    {
                        return;
                    }
                    // Get width and height of bitmap
                    Width = source.Width;
                    Height = source.Height;

                    // get total locked pixels count
                    var pixelCount = Width * Height;

                    // Create rectangle to lock
                    var rect = new Rectangle(0, 0, Width, Height);

                    // get source bitmap pixel format size
                    Depth = Image.GetPixelFormatSize(source.PixelFormat);

                    // Check if bpp (Bits Per Pixel) is 24, or 32
                    if (Depth != 24 && Depth != 32)
                    {
                        throw new ArgumentException("Only 24 and 32 bpp images are supported.");
                    }

                    // Lock bitmap and return bitmap data
                    bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                                 source.PixelFormat);

                    // create byte array to copy pixel values
                    var step = Depth / 8;
                    Pixels = new byte[pixelCount * step];
                    iptr = bitmapData.Scan0;

                    // Copy data from pointer to array
                    Marshal.Copy(iptr, Pixels, 0, Pixels.Length);
                    locked = true;
                }
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBitmap()
        {
            if (Pixels == null) return;
            if (bitmapData == null) return;

            try
            {
                if (unlocked)
                {
                    return;
                }
                lock (unlockObject)
                {
                    if (unlocked)
                    {
                        return;
                    }
                    // Copy data from byte array to pointer
                    Marshal.Copy(Pixels, 0, iptr, Pixels.Length);

                    // Unlock bitmap data
                    source.UnlockBits(bitmapData);
                    unlocked = true;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public void GetPixel(int x, int y, out byte r, out byte b, out byte g, out byte a)
        {
            // Get color components count
            var cCount = Depth / 8;

            // Get start index of the specified pixel
            var i = ((y * Width) + x) * cCount;

            r = g = b = a = 0;

            if (Pixels == null) return;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 BPP get Red, Green, Blue and Alpha
            {
                b = Pixels[i];
                g = Pixels[i + 1];
                r = Pixels[i + 2];
                a = Pixels[i + 3];
            }
            else if (Depth == 24) // For 24 BPP get Red, Green and Blue
            {
                b = Pixels[i];
                g = Pixels[i + 1];
                r = Pixels[i + 2];
            }
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, byte r, byte g, byte b, byte a)
        {
            if (Pixels == null) return;

            // Get color components count
            var cCount = Depth / 8;

            // Get start index of the specified pixel
            var i = ((y * Width) + x) * cCount;

            if (Depth == 32) // For 32 BPP set Red, Green, Blue and Alpha
            {
                Pixels[i] = b;
                Pixels[i + 1] = g;
                Pixels[i + 2] = r;
                Pixels[i + 3] = a;
            }
            else if (Depth == 24) // For 24 BPP set Red, Green and Blue
            {
                Pixels[i] = b;
                Pixels[i + 1] = g;
                Pixels[i + 2] = r;
            }
        }

        public void Dispose()
        {
            UnlockBitmap();
        }
    }
}