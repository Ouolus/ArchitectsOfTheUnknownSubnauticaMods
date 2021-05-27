using SMLHelper.V2.Assets;
using ECCLibrary;
using RotA.Mono;
using UnityEngine;
using UWE;

namespace RotA.Prefabs
{
    class ExplosionRoarInitializer : Spawnable
    {
        public ExplosionRoarInitializer()
            : base("KAJWGHDKJAGWKDGAIWUEYOAW", ".", ".")
        {
            OnFinishedPatching = () =>
            {
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(this.TechType, new Vector3(1775f, 0f, 536f),
                    "GargantuanRoarAfterExplosion", 20000f));
            };
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID, cellLevel = LargeWorldEntity.CellLevel.Global, localScale = Vector3.one,
            slotType = EntitySlot.Type.Creature, techType = this.TechType
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