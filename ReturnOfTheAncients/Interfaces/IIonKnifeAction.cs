using RotA.Mono.Equipment;
using UnityEngine;

namespace RotA.Interfaces
{
    public interface IIonKnifeAction
    {
        void Initialize(IonKnife ionKnife);

        void OnHit(IonKnife ionKnife, GameObject hitObj);
    }
}