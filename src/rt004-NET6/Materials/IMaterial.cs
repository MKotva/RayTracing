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
        public float GetReflection(Vector vec);
        public Color GetColor(Vector vec);
        public Color Spectacular(Vector vec);
    }
}
