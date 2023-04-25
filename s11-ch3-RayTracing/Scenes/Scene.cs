using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rt004.Materials;
using rt004.Objects;

namespace rt004
{
    public class Scene
    {
        public LightSource[] Lights;
        public ISceneObject[] Objects;
        public Camera Camera;

        public Scene()
        {
            Lights = new LightSource[2] { new LightSource(new Vector(-2, 2.5, 0), new Color(0.5f, 0.5f, 0.5f, 0)), new LightSource(new Vector(2, 1.5, 0), new Color(0.4f, 0.4f, 0.4f, 0)) };
            Objects = new ISceneObject[3] { new Sphere(new Vector(-0.5, 0.5, 0), 0.5, new SphereMaterial()),
                new Sphere(new Vector(-1.5, 0.5, 0), 0.2, new SphereMaterial()),
                new Plane(new Vector(0, 1, 0), 0, new ChessBoardMaterial())
            };
            Camera = new Camera(new Vector(2.75, 2, 3.75), new Vector(-0.5, 0.5, 0));
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
