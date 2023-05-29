namespace SQLLibrary
{
    public class GuildModel
    {
        public string? WebID { get; set; }
        public string? Name { get; set; }
        public bool IsValid => !string.IsNullOrEmpty(WebID) && !string.IsNullOrEmpty(Name);
    }
}
