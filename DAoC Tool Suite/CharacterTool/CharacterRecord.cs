using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DAoCToolSuite.CharacterTool
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(CharacterRecord))]
    internal class CharacterRecord
    {
        [JsonProperty]
        public string? Name { get; set; }
        [JsonProperty]
        public string? Realm { get; set; }
        [JsonProperty]
        public string? Class { get; set; }
        [JsonProperty]
        public string? Description { get; set; }
        [JsonProperty]
        public CharacterINI? CharacterINI { get; set; }
        [JsonProperty]
        public string? Server { get; set; }
        [JsonProperty]
        public int Index { get; set; }
    }
}
