using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class ChessBoardMaterial : IMaterial
    {
        public Color GetDiffuse(Vector3 position)
        {
            if (Math.Floor(position.Z) + Math.Floor(position.X) % 2 == 0)
            {
                return new Color(1, 1, 1, 0);
            }
            return new Color(0.2f, 0.0f, 0.14f, 0);
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
            return 0.2f;
        }

        public float GetDisparity()
        {
            return 250;
        }

        public float GetRefractionIndex()
        {
            return 0;
        }

        public float GetTransparency()
        {
            return 0;
        }
    }
}
