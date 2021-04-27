using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ArchitectsLibrary.Utility;
using System.Collections;
using System.Collections.Generic;

namespace CreatorKit.Mono
{
    /// <summary>
    /// A UI element that can holds several values that can be filtered or clicked on.
    /// </summary>
    public abstract class FilterableList : MonoBehaviour, IGenericButtonCallback
    {
        public GameObject listParent;
        public Dictionary<string, string> allData = new Dictionary<string, string>();
        Dictionary<string, string> filteredData;
        bool updatingDisplay = false;
        InputField filterInput;

        public void Start()
        {
            listParent = gameObject.SearchChild("Content");
            gameObject.SearchChild("Header").GetComponent<Text>().text = GetHeaderText;
            filterInput = gameObject.SearchChild("Filter").GetComponent<InputField>();
            filterInput.onEndEdit.AddListener(new UnityAction<string>(OnFilter));

            ConstructInitialData();
        }

        /// <summary>
        /// The text at the top of the list.
        /// </summary>
        public abstract string GetHeaderText { get; }

        /// <summary>
        /// Called when the FIlterableList is first created.
        /// </summary>
        public abstract void ConstructInitialData();

        /// <summary>
        /// Called when the user filters the list.
        /// </summary>
        /// <param name="filter"></param>
        public void OnFilter(string filter)
        {
            filteredData = new Dictionary<string, string>();
            foreach(var pair in allData)
            {
                if (string.IsNullOrEmpty(filter) || pair.Key.ToLower().Contains(filter.ToLower()) || pair.Value.ToLower().Contains(filter.ToLower()))
                {
                    filteredData.Add(pair.Key, pair.Value);
                }
            }
            StartCoroutine(UpdateDisplay());
        }

        /// <summary>
        /// Use OnFilter instead.
        /// </summary>
        /// <returns></returns>
        public IEnumerator UpdateDisplay()
        {
            if (updatingDisplay)
            {
                yield return new WaitUntil(() => updatingDisplay == false);
            }
            updatingDisplay = true;
            filterInput.interactable = false;
            foreach (Transform child in listParent.transform)
            {
                Destroy(child.gameObject);
            }
            yield return null;
            GameObject buttonPrefab = UI.UIAssets.GetFilteredListButtonPrefab();
            foreach(var keyValuePair in filteredData)
            {
                string key = keyValuePair.Key;
                string val = keyValuePair.Value;
                GameObject instantiatedButton = Utility.Utils.InstantiateUIChild(buttonPrefab, listParent.transform);
                instantiatedButton.SearchChild("Key").GetComponent<Text>().text = string.Format("Key: {0}", key);
                instantiatedButton.SearchChild("Text").GetComponent<Text>().text = string.Format("Translation: {0}", val);
                var listItem = instantiatedButton.AddComponent<FilterableListItem>();
                listItem.key = key;
                listItem.value = val;
                yield return null;
            }
            filterInput.interactable = true;
            updatingDisplay = false;
        }

        public void OnButtonClicked(GameObject gameObject)
        {
            OnElementSelected(gameObject.GetComponent<FilterableListItem>());
        }

        /// <summary>
        /// Called when an item in the list is clicked on.
        /// </summary>
        /// <param name="selected"></param>
        public abstract void OnElementSelected(FilterableListItem selected);
    }
}
