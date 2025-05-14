namespace BrainfuckToAsm;

public interface IToken
{
}

public readonly struct EofToken : IToken
{
}

public readonly struct NextCellToken : IToken
{
}

public readonly struct PrevCellToken : IToken
{
}

public readonly struct AddToken : IToken
{
}

public readonly struct SubtractToken : IToken
{
}

public readonly struct OutputToken : IToken
{
}

public readonly struct InputToken : IToken
{
}

public readonly struct LoopStartToken : IToken
{
}

public readonly struct LoopEndToken : IToken
{
}