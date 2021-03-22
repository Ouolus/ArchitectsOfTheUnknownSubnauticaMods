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
        }
    }
}
