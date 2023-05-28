using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korepi_Tool.Methods
{
    class Save
    {
        public string Path =null;
        public int DelayTime = 10000;
        public bool MHYProtect = false;
    }
    class Json
    {
        public Version version { get; set; }
        public string url { get; set; }
    }
    class KorpiJson
    {
        public string dll_md5 { get; set; }
        public string dll_url { get; set; }
        public string injector_md5 { get; set; }
        public string injector_url { get; set; }
    }
}
