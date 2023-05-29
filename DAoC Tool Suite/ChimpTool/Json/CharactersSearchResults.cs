using Newtonsoft.Json;

namespace DAoCToolSuite.ChimpTool.Json
{
    public class CharacterSearchResult
    {
        [JsonProperty("parameters")]
        public List<Parameter>? Parameters { get; set; }

        [JsonProperty("results")]
        public List<Result>? Results { get; set; }
        public bool IsValid { get; set; }
    }

    public class Parameter
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("value")]
        public string? Value { get; set; }
    }

    public class Result
    {
        [JsonProperty("character_web_id")]
        public string? CharacterWebId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("server_name")]
        public string? ServerName { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("realm")]
        public int Realm { get; set; }

        [JsonProperty("race")]
        public string? Race { get; set; }

        [JsonProperty("class_name")]
        public string? ClassName { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("last_on_range")]
        public int LastOnRange { get; set; }

        [JsonProperty("realm_points")]
        public int RealmPoints { get; set; }

        [JsonProperty("guild_info")]
        public GuildInfo? GuildInfo { get; set; }
    }
}
