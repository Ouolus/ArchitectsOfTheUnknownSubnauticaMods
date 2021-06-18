using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;
using RotA.Mono.AlienTech;

namespace RotA.Prefabs.AlienBase.Teleporter
{
    public class TeleporterPrimaryPrefab : Spawnable
    {
        const string referenceClassId = "63e69987-7d34-41f0-aab9-1187ea06e740";
        private TeleporterFramePrefab frame;
        private CustomItemSettings customItemSettings;

        public TeleporterPrimaryPrefab(string classId, string teleporterId, Vector3 teleportPosition, float teleportAngle, bool disablePlatform, bool omegaTeleporter, CustomItemSettings customItemSettings = default) : base(classId, "", "")
        {
            frame = new TeleporterFramePrefab(string.Format("{0}Frame", classId), teleporterId, teleportPosition, teleportAngle, disablePlatform, omegaTeleporter);
            frame.Patch();
            this.customItemSettings = customItemSettings;
        }

#if SN1
        public override GameObject GetGameObject()
        {
            PrefabDatabase.TryGetPrefab(referenceClassId, out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);

            obj.SetActive(false);
            var prefabPlaceholder = obj.GetComponent<PrefabPlaceholdersGroup>();
            prefabPlaceholder.prefabPlaceholders[0].prefabClassId = frame.ClassID;

            if (customItemSettings.useCustomItem)
            {
                var ionCubeTerminal = obj.GetComponent<PrecursorTeleporterActivationTerminal>();
                var newTerminal = obj.EnsureComponent<CustomTeleporterTerminal>();
                newTerminal.acceptedTechTypes = customItemSettings.acceptedTechTypes;
                newTerminal.interactText = customItemSettings.interactText;
                newTerminal.storyGoalName = customItemSettings.storyGoalName;
                newTerminal.cinematicController = ionCubeTerminal.cinematicController;
                newTerminal.animator = ionCubeTerminal.animator;
                newTerminal.useSound = ionCubeTerminal.useSound;
                newTerminal.openSound = ionCubeTerminal.openSound;
                newTerminal.closeSound = ionCubeTerminal.closeSound;
                newTerminal.root = ionCubeTerminal.root;
                Object.DestroyImmediate(ionCubeTerminal);
                Object.DestroyImmediate(obj.GetComponent<PrecursorTeleporterActivationTerminalProxy>());
            }
            return obj;
        }
#elif SN1_exp
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(referenceClassId);
            yield return request;
            request.TryGetPrefab(out GameObject prefab);
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            var prefabPlaceholder = obj.GetComponent<PrefabPlaceholdersGroup>();
            prefabPlaceholder.prefabPlaceholders[0].prefabClassId = frame.ClassID;

            if (customItemSettings.useCustomItem)
            {
                var ionCubeTerminal = obj.GetComponent<PrecursorTeleporterActivationTerminal>();
                var newTerminal = obj.EnsureComponent<CustomTeleporterTerminal>();
                newTerminal.acceptedTechTypes = customItemSettings.acceptedTechTypes;
                newTerminal.interactText = customItemSettings.interactText;
                newTerminal.storyGoalName = customItemSettings.storyGoalName;
                newTerminal.cinematicController = ionCubeTerminal.cinematicController;
                newTerminal.animator = ionCubeTerminal.animator;
                newTerminal.useSound = ionCubeTerminal.useSound;
                newTerminal.openSound = ionCubeTerminal.openSound;
                newTerminal.closeSound = ionCubeTerminal.closeSound;
                newTerminal.root = ionCubeTerminal.root;
                Object.DestroyImmediate(ionCubeTerminal);
                Object.DestroyImmediate(obj.GetComponent<PrecursorTeleporterActivationTerminalProxy>());
            }
            gameObject.Set(obj);
        }
#endif

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };

        public struct CustomItemSettings
        {
            public bool useCustomItem;
            public TechType[] acceptedTechTypes;
            public string storyGoalName;
            public string interactText;

            public CustomItemSettings(TechType[] acceptedTechTypes, string storyGoalName, string interactText)
            {
                useCustomItem = true;
                this.acceptedTechTypes = acceptedTechTypes;
                this.storyGoalName = storyGoalName;
                this.interactText = interactText;
            }
        }
    }
}
