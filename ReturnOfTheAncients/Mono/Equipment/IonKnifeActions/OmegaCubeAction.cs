using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class OmegaCubeAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = 80f;
            ionKnife.AttackDistance = 1.8f;
            ionKnife.DamageType = DamageType.Electrical;
            ionKnife.PlaySwitchSound("event:/env/damage/cold_loop");
            ionKnife.VfxEventType = VFXEventTypes.diamondBlade;
            // Omega pew pew
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}