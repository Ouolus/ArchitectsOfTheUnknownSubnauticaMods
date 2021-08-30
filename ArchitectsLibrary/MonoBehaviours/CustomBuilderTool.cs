// TODO: delete whenever the Alien Builder is ready.
namespace ArchitectsLibrary.MonoBehaviours
{
    using System.Collections.Generic;
    using Interfaces;
    using UnityEngine;
    
    // in-complete BuilderTool sub-class, merely serves as a test for the Custom Builder API
    internal class CustomBuilderTool : BuilderTool, IBuilderGroups
    {
        public override string animToolName => TechType.Builder.AsString(true);

        List<TechGroup> IBuilderGroups.AllowedTechGroups => new() {Main.DecorationGroup};

        Sprite IBuilderGroups.BackgroundImage => Main.assetBundle.LoadAsset<Sprite>("BuilderMenuBackground");
    }
}