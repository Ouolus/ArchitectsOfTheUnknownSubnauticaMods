namespace ArchitectsLibrary.API
{
    /// <summary>
    /// Common methods to interact with the Precursor Fabricator.
    /// </summary>
    public static class PrecursorFabricatorService
    {
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

        internal static string TabToNameID(PrecursorFabricatorTab tab)
        {
            switch (tab)
            {
                default:
                    return null;
                case PrecursorFabricatorTab.Materials:
                    return "AlienMaterials";
                case PrecursorFabricatorTab.Equipment:
                    return "AlienEquipment";
                case PrecursorFabricatorTab.Devices:
                    return "AlienDevices";
                case PrecursorFabricatorTab.UpgradeModules:
                    return "AlienUpgrades";
                case PrecursorFabricatorTab.Decorations:
                    return "AlienDecorations";
            }
        }
    }

    /// <summary>
    /// Data related to adding items to the precursor fabricator.
    /// </summary>
    public struct PrecursorFabricatorEntry
    {
        /// <summary>
        /// The TechType of the item to add to the fabricator.
        /// </summary>
        public TechType techType;
        /// <summary>
        /// The tab that the item will go to.
        /// </summary>
        public PrecursorFabricatorTab tab;

        /// <summary>
        /// Constructor for this struct.
        /// </summary>
        /// <param name="techType">The TechType of the item to add to the fabricator.</param>
        /// <param name="tab">The tab that the item will go to.</param>
        public PrecursorFabricatorEntry(TechType techType, PrecursorFabricatorTab tab)
        {
            this.techType = techType;
            this.tab = tab;
        }
    }

    /// <summary>
    /// Defines all Tabs used in the precursor fabricator, so you can easily add new items.
    /// </summary>
    public enum PrecursorFabricatorTab
    {
        /// <summary>
        /// This item will not appear in the fabricator.
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
        Decorations
    }
}