namespace ServiceXpert.Application.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public static class EnumExtensions
{
    /// <summary>
    /// Gets all enum values as IEnumerable.
    /// </summary>
    public static IEnumerable<TEnum> GetValues<TEnum>() where TEnum : struct, Enum => Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

    /// <summary>
    /// Gets the string name of the enum value.
    /// </summary>
    public static string GetName<TEnum>(this TEnum value) where TEnum : struct, Enum => Enum.GetName(typeof(TEnum), value) ?? string.Empty;

    /// <summary>
    /// Gets the [Description] attribute text if defined, or the name otherwise.
    /// </summary>
    public static string GetDescription<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var member = typeof(TEnum).GetMember(value.ToString()).FirstOrDefault();
        var attribute = member?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    /// <summary>
    /// Parses a string into an enum value (case-insensitive by default).
    /// </summary>
    public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, Enum => Enum.TryParse(value, ignoreCase, out TEnum result) ? result : throw new ArgumentException($"Invalid cast of string '{value}' to enum {typeof(TEnum).Name}.");

    /// <summary>
    /// Gets the integer value of an enum.
    /// </summary>
    public static int ToInt<TEnum>(this TEnum value) where TEnum : struct, Enum => Convert.ToInt32(value)
}
