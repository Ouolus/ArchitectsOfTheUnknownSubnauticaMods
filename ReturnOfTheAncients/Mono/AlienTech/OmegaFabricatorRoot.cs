using UnityEngine;
using Story;
using System.Collections;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;
using UWE;

namespace RotA.Mono
{
    public class OmegaFabricatorRoot : MonoBehaviour
    {
        public OmegaTerminal terminal;

        public StoryGoal interactFailGoal = new StoryGoal("OmegaFabricatorInteractFail", Story.GoalType.Story, 0f);
        public StoryGoal interactSuccessGoal = new StoryGoal("OmegaFabricatorInteractSuccess", Story.GoalType.Story, 0f);

        public Transform generateCubeTransform;

        public Animator animator;
        public FMOD_CustomLoopingEmitter constructSoundEmitter;

        private bool cubeBeingBuilt;

        GameObject currentCube;

        public bool FormulaUnlocked()
        {
            if (StoryGoalManager.main.IsGoalComplete(Patches.PDAScanner_Patches.scanAdultGargGoal.key))
            {
                return true;
            }
            if (PDAEncyclopedia.ContainsEntry(Mod.gargVoidPrefab.ClassID))
            {
                return true;
            }
            return false;
        }

        public bool FabricatorEnabled()
        {
            return StoryGoalManager.main.IsGoalComplete(interactSuccessGoal.key);
        }

        public bool CanGenerateCube()
        {
            if (!FabricatorEnabled())
            {
                return false;
            }
            if (generateCubeTransform == null)
            {
                return false;
            }
            if (generateCubeTransform.childCount == 1)
            {
                return false;
            }
            if (cubeBeingBuilt == true)
            {
                return false;
            }
            if (currentCube != null)
            {
                return !currentCube.activeSelf;
            }
            return true;
        }

        public void AttemptToGenerateCube()
        {
            if (CanGenerateCube())
            {
                StartCoroutine(GenerateCubeCoroutine());
            }
        }

        void Update()
        {

        }

        public IEnumerator GenerateCubeCoroutine()
        {
            animator.SetBool("fabricating", true);
            constructSoundEmitter.Play();
            cubeBeingBuilt = true;
            var task = PrefabDatabase.GetPrefabAsync("41406e76-4b4c-4072-86f8-f5b8e6523b73");
            yield return task;
            task.TryGetPrefab(out GameObject prefab);
            GameObject cube = GameObject.Instantiate(prefab, generateCubeTransform);
            cube.SetActive(false);
            cube.TryDestroyChildComponents<LargeWorldEntity>();
            cube.TryDestroyChildComponents<PrefabIdentifier>();
            cube.TryDestroyChildComponents<ResourceTracker>();
            cube.TryDestroyChildComponents<TechTag>();
            cube.transform.localPosition = Vector3.zero;
            cube.GetComponentInChildren<Drillable>().resources = new Drillable.ResourceType[] { new() { techType = Mod.omegaCube.TechType, chance = 1f } };
            cube.GetComponentInChildren<Light>().color = Color.white;
            cube.GetComponentInChildren<Light>().color = Color.white;
            foreach (Renderer renderer in cube.GetComponentsInChildren<Renderer>())
            {
                renderer.material.SetColor("_Color", new(0.30f, 0.30f, 0.30f));
                renderer.material.SetColor("_SpecColor", Color.white);
                renderer.material.SetColor("_DetailsColor", new(1f, 4f, 2.5f));
                renderer.material.SetColor("_SquaresColor", new(0.5f, 0.5f, 0.5f));
                renderer.material.SetFloat("_SquaresSpeed", 4f);
            }
            cube.SetActive(true);
            yield return new WaitForSeconds(20f);
            currentCube = cube;
            cubeBeingBuilt = false;
            animator.SetBool("fabricating", false);
            constructSoundEmitter.Stop();
        }
    }
}
