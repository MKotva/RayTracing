using Newtonsoft.Json;
using System.Numerics;

namespace rt004
{
    public class Color
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }

        [JsonConstructor]
        public Color(float r, float g, float b, float a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public Color(int r, int g, int b, int a)
        {
            this.R = (byte)r;
            this.G = (byte)g;
            this.B = (byte)b;
            this.A = (byte)a;
        }

        public static void NormalizeColor(ref Color color)
        {
            color.R = Math.Min(color.R, 255);
            color.G = Math.Min(color.G, 255);
            color.B = Math.Min(color.B, 255);
            color.R = Math.Max(color.R, 0);
            color.G = Math.Max(color.G, 0);
            color.B = Math.Max(color.B, 0);
        }

        public float[] GetRGB()
        {
            return new float[3] { R, G, B };
        }

        public static float[] GetBlack()
        {
            return new float[3] { 0, 0, 0 };
        }

        public static Color operator *(double multD, Color origin)
        {
           var mult = (float) multD;
           return new Color(mult * origin.R, mult * origin.G, mult * origin.B, mult * origin.A);
        }
        public static Color operator *(Color c1, Color c2)
        {
          return new Color(c1.R * c2.R, c1.G * c2.G, c1.B * c2.B, c1.A * c2.A);
        }

        public static Color operator +(Color c1, Color c2)
        {
          return new Color(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B, c1.A + c2.A);
        }
        public static Color operator -(Color c1, Color c2)
        {
          return new Color(c1.R - c2.R, c1.G - c2.G, c1.B - c2.B, c1.A - c2.A);
        }

        public override string ToString()
        {
            return $"Color({R};{G};{B})";
        }

        public static Color FromString(string s)
        {
            var substring = s.Substring(s.IndexOf('('), s.IndexOf(')'));
            var values = substring.Split(';');

            return new Color(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]), 0);
        }
    }
}
