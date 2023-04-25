using System.Collections.Generic;
using Util;
using static System.Formats.Asn1.AsnWriter;

namespace rt004
{
    public class RayTracer
    { 
        public int Width { get; set; }
        public int Height { get; set; }
        public int ReflectionDepth { get; set; }

        public Scene _loadedScene { get; set; }

        public RayTracer(int width, int height, int depthOfReflection, Scene scene)
        {
            Width = width;
            Height = height;
            ReflectionDepth = depthOfReflection;
            _loadedScene = scene;
        }

        public void Render(FloatImage img)
        {
            Camera camera = _loadedScene.Camera;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var color = CastRay(camera.Position, GetPoint(x, y, camera), 0);
                    img.PutPixel(x, y, color.GetRGB());
                }
            }
        }

        private Color CastRay(Vector start, Vector direction, int depth)
        {
            var intersect = Intersect(new Ray(start, direction), _loadedScene);
            if (intersect == null)
            {
                return new Color(0,0,0,0);
            }

            return CalculateColor(intersect, depth);
        }

        private double CastShadowRay(Vector start, Vector direction)
        {
            var intersect = Intersect(new Ray(start, direction), _loadedScene);
            if (intersect == null)
            {
                return 0;
            }

            return intersect.Distance;
        }

        private Selection Intersect(Ray ray, Scene scene)
        {
            Selection min = null;
            foreach (var obj in scene.Objects)
            {
                var intersect = obj.Intersect(ray);
                if (intersect != null)
                {
                    if (min == null || min.Distance > intersect.Distance)
                    {
                        min = intersect;
                    }
                }
            }
            return min;
        }


        private Color CalculateColor(Selection selection, int depth)
        {
            var position = selection.Distance * selection.Ray.Direction + selection.Ray.Start;
            var normal = selection.Object.GetNormal(position);
            var reflectDirection = selection.Ray.Direction - 2 * Vector.Dot(normal, selection.Ray.Direction) * normal;
            var illuminated = GetLight(selection.Object.GetMaterial(), position, normal, reflectDirection, _loadedScene);

            if(depth < ReflectionDepth)
            {
               return illuminated + CreateReflection(selection.Object.GetMaterial(), position + 0.0001 * reflectDirection, reflectDirection, depth);
            }
            return illuminated;
        }


        private Color GetLight(IMaterial material, Vector position, Vector normal, Vector reflectDirection, Scene scene)
        {
            var illuminatedColor = new Color(0, 0, 0, 0);
            foreach (LightSource light in scene.Lights)
            {
                Vector lightVector = Vector.Normalize(light.Position - position);
                var illumination = (float) Vector.Dot(lightVector, normal);

                if (!CheckShading(light, position))
                {

                    Color lightColor = new Color(0, 0, 0, 0);
                    if (illumination > 0)
                    {
                        lightColor = illumination * light.Color;
                    }

                    Color specularColor = new Color(0, 0, 0, 0);
                    var specular = ( (float) Vector.Dot(lightVector, Vector.Normalize(reflectDirection)) );
                    if (specular > 0)
                    {
                        specularColor = ( (float) Math.Pow(specular, material.GetDisparity()) ) * light.Color;
                    }

                    illuminatedColor = illuminatedColor + material.GetDiffuse(position) * lightColor + material.GetSpecular(position) * specularColor;
                }
            }
            return illuminatedColor;
        }

        private bool CheckShading(LightSource source, Vector position)
        { 
            Vector lightDistance = source.Position - position;
            Vector lightVector = Vector.Normalize(lightDistance);
            double intersectionDistance = CastShadowRay(position, lightVector);
            return !( intersectionDistance == 0 || intersectionDistance > Vector.Magnitude(lightDistance));
        }

        private Color CreateReflection(IMaterial material, Vector position, Vector rayDirection, int depth)
        {
            return material.GetReflection(position) * CastRay(position, rayDirection, depth + 1);
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
