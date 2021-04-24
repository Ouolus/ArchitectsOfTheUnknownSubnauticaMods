using UnityEngine;
using ECCLibrary;
using System.Collections.Generic;
using System.Collections;
using Story;

namespace ProjectAncients.Mono.AlienTech
{
    public class VoidBaseReveal : OnBiomeChanged
    {
        public override string[] TargetBiomes => new string[] { "Prison_Antechamber", "PrecursorGun" };
        private Transform lightsParent;
        private Material[] interiorMaterials;
        private FMODAsset turnOnSound;
        private float timeVoiceNotifyAgain = 0;

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
            interiorMaterials = new Material[0];
            //interiorMaterials[0] = gameObject.SearchChild("VoidBase-UpperMaze.002").GetComponent<Renderer>().sharedMaterials[0];
            //interiorMaterials[1] = gameObject.SearchChild("VoidBase-UpperMaze.004").GetComponent<Renderer>().sharedMaterials[3];
            SetMaterialBrightness(0f);
            turnOnSound = ScriptableObject.CreateInstance<FMODAsset>();
            turnOnSound.path = "event:/env/antechamber_lights_on";

            var voTrigger1 = gameObject.AddComponent<AlienBasePlayerTrigger>();
            voTrigger1.onTrigger = new AlienBasePlayerTrigger.OnTriggered(OnTrigger1);
            voTrigger1.triggerObject = gameObject.SearchChild("VOTrigger1");
        }

        public void OnTrigger1(GameObject obj)
        {
            if (!StoryGoalManager.main.OnGoalComplete(approachBaseGoal.key))
            {
                CustomPDALinesManager.PlayPDAVoiceLine(Mod.assetBundle.LoadAsset<AudioClip>("VoidBaseEncounter"), "VoidBaseEncounter", "Detecting leviathan-class lifeforms beyond this doorway. Approach with caution.");
            }
        }
    }
}
