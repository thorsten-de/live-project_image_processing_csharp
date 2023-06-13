using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_processor
{
    public struct HSL
    {
        public float H { get; set; }
        public float S { get; set; }
        public float L { get; set; }

        public HSL(float h, float s, float l)
        {
            H = h;
            S = s;
            L = l;
        }

        public static HSL FromRgb(byte r, byte g, byte b)
        {
            byte[] rgb = new byte[3] { r, g, b };
            byte xMax = rgb.Max();
            byte xMin = rgb.Min();

            float V = ByteToFloat(xMax);
            float C = V - ByteToFloat(xMin);
            float L = V - (C / 2f);
            float r1 = ByteToFloat(r);
            float g1 = ByteToFloat(g);
            float b1 = ByteToFloat(b);


            float H = 0;
            if (Math.Abs(C) > 0.0001)
            {
                if (xMax == r)
                {
                    float remainder = (g1 - b1) / C % 6f;
                    H = 60f * (remainder < 0 ? remainder + 6 : remainder);
                }
                else if (xMax == g)
                    H = 60f * ((b1 - r1) / C + 2);
                else if (xMax == b)
                    H = 60f * ((r1 - g1) / C + 4);
            }

            float S = L == 0 || L == 1 ? 0 : ((V - L) / Math.Min(L, 1 - L));
            return new HSL(H, S, L);
        }

        public void ToRgb(out byte r, out byte g, out byte b)
        {
            r = ToByte(F(0));
            g = ToByte(F(8));
            b = ToByte(F(4));
        }

        private float F(float n)
        {
            float k = (n + H / 30f) % (12f);
            float a = S * Math.Min(L, 1 - L);
            return L - a * Math.Max(-1, Math.Min(Math.Min(k - 3, 9 - k), 1));
        }
     
        public static byte ToByte(float f) =>
            f < 0 ? (byte)0 : (byte)(Math.Min(255f, Math.Round(f * 255f)));

        public static float ByteToFloat(byte b) => b / 255f;
    }
}
