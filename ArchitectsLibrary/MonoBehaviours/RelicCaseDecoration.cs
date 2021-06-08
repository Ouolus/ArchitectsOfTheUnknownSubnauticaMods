using System;
using System.Collections.Generic;
using UnityEngine;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary.MonoBehaviours
{
    class RelicCaseDecoration : MonoBehaviour
    {
        public Transform spawnPosition;

        // add whatever other item thats allowed to be displayed
        List<TechType> _allowedTechTypes = new()
        {
            TechType.Quartz,
            TechType.ScrapMetal,
            TechType.FiberMesh,
            TechType.Copper,
            TechType.Lead,
            TechType.Salt,
            TechType.MercuryOre,
            TechType.Glass,
            TechType.Titanium,
            TechType.Silicone,
            TechType.Gold,
            TechType.Magnesium,
            TechType.Sulphur,
            TechType.Bleach,
            TechType.Silver,
            TechType.TitaniumIngot,
            TechType.CopperWire,
            TechType.WiringKit,
            TechType.AdvancedWiringKit,
            TechType.CrashPowder,
            TechType.Diamond,
            TechType.Lithium,
            TechType.PlasteelIngot,
            TechType.EnameledGlass,
            TechType.PowerCell,
            TechType.ComputerChip,
            TechType.Fiber,
            TechType.Uranium,
            TechType.AluminumOxide,
            TechType.HydrochloricAcid,
            TechType.Magnetite,
            TechType.Polyaniline,
            TechType.AramidFibers,
            TechType.Aerogel,
            TechType.Benzene,
            TechType.Lubricant,
            TechType.UraniniteCrystal,
            TechType.ReactorRod,
            TechType.DepletedReactorRod,
            TechType.PrecursorIonCrystal,
            TechType.PrecursorIonCrystalMatrix,
            TechType.Kyanite,
            TechType.Nickel,
            TechType.DiveSuit,
            TechType.Fins,
            TechType.Tank,
            TechType.Battery,
            TechType.Knife,
            TechType.Flashlight,
            TechType.Beacon,
            TechType.Builder,
            TechType.Compass,
            TechType.AirBladder,
            TechType.Terraformer,
            TechType.Pipe,
            TechType.Thermometer,
            TechType.DiveReel,
            TechType.Rebreather,
            TechType.RadiationSuit,
            TechType.RadiationHelmet,
            TechType.RadiationGloves,
            TechType.ReinforcedDiveSuit,
            TechType.Scanner,
            TechType.FireExtinguisher,
            TechType.MapRoomHUDChip,
            TechType.Welder,
            TechType.StasisRifle,
            TechType.PropulsionCannon,
            TechType.LaserCutter,
            TechType.HeatBlade,
            TechType.UltraGlideFins,
            TechType.SwimChargeFins,
            TechType.RepulsionCannon,
            TechType.Stillsuit,
            TechType.CrashEgg,
            TechType.CutefishEgg,
            TechType.LavaLizardEgg,
            TechType.Snack1,
            TechType.Snack2,
            TechType.Snack3,
            TechType.Coffee,
            AUHandler.EmeraldTechType,
            AUHandler.SapphireTechType,
            AUHandler.RedBerylTechType,
            AUHandler.MorganiteTechType,
            AUHandler.ElectricubeTechType,
            AUHandler.RedIonCubeTechType,
            AUHandler.PrecursorAlloyIngotTechType
        };

        GameObject _spawnedObj;

        StorageContainer _storageContainer;
        
        bool _initialized;

        void Awake() => _storageContainer = GetComponent<StorageContainer>();

        void OnEnable()
        {
            if (!_initialized)
            {
                _storageContainer.enabled = true;
                _storageContainer.container.onAddItem += AddItem;
                _storageContainer.container.onRemoveItem += RemoveItem;
                _storageContainer.container.isAllowedToAdd += IsAllowedToAdd;
                _initialized = true;
            }
        }

        void OnDisable()
        {
            if (_initialized)
            {
                _storageContainer.container.onAddItem -= AddItem;
                _storageContainer.container.onRemoveItem -= RemoveItem;
                _storageContainer.container.isAllowedToAdd = null;
                _storageContainer.enabled = false;
                _initialized = false;
            }
        }

        bool IsAllowedToAdd(Pickupable pickupable, bool verbose)
        {
            var tt = pickupable.GetTechType();
            if (!_allowedTechTypes.Contains(tt)) return false;
            return _storageContainer.container.count < 1;
        }
        void AddItem(InventoryItem item) => Spawn(item.item.gameObject);

        void RemoveItem(InventoryItem item) => DeSpawn();

        void Spawn(GameObject obj)
        {
            _spawnedObj = Instantiate(obj, spawnPosition, false);
            Destroy(_spawnedObj.GetComponent<Rigidbody>());
            Destroy(_spawnedObj.GetComponent<WorldForces>());
            _spawnedObj.SetActive(true);
        }

        void DeSpawn()
        {
            if (_spawnedObj == null)
                return;
            
            Destroy(_spawnedObj);
        }
    }
}
