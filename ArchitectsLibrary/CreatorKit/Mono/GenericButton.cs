using UnityEngine;
using UnityEngine.UI;

namespace CreatorKit.Mono
{
    public class GenericButton : MonoBehaviour
    {
        public GameObject callbackObject;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }
        void OnClick()
        {
            callbackObject.GetComponent<IGenericButtonCallback>().OnButtonClicked(gameObject);
        }
    }

    public interface IGenericButtonCallback
    {
        void OnButtonClicked(GameObject gameObject);
    }
}
