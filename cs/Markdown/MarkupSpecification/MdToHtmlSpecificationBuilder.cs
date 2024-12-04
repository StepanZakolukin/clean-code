using Markdown.Tags;
using Markdown.Tags.TagSpecification;

namespace Markdown.MarkupSpecification;

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