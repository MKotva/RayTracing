using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public interface IMaterial
    {
        public float GetDisparity();
        public float GetReflection(Vector vector);
        public Color GetDiffuse(Vector vector);
        public Color GetSpecular(Vector vector);
    }
}
