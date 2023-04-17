using System.Collections.Generic;
using Util;

namespace rt004
{
    public class RayTracer
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Scene _loadedScene { get; set; }

        public RayTracer(int width, int height, Scene scene)
        {
            Width = width;
            Height = height;
            _loadedScene = scene;
        }

        public void Render(FloatImage img)
        {
            Camera camera = _loadedScene.Camera;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var color = CastRay(camera.Position, GetPoint(x, y, camera));
                    img.PutPixel(x, y, color);
                }
            }
        }

        private float[] CastRay(Vector start, Vector direction)
        {
            var isect = Intersect(new Ray(start, direction), _loadedScene);
            if (isect == null)
            {
                return Color.GetBlack();
            }
            return CalculateColor(isect);
        }

        private Selection Intersect(Ray ray, Scene scene)
        {
            Selection min = null;
            foreach (var obj in scene.Objects)
            {
                var isect = obj.Intersect(ray);
                if (isect != null)
                {
                    if (min == null || min.Distance > isect.Distance)
                    {
                        min = isect;
                    }
                }
            }
            return min;
        }


        private float[] CalculateColor(Selection selection)
        {
            var posisiton = selection.Distance * selection.Ray.Direction + selection.Ray.Start;
            var normal = selection.Object.GetNormal(posisiton);
            var reflectDirection = selection.Ray.Direction - 2 * Vector.Dot(normal, selection.Ray.Direction) * normal;
            var illuminatedColor = GetLight(selection.Object.GetMaterial(), posisiton, normal, reflectDirection, _loadedScene);

            return illuminatedColor.GetRGB();
        }


        private Color GetLight(IMaterial material, Vector position, Vector normal, Vector reflectDirection, Scene scene)
        {
            var illuminatedColor = new Color(0, 0, 0, 0);
            foreach (LightSource light in scene.Lights)
            {
                Vector lightVector = Vector.Normalize(light.Position - position);
                var illumination = (float) Vector.Dot(lightVector, normal);

                Color lightColor = new Color(0, 0, 0, 0);
                if (illumination > 0)
                {
                    lightColor = illumination * light.Color;
                }

                Color specularColor = new Color(0, 0, 0, 0);
                var specular = ((float)Vector.Dot(lightVector, Vector.Normalize(reflectDirection)));
                if (specular > 0)
                {
                    specularColor = ( (float) Math.Pow(specular, material.GetDisparity()) ) * light.Color;
                }

                illuminatedColor = illuminatedColor + material.GetDiffuse(position) * lightColor + material.GetSpecular(position) * specularColor;
            }
            return illuminatedColor;
        }

        private Vector GetPoint(double x, double y, Camera camera)
        {
            return Vector.Normalize(camera.Forward + ( CenetralizeX(x) * camera.Right + CentralizeY(y) * camera.Up ));
        }

        private double CenetralizeX(double x)
        {
            return ( x - Width / 2.0 ) / ( 2.0 * Width );
        }
        private double CentralizeY(double y)
        {
            return -( y - Height / 2.0 ) / ( 2.0 * Height );
        }
    }
}
