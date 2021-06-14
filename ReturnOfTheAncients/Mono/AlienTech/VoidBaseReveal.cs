using UnityEngine;
using ECCLibrary;
using System.Collections.Generic;
using System.Collections;
using System;
using Story;

namespace RotA.Mono.AlienTech
{
    public class VoidBaseReveal : OnBiomeChanged, IStoryGoalListener
    {
        public override string[] TargetBiomes => new string[] { "Prison_Antechamber", "PrecursorGun" };
        private Transform lightsParent;
        private Material[] interiorMaterials;
        private FMODAsset turnOnSound;
        private float timeVoiceNotifyAgain = 0;

        private GameObject tabletGlowPurple;
        private GameObject tabletGlowOrange;
        private GameObject tabletGlowBlue;
        private GameObject tabletGlowWhite;
        private GameObject tabletGlowRed;

        private StoryGoal approachBaseGoal = new StoryGoal("ApproachVoidBase", Story.GoalType.Story, 0f);

        protected override void OnTargetBiomeEntered()
        {
            StopAllCoroutines();
            StartCoroutine(SetLightsActive(true));
            StartCoroutine(ToggleEmission(true));
            Utils.PlayFMODAsset(turnOnSound, lightsParent);
            if (Time.time > timeVoiceNotifyAgain)
            {
                CustomPDALinesManager.PlayPDAVoiceLineFMOD("event:/player/gunterminal_access_denied", "VoidBaseWarningLog", "Translation: 'Infected individuals are not permitted to enter this facility. Housed specimen may be at risk of infection.'");
                timeVoiceNotifyAgain = Time.time + 60f;
            }
            SetExitCooldown(2f);
        }

        protected override void OnTargetBiomeExited()
        {
            StopAllCoroutines();
            StartCoroutine(SetLightsActive(false));
            StartCoroutine(ToggleEmission(false));
            SetEnterCooldown(2f);
        }

        private IEnumerator SetLightsActive(bool active)
        {
            foreach(Transform child in lightsParent)
            {
                child.gameObject.SetActive(active);
                yield return new WaitForSeconds(0.2f);
            }
        }

        private IEnumerator ToggleEmission(bool active)
        {
            float originalBrightness = active ? 0f : 1f;
            float targetBrightness = active ? 1f : 0f;
            for(int i = 1; i <= 10; i++)
            {
                yield return new WaitForSeconds(0.2f);
                SetMaterialBrightness(Mathf.Lerp(originalBrightness, targetBrightness, i / 10f));
            }
        }

        private void SetMaterialBrightness(float brightnessNormalized)
        {
            foreach (Material material in interiorMaterials)
            {
                material.SetFloat("_GlowStrength", brightnessNormalized);
                material.SetFloat("_GlowStrengthNight", brightnessNormalized);
                material.SetFloat("_SpecInt", brightnessNormalized * 10f);
            }
        }

        protected override void Setup()
        {
            lightsParent = gameObject.SearchChild("Lights").transform;
            interiorMaterials = new Material[5];
            interiorMaterials[0] = gameObject.SearchChild("EntryFloor").GetComponent<Renderer>().sharedMaterials[0];
            interiorMaterials[1] = gameObject.SearchChild("EntryFloor").GetComponent<Renderer>().sharedMaterials[1];
            interiorMaterials[2] = gameObject.SearchChild("VoidBaseV2Mural").GetComponent<Renderer>().sharedMaterials[0];
            interiorMaterials[3] = gameObject.SearchChild("VoidBaseV2Mural").GetComponent<Renderer>().sharedMaterials[1];
            interiorMaterials[4] = gameObject.SearchChild("VoidBaseInterior").GetComponent<Renderer>().sharedMaterials[0];
            //interiorMaterials[1] = gameObject.SearchChild("VoidBase-UpperMaze.004").GetComponent<Renderer>().sharedMaterials[3];
            SetMaterialBrightness(0f);
            turnOnSound = ScriptableObject.CreateInstance<FMODAsset>();
            turnOnSound.path = "event:/env/antechamber_lights_on";

            var voTrigger1 = gameObject.AddComponent<AlienBasePlayerTrigger>();
            voTrigger1.onTrigger = new AlienBasePlayerTrigger.OnTriggered();
            voTrigger1.onTrigger.AddListener(OnTrigger1);
            voTrigger1.triggerObject = gameObject.SearchChild("VOTrigger1");
        }

        public void OnEnable()
        {
            if(tabletGlowPurple is null)
            {
                tabletGlowPurple = gameObject.SearchChild("TabletGlowPurple");
                tabletGlowOrange = gameObject.SearchChild("TabletGlowOrange");
                tabletGlowBlue = gameObject.SearchChild("TabletGlowBlue");
                tabletGlowWhite = gameObject.SearchChild("TabletGlowWhite");
                tabletGlowRed = gameObject.SearchChild("TabletGlowRed");
                SetGlowActive(tabletGlowPurple, false);
                SetGlowActive(tabletGlowOrange, false);
                SetGlowActive(tabletGlowBlue, false);
                SetGlowActive(tabletGlowWhite, false);
                SetGlowActive(tabletGlowRed, false);
            }
            StoryGoalManager main = StoryGoalManager.main;
            if (main)
            {
                UpdateGlowsActive(main);
                main.AddListener(this);
            }
        }

        public void OnDisable()
        {
            StoryGoalManager main = StoryGoalManager.main;
            if (main)
            {
                main.RemoveListener(this);
            }
        }

        public void OnTrigger1(GameObject obj)
        {
            if (StoryGoalManager.main.OnGoalComplete(approachBaseGoal.key))
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("VoidBaseEncounter"), "VoidBaseEncounter", "Detecting unusual biological signatures originating from beyond this doorway. Approach with caution.");
            }
        }

        public void NotifyGoalComplete(string key)
        {
            if (key.StartsWith("VoidDoor"))
            {
                var main = StoryGoalManager.main;
                if (main)
                {
                    UpdateGlowsActive(main);
                }
            }
        }

        private void UpdateGlowsActive(StoryGoalManager storyGoalManager)
        {
            if (storyGoalManager.IsGoalComplete("VoidDoorPurple"))
            {
                SetGlowActive(tabletGlowPurple, true, new Color(0.81f, 0.38f, 1f));
            }
            if (storyGoalManager.IsGoalComplete("VoidDoorOrange"))
            {
                SetGlowActive(tabletGlowOrange, true, new Color(1f, 0.79f, 0.10f));
            }
            if (storyGoalManager.IsGoalComplete("VoidDoorBlue"))
            {
                SetGlowActive(tabletGlowBlue, true, new Color(0f, 0.89f, 1f));
            }
            if (storyGoalManager.IsGoalComplete("VoidDoorWhite"))
            {
                SetGlowActive(tabletGlowWhite, true, new Color(1f, 1f, 1f));
            }
            if (storyGoalManager.IsGoalComplete("VoidDoorRed"))
            {
                SetGlowActive(tabletGlowRed, true, new Color(1f, 0.20f, 0.20f));
            }
        }

        private void SetGlowActive(GameObject glowObj, bool active, Color color = default)
        {
            var renderer = glowObj.GetComponentInChildren<Renderer>();
            renderer.material.SetFloat("_GlowStrength", 2f);
            renderer.material.SetFloat("_GlowStrengthNight", 2f);
            if (active)
            {
                renderer.material.SetColor("_GlowColor", color);
            }
            else
            {
                renderer.material.SetColor("_GlowColor", Color.black);
            }
        }
    }
}
