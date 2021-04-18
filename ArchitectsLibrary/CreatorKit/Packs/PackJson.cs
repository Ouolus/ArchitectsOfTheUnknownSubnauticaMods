using System;

namespace ArchitectsLibrary.CreatorKit.Packs
{
    [Serializable]
    internal struct PackJson
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
