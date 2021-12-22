using Microsoft.AspNetCore.Http;

namespace Loonfactory.Translate.Events;

/// <summary>
/// Provides access denied failure context information to handler providers.
/// </summary>
public class AccessDeniedContext : HandleRequestContext<RemoteTranslateOptions>
{
    /// <summary>
    /// Initializes a new instance of <see cref="AccessDeniedContext"/>.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The <see cref="TranslateScheme"/>.</param>
    /// <param name="options">The <see cref="RemoteTranslateOptions"/>.</param>
    public AccessDeniedContext(
        HttpContext context,
        TranslateScheme scheme,
        RemoteTranslateOptions options)
        : base(context, scheme, options)
    {
    }

    /// <summary>
    /// Gets or sets the endpoint path the user agent will be redirected to.
    /// By default, this property is set to <see cref="RemoteTranslateOptions.AccessDeniedPath"/>.
    /// </summary>
    public PathString AccessDeniedPath { get; set; }

    /// <summary>
    /// Additional state values for the translate session.
    /// </summary>
    public TranslateProperties? Properties { get; set; }

    /// <summary>
    /// Gets or sets the return URL that will be flowed up to the access denied page.
    /// If <see cref="ReturnUrlParameter"/> is not set, this property is not used.
    /// </summary>
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// Gets or sets the parameter name that will be used to flow the return URL.
    /// By default, this property is set to <see cref="RemoteTranslateOptions.ReturnUrlParameter"/>.
    /// </summary>
    public string ReturnUrlParameter { get; set; } = default!;
}