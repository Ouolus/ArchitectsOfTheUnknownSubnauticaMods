using System.Collections;
using UnityEngine;
using UWE;
namespace LeviathanEggs.MonoBehaviours
{
    public class StagedGrowing : MonoBehaviour
    {
        public TechType nextStageTechType = TechType.None;
        public int daysToNextStage = 5;

        double timePassed;
        double days = -1;
        bool coroutineRunning = false;
        void Update()
        {
            timePassed = DayNightCycle.main.timePassed;

            days = (timePassed / 1200) - 1;

            if (gameObject.transform.localScale.x < 1f && gameObject.transform.localScale.y < 1f && gameObject.transform.localScale.z < 1f)
                if (!coroutineRunning)
                    CoroutineHost.StartCoroutine(StartGrowing());

            if (gameObject.transform.localScale.x >= 1f && gameObject.transform.localScale.y >= 1f && gameObject.transform.localScale.z >= 1f)
            {
                if (nextStageTechType != TechType.None && days >= daysToNextStage)
                {
                    foreach (var collider in gameObject.GetAllComponentsInChildren<Collider>())
                        collider.enabled = false;
                    foreach (var rend in gameObject.GetAllComponentsInChildren<Renderer>())
                        rend.enabled = false;

                    GameObject prefab = CraftData.GetPrefabForTechType(nextStageTechType);
                    prefab.SetActive(false);

                    GameObject nextStageObject = GameObject.Instantiate(prefab, transform.position, transform.rotation);
                    nextStageObject.EnsureComponent<ReadyToGrow>();
                    nextStageObject.SetActive(true);

                    DestroyImmediate(gameObject);
                }
            }
        }
        IEnumerator StartGrowing()
        {
            coroutineRunning = true;
            while (Time.timeScale > 0f && gameObject.transform.localScale.x <= 1f && gameObject.transform.localScale.y <= 1f && gameObject.transform.localScale.z <= 1f)
            {
                gameObject.transform.localScale += 0.001f * Vector3.one;
                yield return null;
            }

            coroutineRunning = false;

            yield break;
        }
    }
}
