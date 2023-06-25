using System.Text.Json.Serialization;

namespace SQLLibrary
{
    public class AccountModel
    {
        public string? Account { get; set; }
        [JsonPropertyName("index")]
        public int? Index { get; set; }
    }

    public class CredentialModel
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
