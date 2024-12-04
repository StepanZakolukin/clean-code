using Markdown.Tags;

namespace Markdown;

public class MdToHtmlSpecificationBuilder : ISpecificationProvider
{
    public IEnumerable<IMarkupTag> GetMarkupSpecification()
    {
        return
        [
            new BoldTag(),
            new HeaderTag(),
            new ItalicsTag(),
            new BulletedListTag()
        ];
    }
}