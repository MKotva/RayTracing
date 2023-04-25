using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public class LightSource
    {
        public Vector3D Position { get; set; }
        public Color Color { get; set; }

        public LightSource(Vector3D position, Color color)
        {
            Position = position;
            Color = color;
        }

        public string ToString()
        {
            return $"Light[{Vector3Extension.ToString(Position)},{Color.ToString()}]";
        }

        public static LightSource FromString(string s) 
        {
            var substring = s.Substring(s.IndexOf('['), s.IndexOf(']'));
            var values = substring.Split(',');

            return new LightSource(Vector3Extension.FromString(values[0]), Color.FromString(values[1]));
        }
    }
}
