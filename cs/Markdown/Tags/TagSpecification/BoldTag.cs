namespace Markdown.Tags.TagSpecification;

public class BoldTag : IMarkupTag
{
    private IMarkupTag markupTagImplementation;
    public Tag Opening { get; init; }
    public Tag Closing { get; init; }
    public BoldTag()
    {
        Opening = new Tag("__", "<strong>");
        Closing = new Tag("__", "</strong>");
    }

    public bool DidConflict(IMarkupTag tag) => tag is BoldTag;

    public TextFragment FindNextPairOfTags(string text, int startIndex)
    {
        throw new NotImplementedException();
    }

    public string PerformTagFormatting(TagReplacementSpecification replacement, IMarkupTag previousTag, IMarkupTag nextTag)
    {
        throw new NotImplementedException();
    }
}