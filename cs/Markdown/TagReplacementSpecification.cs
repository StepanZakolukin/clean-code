using System.Collections;

namespace Markdown;

public class TagReplacementSpecification
{
    public Tag InputTag { get; }
    public Tag OutputTag { get; }
    
    public IEnumerable<string> InvalidSubstringsInMarkup { get; }
    private readonly IEnumerable<string> forbiddenPrefixesOfOpeningTag;
    private readonly IEnumerable<string> forbiddenPrefixesOfClosingTag;

    public TagReplacementSpecification(IEnumerable<string>? invalidSubstringsInMarkup,
        Tag? inputTag, Tag? outputTag,
        IEnumerable<string>? forbiddenPrefixesOfOpeningTag = null,
        IEnumerable<string>? forbiddenPrefixesOfClosingTag = null)
    {
        InputTag = inputTag ?? throw new ArgumentNullException(nameof(inputTag));
        OutputTag = outputTag ?? throw new ArgumentNullException(nameof(outputTag));
        
        InvalidSubstringsInMarkup = invalidSubstringsInMarkup ?? [];
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