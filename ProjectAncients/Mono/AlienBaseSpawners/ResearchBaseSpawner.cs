using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class ResearchBaseSpawner : SecondaryBaseSpawner
    {
        protected override string TerminalClassId => Mod.guardianTerminal.ClassID;

        protected override string TabletClassId => supplies_redTablet;

        public override void ConstructBase()
        {
            base.ConstructBase();
            SpawnPrefab(Mod.door_researchBase.ClassID, new Vector3(centerLocalX, floorLocalY, 24f));
            SpawnPrefab(Mod.researchBaseTerminal.ClassID, new Vector3(centerLocalX + 22f, floorLocalY, 0f), new Vector3(0f, 90f, 0f));
            SpawnPrefab(Mod.archElectricityTerminal.ClassID, new Vector3(centerLocalX - 22f, floorLocalY, 0f), new Vector3(0f, -90f, 0f));

            SpawnPrefabGlobally(bone_curly, new Vector3(-891.40f, -197.51f, -597.30f), Vector3.up, false);
            SpawnPrefabGlobally(bone_generic1, new Vector3(-861.62f, -193.75f, -575.27f), new Vector3(6f, 0f, 9f), Vector3.one);
            SpawnPrefabGlobally(bone_generic2, new Vector3(-836.00f, -190.14f, -569.54f), new Vector3(340.99f, 357.34f, 15.78f), Vector3.one);
            SpawnPrefabGlobally(bone_reaperSkull, new Vector3(-815.32f, -192.80f, -598.73f), new Vector3(349.11f, 358.48f, 15.82f), Vector3.one);
            SpawnPrefabGlobally(bone_reaperFullRibcage_chipped, new Vector3(-805.79f, -183.85f, -576.63f), new Vector3(335.11f, 358.04f, 8.85f), Vector3.one);
        }
    }
}
