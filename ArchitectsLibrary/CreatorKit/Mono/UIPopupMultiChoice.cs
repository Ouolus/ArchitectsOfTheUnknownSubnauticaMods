using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ArchitectsLibrary.Utility;

namespace CreatorKit.Mono
{
    public class UIPopupMultiChoice : MonoBehaviour
    {
        private int chosen = -1;

        public IEnumerator InitializeAndWaitForResult(string question, string[] choices, TaskResult<int> chosenIndex)
        {
            chosen = -1;
            transform.GetChild(0).gameObject.GetComponent<Text>().text = question;
            GameObject buttonPrefab = UI.UIAssets.GetMultipleChoiceButtonPrefab();
            for(int i = 0; i < choices.Length; i++)
            {
                GameObject spawnedButton = Instantiate(buttonPrefab);
                spawnedButton.GetComponentInChildren<Text>().text = choices[i];
                spawnedButton.transform.parent = this.transform;
                Button buttonComponent = spawnedButton.GetComponent<Button>();
                buttonComponent.onClick.AddListener(delegate { ClickButton(i); });
            }
            yield return new WaitUntil(() => chosen != -1);
            chosenIndex.Set(chosen);
        }

        private void ClickButton(int index)
        {
            chosen = index;
        }
    }
}
