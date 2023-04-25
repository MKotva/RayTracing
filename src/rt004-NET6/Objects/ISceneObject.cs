using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace rt004
{
    public interface ISceneObject
    {
        public Vector3D GetNormal(Vector3D position);
        public Selection Intersect(Ray ray);
        public IMaterial GetMaterial();
        public string ToString();
    }
}
