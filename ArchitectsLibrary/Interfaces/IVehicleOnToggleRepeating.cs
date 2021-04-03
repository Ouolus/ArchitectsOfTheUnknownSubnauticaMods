namespace ArchitectsLibrary.Interfaces
{
    /// <summary>
    /// Used for when a module is toggled on and has a repeating action, on an interval.
    /// </summary>
    public interface IVehicleOnToggleRepeating
    {
        /// <summary>
        /// The time between each call of OnToggleRepeating.
        /// </summary>
        float RepeatingActionCooldown { get; }

        /// <summary>
        /// your logic for your module when its Toggled on by a Seamoth or Exosuit, called on an interval of <see cref="RepeatingActionCooldown"/>
        /// </summary>
        /// <param name="slotID">which slot your upgrade is on</param>
        /// <param name="seaMoth">an instance of the <see cref="Vehicle"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void DoRepeatingAction(int slotID, Vehicle seaMoth);
    }
}