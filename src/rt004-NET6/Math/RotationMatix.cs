using rt004.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class RotationMatix
    {
        public Matrix4x4 matrix;
        private RotationMatix() 
        {
            matrix = new Matrix4x4(1, 0, 0, 0,
                                   0, 1, 0, 0,
                                   0, 0, 1, 0,
                                   0, 0, 0, 1);
        }

        public static RotationMatix GetXRotationMatrix(double angle)
        {
            var matrixTransform = new RotationMatix();

            var cosAngle = (float) Math.Cos(angle);
            matrixTransform.matrix.M22 = cosAngle;
            matrixTransform.matrix.M33 = cosAngle;

            var sinAngle = (float) Math.Sin(angle);
            matrixTransform.matrix.M23 = -sinAngle;
            matrixTransform.matrix.M33 = sinAngle;

            return matrixTransform;
        }

        public static RotationMatix GetYRotationMatrix(double angle)
        {
            var matrixTransform = new RotationMatix();

            var cosAngle = (float) Math.Cos(angle);
            matrixTransform.matrix.M11 = cosAngle;
            matrixTransform.matrix.M33 = cosAngle;

            var sinAngle = (float) Math.Sin(angle);
            matrixTransform.matrix.M31 = -sinAngle;
            matrixTransform.matrix.M13 = sinAngle;

            return matrixTransform;
        }

        public static RotationMatix GetZRotationMatrix(double angle)
        {
            var matrixTransform = new RotationMatix();

            var cosAngle = (float) Math.Cos(angle);
            matrixTransform.matrix.M11 = cosAngle;
            matrixTransform.matrix.M22 = cosAngle;

            var sinAngle = (float) Math.Sin(angle);
            matrixTransform.matrix.M12 = -sinAngle;
            matrixTransform.matrix.M21 = sinAngle;

            return matrixTransform;
        }
    }
}
