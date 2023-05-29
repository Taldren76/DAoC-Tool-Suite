using Newtonsoft.Json;
using SQLLibrary.Enums;

namespace DAoCToolSuite.ChimpTool.Settings
{
    internal class Settings
    {
        [JsonProperty]
        public bool? AlwaysOnTop { get; set; }
        [JsonProperty]
        public string? LastAccount { get; set; }
        [JsonProperty]
        public string? DAoCCharacterFileDirectory { get; set; }
        [JsonProperty]
        public string? JsonBackupFileFullPath { get; set; }
        [JsonProperty]
        public bool? UseSelenium { get; set; }
        [JsonProperty]
        public bool? UseAPI { get; set; }
        [JsonProperty]
        public ServerCluster? Server { get; set; }
        [JsonProperty]
        public HeaderNames? DisplayedDataGridViewHeaderNames { get; set; }
        [JsonProperty]
        public ColumnNames? DisplayedDatabaseColumnNames { get; set; }

    }
}
