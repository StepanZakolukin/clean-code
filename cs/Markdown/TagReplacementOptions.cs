namespace Markdown;

public class TagReplacementOptions : IComparable
{
    public readonly string OldTag;
    public readonly string NewTag;
    public readonly int StartIndex;

    public TagReplacementOptions(string currentTag, string newTag, int startIndex)
    {
        NewTag = newTag ?? throw new ArgumentNullException(nameof(newTag));
        OldTag = currentTag ?? throw new ArgumentNullException(nameof(currentTag));
        
        if (startIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        
        StartIndex = startIndex;
    }

    public int CompareTo(object? obj)
    {
        if (obj is not TagReplacementOptions other)
            throw new ArgumentException($"Object must be of type {nameof(TagReplacementOptions)}");
        
        if (ReferenceEquals(this, other)) return 0;
        return StartIndex.CompareTo(other.StartIndex);
    }
}