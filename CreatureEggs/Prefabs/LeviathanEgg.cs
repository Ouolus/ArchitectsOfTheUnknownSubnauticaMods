using ArchitectsLibrary.API;

namespace CreatureEggs.Prefabs
{
    public abstract class LeviathanEgg : EggPrefab
    {
        public LeviathanEgg(string classId, string friendlyName, string description)
            : base(classId, friendlyName, description)
        {}
        
        public override float HatchingTime => 5f;
        
        public override bool AcidImmune => true;
        
        public override Vector2int SizeInInventory => new(3, 3);
        
        public override int RequiredACUSize => 2;
    }
}