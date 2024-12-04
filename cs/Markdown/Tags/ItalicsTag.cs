namespace Markdown.Tags;

public class ItalicsTag : IMarkupTag
{
    public Tag InputTag { get; } = new Tag("_", "_");
    public Tag OutputTag { get; } = new Tag("<em>", "</em>");
    
    public bool DidConflict(IMarkupTag tag)
    {
        throw new NotImplementedException();
    }

    public IMarkupTag FindNextTag(string text, int startIndex)
    {
        throw new NotImplementedException();
    }

    public string PerformTagFormatting(string text, IMarkupTag currentTag, IMarkupTag nextTag)
    {
        throw new NotImplementedException();
    }
}