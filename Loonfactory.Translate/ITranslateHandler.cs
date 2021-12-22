// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to IAuthenticationHandler.cs of dotnet/aspnetcore.

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Loonfactory.Translate;

/// <summary>
/// Created per request to handle translate for a particular scheme.
/// </summary>
public interface ITranslateHandler
{
    /// <summary>
    /// Initialize the transalted handler. The handler should initialize anything it needs from the request and scheme as part of this method.
    /// </summary>
    /// <param name="scheme">The <see cref="TranslateScheme"/> scheme.</param>
    /// <param name="context">The <see cref="HttpContext"/> context.</param>
    Task InitializeAsync(TranslateScheme scheme, HttpContext context);

    /// <summary>
    /// Translate the current request.
    /// </summary>
    /// <returns>The <see cref="TranslateResult"/> result.</returns>
    Task<TranslateResult> TranslateAsync();

    /// <summary>
    /// Challenge the current request.
    /// </summary>
    /// <param name="properties">The <see cref="TranslateProperties"/> that contains the extra meta-data arriving with the translate.</param>
    Task ChallengeAsync(TranslateProperties? properties);

    /// <summary>
    /// Forbid the current request.
    /// </summary>
    /// <param name="properties">The <see cref="TranslateProperties"/> that contains the extra meta-data arriving with the translate.</param>
    Task ForbidAsync(TranslateProperties? properties);
}
