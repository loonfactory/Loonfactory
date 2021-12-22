// AuthenticationToken.cs

namespace Loonfactory.Translate;

/// <summary>
/// Name/Value representing a token.
/// </summary>
public class TranslateToken
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Value.
    /// </summary>
    public string Value { get; set; } = default!;
}
