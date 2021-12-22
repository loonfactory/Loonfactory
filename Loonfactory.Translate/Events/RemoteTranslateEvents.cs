using System;
using System.Threading.Tasks;

namespace Loonfactory.Translate.Events;

/// <summary>
/// Allows subscribing to events raised during remote translate.
/// </summary>
public class RemoteTranslateEvents
{
    /// <summary>
    /// Invoked when an access denied error was returned by the remote server.
    /// </summary>
    public Func<AccessDeniedContext, Task> OnAccessDenied { get; set; } = context => Task.CompletedTask;

    /// <summary>
    /// Invoked when there is a remote failure.
    /// </summary>
    public Func<RemoteFailureContext, Task> OnRemoteFailure { get; set; } = context => Task.CompletedTask;

    /// <summary>
    /// Invoked after the remote ticket has been received.
    /// </summary>
    public Func<TicketReceivedContext, Task> OnTicketReceived { get; set; } = context => Task.CompletedTask;

    /// <summary>
    /// Invoked when an access denied error was returned by the remote server.
    /// </summary>
    public virtual Task AccessDenied(AccessDeniedContext context) => OnAccessDenied(context);

    /// <summary>
    /// Invoked when there is a remote failure.
    /// </summary>
    public virtual Task RemoteFailure(RemoteFailureContext context) => OnRemoteFailure(context);

    /// <summary>
    /// Invoked after the remote ticket has been received.
    /// </summary>
    public virtual Task TicketReceived(TicketReceivedContext context) => OnTicketReceived(context);
}
