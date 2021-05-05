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
        Text emptyListText;
        public int maxResultsInList = 250;

        public void Start()
        {
            listParent = gameObject.SearchChild("Content");
            gameObject.SearchChild("Header").GetComponent<Text>().text = GetHeaderText;
            filterInput = gameObject.SearchChild("Filter").GetComponent<InputField>();
            filterInput.onEndEdit.AddListener(new UnityAction<string>(OnFilter));
            emptyListText = gameObject.SearchChild("ListEmptyText").GetComponent<Text>();

            ConstructInitialData();

            OnFilter("");
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
            StopAllCoroutines();
            StartCoroutine(UpdateDisplay());
        }

        /// <summary>
        /// Use OnFilter instead.
        /// </summary>
        /// <returns></returns>
        public IEnumerator UpdateDisplay()
        {
            foreach (Transform child in listParent.transform)
            {
                Destroy(child.gameObject);
            }
            if (filteredData.Count > maxResultsInList)
            {
                emptyListText.text = string.Format("Amount of results ({0}) exceeds maximum recommended amount ({1}). Use a more specific filter.", filteredData.Count, maxResultsInList);
                emptyListText.enabled = true;
                yield break;
            }
            else
            {
                emptyListText.enabled = false;
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
