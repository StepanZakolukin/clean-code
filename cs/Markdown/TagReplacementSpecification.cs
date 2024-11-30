using System.Collections;

namespace Markdown;

public class TagReplacementSpecification
{
    public readonly string InputOpeningTag;
    public readonly string InputClosingTag;
    public readonly string OutputOpeningTag;
    public readonly string OutputClosingTag;
    
    public readonly IEnumerable<string> InvalidSubstringsInMarkup;
    private readonly IEnumerable<string> forbiddenPrefixesOfOpeningTag;
    private readonly IEnumerable<string> forbiddenPrefixesOfClosingTag;

    public TagReplacementSpecification(IEnumerable<string> invalidSubstringsInMarkup,
        string inputOpeningTag, string outputOpeningTag,
        string inputClosingTag = null, string outputClosingTag = null,
        IEnumerable<string> forbiddenPrefixesOfOpeningTag = null,
        IEnumerable<string> forbiddenPrefixesOfClosingTag = null)
    {
        InputOpeningTag = inputOpeningTag ?? throw new ArgumentNullException(nameof(inputOpeningTag));
        InputClosingTag = inputClosingTag ?? inputOpeningTag;
        OutputOpeningTag = outputOpeningTag ?? throw new ArgumentNullException(nameof(outputOpeningTag));
        OutputClosingTag = outputClosingTag ?? outputOpeningTag[0] + "/" + outputOpeningTag.Substring(1);

        if (invalidSubstringsInMarkup.Any(substring => substring.Length > inputOpeningTag.Length + 2))
            throw new ArgumentException("Запрещенные подстроки не могут быть длиннее inputOpeningTag.Length + 2");
        
        InvalidSubstringsInMarkup = invalidSubstringsInMarkup;
        this.forbiddenPrefixesOfClosingTag = forbiddenPrefixesOfClosingTag ?? [];
        this.forbiddenPrefixesOfOpeningTag = forbiddenPrefixesOfOpeningTag ?? [];
    }

    private bool CheckForbiddenPrefixes(string substring, IEnumerable<string> forbiddenPrefixes)
    {
        if (forbiddenPrefixes.Any(substring.EndsWith))
            return false;
        return substring[1] != '\\' || substring.StartsWith(@"\\");
    }

    public bool CheckOpeningTag(string substring) => CheckForbiddenPrefixes(substring, forbiddenPrefixesOfOpeningTag);
    
    public bool CheckClosingTag(string substring) => CheckForbiddenPrefixes(substring, forbiddenPrefixesOfClosingTag);
}