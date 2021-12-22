using System;

namespace Loonfactory.Translate;

/// <summary>
/// Contains the result of an Translate call
/// </summary>
public class HandleRequestResult : TranslateResult
{
    /// <summary>
    /// Indicates that stage of translate was directly handled by
    /// user intervention and no further processing should be attempted.
    /// </summary>
    public bool Handled { get; private set; }

    /// <summary>
    /// Indicates that the default translate logic should be
    /// skipped and that the rest of the pipeline should be invoked.
    /// </summary>
    public bool Skipped { get; private set; }

    /// <summary>
    /// Indicates that translate was successful.
    /// </summary>
    /// <param name="ticket">The ticket representing the translate result.</param>
    /// <returns>The result.</returns>
    public static new HandleRequestResult Success(TranslateTicket ticket)
    {
        if (ticket == null)
        {
            throw new ArgumentNullException(nameof(ticket));
        }
        return new HandleRequestResult() { Ticket = ticket, Properties = ticket.Properties };
    }

    /// <summary>
    /// Indicates that there was a failure during translate.
    /// </summary>
    /// <param name="failure">The failure exception.</param>
    /// <returns>The result.</returns>
    public static new HandleRequestResult Fail(Exception failure)
    {
        return new HandleRequestResult() { Failure = failure };
    }

    /// <summary>
    /// Indicates that there was a failure during translate.
    /// </summary>
    /// <param name="failure">The failure exception.</param>
    /// <param name="properties">Additional state values for the translate session.</param>
    /// <returns>The result.</returns>
    public static new HandleRequestResult Fail(Exception failure, TranslateProperties? properties)
    {
        return new HandleRequestResult() { Failure = failure, Properties = properties };
    }

    /// <summary>
    /// Indicates that there was a failure during translate.
    /// </summary>
    /// <param name="failureMessage">The failure message.</param>
    /// <returns>The result.</returns>
    public static new HandleRequestResult Fail(string failureMessage)
        => Fail(new Exception(failureMessage));

    /// <summary>
    /// Indicates that there was a failure during translate.
    /// </summary>
    /// <param name="failureMessage">The failure message.</param>
    /// <param name="properties">Additional state values for the translate session.</param>
    /// <returns>The result.</returns>
    public static new HandleRequestResult Fail(string failureMessage, TranslateProperties? properties)
        => Fail(new Exception(failureMessage), properties);

    /// <summary>
    /// Discontinue all processing for this request and return to the client.
    /// The caller is responsible for generating the full response.
    /// </summary>
    /// <returns>The result.</returns>
    public static HandleRequestResult Handle()
    {
        return new HandleRequestResult() { Handled = true };
    }

    /// <summary>
    /// Discontinue processing the request in the current handler.
    /// </summary>
    /// <returns>The result.</returns>
    public static HandleRequestResult SkipHandler()
    {
        return new HandleRequestResult() { Skipped = true };
    }

    /// <summary>
    /// Indicates that there were no results produced during translate.
    /// </summary>
    /// <returns>The result.</returns>
    public static new HandleRequestResult NoResult()
    {
        return new HandleRequestResult() { None = true };
    }
}
