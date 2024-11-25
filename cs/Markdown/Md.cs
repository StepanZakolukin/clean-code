namespace Markdown;

internal class Md
{
    public string Render(string markdown)
    {
        return PerformTextFormatting(markdown,
            FindAllSubstringsForFormatting(markdown, GetMarkupSpecification(markdown)));
    }

    private IEnumerable<TagReplacementSpecification> GetMarkupSpecification(string markdown)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<TextFragment> FindAllSubstringsForFormatting(string text,
        IEnumerable<TagReplacementSpecification> markupSpecification)
    {
        throw new NotImplementedException();
    }
    
    private string PerformTextFormatting(string text, IEnumerable<TextFragment> fragments)
    {
        throw new NotSupportedException();
    }
}
