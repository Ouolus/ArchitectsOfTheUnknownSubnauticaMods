using UnityEngine;
using ECCLibrary;
using System.Collections.Generic;
using System.Collections;

namespace ProjectAncients.Mono.AlienTech
{
    public class VoidBaseReveal : OnBiomeChanged
    {
        public override string[] TargetBiomes => new string[] { "Prison_Antechamber", "PrecursorGun" };
        private Transform lightsParent;
        private Material[] interiorMaterials;
        private FMODAsset turnOnSound;

        protected override void OnTargetBiomeEntered()
        {
            StartCoroutine(SetLightsActive(true));
            StartCoroutine(ToggleEmission(true));
            Utils.PlayFMODAsset(turnOnSound, lightsParent);
            SetExitCooldown(6f);
        }

        protected override void OnTargetBiomeExited()
        {
            StartCoroutine(SetLightsActive(false));
            StartCoroutine(ToggleEmission(false));
            SetEnterCooldown(4f);
        }

        private IEnumerator SetLightsActive(bool active)
        {
            foreach(Transform child in lightsParent)
            {
                child.gameObject.SetActive(active);
                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator ToggleEmission(bool active)
        {
            float originalBrightness = active ? 0f : 1f;
            float targetBrightness = active ? 1f : 0f;
            for(int i = 1; i <= 10; i++)
            {
                yield return new WaitForSeconds(0.25f);
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
        }
    }
}
