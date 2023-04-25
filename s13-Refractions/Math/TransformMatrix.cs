﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class TransformMatrix
    {
        public Matrix4x4 matrix = new Matrix4x4(1, 0, 0, 0,
                                                0, 1, 0, 0,
                                                0, 0, 1, 0,
                                                0, 0, 0, 1);

        public Matrix4x4 invert = new Matrix4x4(1, 0, 0, 0,
                                                0, 1, 0, 0,
                                                0, 0, 1, 0,
                                                0, 0, 0, 1);

        public List<ISceneObject> TransformedObjects { get; set; }


        public TransformMatrix() 
        {
            TransformedObjects = new List<ISceneObject>();
        }

        public TransformMatrix(List<ISceneObject> sceneObjects)
        {
            TransformedObjects = sceneObjects;
        }

        public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
        {

            float a = matrix.M11, b = matrix.M12, c = matrix.M13, d = matrix.M14;
            float e = matrix.M21, f = matrix.M22, g = matrix.M23, h = matrix.M24;
            float i = matrix.M31, j = matrix.M32, k = matrix.M33, l = matrix.M34;
            float m = matrix.M41, n = matrix.M42, o = matrix.M43, p = matrix.M44;

            float kp_lo = k * p - l * o;
            float jp_ln = j * p - l * n;
            float jo_kn = j * o - k * n;
            float ip_lm = i * p - l * m;
            float io_km = i * o - k * m;
            float in_jm = i * n - j * m;

            float a11 = +( f * kp_lo - g * jp_ln + h * jo_kn );
            float a12 = -( e * kp_lo - g * ip_lm + h * io_km );
            float a13 = +( e * jp_ln - f * ip_lm + h * in_jm );
            float a14 = -( e * jo_kn - f * io_km + g * in_jm );

            float det = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(det) < float.Epsilon)
            {
                result = new Matrix4x4(float.NaN, float.NaN, float.NaN, float.NaN,
                                       float.NaN, float.NaN, float.NaN, float.NaN,
                                       float.NaN, float.NaN, float.NaN, float.NaN,
                                       float.NaN, float.NaN, float.NaN, float.NaN);
                return false;
            }

            float invDet = 1.0f / det;

            result.M11 = a11 * invDet;
            result.M21 = a12 * invDet;
            result.M31 = a13 * invDet;
            result.M41 = a14 * invDet;

            result.M12 = -( b * kp_lo - c * jp_ln + d * jo_kn ) * invDet;
            result.M22 = +( a * kp_lo - c * ip_lm + d * io_km ) * invDet;
            result.M32 = -( a * jp_ln - b * ip_lm + d * in_jm ) * invDet;
            result.M42 = +( a * jo_kn - b * io_km + c * in_jm ) * invDet;

            float gp_ho = g * p - h * o;
            float fp_hn = f * p - h * n;
            float fo_gn = f * o - g * n;
            float ep_hm = e * p - h * m;
            float eo_gm = e * o - g * m;
            float en_fm = e * n - f * m;

            result.M13 = +( b * gp_ho - c * fp_hn + d * fo_gn ) * invDet;
            result.M23 = -( a * gp_ho - c * ep_hm + d * eo_gm ) * invDet;
            result.M33 = +( a * fp_hn - b * ep_hm + d * en_fm ) * invDet;
            result.M43 = -( a * fo_gn - b * eo_gm + c * en_fm ) * invDet;

            float gl_hk = g * l - h * k;
            float fl_hj = f * l - h * j;
            float fk_gj = f * k - g * j;
            float el_hi = e * l - h * i;
            float ek_gi = e * k - g * i;
            float ej_fi = e * j - f * i;

            result.M14 = -( b * gl_hk - c * fl_hj + d * fk_gj ) * invDet;
            result.M24 = +( a * gl_hk - c * el_hi + d * ek_gi ) * invDet;
            result.M34 = -( a * fl_hj - b * el_hi + d * ej_fi ) * invDet;
            result.M44 = +( a * fk_gj - b * ek_gi + c * ej_fi ) * invDet;

            return true;
        }


        public void SetTranslation(float x, float y, float z)
        {
            matrix.M41 = x;
            matrix.M42 = y;
            matrix.M43 = z;

            Invert(matrix, out invert);
        }

        public void SetTranslation(Vector3 vector)
        {
            SetTranslation((float)vector.X, (float)vector.Y, (float)vector.Z);
        }


        public void SetScaling(float x, float y, float z) 
        {
            matrix.M11 = x;
            matrix.M22 = y;
            matrix.M33 = z;

            Invert(matrix, out invert);
        }

        public void SetScaling(Vector3 vector)
        {
            SetScaling((float)vector.X, (float)vector.Y, (float)vector.Z); //TODO: Change vector values from double to float.
        }

        public void SetShear(float yx, float zx, float xy, float zy, float xz, float yz)
        {
            matrix.M12 = yx;
            matrix.M13 = zx;
            
            matrix.M21 = xy;
            matrix.M23 = zy;

            matrix.M31 = xz;
            matrix.M32 = yz;

            Invert(matrix, out invert);
        }

        public void SetXRotation(float angle) 
        {
            var cosAngle = (float) Math.Cos(angle);
            matrix.M22 += cosAngle;
            matrix.M33 += cosAngle;

            var sinAngle = (float) Math.Sin(angle);
            matrix.M23 += -sinAngle;
            matrix.M33 += sinAngle;
        }
        public void SetYRotation(float angle) 
        {
            var cosAngle = (float) Math.Cos(angle);
            matrix.M11 += cosAngle;
            matrix.M33 += cosAngle;

            var sinAngle = (float) Math.Sin(angle);
            matrix.M31 += -sinAngle;
            matrix.M13 += sinAngle;
        }
        public void SetZRotation(float angle) 
        {
            var cosAngle = (float) Math.Cos(angle);
            matrix.M11 += cosAngle;
            matrix.M22 += cosAngle;

            var sinAngle = (float) Math.Sin(angle);
            matrix.M12 += -sinAngle;
            matrix.M21 += sinAngle;
        }

        public static Vector3 GetScaledPos(Matrix4x4 matrix, Vector3 position)
        {
            return new Vector3(position.X * matrix.M11, position.Y * matrix.M22, position.Z * matrix.M33);
        }

        public static Vector3 GetSheared(Matrix4x4 matrix, Vector3 position)
        {
            var x = position.X + position.Y * matrix.M12 + position.Z * matrix.M13;
            var y = position.X * matrix.M21 + position.Y + position.Z * matrix.M23;
            var z = position.X * matrix.M31 + position.Y * matrix.M32 + position.Z;

            return new Vector3(x, y, z);
        }

        public static Vector3 GetTranslation(Matrix4x4 matrix, Vector3 position)
        {
            return new Vector3(position.X + matrix.M41, position.Y + matrix.M42, position.Z + matrix.M43);
        }

        public static Vector3 GetTransformatedPosition(Matrix4x4 matrix, Vector3 position)
        {
            return GetScaledPos(matrix, GetSheared(matrix, GetTranslation(matrix, position)));
        }
    }
}
