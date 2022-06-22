
using System.Text;

var lines = new List<Line>(32);

void ClearLine(int linePos)
{
    for (int i = 0; i < Console.BufferWidth; i++)
        Console.Write('\0');

    Console.CursorTop = linePos;
    Console.CursorLeft = 0;
}

void ClearFromCurrent()
{
    for (int i = Console.CursorLeft; i < Console.BufferWidth; i++)
        Console.Write('\0');
}

void DrawLine(Line line)
{
    Console.CursorTop = line.Position;
    Console.CursorLeft = 0;
    foreach (var part in line.Text)
        DrawNToken(part);
}
void DrawNToken(NToken token)
{
    Console.ForegroundColor = token.Color;
    Console.Write(token.Content);
    if (token.Next is not '\0')
        Console.Write(token.Next);
}

void Input()
{
    var lineB = new StringBuilder();
    var line = new Line();

    void ProcessLine()
    {
        ClearFromCurrent();
        if(line.Text.Count > 0)
            line.Text.RemoveAt(line.Text.Count - 1);
        var tokens = lineB.ToString().Split(' ');
        foreach(var token in tokens)
        {
            switch(token)
            {
                case "public":
                    line.Text.Add(new(token, ConsoleColor.DarkCyan));
                    break;
                case "class":
                    line.Text.Add(new(token, ConsoleColor.DarkCyan));
                    break;
                case "void":
                    line.Text.Add(new(token, ConsoleColor.DarkBlue));
                    break;
                default:
                    line.Text.Add(new(token));
                    break;
            }
        }
        lineB.Clear();
        lineB.Append(tokens[^1]);
        DrawLine(line);
    }

    while(true)
    {
        var key = Console.ReadKey(true);
        if (char.IsLetterOrDigit(key.KeyChar) || char.IsPunctuation(key.KeyChar) || char.IsWhiteSpace(key.KeyChar))
            lineB.Append(key.KeyChar);
        ProcessLine();
    }
}

Input();

class Line
{
    public List<NToken> Text = new (32);
    public int Position;
}
readonly record struct NToken(
    string Content, 
    ConsoleColor Color = ConsoleColor.White, 
    char Next = ' '
);