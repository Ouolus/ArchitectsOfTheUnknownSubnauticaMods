using SMLHelper.V2.Assets;
using ECCLibrary;
using UWE;
using UnityEngine;
using RotA.Mono;

namespace RotA.Prefabs.Initializers
{
    class MiscInitializers : Spawnable
    {
        public MiscInitializers()
            : base("MiscRotAInitializers", ".", ".")
        {
            OnFinishedPatching = () =>
            {
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(TechType, new Vector3(0f, 0f, 0f),
                    "MiscRotAInitializers", 20000f));
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
            GameObject obj = new GameObject("MiscRotAInitializers");
            obj.EnsureComponent<MiscPDALines>();
            obj.EnsureComponent<VoidShoalSpawner>();
            obj.EnsureComponent<GargGrayscaleCameraEffects>();
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.SetActive(true);
            return obj;
        }
    }
}
