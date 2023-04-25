using rt004.Materials;
using rt004.Objects;
using System.Numerics;

namespace rt004
{
    public class Scene
    {
        public LightSource[] Lights { get; set; }
        public List<TransformMatrix> Transforms { get; set; }
        public Camera Camera { get; set; }

        public Scene()
        {
            Transforms = new List<TransformMatrix>();
            Lights = new LightSource[2] { new LightSource(new Vector3(-2, 2.5f, 0), new Color(0.5f, 0.5f, 0.5f, 0)), new LightSource(new Vector3(2, 1.5f, 0), new Color(0.4f, 0.4f, 0.4f, 0)) };
            var transform = new TransformMatrix(new List<ISceneObject>(){ new MyPlane(new Vector3(0, 1, 0), 0, new ChessBoardMaterial()), new Sphere(new Vector3(-2.5f, 1.5f, 0), 0.2, new SphereMaterial())
            });

            Transforms.Add(transform);

            var transform1 = new TransformMatrix(new List<ISceneObject>(){ new Sphere(new Vector3(-0.5f, 0.5f, 0), 0.5, new TransparentMaterial())});
            transform1.SetTranslation(0, 1.2f, 0);
            Transforms.Add(transform1);

            Camera = new Camera(new Vector3(2.75f, 2, 3.75f), new Vector3(-0.5f, 0.5f, 0));
        }
    }
}
