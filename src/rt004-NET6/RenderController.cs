using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        int ReflectionDepth { get; set; }

        bool AntiAlias { get; set; }

        RayTracer RayTracer { get; set; }

        public RenderController(int width, int height, int reflectionDepth, bool shouldAntiAlias) 
        {
            Width = width;
            Height = height;
            ReflectionDepth = reflectionDepth;

            Scene scene = new Scene();
            scene.LoadDefaultcScene();
            Scene.ToJson(scene, "test.json");
            scene = Scene.FromJson("test.json");

            if(shouldAntiAlias)
            {
                Trace.TraceInformation("Rendering with Anti-Alias.");
                Trace.Flush();
            }

            RayTracer = new RayTracer(width, height, ReflectionDepth, shouldAntiAlias, scene);
        }

        public void Generate()
        {
            Trace.TraceInformation("Single thread rendering");
            Trace.Flush();

            var fi = new FloatImage(Width, Height, 3);
            RayTracer.Render(fi);
            fi.SavePFM("testLightScene1.pfm");
        }

        public void GenerateParallel()
        {
            Trace.TraceInformation("Parallel rendering");
            Trace.Flush();

            var fi = new FloatImage(Width, Height, 3);
            RayTracer.RenderParallel(fi, new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount});
            fi.SavePFM("testLightScene1.pfm");
        }
    }
}
