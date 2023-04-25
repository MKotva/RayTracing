using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rt004.Objects;

namespace rt004
{
    public class Selection
    {
        public ISceneObject Object { get; set; }
        public Ray Ray { get; set; }
        public float Distance { get; set; }

        public Selection(ISceneObject sceneObject, Ray ray, float distance)
        {
            Object = sceneObject;
            Ray = ray;
            Distance = distance;
        }
    }
}
