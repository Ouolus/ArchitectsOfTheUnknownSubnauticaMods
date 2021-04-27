using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CreatorKit.Utility
{
    public static class Utils
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

        /// <summary>
        /// Adds a message to the ErrorMessage and the log.
        /// </summary>
        /// <param name="message"></param>
        public static void PrintErrorMessage(string message)
        {
            UnityEngine.Debug.Log(message);
            ErrorMessage.AddMessage(message);
        }

        /// <summary>
        /// Reads all the text from a file and then closes the stream.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadText(string path)
        {
            if (!File.Exists(path))
            {
                ErrorMessage.AddMessage("File does not exist at path " + path);
                return "";
            }
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Write all text to a file and then closes the stream.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="text">The text to write in the file.</param>
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

        /// <summary>
        /// Loads an uncompressed sprite from a file.
        /// </summary>
        /// <param name="iconPath"></param>
        /// <param name="defaultSprite"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Loads an uncompressed PNG from a file (other file types may work too).
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static Texture2D LoadPNG(string imagePath)
        {
            return SMLHelper.V2.Utility.ImageUtils.LoadTextureFromFile(imagePath);
        }

        /// <summary>
        /// EventSystems are NECESSARY for UI elements to work correctly.
        /// </summary>
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

        /// <summary>
        /// Instantiates a UI element, sets its parent, and returns the spawned object.
        /// </summary>
        /// <returns></returns>
        public static GameObject InstantiateUIChild(GameObject prefab, Transform parent)
        {
            GameObject instantiated = GameObject.Instantiate(prefab);
            instantiated.GetComponent<RectTransform>().SetParent(parent, false);
            return instantiated;
        }
    }
}