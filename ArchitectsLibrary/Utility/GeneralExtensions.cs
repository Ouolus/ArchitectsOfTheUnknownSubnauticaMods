using System;
using UnityEngine;

namespace ArchitectsLibrary.Utility
{
    /// <summary>
    /// a Class that contains a collection of useful extensions.
    /// </summary>
    public static class GeneralExtensions
    {
        /// <summary>
        /// Disables the <see cref="Collider"/>s and <see cref="Renderer"/>s of a <see cref="GameObject"/> without making it completely In Active.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void SemiInActive(this GameObject gameObject)
        {
            foreach (var collider in gameObject.GetComponentsInChildren<Collider>())
                collider.enabled = false;

            foreach (var renderer in gameObject.GetAllComponentsInChildren<Renderer>())
                renderer.enabled = false;
        }
		
		/// <summary>
		/// An extension for adding an item to an array.
		/// </summary>
		/// <param name="array">the array to add to.</param>
		/// <param name="item">item to add to the array.</param>
		public static void Add<T>(this T[] array, T item)
		{
			Array.Resize<T>(ref array, array.Length + 1);
			array[array.Length - 1] = item;
		}

        /// <summary>
        /// searches for a Child Object of a <see cref="GameObject"/> by passing the Child's name.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="byName">The Child to search for.</param>
        /// <returns>Found Child from the passed string.</returns>
        public static GameObject SearchChild(this GameObject gameObject, string byName)
        {
            GameObject obj = SearchChildRecursive(gameObject, byName);
            if (obj is null)
            {
                ErrorMessage.AddMessage($"No child found in hierarchy by name {byName}.");
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