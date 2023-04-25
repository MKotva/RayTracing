using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace rt004.Objects
{
    public class Sphere : ISceneObject
    {
        public IMaterial Material { get; set; }
        public Vector3D Center { get; set; }
        public double Radius { get; set; }

        public Sphere(Vector3D center, double radius, IMaterial material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public Selection Intersect(Ray ray)
        {
            var eo = Center - ray.Start;
            var adj = Vector3D.Dot(eo, ray.Direction);
            var d2 = Vector3D.Dot(eo, eo) - Math.Pow(adj, 2);

            var radius2 = Math.Pow(Radius, 2);
            if (d2 >= radius2)
                return null;

            var thc = Math.Sqrt(radius2 - d2);
            var t0 = adj - thc;
            var t1 = adj + thc;

            if (t0 < 0.0 && t1 < 0.0)
            {
                return null;
            }
            else if (t0 <= 0.0)
            {
                return new Selection(this, ray, t1);
            }
            else if(t1 <= 0.0) 
            {
                return new Selection(this, ray, t0);
            }
            else
            {
                if(t0 <= t1)
                    return new Selection(this, ray, t0);
                return new Selection(this, ray, t1);
            }


            return null;
        }

    //public Selection Intersect2(Ray ray)
    //    {
    //        Vector3 o_minus_c = ray.Start - Center;

    //        float p = (float) Vector3.Dot(ray.Direction, o_minus_c);
    //        float q = (float) (Vector3.Dot(o_minus_c, o_minus_c) - ( Radius * Radius ));

    //        float discriminant = ( p * p ) - q;
    //        if (discriminant < 0.0f)
    //        {
    //            return null;
    //        }



    //        float dRoot = MathF.Sqrt((float)discriminant);
    //        float dist1 = -p - dRoot;
    //        float dist2 = -p + dRoot;


    //        if ( discriminant > 1e-7 )
    //        {

    //        }

    //        if (ray.Start + MathF.Abs(dist1) * ray.Direction == ray.Start + dist1 * ray.Direction)
    //            return new Selection(this, ray, dist1);
    //        if (ray.Start + MathF.Abs(dist2) * ray.Direction == ray.Start + dist2 * ray.Direction)
    //            return new Selection(this, ray, dist2);
    //        return null;
    //    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="px">origin x</param>
        /// <param name="py">origin y</param>
        /// <param name="pz">origin z</param>
        /// <param name="x">direction x</param>
        /// <param name="y">direction y</param>
        /// <param name="z">direction z</param>
        /// <returns></returns>
        //public Selection Intersect(Ray ray)
        //{
        //    // x-xo 2 + y-yo 2 + z-zo 2 = r 2
        //    // x,y,z = p+tv 
        //    // At2 + Bt + C = 0


        //    double px = ray.Start.X;
        //    double py = ray.Start.Y;
        //    double pz = ray.Start.Z;
        //    double vx = ray.Direction.X;
        //    double vy = ray.Direction.Y;
        //    double vz = ray.Direction.Z;
        //    double cx = Center.X;
        //    double cy = Center.Y;
        //    double cz = Center.Z;

        //    Vector3 hitPoint = new Vector3(0, 0, 0);

        //    double A = ( vx * vx + vy * vy + vz * vz );
        //    double B = 2.0 * ( px * vx + py * vy + pz * vz -
        //              vx * cx - vy * cy - vz * cz );
        //    double C = px * px - 2 * px * cx + cx * cx + py * py -
        //              2 * py * cy + cy * cy + pz * pz - 2 * pz * cz +
        //              cz * cz - Radius * Radius;
        //    double D = B * B - 4 * A * C;
        //    double t = -1.0;
        //    if (D >= 0)
        //    {
        //        double t1 = ( -B - System.Math.Sqrt(D) ) / ( 2.0 * A );
        //        double t2 = ( -B + System.Math.Sqrt(D) ) / ( 2.0 * A );

        //        if (t1 < t2 && t1 > -1.0E-6) t = t2; else t = t1;

        //        hitPoint.X = px + t * vx;
        //        hitPoint.Y = py + t * vy;
        //        hitPoint.Z = pz + t * vz;


        //        var distFromStart = ( hitPoint - ray.Start ).Magnitude();               
        //        if ((ray.Start + distFromStart * ray.Direction - hitPoint).Magnitude() <= 1.0E-6)
        //        {
        //            return new Selection(this, ray, t);
        //        }
        //    }

        //    return null;
        //}

        public Vector3D GetNormal(Vector3D pos)
        {
            return Vector3D.Normalize(pos - Center);
        }

        public IMaterial GetMaterial()
        {
            return Material;
        }
    }
}
