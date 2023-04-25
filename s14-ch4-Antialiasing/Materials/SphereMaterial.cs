using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004.Materials
{
    internal class SphereMaterial : IMaterial
    {
        public Color GetDiffuse(Vector3 position)
        {
            return new Color(0.0f, 0.0f, 0.24f, 0);
        }

        public Color GetSpecular(Vector3 position)
        {
            return new Color(1, 1, 1, 0);
        }

        public float GetReflection(Vector3 position)
        {
            if (( Math.Floor(position.Z) + Math.Floor(position.X) ) % 2 != 0)
            {
                return 0.1f;
            }
            return 0.5f;
        }

        public float GetDisparity()
        {
            return 150;
        }

        public float GetTransparency()
        {
            return 0f;
        }

        public float GetRefractionIndex()
        {
            return 0f;
        }
    }
}
