using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class ElectricubeAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            // Electrical pew pew
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}