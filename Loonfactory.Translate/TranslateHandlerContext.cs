using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Loonfactory.Translate;

/// <summary>
/// Contains translate information used by <see cref="ITranslateHandler"/>.
/// </summary>
public class TranslateHandlerContext
{
    private readonly HashSet<ITranslateRequirement> _pendingRequirements;
    private List<TranslateFailureReason>? _failedReasons;
    private bool _failCalled;
    private bool _succeedCalled;

    /// <summary>
    /// Creates a new instance of <see cref="TranslateHandlerContext"/>.
    /// </summary>
    /// <param name="requirements">A collection of all the <see cref="ITranslateRequirement"/> for the current authorization action.</param>
    /// <param name="user">A <see cref="ClaimsPrincipal"/> representing the current user.</param>
    /// <param name="resource">An optional resource to evaluate the <paramref name="requirements"/> against.</param>
    public TranslateHandlerContext(
        IEnumerable<ITranslateRequirement> requirements,
        ClaimsPrincipal user,
        object? resource)
    {
        if (requirements == null)
        {
            throw new ArgumentNullException(nameof(requirements));
        }

        Requirements = requirements;
        User = user;
        Resource = resource;
        _pendingRequirements = new HashSet<ITranslateRequirement>(requirements);
    }

    /// <summary>
    /// The collection of all the <see cref="ITranslateRequirement"/> for the current translate action.
    /// </summary>
    public virtual IEnumerable<ITranslateRequirement> Requirements { get; }

    /// <summary>
    /// The <see cref="ClaimsPrincipal"/> representing the current user.
    /// </summary>
    public virtual ClaimsPrincipal User { get; }

    /// <summary>
    /// The optional resource to evaluate the <see cref="Requirements"/> against.
    /// </summary>
    public virtual object? Resource { get; }

    /// <summary>
    /// Gets the requirements that have not yet been marked as succeeded.
    /// </summary>
    public virtual IEnumerable<ITranslateRequirement> PendingRequirements { get { return _pendingRequirements; } }

    /// <summary>
    /// Gets the reasons why authorization has failed.
    /// </summary>
    public virtual IEnumerable<TranslateFailureReason> FailureReasons
        => (IEnumerable<TranslateFailureReason>?)_failedReasons ?? Array.Empty<TranslateFailureReason>();

    /// <summary>
    /// Flag indicating whether the current authorization processing has failed due to Fail being called.
    /// </summary>
    public virtual bool HasFailed { get { return _failCalled; } }

    /// <summary>
    /// Flag indicating whether the current translate processing has succeeded.
    /// </summary>
    public virtual bool HasSucceeded
    {
        get
        {
            return !_failCalled && _succeedCalled && !PendingRequirements.Any();
        }
    }

    /// <summary>
    /// Called to indicate <see cref="HasSucceeded"/> will
    /// never return true, even if all requirements are met.
    /// </summary>
    public virtual void Fail()
    {
        _failCalled = true;
    }

    /// <summary>
    /// Called to indicate <see cref="HasSucceeded"/> will
    /// never return true, even if all requirements are met.
    /// </summary>
    /// <param name="reason">Optional <see cref="TranslateFailureReason"/> for why translate failed.</param>
    public virtual void Fail(TranslateFailureReason reason)
    {
        Fail();
        if (reason != null)
        {
            if (_failedReasons == null)
            {
                _failedReasons = new List<TranslateFailureReason>();
            }

            _failedReasons.Add(reason);
        }
    }

    /// <summary>
    /// Called to mark the specified <paramref name="requirement"/> as being
    /// successfully evaluated.
    /// </summary>
    /// <param name="requirement">The requirement whose evaluation has succeeded.</param>
    public virtual void Succeed(ITranslateRequirement requirement)
    {
        _succeedCalled = true;
        _pendingRequirements.Remove(requirement);
    }

}
