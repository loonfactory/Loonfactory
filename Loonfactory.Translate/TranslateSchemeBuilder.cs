using System;
using System.Diagnostics.CodeAnalysis;

namespace Loonfactory.Translate;

/// <summary>
/// Used to build <see cref="TranslateScheme"/>s.
/// </summary>
public class TranslateSchemeBuilder
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name">The name of the scheme being built.</param>
    public TranslateSchemeBuilder(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets the name of the scheme being built.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets or sets the display name for the scheme being built.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="ITranslateHandler"/> type responsible for this scheme.
    /// </summary>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    public Type? HandlerType { get; set; }

    /// <summary>
    /// Builds the <see cref="TranslateScheme"/> instance.
    /// </summary>
    /// <returns>The <see cref="TranslateScheme"/>.</returns>
    public TranslateScheme Build()
    {
        if (HandlerType is null)
        {
            throw new InvalidOperationException($"{nameof(HandlerType)} must be configured to build an {nameof(TranslateScheme)}.");
        }

        return new TranslateScheme(Name, DisplayName, HandlerType);
    }
}
