namespace BrainfuckToAsm;

public sealed class Parser
{
    private readonly IToken[] _tokens;
    private int _tokenIndex;

    public Parser(string input)
    {
        _tokens = new Tokenizer(input).ToArray();
    }

    private IToken CurrentToken => _tokenIndex >= _tokens.Length ? new EofToken() : _tokens[_tokenIndex];

    public IEnumerable<IOperation> Parse()
    {
        while (CurrentToken is not EofToken)
        {
            yield return Term();
        }
    }

    private void Eat(Type tokenType)
    {
        if (_tokens[_tokenIndex].GetType() != tokenType)
        {
            throw new ArgumentException($"Expected {tokenType} got {_tokens[_tokenIndex].GetType()}");
        }

        Advance();
    }

    private void Advance()
    {
        _tokenIndex++;
    }

    private IOperation Term()
    {
        var counter = 0;

        switch (CurrentToken)
        {
            case NextCellToken:
                while (CurrentToken is NextCellToken)
                {
                    counter++;
                    Eat(typeof(NextCellToken));
                }
                return new NextCellOperation(counter);
            case PrevCellToken:
                while (CurrentToken is PrevCellToken)
                {
                    counter++;
                    Eat(typeof(PrevCellToken));
                }
                return new PreviousCellOperation(counter);
            case AddToken:
                while (CurrentToken is AddToken)
                {
                    counter++;
                    Eat(typeof(AddToken));
                }
                return new AddOperation(counter);
            case SubtractToken:
                while (CurrentToken is SubtractToken)
                {
                    counter++;
                    Eat(typeof(SubtractToken));
                }
                return new SubtractOperation(counter);
            case LoopStartToken:
                Eat(typeof(LoopStartToken));
                return Loop();
            case InputToken:
                Eat(typeof(InputToken));
                return new InputOperation();
            case OutputToken:
                Eat(typeof(OutputToken));
                return new OutputOperation();
        }

        throw new ArgumentException($"Unexpected token {CurrentToken.GetType()}");
    }

    private LoopOperation Loop()
    {
        var operations = new List<IOperation>();

        while (CurrentToken is not LoopEndToken)
        {
            operations.Add(Term());
        }

        Eat(typeof(LoopEndToken));

        return new LoopOperation(operations);
    }
}