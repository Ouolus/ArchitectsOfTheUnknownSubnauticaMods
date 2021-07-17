using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class ElectricubeAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = 25f;
            ionKnife.AttackDistance = 1.2f;
            ionKnife.DamageType = DamageType.Electrical;
            ionKnife.PlaySwitchSound("event:/env/green_artifact_loop");
            ionKnife.VfxEventType = VFXEventTypes.diamondBlade;
            // Electrical pew pew
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}