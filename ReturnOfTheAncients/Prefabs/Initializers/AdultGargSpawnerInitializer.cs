using SMLHelper.V2.Assets;
using ECCLibrary;
using RotA.Mono;
using UnityEngine;
using UWE;

namespace RotA.Prefabs
{
    //Initializer for the adult garg, but ALSO the spinefish spawning
    class AdultGargSpawnerInitializer : Spawnable
    {
        public AdultGargSpawnerInitializer()
            : base("AdultGargSpawner", ".", ".")
        {
            OnFinishedPatching = () =>
            {
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(TechType, new Vector3(0f, 0f, 0f),
                    "AdultGargSpawner", 20000f));
            };
        }

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
            GameObject obj = new GameObject("AdultGargSpawner");
            obj.EnsureComponent<VoidGargSpawner>();
            obj.EnsureComponent<VoidShoalSpawner>();
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.SetActive(true);
            return obj;
        }
    }
}
