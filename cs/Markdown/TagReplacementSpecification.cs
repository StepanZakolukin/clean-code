namespace Markdown;

public class TagReplacementSpecification
{
    public readonly string InputOpeningTag;
    public readonly string InputClosingTag;
    public readonly string OutputOpeningTag;
    public readonly string OutputClosingTag;
    public readonly HashSet<string> InvalidSubstringsInMarkup;

    public TagReplacementSpecification(IEnumerable<string> invalidSubstringsInMarkup,
        string inputOpeningTag, string outputOpeningTag,
        string inputClosingTag = null, string outputClosingTag = null)
    {
        InputOpeningTag = inputOpeningTag ?? throw new ArgumentNullException(nameof(inputOpeningTag));
        InputClosingTag = inputClosingTag ?? inputOpeningTag;
        OutputOpeningTag = outputOpeningTag ?? throw new ArgumentNullException(nameof(outputOpeningTag));
        OutputClosingTag = outputClosingTag ?? outputOpeningTag[0] + "/" + outputOpeningTag.Substring(1);

        foreach (var substring in invalidSubstringsInMarkup)
        {
            if (substring.Length > inputOpeningTag.Length + 2)
                throw new ArgumentException("Запрещенные подстроки не могут быть длиннее inputOpeningTag.Length + 2");
        }
        InvalidSubstringsInMarkup = invalidSubstringsInMarkup.ToHashSet();
    }
}