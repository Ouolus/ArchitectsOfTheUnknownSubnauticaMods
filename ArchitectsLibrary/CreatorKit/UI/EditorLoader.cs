using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UWE;

namespace CreatorKit.UI
{
    internal static class EditorLoader
    {
        private static IEnumerator LoadEditorScene<EditorType>() where EditorType : EditorBase
        {
            EditorType editorInstance = Activator.CreateInstance<EditorType>();
            Scene mainScene = SceneManager.GetActiveScene();
            Scene newScene = SceneManager.CreateScene(editorInstance.SceneName);
            SceneManager.SetActiveScene(newScene);
            yield return SceneManager.UnloadSceneAsync(mainScene);
            Utility.Utils.GenerateEventSystemIfNeeded();
            editorInstance.OnSceneLoaded();
        }

        public static void LoadLanguageEditor()
        {
            CoroutineHost.StartCoroutine(LoadEditorScene<LanguageEditor>());
        }
    }
}
