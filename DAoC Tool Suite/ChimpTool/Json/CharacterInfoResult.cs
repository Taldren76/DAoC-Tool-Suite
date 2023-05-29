using Newtonsoft.Json;

namespace DAoCToolSuite.ChimpTool.Json
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CharacterInfoResult
    {
        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("character_web_id")]
        public string? CharacterWebId { get; set; }

        [JsonProperty("class_name")]
        public string? ClassName { get; set; }

        [JsonProperty("crafting")]
        public Crafting? Crafting { get; set; }

        [JsonProperty("guild_info")]
        public GuildInfo? GuildInfo { get; set; }

        [JsonProperty("last_on_range")]
        public int LastOnRange { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("master_level")]
        public MasterLevel? MasterLevel { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("race")]
        public string? Race { get; set; }

        [JsonProperty("realm")]
        public int Realm { get; set; }

        [JsonProperty("realm_war_stats")]
        public RealmWarStats? RealmWarStats { get; set; }

        [JsonProperty("server_name")]
        public string? ServerName { get; set; }

        public bool IsValid { get; set; } = false;
    }

    public class Crafting
    {
        [JsonProperty("alchemy")]
        public int Alchemy { get; set; }

        [JsonProperty("armorcraft")]
        public int Armorcraft { get; set; }

        [JsonProperty("fletching")]
        public int Fletching { get; set; }

        [JsonProperty("siegecraft")]
        public int Siegecraft { get; set; }

        [JsonProperty("spellcraft")]
        public int Spellcraft { get; set; }

        [JsonProperty("tailoring")]
        public int Tailoring { get; set; }

        [JsonProperty("weaponcraft")]
        public int Weaponcraft { get; set; }
    }

    public class Current
    {
        [JsonProperty("bounty_points")]
        public int BountyPoints { get; set; }

        [JsonProperty("player_kills")]
        public PlayerKills? PlayerKills { get; set; }

        [JsonProperty("realm_points")]
        public int RealmPoints { get; set; }
    }

    public class GuildInfo
    {
        [JsonProperty("guild_name")]
        public string? GuildName { get; set; }

        [JsonProperty("guild_rank")]
        public int GuildRank { get; set; }

        [JsonProperty("guild_web_id")]
        public string? GuildWebId { get; set; }

        [JsonProperty("insignia")]
        public Insignia? Insignia { get; set; }
    }

    public class Albion
    {
        [JsonProperty("death_blows")]
        public int DeathBlows { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("kills")]
        public int Kills { get; set; }

        [JsonProperty("solo_kills")]
        public int SoloKills { get; set; }
    }

    public class Hibernia
    {
        [JsonProperty("death_blows")]
        public int DeathBlows { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("kills")]
        public int Kills { get; set; }

        [JsonProperty("solo_kills")]
        public int SoloKills { get; set; }
    }

    public class MasterLevel
    {
        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }
    }

    public class Midgard
    {
        [JsonProperty("death_blows")]
        public int DeathBlows { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("kills")]
        public int Kills { get; set; }

        [JsonProperty("solo_kills")]
        public int SoloKills { get; set; }
    }

    public class PlayerKills
    {
        [JsonProperty("albion")]
        public Midgard? Albion { get; set; }

        [JsonProperty("hibernia")]
        public Hibernia? Hibernia { get; set; }

        [JsonProperty("midgard")]
        public Midgard? Midgard { get; set; }

        [JsonProperty("total")]
        public Total? Total { get; set; }
    }

    public class RealmWarStats
    {
        [JsonProperty("current")]
        public Current? Current { get; set; }
    }

    public class Total
    {
        [JsonProperty("death_blows")]
        public int DeathBlows { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("kills")]
        public int Kills { get; set; }

        [JsonProperty("solo_kills")]
        public int SoloKills { get; set; }
    }
}
