namespace ArchitectsLibrary.Interfaces
{
    public interface ISeaMothOnToggle
    {
        float Cooldown { get; }
        
        /// <summary>
        /// your logic for your module when its Toggled on by a Seamoth
        /// </summary>
        /// <param name="slotID">which slot your upgrade is on</param>
        /// <param name="seaMoth">an instance of the <see cref="SeaMoth"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnToggle(int slotID, SeaMoth seaMoth);
    }
}