using System.Collections.Generic;
using ECCLibrary;
using RotA.Mono;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.Initializers
{
    //Initializer for the adult garg, but ALSO the spinefish spawning
    class AdultGargSpawnerInitializer : Spawnable
    {
        public AdultGargSpawnerInitializer()
            : base("AdultGargSpawner", ".", ".")
        {
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Global,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Creature,
            techType = this.TechType
        };

        public override List<SpawnLocation> CoordinatedSpawns => new()
        {
            new SpawnLocation(new Vector3(0f, 0f, 0f))
        };

        public override GameObject GetGameObject()
        {
            GameObject obj = new GameObject("AdultGargSpawner");
            obj.EnsureComponent<VoidGargSpawner>();
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.SetActive(true);
            return obj;
        }
    }
}
