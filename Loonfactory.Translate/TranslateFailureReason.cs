// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to AuthorizationFailureReason.cs of dotnet/aspnetcore.

namespace Loonfactory.Translate;

/// <summary>
/// Encapsulates a reason why translate failed.
/// </summary>
public class TranslateFailureReason
{
    /// <summary>
    /// Creates a new failure reason.
    /// </summary>
    /// <param name="handler">The handler responsible for this failure reason.</param>
    /// <param name="message">The message describing the failure.</param>
    public TranslateFailureReason(ITranslateHandler handler, string message)
    {
        Handler = handler;
        Message = message;
    }

    /// <summary>
    /// A message describing the failure reason.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// The <see cref="ITranslateHandler"/> responsible for this failure reason.
    /// </summary>
    public ITranslateHandler Handler { get; }
}
