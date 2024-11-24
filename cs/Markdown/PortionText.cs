namespace Markdown;

internal class PortionText
{
    public readonly int StartIndex;
    public readonly int Length;

    public PortionText(int startIndex, int length)
    {
        if (startIndex < 0)
            throw new ArgumentException("startIndex не может быть отрицательным");
        if (length < 0)
            throw new ArgumentException("length не может быть отрицательной");

        Length = length;
        StartIndex = startIndex;
    }
}
