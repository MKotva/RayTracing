using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public interface ISceneObject
    {
        public Vector GetNormal(Vector position);
        public Selection Intersect(Ray ray);
    }
}
