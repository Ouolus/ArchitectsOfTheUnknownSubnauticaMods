namespace ArchitectsLibrary.Interfaces
{
    /// <summary>
    /// Used for when a module is equipped into a cyclops.
    /// </summary>
    public interface ICyclopsOnEquip
    {
        /// <summary>
        /// your logic for your module when it is equipped into a cyclops.
        /// </summary>
        /// <param name="slotID">which slot in the upgrade console your upgrade is in</param>
        /// <param name="equipped">whether the module has been equipped or unequipped</param>
        /// <param name="sub">an instance of the <see cref="SubRoot"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnEquip(int slotID, bool equipped, SubRoot sub);
    }
}