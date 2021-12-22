using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Loonfactory.Translate.Events;

/// <summary>
/// Base context for remote translate.
/// </summary>
public abstract class RemoteTranslateContext<TOptions> : HandleRequestContext<TOptions> where TOptions : TranslateSchemeOptions
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="scheme">The translate scheme.</param>
    /// <param name="options">The translate options associated with the scheme.</param>
    /// <param name="properties">The translate properties.</param>
    protected RemoteTranslateContext(
        HttpContext context,
        TranslateScheme scheme,
        TOptions options,
        TranslateProperties? properties)
        : base(context, scheme, options)
        => Properties = properties ?? new TranslateProperties();

    /// <summary>
    /// Gets the <see cref="ClaimsPrincipal"/> containing the user claims.
    /// </summary>
    public ClaimsPrincipal? Principal { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="TranslateProperties"/>.
    /// </summary>
    public virtual TranslateProperties? Properties { get; set; }

    /// <summary>
    /// Calls success creating a ticket with the <see cref="Principal"/> and <see cref="Properties"/>.
    /// </summary>
    public void Success() => Result = HandleRequestResult.Success(new TranslateTicket(Principal!, Properties, Scheme.Name));

    /// <summary>
    /// Indicates that translate failed.
    /// </summary>
    /// <param name="failure">The exception associated with the failure.</param>
    public void Fail(Exception failure) => Result = HandleRequestResult.Fail(failure);

    /// <summary>
    /// Indicates that translate failed.
    /// </summary>
    /// <param name="failureMessage">The exception associated with the failure.</param>
    public void Fail(string failureMessage) => Result = HandleRequestResult.Fail(failureMessage);
}
