using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAoCToolSuite.CharacterTool.Items.Metadata
{
    public class SubTypes
    {
        public Dictionary<int, List<SubType>> sub_types = new Dictionary<int, List<SubType>>();
        public SubTypes()
        {
            //Stats
            sub_types.Add(
            1,
            new List<SubType>(){
                { new SubType(){id =0, sub_type="Strength"} },
                { new SubType(){id =1, sub_type="Dexterity"} },
                { new SubType(){id =2, sub_type="Constitution"} },
                { new SubType(){id =3, sub_type="Quickness"} },
                { new SubType(){id =4, sub_type="Intelligence"} },
                { new SubType(){id =5, sub_type="Piety"} },
                { new SubType(){id =6, sub_type="Empathy"} },
                { new SubType(){id =7, sub_type="Charisma"} },
                { new SubType(){id =10, sub_type="Acuity"} }
            });
            //Skills
            sub_types.Add(
            2,
            new List<SubType>(){
                { new SubType(){id =1, sub_type="Slash"}},
                { new SubType(){id =2, sub_type="Thrust"}},
                { new SubType(){id =4, sub_type="Bow"}},
                { new SubType(){id =8, sub_type="Parry"}},
                { new SubType(){id =14, sub_type="Sword"}},
                { new SubType(){id =16, sub_type="Hammer"}},
                { new SubType(){id =17, sub_type="Axe"}},
                { new SubType(){id =18, sub_type="LeftAxe"}},
                { new SubType(){id =19, sub_type="Stealth"}},
                { new SubType(){id =26, sub_type="Spear"}},
                { new SubType(){id =29, sub_type="Mending"}},
                { new SubType(){id =30, sub_type="Augmentation"}},
                { new SubType(){id =33, sub_type="Crush"}},
                { new SubType(){id =34, sub_type="Pacification"}},
                { new SubType(){id =37, sub_type="Cave Magic"}},
                { new SubType(){id =38, sub_type="Darkness"}},
                { new SubType(){id =39, sub_type="Suppression"}},
                { new SubType(){id =42, sub_type="Runecarving"}},
                { new SubType(){id =43, sub_type="Shield"}},
                { new SubType(){id =44, sub_type="Thrown"}},
                { new SubType(){id =46, sub_type="Flexible"}},
                { new SubType(){id =47, sub_type="Staff"}},
                { new SubType(){id =48, sub_type="Summoning"}},
                { new SubType(){id =50, sub_type="Stormcalling"}},
                { new SubType(){id =62, sub_type="Beastcraft"}},
                { new SubType(){id =64, sub_type="Polearm"}},
                { new SubType(){id =65, sub_type="Two Handed"}},
                { new SubType(){id =66, sub_type="Fire Magic"}},
                { new SubType(){id =67, sub_type="Wind Magic"}},
                { new SubType(){id =68, sub_type="Cold Magic"}},
                { new SubType(){id =69, sub_type="Earth Magic"}},
                { new SubType(){id =70, sub_type="Light Magic"}},
                { new SubType(){id =71, sub_type="Matter Magic"}},
                { new SubType(){id =72, sub_type="Body Magic"}},
                { new SubType(){id =73, sub_type="Spirit Magic"}},
                { new SubType(){id =74, sub_type="Mind Magic"}},
                { new SubType(){id =75, sub_type="Void Magic"}},
                { new SubType(){id =76, sub_type="Mana Magic"}},
                { new SubType(){id =77, sub_type="Dual Wield"}},
                { new SubType(){id =78, sub_type="Composite Bow"}},
                { new SubType(){id =79, sub_type="Envenom"}},
                { new SubType(){id =82, sub_type="Battlesongs"}},
                { new SubType(){id =83, sub_type="Enhancements"}},
                { new SubType(){id =84, sub_type="Enchantments"}},
                { new SubType(){id =85, sub_type="Illusions"}},
                { new SubType(){id =88, sub_type="Rejuvenation"}},
                { new SubType(){id =89, sub_type="Smiting"}},
                { new SubType(){id =90, sub_type="Longbow"}},
                { new SubType(){id =91, sub_type="Crossbow"}},
                { new SubType(){id =95, sub_type="Flute"}},
                { new SubType(){id =97, sub_type="Chants"}},
                { new SubType(){id =98, sub_type="Instruments"}},
                { new SubType(){id =101, sub_type="Blades"}},
                { new SubType(){id =102, sub_type="Blunt"}},
                { new SubType(){id =103, sub_type="Piercing"}},
                { new SubType(){id =104, sub_type="Large Weaponry"}},
                { new SubType(){id =105, sub_type="Mentalism"}},
                { new SubType(){id =106, sub_type="Regrowth"}},
                { new SubType(){id =107, sub_type="Nurture"}},
                { new SubType(){id =108, sub_type="Nature Affinity"}},
                { new SubType(){id =109, sub_type="Music"}},
                { new SubType(){id =110, sub_type="Celtic Dual"}},
                { new SubType(){id =112, sub_type="Celtic Spear"}},
                { new SubType(){id =113, sub_type="Recurve Bow"}},
                { new SubType(){id =114, sub_type="Valor"}},
                { new SubType(){id =117, sub_type="Envenom"}},
                { new SubType(){id =118, sub_type="Critical Strike"}},
                { new SubType(){id =119, sub_type="Nightshade"}},
                { new SubType(){id =120, sub_type="Deathsight"}},
                { new SubType(){id =121, sub_type="Painworking"}},
                { new SubType(){id =122, sub_type="Death Servant"}},
                { new SubType(){id =123, sub_type="Soulrending"}},
                { new SubType(){id =124, sub_type="Hand to Hand"}},
                { new SubType(){id =125, sub_type="Scythe"}},
                { new SubType(){id =126, sub_type="Bone Army"}},
                { new SubType(){id =127, sub_type="Arboreal Path"}},
                { new SubType(){id =128, sub_type="Savagery"}},
                { new SubType(){id =129, sub_type="Creeping Path"}},
                { new SubType(){id =130, sub_type="Verdant Path"}},
                { new SubType(){id =133, sub_type="Odin's Will"}},
                { new SubType(){id =134, sub_type="Spectral Guard"}},
                { new SubType(){id =135, sub_type="Phantasmal Wail"}},
                { new SubType(){id =136, sub_type="Ethereal Shriek"}},
                { new SubType(){id =137, sub_type="Shadow Mastery"}},
                { new SubType(){id =138, sub_type="Vampiiric Embrace"}},
                { new SubType(){id =139, sub_type="Dementia"}},
                { new SubType(){id =140, sub_type="Witchcraft"}},
                { new SubType(){id =141, sub_type="Cursing"}},
                { new SubType(){id =142, sub_type="Hexing"}},
                { new SubType(){id =144, sub_type="Power Strikes"}},
                { new SubType(){id =145, sub_type="Magnetism"}},
                { new SubType(){id =146, sub_type="Aura Manipulation"}},
                { new SubType(){id =147, sub_type="Fist Wraps"}},
                { new SubType(){id =148, sub_type="Mauler Staff"}},
                { new SubType(){id =300, sub_type="All Melee"}},
                { new SubType(){id =301, sub_type="All Duel"}},
                { new SubType(){id =302, sub_type="All Archery"}},
                { new SubType(){id =303, sub_type="All Casting"}},
                { new SubType(){id =304, sub_type="All Focus"}}
            });
            //Resistances
            sub_types.Add(
            3,
            new List<SubType>() {

                { new SubType() { id = 1, sub_type = "Crush" } },
                { new SubType() { id = 2, sub_type = "Slash" } },
                { new SubType() { id = 3, sub_type = "Thrust" } },
                { new SubType() { id = 5, sub_type = "Siege" } },
                { new SubType() { id = 10, sub_type = "Heat" } },
                { new SubType() { id = 11, sub_type = "Spirit" } },
                { new SubType() { id = 12, sub_type = "Cold" } },
                { new SubType() { id = 13, sub_type = "Matter" } },
                { new SubType() { id = 14, sub_type = "Heat" } },
                { new SubType() { id = 15, sub_type = "Matter" } },
                { new SubType() { id = 16, sub_type = "Body" } },
                { new SubType() { id = 17, sub_type = "Spirit" } },
                { new SubType() { id = 18, sub_type = "Spirit" } },
                { new SubType() { id = 19, sub_type = "Cold" } },
                { new SubType() { id = 20, sub_type = "Energy" } },
                { new SubType() { id = 21, sub_type = "Essence" } },
                { new SubType() { id = 22, sub_type = "Energy" } },
                { new SubType() { id = 23, sub_type = "Cold" } },
                { new SubType() { id = 25, sub_type = "Body" } },
                { new SubType() { id = 26, sub_type = "Body" } },
                { new SubType() { id = 27, sub_type = "Body" } }
            });
            //Spell Lines
            sub_types.Add(
            4,
            new List<SubType>()
            {
                {new SubType() { id = 37, sub_type="Cave Magic"}},
                {new SubType() { id = 38, sub_type="Darkness"}},
                {new SubType() { id = 39, sub_type="Suppression"}},
                {new SubType() { id = 42, sub_type="Runecarving"}},
                {new SubType() { id = 48, sub_type="Summoning"}},
                {new SubType() { id = 66, sub_type="Fire Magic"}},
                {new SubType() { id = 67, sub_type="Wind Magic"}},
                {new SubType() { id = 68, sub_type="Cold Magic"}},
                {new SubType() { id = 69, sub_type="Earth Magic"}},
                {new SubType() { id = 70, sub_type="Light Magic"}},
                {new SubType() { id = 71, sub_type="Matter Magic"}},
                {new SubType() { id = 72, sub_type="Body Magic"}},
                {new SubType() { id = 73, sub_type="Spirit Magic"}},
                {new SubType() { id = 74, sub_type="Mind Magic"}},
                {new SubType() { id = 75, sub_type="Void =  Magic"}},
                {new SubType() { id = 76, sub_type="Mana Magic"}},
                {new SubType() { id = 84, sub_type="Enchantments"}},
                {new SubType() { id = 85, sub_type="Illusions"}},
                {new SubType() { id = 105, sub_type="Mentalism"}},
                {new SubType() { id = 120, sub_type="Deathsight"}},
                {new SubType() { id = 121, sub_type="Painworking"}},
                {new SubType() { id = 122, sub_type="Death Servant"}},
                {new SubType() { id = 126, sub_type="Bone Army"}},
                {new SubType() { id = 127, sub_type="Arboreal Path"}},
                {new SubType() { id = 129, sub_type="Creeping Path"}},
                {new SubType() { id = 130, sub_type="Verdant Path"}},
                {new SubType() { id = 134, sub_type="Spectral Guard"}},
                {new SubType() { id = 135, sub_type="Phantasmal Wail"}},
                {new SubType() { id = 136, sub_type="Ethereal Shriek"}},
                {new SubType() { id = 140, sub_type="Witchcraft"}},
                {new SubType() { id = 141, sub_type="Cursing"}},
                {new SubType() { id = 142, sub_type="Hexing"}},
                {new SubType() { id = 303, sub_type="All Casting"}},
                {new SubType() { id = 304, sub_type="All Focus" }}
            });
            //Crafting
            sub_types.Add(
            5,
            new List<SubType>()
            {
                { new SubType(){id =1, sub_type="Weaponcraft"}},
                { new SubType(){id =2, sub_type="Armorcraft"}},
                { new SubType(){id =3, sub_type="Siegecraft"}},
                { new SubType(){id =4, sub_type="Alchemy"}},
                { new SubType(){id =11, sub_type="Tailoring"}},
                { new SubType(){id =12, sub_type="Fletching"}},
                { new SubType(){id =13, sub_type="Spellcrafting"}}
            });
            //ToA Artifact
            sub_types.Add(
            6,
            new List<SubType>()
            {
                { new SubType(){id =1, sub_type="Arcane Siphon"}},
                { new SubType(){id =2, sub_type="Conversion"}},
                { new SubType(){id =3, sub_type="Radiant Aura"}},
                { new SubType(){id =4, sub_type="XP Bonus"}},
                { new SubType(){id =5, sub_type="Gold Bonus"}},
                { new SubType(){id =6, sub_type="Realm Point Bonus"}},
                { new SubType(){id =7, sub_type="Bounty Point Bonus" } }
            });
        }
    }

    public class SubType
    {
        public int id { get; set; } = -1;
        public string? sub_type { get; set; }
    }
}
