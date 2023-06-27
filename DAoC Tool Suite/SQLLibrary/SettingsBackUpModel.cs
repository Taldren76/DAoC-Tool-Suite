using System.Text.Json;
using System.Text.Json.Serialization;

namespace SQLLibrary
{
    public class SettingsBackUpModel
    {
        [JsonPropertyName("index")]
        public int? Index { get; set; }
        public string? Date { get; set; }
        public string? FirstName { get; set; }
        public string? Realm { get; set; }
        public string? Class { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
        public string? INIFileName { get; set; }
        public string? IGNFileName { get; set; }
        public string? INIData { get; set; }
        public string? IGNData { get; set; }
        public DateTime DateTime
        {
            get
            {
                _ = DateTime.TryParse(Date, out DateTime temp);
                return temp;
            }
        }
        public DateOnly DateOnly => DateOnly.FromDateTime(DateTime);
    }
}
