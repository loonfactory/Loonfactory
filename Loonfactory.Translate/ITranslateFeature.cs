// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to IAuthenticationFeature.cs of dotnet/aspnetcore.

using Microsoft.AspNetCore.Http;

namespace Loonfactory.Translate
{
    /// <summary>
    /// Used to capture path info so redirects can be computed properly within an app.Map().
    /// </summary>
    internal interface ITranslateFeature
    {
        /// <summary>
        /// The original path base.
        /// </summary>
        PathString OriginalPathBase { get; set; }

        /// <summary>
        /// The original path.
        /// </summary>
        PathString OriginalPath { get; set; }
    }
}