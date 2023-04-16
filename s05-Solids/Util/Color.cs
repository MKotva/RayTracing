namespace rt004
{
    class Color
    {
        float r;
        float g;
        float b;
        float a;

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color(int r, int g, int b, int a)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
            this.a = (byte)a;
        }

        public float[] GetRGB()
        {
            return new float[3] { r, g, b };
        }

        public static float[] GetBlack()
        {
            return new float[3] { 0, 0, 0 };
        }
    }
}
