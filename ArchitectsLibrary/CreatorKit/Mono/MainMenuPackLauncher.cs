using UnityEngine;
using ArchitectsLibrary.Utility;
using System.Collections;
using System.Collections.Generic;
using CreatorKit.Packs;
using UnityEngine.UI;

namespace CreatorKit.Mono
{
    internal class MainMenuPackLauncher : MonoBehaviour
    {
        private Transform listParent;
        private GameObject listButtonPrefab;
        private PackListButton selectedPack;
        private Button launchButton;
        private Button refreshButton;
        bool refreshing;
        MainMenuEditorLauncher editorLauncher;

        private void Start()
        {
            listParent = gameObject.SearchChild("PackListContent").transform;
            launchButton = gameObject.SearchChild("SelectEditorButton").GetComponent<Button>();
            launchButton.onClick.AddListener(LaunchWithCurrentPack);
            launchButton.interactable = false;
            refreshButton = gameObject.SearchChild("RefreshButton").GetComponent<Button>();
            refreshButton.onClick.AddListener(RefreshList);
            listButtonPrefab = UI.UIAssets.GetPackListButtonPrefab();
            GameObject editorLauncherObj = Instantiate(UI.UIAssets.GetEditorLauncherPrefab(), this.transform.parent, false);
            editorLauncherObj.SetActive(false);
            editorLauncher = editorLauncherObj.AddComponent<MainMenuEditorLauncher>();
            editorLauncher.packLauncher = this;
            RefreshList();
        }

        private IEnumerator ClearList()
        {
            foreach (Transform child in listParent.transform)
            {
                Destroy(child.gameObject);
                //yield return new WaitForSeconds(0.1f); Can't edit the list of children while we're during foreach, oops. This really doesn't need to be async anymore.
            }
            yield return null;
        }

        private IEnumerator LoadList()
        {
            List<string> allPacks = PackFolderUtils.GetAllPacks(false);
            Debug.Log(allPacks.Count);
            foreach (string packName in allPacks)
            {
                PackData data = PackHelper.GetPackData(packName);
                GameObject buttonObj = Instantiate(listButtonPrefab);
                buttonObj.GetComponent<RectTransform>().SetParent(listParent, false);
                if (data.Valid)
                {
                    SetListButtonDisplay(buttonObj, data.json.DisplayName, data.json.ShortDescription, data.json.Version, data.json.Author, data.id, PackHelper.LoadPackSprite(packName), true, Color.white);
                    buttonObj.AddComponent<PackListButton>().Initialize(this, packName);
                }
                else
                {
                    SetListButtonDisplay(buttonObj, packName, "This pack failed to load.", "N/A", "N/A", "N/A", UI.UIAssets.GetDefaultPackImage(), false, Color.red);
                }
                yield return new WaitForSeconds(0.1f); //Delay of 0.1 for a few reasons. An instant refresh makes it look like nothing even happened in the first place. A delay of 1 frame looks bad. A delay that is too slow makes it look poorly optimized.
            }
        }

        private IEnumerator RefreshListCoroutine()
        {
            refreshing = true;
            SelectPack(null);
            yield return ClearList();
            yield return LoadList();
            refreshing = false;
        }

        public void RefreshList()
        {
            if (!refreshing)
            {
                StartCoroutine(RefreshListCoroutine());
            }
        }
        private void SetListButtonDisplay(GameObject button, string title, string desc, string version, string author, string id, Sprite icon, bool interactable, Color color)
        {
            button.SearchChild("Title").GetComponent<Text>().text = title;
            button.SearchChild("Description").GetComponent<Text>().text = desc;
            button.SearchChild("Version").GetComponent<Text>().text = version;
            button.SearchChild("Author").GetComponent<Text>().text = author;
            button.SearchChild("ID").GetComponent<Text>().text = id;
            button.SearchChild("Icon").GetComponent<Image>().sprite = icon;
            button.GetComponent<Button>().interactable = interactable;
            button.GetComponent<Image>().color = color;
        }
        public void SelectPack(PackListButton packButton)
        {
            selectedPack = packButton;
            launchButton.interactable = packButton is not null;
            editorLauncher.CloseLauncher();
            foreach (Transform child in listParent)
            {
                PackListButton button = child.gameObject.GetComponent<PackListButton>();
                if (button)
                {
                    if (packButton is not null && button == packButton)
                    {
                        button.GetComponent<Image>().color = new Color(0f, 1f, 0.65f);
                    }
                    else
                    {
                        button.GetComponent<Image>().color = Color.white;
                    }
                }
            }
        }
        public void LaunchWithCurrentPack()
        {
            editorLauncher.OpenLauncher(selectedPack.packName);
        }
    }
}
