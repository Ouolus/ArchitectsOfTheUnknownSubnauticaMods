using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class SupplyCacheBaseSpawner : SecondaryBaseSpawner
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
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 7f, floorLocalY, -6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 7f, floorLocalY, 6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 7f, floorLocalY, -6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 7f, floorLocalY, 6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 7f, foundationUnderLocalY - 8f, -21f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 7f, foundationUnderLocalY - 8f, -21f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableLithium, new Vector3(centerLocalX + 6f, floorLocalY, -8f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableLithium, new Vector3(centerLocalX - 5f, floorLocalY, 13f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableLithium, new Vector3(centerLocalX - 2.5f, floorLocalY, -7f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableIonCube, new Vector3(centerLocalX - 9f, floorLocalY, 11f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(centerLocalX + 3f, foundationUnderLocalY, -18f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_2, new Vector3(centerLocalX + 6f, foundationUnderLocalY, -21f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(centerLocalX + -4f, foundationUnderLocalY, -19)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_3, new Vector3(centerLocalX + -5f, foundationUnderLocalY, -22)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(centerLocalX + -2f, foundationUnderLocalY, -19.5f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX + 6f, floorLocalY, 15f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX + 6f, floorLocalY, 17f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX + 6f, floorLocalY, 19f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX - 6f, floorLocalY, 15f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX - 6f, floorLocalY, 17f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX - 6f, floorLocalY, 19f), Mod.ingotRelic.ClassID, new Vector3(0f, 1.25f, 0f)));
        }

        protected override string TerminalClassId => Mod.supplyCacheTerminal.ClassID;
    }
}
