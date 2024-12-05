namespace Markdown.Tags.TagSpecification;

public class ItalicsTag : BaseTag
{
    private readonly string[] forbiddenSubstrings;

    public ItalicsTag() : base(new Tag("_", "<em>"), new Tag("_", "</em>"))
    {
        var list = new List<string> { "__" };
        
        for (var i = 0; i < 10; i++)
        {
            list.Add($"_{i}");
            list.Add($"{i}_");
        }
        
        forbiddenSubstrings = list.ToArray();
    }
    public override bool DidConflict(BaseTag tag) => tag is BoldTag;

    public override TextFragment? FindNextPairOfTags(string text, int startIndex, BaseTag tagSpecification)
    {
        var pair = base.FindNextPairOfTags(text, startIndex, tagSpecification);
        if (pair is null) return null;
        
        var fragment = text[pair.OpeningTag.StartIndex..(pair.ClosingTag.StartIndex + Closing.Old.Length)];
        var indexBeforeFragment = pair.OpeningTag.StartIndex - 1;
        var indexAfterFragment = pair.ClosingTag.StartIndex + pair.ClosingTag.Tag.Old.Length;
        var tagOpensBeforeWord = indexBeforeFragment >= 0 &&
            (char.IsWhiteSpace(text, indexBeforeFragment) || text[indexBeforeFragment] == '\\') ||
            pair.OpeningTag.StartIndex == 0;
        var tagClosesAfterWord = indexAfterFragment < text.Length && char.IsWhiteSpace(text, indexAfterFragment) ||
                                  indexAfterFragment == text.Length;
        if (fragment.Split().Length == 1 || tagOpensBeforeWord && tagClosesAfterWord)
        {
            return pair;
        }
        
        return FindNextPairOfTags(text, pair.ClosingTag.StartIndex, tagSpecification);
    }

    protected override bool AdditionallyCheckCurrentPosition(string text, int currentIndex, Tag tag)
    {
        var index = Math.Max(currentIndex - 1, 0);
        var substring = text.Substring(index, Math.Min(currentIndex + tag.Old.Length, text.Length - 1) - index + 1);

        return !forbiddenSubstrings.Any(s => substring.StartsWith(s) || substring.EndsWith(s)) &&
               (tag == Opening && substring[^1] != ' ' || tag == Closing && substring[0] != ' ');
    }
}