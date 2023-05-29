using Newtonsoft.Json;

namespace DAoCToolSuite.ChimpTool.Json
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Alliance
    {
        [JsonProperty("alliance_leader")]
        public AllianceLeader? AllianceLeader { get; set; }

        [JsonProperty("alliance_members")]
        public List<AllianceMember>? AllianceMembers { get; set; }
    }

    public class AllianceLeader
    {
        [JsonProperty("guild_web_id")]
        public string? GuildWebId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("realm_points")]
        public int RealmPoints { get; set; }
    }

    public class AllianceMember
    {
        [JsonProperty("guild_web_id")]
        public string? GuildWebId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("realm_points")]
        public int RealmPoints { get; set; }
    }

    public class Entry
    {
        [JsonProperty("character_web_id")]
        public string? CharacterWebId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("class_name")]
        public string? ClassName { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("leaderboard_position")]
        public int LeaderboardPosition { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }



    public class Leaderboard
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("entries")]
        public List<Entry>? Entries { get; set; }
    }

    public class RealmWarOverall
    {
        [JsonProperty("realm_points")]
        public int RealmPoints { get; set; }

        [JsonProperty("bounty_points")]
        public int BountyPoints { get; set; }

        [JsonProperty("outposts_captured")]
        public int OutpostsCaptured { get; set; }

        [JsonProperty("relics_captured")]
        public int RelicsCaptured { get; set; }
    }

    public class GuildInfoResults
    {
        [JsonProperty("guild_web_id")]
        public string? GuildWebId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("server_name")]
        public string? ServerName { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("insignia")]
        public Insignia? Insignia { get; set; }

        [JsonProperty("webpage")]
        public string? Webpage { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("owned_housing_lot")]
        public int OwnedHousingLot { get; set; }

        [JsonProperty("claimed_outpost")]
        public int ClaimedOutpost { get; set; }

        [JsonProperty("member_characters")]
        public int MemberCharacters { get; set; }

        [JsonProperty("member_accounts")]
        public int MemberAccounts { get; set; }

        [JsonProperty("alliance")]
        public Alliance? Alliance { get; set; }

        [JsonProperty("realm_war_overall")]
        public RealmWarOverall? RealmWarOverall { get; set; }

        [JsonProperty("leaderboards")]
        public List<Leaderboard>? Leaderboards { get; set; }
    }
}
