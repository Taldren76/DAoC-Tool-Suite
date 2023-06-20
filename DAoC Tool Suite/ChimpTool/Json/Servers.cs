using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAoCToolSuite.ChimpTool.Json
{
    public class ServerListINI
    {
        [JsonPropertyName("Servers")]
        public Servers? Servers { get; set; }
    }

    public class Server
    {
        [JsonPropertyName("IP")]
        public string? IP { get; set; }

        [JsonPropertyName("Port")]
        public int? Port { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Index")]
        public int? Index { get; set; }
    }

    public class Servers
    {
        [JsonPropertyName("Server")]
        public List<Server>? Server { get; set; }
    }
}
