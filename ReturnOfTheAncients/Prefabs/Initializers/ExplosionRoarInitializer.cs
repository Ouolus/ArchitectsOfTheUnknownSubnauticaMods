using System.Collections.Generic;
using ECCLibrary;
using RotA.Mono;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.Initializers
{
    class ExplosionRoarInitializer : Spawnable
    {
        public ExplosionRoarInitializer()
            : base("KAJWGHDKJAGWKDGAIWUEYOAW", ".", ".")
        {
        }

        public override List<SpawnLocation> CoordinatedSpawns => new()
        {
            new SpawnLocation(new Vector3(1775f, 0f, 536f))
        };

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Global,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Creature,
            techType = this.TechType
        };

        public override GameObject GetGameObject()
        {
            GameObject obj = new GameObject();
            obj.EnsureComponent<ExplosionRoar>();
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.SetActive(true);
            return obj;
        }

    }
}