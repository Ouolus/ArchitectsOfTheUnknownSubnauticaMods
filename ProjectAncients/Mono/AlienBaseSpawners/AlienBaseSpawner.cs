using ECCLibrary;
using ECCLibrary.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public abstract class AlienBaseSpawner : MonoBehaviour
    {
        public const string box2x2x2 = "2f996953-bd0a-48e2-8aae-57dd6b6a2507";
        public const string box2x1x2 = "96edb813-c7c7-4c44-9bf4-5f1975edeff8";
        public const string box2x1x2_variant1 = "5ecd846d-1629-4d3c-9119-f4f16179a408";
        public const string box2x1x2_variant2 = "fa5e644a-777b-4b54-a92a-0241752b8e06";
        public const string box2x1x2_variant3 = "3c9344a2-4715-4773-9c58-dc0437002278";
        public const string light_small = "742b410c-14d4-42c6-ac84-0e2bcaff09c1";
        public const string light_big_animated = "6a02aa5c-8d4d-4801-aad4-ea61dccddae5";
        public const string starfish = "d571d3dc-6229-430e-a513-0dcafc2c41f3";
        public const string structure_outpost_1 = "c5512e00-9959-4f57-98ae-9a9962976eaa";
        public const string structure_outpost_2 = "$542aaa41-26df-4dba-b2bc-3fa3aa84b777";
        public const string pedestal_empty1 = "78009225-a9fa-4d21-9580-8719a3368373";
        public const string pedestal_empty2 = "3bbf8830-e34f-43a1-bbb3-743c7e6860ac";
        public const string pedestal_ionCrystal = "7e1e5d12-7169-4ff9-abcd-520f11196764";
        /// <summary>
        /// 2x8x2
        /// </summary>
        public const string structure_column = "640f57a6-6436-4132-a9bb-d914f3e19ef5";
        public const string structure_doorway = "db5a85f5-a5fe-43f8-b71e-7b1f0a8636fe";
        public const string structure_specialPlatform = "738892ae-64b0-4240-953c-cea1d19ca111";
        public const string cables_attachToBase = "18aa16f9-d1d8-4ccd-8a10-7ad32a5fd283";
        public const string cables_mid01 = "69cd7462-7cd2-456c-bfff-50903c391737";
        public const string cables_mid02 = "94933bb3-0587-4e8d-a38d-b7ec4c859b1a";
        public const string cables_mid03 = "31f84eba-d435-438c-a58e-f3f7bae8bfbd";
        public const string cables_attachToWall = "a0a9237e-dee3-4efa-81ff-fea3893a6eb7";
        public const string tabletDoor_orange = "83a70a58-f7da-4f18-b9b2-815dc8a7ffb4";
        public const string prop_microscope = "a30d0115-c37e-40ec-a5d9-318819e94f81";
        public const string prop_specimens = "da8f10dd-e181-4f28-bf98-9b6de4a9976a";
        public const string prop_claw = "6a01a336-fb46-469a-9f7d-1659e07d11d7";
        /// <summary>
        /// Slightly higher than 2m tall, has a 22.5ish degree angle.
        /// </summary>
        public const string prop_tabletPedestal = "814beddb-62cf-4c55-a86d-5da0684932a8";
        public const string genericDataTerminal = "b629c806-d3cd-4ee4-ae99-7b1359b60049";
        public const string supplies_nutrientBlock = "30373750-1292-4034-9797-387cf576d150";
        public const string supplies_smallWater = "22b0ce08-61c9-4442-a83d-ba7fb99f26b0";
        public const string supplies_mediumWater = "f7fb4077-b4d7-443c-b367-349cc1d39cc8";
        public const string supplies_bigWater = "$545c54a8-b23e-41bc-9d7c-af0b729e502f";
        public const string supplies_ionCube = "38ebd2e5-9dcc-4d7a-ada4-86a22e01191a";
        public const string supplies_drillableIonCube = "41406e76-4b4c-4072-86f8-f5b8e6523b73";
        public const string supplies_drillableLithium = "846c3df6-ffbf-4206-b591-72f5ba11ed40";
        public const string supplies_drillableTitanium = "9f855246-76c4-438b-8e4d-9cd6d7ce4224";
        public const string supplies_reactorRod = "cfdd714a-55fb-40df-86e5-6acf0d013b34";
        public const string supplies_titaniumIngot = "41919ae1-1471-4841-a524-705feb9c2d20";
        public const string supplies_firstAidKit = "bc70e8c8-f750-4c8e-81c1-4884fe1af34e";
        public const string supplies_whiteTablet = "066e533d-f854-435d-82c6-b28ba59858e0";
        public const string supplies_redTablet = "7d19f47b-6ec6-4a25-9b28-b3fd7f5661b7";
        public const string creature_alienRobot = "4fae8fa4-0280-43bd-bcf1-f3cba97eed77";
        public const string atmosphereVolume_cache = "f5dc3fa5-7ef7-429e-9dc6-2ea0e97b6187";
        public const string ambience_greenLight = "0b359b03-92e4-40df-81ed-aad488a7f13e";
        public const string airlock_1 = "03809334-e82d-40f5-9ccd-920e753887de";
        public const string natural_rockBlade1 = "f0438971-2761-412c-bc42-df80577de473";
        public const string natural_rockBlade2 = "282cdcbc-8670-4f9a-ae1d-9d8a09f9e880";
        public const string natural_coralClumpYellow = "5e8261d5-acce-4ec6-b77c-0f138770d5cb";
        public const string natural_coralClumpYellow_small = "339d91c3-9203-4c8f-8592-14b72ba7d5cc";
        public const string natural_coralClumpYellow_variant = "db86ef34-e1fa-4eb2-aa18-dda5af30cb45";
        public const string natural_boulder_small = "3e581d94-c873-4ad7-a2f4-a35ec6ac3ecb";
        public const string natural_boulder_big = "8d13d081-431e-4ed5-bc99-2b8b9fabe9c2";
        public const string natural_lr_hangingplant1_1 = "87f8fd44-43ca-44df-94c6-28b5962a6524";
        public const string natural_lr_hangingplant1_2 = "31d941ee-b00b-437c-abea-c2d42526ba0a";
        public const string natural_lr_hangingplant1_3 = "6d535e87-a4b1-4044-802b-f99491fe21fd";
        public const string natural_lr_hangingplant2_1 = "59f9f106-e2d4-45cc-9211-2d843d456282";
        public const string natural_lr_hangingplant2_2 = "c5664c82-d9f4-445e-86b1-b943e97e3913";
        public const string natural_lr_hangingplant2_3 = "c40f058c-e73b-4cf5-a4e5-6ce78a73899a";
        /// <summary>
        /// Faces up by default.
        /// </summary>
        public const string vfx_entrance = "8b5e6a02-533c-44cb-9f34-d2773aa82dc4";

        private List<GameObject> spawnedChildren;

        private void Start()
        {
            spawnedChildren = new List<GameObject>();
            ConstructBase();
            foreach (GameObject obj in spawnedChildren)
            {
                obj.transform.parent = null;
                LargeWorld.main.streamer.cellManager.RegisterEntity(obj.GetComponent<LargeWorldEntity>());
            }
        }

        /// <summary>
        /// Override this method to spawn the prefabs;
        /// </summary>
        public abstract void ConstructBase();

        public GameObject SpawnPrefab(string classId, Vector3 localPosition)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab, this.transform);
                spawnedObject.transform.localPosition = localPosition;
                spawnedObject.transform.localRotation = Quaternion.identity;
                spawnedObject.transform.localScale = Vector3.one;
                spawnedObject.SetActive(true);
                spawnedChildren.Add(spawnedObject);
                return spawnedObject;
            }
            return null;
        }

        public GameObject SpawnPrefab(string classId, Vector3 localPosition, Vector3 localRotation)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab, this.transform);
                spawnedObject.transform.localPosition = localPosition;
                spawnedObject.transform.localEulerAngles = localRotation;
                spawnedObject.transform.localScale = Vector3.one;
                spawnedObject.SetActive(true);
                spawnedChildren.Add(spawnedObject);
                return spawnedObject;
            }
            return null;
        }

        public GameObject SpawnPrefab(string classId, Vector3 localPosition, Vector3 localRotation, Vector3 scale)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab, this.transform);
                spawnedObject.transform.localPosition = localPosition;
                spawnedObject.transform.localEulerAngles = localRotation;
                spawnedObject.transform.localScale = scale;
                spawnedObject.SetActive(true);
                spawnedChildren.Add(spawnedObject);
                return spawnedObject;
            }
            return null;
        }

        public GameObject SpawnPrefabGlobally(string classId, Vector3 worldPosition)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab);
                spawnedObject.transform.position = worldPosition;
                spawnedObject.SetActive(true);
                LargeWorld.main.streamer.cellManager.RegisterEntity(spawnedObject.GetComponent<LargeWorldEntity>());
                return spawnedObject;
            }
            return null;
        }

        public GameObject SpawnPrefabGlobally(string classId, Vector3 worldPosition, Vector3 worldRotation, Vector3 scale)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab);
                spawnedObject.transform.position = worldPosition;
                spawnedObject.transform.eulerAngles = worldRotation;
                spawnedObject.transform.localScale = scale;
                spawnedObject.SetActive(true);
                LargeWorld.main.streamer.cellManager.RegisterEntity(spawnedObject.GetComponent<LargeWorldEntity>());
                return spawnedObject;
            }
            return null;
        }

        public GameObject SpawnPrefabGlobally(string classId, Vector3 worldPosition, Vector3 direction, bool directionIsRight)
        {
            if (PrefabDatabase.TryGetPrefab(classId, out GameObject prefab))
            {
                GameObject spawnedObject = GameObject.Instantiate(prefab);
                spawnedObject.transform.position = worldPosition;
                if (directionIsRight)
                {
                    spawnedObject.transform.right = direction;
                }
                else
                {
                    spawnedObject.transform.forward = direction;
                }
                spawnedObject.SetActive(true);
                LargeWorld.main.streamer.cellManager.RegisterEntity(spawnedObject.GetComponent<LargeWorldEntity>());
                return spawnedObject;
            }
            return null;
        }

        public void SpawnPrefabsArray(string classId, float spacing, Vector3 size, Vector3 individualScale, Vector3 offset = default)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int z = 0; z < size.z; z++)
                    {
                        Vector3 rawPosition = new Vector3(x, y, z);
                        Vector3 spacedPosition = Vector3.Scale(rawPosition, spacing * individualScale);
                        Vector3 positionWithOffset = spacedPosition - (Vector3.Scale(size, (spacing * individualScale) / 2f)) + offset;
                        SpawnPrefab(classId, positionWithOffset, Quaternion.identity.eulerAngles, individualScale);
                    }
                }
            }
        }

        /// <summary>
        /// All in terms of world positions.
        /// </summary>
        /// <param name="baseAttachPosition"></param>
        /// <param name="baseAttachForward"></param>
        /// <param name="terrainPosition"></param>
        /// <param name="terrainAttachForward"></param>
        /// <param name="offsetDirection"></param>
        /// <param name="quadraticMagnitude"></param>
        public void GenerateCable(Vector3 baseAttachPosition, Vector3 baseAttachForward, Vector3 terrainPosition, Vector3 terrainAttachForward, Vector3 offsetDirection, float quadraticMagnitude)
        {
            List<CableSegment> segments = GetCableSegments(baseAttachPosition, baseAttachForward, terrainPosition, terrainAttachForward, offsetDirection, quadraticMagnitude);
            foreach (CableSegment segment in segments)
            {
                SpawnPrefabGlobally(segment.classId, segment.position, segment.forward, true);
            }
        }

        const float midCableSpacing = 1.05f;
        private List<CableSegment> GetCableSegments(Vector3 basePosition, Vector3 baseAttachForward, Vector3 terrainPosition, Vector3 terrainAttachForward, Vector3 offsetDirection, float quadraticMagnitude)
        {
            List<CableSegment> segments = new List<CableSegment>();
            segments.Add(new CableSegment(cables_attachToBase, basePosition, baseAttachForward));
            segments.Add(new CableSegment(cables_attachToWall, terrainPosition, terrainAttachForward));
            int maxSegments = Mathf.RoundToInt(Vector3.Distance(basePosition, terrainPosition) / midCableSpacing);
            for (int i = 0; i < maxSegments; i++)
            {
                float percent = (float)i / (float)maxSegments;
                if (percent == 0f)
                {
                    continue;
                }
                Vector3 position = GetPositionOfCable(basePosition, terrainPosition, percent, offsetDirection, quadraticMagnitude);

                Vector3 forwardAfter = baseAttachForward;
                float nextPercent = Mathf.Clamp01((i + 1) / maxSegments);
                if (nextPercent == 1f)
                {
                    forwardAfter = terrainAttachForward;
                }
                else
                {
                    Vector3 nextSegmentPosition = GetPositionOfCable(basePosition, terrainPosition, nextPercent, offsetDirection, quadraticMagnitude);
                    forwardAfter = (position - nextSegmentPosition).normalized;
                }

                segments.Add(new CableSegment(GetMiddleCableRandom(i), position, forwardAfter));
            }
            return segments;
        }

        Vector3 GetPositionOfCable(Vector3 basePos, Vector3 terrainPos, float x, Vector3 offsetDir, float quadrMagnitude)
        {
            return Vector3.Lerp(basePos, terrainPos, x) + EvaluateQuadraticOffset(offsetDir, quadrMagnitude, x);
        }

        Vector3 EvaluateQuadraticOffset(Vector3 dir, float mag, float x)
        {
            float localMagnitude = -mag * x * (x - 1);
            return dir * localMagnitude;
        }

        public struct CableSegment
        {
            public string classId;
            public Vector3 position;
            public Vector3 forward;

            public CableSegment(string classId, Vector3 position, Vector3 forward)
            {
                this.classId = classId;
                this.position = position;
                this.forward = forward;
            }
        }

        static string GetMiddleCableRandom(int index)
        {
            int value = index % 3;
            if (value == 2)
            {
                return cables_mid01;
            }
            else if (value == 1)
            {
                return cables_mid02;
            }
            else
            {
                return cables_mid03;
            }
        }

        public void GenerateAtmospheres(GameObject placeholderHolder, string parentName, string atmosVolClassId)
        {
            GameObject parent = placeholderHolder.SearchChild(parentName);
            foreach (Transform child in parent.transform)
            {
                SpawnPrefabGlobally(atmosVolClassId, child.transform.position, child.transform.eulerAngles, child.transform.lossyScale);
            }
        }
    }
}
