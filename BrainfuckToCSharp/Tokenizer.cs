using System.Collections;

namespace BrainfuckToCSharp;

public sealed class Tokenizer : IEnumerable<IToken>
{
    private readonly string _input;

    public Tokenizer(string input)
    {
        _input = input;
    }

    public IEnumerator<IToken> GetEnumerator()
    {
        foreach (var token in _input)
        {
            if (char.IsWhiteSpace(token))
            {
                continue;
            }

            yield return token switch
            {
                '>' => new NextCellToken(),
                '<' => new PrevCellToken(),
                '+' => new AddToken(),
                '-' => new SubtractToken(),
                '.' => new OutputToken(),
                ',' => new InputToken(),
                '[' => new LoopStartToken(),
                ']' => new LoopEndToken(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}