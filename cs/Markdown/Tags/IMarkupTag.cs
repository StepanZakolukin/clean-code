namespace Markdown.Tags;

public interface IMarkupTag
{
    public Tag InputTag { get; }
    public Tag OutputTag { get; }
    
    public bool DidConflict(IMarkupTag tag);
    
    public IMarkupTag FindNextTag(string text, int startIndex);

    public string PerformTagFormatting(string text, IMarkupTag currentTag, IMarkupTag nextTag);
}