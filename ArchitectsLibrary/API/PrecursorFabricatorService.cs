using System.Collections.Generic;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// Common methods to interact with the Precursor Fabricator.
    /// </summary>
    public static class PrecursorFabricatorService
    {
        internal static Dictionary<TechType, float> itemEnergyUsage = new();
        
        /// <summary>
        /// Adds a Crafting Node to the Precursor Fabricator.
        /// </summary>
        /// <param name="entry">Defines the <see cref="TechType"/> to add and the <see cref="PrecursorFabricatorTab"/></param>
        public static void SubscribeToFabricator(PrecursorFabricatorEntry entry)
        {
            bool isPatched = Main.PrecursorFabricator?.IsPatched ?? false;
            
            if (isPatched)
            {
                Main.PrecursorFabricator.Root.GetTabNode(TabToNameID(entry.tab)).AddCraftingNode(entry.techType);
                return;
            }
            
            Main.PrecursorFabricatorEntriesToAdd.Add(entry);
            if (entry.overridePowerUsage.HasValue)
            {
                itemEnergyUsage.Add(entry.techType, entry.overridePowerUsage.Value);
            }
        }

        /// <summary>
        /// Adds a Crafting Node to the Precursor Fabricator. This overload requires slightly less writing than the other, but does the same thing.
        /// </summary>
        /// <param name="techType">The TechType of the item to add to the fabricator.</param>
        /// <param name="tab">The tab that the item will go to.</param>
        public static void SubscribeToFabricator(TechType techType, PrecursorFabricatorTab tab)
        {
            SubscribeToFabricator(new PrecursorFabricatorEntry(techType, tab));
        }
        
        /// <summary>
        /// Adds a Crafting Node to the Precursor Fabricator. This overload requires slightly less writing than the other, but does the same thing.
        /// </summary>
        /// <param name="techType">The TechType of the item to add to the fabricator.</param>
        /// <param name="tab">The tab that the item will go to.</param>
        /// <param name="overridePowerUsage">The amount of power the Precursor Fabricator requires when crafting this item.</param>
        public static void SubscribeToFabricator(TechType techType, PrecursorFabricatorTab tab, float overridePowerUsage)
        {
            SubscribeToFabricator(new PrecursorFabricatorEntry(techType, tab, overridePowerUsage));
        }

        internal static string TabToNameID(PrecursorFabricatorTab tab)
        {
            return tab switch
            {
                PrecursorFabricatorTab.Materials => "AlienMaterials",
                PrecursorFabricatorTab.Equipment => "AlienEquipment",
                PrecursorFabricatorTab.Devices => "AlienDevices",
                PrecursorFabricatorTab.UpgradeModules => "AlienUpgrades",
                PrecursorFabricatorTab.Decorations => "AlienDecorations",
                PrecursorFabricatorTab.Eggs => "AlienEggs",
                _ => null // Fallback should never happen
            };
        }
    }

    /// <summary>
    /// Data related to adding items to the Precursor Fabricator.
    /// </summary>
    public struct PrecursorFabricatorEntry
    {
        internal readonly TechType techType;
        
        internal readonly PrecursorFabricatorTab tab;

        internal readonly float? overridePowerUsage;

        internal readonly bool flickerLights;

        /// <summary>
        /// Constructor for this struct.
        /// </summary>
        /// <param name="techType">The TechType of the item to add to the fabricator.</param>
        /// <param name="tab">The tab that the item will go to.</param>
        public PrecursorFabricatorEntry(TechType techType, PrecursorFabricatorTab tab)
        {
            this.techType = techType;
            this.tab = tab;
            this.overridePowerUsage = null;
            this.flickerLights = false;
        }

        /// <summary>
        /// Constructor for this struct.
        /// </summary>
        /// <param name="techType">The TechType of the item to add to the fabricator.</param>
        /// <param name="tab">The tab that the item will go to.</param>
        /// <param name="overridePowerUsage">The amount of power the Precursor Fabricator requires when crafting this item.</param>
        public PrecursorFabricatorEntry(TechType techType, PrecursorFabricatorTab tab, float overridePowerUsage,
            bool flickerLights = false)

        {
            this.techType = techType;
            this.tab = tab;
            this.overridePowerUsage = overridePowerUsage;
            this.flickerLights = false;
        }
    }

    /// <summary>
    /// Defines all Tabs used in the precursor fabricator, so you can easily add new items.
    /// </summary>
    public enum PrecursorFabricatorTab
    {
        /// <summary>
        /// Same as <see langword="null" />, This item will not appear in the fabricator.
        /// </summary>
        None,
        
        /// <summary>
        /// "Alien Materials" tab
        /// </summary>
        Materials,
        
        /// <summary>
        /// "Equipment" tab
        /// </summary>
        Equipment,
        
        /// <summary>
        /// "Devices" tab
        /// </summary>
        Devices,
        
        /// <summary>
        /// "Advanced Upgrade Modules" tab
        /// </summary>
        UpgradeModules,
        
        /// <summary>
        /// "Decorations" tab
        /// </summary>
        Decorations,

        /// <summary>
        /// "Eggs" tab
        /// </summary>
        Eggs
    }
}