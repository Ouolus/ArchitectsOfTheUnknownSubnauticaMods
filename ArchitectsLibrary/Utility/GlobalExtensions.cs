// ReSharper disable CheckNamespace
using UnityEngine;

/// <summary>
/// A collection of extensions that are on the global namespace.
/// </summary>
public static class GlobalExtensions
{
    /// <summary>
    /// Adds a message and increases the life of it, instead of spamming it.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="message">the message text</param>
    public static void AddHint(this ErrorMessage @this, string message)
    {
        var msg = @this.GetExistingMessage(message);
        if (msg is null)
            ErrorMessage.AddMessage(message);
        else if (msg.timeEnd < Time.time + 2)
            msg.timeEnd += Time.deltaTime;
    }
}