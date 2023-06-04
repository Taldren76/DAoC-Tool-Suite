using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DAoCToolSuite.CharacterTool.Json
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(RecordRepository))]
    internal class RecordRepository
    {
        [JsonProperty]
        public List<CharacterRecord>? Characters { get; set; }
        internal int Count => Characters?.Count ?? -1;
        internal bool Contains(string name)
        {
            return Characters?.Where(x => x.Name == name).Count() > 0;
        }
    }
}
