namespace BrainfuckToAsm;

public interface IOperation
{
}

public sealed class ExitOperation : IOperation
{
}

public sealed class NextCellOperation : IOperation
{
    public int Count { get; }

    public NextCellOperation(int count)
    {
        Count = count;
    }
}

public sealed class PreviousCellOperation : IOperation
{
    public int Count { get; }

    public PreviousCellOperation(int count)
    {
        Count = count;
    }
}

public sealed class AddOperation : IOperation
{
    public int Count { get; }

    public AddOperation(int count)
    {
        Count = count;
    }
}

public sealed class SubtractOperation : IOperation
{
    public int Count { get; }

    public SubtractOperation(int count)
    {
        Count = count;
    }
}

public sealed class InputOperation : IOperation
{
}

public sealed class OutputOperation : IOperation
{
}

public sealed class LoopOperation : IOperation
{
    public List<IOperation> Operations { get; }

    public LoopOperation(List<IOperation> operations)
    {
        Operations = operations;
    }
}