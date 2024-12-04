namespace Markdown.Tags;

public class TagReplacementSpecification : IComparable
{
    public required Tag Tag { get; init; }
    public required int StartIndex { get; init; }
    public required IMarkupTag Markup { get; init; }

    public TagReplacementSpecification(Tag tag, IMarkupTag markup, int startIndex)
    {
        Tag = tag;
        StartIndex = startIndex;
        Markup = markup;
    }

    public int CompareTo(object? obj)
    {
        if (obj is TagReplacementSpecification specification)
            return StartIndex.CompareTo(specification.StartIndex);
        throw new ArgumentException($"Object must be of type {nameof(TagReplacementSpecification)}");
    }
}