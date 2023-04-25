using System.Collections.Generic;
using System.Numerics;
using Util;

namespace rt004
{
    public class RayTracer
    { 
        public int Width { get; set; }
        public int Height { get; set; }
        public int ReflectionDepth { get; set; }

        public bool IsAntialiasing { get; set; }

        public Scene _loadedScene { get; set; }

        public RayTracer(int width, int height, int depthOfReflection, bool isAntiAlias, Scene scene)
        {
            Width = width;
            Height = height;
            ReflectionDepth = depthOfReflection;
            IsAntialiasing = isAntiAlias;
            _loadedScene = scene;
        }

        public void Render(FloatImage img)
        {
            Camera camera = _loadedScene.Camera;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Color color;

                    if (!IsAntialiasing)
                    {
                        color = CastRay(camera.Position, GetPoint(x, y, camera), 0);
                    }
                    else
                    {
                        color = RenderAntiAlias(x, y, camera);
                    }
                    img.PutPixel(x, y, color.GetRGB());
                }
            }
        }

        private Color RenderAntiAlias(int x, int y, Camera camera)
        {
            double delta = 0.5000000;
            if (x > 0 && y > 0 && x < Width - 1 && y < Height - 1)
            {
                Color color1 = CastRay(camera.Position, GetPoint(x -1, y -1, camera), 0);   //  -1, -1
                Color color2 = CastRay(camera.Position, GetPoint(x, y - 1, camera), 0);     //   0  -1
                Color color3 = CastRay(camera.Position, GetPoint(x + 1, y - 1, camera), 0); //   1  -1
                Color color4 = CastRay(camera.Position, GetPoint(x - 1, y, camera), 0);     //  -1   0
                Color color5 = CastRay(camera.Position, GetPoint(x, y, camera), 0);         //   0   0
                Color color6 = CastRay(camera.Position, GetPoint(x + 1, y, camera), 0);     //   1   0
                Color color7 = CastRay(camera.Position, GetPoint(x - 1, y + 1, camera), 0); //  -1   1
                Color color8 = CastRay(camera.Position, GetPoint(x, y + 1, camera), 0);     //   0   1
                Color color9 = CastRay(camera.Position, GetPoint(x + 1, y + 1, camera), 0); //   1   1

                var r = (color1.R + color2.R + color3.R + color4.R + color5.R + color6.R + color7.R + color8.R + color9.R ) / 9.0f;
                var g = (color1.G + color2.G + color3.G + color4.G + color5.G + color6.G + color7.G + color8.G + color9.G ) / 9.0f;
                var b = (color1.B + color2.B + color3.B + color4.B + color5.B + color6.B + color7.B + color8.B + color9.B ) / 9.0f;

                return new Color(r, g, b, 0);
            }
            else
            {
                return CastRay(camera.Position, GetPoint(x, y, camera), 0);
            }
        }

        private Color CastRay(Vector3 start, Vector3 direction, int depth)
        {
            var intersect = Intersect(new Ray(start, direction), _loadedScene);
            if (intersect == null)
            {
                return new Color(0,0,0,0);
            }

            return CalculateColor(intersect, depth);
        }

        private double CastShadowRay(Vector3 start, Vector3 direction)
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

            foreach (var transform in scene.Objects)
            {
                foreach (var obj in transform.TransformedObjects)
                {
                    ray.Start = Vector3.Transform(ray.Start, transform.invert);
                    ray.Direction = Vector3.Normalize(Vector3.TransformNormal(ray.Direction, transform.invert));

                    var intersect = obj.Intersect(ray);
                    if (intersect != null)
                    {
                        if (min == null || min.Distance > intersect.Distance)
                        {
                            min = intersect;
                        }
                    }
                }
            }
            return min;
        }


        private Color CalculateColor(Selection selection, int depth)
        {
            IMaterial material = selection.Object.GetMaterial();
            var position = selection.Distance * selection.Ray.Direction + selection.Ray.Start;
            var normal = selection.Object.GetNormal(position);
            var reflectDirection = selection.Ray.Direction - 2 * Vector3.Dot(normal, selection.Ray.Direction) * normal;
            var illuminated = GetLight(selection.Object.GetMaterial(), position, normal, reflectDirection, _loadedScene) + new Color(0.011f, 0.011f, 0.011f, 0);

            if (depth < ReflectionDepth)
            {
                float reflectivity = FresnelReflectivity(selection.Ray.Direction, normal, material.GetReflection(position), 1.0F, material.GetRefractionIndex());
                float transmittance = 1.0F - reflectivity;

                if (material.GetTransparency() > 0.0F && transmittance > 1e-5F)
                {
                    if (Refract(selection.Ray.Direction, normal, material.GetRefractionIndex(), out var transmissionDirection))
                    {
                        var biasedRefractiveSurfacePoint = position + transmissionDirection * 0.0001F;
                        illuminated += ( material.GetTransparency() * transmittance ) * CastRay(biasedRefractiveSurfacePoint, transmissionDirection, depth + 1);
                    }
                }

                if (reflectivity > 0.0F)
                {
                    return illuminated + CreateReflection(selection.Object.GetMaterial(), position + 0.0001f * reflectDirection, reflectDirection, depth);
                }
            }
            return illuminated; //Ambient light
        }


        private Color GetLight(IMaterial material, Vector3 position, Vector3 normal, Vector3 reflectDirection, Scene scene)
        {
            var illuminatedColor = new Color(0, 0, 0, 0);
            foreach (LightSource light in scene.Lights)
            {
                Vector3 lightVector = Vector3.Normalize(light.Position - position);
                var illumination = (float) Vector3.Dot(lightVector, normal);

                if (!CheckShading(light, position))
                {

                    Color lightColor = new Color(0, 0, 0, 0);
                    if (illumination > 0)
                    {
                        lightColor = illumination * light.Color;
                    }

                    Color specularColor = new Color(0, 0, 0, 0);
                    var specular = ( (float) Vector3.Dot(lightVector, Vector3.Normalize(reflectDirection)) );
                    if (specular > 0)
                    {
                        specularColor = ( (float) Math.Pow(specular, material.GetDisparity()) ) * light.Color;
                    }

                    illuminatedColor = illuminatedColor + material.GetDiffuse(position) * lightColor + material.GetSpecular(position) * specularColor;
                }
            }
            return illuminatedColor;
        }

        private bool CheckShading(LightSource source, Vector3 position)
        { 
            Vector3 lightDistance = source.Position - position;
            Vector3 lightVector = Vector3.Normalize(lightDistance);
            double intersectionDistance = CastShadowRay(position, lightVector);
            return !( intersectionDistance == 0 || intersectionDistance > Vector3Extension.Magnitude(lightDistance));
        }

        private float FresnelReflectivity(in Vector3 incidentDirection, in Vector3 normal, float minReflectance, float ior1, float ior2)
        {
            var normalTmp = normal;
            float theta = Math.Min(1.0F, Math.Max(-1.0F, Vector3.Dot(incidentDirection, normal)));
            if (theta < 0.0F)
            {
                theta = -theta;
            }
            else
            {
                float temp = ior1;
                ior1 = ior2;
                ior2 = temp;

                normalTmp = -normalTmp;
            }

            float t1 = ( ior1 - ior2 ) / ( ior1 + ior2 );
            float r0 = Math.Max(minReflectance, t1 * t1);
            float t2 = 1.0F - (float) Math.Cos(theta);
            float t3 = t2 * t2;
            float t4 = t3 * t3 * t2;
            return r0 + ( 1.0F - r0 ) * t4;

        }

        private bool Refract(Vector3 incidentDirection, Vector3 surfaceNormal, float refractiveIndex, out Vector3 transmissionDirection)
        {
            float cosi = Math.Min(1.0F, Math.Max(-1.0F, Vector3.Dot(incidentDirection, surfaceNormal)));
            float fior = 1.0F;
            float sior = refractiveIndex;

            if (cosi < 0.0F)
            {
                cosi = -cosi;
            }
            else
            {
                float temp = fior;
                fior = sior;
                sior = temp;

                surfaceNormal = -surfaceNormal;
            }

            float ratio = fior / sior;
            float t1 = ( 1.0F - cosi * cosi );
            float k = 1.0F - ratio * ratio * t1;
            if (k > 0.0F)
            {
                transmissionDirection = ( incidentDirection * ratio ) + surfaceNormal * ( ratio * cosi - (float) Math.Sqrt(k) );
                return true;
            }

            transmissionDirection = Vector3.Zero;
            return false;
        }


        private Color CreateReflection(IMaterial material, Vector3 position, Vector3 rayDirection, int depth)
        {
            return material.GetReflection(position) * CastRay(position, rayDirection, depth + 1);
        }

        private Vector3 GetPoint(double x, double y, Camera camera)
        {
            return Vector3.Normalize(camera.Forward + (CenetralizeX(x) * camera.Right + CentralizeY(y) * camera.Up ));
        }

        private float CenetralizeX(double x)
        {
            return (float)((x - Width / 2.0 ) / ( 2.0 * Width));
        }
        private float CentralizeY(double y)
        {
            return (float)(-( y - Height / 2.0 ) / ( 2.0 * Height ));
        }
    }
}
