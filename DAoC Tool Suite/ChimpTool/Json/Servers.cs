using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAoCToolSuite.ChimpTool.Json
{
    public class ServerListINI
    {
        public Servers? Servers { get; set; }
    }

    public class Server
    {
        public string? IP { get; set; }
        public int Port { get; set; }
        public string? Name { get; set; }
        public int Index { get; set; }
    }

    public class Servers
    {
        public List<Server>? Server { get; set; }
    }
}
