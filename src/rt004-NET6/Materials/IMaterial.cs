using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public interface IMaterial
    {
        public double GetAmbient();
        public double GetRefractionIndex();
        public double GetTransparency();
        public double GetDisparity();
        public double GetReflection(Vector3D vector);
        public Color GetDiffuse(Vector3D vector);
        public Color GetSpecular(Vector3D vector);
    }
}
