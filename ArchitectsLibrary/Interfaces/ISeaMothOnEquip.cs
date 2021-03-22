namespace ArchitectsLibrary.Interfaces
{
    public interface ISeaMothOnEquip
    {
        /// <summary>
        /// your logic for your module when its Equipped by a seamoth
        /// </summary>
        /// <param name="slotID">which slot your upgrade is on</param>
        /// <param name="seaMoth">an instance of the <see cref="SeaMoth"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnEquip(int slotID, SeaMoth seaMoth);
    }
}