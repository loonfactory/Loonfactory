// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to Microsoft.AspNetCore.Authentication.Resources of dotnet/aspnetcore.

using System.Globalization;
using System.Resources;

namespace Loonfactory.Translate;
internal static partial class Resources
{
    private static ResourceManager s_resourceManager = default!;
    internal static ResourceManager ResourceManager => s_resourceManager ??= new ResourceManager(typeof(Resources));
    internal static CultureInfo Culture { get; set; } = CultureInfo.CurrentCulture;

    internal static string GetResourceString(string resourceKey, string? defaultValue = null) => ResourceManager.GetString(resourceKey, Culture)!;

    private static string GetResourceString(string resourceKey, string[] formatterNames)
    {
        var value = GetResourceString(resourceKey);
        if (formatterNames != null)
        {
            for (var i = 0; i < formatterNames.Length; i++)
            {
                value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
            }
        }
        return value;
    }

    /// <summary>The default data protection provider may only be used when the IApplicationBuilder.Properties contains an appropriate 'host.AppName' key.</summary>
    internal static string @Exception_UnableToFindServices => GetResourceString("Exception_UnableToFindServices");
    internal static string @Exception_RemoteTranslateSchemeCannotBeSelf => GetResourceString("Exception_RemoteTranslateSchemeCannotBeSelf");

    internal static string FormatException_OptionMustBeProvided(object p0)
       => string.Format(Culture, GetResourceString("Exception_OptionMustBeProvided"), p0);
}
