using ArchitectsLibrary.CreatorKit.Packs;

namespace ArchitectsLibrary.CreatorKit
{
    /// <summary>
    /// The main class that sets up all behaviour for the Subnautica Creation Kit.
    /// </summary>
    public static class SNCreatorKit
    {
        /// <summary>
        /// The first method that runs in the Creator Kit that sets up everything.
        /// </summary>
        public static void Entry()
        {
            PackGenerator templatePack = new PackGenerator(new PackData(new PackJson("Template Pack", "An example of a pack created with the Subnautica Creator Kit.", "An example pack.", "1.0", "Architects of the Unknown"), "TemplatePack"));
            templatePack.GeneratePack();

            UI.UIAssets.LoadAssetBundle();
        }
    }
}
