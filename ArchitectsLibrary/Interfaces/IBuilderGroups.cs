namespace ArchitectsLibrary.Interfaces
{
    using System.Collections.Generic;
    using UnityEngine;
    
    /// <summary>
    /// Implement this interface to your <see cref="BuilderTool"/> sub-class to create your own Builder Menu.
    /// </summary>
    public interface IBuilderGroups
    {
        /// <summary>
        /// The Allowed <see cref="TechGroup"/>s that your Custom Builder tool will show up in its menu.<br/>
        /// if this is <see langword="null"/>, your builder menu will use the vanilla's allowed TechGroups.
        /// </summary>
        List<TechGroup> AllowedTechGroups { get; }
        
        /// <summary>
        /// The Background Image of your Custom builder Menu.<br/>
        /// assign this to <see langword="null"/> if you want your Background Image to be the default one. 
        /// </summary>
        Sprite BackgroundImage { get; }
    }
}