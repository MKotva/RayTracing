using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class Vector3D
    {
        public double X {  get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public static Vector3D Zero = new Vector3D(0, 0, 0);

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3D Normalize(Vector3D vector)
        {
            var normalized =  Vector3.Normalize(new Vector3((float)vector.X, (float)vector.Y, (float)vector.Z));
            return new Vector3D(normalized.X, normalized.Y, normalized.Z);
        }

        public static Vector3D Cross(Vector3D lV, Vector3D rV)
        {
            var cross = Vector3.Cross(new Vector3((float)lV.X, (float)lV.Y, (float)lV.Z), new Vector3((float)rV.X, (float)rV.Y, (float)rV.Z));
            return new Vector3D(cross.X, cross.Y, cross.Z);
        }

        public static double Dot(Vector3D lV, Vector3D rV)
        {
            return Vector3.Dot(new Vector3((float)lV.X, (float)lV.Y, (float)lV.Z), new Vector3((float)rV.X, (float)rV.Y, (float)rV.Z));
        }

        public static Vector3D Transform(Vector3D v, Matrix4x4 matrix)
        {
            var transformed = Vector3.Transform(new Vector3((float)v.X, (float)v.Y, (float)v.Z), matrix);
            return new Vector3D(transformed.X, transformed.Y, transformed.Z);
        }

        public static Vector3D TransformNormal(Vector3D v, Matrix4x4 matrix)
        {
            var transformed = Vector3.TransformNormal(new Vector3((float)v.X, (float)v.Y, (float)v.Z), matrix);
            return new Vector3D(transformed.X, transformed.Y, transformed.Z);
        }

        public static Vector3D Lerp(Vector3D a, Vector3D b, double t)
        {
            t = Math.Clamp(t, 0, 1);
            return new Vector3D(
                a.X + ( b.X - a.X ) * t,
                a.Y + ( b.Y - a.Y ) * t,
                a.Z + ( b.Z - a.Z ) * t
            );
        }

        public static Vector3D operator*(double coef,  Vector3D v)
        {
            return new Vector3D(v.X * coef, v.Y* coef, v.Z * coef);
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static bool operator ==(Vector3D v1, Vector3D v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }

        public static bool operator !=(Vector3D v1, Vector3D v2)
        {
            return !( v1 == v2 );
        }


    }
}
