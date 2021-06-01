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
        private const float animationLength = 20f;
        private float timeFabricationStart;

        GameObject currentCube;

        List<Material> cachedMaterials;

        public VFXController vfxController;

        bool CubeBeingFabricated
        {
            get
            {
                return cubeBeingBuilt && currentCube != null;
            }
        }

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

        void StartFabricationAnimation()
        {
            timeFabricationStart = Time.time;
            animator.SetBool("fabricating", true);
            CacheMaterials();
            vfxController.Play();
        }

        void StopFabricationAnimation()
        {
            animator.SetBool("fabricating", false);
            constructSoundEmitter.Stop();
            Collider[] componentsInChildren = currentCube.GetComponentsInChildren<Collider>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                componentsInChildren[i].enabled = true;
            }
            vfxController.StopAndDestroy(1f);
        }

        void Update()
        {
            if (CubeBeingFabricated)
            {
                UpdateScanAmountMaterials();
            }
        }

        void CacheMaterials()
        {
            cachedMaterials = new List<Material>();
            Renderer[] componentsInChildren = currentCube.GetComponentsInChildren<Renderer>();
            float value = currentCube.transform.position.y - 1.5f;
            float value2 = currentCube.transform.position.y + 5f;
            foreach (Renderer renderer in componentsInChildren)
            {
                for (int j = 0; j < renderer.materials.Length; j++)
                {
                    renderer.materials[j].EnableKeyword("FX_BUILDING");
                    renderer.materials[j].SetFloat(ShaderPropertyID._Built, 1f);
                    renderer.materials[j].SetFloat(ShaderPropertyID._minYpos, value);
                    renderer.materials[j].SetFloat(ShaderPropertyID._maxYpos, value2);
                    //renderer.materials[j].SetTexture(ShaderPropertyID._EmissiveTex, this._EmissiveTex);
                    renderer.materials[j].SetColor(ShaderPropertyID._BorderColor, new Color(0.75f, 1f, 0.9f, 1f));
                    renderer.materials[j].SetFloat(ShaderPropertyID._Cutoff, 0.42f);
                    renderer.materials[j].SetVector(ShaderPropertyID._BuildParams, new Vector4(2f, 0.7f, 3f, -0.25f));
                    renderer.materials[j].SetFloat(ShaderPropertyID._NoiseStr, 0.25f);
                    renderer.materials[j].SetFloat(ShaderPropertyID._NoiseThickness, 0.49f);
                    renderer.materials[j].SetFloat(ShaderPropertyID._BuildLinear, 1f);
                    renderer.materials[j].SetFloat(ShaderPropertyID._MyCullVariable, 0f);
                    cachedMaterials.Add(renderer.materials[j]);
                }
            }
        }

        void UpdateScanAmountMaterials()
        {
            float value = currentCube.transform.position.y - 1.5f;
            float value2 = currentCube.transform.position.y + 5f;
            float num = 1f;
            if (CubeBeingFabricated)
            {
                num = Mathf.Clamp((Time.time - timeFabricationStart) / animationLength, 0f, animationLength);
            }
            for (int i = 0; i < cachedMaterials.Count; i++)
            {
                Material material = cachedMaterials[i];
                if (material != null)
                {
                    material.SetFloat(ShaderPropertyID._Built, num);
                    material.SetFloat(ShaderPropertyID._minYpos, value);
                    material.SetFloat(ShaderPropertyID._maxYpos, value2);
                }
            }
        }

        IEnumerator GenerateCubeCoroutine()
        {
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
            foreach(Collider col in cube.GetComponentsInChildren<Collider>())
            {
                col.enabled = false;
            }
            currentCube = cube;
            constructSoundEmitter.Play();
            yield return new WaitForSeconds(4f);
            StartFabricationAnimation();
            cube.SetActive(true);
            yield return new WaitForSeconds(animationLength);
            cubeBeingBuilt = false;
            StopFabricationAnimation();
        }
    }
}
