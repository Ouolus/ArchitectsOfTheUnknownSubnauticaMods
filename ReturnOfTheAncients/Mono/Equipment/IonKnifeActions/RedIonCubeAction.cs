using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class RedIonCubeAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = new[] { 40f, 20f };
            ionKnife.AttackDistance = 1.8f;
            ionKnife.DamageType = new[] { DamageType.Heat, DamageType.Normal };
            ionKnife.PlaySwitchSound("event:/env/pink_artifact_loop");
            ionKnife.VfxEventType = VFXEventTypes.heatBlade;
            ionKnife.ResourceBonus = 1;
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            if (IonKnife.IsCreature(hitLiveMixin))
            {
                Utils.PlayFMODAsset(ionKnife.StrongHitFishSound, transform);
            }
        }
    }
}