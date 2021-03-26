namespace ArchitectsLibrary.Interfaces
{
    /// <summary>
    /// collection of the needed stuff for a <see cref="SeaMoth"/> Module to become functional on use.
    /// </summary>
    public interface ISeaMothOnUse
    {
        /// <summary>
        /// the cooldown of the upgrade module by seconds
        /// </summary>
        float Cooldown { get; }
        
        /// <summary>
        /// your core logic of the upgrade when its uses via a seamoth
        /// </summary>
        /// <param name="slotID">which slot your upgrade is on</param>
        /// <param name="seaMoth">an instance of the <see cref="SeaMoth"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnUpgradeUse(int slotID, SeaMoth seaMoth);
    }
}