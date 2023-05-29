using Newtonsoft.Json;

namespace DAoCToolSuite.ChimpTool.Json
{
    public class Insignia
    {
        [JsonProperty("insignia_emblem")]
        public int InsigniaEmblem { get; set; }

        [JsonProperty("insignia_emblem_color")]
        public int InsigniaEmblemColor { get; set; }

        [JsonProperty("insignia_pattern")]
        public int InsigniaPattern { get; set; }

        [JsonProperty("insignia_color_one")]
        public int InsigniaColorOne { get; set; }

        [JsonProperty("insignia_color_two")]
        public int InsigniaColorTwo { get; set; }
    }
}
