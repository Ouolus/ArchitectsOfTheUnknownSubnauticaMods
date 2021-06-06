using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArchitectsLibrary.Utility
{
    /// <summary>
    /// Contains methods related to translations and other Language-related utility.
    /// </summary>
    public static class LanguageUtils
    {
        /// <summary>
        /// LanguageCache does this in the base game. I assume the language system is slow and that's why we cache it.
        /// </summary>
        private static Dictionary<string, MultipleButtonCache> translatedButtonCache = new Dictionary<string, MultipleButtonCache>();

        /// <summary>
        /// A format with support for multiple buttons rather than just one.
        /// </summary>
        /// <param name="key">The untranslated key. This key's translated language line should look something like this: '... {0}...{1}...`</param>
        /// <param name="button">The button to replace '{0}' in the translated text.</param>
        /// <param name="button2">The button to replace '{1}' in the translated text.</param>
        /// <returns></returns>
        public static string GetMultipleButtonFormat(string key, GameInput.Button button, GameInput.Button button2)
        {
            string bindingName = GameInput.GetBindingName(button, GameInput.BindingSet.Primary, false);
            string bindingName2 = GameInput.GetBindingName(button2, GameInput.BindingSet.Primary, false);
            MultipleButtonCache orAddNew = translatedButtonCache.GetOrAddNew(key);
            if (orAddNew.button != button || orAddNew.button2 != button2 || orAddNew.bindingName != bindingName || orAddNew.bindingName2 != bindingName2 || string.IsNullOrEmpty(orAddNew.cachedUIText))
            {
                orAddNew.button = button;
                orAddNew.button2 = button2;
                orAddNew.bindingName = bindingName;
                orAddNew.bindingName2 = bindingName2;
                orAddNew.cachedUIText = Language.main.GetFormat(key, uGUI.FormatButton(button, false, " / ", false), uGUI.FormatButton(button2, false, " / ", false));
            }
            return orAddNew.cachedUIText;
        }

        /// <summary>
        /// A format with support for multiple buttons rather than just one. This overload allows you to put in a custom mod keybinding if you want.
        /// </summary>
        /// <param name="key">The untranslated key. This key when translated should look something like this: '... {0}...{1}...`</param>
        /// <param name="button">The button to replace '{0}' in the translated text.</param>
        /// <param name="button2">Whatever you put here will replace '{1}' in the translated text. For modded input, use <see cref="FormatKeyCode(KeyCode)"/></param>
        /// <returns>The formatted Buttons as a string.</returns>
        public static string GetMultipleButtonFormat(string key, GameInput.Button button, string button2)
        {
            string bindingName = GameInput.GetBindingName(button, GameInput.BindingSet.Primary, false);
            MultipleButtonCache orAddNew = translatedButtonCache.GetOrAddNew(key);
            if (orAddNew.button != button || orAddNew.bindingName != bindingName || orAddNew.bindingName2 != button2 || string.IsNullOrEmpty(orAddNew.cachedUIText))
            {
                orAddNew.button = button;
                orAddNew.bindingName = bindingName;
                orAddNew.bindingName2 = button2;
                orAddNew.cachedUIText = Language.main.GetFormat(key, uGUI.FormatButton(button), button2);
            }
            return orAddNew.cachedUIText;
        }

        /// <summary>
        /// A format with support for multiple buttons rather than just one. This overload allows you to put in a custom mod keybinding if you want.
        /// </summary>
        /// <param name="key">The untranslated key. This key when translated should look something like this: '... {0}...{1}...`</param>
        /// <param name="button">Whatever you put here will replace '{1}' in the translated text. For modded input, use <see cref="FormatKeyCode(KeyCode)"></see>.</param>
        /// <param name="button2">The button to replace '{0}' in the translated text</param>
        /// <returns>The formatted Buttons as a string.</returns>
        public static string GetMultipleButtonFormat(string key, string button, GameInput.Button button2)
        {
            string bindingName = GameInput.GetBindingName(button2, GameInput.BindingSet.Primary, false);
            MultipleButtonCache orAddNew = translatedButtonCache.GetOrAddNew(key);
            if (orAddNew.button != button2 || orAddNew.bindingName != bindingName || orAddNew.bindingName2 != button || string.IsNullOrEmpty(orAddNew.cachedUIText))
            {
                orAddNew.button = button2;
                orAddNew.bindingName = bindingName;
                orAddNew.bindingName2 = button;
                orAddNew.cachedUIText = Language.main.GetFormat(key, button, uGUI.FormatButton(button2));
            }
            return orAddNew.cachedUIText;
        }

        /// <summary>
        /// Converts a KeyCode into something that looks pretty.
        /// </summary>
        /// <param name="keyCode">Keycode to format</param>
        /// <returns>The formatted keycode as a string.</returns>
        public static string FormatKeyCode(KeyCode keyCode)
        {
            string formattedKeyCode;
            string bindingName = ConvertKeyCodeToBindingName(keyCode);
            string translated = uGUI.GetDisplayTextForBinding(bindingName);
            if (!string.IsNullOrEmpty(translated))
            {
                formattedKeyCode = string.Format("<color=#ADF8FFFF>{0}</color>", translated);
            }
            else
            {
                formattedKeyCode = string.Format("<color=#ADF8FFFF>{0}</color>", Language.main.Get("NoInputAssigned"));
            }
            return formattedKeyCode;
        }
        
        // Binding name strings and GameInput.Button are the preferred forms of input for Subnautica.
        static string ConvertKeyCodeToBindingName(KeyCode keyCode)
        {
            if(keyCodeToBindingName.TryGetValue(keyCode.ToString(), out string bindingName))
            {
                return bindingName;
            }
            return keyCode.ToString();
        }


        // Used for binding formatting for modded inputs. Converts it from Unity KeyCodes to the names used by GameInput.
        static readonly Dictionary<string, string> keyCodeToBindingName = new Dictionary<string, string>()
        {
            {
                "Mouse0",
                "MouseButtonLeft"
            },
            {
                "Mouse1",
                "MouseButtonRight"
            },
            {
                "Mouse2",
                "MouseButtonMiddle"
            },
        };


        class MultipleButtonCache
        {
            public GameInput.Button button;
            public GameInput.Button button2;
            public string bindingName;
            public string bindingName2;
            public string cachedUIText;
        }
    }
}
