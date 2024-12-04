namespace Markdown.Tags.TagSpecification;

public abstract class BasicSingleTag(Tag opening, Tag closing) : BaseTag(opening, closing)
{
    protected override bool AdditionallyCheckCurrentPosition(string text, int currentIndex, Tag tag)
    {
        var startIndex = Math.Min(currentIndex - 2, 0);
        
        if (tag == Opening) return currentIndex == 0 || text[startIndex..currentIndex] == Environment.NewLine;
        
        return true;
    }
}