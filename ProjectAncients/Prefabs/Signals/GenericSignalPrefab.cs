using ECCLibrary;
using ProjectAncients.Mono;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using System.Collections.Generic;
using UnityEngine;
using UWE;

namespace ProjectAncients.Prefabs
{
    public class GenericSignalPrefab : Spawnable
    {
        public static List<PingType> registeredPingTypes = new List<PingType>();
        PingType pingType;
        Vector3 position;
        int defaultColorIndex;
        string pingTypeName;
        string labelKey;

        public GenericSignalPrefab(string classId, string textureName, string displayName, string label, Vector3 position, int defaultColorIndex = 0)
            : base(classId, displayName, ".")
        {
            this.pingTypeName = classId;
            this.defaultColorIndex = defaultColorIndex;
            this.position = position;
            OnFinishedPatching = () =>
            {
                Atlas.Sprite pingSprite = ImageUtils.LoadSpriteFromTexture(Mod.assetBundle.LoadAsset<Texture2D>(textureName));
                SpriteHandler.RegisterSprite(SpriteManager.Group.Pings, pingTypeName, pingSprite);
                pingType = PingHandler.RegisterNewPingType(pingTypeName, SpriteManager.Get(SpriteManager.Group.Pings, pingTypeName));
                registeredPingTypes.Add(pingType);
                LanguageHandler.SetLanguageLine(pingTypeName, displayName);

                labelKey = string.Format("{0}_label", new object[] { pingTypeName});
                LanguageHandler.SetLanguageLine(labelKey, label);
            };
        }

        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Global,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Small,
            techType = TechType
        };

        public override GameObject GetGameObject()
        {
            GameObject obj = new GameObject(ClassID);
            obj.SetActive(false);

            PingInstance ping = obj.EnsureComponent<PingInstance>();
            ping.pingType = pingType;
            ping.origin = obj.transform;

            SphereCollider trigger = obj.AddComponent<SphereCollider>(); //if you enter this trigger the ping gets disabled
            trigger.isTrigger = true;
            trigger.radius = 15f;

            SignalPing signalPing = obj.EnsureComponent<SignalPing>(); //basically to enable the disable on approach
            signalPing.pingInstance = ping;
            signalPing.disableOnEnter = true;

            var delayedInit = obj.AddComponent<SignalPingDelayedInitialize>(); //to override the serializer
            delayedInit.position = position;
            delayedInit.label = labelKey;
            delayedInit.pingTypeName = pingTypeName;

            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.SetActive(true);
            return obj;
        }
    }
}
