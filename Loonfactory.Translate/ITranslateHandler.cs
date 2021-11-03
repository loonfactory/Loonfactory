// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to IAuthenticateHandler.cs of dotnet/aspnetcore.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Loonfactory.Translate
{
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
  }
}
