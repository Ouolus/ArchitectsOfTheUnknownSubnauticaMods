using System.Linq;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;
using ArchitectsLibrary.Items;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace ArchitectsLibrary.Handlers
{
    /// <summary>
    /// a General Handler that mostly used by this Library's APIs and the Mod Team.
    /// </summary>
    public static class AUHandler
    {
        internal static readonly IDictionary<TechType, TechType> CustomCreatureEggDictionary = 
            new Dictionary<TechType, TechType>();

        internal static readonly IDictionary<TechType, WaterParkCreatureParameters> CustomWaterParkCreatureParameters =
            new Dictionary<TechType, WaterParkCreatureParameters>();

        /// <summary>
        /// Gets the <see cref="PrecursorAlloyIngot"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType PrecursorAlloyIngotTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Emerald"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType EmeraldTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="DrillableEmerald"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType DrillableEmeraldTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Sapphire"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType SapphireTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="DrillableSapphire"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType DrillableSapphireTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="RedBeryl"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType RedBerylTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="DrillableRedBeryl"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType DrillableRedBerylTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="RedIonCube"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType RedIonCubeTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Morganite"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType MorganiteTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="DrillableMorganite"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType DrillableMorganiteTechType { get; internal set; }
        
        /// <summary>
        /// Gets the <see cref="OmegaCube"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType OmegaCubeTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Electricube"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType ElectricubeTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Cobalt"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType CobaltTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="DrillableCobalt"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType DrillableCobaltTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="CobaltIngot"/>'s TechType so you can spawn it up in your Mod.
        /// </summary>
        public static TechType CobaltIngotTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="ReinforcedGlass"/>'s TechType so you can spawn it up in your Mod. Crafted with 2 sapphire.
        /// </summary>
        public static TechType ReinforcedGlassTechType { get; internal set; }

        /// <summary>
        /// Gets the <see cref="AlienCompositeGlass"/>'s TechType so you can spawn it up in your Mod. Crafted with 1 reinforced glass and 1 emerald.
        /// </summary>
        public static TechType AlienCompositeGlassTechType { get; internal set; }

        /// <summary>
        /// Gets the TechType that is used to unlock all basic alien technology. Return of the Ancients unlocks this in the Supply Cache base.
        /// </summary>
        public static TechType AlienTechnologyMasterTech { get; internal set; }

        /// <summary>
        /// makes the object given Scannable from the Scanner Room.
        /// </summary>
        /// <param name="gameObject">the <see cref="GameObject"/> to make Scannable</param>
        /// <param name="categoryTechType">Category in the Scanner Room</param>
        public static void SetObjectScannable(GameObject gameObject, TechType categoryTechType = TechType.GenericEgg)
        {
            var tt = CraftData.GetTechType(gameObject);

            if (tt != TechType.None)
            {
                ResourceTracker resourceTracker = gameObject.EnsureComponent<ResourceTracker>();
                resourceTracker.techType = tt;
                resourceTracker.overrideTechType = categoryTechType;
                resourceTracker.rb = gameObject.GetComponent<Rigidbody>();
                resourceTracker.prefabIdentifier = gameObject.GetComponent<PrefabIdentifier>();
                resourceTracker.pickupable = gameObject.GetComponent<Pickupable>();
            }
            else
                Logger.Log(Logger.Level.Warn, "TechType to get from SetObjectScannable() is null");
        }

        /// <summary>
        /// Makes a <see cref="TechType"/> immune to the AcidicBrine of the LostRiver.
        /// </summary>
        /// <param name="techType">the <see cref="TechType"/> to make immune</param>
        public static void MakeItemAcidImmune(TechType techType)
        {
            DamageSystem.acidImmune.Add(techType);
        }

        /// <summary>
        /// Set an Egg <see cref="TechType"/> to a Creature's <see cref="TechType"/> so the creature breeds the passed Egg
        ///  instead of breeding Creatures to the ACU.
        /// </summary>
        /// <param name="creatureType">the Creature's <see cref="TechType"/> to specify an egg for</param>
        /// <param name="eggType">the Egg's <see cref="TechType"/></param>
        public static void SetCreatureEgg(TechType creatureType, TechType eggType)
        {
            CustomCreatureEggDictionary[creatureType] = eggType;
        }
        
        /// <summary>
        /// Sets a <see cref="WaterParkCreatureParameters"/> for the passed <see cref="TechType"/> Creature.
        /// </summary>
        /// <param name="creatureType">The Creature's <see cref="TechType"/>.</param>
        /// <param name="waterParkCreatureParameters">the <see cref="WaterParkCreatureParameters"/> to set.</param>
        public static void SetCreatureParameters(TechType creatureType, WaterParkCreatureParameters waterParkCreatureParameters)
        {
            CustomWaterParkCreatureParameters[creatureType] = waterParkCreatureParameters;
        }
    }
}