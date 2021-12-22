using Microsoft.AspNetCore.Http;

namespace Loonfactory.Translate.Events;

/// <summary>
/// Provides context information to handler providers.
/// </summary>
public class TicketReceivedContext : RemoteTranslateContext<RemoteTranslateOptions>
{
    /// <summary>
    /// Initializes a new instance of <see cref="TicketReceivedContext"/>.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The <see cref="TranslateScheme"/>.</param>
    /// <param name="options">The <see cref="RemoteTranslateOptions"/>.</param>
    /// <param name="ticket">The received ticket.</param>
    public TicketReceivedContext(
        HttpContext context,
        TranslateScheme scheme,
        RemoteTranslateOptions options,
        TranslateTicket ticket)
        : base(context, scheme, options, ticket?.Properties)
        => Principal = ticket?.Principal;

    /// <summary>
    /// Gets or sets the URL to redirect to after signin.
    /// </summary>
    public string? ReturnUri { get; set; }
}
