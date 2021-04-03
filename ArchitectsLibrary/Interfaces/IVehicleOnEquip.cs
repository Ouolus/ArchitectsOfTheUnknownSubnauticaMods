namespace ArchitectsLibrary.Interfaces
{
    /// <summary>
    /// Used for when a module is equipped.
    /// </summary>
    public interface IVehicleOnEquip
    {
        /// <summary>
        /// your logic for your module when its Equipped into  a seamoth or prawn suit
        /// </summary>
        /// <param name="slotID">which slot your upgrade is on</param>
        /// <param name="vehicle">an instance of the <see cref="Vehicle"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnEquip(int slotID, bool equipped, Vehicle vehicle);
    }
}