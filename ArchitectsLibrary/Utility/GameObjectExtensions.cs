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

        public static GameObject SearchChild(this GameObject gameObject, string byName)
        {
            GameObject obj = SearchChildRecursive(gameObject, byName);
            if (obj == null)
            {
                ErrorMessage.AddMessage(string.Format("No child found in hierarchy by name {0}.", byName));
            }
            return obj;
        }

        static GameObject SearchChildRecursive(GameObject gameObject, string byName)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (string.Equals(child.gameObject.name, byName))
                {
                    return child.gameObject;
                }
                GameObject recursive = SearchChildRecursive(child.gameObject, byName);
                if (recursive)
                {
                    return recursive;
                }
            }
            return null;
        }
    }
}