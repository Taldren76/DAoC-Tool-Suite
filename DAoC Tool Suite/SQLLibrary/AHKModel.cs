using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SQLLibrary
{
    public class AHKModel
    {    
        public string? WebID { get; set; }
        public string? Account { get; set; }
        public string? AHKScriptPath { get; set; }
        public int? Version { get; set; }
    }
}
