using System.IO;
using System.Text.Json;

namespace DAoCToolSuite.CharacterTool.Json
{
    public static class JsonFileReader
    {
        public static T? Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            T? output = JsonSerializer.Deserialize<T>(text);
            return output;
        }
    }


    public class ServerListINI
    {
        public Servers? Servers { get; set; }
    }

    public class Server
    {
        public string? IP { get; set; }
        public int Port { get; set; }
        public string? Name { get; set; }
        public int Index { get; set; }
    }

    public class Servers
    {
        public List<Server>? Server { get; set; }
    }

    public class Realm
    {
        public string? Hibernia { get; set; }
        public string? Albion { get; set; }
        public string? Midgard { get; set; }
    }

    public class RealmClassINI
    {
        public Realm? Realm { get; set; }
    }
}
