namespace RotA.Interfaces
{ 
    using Mono.Equipment;

    public interface IIonKnifeRightHand
    {
        /// <summary>
        /// Called in IonKnife.OnRightHandDown
        /// </summary>
        /// <param name="ionKnife">the Ion Knife instance.</param>
        /// <returns>true if you want the use animation to be played, otherwise false.</returns>
        bool OnRightHandDown(IonKnife ionKnife);
        
        /// <summary>
        /// Called in IonKnife.OnRightHandHeld
        /// </summary>
        /// <param name="ionKnife">the Ion Knife instance.</param>
        /// <returns></returns>
        bool OnRightHandHeld(IonKnife ionKnife);
        
        /// <summary>
        /// Called in IonKnife.OnRightHandUp
        /// </summary>
        /// <param name="ionKnife"></param>
        /// <returns></returns>
        bool OnRightHandUp(IonKnife ionKnife);
    }
}