using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreatorKit.Utility
{
    internal static class Utils
    {
        private static string snFolderPath;

        /// <summary>
        /// Returns the path to the SN folder, an example would be C:\Program Files (x86)\Steam\steamapps\common\Subnautica.
        /// </summary>
        public static string GetSNFolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(snFolderPath))
                {
                    RefreshSNFolderPath();
                }
                return snFolderPath;
            }
        }

        /// <summary>
        /// Updates the cached folder path <see cref="GetSNFolderPath"/>. This is generally done automatically so I'm not sure if it will ever be necessary.
        /// </summary>
        public static void RefreshSNFolderPath()
        {
            snFolderPath = Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\");
        }

        public static void PrintErrorMessage(string message)
        {
            UnityEngine.Debug.Log(message);
            QModManager.API.QModServices.Main.AddCriticalMessage(message);
            ErrorMessage.AddMessage(message);
        }

        public static string ReadText(string path)
        {
            if (!File.Exists(path))
            {
                return "";
            }
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        public static void WriteText(string path, string text)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(text);
                }
            }
        }

        public static Sprite LoadSprite(string iconPath, Sprite defaultSprite = null)
        {
            Texture2D tex = LoadPNG(iconPath);
            if (tex == null)
            {
                return defaultSprite;
            }
            else
            {
                Sprite sprite = Sprite.Create(tex, new Rect(new Vector2(0f, 0f), new Vector2(tex.width, tex.width)), new Vector2(0.5f, 0.5f));
                return sprite;
            }
        }

        public static Texture2D LoadPNG(string imagePath)
        {
            return SMLHelper.V2.Utility.ImageUtils.LoadTextureFromFile(imagePath);
        }

        public static void GenerateEventSystemIfNeeded()
        {
            var currentEventSystem = Object.FindObjectOfType<EventSystem>();
            if(currentEventSystem is null)
            {
                GameObject go = new GameObject("EventSystem");
                go.AddComponent<EventSystem>();
                go.AddComponent<StandaloneInputModule>();
            }
        }
    }
}