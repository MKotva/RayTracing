using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    public struct Ray
    {
        public Vector Start;
        public Vector Direction;
        public Ray(Vector start, Vector direction)
        {
            Start = start;
            Direction = direction;
        }
    }
}
