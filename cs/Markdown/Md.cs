using System.Text;

namespace Markdown;

internal class Md
{
    public string Render(string markdown)
    {
        return PerformTextFormatting(markdown,
            FindAllSubstringsForFormatting(markdown, GetMarkupSpecification()));
    }

    private IEnumerable<TagReplacementSpecification> GetMarkupSpecification()
    {
        var result = new List<TagReplacementSpecification>();
        
        var invalidSubstring = new List<string> { " ", "__" };
        for (var digit = 1; digit < 10; digit++)
            invalidSubstring.Add(digit.ToString());
        result.Add(new(invalidSubstring, "_", "<em>"));
        
        result.Add(new([],
            "__", "<strong>"));
        
        result.Add(new SingleReplacementTagSpecification([],
            "#", "<h1>"));
        
        return result;
    }

    private IEnumerable<TextFragment> FindAllSubstringsForFormatting(string text,
        IEnumerable<TagReplacementSpecification> markupSpecification)
    {
        text = '.' + text;
        
        foreach (var tagSpecific in markupSpecification)
        {
            var tagLength = tagSpecific.InputOpeningTag.Length;
            var index = 0;
            var openingTag = true;

            for (var i = 0; i < text.Length - tagLength; i++)
            {
                var currentSubstring = text.Substring(i, tagLength + 1);
                
                if (openingTag && currentSubstring[0] != '\\' && currentSubstring.EndsWith(tagSpecific.InputOpeningTag))
                {
                    index = i;
                    openingTag = false;
                }
                else if (!openingTag && currentSubstring[0] != '\\' && currentSubstring.EndsWith(tagSpecific.InputClosingTag))
                {
                    var length = i + tagLength - index;

                    if (length > 2 * tagLength)
                    {
                        yield return new TextFragment(
                            index,
                            length, tagSpecific);
                    }
                    openingTag = true;
                }
                else
                {
                    foreach (var invalisSubstring in tagSpecific.InvalidSubstringsInMarkup)
                    {
                        if (currentSubstring.EndsWith(invalisSubstring))
                        {
                            openingTag = true;
                            break;
                        }
                    }
                }
            }
        }
    }
    
    private string PerformTextFormatting(string text, IEnumerable<TextFragment> fragments)
    {
        if (!fragments.Any())
            return text;
        
        var binaryTree = new BinaryTree<TagReplacementOptions>();
        foreach (var fragment in fragments)
        {
            binaryTree.Add(new TagReplacementOptions(
                fragment.Specification.InputOpeningTag,
                fragment.Specification.OutputOpeningTag,
                fragment.StartIndex));
            binaryTree.Add(new TagReplacementOptions(
                fragment.Specification.InputClosingTag,
                fragment.Specification.OutputClosingTag,
                fragment.StartIndex + fragment.Length - fragment.Specification.InputClosingTag.Length));
        }

        var result = new StringBuilder();
        var endOfLastReplacement = -1;
        
        foreach (var replacementOptions in binaryTree)
        {
            result.Append(text[(endOfLastReplacement + 1)..replacementOptions.StartIndex]);
            result.Append(replacementOptions.NewTag);
            endOfLastReplacement = replacementOptions.StartIndex + replacementOptions.OldTag.Length - 1;
        }
        
        if (endOfLastReplacement + 1 != text.Length)
            result.Append(text[(endOfLastReplacement + 1)..text.Length]);
        
        return result.ToString();
    }
}
