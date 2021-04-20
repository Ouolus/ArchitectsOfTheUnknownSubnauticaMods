using UnityEngine;
using UnityEngine.UI;
using ArchitectsLibrary.Utility;

namespace CreatorKit.Mono
{
    internal class MainMenuEditorLauncher : MonoBehaviour
    {
        public MainMenuPackLauncher packLauncher;
        private string packName;

        void Start()
        {
            gameObject.SearchChild("ExitButton").GetComponent<Button>().onClick.AddListener(OnButtonExit);
            gameObject.SearchChild("LanguageEditorButton").GetComponent<Button>().onClick.AddListener(OnButtonLanguageEditor);
        }

        public void OpenLauncher(string packName)
        {
            gameObject.SetActive(true);
            this.packName = packName;
        }

        public void CloseLauncher()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonExit()
        {
            CloseLauncher();
        }

        public void OnButtonLanguageEditor()
        {
            UI.EditorLoader.LoadLanguageEditor(packName);
        }
    }
}
