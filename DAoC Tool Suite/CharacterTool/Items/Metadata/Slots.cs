using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAoCToolSuite.CharacterTool.Items.Metadata
{
    public class Slots
    {
        public List<Slot> slots = new List<Slot>();
        public void AddSlot(int _id, string _slot)
        {
            slots.Add(new Slot() { id = _id, slot = _slot });
        }
        public Slots() {
            AddSlot(1, "Helm");
            AddSlot(2, "Hands");
            AddSlot(3, "Feet");
            AddSlot(4, "Jewel");
            AddSlot(5, "Torso");
            AddSlot(6, "Cloak");
            AddSlot(7, "Legs");
            AddSlot(8, "Arms");
            AddSlot(9, "Necklace");
            AddSlot(12, "Belt");
            AddSlot(13, "Bracer");
            AddSlot(15, "Ring");
            AddSlot(16, "Ring");
            AddSlot(17, "Mythirian");
        }
    }

    public class Slot
    {
        public int id { get; set; }
        public string? slot { get; set; }
    }
}
