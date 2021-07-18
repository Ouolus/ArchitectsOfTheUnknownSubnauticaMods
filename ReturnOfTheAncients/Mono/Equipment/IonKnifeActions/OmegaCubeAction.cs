using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class OmegaCubeAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = new[] { 40f, 60f, 60f };
            ionKnife.AttackDistance = 1.8f;
            ionKnife.DamageType = new[] { DamageType.Heat, DamageType.Normal, DamageType.Electrical };
            ionKnife.PlaySwitchSound("event:/env/damage/cold_loop");
            ionKnife.VfxEventType = VFXEventTypes.diamondBlade;
            ionKnife.ResourceBonus = 2;
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