using System.Linq;
using System.Collections.Generic;
using System.Collections;
using HarmonyLib;
using UnityEngine;
using UWE;
namespace LeviathanEggs.MonoBehaviours
{
    public class StagedGrowing : MonoBehaviour
    {
        public TechType nextStageTechType = TechType.None;
        public float daysToNextStage = 5;

        float startTime;
        float growTime;
        float t = 0;
        Dictionary<CreatureAction, float> originalSpeeds = new Dictionary<CreatureAction, float>();

        void Start()
        {
            List<CreatureAction> creatureActions = gameObject.GetComponentsInChildren<CreatureAction>(true).Where((x) => x.GetType().GetField("swimVelocity") != null)?.ToList() ?? new List<CreatureAction>();
            foreach (CreatureAction creatureAction in creatureActions)
            {
                Traverse swimVelocity = Traverse.Create(creatureAction).Field("swimVelocity");
                if (swimVelocity.FieldExists())
                {
                    originalSpeeds[creatureAction] = swimVelocity.GetValue<float>();
                }
            }
            startTime = DayNightCycle.main.timePassedAsFloat;
            growTime = (1200 * daysToNextStage) ;
            CoroutineHost.StartCoroutine(StartGrowing());
        }

        IEnumerator StartGrowing()
        {
            var startsSize = gameObject.transform.localScale.sqrMagnitude < Vector3.one.sqrMagnitude ? transform.localScale : Vector3.one * 0.1f;
            transform.localScale = startsSize;

            while (t < 1)
            {
                if (Time.timeScale > 0f)
                {
                    WaterParkCreature waterParkCreature = gameObject.GetComponent<WaterParkCreature>();
                    if (waterParkCreature is null || !waterParkCreature.IsInsideWaterPark())
                    {
                        t = (DayNightCycle.main.timePassedAsFloat - startTime) / growTime;
                        var scale = Vector3.Lerp(startsSize, Vector3.one, t);
                        gameObject.transform.localScale = scale;

                        foreach (KeyValuePair<CreatureAction, float> pair in originalSpeeds)
                        {
                            Traverse swimVelocity = Traverse.Create(pair.Key).Field("swimVelocity");
                            if (swimVelocity.FieldExists())
                            {
                                swimVelocity.SetValue(pair.Value * scale.x);
                            }
                        }
                    }
                    else
                    {
                        t = (DayNightCycle.main.timePassedAsFloat - startTime) / growTime;
                    }
                }
                yield return null;
            }

            if (nextStageTechType != TechType.None)
            {
                CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(nextStageTechType, false);
                yield return task;

                GameObject prefab = task.GetResult();
                prefab.SetActive(false);

                GameObject nextStageObject = Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation, Vector3.one*0.1f, false);

                if (gameObject.TryGetComponent(out WaterParkCreature waterParkCreature) && waterParkCreature.IsInsideWaterPark())
                {
                    gameObject.SetActive(false);
                    WaterParkCreature parkCreature = nextStageObject.EnsureComponent<WaterParkCreature>();
                    Pickupable pickupable = nextStageObject.EnsureComponent<Pickupable>();
                    WaterParkCreatureParameters waterParkCreatureParameters = WaterParkCreature.GetParameters(nextStageTechType);
                    WaterPark waterPark = waterParkCreature.currentWaterPark;

                    parkCreature.age = 0f;
                    parkCreature.parameters = waterParkCreatureParameters;
                    parkCreature.pickupable = pickupable;
                    pickupable.isPickupable = true;
                    pickupable.timeDropped = Time.time;

                    waterPark.RemoveItem(waterParkCreature);
                    waterPark.AddItem(parkCreature);

                }

                nextStageObject.EnsureComponent<StagedGrowing>();
                nextStageObject.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
                nextStageObject.SetActive(true);

                Destroy(gameObject);
            }
            else
            {
                if (gameObject.TryGetComponent(out WaterParkCreature waterParkCreature) && waterParkCreature.IsInsideWaterPark())
                    gameObject.EnsureComponent<Pickupable>().isPickupable = true;

                Destroy(this);
            }

            yield break;
        }
    }
}
