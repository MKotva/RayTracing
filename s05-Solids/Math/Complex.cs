namespace rt004
{
    class Complex
    {
        float real;
        float imag;

        public Complex(float real, float imag)
        {
            this.real = real;
            this.imag = imag;
        }
        public float SquareSqrLength()
        {
            return real * real + imag * imag;
        }

        public void Square()
        {
            float temp = real * real - imag * imag;
            imag = 2.0f * real * imag;
            real = temp;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(real * real + imag * imag);
        }

        public static Complex operator *(Complex a, Complex b)
        {
            float retReal = a.real * b.real - a.imag * b.imag;
            float retImag = a.real * b.imag - a.imag * b.real;
            return new Complex(retReal, retImag);
        }
        public static Complex operator +(Complex a, Complex b)
        {
            float retReal = a.real + b.real;
            float retImag = a.imag + b.imag;
            return new Complex(retReal, retImag);
        }
    }
}
