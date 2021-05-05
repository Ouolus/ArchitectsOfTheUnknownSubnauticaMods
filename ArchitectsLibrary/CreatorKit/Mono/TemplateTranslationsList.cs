using UnityEngine;

namespace CreatorKit.Mono
{
    public class TemplateTranslationsList : FilterableList
    {
        public override string GetHeaderText => "Template translations";

        public override void ConstructInitialData()
        {
            if(Language.main == null)
            {
                GameObject langManager = new GameObject("Language");
                langManager.AddComponent<Language>();
            }
            foreach (string str in Language.main.strings.Keys)
            {
                string translation = "???";
                Language.main.TryGet(str, out translation);
                allData.Add(str, translation);
            }
        }

        public override void OnElementSelected(FilterableListItem selected)
        {
            //will do this later
        }
    }
}
