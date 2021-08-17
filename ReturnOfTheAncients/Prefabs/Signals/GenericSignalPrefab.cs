using RotA.Mono.AlienTech;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using System.Collections.Generic;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.Signals
{
    public class GenericSignalPrefab : Spawnable
    {
        public static List<PingType> registeredPingTypes = new List<PingType>();
        PingType pingType;
        Vector3 position;
        int defaultColorIndex;
        string pingTypeName;
        string labelKey;
        PlayVoiceLineTrigger.Data voiceLineData;

        /// <summary>
        /// Constructor for a generic signal.
        /// </summary>
        /// <param name="classId">Must be unique.</param>
        /// <param name="textureName"></param>
        /// <param name="displayName">Shows up in the beacon manager only.</param>
        /// <param name="label">Shows up in the HUD.</param>
        /// <param name="position"></param>
        /// <param name="defaultColorIndex"></param>
        /// <param name="voiceLineSettings">Settings related to the voice line that plays when approaching the signal. By default does no voice line.</param>
        public GenericSignalPrefab(string classId, string textureName, string displayName, string label, Vector3 position, int defaultColorIndex = 0, PlayVoiceLineTrigger.Data voiceLineSettings = default)
            : base(classId, displayName, ".")
        {
            this.pingTypeName = classId;
            this.defaultColorIndex = defaultColorIndex;
            this.position = position;
            this.voiceLineData = voiceLineSettings;
            OnFinishedPatching = () =>
            {
                Atlas.Sprite pingSprite = ImageUtils.LoadSpriteFromTexture(Mod.assetBundle.LoadAsset<Texture2D>(textureName));
                SpriteHandler.RegisterSprite(SpriteManager.Group.Pings, pingTypeName, pingSprite);
                pingType = PingHandler.RegisterNewPingType(pingTypeName, SpriteManager.Get(SpriteManager.Group.Pings, pingTypeName));
                registeredPingTypes.Add(pingType);
                LanguageHandler.SetLanguageLine(pingTypeName, displayName);

                labelKey = string.Format("{0}_label", new object[] { pingTypeName });
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

            obj.EnsureComponent<PrefabIdentifier>().classId = ClassID;
            obj.EnsureComponent<TechTag>().type = TechType;

            PingInstance ping = obj.EnsureComponent<PingInstance>();
            ping.pingType = pingType;
            ping.origin = obj.transform;

            SphereCollider trigger = obj.EnsureComponent<SphereCollider>(); //if you enter this trigger the ping gets disabled
            trigger.isTrigger = true;
            trigger.radius = 20f;

            SignalPing signalPing = obj.EnsureComponent<SignalPing>(); //basically to enable the disable on approach
            signalPing.pingInstance = ping;
            signalPing.disableOnEnter = true;

            var delayedInit = obj.EnsureComponent<SignalPingDelayedInitialize>(); //to override the serializer
            delayedInit.position = position;
            delayedInit.label = labelKey;
            delayedInit.pingTypeName = pingTypeName;
            delayedInit.colorIndex = defaultColorIndex;

            if (voiceLineData.hasVoiceLine)
            {
                var signalPingVoiceLine = obj.EnsureComponent<PlayVoiceLineTrigger>();
                signalPingVoiceLine.audioClip = voiceLineData.audioClip;
                signalPingVoiceLine.delay = voiceLineData.delay;
                signalPingVoiceLine.storyGoalKey = voiceLineData.storyGoalKey;
                signalPingVoiceLine.subtitleKey = voiceLineData.subtitleKey;
                signalPingVoiceLine.subtitleDisplayText = voiceLineData.subtitleDisplayText;
            }

            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.SetActive(true);
            return obj;
        }
    }
}
