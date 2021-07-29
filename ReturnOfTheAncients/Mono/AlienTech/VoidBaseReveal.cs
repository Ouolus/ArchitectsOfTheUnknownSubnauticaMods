using ArchitectsLibrary.API;
using ECCLibrary;
using Story;
using System.Collections;
using UnityEngine;

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
            foreach (Transform child in lightsParent)
            {
                child.gameObject.SetActive(active);
                yield return new WaitForSeconds(0.2f);
            }
        }

        private IEnumerator ToggleEmission(bool active)
        {
            float originalBrightness = active ? 0f : 1f;
            float targetBrightness = active ? 1f : 0f;
            for (int i = 1; i <= 10; i++)
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
            interiorMaterials = new Material[13];
            Renderer muralRenderer = gameObject.SearchChild("VoidBaseV2Mural").GetComponent<Renderer>();
            Renderer interiorRenderer = gameObject.SearchChild("VoidBaseV2Interior").GetComponent<Renderer>();
            interiorMaterials[0] = muralRenderer.sharedMaterials[0];
            interiorMaterials[1] = muralRenderer.sharedMaterials[1];
            interiorMaterials[2] = interiorRenderer.sharedMaterials[0];
            interiorMaterials[3] = interiorRenderer.sharedMaterials[1];
            interiorMaterials[4] = interiorRenderer.sharedMaterials[2];
            interiorMaterials[5] = interiorRenderer.sharedMaterials[3];
            interiorMaterials[6] = interiorRenderer.sharedMaterials[4];
            interiorMaterials[7] = interiorRenderer.sharedMaterials[5];
            interiorMaterials[8] = interiorRenderer.sharedMaterials[6];
            interiorMaterials[9] = interiorRenderer.sharedMaterials[7];
            interiorMaterials[10] = interiorRenderer.sharedMaterials[8];
            interiorMaterials[11] = interiorRenderer.sharedMaterials[9];
            interiorMaterials[12] = interiorRenderer.sharedMaterials[10];
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
            if (tabletGlowPurple is null)
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
                AchievementServices.CompleteAchievement("VisitVoidBase");
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
            renderer.material.SetFloat("_GlowStrength", 4f);
            renderer.material.SetFloat("_GlowStrengthNight", 4f);
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
