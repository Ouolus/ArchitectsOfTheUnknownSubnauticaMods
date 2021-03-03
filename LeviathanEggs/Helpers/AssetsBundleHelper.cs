using UnityEngine;

namespace LeviathanEggs.Helpers
{
    public class AssetsBundleHelper
    {
        public static Sprite LoadSprite(string fileName) => Main.assetBundle.LoadAsset<Sprite>(fileName);
        public static GameObject LoadGameObject(string fileName) => Main.assetBundle.LoadAsset<GameObject>(fileName);
        public static Texture2D LoadTexture2D(string fileName) => Main.assetBundle.LoadAsset<Texture2D>(fileName);
    }
}
