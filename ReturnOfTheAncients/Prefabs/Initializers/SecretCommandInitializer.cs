using SMLHelper.V2.Assets;
using ECCLibrary;
using RotA.Mono.Commands;
using UnityEngine;
using UWE;

namespace RotA.Prefabs
{
    public class SecretCommandInitializer : Spawnable
    {
        public SecretCommandInitializer()
    : base("GargSecretCommandInitializer", ".", ".")
        {
            OnFinishedPatching = () =>
            {
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(this.TechType, new Vector3(0f, 0f, 0f),
                    "GargSecretCommand", 20000f));
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
            GameObject obj = new GameObject();
            obj.EnsureComponent<SecretCommand>();
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.SetActive(true);
            return obj;
        }
    }
}
