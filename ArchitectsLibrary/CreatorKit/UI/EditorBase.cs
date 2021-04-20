using UnityEngine;
using UnityEngine.UI;
using CreatorKit.Packs;
using ArchitectsLibrary.Utility;
using System.Collections;

namespace CreatorKit.UI
{
    internal abstract class EditorBase : MonoBehaviour
    {
        public static EditorBase currentEditor;
        protected PackData packData;
        public GameObject header;

        public void OnSceneLoaded(string packName)
        {
            packData = PackHelper.GetPackData(packName);
            GameObject.Instantiate(UIAssets.GetEditorBackgroundPrefab(), transform, false);
            header = GameObject.Instantiate(UIAssets.GetEditorHeaderPrefab(), transform, false);
            header.gameObject.SearchChild("EditorName").GetComponent<Text>().text = SceneName;
            header.gameObject.SearchChild("PackName").GetComponent<Text>().text = packData.json.DisplayName;
            header.gameObject.SearchChild("Exit").GetComponent<Button>().onClick.AddListener(OnExitButton);
            currentEditor = this;
            
            OnLoaded();
        }

        protected abstract void OnLoaded();

        public abstract string SceneName { get; }

        public abstract void OnSave();

        public void OnExitButton()
        {
            StartCoroutine(ExitButtonCoroutine());
        }

        private IEnumerator ExitButtonCoroutine()
        {
            TaskResult<int> result = new TaskResult<int>();
            yield return UIPopups.MultiChoicePopup("Would you like to exit? All unsaved changes will be lost.", new string[] { "Yes", "No" }, result);
            ErrorMessage.AddMessage(result.Get().ToString());
            if(result.Get() == 0)
            {
                Application.Quit();
            }
            else
            {
                yield break;
            }
        }
    }
}
