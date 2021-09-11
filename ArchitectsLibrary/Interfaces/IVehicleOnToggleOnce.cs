namespace ArchitectsLibrary.Interfaces
{
    /// <summary>
    /// Used for when a module is enabled or disabled. For small vehicles only.
    /// </summary>
    public interface IVehicleOnToggleOnce
    {
        /// <summary>
        /// your logic for your module when its toggled on or off via the hotbar
        /// </summary>
        /// <param name="slotID">which slot your upgrade is in</param>
        /// <param name="toggledOn">whether the module is toggled on or off</param>
        /// <param name="vehicle">an instance of the <see cref="Vehicle"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnToggleOnce(int slotID, bool toggledOn, Vehicle vehicle);
    }
}