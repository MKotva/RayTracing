using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class Matrix
    {
        public double M11 { get; set; } = 1.0;
        public double M12 { get; set; }
        public double M13 { get; set; }
        public double M14 { get; set; }
        public double M21 { get; set; }
        public double M22 { get; set; } = 1.0;
        public double M23 { get; set; }
        public double M24 { get; set; }
        public double M31 { get; set; }
        public double M32 { get; set; }
        public double M33 { get; set; } = 1.0;
        public double M34 { get; set; }
        public double M41 { get; set; }
        public double M42 { get; set; }
        public double M43 { get; set; }
        public double M44 { get; set; } = 1.0;

        public Matrix() { }
        public Matrix(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        public Matrix(Matrix4x4 matrix4D)
        {
           M11 = matrix4D.M11;
           M12 = matrix4D.M12;
           M13 = matrix4D.M13;
           M14 = matrix4D.M14;

           M21 = matrix4D.M21;
           M22 = matrix4D.M22;
           M23 = matrix4D.M23;
           M24 = matrix4D.M24;

           M31 = matrix4D.M31;
           M32 = matrix4D.M32;
           M33 = matrix4D.M33;
           M34 = matrix4D.M34;

           M41 = matrix4D.M41;
           M42 = matrix4D.M42;
           M43 = matrix4D.M43;
           M44 = matrix4D.M44;
        }

        public Matrix4x4 GetOpenTKMatrix()
        {
            return new Matrix4x4((float)M11, (float)M12, (float)M13, (float)M14,
                                 (float)M21, (float)M22, (float)M23, (float)M24,
                                 (float)M31, (float)M32, (float)M33, (float)M34,
                                 (float)M41, (float)M42, (float)M43, (float)M44);
        }
    }
}
