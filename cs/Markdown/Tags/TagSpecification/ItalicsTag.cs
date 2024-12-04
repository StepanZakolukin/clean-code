namespace Markdown.Tags.TagSpecification;

public class ItalicsTag : IMarkupTag
{
    public Tag Opening { get; init; }
    public Tag Closing { get; init; } 

    public ItalicsTag()
    {
        Opening = new Tag("_", "<em>");
        Closing= new Tag("_", "</em>");
    }

    public bool DidConflict(IMarkupTag tag) => tag is BoldTag or ItalicsTag;

    public TextFragment FindNextPairOfTags(string text, int startIndex)
    {
        throw new NotImplementedException();
    }

    public string PerformTagFormatting(TagReplacementSpecification replacement, IMarkupTag previousTag, IMarkupTag nextTag)
    {
        throw new NotImplementedException();
    }
}