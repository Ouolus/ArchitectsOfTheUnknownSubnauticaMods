using UnityEngine;
using UnityEngine.UI;
using ArchitectsLibrary.Utility;

namespace CreatorKit.Mono
{
    internal class MainMenuEditorLauncher : MonoBehaviour
    {
        public MainMenuPackLauncher packLauncher;

        void Start()
        {
            gameObject.SearchChild("ExitButton").GetComponent<Button>().onClick.AddListener(OnButtonExit);
        }

        public void OpenLauncher(string packName)
        {
            gameObject.SetActive(true);
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
            UI.EditorLoader.LoadLanguageEditor();
        }
    }
}
