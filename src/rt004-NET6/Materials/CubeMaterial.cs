using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    internal class CubeMaterial : IMaterial
    {
        public Color GetDiffuse(Vector3D position)
        {
            return new Color(0.5f, 0.0f, 0.0f, 0);
        }

        public Color GetSpecular(Vector3D position)
        {
            return new Color(1, 1, 1, 0);
        }

        public double GetReflection(Vector3D position)
        {
            return 0;
        }

        public double GetDisparity()
        {
            return 150;
        }

        public double GetTransparency()
        {
            return 0;
        }

        public double GetRefractionIndex()
        {
            return 0;
        }

        public double GetAmbient()
        {
            return 0.2;
        }
    }
}
