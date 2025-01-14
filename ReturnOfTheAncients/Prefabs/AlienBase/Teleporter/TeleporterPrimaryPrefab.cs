﻿using RotA.Mono.AlienTech;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase.Teleporter
{
    public class TeleporterPrimaryPrefab : Spawnable
    {
        const string referenceClassId = "63e69987-7d34-41f0-aab9-1187ea06e740";
        private TeleporterFramePrefab frame;
        private CustomItemSettings customItemSettings;

        public TeleporterPrimaryPrefab(string classId, string teleporterId, Vector3 teleportPosition, float teleportAngle, bool disablePlatform, CustomItemSettings customItemSettings = default) : base(classId, "", "")
        {
            frame = new TeleporterFramePrefab(string.Format("{0}Frame", classId), teleporterId, teleportPosition, teleportAngle, disablePlatform, null);
            frame.Patch();
            this.customItemSettings = customItemSettings;
        }

        public void SetColor(Color color)
        {
            frame.SetColor(color);
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
                var ionCubeTerminal = obj.GetComponentInChildren<PrecursorTeleporterActivationTerminal>();
                var ionCubeTerminalProxy = obj.GetComponentInChildren<PrecursorTeleporterActivationTerminalProxy>();
                var newTerminal = ionCubeTerminalProxy.gameObject.EnsureComponent<CustomTeleporterTerminal>();
                newTerminal.acceptedTechTypes = customItemSettings.acceptedTechTypes;
                newTerminal.interactText = customItemSettings.interactText;
                newTerminal.storyGoalName = customItemSettings.storyGoalName;
                newTerminal.cinematicController = ionCubeTerminal.cinematicController;
                newTerminal.cinematicController.informGameObject = newTerminal.gameObject;
                newTerminal.animator = ionCubeTerminal.animator;
                newTerminal.useSound = ionCubeTerminal.useSound;
                newTerminal.openSound = ionCubeTerminal.openSound;
                newTerminal.closeSound = ionCubeTerminal.closeSound;
                newTerminal.root = obj;
                Transform trigger = obj.GetComponentInChildren<PrecursorKeyTerminalTrigger>().transform;
                trigger.transform.parent = newTerminal.transform;
                Object.DestroyImmediate(ionCubeTerminal);
                Object.DestroyImmediate(ionCubeTerminalProxy);
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
                var ionCubeTerminal = obj.GetComponentInChildren<PrecursorTeleporterActivationTerminal>();
                var ionCubeTerminalProxy = obj.GetComponentInChildren<PrecursorTeleporterActivationTerminalProxy>();
                var newTerminal = ionCubeTerminalProxy.gameObject.EnsureComponent<CustomTeleporterTerminal>();
                newTerminal.acceptedTechTypes = customItemSettings.acceptedTechTypes;
                newTerminal.interactText = customItemSettings.interactText;
                newTerminal.storyGoalName = customItemSettings.storyGoalName;
                newTerminal.cinematicController = ionCubeTerminal.cinematicController;
                newTerminal.cinematicController.informGameObject = newTerminal.gameObject;
                newTerminal.animator = ionCubeTerminal.animator;
                newTerminal.useSound = ionCubeTerminal.useSound;
                newTerminal.openSound = ionCubeTerminal.openSound;
                newTerminal.closeSound = ionCubeTerminal.closeSound;
                newTerminal.root = obj;
                Transform trigger = obj.GetComponentInChildren<PrecursorKeyTerminalTrigger>().transform;
                trigger.transform.parent = newTerminal.transform;
                Object.DestroyImmediate(ionCubeTerminal);
                Object.DestroyImmediate(ionCubeTerminalProxy);
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
