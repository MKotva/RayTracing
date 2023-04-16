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

        RenderController controller = new RenderController(2048, 2048);
        controller.Generate();

        Trace.TraceInformation("HDR image created");
        Trace.Flush();
    }

}
