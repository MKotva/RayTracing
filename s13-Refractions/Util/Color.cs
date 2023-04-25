namespace rt004
{
    public class Color
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }

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

        public float[] GetRGB()
        {
            return new float[3] { R, G, B };
        }

        public static float[] GetBlack()
        {
            return new float[3] { 0, 0, 0 };
        }

        public static Color operator *(float mult, Color origin)
        {
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
    }
}
