using Markdown.Tags.TagSpecification;

namespace Markdown.Tags;

public class TagReplacementSpecification : IComparable
{
    public Tag Tag { get; init; }
    public int StartIndex { get; init; }
    public BaseTag Markup { get; init; }

    public TagReplacementSpecification(Tag tag, BaseTag markup, int startIndex)
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