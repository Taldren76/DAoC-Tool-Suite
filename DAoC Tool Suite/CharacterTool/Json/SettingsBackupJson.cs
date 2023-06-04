using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLLibrary;

namespace DAoCToolSuite.CharacterTool.Json
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(SettingsBackupJson))]
    public class SettingsBackupJson
    {
        public int Count => Backups?.Count??0;

        [JsonProperty]
        public List<SettingsBackUpModel>? Backups { get; set; }
    }
}
