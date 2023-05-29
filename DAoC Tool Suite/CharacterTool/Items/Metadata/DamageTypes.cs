using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAoCToolSuite.CharacterTool.Items.Metadata
{
    public class DamageTypes
    {
        public List<DamageType> damage_types = new List<DamageType>();
        public void AddDamageType(int _id, string _damage_type)
        {
            damage_types.Add(new DamageType() { id = _id, damage_type = _damage_type });
        }
        public DamageTypes()
        {
            AddDamageType(1, "Crush");
            AddDamageType(2, "Slash");
            AddDamageType(3, "Thrust");
            AddDamageType(5, "Siege");
            AddDamageType(17, "Spirit");
        }
    }
    public class DamageType
    {
        public int id { get; set; } = -1;
        public string? damage_type { get; set; }
    }
}
