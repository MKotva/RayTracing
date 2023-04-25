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
        public Color GetDiffuse(Vector3 vector)
        {
            return new Color(0.0f, 0.0f, 0.24f, 0);
        }

        public float GetDisparity()
        {
            return 50;
        }

        public float GetReflection(Vector3 vector)
        {
            return 0.01f;
        }

        public float GetRefractionIndex()
        {
            return 1f;
        }

        public Color GetSpecular(Vector3 vector)
        {
            return new Color(1, 1, 1, 0);
        }

        public float GetTransparency()
        {
            return 1f;
        }
    }
}
