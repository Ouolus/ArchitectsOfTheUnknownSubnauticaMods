using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace LeviathanEggs.Helpers
{
    public static class GameObjectExtensions
    {
        public static void SemiInActive(this GameObject gameObject)
        {
            foreach (Collider collider in gameObject.GetComponentsInChildren<Collider>())
                collider.enabled = false;

            foreach (Renderer renderer in gameObject.GetAllComponentsInChildren<Renderer>())
                renderer.enabled = false;
        }
    }
}
