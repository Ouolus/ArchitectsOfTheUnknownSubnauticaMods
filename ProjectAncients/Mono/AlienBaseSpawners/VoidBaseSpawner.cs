using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class VoidBaseSpawner : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            TaskResult<GameObject> baseModel = new TaskResult<GameObject>();
            yield return SpawnPrefab(Mod.voidBaseModel.ClassID, Vector3.zero, baseModel);
            GenerateAtmospheres(baseModel.Get(), "AtmosphereRoot", atmosphereVolume_antechamber);

            //Exterior platform
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_red.ClassID, new Vector3(0f, 0f, 24f)));
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_orange.ClassID, new Vector3(0f, 0f, 24f)));
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_white.ClassID, new Vector3(0f, 0f, 24f)));
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_blue.ClassID, new Vector3(0f, 0f, 24f)));
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_purple.ClassID, new Vector3(0f, 0f, 24f)));
            yield return StartCoroutine(SpawnPrefab(vfx_entrance, new Vector3(-0.5f, 3f, 24f), new Vector3(90, 0, 0), new Vector3(1.30f, 1.5f, 1f)));
            yield return StartCoroutine(SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 7), Vector3.one, new Vector3(-8.7f + 0.5f, 0.5f, 35)));
            yield return StartCoroutine(SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 7), Vector3.one, new Vector3(8.7f + 1.5f, 0.5f, 35)));

            //Exterior cables
            const float cableDrop = 12f;
            //Left side
            GenerateCable(new Vector3(388, -393, -1865 + Mod.voidBaseZOffset), new Vector3(0.7f, 0f, 0.7f), new Vector3(396, -395, -1762), new Vector3(-0.3f, -0.1f, 0.9f), Vector3.right, cableDrop, 2f);
            GenerateCable(new Vector3(383, -412, -1870 + Mod.voidBaseZOffset), new Vector3(0.7f, 0f, 0.7f), new Vector3(383, -416, -1763), new Vector3(0, 0, 1), Vector3.down, cableDrop, 2f);
            GenerateCable(new Vector3(381, -424, -1872 + Mod.voidBaseZOffset), new Vector3(0.7f, 0f, 0.7f), new Vector3(370.5f, -426, -1769.7f), new Vector3(0.4f, -0.1f, 0.9f), Vector3.down, cableDrop, 2f);
            GenerateCable(new Vector3(379, -436, -1874 + Mod.voidBaseZOffset), new Vector3(0.7f, 0f, 0.7f), new Vector3(398, -451, -1769.9f), new Vector3(-0.1f, 0, 1), Vector3.down, cableDrop, 2f);
            //Right side
            GenerateCable(new Vector3(358, -393, -1865 + Mod.voidBaseZOffset), new Vector3(-0.7f, 0f, 0.7f), new Vector3(346.5f, -393, -1765), new Vector3(0f, 0f, 1f), Vector3.right, cableDrop, 2f);
            GenerateCable(new Vector3(363, -412, -1870 + Mod.voidBaseZOffset), new Vector3(-0.7f, 0f, 0.7f), new Vector3(344f, -418, -1763), new Vector3(0, 0, 1), Vector3.down, cableDrop, 2f);
            GenerateCable(new Vector3(365, -424, -1872 + Mod.voidBaseZOffset), new Vector3(-0.7f, 0f, 0.7f), new Vector3(338, -427, -1770), new Vector3(0f, 0f, 1f), Vector3.down, cableDrop, 2f);
            GenerateCable(new Vector3(367, -436, -1874 + Mod.voidBaseZOffset), new Vector3(-0.7f, 0f, 0.7f), new Vector3(340f, -450f, -1771f), new Vector3(0.2f, -0.2f, 0.9f), Vector3.down, cableDrop, 2f);

            yield return null;
        }
    }
}
