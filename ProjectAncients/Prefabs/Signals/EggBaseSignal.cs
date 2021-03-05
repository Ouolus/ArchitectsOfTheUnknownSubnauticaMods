using ECCLibrary;
using ProjectAncients.Mono;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using UnityEngine;
using UWE;

namespace ProjectAncients.Prefabs
{
    public class EggBaseSignal : Spawnable
    {
        public static PingType pingType;
        static readonly Vector3 eggBasePosition = new Vector3(2000f, 0f, 0f);

        public EggBaseSignal()
            : base("EggBaseSignal", ".", ".")
        {
            OnFinishedPatching = () =>
            {
                Atlas.Sprite pingSprite = ImageUtils.LoadSpriteFromTexture(Mod.assetBundle.LoadAsset<Texture2D>("EggBasePingIcon"));
                SpriteHandler.RegisterSprite(SpriteManager.Group.Pings, "EggBase", pingSprite);
                pingType = PingHandler.RegisterNewPingType("EggBase", SpriteManager.Get(SpriteManager.Group.Pings, "EggBase"));
                LanguageHandler.SetLanguageLine("EggBase", "Mysterious energy signature");
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(TechType, eggBasePosition, "EggBasePing", 5000f));
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
            GameObject obj = new GameObject("EggBaseSignal");
            obj.SetActive(false);

            PingInstance ping = obj.EnsureComponent<PingInstance>();
            ping.pingType = pingType;
            ping.origin = obj.transform;
            ping.SetColor(3);

            SphereCollider trigger = obj.AddComponent<SphereCollider>(); //if you enter this trigger the ping gets disabled
            trigger.isTrigger = true;
            trigger.radius = 15f;

            SignalPing signalPing = obj.EnsureComponent<SignalPing>(); //basically to enable the disable on approach
            signalPing.pingInstance = ping;
            signalPing.disableOnEnter = true;

            var delayedInit = obj.AddComponent<SignalPingDelayedInitialize>(); //to override the serializer
            delayedInit.position = eggBasePosition;
            delayedInit.label = "EggBase";

            obj.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            obj.SetActive(true);
            return obj;
        }
    }
}
