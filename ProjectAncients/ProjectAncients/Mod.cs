using QModManager.API.ModLoading;
using ECCLibrary;
using UnityEngine;
using System.Reflection;
using ProjectAncients.Prefabs;
using SMLHelper.V2.Handlers;

namespace ProjectAncients
{
    [QModCore]
    public class Mod
    {
        public static AssetBundle assetBundle;

        public static GargantuanPrefab gargantuanPrefab;

        private const string assetBundleName = "projectancientsassets";

        [QModPatch]
        public static void Patch()
        {
            assetBundle = ECCHelpers.LoadAssetBundleFromAssetsFolder(Assembly.GetExecutingAssembly(), assetBundleName);
            ECCAudio.RegisterClips(assetBundle);

            LanguageHandler.SetLanguageLine("EncyPath_Lifeforms/Fauna/Titans", "Titans");

            gargantuanPrefab = new GargantuanPrefab("gargantuanleviathan", "Gargantuan leviathan", "A titan-class lifeform. How did it get in your inventory?", assetBundle.LoadAsset<GameObject>("Garg_Prefab"), null);
            gargantuanPrefab.Patch();
        }
    }
}
