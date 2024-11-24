namespace Markdown;

internal class Md
{
    public string Render(string markdown)
    {
        return markdown.PerformTextFormatting(GetFragmentToFormatInHeading, ToFormatFragmentIntoHeader)
            .PerformTextFormatting(GetFragmentToFormatInItalics, ToFormatFragmentInItalics)
            .PerformTextFormatting(GetFragmentToFormatInSemiBold, ToFormatFragmentIntoSemiBold);
    }

    private PortionText GetFragmentToFormatInHeading(string text, int startIndex)
    {
        throw new NotImplementedException();
    }

    private PortionText GetFragmentToFormatInItalics(string text, int startIndex)
    {
        throw new NotImplementedException();
    }

    private PortionText GetFragmentToFormatInSemiBold(string text, int startIndex)
    {
        throw new NotImplementedException();
    }

    private string ToFormatFragmentIntoHeader(string fragment)
    {
        throw new NotImplementedException();
    }

    private string ToFormatFragmentInItalics(string fragment)
    {
        throw new NotImplementedException();
    }

    private string ToFormatFragmentIntoSemiBold(string fragment)
    {
        throw new NotImplementedException();
    }
}
