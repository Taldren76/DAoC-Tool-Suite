namespace DAoCToolSuite.CharacterTool.Items.Metadata
{
    public class Realms
    {
        public List<Realm> realms { get; private set; } = new List<Realm>();
        private void AddRealm(int _id, string _realm)
        {
            realms.Add(new Realm() { id = _id, realm = _realm });
        }
        public Realms()
        {
            AddRealm(0, "All");
            AddRealm(1, "Albion");
            AddRealm(2, "Midgard");
            AddRealm(3, "Hibernia");
        }
    }

    public class Realm
    {
        public int id { get; set; } = -1;
        public string? realm { get; set; }
    }
}

