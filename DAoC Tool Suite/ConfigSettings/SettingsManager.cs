using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigSettings
{
    public class SettingsManager
    {
        public static ChimpTool.ChimpTool ChimpTool = new();
        public static CharacterTool.CharacterTool CharacterTool = new();

        public SettingsManager() { }
    }
}
