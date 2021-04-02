namespace ArchitectsLibrary.Interfaces
{
    /// <summary>
    /// collection of the needed stuff for a <see cref="Exosuit"/> Module to turn on and off.
    /// </summary>
    public interface IExosuitOnToggle
    {
        /// <summary>
        /// your logic for your module when its toggled on or off via the hotbar
        /// </summary>
        /// <param name="slotID">which slot your upgrade is in</param>
        /// <param name="toggledOn">whether the module is toggled on or off</param>
        /// <param name="exosuit">an instance of the <see cref="Exosuit"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnToggle(int slotID, bool toggledOn, Exosuit exosuit);
    }
}