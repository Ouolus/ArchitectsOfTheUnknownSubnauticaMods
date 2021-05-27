using RotA.Mono.Modules;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using SMLHelper.V2.Assets;
using UnityEngine;
using System.Collections;
using ECCLibrary;

namespace RotA.Prefabs.Modules
{
    public class SuperDecoy : Equipable
    {
        public SuperDecoy() : base("CyclopsDecoyMk2", "Creature Decoy MK2", "Attracts creatures to its location using ionic energy pulses. Can be deployed by hand or by a submarine. Reclaimable once deployed.")
        {
        }

        public override EquipmentType EquipmentType => EquipmentType.DecoySlot;

        public override TechCategory CategoryForPDA => TechCategory.Machines;

        public override TechGroup GroupForPDA => TechGroup.Machines;

        public override TechType RequiredForUnlock => Mod.architectElectricityMasterTech;

        public override float CraftingTime => 7f;

        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 3,
                Ingredients =
                {
                    new Ingredient(TechType.CyclopsDecoy, 3),
                    new Ingredient(TechType.PrecursorIonCrystal, 1),
                    new Ingredient(TechType.UraniniteCrystal, 2),
                    new Ingredient(TechType.Polyaniline, 1),
                }
            };
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.CyclopsDecoy);
            yield return task;

            var prefab = task.GetResult();
            var obj = GameObject.Instantiate(prefab);
            obj.AddComponent<EcoTarget>().type = Mod.superDecoyTargetType;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.Shark;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.MediumFish;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.Whale;
            MeshRenderer mesh = obj.GetComponentInChildren<MeshRenderer>();
            mesh.material.SetTexture(ShaderPropertyID._MainTex, Mod.assetBundle.LoadAsset<Texture2D>("DecoyMk2_Diffuse"));
            mesh.material.SetTexture(ShaderPropertyID._Illum, Mod.assetBundle.LoadAsset<Texture2D>("DecoyMk2_Illum"));
            mesh.material.SetFloat("_EmissionLM", 0.1f);
            mesh.material.SetFloat("_EmissionLMNight", 0.1f);
            mesh.material.SetFloat("_SpecInt", 3f);
            ParticleSystem flareParticleSystem = obj.SearchChild("xFlare").GetComponent<ParticleSystem>();
            var main = flareParticleSystem.main;
            main.startColor = new ParticleSystem.MinMaxGradient(Color.green);
            obj.transform.localScale = Vector3.one * 2f;
            ZapOnDamage zod = obj.AddComponent<ZapOnDamage>();
            zod.zapPrefab = Mod.electricalDefensePrefab;
            LiveMixinData lmd = ECCHelpers.CreateNewLiveMixinData();
            lmd.maxHealth = 650f;
            lmd.weldable = true;
            obj.AddComponent<LiveMixin>().data = lmd;
            obj.GetComponent<CyclopsDecoy>().lifeTime = float.MaxValue;
            obj.GetComponent<Pickupable>().isPickupable = true;
            Object.DestroyImmediate(obj.GetComponentInChildren<GenericHandTarget>());
            obj.GetComponentInChildren<VFXFabricating>(true).eulerOffset = new Vector3(0f, 90f, 270f);
            obj.GetComponentInChildren<VFXFabricating>(true).localMinY = -0.36f;
            obj.GetComponent<WorldForces>().underwaterGravity = 0f;

            prefab.SetActive(false);
            obj.SetActive(true);

            gameObject.Set(obj);
        }

#if SN1
        public override GameObject GetGameObject()
        {
            var prefab = CraftData.GetPrefabForTechType(TechType.CyclopsDecoy);
            var obj = GameObject.Instantiate(prefab);
            obj.AddComponent<EcoTarget>().type = Mod.superDecoyTargetType;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.Shark;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.MediumFish;
            obj.AddComponent<EcoTarget>().type = EcoTargetType.Whale;
            MeshRenderer mesh = obj.GetComponentInChildren<MeshRenderer>();
            mesh.material.SetTexture(ShaderPropertyID._MainTex, Mod.assetBundle.LoadAsset<Texture2D>("DecoyMk2_Diffuse"));
            mesh.material.SetTexture(ShaderPropertyID._Illum, Mod.assetBundle.LoadAsset<Texture2D>("DecoyMk2_Illum"));
            mesh.material.SetFloat("_EmissionLM", 0.1f);
            mesh.material.SetFloat("_EmissionLMNight", 0.1f);
            mesh.material.SetFloat("_SpecInt", 3f);
            ParticleSystem flareParticleSystem = obj.SearchChild("xFlare").GetComponent<ParticleSystem>();
            var main = flareParticleSystem.main;
            main.startColor = new ParticleSystem.MinMaxGradient(Color.green);
            obj.transform.localScale = Vector3.one * 2f;
            ZapOnDamage zod = obj.AddComponent<ZapOnDamage>();
            zod.zapPrefab = Mod.electricalDefensePrefab;
            LiveMixinData lmd = ECCHelpers.CreateNewLiveMixinData();
            lmd.maxHealth = 650f;
            lmd.weldable = true;
            obj.AddComponent<LiveMixin>().data = lmd;
            obj.GetComponent<CyclopsDecoy>().lifeTime = float.MaxValue;
            obj.GetComponent<Pickupable>().isPickupable = true;
            Object.DestroyImmediate(obj.GetComponentInChildren<GenericHandTarget>());
            obj.GetComponentInChildren<VFXFabricating>(true).eulerOffset = new Vector3(0f, 90f, 270f);
            obj.GetComponentInChildren<VFXFabricating>(true).localMinY = -0.36f;
            obj.GetComponent<WorldForces>().underwaterGravity = 0f;

            prefab.SetActive(false);
            obj.SetActive(true);

            return obj;
        }
#endif

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Mod.assetBundle.LoadAsset<Sprite>("DecoyMk2_Icon"));
        }

        public override Vector2int SizeInInventory => new Vector2int(1, 2);
    }
}
