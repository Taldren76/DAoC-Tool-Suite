using Newtonsoft.Json;
using SQLLibrary.Enums;

namespace DAoCToolSuite.CharacterTool.Settings
{
    internal class Settings
    {
        [JsonProperty]
        public bool? AlwaysOnTop { get; set; }
        [JsonProperty]
        public string? DAoCCharacterFileDirectory { get; set; }
        [JsonProperty]
        public string? JsonBackupFileFullPath { get; set; }
        [JsonProperty]
        public HeaderNames? DisplayedDataGridViewHeaderNames { get; set; }
        [JsonProperty]
        public ColumnNames? DisplayedDatabaseColumnNames { get; set; }

    }
}
