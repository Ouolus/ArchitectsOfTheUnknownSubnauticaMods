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
            header.gameObject.SearchChild("Save").GetComponent<Button>().onClick.AddListener(OnSaveButton);
            currentEditor = this;
            
            OnLoaded();
        }

        /// <summary>
        /// Called when the Scene is loaded.
        /// </summary>
        protected abstract void OnLoaded();

        /// <summary>
        /// Defines what goes in the header of this Editor.
        /// </summary>
        public abstract string SceneName { get; }

        /// <summary>
        /// What code runs when the Save button is clicked.
        /// </summary>
        public abstract void OnSave();


        /// <summary>
        /// The code that runs when the Exit button is clicked. Opens up a prompt.
        /// </summary>
        public void OnExitButton()
        {
            StartCoroutine(ExitButtonCoroutine());
        }

        /// <summary>
        /// The code that runs when the Save button is clicked. Opens up a prompt.
        /// </summary>
        public void OnSaveButton()
        {
            StartCoroutine(SaveButtonCoroutine());
        }

        private IEnumerator ExitButtonCoroutine()
        {
            TaskResult<int> result = new TaskResult<int>();
            yield return UIPopups.MultiChoicePopup("Would you like to exit? All unsaved changes will be lost.", new string[] { "Yes", "No" }, result);
            if(result.Get() == 0)
            {
                Application.Quit();
            }
            else
            {
                yield break;
            }
        }

        private IEnumerator SaveButtonCoroutine()
        {
            TaskResult<int> result = new TaskResult<int>();
            yield return UIPopups.MultiChoicePopup("Would you like to save your changes?", new string[] { "Yes", "No" }, result);
            if (result.Get() == 0)
            {
                OnSave();
            }
            else
            {
                yield break;
            }
        }
    }
}
