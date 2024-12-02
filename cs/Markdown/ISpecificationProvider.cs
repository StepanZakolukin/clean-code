namespace Markdown;

public interface ISpecificationProvider
{
    public IEnumerable<TagReplacementSpecification> GetMarkupSpecification();
}