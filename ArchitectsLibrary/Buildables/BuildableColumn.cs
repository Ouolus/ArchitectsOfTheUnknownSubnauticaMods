using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableColumn : GenericPrecursorDecoration
    {
        public BuildableColumn() : base("BuildableColumn", "Alien Column Structure", "A tall, column-like object with a complex design.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(false, false, true, true, true, true, true);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 4f, 0f), Quaternion.identity, new Vector3(1f, 3.9f, 1f)) };

        protected override string GetOriginalClassId => "640f57a6-6436-4132-a9bb-d914f3e19ef5";
    }
}
