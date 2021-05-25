using UnityEngine;
using ArchitectsLibrary.API;

namespace ArchitectsLibrary.Items
{
    class AotuPoster : HolographicPoster
    {
        public AotuPoster() : base("AotuPoster", "Architects of the Unknown holographic projector", "A team logo, projected with holographic technology.")
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
