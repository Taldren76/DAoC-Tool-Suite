using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAoCToolSuite.CharacterTool.Items.Metadata
{
    public class Absorptions
    {
        public List<Absorption> absorptions = new List<Absorption>();
        public void AddAbsorbtion(int _id, string _absorption)
        {
            absorptions.Add(new Absorption() { id=_id, absorption =  _absorption });
        }
        public Absorptions() {
            AddAbsorbtion(0, "Cloth");
            AddAbsorbtion(10, "Leather");
            AddAbsorbtion(19, "Studded");
            AddAbsorbtion(27, "Chain");
            AddAbsorbtion(34, "Plate");
        }

    }

    public class Absorption
    {
        public int id { get; set; } = -1;
        public string? absorption { get; set; }
    }
}
