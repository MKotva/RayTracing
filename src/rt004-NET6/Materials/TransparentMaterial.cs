using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004.Materials
{
    internal class TransparentMaterial : IMaterial
    {
        public Color GetDiffuse(Vector3D vector)
        {
            return new Color(1f, 1f, 1f, 0);
        }

        public double GetDisparity()
        {
            return 0;
        }

        public double GetReflection(Vector3D vector)
        {
            return 0;
        }

        public double GetRefractionIndex()
        {
            return 1.3;
        }

        public Color GetSpecular(Vector3D vector)
        {
            return new Color(1, 1, 1, 0);
        }

        public double GetTransparency()
        {
            return 1;
        }

        public double GetAmbient()
        {
            return 0.12;
        }
    }
}
