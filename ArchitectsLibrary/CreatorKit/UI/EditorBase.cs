using UnityEngine;

namespace ArchitectsLibrary.CreatorKit.UI
{
    internal abstract class EditorBase : MonoBehaviour
    {
        public abstract void OnSceneLoaded();

        public abstract string SceneName { get; }
    }
}
