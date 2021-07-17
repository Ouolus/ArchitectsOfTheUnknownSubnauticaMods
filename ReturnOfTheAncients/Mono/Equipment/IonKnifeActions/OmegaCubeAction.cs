using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class OmegaCubeAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            // Omega pew pew
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}