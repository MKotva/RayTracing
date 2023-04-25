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

        public void RenderParallel(FloatImage img, ParallelOptions options)
        {
            Camera camera = _loadedScene.Camera;
            try
            {
                Parallel.For(0, Height, options, y =>
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

                });
            }
            catch (OperationCanceledException) { }
        }

        private Color RenderAntiAlias(int x, int y, Camera camera)
        {
            double delta = 0.5000000;
            if (x > 0 && y > 0 && x < Width - 1 && y < Height - 1)
            {
                Color color1 = CastRay(camera.Position, GetPoint(x - delta, y - delta, camera), 0);   //  -1, -1
                Color color2 = CastRay(camera.Position, GetPoint(x + delta, y - delta, camera), 0); //   1  -1
                Color color3 = CastRay(camera.Position, GetPoint(x - delta, y + delta, camera), 0); //  -1   1
                Color color4 = CastRay(camera.Position, GetPoint(x + delta, y + delta, camera), 0); //   1   1
                if((color1 == color2 && color2 == color3 && color3 == color4))
                {
                    return new Color(color1.R, color1.G, color1.B, 0);
                }


                Color color5 = CastRay(camera.Position, GetPoint(x, y - delta, camera), 0);     //   0  -1
                Color color6 = CastRay(camera.Position, GetPoint(x - delta, y, camera), 0);     //  -1   0
                Color color7 = CastRay(camera.Position, GetPoint(x, y, camera), 0);         //   0   0
                Color color8 = CastRay(camera.Position, GetPoint(x + delta, y, camera), 0);     //   1   0
                Color color9 = CastRay(camera.Position, GetPoint(x, y + delta, camera), 0);     //   0   1

                var r = ( color1.R + color2.R + color3.R + color4.R + color5.R + color6.R + color7.R + color8.R + color9.R ) / 9.0f;
                var g = ( color1.G + color2.G + color3.G + color4.G + color5.G + color6.G + color7.G + color8.G + color9.G ) / 9.0f;
                var b = ( color1.B + color2.B + color3.B + color4.B + color5.B + color6.B + color7.B + color8.B + color9.B ) / 9.0f;

                return new Color(r, g, b, 0);
            }
            else
            {
                return CastRay(camera.Position, GetPoint(x, y, camera), 0);
            }
        }

        private Color CastRay(Vector3D start, Vector3D direction, int depth)
        {
            var intersect = Intersect(new Ray(start, direction), _loadedScene);
            if (intersect == null)
            {
                return new Color(0, 0, 0, 0);
            }

            return CalculateColor(intersect, depth);
        }

        private double CastShadowRay(Vector3D start, Vector3D direction)
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

            foreach (var transform in scene.Transforms)
            {
                foreach (var obj in transform.TransformedObjects)
                {
                    var invertedMatrix = transform.Inverted.GetOpenTKMatrix();
                    var transformedRay = new Ray(Vector3D.Transform(ray.Start, invertedMatrix),
                        Vector3D.Normalize(Vector3D.TransformNormal(ray.Direction, invertedMatrix)));

                    var intersect = obj.Intersect(transformedRay);
                    if (intersect != null)
                    {
                        if (min == null || min.Distance > intersect.Distance)
                        {
                            intersect.Transform = transform;
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
            var reflectDirection = selection.Ray.Direction - 2 * Vector3D.Dot(normal, selection.Ray.Direction) * normal;

            //return material.GetDiffuse(position);

            var color = ( 1 - material.GetTransparency() ) * ( 1 - material.GetReflection(position) ) * GetLight(selection.Object.GetMaterial(), position, normal, reflectDirection, _loadedScene);

            if (depth < ReflectionDepth)
            {
                var reflectivity = FresnelReflectivity(selection.Ray.Direction, normal, material.GetReflection(position), 1.0, material.GetRefractionIndex());
                var transmittance = 1.0 - reflectivity;

                if (material.GetTransparency() > 0.0 && transmittance > 1e-5)
                {
                    if (Refract(selection.Ray.Direction, normal, material.GetRefractionIndex(), out var transmissionDirection))
                    {
                        var biasedRefractiveSurfacePoint = position + 0.001 * transmissionDirection;
                        color += material.GetTransparency() * CastRay(biasedRefractiveSurfacePoint, transmissionDirection, depth + 1);
                    }
                }

                if (reflectivity > 0.0)
                {
                    return color + CreateReflection(selection.Object.GetMaterial(), position + 0.001 * reflectDirection, reflectDirection, depth);
                }
            }
            return color; //Ambient light
        }


        private Color GetLight(IMaterial material, Vector3D position, Vector3D normal, Vector3D reflectDirection, Scene scene)
        {
            var illuminatedColor = new Color(0, 0, 0, 0);
            foreach (LightSource light in scene.Lights)
            {
                Vector3D lightVector = Vector3D.Normalize(light.Position - position);
                var illumination = Math.Abs(Vector3D.Dot(lightVector, normal));

                if (!CheckShading(light, position))
                {
                    Color lightColor = new Color(0, 0, 0, 0);
                    if (illumination > 0)
                    {
                        lightColor = illumination * light.Color;
                    }

                    Color specularColor = new Color(0, 0, 0, 0);
                    var specular = Vector3D.Dot(lightVector, Vector3D.Normalize(reflectDirection));
                    if (specular > 0)
                    {
                        specularColor = Math.Pow(specular, material.GetDisparity()) * light.Color;
                    }

                    illuminatedColor += material.GetDiffuse(position) * lightColor + material.GetSpecular(position) * specularColor;
            }
                else
            {
                illuminatedColor += material.GetAmbient() * illumination * material.GetDiffuse(position) * light.Color;
            }
        }
            return illuminatedColor;
        }

        private bool CheckShading(LightSource source, Vector3D position)
        { 
            Vector3D lightDistance = source.Position - position;
            Vector3D lightVector = Vector3D.Normalize(lightDistance);
            Vector3D shadowStart = position + ( 0.01 * lightVector );
            double intersectionDistance = CastShadowRay(shadowStart, lightVector);
            return !( intersectionDistance == 0 || intersectionDistance > Vector3Extension.Magnitude(lightDistance) );
        }

        private double FresnelReflectivity(in Vector3D incidentDirection, in Vector3D normal, double minReflectance, double ior1, double ior2)
        {
            var normalTmp = normal;
            var theta = Math.Min(1.0, Math.Max(-1.0, Vector3D.Dot(incidentDirection, normal)));
            if (theta < 0.0)
            {
                theta = -theta;
            }
            else
            {
                var temp = ior1;
                ior1 = ior2;
                ior2 = temp;

                normalTmp = -1 * normalTmp;
            }

            var t1 = ( ior1 - ior2 ) / ( ior1 + ior2 );
            var r0 = Math.Max(minReflectance, t1 * t1);
            var t2 = 1.0F - (float) Math.Cos(theta);
            var t3 = t2 * t2;
            var t4 = t3 * t3 * t2;
            return r0 + ( 1.0F - r0 ) * t4;

        }

        private bool RefractOld(Vector3D incidentDirection, Vector3D surfaceNormal, double refractiveIndex, out Vector3D transmissionDirection)
        {
            var cosi = Math.Min(1.0, Math.Max(-1.0, Vector3D.Dot(incidentDirection, surfaceNormal)));
            var fior = 1.0  ;
            var sior = refractiveIndex;

            if (cosi < 0.0)
            {
                cosi = -cosi;
            }
            else
            {
                var temp = fior;
                fior = sior;
                sior = temp;

                surfaceNormal = -1 * surfaceNormal;
            }

            var ratio = fior / sior;
            var t1 = ( 1.0 - cosi * cosi );
            var k = 1.0 - ratio * ratio * t1;
            if (k > 0.0F)
            {
                transmissionDirection = ( ratio * incidentDirection ) + ( ratio * cosi - Math.Sqrt(k) ) * surfaceNormal;
                return true;
            }

            transmissionDirection = Vector3D.Zero;
            return false;
        }

        private bool Refract(Vector3D direction, Vector3D surfaceNormal, double refractiveIndex, out Vector3D transmissionDirection)
        {
            var cosi = Math.Min(1.0, Math.Max(-1.0, Vector3D.Dot(direction, surfaceNormal)));
            var fior = 1.0;
            var sior = refractiveIndex;

            if (cosi < 0.0)
            {
                cosi = -cosi;
            }
            else
            {
                var temp = fior;
                fior = sior;
                sior = temp;

                surfaceNormal = -1 * surfaceNormal;
            }

            var ratio = fior / sior;
            var t1 = ( 1.0 - cosi * cosi );
            var k = 1.0 - ratio * ratio * t1;
            if (k > 0.0F)
            {
                transmissionDirection = ( ratio * direction ) + ( ratio * cosi - Math.Sqrt(k) ) * surfaceNormal;
                return true;
            }

            transmissionDirection = Vector3D.Zero;
            return false;
        }


        private Color CreateReflection(IMaterial material, Vector3D position, Vector3D rayDirection, int depth)
        {
            return material.GetReflection(position) * CastRay(position, rayDirection, depth + 1);
        }

        private Vector3D GetPoint(double x, double y, Camera camera)
        {
            return Vector3D.Normalize(camera.Forward + ( CenetralizeX(x) * camera.Right + CentralizeY(y) * camera.Up ));
        }

        private double CenetralizeX(double x)
        {
            return ( ( x - Width / 2.0 ) / ( 2.0 * Width ) );
        }
        private double CentralizeY(double y)
        {
            return ( -( y - Height / 2.0 ) / ( 2.0 * Height ) );
        }
    }
}
