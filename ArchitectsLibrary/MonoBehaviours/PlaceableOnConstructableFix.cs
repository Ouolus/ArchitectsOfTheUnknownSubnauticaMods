using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArchitectsLibrary.MonoBehaviours
{
    /// <summary>
    /// Fixes an issue that allows you to delete items by placing constructable A on top of constructable B and deconstructing constructable B.
    /// </summary>
    class PlaceableOnConstructableFix : MonoBehaviour
    {
        Constructable constructable;

        void Start()
        {
            constructable = GetComponent<Constructable>();
        }

        void Update()
        {
            if(constructable.constructedAmount == 1f)
            {
                Destroy(this);
                if(transform.parent.GetComponent<Constructable>() is not null)
                {
                    transform.parent = transform.parent.parent;
                }
            }
        }
    }
}
