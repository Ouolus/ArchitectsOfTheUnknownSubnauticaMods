using System;
using System.Collections.Generic;
using UnityEngine;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.API;
using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    class DisplayCaseDecoration : MonoBehaviour
    {
        public Transform[] spawnPositions;
        public DisplayCaseType displayCaseType;

        // add whatever other item thats allowed to be displayed
        List<TechType> _allowedTechTypes = new()
        {
            TechType.Quartz,
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
            TechType.HatchingEnzymes,
            TechType.SeaTreaderPoop,
            TechType.AcidMushroom,
            TechType.StarshipSouvenir,
            TechType.ArcadeGorgetoy,
            TechType.ToyCar,
            AUHandler.EmeraldTechType,
            AUHandler.SapphireTechType,
            AUHandler.RedBerylTechType,
            AUHandler.MorganiteTechType,
            AUHandler.ElectricubeTechType,
            AUHandler.RedIonCubeTechType,
            AUHandler.PrecursorAlloyIngotTechType,
            AUHandler.CobaltTechType,
            AUHandler.CobaltIngotTechType,
            AUHandler.AlienCompositeGlassTechType,
            AUHandler.ReinforcedGlassTechType
        };

        GameObject[] _spawnedObjs;

        StorageContainer _storageContainer;

        bool _initialized;

        void Awake()
        {
            _storageContainer = GetComponent<StorageContainer>();
            _spawnedObjs = new GameObject[spawnPositions.Length];
        }
		
		void Start()
		{
			foreach (var item in _storageContainer.container)
				Spawn(item.item.gameObject);
		}

        void OnEnable()
        {
            if (!_initialized)
            {
                _storageContainer.enabled = true;
                _storageContainer.container.onAddItem += AddItem;
                _storageContainer.container.onRemoveItem += RemoveItem;
                _storageContainer.container.isAllowedToAdd = IsAllowedToAdd;
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
            if (!_allowedTechTypes.Contains(tt) && !DisplayCaseServices.WhitelistedTechTypes.Contains(tt)) 
                return false;
            
            if (DisplayCaseServices.BlacklistedTechTypes.Contains(tt)) 
                return false;
            
            return _storageContainer.container.count < spawnPositions.Length;
        }

        void AddItem(InventoryItem item) => Spawn(item.item.gameObject);

        void RemoveItem(InventoryItem item) => DeSpawn(item);

        void Spawn(GameObject obj)
        {
            var spawnPosition = GetEmptySpawnPosition(out int spawnedObjIndex);
            if (spawnPosition == null)
            {
                return;
            }
            _spawnedObjs[spawnedObjIndex] = Instantiate(obj, spawnPosition, false);
            var spawnedObj = _spawnedObjs[spawnedObjIndex];
            Destroy(spawnedObj.GetComponent<Rigidbody>());
            Destroy(spawnedObj.GetComponent<WorldForces>());
            if (displayCaseType == DisplayCaseType.RelicTank) spawnedObj.EnsureComponent<SpinInRelicCase>();
            TechType techType = obj.GetComponent<Pickupable>().GetTechType();
            spawnedObj.transform.localScale = Vector3.one * DisplayCaseServices.GetScaleForItem(techType, displayCaseType);
            spawnedObj.transform.localPosition = DisplayCaseServices.GetOffsetForItem(techType);
            spawnedObj.transform.localEulerAngles = Vector3.zero;
            spawnedObj.SetActive(true);
        }

        void DeSpawn(InventoryItem item)
        {
            if (_spawnedObjs == null)
                return;

            var index = GetSpawnedObjIndex(item.item.GetComponent<Pickupable>().GetTechType());
            if (index > -1)
            {
                Destroy(_spawnedObjs[index]);
            }
        }

        Transform GetEmptySpawnPosition(out int index)
        {
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                if (spawnPositions[i].childCount != 0) 
                    continue;
                
                index = i;
                return spawnPositions[i];
            }
            index = -1;
            return null;
        }

        int GetSpawnedObjIndex(TechType techType)
        {
            for (int i = 0; i < _spawnedObjs.Length; i++)
            {
                if (_spawnedObjs[i] == null)
                {
                    continue; // safety check
                }
                
                var tt = CraftData.GetTechType(_spawnedObjs[i]);
                if (tt == techType)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    enum DisplayCaseType
    {
        RelicTank,
        Pedestal,
        SpecimenCase
    }
}
