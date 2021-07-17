using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class PrecursorIonCrystalAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = 25f;
            ionKnife.AttackDistance = 1.2f;
            ionKnife.DamageType = DamageType.Normal;
            ionKnife.SwitchSoundPath = "event:/loot/prec_crystal_loop";
            ionKnife.VfxEventType = VFXEventTypes.knife;
            // pew pew
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}