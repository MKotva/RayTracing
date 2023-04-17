using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class LightSource
    {
        public readonly Vector Position;
        public readonly Color Color;

        public LightSource(Vector position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
