﻿using ArchitectsLibrary.Handlers;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class KooshBaseSpawner : CacheBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return StartCoroutine(base.ConstructBase());
            yield return StartCoroutine(SpawnPrefab(Mod.door_kooshBase.ClassID, new Vector3(centerLocalX, floorLocalY, 24f)));
            Vector3 columnScale = Vector3.one * 1.7f;
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, 0f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, -6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, 6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, 0f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, -6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, 6f), Vector3.zero, columnScale));
            yield return StartCoroutine(SpawnPrefab(CraftData.GetClassIdForTechType(AUHandler.PrecursorAlloyIngotTechType), new Vector3(centerLocalX, floorLocalY + 0.1f, 20f), new Vector3(0f, 240f, 0f)));
            //yield return StartCoroutine(SpawnPrefab(Mod.precursorMasterTechTerminal.ClassID, new Vector3(centerLocalX, floorLocalY, -3f), new Vector3(0f, 0f, 0f)));
        }

        protected override string MainTerminalClassId => Mod.kooshBaseTerminal.ClassID;
    }
}
