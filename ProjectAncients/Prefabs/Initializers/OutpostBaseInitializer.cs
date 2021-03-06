using SMLHelper.V2.Assets;
using ECCLibrary;
using ProjectAncients.Mono;
using UnityEngine;
using UWE;
using ProjectAncients.Mono.AlienBaseSpawners;

namespace ProjectAncients.Prefabs
{
    class OutpostBaseInitializer : Spawnable
    {
        public OutpostBaseInitializer()
            : base("PrecursorGargOutpost", ".", ".")
        {
            OnFinishedPatching = () =>
            {
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(this.TechType, new Vector3(0f, 0f, 0f),
                    "GargOutpost1", 200f));
            };
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Creature,
            techType = this.TechType
        };

        public override GameObject GetGameObject()
        {
            GameObject obj = new GameObject("GargOutpost");
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;
            obj.EnsureComponent<OutpostBaseSpawner>();
            obj.SetActive(true);
            return obj;
        }

    }
}