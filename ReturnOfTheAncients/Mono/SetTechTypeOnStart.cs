using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RotA.Mono
{
    public class SetTechTypeOnStart : MonoBehaviour
    {
        public TechType type;

        void Start()
        {
            gameObject.EnsureComponent<TechTag>().type = type;
        }
    }
}
