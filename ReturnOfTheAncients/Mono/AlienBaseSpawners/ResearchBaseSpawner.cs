using ArchitectsLibrary.Handlers;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class ResearchBaseSpawner : CacheBaseSpawner
    {
        protected override string MainTerminalClassId => Mod.guardianTerminal.ClassID;

        protected override string TabletClassId => null;

        public override IEnumerator ConstructBase()
        {
            //Inside
            yield return StartCoroutine(base.ConstructBase());
            yield return StartCoroutine(SpawnPrefab(Mod.door_researchBase.ClassID, new Vector3(centerLocalX, floorLocalY, 24f)));
            yield return StartCoroutine(SpawnPrefab(Mod.researchBaseTerminal.ClassID, new Vector3(centerLocalX + 22f, floorLocalY, 0f), new Vector3(0f, 90f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 22f, floorLocalY, 3f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 22f, floorLocalY, -3f)));

            yield return StartCoroutine(SpawnPrefab(Mod.archElectricityTerminal.ClassID, new Vector3(centerLocalX - 22f, floorLocalY, 0f), new Vector3(0f, -90f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 22f, floorLocalY, 3f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 22f, floorLocalY, -3f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(centerLocalX, floorLocalY + 1f, -4f)));

            yield return StartCoroutine(SpawnRelicInCase(new Vector3(centerLocalX, floorLocalY, 6f), Mod.rifleRelic.ClassID, new Vector3(0f, 1.25f, 0f))); //11.77, -13.88, 10.00

            yield return StartCoroutine(SpawnPrefab(Mod.blackHole.ClassID, new Vector3(centerLocalX, floorLocalY + 4.3f, 0f)));

            yield return StartCoroutine(SpawnPrefab(CraftData.GetClassIdForTechType(AUHandler.PrecursorAlloyIngotTechType), new Vector3(centerLocalX, floorLocalY + 0.1f, 20f), new Vector3(0f, 37f, 0f)));

            yield return StartCoroutine(SpawnPrefab(Mod.redTabletHolder.ClassID, new Vector3(centerLocalX, floorLocalY, 17 + 1f), Vector3.up * 180f));
            yield return StartCoroutine(SpawnPrefab(supplies_redTablet, new Vector3(centerLocalX, floorLocalY + 2.5f, 17.5f - 0.825f + 1f), new Vector3(90f, 0f, 0f), Vector3.one * 1.5f));

            //Outside
            yield return StartCoroutine(SpawnPrefabGlobally(Mod.ghostSkeletonPose2.ClassID, new Vector3(-866.40f, -191.00f, -586.30f), new Vector3(355.00f, 320.72f, 0f), Vector3.one));
            yield return StartCoroutine(SpawnPrefabGlobally(Mod.guardianTailfinModel.ClassID, new Vector3(-847.15f, -193.53f, -593.49f), new Vector3(353.23f, 0f, 358.68f), Vector3.one));
        }

        protected override float MainTerminalZOffset => 12f;
    }
}
