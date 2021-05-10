using System.Collections;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Utility
{
    /// <summary>
    /// a Utility Class for Enhancing Materials and Shaders.
    /// </summary>
    public static class MaterialUtils
    {
        internal static void LoadMaterials()
        {
            CoroutineHost.StartCoroutine(LoadIonCubeMaterial());
            CoroutineHost.StartCoroutine(LoadPrecursorGlassMaterial());
        }

        /// <summary>
        /// Gets the Ion Cube's Material.
        /// </summary>
        public static Material IonCubeMaterial { get; private set; }

        /// <summary>
        /// Gets the Precursor Glass' Material.
        /// </summary>
        public static Material PrecursorGlassMaterial { get; private set; }

        /// <summary>
        /// Applies the Necessary Subnautica's shader(MarmosetUBER) to the passed <see cref="GameObject"/>.
        /// </summary>
        /// <param name="prefab">the <see cref="GameObject"/> to apply the shaders to.</param>
        public static void ApplySNShaders(GameObject prefab)
        {
            var renderers = prefab.GetComponentsInChildren<Renderer>(true);
            var newShader = Shader.Find("MarmosetUBER");
            for (int i = 0; i < renderers.Length; i++)
            {
                if(renderers[i] is ParticleSystemRenderer)
                {
                    return;
                }
                for (int j = 0; j < renderers[i].materials.Length; j++)
                {
                    Material material = renderers[i].materials[j];
                    Texture specularTexture = material.GetTexture("_SpecGlossMap");
                    Texture emissionTexture = material.GetTexture("_EmissionMap");
                    material.shader = newShader;

                    material.DisableKeyword("_SPECGLOSSMAP");
                    material.DisableKeyword("_NORMALMAP");
                    if (specularTexture != null)
                    {
                        material.SetTexture("_SpecTex", specularTexture);
                        material.SetFloat("_SpecInt", 1);
                        material.SetFloat("_Shininess", 8);
                        material.EnableKeyword("MARMO_SPECMAP");
                        material.SetColor("_SpecColor", new Color(1f, 1f, 1f, 1f));
                        material.SetFloat("_Fresnel", 0.24f);
                        material.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
                    }
                    if (material.IsKeywordEnabled("_EMISSION"))
                    {
                        material.EnableKeyword("MARMO_EMISSION");
                        material.SetFloat("_EnableGlow", 1f);
                        material.SetTexture("_Illum", emissionTexture);
                        material.SetFloat("_GlowStrength", 1);
                        material.SetFloat("_GlowStrengthNight", 1);
                    }

                    if (material.GetTexture("_BumpMap"))
                    {
                        material.EnableKeyword("MARMO_NORMALMAP");
                    }

                    if(material.name.Contains("Cutout"))
                    {
                        material.EnableKeyword("MARMO_ALPHA_CLIP");
                    }
                    if (material.name.Contains("Transparent"))
                    {
                        material.EnableKeyword("_ZWRITE_ON");
                        material.EnableKeyword("WBOIT");
                        material.SetInt("_ZWrite", 0);
                        material.SetInt("_Cutoff", 0);
                        material.SetFloat("_SrcBlend", 1f);
                        material.SetFloat("_DstBlend", 1f);
                        material.SetFloat("_SrcBlend2", 0f);
                        material.SetFloat("_DstBlend2", 10f);
                        material.SetFloat("_AddSrcBlend", 1f);
                        material.SetFloat("_AddDstBlend", 1f);
                        material.SetFloat("_AddSrcBlend2", 0f);
                        material.SetFloat("_AddDstBlend2", 10f);
                        material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack | MaterialGlobalIlluminationFlags.RealtimeEmissive;
                        material.renderQueue = 3101;
                        material.enableInstancing = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// Applies the Necessary Precursor Materials to the passed <see cref="GameObject"/>.
        /// </summary>
        /// <param name="prefab">the <see cref="GameObject"/> to apply the Materials to.</param>
        /// <param name="specint">Specular Strength.</param>
        public static void ApplyPrecursorMaterials(GameObject prefab, float specint)
        {
            foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>())
            {
                if(renderer is ParticleSystemRenderer)
                {
                    continue;
                }
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    Material material = renderer.materials[i];
                    if (material.name.Contains("IonShader"))
                    {
                        Material[] sharedMats = renderer.sharedMaterials;
                        sharedMats[i] = IonCubeMaterial;
                        renderer.sharedMaterials = sharedMats;
                        continue;
                    }
                    if (material.name.Contains("Transparent"))
                    {
                        Material[] sharedMats = renderer.sharedMaterials;
                        sharedMats[i] = PrecursorGlassMaterial;
                        renderer.sharedMaterials = sharedMats;
                        continue;
                    }
                    material.SetColor("_SpecColor", new Color(0.25f, 0.54f, 0.41f));
                    material.SetFloat("_SpecInt", specint);
                    material.SetFloat("_Fresnel", 0.4f);
                    /*if (material.name.Contains("Transparent"))
                    {
                        material.SetFloat("_SrcBlend", 1f);
                        material.SetFloat("_DstBlend", 1f);
                        material.SetFloat("_SrcBlend2", 0f);
                        material.SetFloat("_DstBlend2", 10f);
                        material.SetFloat("_AddSrcBlend", 1f);
                        material.SetFloat("_AddDstBlend", 1f);
                        material.SetFloat("_AddSrcBlend2", 0f);
                        material.SetFloat("_AddDstBlend2", 10f);
                        material.SetColor("_SpecColor", new Color(0.54f, 0.87f, 0.74f));
                        material.SetColor("_GlowColor", new Color(0.09f, 0.86f, 0.42f));
                        material.SetFloat("_GlowStrength", 0.05f);
                        material.SetFloat("_GlowStrengthNight", 0.05f);
                        material.SetFloat("_SpecInt", 1f);
                        material.SetFloat("_Shininess", 7f);
                        material.SetFloat("_Mode", 3f);
                        material.SetFloat("_Fresnel", 0.3f);
                        material.SetInt("_ZWrite", 0);
                        material.EnableKeyword("MARMO_EMISSION");
                        material.EnableKeyword("MARMO_SIMPLE_GLASS");
                        material.EnableKeyword("WBOIT");
                        material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                        material.EnableKeyword("MARMO_SPECMAP");
                    }*/
                }
            }
        }
        
        static IEnumerator LoadIonCubeMaterial()
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;

            GameObject ionCube = task.GetResult();
            IonCubeMaterial = ionCube.GetComponentInChildren<MeshRenderer>().material;
        }

        static IEnumerator LoadPrecursorGlassMaterial()
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync("2b43dcb7-93b6-4b21-bd76-c362800bedd1");
            yield return request;

            if(request.TryGetPrefab(out GameObject glassPanel))
            {
                PrecursorGlassMaterial = glassPanel.GetComponentInChildren<MeshRenderer>().material;
            }
        }
    }
}