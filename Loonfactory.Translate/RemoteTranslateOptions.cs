// RemoteAuthenticationOptions.cs

using Loonfactory.Translate.Events;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace Loonfactory.Translate;

/// <summary>
/// Contains the options used by the <see cref="RemoteTranslateHandler{T}"/>.
/// </summary>
public class RemoteTranslateOptions : TranslateSchemeOptions
{
    private const string CorrelationPrefix = ".Loonfactory.Correlation.";

    private CookieBuilder _correlationCookieBuilder;

    /// <summary>
    /// Initializes a new <see cref="RemoteTranslateOptions"/>.
    /// </summary>
    public RemoteTranslateOptions()
    {
        _correlationCookieBuilder = new CorrelationCookieBuilder(this)
        {
            Name = CorrelationPrefix,
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            SecurePolicy = CookieSecurePolicy.SameAsRequest,
            IsEssential = true,
        };
    }

    /// <summary>
    /// Checks that the options are valid for a specific scheme
    /// </summary>
    /// <param name="scheme">The scheme being validated.</param>
    public override void Validate(string scheme)
    {
        base.Validate(scheme);
        if (string.Equals(scheme, SignInScheme, StringComparison.Ordinal))
        {
            throw new InvalidOperationException(Resources.Exception_RemoteTranslateSchemeCannotBeSelf);
        }
    }

    /// <summary>
    /// Check that the options are valid.  Should throw an exception if things are not ok.
    /// </summary>
    public override void Validate()
    {
        base.Validate();
        if (CallbackPath == null || !CallbackPath.HasValue)
        {
            throw new ArgumentException(Resources.FormatException_OptionMustBeProvided(nameof(CallbackPath)), nameof(CallbackPath));
        }
    }

    /// <summary>
    /// Gets or sets timeout value in milliseconds for back channel communications with the remote identity provider.
    /// </summary>
    /// <value>
    /// The back channel timeout.
    /// </value>
    public TimeSpan BackchannelTimeout { get; set; } = TimeSpan.FromSeconds(60);

    /// <summary>
    /// The HttpMessageHandler used to communicate with remote identity provider.
    /// This cannot be set at the same time as BackchannelCertificateValidator unless the value
    /// can be downcast to a WebRequestHandler.
    /// </summary>
    public HttpMessageHandler? BackchannelHttpHandler { get; set; }

    /// <summary>
    /// Used to communicate with the remote identity provider.
    /// </summary>
    public HttpClient Backchannel { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type used to secure data.
    /// </summary>
    public IDataProtectionProvider? DataProtectionProvider { get; set; }

    /// <summary>
    /// The request path within the application's base path where the user-agent will be returned.
    /// The middleware will process this request when it arrives.
    /// </summary>
    public PathString CallbackPath { get; set; }

    /// <summary>
    /// Gets or sets the optional path the user agent is redirected to if the user
    /// doesn't approve the authorization demand requested by the remote server.
    /// This property is not set by default. In this case, an exception is thrown
    /// if an access_denied response is returned by the remote authorization server.
    /// </summary>
    public PathString AccessDeniedPath { get; set; }

    /// <summary>
    /// Gets or sets the name of the parameter used to convey the original location
    /// of the user before the remote challenge was triggered up to the access denied page.
    /// This property is only used when the <see cref="AccessDeniedPath"/> is explicitly specified.
    /// </summary>
    // Note: this deliberately matches the default parameter name used by the cookie handler.
    public string ReturnUrlParameter { get; set; } = "ReturnUrl";

    /// <summary>
    /// Gets or sets the translate scheme corresponding to the middleware
    /// responsible of persisting user's identity after a successful translate.
    /// This value typically corresponds to a cookie middleware registered in the Startup class.
    /// When omitted, <see cref="TranslateOptions.DefaultSignInScheme"/> is used as a fallback value.
    /// </summary>
    public string? SignInScheme { get; set; }

    /// <summary>
    /// Gets or sets the time limit for completing the authentication flow (15 minutes by default).
    /// </summary>
    public TimeSpan RemoteAuthenticationTimeout { get; set; } = TimeSpan.FromMinutes(15);

    /// <summary>
    /// Gets or sets a value that allows subscribing to remote translate events.
    /// </summary>
    public new RemoteTranslateEvents Events
    {
        get => (RemoteTranslateEvents)base.Events!;
        set => base.Events = value;
    }

    /// <summary>
    /// Defines whether access and refresh tokens should be stored in the
    /// <see cref="TranslateProperties"/> after a successful authorization.
    /// This property is set to <c>false</c> by default to reduce
    /// the size of the final authentication cookie.
    /// </summary>
    public bool SaveTokens { get; set; }

    /// <summary>
    /// Determines the settings used to create the correlation cookie before the
    /// cookie gets added to the response.
    /// </summary>
    public CookieBuilder CorrelationCookie
    {
        get => _correlationCookieBuilder;
        set => _correlationCookieBuilder = value ?? throw new ArgumentNullException(nameof(value));
    }

    private class CorrelationCookieBuilder : RequestPathBaseCookieBuilder
    {
        private readonly RemoteTranslateOptions _options;

        public CorrelationCookieBuilder(RemoteTranslateOptions remoteAuthenticationOptions)
        {
            _options = remoteAuthenticationOptions;
        }

        protected override string AdditionalPath => _options.CallbackPath;

        public override CookieOptions Build(HttpContext context, DateTimeOffset expiresFrom)
        {
            var cookieOptions = base.Build(context, expiresFrom);

            if (!Expiration.HasValue || !cookieOptions.Expires.HasValue)
            {
                cookieOptions.Expires = expiresFrom.Add(_options.RemoteAuthenticationTimeout);
            }

            return cookieOptions;
        }
    }

}
