namespace rt004.Objects
{
    class Plane : ISceneObject
    {
        public IMaterial Material;
        public Vector Normal;
        public double Offset;

        public Plane(Vector normal, double offset, IMaterial material)
        {
            Normal = normal;
            Offset = offset;
            Material = material;
        }

        public Selection Intersect(Ray ray)
        {
            var denom = Vector.Dot(Normal, ray.Direction);
            if (denom <= 0)
                return new Selection(this, ray, (Vector.Dot(Normal, ray.Start) + Offset) / -denom);
            return null;
        }

        public Vector GetNormal(Vector pos)
        {
            return Normal;
        }

        public IMaterial GetMaterial() 
        {
            return Material;
        }
    }
}
