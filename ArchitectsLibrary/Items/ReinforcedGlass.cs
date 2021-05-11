using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectsLibrary.Items
{
    class ReinforcedGlass : ReskinItem
    {
        public ReinforcedGlass() : base("Reinforced Glass", "Reinforced Glass", "Glass reinforced with sapphire.")
        {
        }

        protected override string ReferenceClassId => "86589e2f-bd06-447f-b23a-1f35e6368010";

    }
}
