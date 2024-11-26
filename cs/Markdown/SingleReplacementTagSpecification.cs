namespace Markdown;

public class SingleReplacementTagSpecification : TagReplacementSpecification
{
    public SingleReplacementTagSpecification(IEnumerable<string> invalidSubstringsInMarkup,
        string inputOpeningTag, string outputOpeningTag)
        : base(invalidSubstringsInMarkup, inputOpeningTag, outputOpeningTag, "\n\r")
    {
    }
}