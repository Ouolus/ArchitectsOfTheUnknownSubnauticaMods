using UnityEngine;
using UnityEngine.UI;

namespace CreatorKit.Mono
{
    internal class PackListButton : MonoBehaviour
    {
        private MainMenuPackLauncher packLauncher;
        public Button button;
        public string packName;

        public void Initialize(MainMenuPackLauncher packLauncher, string packName)
        {
            this.packLauncher = packLauncher;
            this.packName = packName;
            this.button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            packLauncher.SelectPack(this);
        }
    }
}
