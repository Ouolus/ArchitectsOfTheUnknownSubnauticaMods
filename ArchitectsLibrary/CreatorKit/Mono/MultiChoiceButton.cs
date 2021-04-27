using UnityEngine;
using UnityEngine.UI;

namespace CreatorKit.Mono
{
    public class MultiChoiceButton : MonoBehaviour
    {
        private int index;
        private UIPopupMultiChoice popup;

        public void Initialize(int index, UIPopupMultiChoice popup)
        {
            this.index = index;
            this.popup = popup;
            GetComponent<Button>().onClick.AddListener(PressButton);
        }

        void PressButton()
        {
            popup.ClickButton(index);
        }
    }
}
