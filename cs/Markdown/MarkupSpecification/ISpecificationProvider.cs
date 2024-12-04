using Markdown.Tags;

namespace Markdown.MarkupSpecification;

public interface ISpecificationProvider
{
    public IEnumerable<IMarkupTag> GetMarkupSpecification();
}