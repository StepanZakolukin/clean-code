using System.Text;

namespace Markdown;

internal class Md
{
    public string Render(string markdown)
    {
        var markupSpecification = GetMarkupSpecification().ToArray();
        
        return RemoveEscapingOfControlSubstrings(PerformTextFormatting(markdown,
            FindAllSubstringsForFormatting(markdown, markupSpecification)), markupSpecification);
    }

    private IEnumerable<TagReplacementSpecification> GetMarkupSpecification()
    {
        var result = new List<TagReplacementSpecification>();
        
        var invalidSubstring = new List<string> { "__" };
        for (var digit = 1; digit < 10; digit++)
            invalidSubstring.Add(digit.ToString());
        result.Add(new TagReplacementSpecification(
            invalidSubstring,
            "_", "<em>",
            null, null,
            ["_ "], [ " _", "__" ]));
        
        result.Add(new TagReplacementSpecification([],
            "__", "<strong>"));
        
        result.Add(new SingleReplacementTagSpecification([],
            "# ", "<h1>"));
        
        return result;
    }

    private IEnumerable<TextFragment> FindAllSubstringsForFormatting(string text,
        IEnumerable<TagReplacementSpecification> markupSpecification)
    {
        foreach (var tagSpecific in markupSpecification)
        {
            foreach (var fragment in FindAllFragmentsHighlightedByTag(tagSpecific, text))
                yield return fragment;
        }
    }

    private IEnumerable<TextFragment> FindAllFragmentsHighlightedByTag(
        TagReplacementSpecification tagSpecific, string text)
    {
        var index = 0;
        text = ".." + text;
        var lookingForOpenTag = true;

        for (var i = 0; i < text.Length - tagSpecific.InputOpeningTag.Length - 1; i++)
        {
            var currentSubstring = text.Substring(i, tagSpecific.InputOpeningTag.Length + 2);
                
            if (lookingForOpenTag && currentSubstring.EndsWith(tagSpecific.InputOpeningTag) &&
                i + tagSpecific.InputOpeningTag.Length + 3 < text.Length &&
                tagSpecific.CheckOpeningTag(text.Substring(i, tagSpecific.InputOpeningTag.Length + 3)))
            {
                index = i;
                lookingForOpenTag = false;
            }
            else if (!lookingForOpenTag && currentSubstring.EndsWith(tagSpecific.InputClosingTag) &&
                     tagSpecific.CheckClosingTag(currentSubstring))
            {
                lookingForOpenTag = true;
                var length = i + tagSpecific.InputOpeningTag.Length - index;

                if (length > 2 * tagSpecific.InputOpeningTag.Length)
                    yield return new TextFragment(index, length, tagSpecific);
            }
            else
            {
                lookingForOpenTag = tagSpecific.InvalidSubstringsInMarkup
                                        .Any(currentSubstring.EndsWith) || lookingForOpenTag;
            }
        }
    }
    
    private string PerformTextFormatting(string text, IEnumerable<TextFragment> fragments)
    {
        if (!fragments.Any()) return text;

        var result = new StringBuilder();
        var endOfLastReplacement = -1;
        
        foreach (var replacementOptions in GetSortedCollectionTags(fragments))
        {
            result.Append(text[(endOfLastReplacement + 1)..replacementOptions.StartIndex]);
            result.Append(replacementOptions.NewTag);
            endOfLastReplacement = replacementOptions.StartIndex + replacementOptions.OldTag.Length - 1;
        }
        
        if (endOfLastReplacement + 1 != text.Length)
            result.Append(text[(endOfLastReplacement + 1)..text.Length]);
        
        return result.ToString();
    }

    private BinaryTree<TagReplacementOptions> GetSortedCollectionTags(IEnumerable<TextFragment> fragments)
    {
        var result = new BinaryTree<TagReplacementOptions>();
        
        foreach (var fragment in fragments)
        {
            result.Add(new TagReplacementOptions(
                fragment.Specification.InputOpeningTag,
                fragment.Specification.OutputOpeningTag,
                fragment.StartIndex));
            result.Add(new TagReplacementOptions(
                fragment.Specification.InputClosingTag,
                fragment.Specification.OutputClosingTag,
                fragment.StartIndex + fragment.Length - fragment.Specification.InputClosingTag.Length));
        }

        return result;
    }
    
    private string RemoveEscapingOfControlSubstrings(string text, IEnumerable<TagReplacementSpecification> tags)
    {
        foreach (var tag in tags)
        {
            text = text.Replace('\\' + tag.InputOpeningTag, tag.InputClosingTag);
            if (tag.InputClosingTag != tag.InputOpeningTag)
                text = text.Replace('\\' + tag.InputClosingTag, tag.InputClosingTag);
        }
        
        return text.Replace(@"\\", "\\");;
    }
}