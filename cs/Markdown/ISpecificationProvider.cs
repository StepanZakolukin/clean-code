using Markdown.Tags;

namespace Markdown;

public interface ISpecificationProvider
{
    public IEnumerable<IMarkupTag> GetMarkupSpecification();
}