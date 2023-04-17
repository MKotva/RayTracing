using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace rt004
{
    public class RenderController
    {     
        int Width { get; set; }
        int Height { get; set; }

        RayTracer RayTracer { get; set; }

        public RenderController(int width, int height) 
        {
            Width = width;
            Height = height;

            Scene scene = new Scene();
            RayTracer = new RayTracer(width, height, scene);
        }

        public void Generate()
        {
            var fi = new FloatImage(Width, Height, 3);
            RayTracer.Render(fi);
            fi.SavePFM("testLightScene.pfm");
        }
    }
}
