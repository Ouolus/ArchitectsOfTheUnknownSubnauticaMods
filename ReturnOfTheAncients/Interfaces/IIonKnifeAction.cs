using RotA.Mono.Equipment;

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
        /// Called the moment this action is replaced by another.
        /// </summary>
        void EndAction(IonKnife ionKnife);

        /// <summary>
        /// Called when the knife hits an object.
        /// </summary>
        /// <param name="ionKnife">Ion Knife instance.</param>
        /// <param name="hitLiveMixin">the hit LiveMixin component.</param>
        void OnHit(IonKnife ionKnife, LiveMixin hitLiveMixin);
    }
}