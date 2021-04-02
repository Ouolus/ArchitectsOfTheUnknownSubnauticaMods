namespace ArchitectsLibrary.Interfaces
{
    public interface IExosuitOnEquip
    {
        /// <summary>
        /// your logic for your module when its Equipped by an exosuit
        /// </summary>
        /// <param name="slotID">which slot your upgrade is on</param>
        /// <param name="exosuit">an instance of the <see cref="Exosuit"/> so you can use the class' stuff to benefit from a more
        /// complex logic for your upgrade.</param>
        void OnEquip(int slotID, Exosuit exosuit);
    }
}