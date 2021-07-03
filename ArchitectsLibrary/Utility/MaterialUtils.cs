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
        /// <param name="shininess">'_Shininess' value of the shader.</param>
        public static void ApplySNShaders(GameObject prefab, float shininess = 8f)
        {
            var renderers = prefab.GetComponentsInChildren<Renderer>(true);
            var newShader = Shader.Find("MarmosetUBER");
            for (int i = 0; i < renderers.Length; i++)
            {
                if(renderers[i] is ParticleSystemRenderer)
                {
                    continue;
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
                        material.SetFloat("_Shininess", shininess);
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
        /// Applies the Necessary Precursor Materials to the passed <see cref="GameObject"/>. Automatically replaces any materials that contains the string "IonShader" with an ion cube material and any material that contains the string "transparent" with a precursor glass material.
        /// </summary>
        /// <param name="prefab">the <see cref="GameObject"/> to apply the Materials to.</param>
        /// <param name="specint">Specular Strength.</param>
        /// <param name="specularColor">The color of the glow.</param>
        /// <param name="fresnelStrength">The strength of the fresnel (higher values cause the object to only glow around the edges).</param>
        public static void ApplyPrecursorMaterials(GameObject prefab, float specint, PrecursorSpecularColor specularColor = PrecursorSpecularColor.Green, float fresnelStrength = 0.4f)
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
                    if (material.name.ToLower().Contains("ionshader"))
                    {
                        Material[] sharedMats = renderer.materials;
                        sharedMats[i] = IonCubeMaterial;
                        renderer.materials = sharedMats;
                        continue;
                    }
                    if (material.name.ToLower().Contains("transparent"))
                    {
                        Material[] sharedMats = renderer.materials;
                        sharedMats[i] = PrecursorGlassMaterial;
                        renderer.materials = sharedMats;
                        continue;
                    }
                    material.SetColor("_SpecColor", GetSpecularColorForEnum(specularColor));
                    material.SetFloat("_SpecInt", specint);
                    material.SetFloat("_Fresnel", fresnelStrength);
                }
            }
        }

        /// <summary>
        /// Fixes any dark shading on any ion cube materials on or in the children of <paramref name="prefab"/> by updating the fakeSSS vector on the shader.
        /// </summary>
        /// <param name="prefab">The object to apply these changes to (includes children as well)</param>
        /// <param name="brightness">The brightness of the ion cube material.</param>
        public static void FixIonCubeMaterials(GameObject prefab, float brightness)
        {
            foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>())
            {
                if (renderer is ParticleSystemRenderer)
                {
                    continue;
                }
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    if (renderer.materials[i].name.ToLower().Contains("precursor_crystal_cube"))
                    {
                        renderer.materials[i].SetVector("_FakeSSSparams", new Vector4(brightness, 0f, 0f, 0f));
                    }
                }
            }
        }

        /// <summary>
        /// Defines specular colors that are commonly used in precursor materials.
        /// </summary>
        public enum PrecursorSpecularColor
        {
            /// <summary>
            /// Green specular. RGB: 0.25, 0.54, 0.41
            /// </summary>
            Green,
            /// <summary>
            /// Blue specular. RGB: 0.40, 0.69, 0.67
            /// </summary>
            Blue
        }

        static Color GetSpecularColorForEnum(PrecursorSpecularColor color)
        {
            if(color == PrecursorSpecularColor.Green)
            {
                return precursorSpecularGreen;
            }
            else
            {
                return precursorSpecularBlue;
            }
        }

        static Color precursorSpecularGreen = new Color(0.25f, 0.54f, 0.41f);
        static Color precursorSpecularBlue = new Color(0.40f, 0.69f, 0.67f);
        
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
                PrecursorGlassMaterial = new Material(glassPanel.GetComponentInChildren<MeshRenderer>().material);
                PrecursorGlassMaterial.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f, 0.4f));
                PrecursorGlassMaterial.SetFloat("_SpecInt", 1f);
            }
        }
    }
}