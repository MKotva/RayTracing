using Newtonsoft.Json;
using rt004.Materials;
using rt004.Objects;
using System.Dynamic;
using System.Numerics;
using System.Text.Json.Serialization;

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
        }

        public void LoadDefaultcScene()
        {
            Lights = new LightSource[2] { new LightSource(new Vector3D(-2, 2.5f, 0), new Color(0.5f, 0.5f, 0.5f, 0)), new LightSource(new Vector3D(2, 1.5f, 0), new Color(0.4f, 0.4f, 0.4f, 0)) };
            var transform = new TransformMatrix(new List<ISceneObject>(){ new MyPlane(new Vector3D(0, 1, 0), 0, new ChessBoardMaterial())});

            Transforms.Add(transform);

            var transform1 = new TransformMatrix(new List<ISceneObject>() { new Sphere(new Vector3D(-2.5f, 1.5f, 0), 0.2, new SphereMaterial()) });
            transform1.SetTranslation(0, -0.5, 0);
            transform1.SetScaling(1.2, 1.2, 1.2);
            Transforms.Add(transform1);

            var transform2 = new TransformMatrix(new List<ISceneObject>() { new Sphere(new Vector3D(-0.5f, 1.5f, 0), 0.5, new TransparentMaterial()) });
            Transforms.Add(transform2);

            var transform3 = new TransformMatrix(new List<ISceneObject>() { new CubeObject(new Vector3D(0, 0, 0), new Vector3D(0.5, 0.5, 0.5), new CubeMaterial()) });
            transform3.SetTranslation(-0.2, 0.5, 0.3);
            transform3.SetScaling(0.8, 1, 0.8);
            Transforms.Add(transform3);


            Camera = new Camera(new Vector3D(2.75f, 2, 3.75f), new Vector3D(-0.5f, 0.5f, 0));
        }

        public static void ToJson(Scene scene, string filename)
        {
            string json = JsonConvert.SerializeObject(scene, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
            using(var sw = new StreamWriter(filename))
            {
                sw.Write(json);
            }
        }

        public static Scene FromJson(string filename)
        {
            string json;
            using (var sr = new StreamReader(filename))
            {
                json = sr.ReadToEnd();
            }

            var scene = (Scene)JsonConvert.DeserializeObject(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return scene;
        }

        //public string ToString()
        //{
        //    string output = $"Lights=[{GetLightsString()}\n]";
        //    output += $"Camera=[{Camera.ToString()}]";
        //    return output;
        //}

        //private string GetLightsString()
        //{
        //    string output = "";
        //    foreach (var light in Lights)
        //    {
        //        output += $"{light.ToString()};";
        //    }
        //    return output;
        //}

        //private string GetTransformObjectString()
        //{
        //    string output = "";
        //    return output;
        //}
    }
}
