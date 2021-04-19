using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace CreatorKit.UI
{
    /// <summary>
    /// This static class contains a static reference to the AssetBundle and has some methods for obtaining commonly used UI Sprites.
    /// </summary>
    internal static class UIAssets
    {
        public static AssetBundle assetBundle;
        public const string assetBundleName = "creationkitassets";

        public static void LoadAssetBundle()
        {
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", assetBundleName));
        }

        public static Sprite GetDefaultPackImage()
        {
            return assetBundle.LoadAsset<Sprite>("PackDefault");
        }
    }
}
