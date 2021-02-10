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
            List<CreatureAction> creatureActions = gameObject.GetComponentsInChildren<CreatureAction>().Where((x) => x.GetType().GetField("swimVelocity") != null)?.ToList() ?? new List<CreatureAction>();
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
            var startsSize = transform.localScale.x < 1f ? transform.localScale :Vector3.one * 0.1f;
            transform.localScale = startsSize;

            while ((!gameObject.TryGetComponent(out WaterParkCreature WaterParkCreature) || !WaterParkCreature.IsInsideWaterPark()) && gameObject.transform.localScale.x <= 1f)
            {
                if(Time.timeScale > 0f)
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
                yield return null;
            }

            if (nextStageTechType != TechType.None)
            {
                CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(nextStageTechType, false);
                yield return task;

                GameObject prefab = task.GetResult();
                prefab.SetActive(false);

                GameObject nextStageObject = Instantiate(prefab, transform.position, transform.rotation, new Vector3(0.1f, 0.1f, 0.1f), true);
                nextStageObject.EnsureComponent<StagedGrowing>();
                nextStageObject.SetActive(true);

                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }

            yield break;
        }
    }
}
