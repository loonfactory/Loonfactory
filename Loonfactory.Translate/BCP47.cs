// (c) 2021 loonfactory
// This code is licensed under MIT license (see LICENSE.txt for details)


namespace Loonfactory.Translate;

/// <summary>
/// BCP 47 Language Tags is the Internet Best Current Practices (BCP) for language tags.
/// The purpose of these language tags is to establish codes to help identify languages both spoken and written.
/// A language tag is composed of a sequence of one or more subtags such as language, region, variant and script subtags.
/// When a language tag is comprised of more than one subtag, the subtag values are separated by the "-" character.
/// 
/// This comment was taken from the <see href="https://www.techonthenet.com/js/language_tags.php">language_tags documentation</see>.
/// </summary>
public record BCP47
{
    /// <summary>
    /// Language Tag
    /// </summary>
    public string Tag { get; init; } = default!;
    /// <summary>
    /// Language Name
    /// </summary>
    public string? Language { get; init; }
    /// <summary>
    /// Language Region
    /// </summary>
    public string? Region { get; init; }
    /// <summary>
    /// Language Description
    /// </summary>
    public string? Description { get; init; }
}
