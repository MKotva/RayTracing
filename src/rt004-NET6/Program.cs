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

        var config = new ConfigDTO();
        ParamLoader.Load(config, ParamLoader.ParseInput(args[0]));

        RenderController controller = new RenderController(config.Width, config.Height);
        controller.Generate();

        Trace.TraceInformation("HDR image created");
        Trace.Flush();
    }

}
