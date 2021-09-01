// ReSharper disable CheckNamespace

using System;
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

    /// <summary>
    /// Checks whether or not the value starts or ends with in the current string.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="value">The value to check.</param>
    /// <param name="comparisonType">One of the enumeration values that determines how this string and value are compared.</param>
    /// <returns><see langword="true"/> if it does, otherwise <see langword="false"/>.</returns>
    public static bool StartsOrEndsWith(this string @this, string value, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
    {
        return @this.StartsWith(value, comparisonType) || @this.EndsWith(value, comparisonType);
    }
}