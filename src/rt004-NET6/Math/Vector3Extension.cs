using System.Net.NetworkInformation;
using System.Numerics;

namespace rt004
{
    public static class Vector3Extension
    {
        public static double Magnitude(this Vector3D v)
        {
            return Math.Sqrt(Vector3D.Dot(v, v));
        }

        public static string ToString(this Vector3D v) 
        {
            return $"Position({v.X};{v.Y};{v.Z})";
        }

        public static Vector3D FromString(string s) 
        {
            var substring = s.Substring(s.IndexOf('('), s.IndexOf(')'));
            var values = substring.Split(';');

            return new Vector3D(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
    }


    //public double X;
    //public double Y;
    //public double Z;

    //public static readonly Vector Zero  = new Vector(0, 0, 0);
    //public Vector(double x, double y, double z)
    //{
    //    X = x;
    //    Y = y;
    //    Z = z;
    //}

    //public static double Dot(Vector v1, Vector v2)
    //{
    //    return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
    //}
    //public static double Magnitude(Vector v)
    //{
    //   return  Math.Sqrt(Dot(v, v));
    //}
    //public static Vector Normalize(Vector v)
    //{
    //    double mag = Magnitude(v);
    //    double div = mag == 0 ? double.PositiveInfinity : 1 / mag;
    //    return div * v;
    //}
    //public static Vector Cross(Vector v1, Vector v2)
    //{
    //    return new Vector(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
    //}

    //public static Vector TransformNoraml(Vector vector, Matrix4x4 transform)
    //{
    //    var vector3 = new Vector3((float)vector.X, (float)vector.Y, (float)vector.Z);
    //    Vector3.TransformNormal()
    //}

    //public static Vector operator *(double n, Vector v)
    //{
    //   return  new Vector(v.X * n, v.Y * n, v.Z * n);
    //}

    //public static Vector operator -(Vector v1, Vector v2)
    //{
    //   return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    //}

    //public static Vector operator +(Vector v1, Vector v2)
    //{
    //   return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    //}
}
