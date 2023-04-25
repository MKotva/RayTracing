using System.Numerics;

namespace rt004.Objects
{
    class MyPlane : ISceneObject
    {
        public IMaterial Material { get; set; }
        public Vector3 Normal { get; set; }
        public float Offset { get; set; }

        public MyPlane(Vector3 normal, float offset, IMaterial material)
        {
            Normal = normal;
            Offset = offset;
            Material = material;
        }

        public Selection Intersect(Ray ray)
        {
            var denom = Vector3.Dot(Normal, ray.Direction);
            if (denom <= 0)
                return new Selection(this, ray, (Vector3.Dot(Normal, ray.Start) + Offset) / -denom);
            return null;
        }

        public Vector3 GetNormal(Vector3 pos)
        {
            return Normal;
        }

        public IMaterial GetMaterial() 
        {
            return Material;
        }
    }
}
