namespace rt004.Objects
{
    public class Sphere : ISceneObject
    {
        public Vector Center { get; set; }
        public double Radius { get; set; }

        public Sphere(Vector center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public Selection Intersect(Ray ray)
        {
            var eo = Center - ray.Start;
            var v = Vector.Dot(eo, ray.Direction);
            if(v >= 0)
            {
                var disc = Math.Pow(Radius, 2) - ( Vector.Dot(eo, eo) - Math.Pow(v, 2) );
                if(disc >= 0)
                    return new Selection(this, ray, v - Math.Sqrt(disc));
            }
            return null;
        }

        public Vector GetNormal(Vector pos)
        {
            return Vector.Normalize(pos - Center);
        }
    }
}
