using Microsoft.AspNetCore.Http;

namespace Loonfactory.Translate.Events;

/// <summary>
/// Base context type for handling authentication request.
/// </summary>
/// <typeparam name="TOptions"></typeparam>
public class HandleRequestContext<TOptions> : BaseContext<TOptions> where TOptions : TranslateSchemeOptions
{
    /// <summary>
    /// Initializes a new instance of <see cref="HandleRequestContext{TOptions}"/>.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/>.</param>
    /// <param name="scheme">The <see cref="TranslateScheme"/>.</param>
    /// <param name="options">The authentication scheme options.</param>
    protected HandleRequestContext(
        HttpContext context,
        TranslateScheme scheme,
        TOptions options)
        : base(context, scheme, options) { }

    /// <summary>
    /// The <see cref="HandleRequestResult"/> which is used by the handler.
    /// </summary>
    public HandleRequestResult Result { get; protected set; } = default!;

    /// <summary>
    /// Discontinue all processing for this request and return to the client.
    /// The caller is responsible for generating the full response.
    /// </summary>
    public void HandleResponse() => Result = HandleRequestResult.Handle();

    /// <summary>
    /// Discontinue processing the request in the current handler.
    /// </summary>
    public void SkipHandler() => Result = HandleRequestResult.SkipHandler();
}
