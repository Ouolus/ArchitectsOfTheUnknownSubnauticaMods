using ArchitectsLibrary.CreatorKit.Packs;

namespace ArchitectsLibrary.CreatorKit
{
    public static class SNCreatorKit
    {
        public static void Entry()
        {
            PackGenerator templatePack = new PackGenerator(new PackData(new PackJson("Template Pack", "An example of a pack created with the Subnautica Creator Kit.", "An example pack.", "1.0", "Architects of the Unknown"), "TemplatePack"));
            templatePack.GeneratePack();
        }
    }
}
