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
            return new float[3] { 125, 125, 0 };
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
        private Vector GetPoint(double x, double y, Camera camera)
        {
            return Vector.Normalize(camera.Forward + (RecenterX(x) * camera.Right + RecenterY(y) * camera.Up));
        }

        private double RecenterX(double x)
        {
            return ( x - Width / 2.0 ) / ( 2.0 * Width );
        }
        private double RecenterY(double y)
        {
            return -( y - Height / 2.0 ) / ( 2.0 * Height );
        }
    }
}
