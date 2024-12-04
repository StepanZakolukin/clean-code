namespace Markdown.Tags;

public interface IMarkupTag
{
    public Tag Opening { get; init; }
    public Tag Closing { get; init; }
    
    public bool DidConflict(IMarkupTag tag);
    
    public TextFragment FindNextPairOfTags(string text, int startIndex);

    public string PerformTagFormatting(TagReplacementSpecification replacement, IMarkupTag previousTag, IMarkupTag nextTag);
}