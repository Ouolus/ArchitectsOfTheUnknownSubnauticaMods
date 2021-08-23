using ArchitectsLibrary.API;
using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildablePedestalLarge : GenericPrecursorDecoration
    {
        public BuildablePedestalLarge() : base("BuildablePedestalLarge", LanguageSystem.Get("BuildablePedestalLarge"), LanguageSystem.GetTooltip("BuildablePedestalLarge"))
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 1.8f, 0f), Quaternion.identity, new Vector3(0.8f, 1.7f, 0.8f)) };

        protected override string GetOriginalClassId => "3bbf8830-e34f-43a1-bbb3-743c7e6860ac";

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "PedestalTall";
    }
}
