using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class SupplyCacheBaseSpawner : SecondaryBaseSpawner
    {
        public override void ConstructBase()
        {
            base.ConstructBase();
            SpawnPrefab(Mod.door_supplyCache.ClassID, new Vector3(centerLocalX, floorLocalY, 24f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, 0f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, -6f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX - 14f, floorLocalY, 6f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, 0f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, -6f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX + 14f, floorLocalY, 6f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX - 7f, floorLocalY, 0f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX - 7f, floorLocalY, -6f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX - 7f, floorLocalY, 6f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX + 7f, floorLocalY, 0f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX + 7f, floorLocalY, -6f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX + 7f, floorLocalY, 6f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX - 7f, foundationUnderLocalY - 8f, -21f));
            SpawnPrefab(structure_column, new Vector3(centerLocalX + 7f, foundationUnderLocalY - 8f, -21f));
            SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(centerLocalX + 3f, foundationUnderLocalY, -18f));
            SpawnPrefab(natural_lr_hangingplant1_2, new Vector3(centerLocalX + 6f, foundationUnderLocalY, -21f));
            SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(centerLocalX + -4f, foundationUnderLocalY, -19));
            SpawnPrefab(natural_lr_hangingplant1_3, new Vector3(centerLocalX + -5f, foundationUnderLocalY, -22));
            SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(centerLocalX + -2f, foundationUnderLocalY, -19.5f));
        }

        protected override string TerminalClassId => Mod.supplyCacheTerminal.ClassID;
    }
}
