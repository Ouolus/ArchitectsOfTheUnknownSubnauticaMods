namespace ArchitectsLibrary.Interfaces
{
    /// <summary>
    /// Used for when a module with a single, instant action is used. Seamoth specific.
    /// </summary>
    public interface ISeaMothOnUse
    {
        /// <summary>
        /// the cooldown of the upgrade module by seconds
        /// </summary>
        float UseCooldown { get; }
        
        /// <summary>
        /// your core logic of the upgrade when its uses via a seamoth
        /// </summary>
        /// <param name="slotID">which slot your upgrade is on</param>
        /// <param name="seaMoth">an instance of the <see cref="SeaMoth"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnUpgradeUse(int slotID, SeaMoth seaMoth);
    }
}