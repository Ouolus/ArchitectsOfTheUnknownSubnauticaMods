using UnityEngine;
using System.Collections;

namespace RotA.Mono.AlienBaseSpawners
{
    public class VoidBaseSpawner : AlienBaseSpawner
    {
        private const float platformY = 0f;

        public override IEnumerator ConstructBase()
        {
            TaskResult<GameObject> baseModel = new TaskResult<GameObject>();
            yield return SpawnPrefab(Mod.voidBaseModel.ClassID, Vector3.zero, baseModel);
            yield return GenerateAtmospheres(baseModel.Get(), "AtmosphereRoot", Mod.precursorAtmosphereVolume.ClassID);

            //Exterior platform
            yield return SpawnPrefab(Mod.voidDoor_red.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(Mod.voidDoor_orange.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(Mod.voidDoor_white.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(Mod.voidDoor_blue.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(Mod.voidDoor_purple.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(vfx_entrance, new Vector3(-0.5f, 3f+ platformY, 62f), new Vector3(90, 0, 0), new Vector3(1.30f, 1.5f, 1.30f));
            //yield return SpawnPrefab(light_big_ceiling_animated, new Vector3(0f, 0f, 72f), Vector3.right * 180f, Vector3.one);
            yield return SpawnPrefab(light_volumetric_1, new Vector3(0f, 11, 62.5f), Vector3.zero, new Vector3(1f, 0.5f, 1f));
            yield return SpawnPrefab(light_volumetric_1, new Vector3(0f, 21, 62.5f), Vector3.zero, new Vector3(1f, 2f, 1f));
            yield return SpawnPrefab(light_volumetric_1, new Vector3(0f, 32, 62.5f), Vector3.zero, new Vector3(1f, 0.5f, 1f));

            //Exterior cables
            const float cableDrop = 24f;
            //Left side
            /*yield return GenerateCable(new Vector3(388, -393, -1865 + Mod.voidBaseZOffset), new Vector3(0.7f, 0f, 0.7f), new Vector3(396, -395, -1762), new Vector3(-0.3f, -0.1f, 0.9f), Vector3.down, cableDrop, 2f);
            yield return GenerateCable(new Vector3(383, -412, -1870 + Mod.voidBaseZOffset), new Vector3(0.7f, 0f, 0.7f), new Vector3(383, -416, -1763), new Vector3(0, 0, 1), Vector3.down, cableDrop, 2f);
            yield return GenerateCable(new Vector3(381, -424, -1872 + Mod.voidBaseZOffset), new Vector3(0.7f, 0f, 0.7f), new Vector3(370.5f, -426, -1769.7f), new Vector3(0.4f, -0.1f, 0.9f), Vector3.down, cableDrop, 2f);
            yield return GenerateCable(new Vector3(379, -436, -1874 + Mod.voidBaseZOffset), new Vector3(0.7f, 0f, 0.7f), new Vector3(398, -451, -1769.9f), new Vector3(-0.1f, 0, 1), Vector3.down, cableDrop, 2f);
            //Right side
            yield return GenerateCable(new Vector3(358, -393, -1865 + Mod.voidBaseZOffset), new Vector3(-0.7f, 0f, 0.7f), new Vector3(346.5f, -393, -1765), new Vector3(0f, 0f, 1f), Vector3.down, cableDrop, 2f);
            yield return GenerateCable(new Vector3(363, -412, -1870 + Mod.voidBaseZOffset), new Vector3(-0.7f, 0f, 0.7f), new Vector3(344f, -418, -1763), new Vector3(0, 0, 1), Vector3.down, cableDrop, 2f);
            yield return GenerateCable(new Vector3(365, -424, -1872 + Mod.voidBaseZOffset), new Vector3(-0.7f, 0f, 0.7f), new Vector3(338, -427, -1770), new Vector3(0f, 0f, 1f), Vector3.down, cableDrop, 2f);
            yield return GenerateCable(new Vector3(367, -436, -1874 + Mod.voidBaseZOffset), new Vector3(-0.7f, 0f, 0.7f), new Vector3(340f, -450f, -1771f), new Vector3(0.2f, -0.2f, 0.9f), Vector3.down, cableDrop, 2f);
            */
        }
    }
}
