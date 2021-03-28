using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;
using System.Collections;
using ECCLibrary;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class AlienRelicPrefab : Spawnable
    {
        static FMODAsset relicSoundAsset;

        GameObject model;
        public AlienRelicPrefab(string classId, string friendlyName, string description, GameObject model) : base(classId, friendlyName, description)
        {
            this.model = model;
        }

#if SN1
        public override GameObject GetGameObject()
        {
            ValidateSoundAsset();
            GameObject obj = new();
            obj.SetActive(false);
            GameObject.Instantiate(model, obj.transform, false);
            CapsuleCollider capsule = obj.AddComponent<CapsuleCollider>();
            capsule.height = 6.296409f;
            capsule.radius = 1.4889f;
            capsule.direction = 2;
            obj.EnsureComponent<SkyApplier>().renderers = obj.GetComponentsInChildren<Renderer>();
            obj.EnsureComponent<TechTag>().type = TechType;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            ECCHelpers.ApplySNShaders(obj, new UBERMaterialProperties(8f, 1f, 2f));
            Mod.ApplyPrecursorMaterials(obj);
            var soundEmitter = obj.EnsureComponent<FMOD_CustomLoopingEmitter>();
            soundEmitter.asset = relicSoundAsset;
            soundEmitter.playOnAwake = true;
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            ValidateSoundAsset();
            GameObject obj = new();
            obj.SetActive(false);
            GameObject.Instantiate(model, obj.transform, false);
            CapsuleCollider capsule = obj.AddComponent<CapsuleCollider>();
            capsule.height = 6.296409f;
            capsule.radius = 1.4889f;
            capsule.direction = 2;
            obj.EnsureComponent<SkyApplier>().renderers = obj.GetComponentsInChildren<Renderer>();
            obj.EnsureComponent<TechTag>().type = TechType;
            obj.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            ECCHelpers.ApplySNShaders(obj, new UBERMaterialProperties(8f, 1f, 2f));
            Mod.ApplyPrecursorMaterials(obj);
            var soundEmitter = obj.EnsureComponent<FMOD_CustomLoopingEmitter>();
            soundEmitter.asset = relicSoundAsset;
            soundEmitter.playOnAwake = true;
            yield return null;
            gameObject.Set(obj);
        }
#endif

        void ValidateSoundAsset()
        {
            if(relicSoundAsset == null)
            {
                relicSoundAsset = ScriptableObject.CreateInstance<FMODAsset>();
                relicSoundAsset.id = "{398345e3-b59f-4eb3-8fd0-f4e87f282968}";
                relicSoundAsset.path = "event:/env/prec_artifact_loop";
            }
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Near,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = TechType
        };
    }
}
