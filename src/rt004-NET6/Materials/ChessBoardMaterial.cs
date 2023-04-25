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

        public Color GetDiffuse(Vector3D position)
        {
            if (( Math.Floor(position.Z) + Math.Floor(position.X) ) % 2 != 0)
            {
                return new Color(0.2f, 0, 0.2f, 0);
            }
            return new Color(1, 1, 1, 0);
        }

        public Color GetSpecular(Vector3D position)
        {
            return new Color(1, 1, 1, 0);
        }

        public double GetReflection(Vector3D position)
        {
            if (( Math.Floor(position.Z) + Math.Floor(position.X) ) % 2 != 0)
            {
                return 0.1;
            }
            return 0.2;
        }

        public double GetDisparity()
        {
            return 250;
        }

        public double GetRefractionIndex()
        {
            return 0;
        }

        public double GetTransparency()
        {
            return 0;
        }

        public double GetAmbient()
        {
            return 0.1;
        }
    }
}
