using System.IO;
using UnityEngine;

namespace ArchitectsLibrary.CreatorKit.Packs
{
    internal static class PackHelper
    {
        public const string packJsonFile = "pack.json";
        public const string packIconFile = "icon.png";

        public static PackData ReadPackData(string packName)
        {
            string jsonPath = GetPackJsonPath(packName);
            if (!File.Exists(jsonPath))
            {
                Utility.Utils.PrintErrorMessage(string.Format("Pack {0} is missing a pack.json file.", packName));
                return new PackData() { invalid = true };
            }
            string rawText;
            using (StreamReader sr = new StreamReader(jsonPath))
            {
                rawText = sr.ReadToEnd();
            }
            if (string.IsNullOrEmpty(rawText))
            {
                Utility.Utils.PrintErrorMessage(string.Format("Failed to load any text from Pack {0}.", packName));
                return new PackData() { invalid = true };
            }
            PackJson loadedData = JsonUtility.FromJson<PackJson>(rawText);
            return new PackData(loadedData, packName);
        }

        public static string GetPackPath(string packName)
        {
            return Path.Combine(Utility.PackFolderUtils.GetPackFolderPath(), packName);
        }

        public static string GetPackJsonPath(string packName)
        {
            return Path.Combine(GetPackPath(packName), packJsonFile);
        }

        public static string GetPackIconPath(string packName)
        {
            return Path.Combine(GetPackPath(packName), packIconFile);
        }

        public static Sprite LoadPackSprite(string packName)
        {
            return Utility.Utils.LoadSprite(GetPackIconPath(packName), UI.UIAssets.GetDefaultPackImage());
        }

        public static bool IsPackValid(string packName)
        {
            return File.Exists(GetPackJsonPath(packName));
        }
    }
}