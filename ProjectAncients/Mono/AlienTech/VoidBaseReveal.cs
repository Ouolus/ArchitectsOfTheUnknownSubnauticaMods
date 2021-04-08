using UnityEngine;
using ECCLibrary;
using System.Collections.Generic;
using System.Collections;

namespace ProjectAncients.Mono.AlienTech
{
    public class VoidBaseReveal : OnBiomeChanged
    {
        public override string TargetBiome => "Prison_Antechamber";
        private Transform lightsParent;
        private Material[] interiorMaterials;

        protected override void OnTargetBiomeEntered()
        {
            StartCoroutine(SetLightsActive(true));
            ToggleEmission(true);
            SetExitCooldown(6f);
        }

        protected override void OnTargetBiomeExited()
        {
            StartCoroutine(SetLightsActive(false));
            ToggleEmission(false);
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

        private void ToggleEmission(bool active)
        {
            foreach(Material material in interiorMaterials)
            {
                if (active)
                {
                    material.SetFloat("_EnableGlow", 1f);
                }
                else
                {
                    material.SetFloat("_EnableGlow", 0f);
                }
            }
        }

        protected override void Setup()
        {
            lightsParent = gameObject.SearchChild("Lights").transform;
            interiorMaterials = new Material[2];
            interiorMaterials[0] = gameObject.SearchChild("VoidBase-UpperMaze.002").GetComponent<Renderer>().materials[0];
            interiorMaterials[1] = gameObject.SearchChild("VoidBase-UpperMaze.004").GetComponent<Renderer>().materials[3];
            ToggleEmission(true);
        }
    }
}
