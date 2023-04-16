using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rt004.Objects;

namespace rt004
{
    public class Scene
    {
        public ISceneObject[] Objects;
        public Camera Camera;

        public Scene()
        {
            Objects = new ISceneObject[1] { new Sphere(new Vector(-0.5, 0.5, 0), 0.5),/* new Plane(new Vector(0, 1, 0), 0)*/ };
            Camera = new Camera(new Vector(2.5, 2, 3.5), new Vector(-0.5, 0.5, 0));
        }

        public IEnumerable<Selection> Intersect(Ray r)
        {
            foreach (ISceneObject obj in Objects)
            {
                yield return obj.Intersect(r);
            }
        }
    }
}
