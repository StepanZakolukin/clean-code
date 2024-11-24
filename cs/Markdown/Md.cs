namespace Markdown;

internal class Md
{
    public string Render(string markdown)
    {
        throw new NotImplementedException();
    }

    public string ToHtml(string markdown)
    {
        throw new NotImplementedException();
    }

    public static bool Check(string str)
    {
        var stack = new Stack<char>();
        char openBracket;
        var dict = new Dictionary<char, char>
        {
            ['('] = ')',
            ['['] = ']'
        };

        foreach (var symbol in str)
        {
            if (dict.ContainsKey(symbol))
                stack.Push(symbol);
            else if (dict.ContainsValue(symbol))
            {
                if (stack.Count == 0) return false;
                openBracket = stack.Pop();
                if (dict[openBracket] != symbol) return false;
            }
            else return false;
        }
        return stack.Count == 0;
    }
}
