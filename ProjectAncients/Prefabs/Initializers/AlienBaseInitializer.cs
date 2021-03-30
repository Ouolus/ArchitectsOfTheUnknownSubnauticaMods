using SMLHelper.V2.Assets;
using ECCLibrary;
using ProjectAncients.Mono;
using UnityEngine;
using UWE;
using ProjectAncients.Mono.AlienBaseSpawners;
using System;

namespace ProjectAncients.Prefabs
{
    class AlienBaseInitializer<T> : Spawnable where T : AlienBaseSpawner
    {

        private LargeWorldEntity.CellLevel cellLevel;

        public AlienBaseInitializer(string classId, Vector3 coords, float distanceToLoad = 200f, LargeWorldEntity.CellLevel cellLevel = LargeWorldEntity.CellLevel.Medium)
            : base(classId, ".", ".")
        {
            this.cellLevel = cellLevel;
            OnFinishedPatching = () =>
            {
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(this.TechType, coords,
                    classId, distanceToLoad));
            };
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = cellLevel,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Creature,
            techType = this.TechType
        };

        public override GameObject GetGameObject()
        {
            GameObject obj = new GameObject(ClassID);
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = cellLevel;
            obj.EnsureComponent<T>();
            obj.SetActive(true);
            return obj;
        }

    }
}