// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)
//
// This document was created by referring to AuthenticateTicket.cs of dotnet/aspnetcore.

using System;
using System.Security.Claims;

namespace Loonfactory.Translate
{
  /// <summary>
  /// Contains user identity information as well as additional translate state.
  /// </summary>
  public class TranslateTicket
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateTicket"/> class
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal"/> that represents the translate user.</param>
    /// <param name="properties">additional properties that can be consumed by the user or runtime.</param>
    /// <param name="translateScheme">the authentication scheme that was responsible for this ticket.</param>
    public TranslateTicket(ClaimsPrincipal principal, TranslateProperties? properties, string translateScheme)
    {
      if (principal == null)
      {
        throw new ArgumentNullException(nameof(principal));
      }

      TranslateScheme = translateScheme;
      Principal = principal;
      Properties = properties ?? new TranslateProperties();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TranslateTicket"/> class
    /// </summary>
    /// <param name="principal">the <see cref="ClaimsPrincipal"/> that represents the translate user.</param>
    /// <param name="translateScheme">the translate scheme that was responsible for this ticket.</param>
    public TranslateTicket(ClaimsPrincipal principal, string translateScheme)
        : this(principal, properties: null, translateScheme: translateScheme)
    { }

    /// <summary>
    /// Gets the translate scheme that was responsible for this ticket.
    /// </summary>
    public string TranslateScheme { get; }

    /// <summary>
    /// Gets the claims-principal with translated user identities.
    /// </summary>
    public ClaimsPrincipal Principal { get; }

    /// <summary>
    /// Additional state values for the translate session.
    /// </summary>
    public TranslateProperties Properties { get; }

    /// <summary>
    /// Returns a copy of the ticket.
    /// </summary>
    /// <remarks>
    /// The method clones the <see cref="Principal"/> by calling <see cref="ClaimsIdentity.Clone"/> on each of the <see cref="ClaimsPrincipal.Identities"/>.
    /// </remarks>
    /// <returns>A copy of the ticket</returns>
    public TranslateTicket Clone()
    {
      var principal = new ClaimsPrincipal();
      foreach (var identity in Principal.Identities)
      {
        principal.AddIdentity(identity.Clone());
      }
      return new TranslateTicket(principal, Properties.Clone(), TranslateScheme);
    }
  }
}
