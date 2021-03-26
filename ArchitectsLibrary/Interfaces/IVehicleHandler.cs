namespace ArchitectsLibrary.Interfaces
{
    public interface IVehicleHandler
    {
        void EnergyCost(TechType techType, float energyCost);
        
        void MaxCharge(TechType techType, float maxCharge);
    }
}