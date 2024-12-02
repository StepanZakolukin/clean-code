namespace Markdown;

public class MdToHtmlSpecificationBuilder : ISpecificationProvider
{
    public IEnumerable<TagReplacementSpecification> GetMarkupSpecification()
    {
        var result = new List<TagReplacementSpecification>();
        
        var invalidSubstring = new List<string> { "__" };
        for (var digit = 1; digit < 10; digit++)
            invalidSubstring.Add(digit.ToString());
        result.Add(new TagReplacementSpecification(
            invalidSubstring,
            new Tag("_", "_"),
            new Tag("<em>", "</em>"),
            ["_ "],
            [ " _", "__" ]));
        
        result.Add(new TagReplacementSpecification(
            [],
            new Tag("__", "__"),
            new Tag("<strong>", "</strong>")));
        
        result.Add(new TagReplacementSpecification(
            [],
            new Tag("# ", Environment.NewLine),
            new Tag("<h1>", "</h1>")));
        
        return result;
    }
}