namespace Markdown;

public class TagReplacementSpecification
{
    public readonly string InputOpeningTag;
    public readonly string InputClosingTag;
    public readonly string OutputOpeningTag;
    public readonly string OutputClosingTag;
    public readonly HashSet<string> InvalidSubstringsInMarkup;

    public TagReplacementSpecification(string inputOpeningTag, string outputOpeningTag,
        string inputClosingTag, string outputClosingTag,
        IEnumerable<string> invalidSubstringsInMarkup)
    {
        InputOpeningTag = inputOpeningTag ?? throw new ArgumentNullException(nameof(inputOpeningTag));
        InputClosingTag = inputClosingTag ?? throw new ArgumentNullException(nameof(inputClosingTag));
        OutputOpeningTag = inputOpeningTag ?? throw new ArgumentNullException(nameof(inputOpeningTag));
        OutputClosingTag = outputClosingTag ?? throw new ArgumentNullException(nameof(outputClosingTag));
        InvalidSubstringsInMarkup = invalidSubstringsInMarkup.ToHashSet();
    }
}