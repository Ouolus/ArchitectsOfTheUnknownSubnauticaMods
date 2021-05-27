using ECCLibrary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace RotA.Mono
{
    public class DataTerminalAnalyzeTech : MonoBehaviour
    {
        public TechType techToUnlock;

        public void OnStoryHandTarget()
        {
            KnownTech.Analyze(techToUnlock, true);
        }
    }
}
