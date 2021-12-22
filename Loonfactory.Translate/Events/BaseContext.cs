using Microsoft.AspNetCore.Http;
using System;

namespace Loonfactory.Translate.Events;

/// <summary>
/// Base class used by other context classes.
/// </summary>
public abstract class BaseContext<TOptions> where TOptions : TranslateSchemeOptions
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="scheme">The translate scheme.</param>
    /// <param name="options">The translate options associated with the scheme.</param>
    protected BaseContext(HttpContext context, TranslateScheme scheme, TOptions options)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (scheme == null)
        {
            throw new ArgumentNullException(nameof(scheme));
        }
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        HttpContext = context;
        Scheme = scheme;
        Options = options;
    }

    /// <summary>
    /// The translate scheme.
    /// </summary>
    public TranslateScheme Scheme { get; }

    /// <summary>
    /// Gets the translate options associated with the scheme.
    /// </summary>
    public TOptions Options { get; }

    /// <summary>
    /// The context.
    /// </summary>
    public HttpContext HttpContext { get; }

    /// <summary>
    /// The request.
    /// </summary>
    public HttpRequest Request => HttpContext.Request;

    /// <summary>
    /// The response.
    /// </summary>
    public HttpResponse Response => HttpContext.Response;
}