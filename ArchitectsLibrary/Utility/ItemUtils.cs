using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace ArchitectsLibrary.Utility
{
    /// <summary>
    /// A Utility class for several generic methods related to editing data related to TechTypes. Includes scannable data.
    /// </summary>
    public static class ItemUtils
    {
        /// <summary>
        /// Makes objects with a TechType of <paramref name="techType"/> scannable. <paramref name="encyKey"/> should be also used for <see cref="PatchEncy(string, string, string, string, Sprite, Texture2D)"/>
        /// </summary>
        /// <param name="techType">The TechType that will be made scannable. ANY object with a TechTag matching this <see cref="TechType"/> will be scannable.</param>
        /// <param name="encyKey">The UNIQUE language line key for this encyclopedia entry.</param>
        /// <param name="scanTime">How long it takes to scan the object.</param>
        public static void MakeObjectScannable(TechType techType, string encyKey, float scanTime)
        {
            PDAHandler.AddCustomScannerEntry(new PDAScanner.EntryData()
            {
                key = techType,
                encyclopedia = encyKey,
                scanTime = scanTime,
                isFragment = false
            });
        }

        /// <summary>
        /// Registers a databank entry into the game.
        /// </summary>
        /// <param name="key">The unique key to be used for this databank entry.</param>
        /// <param name="path">The path to this databank entry. An example would be 'PlanetaryGeology' or 'Lifeforms/Fauna/Leviathans'.</param>
        /// <param name="title">The displayed title in the databank entry.</param>
        /// <param name="desc">The displayed descritpion in the databank entry.</param>
        /// <param name="popupSprite">The 256x128 popup that shows on-screen for a second when the entry is unlocked.</param>
        /// <param name="databankImage">The image seen in the databank.</param>
        public static void PatchEncy(string key, string path, string title, string desc, Sprite popupSprite = null, Texture2D databankImage = null)
        {
            PDAEncyclopediaHandler.AddCustomEntry(new PDAEncyclopedia.EntryData()
            {
                key = key,
                path = path,
                nodes = path.Split('/'),
                popup = popupSprite,
                image = databankImage
            });
            LanguageHandler.SetLanguageLine("Ency_" + key, title);
            LanguageHandler.SetLanguageLine("EncyDesc_" + key, desc);
        }
    }
}
