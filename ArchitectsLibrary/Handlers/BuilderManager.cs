namespace ArchitectsLibrary.Handlers
{
    using System.Collections.Generic;
    using Interfaces;
    using UnityEngine;
    using UnityEngine.UI;
    
    internal static class BuilderManager
    {
        // vanilla builder TechGroups
        static readonly List<TechGroup> _initialBuilderGroups = new();
        
        // vanilla builder background image
        static Sprite _initialSprite;

        static bool _initialized;

        internal static void Initialize()
        {
            if (_initialized)
                return;
            
            _initialBuilderGroups.AddRange(uGUI_BuilderMenu.groups);
            _initialSprite = Object.Instantiate(uGUI_BuilderMenu.GetInstance().content.GetComponentInChildren<Image>().sprite);
            _initialized = true;
        }

        internal static void EnsureCorrectGroups(IBuilderGroups builderGroups = null)
        {
            if (builderGroups?.AllowedTechGroups is null)
            {
                if (uGUI_BuilderMenu.groups == _initialBuilderGroups)
                    return;

                ResetSettings();
                ApplyTechGroups(_initialBuilderGroups);
                ApplyImage(_initialSprite);
                return;
            }
            
            if (uGUI_BuilderMenu.groups == builderGroups.AllowedTechGroups) // no need to re-apply the changes if they're already applied
                return;
            
            ResetSettings();
            ApplyTechGroups(builderGroups.AllowedTechGroups);
            ApplyImage(builderGroups.BackgroundImage);
        }

        private static void ResetSettings()
        {
            uGUI_BuilderMenu.groups.Clear();
            uGUI_BuilderMenu.techTypeToTechGroupIdx.Clear();
            uGUI_BuilderMenu.groupsTechTypesInitialized = false;
            uGUI_BuilderMenu.singleton.selected = 0;
        }

        private static void ApplyTechGroups(List<TechGroup> groups)
        {
            var builderMenu = uGUI_BuilderMenu.singleton;
            if (!builderMenu)
                return;
            
            uGUI_BuilderMenu.groups.AddRange(groups);
            
            uGUI_BuilderMenu.EnsureTechGroupTechTypeDataInitialized();
            
            var sprites = new Atlas.Sprite[groups.Count];

            for (int i = 0; i < groups.Count; i++)
            {
                var text = builderMenu.techGroupNames.Get(groups[i]);
                sprites[i] = SpriteManager.Get(SpriteManager.Group.Tab, "group" + text);
            }
            var toolbar = builderMenu.toolbar;
            toolbar.Initialize(builderMenu, sprites);
            builderMenu.CacheToolbarTooltips();
            builderMenu.UpdateItems();
        }

        private static void ApplyImage(Sprite sprite)
        {
            var builderMenu = uGUI_BuilderMenu.singleton;
            if (!builderMenu)
                return;

            if (!sprite)
                return;
                    
            builderMenu.content.GetComponentInChildren<Image>().sprite = sprite;
        }
    }
}