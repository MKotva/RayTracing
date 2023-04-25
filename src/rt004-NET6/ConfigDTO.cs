using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rt004
{
    class ConfigDTO
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ReflectionDepth { get; set; }
        public bool AntiAlias { get; set; }
        public bool Parallel { get; set; }
        public ConfigDTO() { }
    }
}
