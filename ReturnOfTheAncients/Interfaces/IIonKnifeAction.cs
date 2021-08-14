using RotA.Mono.Equipment;
using UnityEngine;

namespace RotA.Interfaces
{
    public interface IIonKnifeAction
    {
        /// <summary>
        /// Called the moment the action is set as the current action.
        /// </summary>
        /// <param name="ionKnife">Ion knife instance.</param>
        void Initialize(IonKnife ionKnife);

        /// <summary>
        /// Called when the knife is swung.
        /// </summary>
        /// <param name="ionKnife">Ion Knife instance.</param>
        /// <param name="hitLiveMixin">the hit LiveMixin component. May be null.</param>
        /// <param name="hitGameObject">the hit GameObject. May be null</param>
        void OnSwing(IonKnife ionKnife, LiveMixin hitLiveMixin, GameObject hitGameObject);

        /// <summary>
        /// Called every frame when the knife is ACTIVE only
        /// </summary>
        /// <param name="ionKnife"></param>
        void OnUpdate(IonKnife ionKnife);
    }
}