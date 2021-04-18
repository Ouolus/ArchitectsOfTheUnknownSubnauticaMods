using System.IO;
using UnityEngine;

namespace ArchitectsLibrary.CreatorKit.Packs
{
    internal class PackGenerator
    {
        private PackData data;

        public PackGenerator(PackData data)
        {
            this.data = data;
        }

        public void GeneratePack()
        {
            string path = PackHelper.GetPackPath(data.id);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string jsonPath = PackHelper.GetPackJsonPath(data.id);
            File.WriteAllText(jsonPath, JsonUtility.ToJson(data.json));
        }
    }
}
