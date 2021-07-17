using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class RedIonCubeAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            ionKnife.Damage = 60f;
            ionKnife.AttackDistance = 1.8f;
            ionKnife.DamageType = DamageType.Heat;
            ionKnife.IdleSoundPath = "event:/env/pink_artifact_loop";
            ionKnife.VfxEventType = VFXEventTypes.heatBlade;
            // Red pew pew
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}