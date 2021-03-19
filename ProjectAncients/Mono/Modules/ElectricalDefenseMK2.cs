namespace ProjectAncients.Mono.Modules
{
    public class ElectricalDefenseMK2 : ElectricalDefense
    {
        void Awake()
        {
            radius = 20f;
            chargeRadius = 5f;
            damage = 100f;
            chargeDamage = 4f;
        }
    }
}