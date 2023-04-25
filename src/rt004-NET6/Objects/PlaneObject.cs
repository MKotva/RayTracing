using System.Numerics;

namespace rt004
{
    class MyPlane : ISceneObject
    {
        public IMaterial Material { get; set; }
        public Vector3D Normal { get; set; }
        public float Offset { get; set; }

        public MyPlane(Vector3D normal, float offset, IMaterial material)
        {
            Normal = normal;
            Offset = offset;
            Material = material;
        }

        public Selection Intersect(Ray ray)
        {
            var denom = Vector3D.Dot(Normal, ray.Direction);
            if (denom <= 0)
                return new Selection(this, ray, (Vector3D.Dot(Normal, ray.Start) + Offset) / -denom);
            return null;
        }

        public Vector3D GetNormal(Vector3D pos)
        {
            return Normal;
        }

        public IMaterial GetMaterial() 
        {
            return Material;
        }
    }
}
