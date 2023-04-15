using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using Util;

namespace rt004;

internal class Program
{
  static void Main(string[] args)
  {
    Logger log = new Logger();

    var parameters = ParamLoader.ParseInput(args[0]);
    ParamLoader.Load<TestScene>(new TestScene(), parameters);

    var fi = GenerateFrame(1024, 1024);
    fi.SavePFM("test.pfm");

    Trace.TraceInformation("HDR image created");
    Trace.Flush();
  }

  public static FloatImage GenerateFrame(int width, int height)
  {
    return MandelBrotScene.GenerateImage(new FloatImage(width, height, 3), -2.0, -1.6, 1.0, 1.6, 1000);
  }
}
