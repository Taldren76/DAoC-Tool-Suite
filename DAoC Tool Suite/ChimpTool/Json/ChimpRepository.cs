using Newtonsoft.Json;

namespace DAoCToolSuite.ChimpTool.Json
{
    public class ChimpRepository
    {
        [JsonProperty]
        public Dictionary<string, List<ChimpJson>> Chimps { get; set; } = new Dictionary<string, List<ChimpJson>>();
        public int AccountCount => Chimps.Count;
        public int CharacterCount => SumAllCharacters();
        public int SumAllCharacters()
        {
            int sum = 0;
            foreach (KeyValuePair<string, List<ChimpJson>> account in Chimps)
            {
                sum += account.Value.Count;
            }
            return sum;
        }
        public bool Contains(string webID)
        {
            return Chimps.Where(x => x.Value.Where(y => y.WebID == webID).Any()).Any();
        }
        public void Add(string account, List<ChimpJson> chimps)
        {
            Chimps.Add(account, chimps);
        }
    }
}
