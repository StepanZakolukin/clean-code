namespace Markdown.Tags;

public class BoldTag : IMarkupTag
{
    public Tag InputTag { get; } = new Tag("__", "__");
    public Tag OutputTag { get; } = new Tag("<strong>", "</strong>");
    
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