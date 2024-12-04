namespace Markdown.Tags;

public class HeaderTag : IMarkupTag
{
    public Tag InputTag { get; } = new Tag("# ", Environment.NewLine);
    public Tag OutputTag { get; } = new Tag("<h1>", "</h1>");
    
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