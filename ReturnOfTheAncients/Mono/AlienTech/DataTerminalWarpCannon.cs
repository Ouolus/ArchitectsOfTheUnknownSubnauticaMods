using UnityEngine;
using UWE;
using ArchitectsLibrary.API;

namespace RotA.Mono.AlienTech
{
    public class DataTerminalWarpCannon : MonoBehaviour
    {
        public void OnStoryHandTarget()
        {
            CraftData.AddToInventory(Mod.warpCannon.TechType);
        }
    }
}
