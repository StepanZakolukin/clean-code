namespace Markdown.Tags.TagSpecification;

public class BulletedListTag : IMarkupTag
{
    public Tag Opening { get; init; }
    public Tag Closing { get; init; }

    public BulletedListTag()
    {
        Opening = new Tag("-", "<li>");
        Closing = new Tag(Environment.NewLine, "</li>");
    }
    public bool DidConflict(IMarkupTag tag) => tag is BulletedListTag;

    public TextFragment FindNextPairOfTags(string text, int startIndex)
    {
        throw new NotImplementedException();
    }

    public string PerformTagFormatting(TagReplacementSpecification replacement, IMarkupTag previousTag, IMarkupTag nextTag)
    {
        throw new NotImplementedException();
    }
}