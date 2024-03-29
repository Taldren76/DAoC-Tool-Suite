﻿using Newtonsoft.Json;
using SQLLibrary;
using System.Text.Json.Serialization;

namespace DAoCToolSuite.CharacterTool.Json
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(SettingsBackupJson))]
    public class SettingsBackupJson
    {
        public int Count => Backups?.Count ?? 0;

        [JsonProperty]
        public List<SettingsBackUpModel>? Backups { get; set; }
    }
}
