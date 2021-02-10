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

        List<CreatureAction> creatureActions;
        void Start()
        {
            creatureActions = gameObject.GetComponentsInChildren<CreatureAction>().Where((x) => x.GetType().GetField("swimVelocity") != null)?.ToList() ?? new List<CreatureAction>();
            startTime = DayNightCycle.main.timePassedAsFloat;
            growTime = (1200 * daysToNextStage) ;
            CoroutineHost.StartCoroutine(StartGrowing());
        }
        void Update()
        {
            foreach (CreatureAction creatureAction in creatureActions)
            {
                Traverse swimVelocity = Traverse.Create(creatureAction).Field("swimVelocity");
                if (swimVelocity.FieldExists())
                {
                    float currentSpeed = swimVelocity.GetValue<float>();
                    swimVelocity.SetValue(currentSpeed * transform.localScale.x);
                }
            }
        }

        IEnumerator StartGrowing()
        {
            var startsSize = transform.localScale.x < 1f ? transform.localScale :Vector3.one * 0.1f;
            transform.localScale = startsSize;

            while ((!TryGetComponent(out WaterParkCreature WaterParkCreature) || !WaterParkCreature.IsInsideWaterPark()) && gameObject.transform.localScale.x <= 1f)
            {
                if(Time.timeScale > 0f)
                {
                    t = (DayNightCycle.main.timePassedAsFloat - startTime) / growTime;
                    gameObject.transform.localScale = Vector3.Lerp(startsSize, Vector3.one, t);
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

                Destroy(gameObject);
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(this);
            }

            yield break;
        }
    }
}
