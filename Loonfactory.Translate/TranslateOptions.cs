// AuthenticationOptions.cs

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Loonfactory.Translate;

/// <summary>
/// Options to configure authentication.
/// </summary>
public class TranslateOptions
{
    private readonly IList<TranslateSchemeBuilder> _schemes = new List<TranslateSchemeBuilder>();

    /// <summary>
    /// Returns the schemes in the order they were added (important for request handling priority)
    /// </summary>
    public IEnumerable<TranslateSchemeBuilder> Schemes => _schemes;

    /// <summary>
    /// Maps schemes by name.
    /// </summary>
    public IDictionary<string, TranslateSchemeBuilder> SchemeMap { get; } = new Dictionary<string, TranslateSchemeBuilder>(StringComparer.Ordinal);

    /// <summary>
    /// Adds an <see cref="TranslateScheme"/>.
    /// </summary>
    /// <param name="name">The name of the scheme being added.</param>
    /// <param name="configureBuilder">Configures the scheme.</param>
    public void AddScheme(string name, Action<TranslateSchemeBuilder> configureBuilder)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (configureBuilder == null)
        {
            throw new ArgumentNullException(nameof(configureBuilder));
        }
        if (SchemeMap.ContainsKey(name))
        {
            throw new InvalidOperationException("Scheme already exists: " + name);
        }

        var builder = new TranslateSchemeBuilder(name);
        configureBuilder(builder);
        _schemes.Add(builder);
        SchemeMap[name] = builder;
    }

    /// <summary>
    /// Adds an <see cref="TranslateScheme"/>.
    /// </summary>
    /// <typeparam name="THandler">The <see cref="ITranslateRequestHandler"/> responsible for the scheme.</typeparam>
    /// <param name="name">The name of the scheme being added.</param>
    /// <param name="displayName">The display name for the scheme.</param>
    public void AddScheme<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THandler>(string name, string? displayName) where THandler : ITranslateHandler
        => AddScheme(name, b =>
        {
            b.DisplayName = displayName;
            b.HandlerType = typeof(THandler);
        });

    /// <summary>
    /// Used as the fallback default scheme for all the other defaults.
    /// </summary>
    public string? DefaultScheme { get; set; }

    /// <summary>
    /// Used as the default scheme by <see cref="ITranslateService.AuthenticateAsync(HttpContext, string)"/>.
    /// </summary>
    public string? DefaultAuthenticateScheme { get; set; }

    /// <summary>
    /// Used as the default scheme by <see cref="IAuthenticationService.SignInAsync(HttpContext, string, System.Security.Claims.ClaimsPrincipal, AuthenticationProperties)"/>.
    /// </summary>
    public string? DefaultSignInScheme { get; set; }

    /// <summary>
    /// Used as the default scheme by <see cref="IAuthenticationService.SignOutAsync(HttpContext, string, AuthenticationProperties)"/>.
    /// </summary>
    public string? DefaultSignOutScheme { get; set; }

    /// <summary>
    /// Used as the default scheme by <see cref="IAuthenticationService.ChallengeAsync(HttpContext, string, AuthenticationProperties)"/>.
    /// </summary>
    public string? DefaultChallengeScheme { get; set; }

    /// <summary>
    /// Used as the default scheme by <see cref="IAuthenticationService.ForbidAsync(HttpContext, string, AuthenticationProperties)"/>.
    /// </summary>
    public string? DefaultForbidScheme { get; set; }

    /// <summary>
    /// If true, SignIn should throw if attempted with a user is not authenticated.
    /// A user is considered authenticated if <see cref="ClaimsIdentity.IsAuthenticated"/> returns <see langword="true" /> for the <see cref="ClaimsPrincipal"/> associated with the HTTP request.
    /// </summary>
    public bool RequireAuthenticatedSignIn { get; set; } = true;

}
