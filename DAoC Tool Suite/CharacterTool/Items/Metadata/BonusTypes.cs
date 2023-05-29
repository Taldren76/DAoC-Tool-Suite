using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DAoCToolSuite.CharacterTool.Items.Metadata
{
    public class BonusTypes
    {
        private SubTypes SubTypes = new SubTypes();
        public List<Bonus> bonus_types { get; private set; } = new List<Bonus>();
        private void AddBonusType(int _id, string _name, double _utility, int subtypeindex=-1)
        {

            bonus_types.Add(new Bonus()
            {
                id = _id,
                name = _name,
                sub_types = (subtypeindex == -1?null: SubTypes.sub_types[subtypeindex]), //Stats
                utility = _utility
            });
        }

        public BonusTypes()
        {
            //Stats
            AddBonusType(1, "Stats", 0.66666666666666666666666666666667, 1);

            //Skills
            AddBonusType(2, "Skills", 5, 2);

            //Hit Points
            AddBonusType(4, "Hit Points", 0.25);

            //Resistance
            AddBonusType(5, "Resistance", 2, 3);

            //Focus
            AddBonusType(6, "Focus", 0, 4);

            //Toa Melee Damage
            AddBonusType(8, "Toa Melee Damage", 5);

            //Toa Magic Damage
            AddBonusType(9, "Toa Magic Damag", 5);

            //Toa Style Damage
            AddBonusType(10 ,"Toa Style Damage", 5);

            //Toa Archery Range
            AddBonusType(11, "Toa Archery Range", 5);

            //Toa Spell1 Range
            AddBonusType(12, "Toa Spell Range", 5);

            //Toa Spell Duration 2
            AddBonusType(13, "Toa Spell Duration", 2);

            //Toa Buff Bonus
            AddBonusType(14, "Toa Buff Bonus", 2);

            //Toa Debuff Bonus 2
            AddBonusType(15, "Toa Debuff Bonus", 2);

            //Toa Heal Bonus 2
            AddBonusType(16, "Toa Heal Bonus", 2);

            //Toa Fatigue 2
            AddBonusType(17, "Toa Fatigue", 2);

            //Toa Melee Speed 5
            AddBonusType(19, "Toa Melee Speed", 5);

            //Toa Archery Speed 5
            AddBonusType(20, "Toa Archery Speed", 5);

            //Toa Cast Speed 5
            AddBonusType(21, "Toa Cast Speed", 5);

            //Armor Factor (AF) 1
            AddBonusType(22, "Armor Factor (AF)", 1);

            //Crafting Min Quality 0
            AddBonusType(22, "Crafting Min Quality", 0);

            //Crafting Quality 0
            AddBonusType(24, "Crafting Quality", 0);

            //Crafting Speed 0
            AddBonusType(25, "Crafting Speed", 0, 5);

            //Crafting Skill Gain 0
            AddBonusType(26, "Crafting Skill Gain", 0, 5);

            //Toa Archery Damage 5
            AddBonusType(27, "Toa Archery Damage", 0);

            //Toa Overcap 2
            AddBonusType(28, "Toa Overcap", 2, 1);

            //Toa Hit Points Cap 0.25
            AddBonusType(29, "Toa Hit Points Cap", 0.25);

            //Toa Power Pool Cap 2
            AddBonusType(30, "Toa Power Pool Cap", 2);

            //Toa Fatigue Cap 2
            AddBonusType(31, "Toa Fatigue Cap", 2);

            //Toa Resistance Piece 5
            AddBonusType(32, "Toa Fatigue Cap", 5);

            //Toa Power Pool 2
            AddBonusType(34, "Toa Power Pool", 2);

            //Toa Artifact 5
            AddBonusType(35, "Toa Overcap", 5, 6);

            //Arrow Recovery 0
            AddBonusType(36, "Arrow Recovery", 0);
            bonus_types[36].category = "catacombs";

            //Spell Power Cost Reduction (PvE)
            AddBonusType(37, "Spell Power Cost Reduction (PvE)",2);

            //Concentration 38 0
            AddBonusType(38, "Concentration", 0);

            //Safe Fall 39 0
            AddBonusType(39, "Safe Fall", 0);

            //Health Regeneration 40 -
            AddBonusType(40, "Health Regeneration", 0);

            //Mana Regeneration 41 0
            AddBonusType(41, "Mana Regeneration", 0);

            //Piece Ablative (PvE) 42 0
            AddBonusType(42, "Piece Ablative (PvE)",0);

            //Death Experience Loss Reduction 44 0
            AddBonusType(44, "Death Experience Loss Reduction", 0);

            //Negative Effect Duration Reduction (PvE) 46 0
            AddBonusType(46, "Negative Effect Duration Reduction (PvE)", 0);

            //Style Cost Reduction (PvE) 47 0
            AddBonusType(47, "Style Cost Reduction",0);

            //To Hit Bonus (PvE) 48 0
            AddBonusType(48, "To Hit Bonus (PvE)", 0);

            //Defensive Bonus (PvE) 49 0
            AddBonusType(49, "Defensive Bonus (PvE)", 0);

            //Bladeturn Reinforcement 50 0
            AddBonusType(50, "Bladeturn Reinforcement", 0);

            //Parry (PvE) 51 0
            AddBonusType(51, "Parry (PvE)", 0);

            //Block (PvE) 52 0
            AddBonusType(52, "Block (PvE)", 0);

            //Evade (PvE) 53 0
            AddBonusType(53, "Evade (PvE)", 0);

            //Reactionary Style Damage Bonus (PvE) 54 0
            AddBonusType(54, "Reactionary Style Damage Bonus (PvE)", 0);

            //55 Mythical Encumbrance 0
            AddBonusType(55, "Mythical Encumbrance", 0);

            //57 Mythical Resistance Cap 0
            AddBonusType(57, "Mythical Resistance Cap", 0);

            //58 Mythical Siege Speed 0
            AddBonusType(58, "Mythical Siege Speed", 0);

            //60 Mythical Parry 0
            AddBonusType(60, "Mythical Parry", 0);

            //61 Mythical Evade 0
            AddBonusType(61, "Mythical Evade", 0);

            //62 Mythical Block 0
            AddBonusType(62, "Mythical Block", 0);

            //63 Mythical Coin 0
            AddBonusType(63, "Mythical Coin", 0);

            //64 Mythical Cap Increase 2 1
            AddBonusType(64, "Mythical Cap Increase", 2, 1);

            //66 Mythical Crowd Control Duration Decrease 0
            AddBonusType(66, "Mythical Crowd Control Duration Decrease", 0);

            //67 Mythical Essence Resist 0
            AddBonusType(67, "Mythical Crowd Control Duration Decrease", 0);

            //68 Mythical Resist and Cap 4 3
            AddBonusType(68, "Mythical Resist and Cap", 4, 3);

            //69 
            AddBonusType(69, "Mythical Siege Damage Ablative", 0);
            
            AddBonusType(71, "Mythical DPS", 0);

            AddBonusType(72, "Mythical Realm Points", 0);

            AddBonusType(73, "Mythical Spell Focus", 0);

            AddBonusType(74, "Mythical Resurrection Sickness Reduction", 0);

            AddBonusType(75, "Mythical Stat and Cap Increase", 4 , 1);

            AddBonusType(76, "Mythical Health Regen", 0);

            AddBonusType(77, "Mythical Power Regen", 0);

            AddBonusType(78, "Mythical Endurance Regen", 0);

            AddBonusType(80, "Mythical Physical Defense", 0);
        }
    }

    public class Bonus
    {
        public string? name { get; set; }
        public int? type { get; set; }
        public int? value { get; set; }
        public int id { get; set; } = -1;
        public int? requirement_id { get; set; }
        public bool has_sub_type => sub_types != null && sub_types.Count > 0;
        public List<SubType>? sub_types { get; set; }
        public double? utility { get; set; }
        public int? realm { get; set; }
        public string? category { get; set; }
    }
}
