using Microsoft.AspNetCore.Http;
using System;

namespace Loonfactory.Translate.Events;

/// <summary>
/// Provides failure context information to handler providers.
/// </summary>
public class RemoteFailureContext : HandleRequestContext<RemoteTranslateOptions>
{
    /// <summary>
    /// Initializes a new instance of <see cref="RemoteFailureContext"/>.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The <see cref="TranslateScheme"/>.</param>
    /// <param name="options">The <see cref="RemoteTranslateOptions"/>.</param>
    /// <param name="failure">User friendly error message for the error.</param>
    public RemoteFailureContext(
        HttpContext context,
        TranslateScheme scheme,
        RemoteTranslateOptions options,
        Exception failure)
        : base(context, scheme, options)
    {
        Failure = failure;
    }

    /// <summary>
    /// User friendly error message for the error.
    /// </summary>
    public Exception? Failure { get; set; }

    /// <summary>
    /// Additional state values for the translate session.
    /// </summary>
    public TranslateProperties? Properties { get; set; }
}
