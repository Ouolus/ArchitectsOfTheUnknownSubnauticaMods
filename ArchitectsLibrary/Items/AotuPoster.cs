using UnityEngine;
using ArchitectsLibrary.API;

namespace ArchitectsLibrary.Items
{
    class AotuPoster : HolographicPoster
    {
        public AotuPoster() : base("AotuPoster", "Architects of the Unknown Holographic Projector", "A team logo, projected with holographic technology.")
        {
        }

        public override PosterDimensions GetPosterDimensions()
        {
            return PosterDimensions.Portait;
        }

        public override Texture2D GetPosterTexture()
        {
            return Main.assetBundle.LoadAsset<Texture2D>("AotuPoster");
        }
    }
}
