using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace rt004.Objects
{
    public class Sphere : ISceneObject
    {
        public IMaterial Material { get; set; }
        public Vector3 Center { get; set; }
        public double Radius { get; set; }

        public Sphere(Vector3 center, double radius, IMaterial material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public Selection Intersect(Ray ray)
        {
            var eo = Center - ray.Start;
            var v = Vector3.Dot(eo, ray.Direction);
            if(v >= 0)
            {
                var disc = Math.Pow(Radius, 2) - ( Vector3.Dot(eo, eo) - Math.Pow(v, 2) );
                if(disc >= 0)
                    return new Selection(this, ray, v - (float)Math.Sqrt(disc));
            }
            return null;
        }

        public Vector3 GetNormal(Vector3 pos)
        {
            return Vector3.Normalize(pos - Center);
        }

        public IMaterial GetMaterial()
        {
            return Material;
        }
    }
}
