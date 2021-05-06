using System.Linq;
using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using ArchitectsLibrary.Utility;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace ArchitectsLibrary.Handlers
{
    public static class AUHandler
    {
        internal static IDictionary<TechType, TechType> customCreatureEggDictionary = 
            new HashDictionary<TechType, TechType>();

        internal static IDictionary<TechType, WaterParkCreatureParameters> customWaterParkCreatureParameters =
            new HashDictionary<TechType, WaterParkCreatureParameters>();
        
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
            var acidToList = DamageSystem.acidImmune.ToList();
            
            if (!acidToList.Contains(techType))
                acidToList.Add(techType);

            DamageSystem.acidImmune = acidToList.ToArray();
        }

        /// <summary>
        /// Set an Egg <see cref="TechType"/> to a Creature's <see cref="TechType"/> so the creature breeds the passed Egg
        ///  instead of breeding Creatures to the ACU.
        /// </summary>
        /// <param name="creatureType">the Creature's <see cref="TechType"/> to specify an egg for</param>
        /// <param name="eggType">the Egg's <see cref="TechType"/></param>
        public static void SetCreatureEgg(TechType creatureType, TechType eggType)
        {
            customCreatureEggDictionary[creatureType] = eggType;
        }
        
        /// <summary>
        /// Sets a <see cref="WaterParkCreatureParameters"/> for the passed <see cref="TechType"/> Creature.
        /// </summary>
        /// <param name="creatureType">The Creature's <see cref="TechType"/>.</param>
        /// <param name="waterParkCreatureParameters">the <see cref="WaterParkCreatureParameters"/> to set.</param>
        public static void SetCreatureParameters(TechType creatureType, WaterParkCreatureParameters waterParkCreatureParameters)
        {
            customWaterParkCreatureParameters[creatureType] = waterParkCreatureParameters;
        }
    }
}