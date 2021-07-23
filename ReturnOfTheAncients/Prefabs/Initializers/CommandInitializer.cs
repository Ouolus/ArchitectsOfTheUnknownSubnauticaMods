using System.Collections.Generic;
using ECCLibrary;
using RotA.Mono.Commands;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.Initializers
{
    public class CommandInitializer : Spawnable
    {
        public CommandInitializer()
    : base("RotACommandInitializer", ".", ".")
        {
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Global,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Creature,
            techType = TechType
        };

        public override List<SpawnLocation> CoordinatedSpawns => new()
        {
            new SpawnLocation(new Vector3(0f, 0f, 0f))
        };

        public override GameObject GetGameObject()
        {
            GameObject obj = new GameObject();
            obj.EnsureComponent<RotACommands>();
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.SetActive(true);
            return obj;
        }
    }
}
