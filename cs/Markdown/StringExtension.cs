namespace Markdown;

internal static class StringExtension
{
    public static string PerformTextFormatting(
        this string text, Func<string, int, PortionText> getNextFragmentToFormat,
        Func<string, string> formatFragment)
    {
        //Используя переданные функции форматирует текст
        throw new NotSupportedException();
    }
}
