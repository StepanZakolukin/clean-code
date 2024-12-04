namespace Markdown.Tags.TagSpecification;

public class HeaderTag : IMarkupTag
{
    public Tag Opening { get; init; }
    public Tag Closing { get; init; }
    public HeaderTag()
    {
       Opening = new Tag("# ", "<h1>");
       Closing = new Tag(Environment.NewLine, "</h1>");
    }
    
    public bool DidConflict(IMarkupTag tag) => tag is HeaderTag;

    public TextFragment FindNextPairOfTags(string text, int startIndex)
    {
        throw new NotImplementedException();
    }

    public string PerformTagFormatting(TagReplacementSpecification replacement, IMarkupTag previousTag, IMarkupTag nextTag)
    {
        throw new NotImplementedException();
    }
}