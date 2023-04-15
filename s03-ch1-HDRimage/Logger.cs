using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
  public class Logger
  {
    public static TraceSource tracer = new TraceSource("rt004");

    public Logger()
    {
      Trace.Listeners.Add(new TextWriterTraceListener("TextWriterOutput.log", "myListener"));
    }
  }
}
