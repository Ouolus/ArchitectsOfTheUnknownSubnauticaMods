using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorKit.Packs
{
    internal struct PackData
    {
        public PackJson json;
        public string id;
        public bool invalid;

        public PackData(PackJson json, string id)
        {
            this.json = json;
            this.id = id;
            invalid = false;
        }

        public bool Valid
        {
            get
            {
                return !invalid;
            }
        }
    }
}
