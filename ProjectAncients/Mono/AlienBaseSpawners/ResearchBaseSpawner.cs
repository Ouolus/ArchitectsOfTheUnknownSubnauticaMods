using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class ResearchBaseSpawner : SecondaryBaseSpawner
    {
        protected override string TerminalClassId => Mod.guardianTerminal.ClassID;

        protected override string TabletClassId => supplies_redTablet;

        public override IEnumerator ConstructBase()
        {
            yield return base.ConstructBase();
            SpawnPrefab(Mod.door_researchBase.ClassID, new Vector3(centerLocalX, floorLocalY, 24f));
            SpawnPrefab(Mod.researchBaseTerminal.ClassID, new Vector3(centerLocalX + 22f, floorLocalY, 0f), new Vector3(0f, 90f, 0f));
            SpawnPrefab(Mod.archElectricityTerminal.ClassID, new Vector3(centerLocalX - 22f, floorLocalY, 0f), new Vector3(0f, -90f, 0f));

            SpawnPrefabGlobally(bone_curly, new Vector3(-891.40f, -197.51f, -597.30f), Vector3.up, false);
            SpawnPrefabGlobally(bone_generic1, new Vector3(-861.62f, -193.75f, -575.27f), new Vector3(6f, 0f, 9f), Vector3.one);
            SpawnPrefabGlobally(bone_generic2, new Vector3(-836.00f, -190.14f, -569.54f), new Vector3(340.99f, 357.34f, 15.78f), Vector3.one);
            SpawnPrefabGlobally(bone_reaperSkull, new Vector3(-815.32f, -192.80f, -598.73f), new Vector3(310.89f, 240.00f, 0f), Vector3.one);
            SpawnPrefabGlobally(bone_reaperFullRibcage_chipped, new Vector3(-805.79f, -183.85f, -576.63f), new Vector3(335.11f, 358.04f, 8.85f), Vector3.one);
            SpawnPrefabGlobally(bone_reaperMandible, new Vector3(-815.81f, -196.04f, -610.00f), new Vector3(342.22f, 85.71f, 337.03f), Vector3.one);
            SpawnPrefabGlobally(bone_reaperMandible, new Vector3(-809.74f, -190f, -587.91f), new Vector3(351.43f, 68.57f, 0f), Vector3.one);
            SpawnPrefabGlobally(bone_reaperFullRibcage_normal, new Vector3(-874.98f, -193.72f, -591.03f), new Vector3(4.39f, 0.69f, 17.87f), Vector3.one);
            SpawnPrefabGlobally(Mod.guardianTailfinModel.ClassID, new Vector3(-847.15f, -193.53f, -593.49f), new Vector3(353.23f, 0f, 358.68f), Vector3.one);
            SpawnPrefabsArray(creature_alienRobot, 2f, new Vector3(4, 1, 4), Vector3.one, new Vector3(0f, 5f, 0f));
            SpawnPrefabsArray(creature_alienRobot, 3f, new Vector3(4, 1, 4), Vector3.one, new Vector3(0f, 6f, 44.93f));
        }
    }
}
