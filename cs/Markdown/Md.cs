using System.Text;

namespace Markdown;

internal class Md
{
    private readonly ISpecificationProvider specificationProvider;
    public Md(ISpecificationProvider specificationProvider)
    {
        this.specificationProvider = specificationProvider;
    }
    
    public string Render(string markdown)
    {
        var markupSpecification = specificationProvider.GetMarkupSpecification().ToArray();

        var fragments = FindAllSubstringsForFormatting(markdown, markupSpecification);
        var renderedString = PerformTextFormatting(markdown, fragments);

        return RemoveEscapingOfControlSubstrings(renderedString, markupSpecification);
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

        for (var i = 0; i < text.Length - tagSpecific.InputTag.Opening.Length - 1; i++)
        {
            var currentSubstring = text.Substring(i, tagSpecific.InputTag.Opening.Length + 2);
                
            if (lookingForOpenTag && currentSubstring.EndsWith(tagSpecific.InputTag.Opening) &&
                i + tagSpecific.InputTag.Opening.Length + 3 < text.Length &&
                tagSpecific.CheckOpeningTag(text.Substring(i, tagSpecific.InputTag.Opening.Length + 3)))
            {
                index = i;
                lookingForOpenTag = false;
            }
            else if (!lookingForOpenTag && currentSubstring.EndsWith(tagSpecific.InputTag.Opening) &&
                     tagSpecific.CheckClosingTag(currentSubstring))
            {
                lookingForOpenTag = true;
                var length = i + tagSpecific.InputTag.Opening.Length - index;

                if (length > 2 * tagSpecific.InputTag.Opening.Length)
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
                fragment.Specification.InputTag.Opening,
                fragment.Specification.OutputTag.Opening,
                fragment.StartIndex));
            result.Add(new TagReplacementOptions(
                fragment.Specification.InputTag.Closing,
                fragment.Specification.OutputTag.Closing,
                fragment.StartIndex + fragment.Length - fragment.Specification.InputTag.Closing.Length));
        }

        return result;
    }
    
    private string RemoveEscapingOfControlSubstrings(string text, IEnumerable<TagReplacementSpecification> tags)
    {
        foreach (var tag in tags)
        {
            text = text.Replace('\\' + tag.InputTag.Opening, tag.InputTag.Closing);
            if (tag.InputTag.Closing != tag.InputTag.Opening)
                text = text.Replace('\\' + tag.InputTag.Closing, tag.InputTag.Closing);
        }
        
        return text.Replace(@"\\", "\\");;
    }
}