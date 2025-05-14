namespace BrainfuckToAsm;

public interface IOperation
{
}

public sealed class ExitOperation : IOperation
{
}

public sealed class NextCellOperation : IOperation
{
}

public sealed class PreviousCellOperation : IOperation
{
}

public sealed class AddOperation : IOperation
{
}

public sealed class SubtractOperation : IOperation
{
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