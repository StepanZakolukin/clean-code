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

    public override TextFragment FindNextPairOfTags(string text, int startIndex, BaseTag tagSpecification)
    {
        var pair = base.FindNextPairOfTags(text, startIndex, tagSpecification);
        if (pair is null) return null;
        
        var fragment = text[pair.OpeningTag.StartIndex..(pair.ClosingTag.StartIndex + Closing.Old.Length)];
        var indexBeforeFragment = pair.OpeningTag.StartIndex - 1;
        var indexAfterFragment = pair.ClosingTag.StartIndex + pair.ClosingTag.Tag.Old.Length;
        if (fragment.Split().Length == 1 ||
            (indexBeforeFragment >= 0 && (text[indexBeforeFragment] == ' ' || text[indexBeforeFragment] == '\\') ||
             pair.OpeningTag.StartIndex == 0) &&
            (indexAfterFragment < text.Length &&
             (text[indexAfterFragment] == ' ' || text[indexAfterFragment] == '\\') ||
             indexAfterFragment == text.Length))
        {
            return pair;
        }
        
        return FindNextPairOfTags(text, pair.ClosingTag.StartIndex, tagSpecification);
    }

    protected override TagReplacementSpecification FindNextTag(string text, int startIndex, Tag tag, BaseTag tagSpecification)
    {
        var numberOfEscapeCharacters = 0;

        for (var i = startIndex; i <= text.Length - tag.Old.Length; i++)
        {
            if (text[i] == '\\') numberOfEscapeCharacters++;
            else if (numberOfEscapeCharacters % 2 == 0 && text.Substring(i, tag.Old.Length) == tag.Old)
            {
                var index = Math.Max(i - 1, 0);
                var substring = text.Substring(index, Math.Min(i + tag.Old.Length, text.Length - 1) - index + 1);

                if (!forbiddenSubstrings.Any(s => substring.StartsWith(s) || substring.EndsWith(s)) &&
                    (tag == Opening && substring[^1] != ' ' || tag == Closing && substring[0] != ' '))
                {
                    return new TagReplacementSpecification(tag, this, i);
                }
                
                numberOfEscapeCharacters = 0;
            }
            else numberOfEscapeCharacters = 0;
        }

        return null;
    }
}