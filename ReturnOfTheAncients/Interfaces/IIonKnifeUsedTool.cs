using RotA.Mono.Equipment;

namespace RotA.Interfaces
{
    public interface IIonKnifeUsedTool
    {
        /// <summary>
        /// Defines whether or not the Ion Knife should play the use animation in the current frame.
        /// </summary>
        /// <param name="ionKnife">the Ion Knife instance</param>
        /// <returns>true if you want it to, otherwise false.</returns>
        bool GetUsedToolThisFrame(IonKnife ionKnife);
    }
}