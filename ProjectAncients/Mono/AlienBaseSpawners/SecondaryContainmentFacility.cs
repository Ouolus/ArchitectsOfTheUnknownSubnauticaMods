using System;
using System.Collections;
using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class SecondaryContainmentFacility : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            // negative X & positive z = bad
            yield return SpawnWreckage(damageprop_box, 0.3f, 0.6f, 0.2f);
            yield return SpawnWreckage(damageprop_box_quadruple, 0.5f, -0.5f, -0.2f);
            yield return SpawnWreckage(damageprop_box_quadruple2, 0.9f, 0.9f, 0.1f);
            yield return SpawnWreckage(damageprop_box_quadruple2, 0f, 0f, 0.5f, 6f);
            yield return SpawnWreckage(damageprop_box_double, 0.4f, 0.6f, 0.45f);
            yield return SpawnWreckage(damageprop_box_quadruple, 0.3f, -0.1f, 0.3f);
            yield return SpawnWreckage(damageprop_box, -0.2f, -0.05f, 0.8f);
            yield return SpawnWreckage(damageprop_box_double, 0.9f, -0.9f, 0.94f);
            yield return SpawnWreckage(damageprop_box_quadruple2, 0.4f, -0.1f, 0.7f);
            yield return SpawnWreckage(damageprop_box, 0.7f, -0.6f, 0.7f);
            yield return SpawnWreckage(damageprop_box, -0.8f, -0.6f, -0.3f);
            yield return SpawnWreckage(damageprop_box_quadruple, 0.2f, 0.05f, -0.2f);
            yield return SpawnWreckage(damageprop_box, 0.2f, -1f, 0.1f);
            yield return SpawnWreckage(damageprop_box_quadruple2, -0.2f, -0.6f, 0.53f);
            yield return SpawnWreckage(damageprop_box_quadruple2, 0.03f, -0.06f, 0.37f);
            yield return SpawnWreckage(damageprop_box, 0.2f, 0.7f, 0.18f);
            yield return SpawnWreckage(damageprop_box_quadruple, -0.2f, -0.2f, -0.9f);
            yield return SpawnWreckage(damageprop_box, -0.3f, -0.6f, 0.43f);
            yield return SpawnPrefab(Mod.spookySkeletonGargPrefab.ClassID, new Vector3(0f, 75f, 0f));
        }

        IEnumerator SpawnWreckage(string classId, float xOffset, float zOffset, float yRotation = 0f, float scale = 3f)
        {
            yield return SpawnPrefab(classId, new Vector3(xOffset * 150f, 0f, zOffset * 150f) + new Vector3(0f, 2f * scale, 0f), new Vector3(0f, yRotation * 360f, 0), Vector3.one * scale);
        }
    }
}
