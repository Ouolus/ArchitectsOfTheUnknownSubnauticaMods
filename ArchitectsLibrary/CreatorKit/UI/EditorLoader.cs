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
    internal class EditorLoader : MonoBehaviour
    {
        static GameObject sceneLoaderObj;
        static EditorLoader editorLoader;

        private static void ValidateSceneLoaderObj()
        {
            if(sceneLoaderObj == null)
            {
                sceneLoaderObj = new GameObject("SceneLoader");
                GameObject.DontDestroyOnLoad(sceneLoaderObj);
                editorLoader = sceneLoaderObj.AddComponent<EditorLoader>();
            }
        }
        private static IEnumerator LoadEditorScene<EditorType>() where EditorType : EditorBase
        {
            EditorType editorInstance = Activator.CreateInstance<EditorType>();
            Scene[] loadedScenes = GetLoadedScenes();
            foreach(Scene sceneToUnload in loadedScenes)
            {
                yield return SceneManager.UnloadSceneAsync(sceneToUnload);
            }
            Scene newScene = SceneManager.CreateScene(editorInstance.SceneName);
            SceneManager.SetActiveScene(newScene);
            Utility.Utils.GenerateEventSystemIfNeeded();
            editorInstance.OnSceneLoaded();
            GameObject cameraObj = new GameObject("MainCamera");
            cameraObj.AddComponent<Camera>();
        }

        public static void LoadLanguageEditor()
        {
            ValidateSceneLoaderObj();
            editorLoader.StartCoroutine(LoadEditorScene<LanguageEditor>());
        }

        static Scene[] GetLoadedScenes()
        {
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }
            return loadedScenes;
        }
    }
}
