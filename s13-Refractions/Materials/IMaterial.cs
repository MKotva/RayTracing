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
        public float GetRefractionIndex();
        public float GetTransparency();
        public float GetDisparity();
        public float GetReflection(Vector3 vector);
        public Color GetDiffuse(Vector3 vector);
        public Color GetSpecular(Vector3 vector);
    }
}
