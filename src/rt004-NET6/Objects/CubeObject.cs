using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class CubeObject : ISceneObject
    {
        public double XMin { get; private set; }
        public double XMax { get; private set; }
        public double YMin { get; private set; }
        public double YMax { get; private set; }
        public double ZMin { get; private set; }
        public double ZMax { get; private set; }

        public IMaterial Material { get; private set; }

        [JsonConstructor]
        public CubeObject(double xMin, double xMax, double yMin, double yMax, double zMin, double zMax, IMaterial material)
        {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
            ZMin = zMin;
            ZMax = zMax;
            this.Material = material;
        }

        public CubeObject(Vector3D leftCorner, Vector3D rightCorner, IMaterial material)
        {
            XMin = Math.Min(leftCorner.X, rightCorner.X);
            XMax = Math.Max(leftCorner.X, rightCorner.X);

            YMin = Math.Min(leftCorner.X, rightCorner.X);
            YMax = Math.Max(leftCorner.X, rightCorner.X);

            ZMin = Math.Min(leftCorner.X, rightCorner.X);
            ZMax = Math.Max(leftCorner.X, rightCorner.X);
            this.Material = material;
        }

        public IMaterial GetMaterial()
        {
            return Material;
        }

        public Vector3D GetNormal(Vector3D position)
        {
            var vMax = new Vector3D(XMax, YMax, ZMax);
            var vMin = new Vector3D(XMin, YMin, ZMin);

            var center = 0.5 * (vMax - vMin);
            var n = new Vector3D(position.X - center.X, position.Y - center.Y, position.Z - center.Z);
            return n;
        }

        public Selection Intersect(Ray ray)
        {
            var tx1 = ( XMin - ray.Start.X ) / ray.Direction.X;
            var tx2 = ( XMax - ray.Start.X ) / ray.Direction.X;
            var ty1 = ( YMin - ray.Start.Y ) / ray.Direction.Y;
            var ty2 = ( YMax - ray.Start.Y ) / ray.Direction.Y;
            var tz1 = ( ZMin - ray.Start.Z ) / ray.Direction.Z;
            var tz2 = ( ZMax - ray.Start.Z ) / ray.Direction.Z;

            var tNear = Math.Max(Math.Min(tx1, tx2), Math.Max(Math.Min(ty1, ty2), Math.Min(tz1, tz2)));
            var tfar = Math.Min(Math.Max(tx1, tx2), Math.Min(Math.Max(ty1, ty2), Math.Max(tz1, tz2)));

            if (tNear > tfar || tfar < 0)
                return null;

            return new Selection(this, ray, tNear);
        }
    }

}
