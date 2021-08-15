using ArchitectsLibrary.Handlers;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class SupplyCacheBaseSpawner : CacheBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return StartCoroutine(base.ConstructBase());
            yield return StartCoroutine(SpawnPrefab(Mod.door_supplyCache.ClassID, new Vector3(centerLocalX, floorLocalY, 24f)));
            Vector3 columnScale = Vector3.one * 1.7f;
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, 0f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, -6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, 6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, 0f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, -6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, 6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableLithium, new Vector3(centerLocalX + 6f, floorLocalY, -8f)));
            yield return StartCoroutine(SpawnPrefab(CraftData.GetClassIdForTechType(AUHandler.DrillableSapphireTechType), new Vector3(centerLocalX - 5f, floorLocalY, 13f)));
            yield return StartCoroutine(SpawnPrefab(CraftData.GetClassIdForTechType(AUHandler.DrillableEmeraldTechType), new Vector3(centerLocalX + 5f, floorLocalY, 13f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableLithium, new Vector3(centerLocalX - 2.5f, floorLocalY, -7f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableIonCube, new Vector3(centerLocalX - 9f, floorLocalY, 11f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX + 6f, floorLocalY, 15f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX + 6f, floorLocalY, 17f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX + 6f, floorLocalY, 19f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX - 6f, floorLocalY, 15f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX - 6f, floorLocalY, 17f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX - 6f, floorLocalY, 19f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnPrefab(CraftData.GetClassIdForTechType(AUHandler.PrecursorAlloyIngotTechType), new Vector3(centerLocalX, floorLocalY + 0.1f, 20f), new Vector3(0f, 37f, 0f)));
            yield return StartCoroutine(SpawnPrefab(Mod.precursorMasterTechTerminal.ClassID, new Vector3(centerLocalX, floorLocalY, -3f), new Vector3(0f, 0f, 0f)));

        }

        protected override string MainTerminalClassId => Mod.supplyCacheTerminal.ClassID;
    }
}
