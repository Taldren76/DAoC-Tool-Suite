﻿using DAoCToolSuite.ChimpTool.Json;
using SQLLibrary;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace DAoCToolSuite.ChimpTool.Extensions
{
    public static class SQLExtenions
    {

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                _ = dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                object[] values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i]?.GetValue(item, null) ?? "";
                }
                _ = dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static List<ChimpJson> ToChimpJsonList(this List<CharacterModel> characterModelList)
        {
            return characterModelList?.Select(x => x.CovertToChimp())?.ToList() ?? new();
        }

        public static CharacterModel ConvertToCharacterModel(this ChimpJson chimp)
        {
            if (chimp is null)
            {
                return new();
            }

            try
            {
                CharacterModel characterModel = new()
                {

                    WebID = chimp?.WebID,
                    Name = chimp?.Name,
                    Realm = chimp?.Realm,
                    Class = chimp?.Class,
                    Server = chimp?.Server
                };

                int totalRealmPoints = int.TryParse(chimp?.TotalRealmPoints, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out totalRealmPoints) ? totalRealmPoints : 0;
                characterModel.TotalRealmPoints = totalRealmPoints;

                int totalSoloKills = int.TryParse(chimp?.TotalSoloKills, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out totalSoloKills) ? totalSoloKills : 0;
                characterModel.TotalSoloKills = totalSoloKills;

                int totalDeathBlows = int.TryParse(chimp?.TotalDeathBlows, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out totalDeathBlows) ? totalDeathBlows : 0;
                characterModel.TotalDeathBlows = totalDeathBlows;

                int totalKills = int.TryParse(chimp?.TotalKills, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out totalKills) ? totalKills : 0;
                characterModel.TotalKills = totalKills;

                int totalDeaths = int.TryParse(chimp?.TotalDeaths, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out totalDeaths) ? totalDeaths : 0;
                characterModel.TotalDeaths = totalDeaths;

                characterModel.Level = chimp?.Level;
                characterModel.Race = chimp?.Race;

                int boundyPoints = int.TryParse(chimp?.BountyPoints, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out boundyPoints) ? boundyPoints : 0;
                characterModel.BountyPoints = boundyPoints;

                if (chimp?.Guild_WebID is not null)
                {
                    characterModel.Guild_WebID = chimp.Guild_WebID;
                }

                if (chimp?.MasterLevel_Level is not null)
                {
                    int masterLevelLevel = int.TryParse(chimp?.MasterLevel_Level, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out masterLevelLevel) ? masterLevelLevel : 0;
                    characterModel.MasterLevel_Level = masterLevelLevel;
                }

                if (chimp?.MasterLevel_Name is not null)
                {
                    characterModel.MasterLevel_Name = chimp?.MasterLevel_Name;
                }

                int alchemy = int.TryParse(chimp?.Alchemy, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out alchemy) ? alchemy : 0;
                characterModel.Alchemy = alchemy;
                //Armorcraft
                int armorcraft = int.TryParse(chimp?.Armorcraft, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out armorcraft) ? armorcraft : 0;
                characterModel.Armorcraft = armorcraft;
                //Fletching
                int fletching = int.TryParse(chimp?.Fletching, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out fletching) ? fletching : 0;
                characterModel.Fletching = fletching;
                //Siegecraft
                int siegecraft = int.TryParse(chimp?.Siegecraft, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out siegecraft) ? siegecraft : 0;
                characterModel.Siegecraft = siegecraft;
                //Spellcraft
                int spellcraft = int.TryParse(chimp?.Spellcrafting, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out spellcraft) ? spellcraft : 0;
                characterModel.Spellcrafting = spellcraft;
                //Tailoring
                int tailoring = int.TryParse(chimp?.Tailoring, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out tailoring) ? tailoring : 0;
                characterModel.Tailoring = tailoring;
                //Weaponcraft
                int weaponcraft = int.TryParse(chimp?.Weaponcraft, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out weaponcraft) ? weaponcraft : 0;
                characterModel.Weaponcraft = weaponcraft;


                int albion_soloKills = int.TryParse(chimp?.Albion_SoloKills, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out albion_soloKills) ? albion_soloKills : 0;
                characterModel.Albion_SoloKills = albion_soloKills;
                int albion_deathBlows = int.TryParse(chimp?.Albion_DeathBlows, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out albion_deathBlows) ? albion_deathBlows : 0;
                characterModel.Albion_DeathBlows = albion_deathBlows;
                int albion_Kills = int.TryParse(chimp?.Albion_Kills, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out albion_Kills) ? albion_Kills : 0;
                characterModel.Albion_Kills = albion_Kills;
                int albion_Deaths = int.TryParse(chimp?.Albion_Deaths, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out albion_Deaths) ? albion_Deaths : 0;
                characterModel.Albion_Deaths = albion_Deaths;

                int hibernia_soloKills = int.TryParse(chimp?.Hibernia_SoloKills, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out hibernia_soloKills) ? hibernia_soloKills : 0;
                characterModel.Hibernia_SoloKills = hibernia_soloKills;
                int hibernia_deathBlows = int.TryParse(chimp?.Hibernia_DeathBlows, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out hibernia_deathBlows) ? hibernia_deathBlows : 0;
                characterModel.Hibernia_DeathBlows = hibernia_deathBlows;
                int hibernia_Kills = int.TryParse(chimp?.Hibernia_Kills, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out hibernia_Kills) ? hibernia_Kills : 0;
                characterModel.Hibernia_Kills = hibernia_Kills;
                int hibernia_Deaths = int.TryParse(chimp?.Hibernia_Deaths, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out hibernia_Deaths) ? hibernia_Deaths : 0;
                characterModel.Hibernia_Deaths = hibernia_Deaths;

                int midgard_soloKills = int.TryParse(chimp?.Midgard_SoloKills, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out midgard_soloKills) ? midgard_soloKills : 0;
                characterModel.Midgard_SoloKills = midgard_soloKills;
                int midgard_deathBlows = int.TryParse(chimp?.Midgard_DeathBlows, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out midgard_deathBlows) ? midgard_deathBlows : 0;
                characterModel.Midgard_DeathBlows = midgard_deathBlows;
                int midgard_Kills = int.TryParse(chimp?.Midgard_Kills, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out midgard_Kills) ? midgard_Kills : 0;
                characterModel.Midgard_Kills = midgard_Kills;
                int midgard_Deaths = int.TryParse(chimp?.Midgard_Deaths, NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out midgard_Deaths) ? midgard_Deaths : 0;
                characterModel.Midgard_Deaths = midgard_Deaths;

                return characterModel;
            }
            catch (System.Exception)
            {
                return new CharacterModel();
            }


        }

        public static GuildModel ConvertToGuildModel(this ChimpJson chimp)
        {
            if (string.IsNullOrEmpty(chimp.GuildName) || string.IsNullOrEmpty(chimp.Guild_WebID))
            {
                return new GuildModel();
            }

            GuildModel guild = new()
            {
                Name = chimp.GuildName,
                WebID = chimp.Guild_WebID
            };
            return guild;
        }

        public static ChimpJson CovertToChimp(this CharacterModel characterModel)
        {
            ChimpJson chimp = new()
            {
                Name = characterModel.Name,
                WebID = characterModel.WebID,
                Level = characterModel.Level,
                Race = characterModel.Race,
                Realm = characterModel.Realm,
                Class = characterModel.Class,
                Server = characterModel.Server,
                TotalRealmPoints = characterModel.TotalRealmPoints?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                TotalSoloKills = characterModel.TotalSoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                TotalDeathBlows = characterModel.TotalDeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                TotalKills = characterModel.TotalKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                TotalDeaths = characterModel.TotalDeaths?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                BountyPoints = characterModel.BountyPoints?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                Alchemy = characterModel.Alchemy?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                Armorcraft = characterModel.Armorcraft?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                Fletching = characterModel.Fletching?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                Siegecraft = characterModel.Siegecraft?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                Spellcrafting = characterModel.Spellcrafting?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                Tailoring = characterModel.Tailoring?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                Weaponcraft = characterModel.Weaponcraft?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0",
                Guild_WebID = characterModel.Guild_WebID
            };
            if (chimp.Guild_WebID != null)
            {
                chimp.GuildName = SqliteDataAccess.LoadGuilds().Where(x => x.WebID == chimp.Guild_WebID).Select(x => x.Name).FirstOrDefault();
            }
            chimp.IRS = characterModel.IRS.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
            chimp.RPNextRank = characterModel.RPNextRank.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);
            chimp.RealmRank = characterModel.RealmRank;
            chimp.RPLastUpdate = characterModel.RPLastUpdate.ToString("N0", System.Globalization.CultureInfo.CurrentCulture);

            chimp.Albion_SoloKills = characterModel.Albion_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Albion_DeathBlows = characterModel.Albion_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Albion_Kills = characterModel.Albion_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Albion_Deaths = characterModel.Albion_Deaths?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Hibernia_SoloKills = characterModel.Hibernia_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Hibernia_DeathBlows = characterModel.Hibernia_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Hibernia_Kills = characterModel.Hibernia_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Hibernia_DeathBlows = characterModel.Hibernia_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Midgard_SoloKills = characterModel.Midgard_SoloKills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Midgard_DeathBlows = characterModel.Midgard_DeathBlows?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Midgard_Kills = characterModel.Midgard_Kills?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.Midgard_Deaths = characterModel.Midgard_Deaths?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";


            chimp.MasterLevel_Level = characterModel.MasterLevel_Level?.ToString("N0", System.Globalization.CultureInfo.CurrentCulture) ?? "0";
            chimp.MasterLevel_Name = characterModel.MasterLevel_Name?.ToString() ?? "";

            return chimp;
        }

    }

}
