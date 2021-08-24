using UnityEngine;
using ArchitectsLibrary.API;

namespace ArchitectsLibrary.Items.AdvancedMaterials
{
    class AotuPoster : HolographicPoster
    {
        public AotuPoster() : base("AotuPoster", LanguageSystem.Get("AotuPoster"), LanguageSystem.GetTooltip("AotuPoster"))
        {
        }

        public override Texture2D GetPosterTexture => Main.assetBundle.LoadAsset<Texture2D>("AotuPoster");

        public override PosterDimensions GetPosterDimensions => PosterDimensions.Portait;

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("AotUPoster_Icon"));
        }
    }
}
