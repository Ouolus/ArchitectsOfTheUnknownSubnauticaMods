using System.Collections.Generic;
using ECCLibrary;
using RotA.Mono;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.Initializers
{
    class MiscInitializers : Spawnable
    {
        public MiscInitializers()
            : base("MiscRotAInitializers", ".", ".")
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
            GameObject obj = new GameObject("MiscRotAInitializers");
            obj.EnsureComponent<MiscPDALines>();
            obj.EnsureComponent<VoidShoalSpawner>();
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.SetActive(true);
            return obj;
        }
    }
}
