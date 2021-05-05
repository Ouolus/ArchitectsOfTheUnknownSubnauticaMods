using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CreatorKit.UI
{
    /// <summary>
    /// Warning: these do NOT work in the main menu
    /// </summary>
    public static class UIPopups
    {
        public static IEnumerator MultiChoicePopup(string question, string[] options, TaskResult<int> result)
        {
            GameObject popupCanvas = GameObject.Instantiate(UIAssets.GetMultipleChoicePopupPrefab());
            var multiChoiceComponent = popupCanvas.transform.GetChild(0).gameObject.AddComponent<Mono.UIPopupMultiChoice>();
            yield return multiChoiceComponent.InitializeAndWaitForResult(question, options, result);
            GameObject.Destroy(popupCanvas);
        }
    }
}
