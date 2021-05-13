using UnityEngine;
using ArchitectsLibrary.API;

namespace ProjectAncients.Prefabs
{
    public class GargPoster : HolographicPoster
    {
        public GargPoster() : base("GargantuanPoster", "Leviathan Holographic Projector", "An approximation of the appearance of an ancient leviathan, projected with holographic technology.")
        {
        }

        public override bool UnlockedAtStart => false;
        public override TechType RequiredForUnlock => TechType.None;

        public override PosterDimensions GetPosterDimensions()
        {
            return PosterDimensions.Landscape;
        }

        public override Texture2D GetPosterTexture()
        {
            return Mod.assetBundle.LoadAsset<Texture2D>("GargPoster");
        }
    }
}
