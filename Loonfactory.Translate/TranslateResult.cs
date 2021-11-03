// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to AuthenticateResult.cs of dotnet/aspnetcore.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Loonfactory.Translate
{
  /// <summary>
  /// Contains the result of an Transalte call
  /// </summary>
  public class TranslateResult
  {
    /// <summary>
    /// Creates a new <see cref="TranslateResult"/> instance.
    /// </summary>
    protected TranslateResult() { }

    /// <summary>
    /// If a ticket was produced, authenticate was successful.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Ticket), nameof(Principal), nameof(Properties))]
    public bool Succeeded => Ticket != null;

    /// <summary>
    /// The authentication ticket.
    /// </summary>
    public TranslateTicket? Ticket { get; protected set; }

    /// <summary>
    /// Gets the claims-principal with translated user identities.
    /// </summary>
    public ClaimsPrincipal? Principal => Ticket?.Principal;

    /// <summary>
    /// Additional state values for the translate session.
    /// </summary>
    public TranslateProperties? Properties { get; protected set; }

    /// <summary>
    /// Holds failure information from the translate.
    /// </summary>
    public Exception? Failure { get; protected set; }

    /// <summary>
    /// Indicates that there was no information returned for this translate scheme.
    /// </summary>
    public bool None { get; protected set; }

    /// <summary>
    /// Create a new deep copy of the result
    /// </summary>
    /// <returns>A copy of the result</returns>
    public TranslateResult Clone()
    {
      if (None)
      {
        return NoResult();
      }
      if (Failure != null)
      {
        return Fail(Failure, Properties?.Clone());
      }
      if (Succeeded)
      {
        return Success(Ticket!.Clone());
      }
      // This shouldn't happen
      throw new NotImplementedException();
    }

    /// <summary>
    /// Indicates that translation was successful.
    /// </summary>
    /// <param name="ticket">The ticket representing the translation result.</param>
    /// <returns>The result.</returns>
    public static TranslateResult Success(TranslateTicket ticket)
    {
      if (ticket == null)
      {
        throw new ArgumentNullException(nameof(ticket));
      }
      return new TranslateResult() { Ticket = ticket, Properties = ticket.Properties };
    }

    /// <summary>
    /// Indicates that there was no information returned for this translation scheme.
    /// </summary>
    /// <returns>The result.</returns>
    public static TranslateResult NoResult()
    {
      return new TranslateResult() { None = true };
    }

    /// <summary>
    /// Indicates that there was a failure during translation.
    /// </summary>
    /// <param name="failure">The failure exception.</param>
    /// <returns>The result.</returns>
    public static TranslateResult Fail(Exception failure)
    {
      return new TranslateResult() { Failure = failure };
    }

    /// <summary>
    /// Indicates that there was a failure during translate.
    /// </summary>
    /// <param name="failure">The failure exception.</param>
    /// <param name="properties">Additional state values for the translation session.</param>
    /// <returns>The result.</returns>
    public static TranslateResult Fail(Exception failure, TranslateProperties? properties)
    {
      return new TranslateResult() { Failure = failure, Properties = properties };
    }

    /// <summary>
    /// Indicates that there was a failure during translate.
    /// </summary>
    /// <param name="failureMessage">The failure message.</param>
    /// <returns>The result.</returns>
    public static TranslateResult Fail(string failureMessage)
        => Fail(new Exception(failureMessage));

    /// <summary>
    /// Indicates that there was a failure during translate.
    /// </summary>
    /// <param name="failureMessage">The failure message.</param>
    /// <param name="properties">Additional state values for the translation session.</param>
    /// <returns>The result.</returns>
    public static TranslateResult Fail(string failureMessage, TranslateProperties? properties)
        => Fail(new Exception(failureMessage), properties);
  }
}