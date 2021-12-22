// IAuthenticationService.cs

using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Loonfactory.Translate;


/// <summary>
/// Used to provide translate.
/// </summary>
public interface ITranslateService
{
    /// <summary>
    /// Authenticate for the specified authentication scheme.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The name of the authentication scheme.</param>
    /// <returns>The result.</returns>
    Task<TranslateResult> TranslateAsync(HttpContext context, string? scheme);

    /// <summary>
    /// Challenge the specified authentication scheme.
    /// An authentication challenge can be issued when an unauthenticated user requests an endpoint that requires authentication.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The name of the authentication scheme.</param>
    /// <param name="properties">The <see cref="TranslateProperties"/>.</param>
    /// <returns>A task.</returns>
    Task ChallengeAsync(HttpContext context, string? scheme, TranslateProperties? properties);

    /// <summary>
    /// Forbids the specified authentication scheme.
    /// Forbid is used when an authenticated user attempts to access a resource they are not permitted to access.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The name of the authentication scheme.</param>
    /// <param name="properties">The <see cref="TranslateProperties"/>.</param>
    /// <returns>A task.</returns>
    Task ForbidAsync(HttpContext context, string? scheme, TranslateProperties? properties);

    /// <summary>
    /// Sign a principal in for the specified authentication scheme.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The name of the authentication scheme.</param>
    /// <param name="principal">The <see cref="ClaimsPrincipal"/> to sign in.</param>
    /// <param name="properties">The <see cref="TranslateProperties"/>.</param>
    /// <returns>A task.</returns>
    Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, TranslateProperties? properties);

    /// <summary>
    /// Sign out the specified authentication scheme.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The name of the authentication scheme.</param>
    /// <param name="properties">The <see cref="TranslateProperties"/>.</param>
    /// <returns>A task.</returns>
    Task SignOutAsync(HttpContext context, string? scheme, TranslateProperties? properties);
}