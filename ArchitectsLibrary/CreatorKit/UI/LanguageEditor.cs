using UnityEngine;
using ArchitectsLibrary.Utility;
using CreatorKit.Mono;

namespace CreatorKit.UI
{
    public class LanguageEditor : EditorBase
    {
        public override string SceneName => "Language Editor";
        GameObject languageEditorBody;

        public override void OnSave()
        {

        }

        protected override void OnLoaded()
        {
            languageEditorBody = Utility.Utils.InstantiateUIChild(UIAssets.GetLanguageEditorPrefab(), body.transform);
            languageEditorBody.gameObject.SearchChild("TemplateTranslationsList").AddComponent<TemplateTranslationsList>();
        }
    }
}
