using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectsLibrary.Utility
{
    /// <summary>
    /// Contains methods related to translations and other Language-related utility.
    /// </summary>
    public static class LanguageUtils
    {
        /// <summary>
        /// A format with support for multiple buttons rather than just one.
        /// </summary>
        /// <param name="key">The untranslated key. This key when translated should look something like this: '... {0}...{1}...`</param>
        /// <param name="button">The button to replace '{0}' in the translated text.</param>
        /// <param name="button2">The button to replace '{1}' in the translated text.</param>
        /// <returns></returns>
        public static string GetMultipleButtonFormat(string key, GameInput.Button button, GameInput.Button button2)
        {
            string bindingName = GameInput.GetBindingName(button, GameInput.BindingSet.Primary, false);
            LanguageCache.ButtonText orAddNew = LanguageCache.buttonTextCache.GetOrAddNew(key);
            if (orAddNew.button != button || orAddNew.bindingName != bindingName || string.IsNullOrEmpty(orAddNew.cachedUIText))
            {
                orAddNew.button = button;
                orAddNew.bindingName = bindingName;
                orAddNew.cachedUIText = Language.main.GetFormat(key, uGUI.FormatButton(button, false, " / ", false), uGUI.FormatButton(button2, false, " / ", false));
            }
            return orAddNew.cachedUIText;
        }
    }
}
