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
        private static IEnumerator LoadEditorScene<EditorType>(string packName) where EditorType : EditorBase
        {
            Scene[] scenesToUnload = GetLoadedScenes();
            Scene newScene = SceneManager.CreateScene("New scene");
            SceneManager.SetActiveScene(newScene);
            foreach (Scene sceneToUnload in scenesToUnload)
            {
                yield return SceneManager.UnloadSceneAsync(sceneToUnload);
            }
            Utility.Utils.GenerateEventSystemIfNeeded();
            GameObject cameraObj = new GameObject("MainCamera");
            cameraObj.AddComponent<Camera>();
            cameraObj.AddComponent<MainCamera>();
            EditorType editorInstance = new GameObject("Editor Parent").AddComponent<EditorType>();
            editorInstance.OnSceneLoaded(packName);
        }

        public static void LoadLanguageEditor(string packName)
        {
            ValidateSceneLoaderObj();
            editorLoader.StartCoroutine(LoadEditorScene<LanguageEditor>(packName));
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
