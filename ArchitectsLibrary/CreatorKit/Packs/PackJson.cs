using System;

namespace CreatorKit.Packs
{
    [Serializable]
    public struct PackJson
    {
        public string DisplayName;
        public string Description;
        public string ShortDescription;
        public string Version;
        public string Author;

        public PackJson(string displayName, string description, string shortDescription, string version, string author)
        {
            DisplayName = displayName;
            Description = description;
            ShortDescription = shortDescription;
            Version = version;
            Author = author;
        }
    }
}
