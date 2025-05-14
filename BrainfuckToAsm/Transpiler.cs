using System.Text;

namespace BrainfuckToAsm;

public sealed class Transpiler
{
    private readonly IOperation[] _operations;
    private readonly StringBuilder _code = new();

    public Transpiler(IEnumerable<IOperation> operations)
    {
        _operations = operations.ToArray();
    }

    public string Transpile()
    {
        _code.AppendLine("var memory = new char[30000];");
        _code.AppendLine("var ptr = 0;");

        foreach (var operation in _operations)
        {
            HandleOperation(operation);
        }

        return _code.ToString();
    }

    private void HandleOperation(IOperation operation)
    {
        switch (operation)
        {
            case NextCellOperation nextCellOperation:
                _code.AppendLine($"ptr += {nextCellOperation.Count};");
                break;
            case PreviousCellOperation previousCellOperation:
                _code.AppendLine($"ptr -= {previousCellOperation.Count};");
                break;
            case AddOperation addOperation:
                _code.AppendLine($"memory[ptr] += (char){addOperation.Count};");
                break;
            case SubtractOperation subtractOperation:
                _code.AppendLine($"memory[ptr] -= (char){subtractOperation.Count};");
                break;
            case OutputOperation:
                _code.AppendLine("Console.Write(memory[ptr]);");
                break;
            case InputOperation:
                _code.AppendLine("memory[ptr] = Console.ReadKey().KeyChar;");
                break;
            case LoopOperation loopOperation:
                HandleLoop(loopOperation);
                break;
        }
    }

    private void HandleLoop(LoopOperation loopOperation)
    {
        _code.AppendLine("while (memory[ptr] != 0)");
        _code.AppendLine("{");

        foreach (var operation in loopOperation.Operations)
        {
            HandleOperation(operation);
        }

        _code.AppendLine("}");
    }
}