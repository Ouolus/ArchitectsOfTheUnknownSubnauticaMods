using RotA.Interfaces;
using UnityEngine;

namespace RotA.Mono.Equipment.IonKnifeActions
{
    public class RedIonCubeAction : MonoBehaviour, IIonKnifeAction
    {
        public void Initialize(IonKnife ionKnife)
        {
            // Red pew pew
        }

        public void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin)
        {
            
        }
    }
}