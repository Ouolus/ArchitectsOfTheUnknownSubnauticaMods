using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class PrecursorIonCrystalAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = new[] { 25f };
            ionKnife.AttackDistance = 1.2f;
            ionKnife.DamageType = new[] { DamageType.Normal };
            ionKnife.PlaySwitchSound("event:/loot/prec_crystal_loop");
            ionKnife.VfxEventType = VFXEventTypes.knife;
            ionKnife.ResourceBonus = 0;
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}