using UnityEngine;

namespace ArchitectsLibrary.Utility
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