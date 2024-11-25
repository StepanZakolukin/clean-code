namespace Markdown;

internal class TextFragment
{
    public readonly int StartIndex;
    public readonly int Length;
    public readonly TagReplacementSpecification Specification;

    public TextFragment(int startIndex, int length, TagReplacementSpecification specification)
    {
        if (startIndex < 0)
            throw new ArgumentException($"{nameof(startIndex)} не может быть отрицательным");
        if (length < 0)
            throw new ArgumentException($"{nameof(length)} не может быть отрицательной");

        Length = length;
        StartIndex = startIndex;
        Specification = specification ?? throw new ArgumentNullException(nameof(specification));
    }
}
