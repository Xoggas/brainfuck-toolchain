namespace BrainfuckToCSharp;

public sealed class Interpreter
{
    private readonly IOperation[] _operations;
    private readonly char[] _memory = new char[30000];
    private int _ptr;

    public Interpreter(IEnumerable<IOperation> operations)
    {
        _operations = operations.ToArray();
    }

    private char CurrentCellValue => _memory[_ptr];

    public void Interpret()
    {
        foreach (var operation in _operations)
        {
            HandleOperation(operation);
        }
    }

    private void HandleOperation(IOperation operation)
    {
        switch (operation)
        {
            case NextCellOperation nextCellOperation:
                _ptr += nextCellOperation.Count;
                break;
            case PreviousCellOperation previousCellOperation:
                _ptr -= previousCellOperation.Count;
                break;
            case AddOperation addOperation:
                _memory[_ptr] += (char)addOperation.Count;
                break;
            case SubtractOperation subtractOperation:
                _memory[_ptr] -= (char)subtractOperation.Count;
                break;
            case OutputOperation:
                Console.Write(_memory[_ptr]);
                break;
            case InputOperation:
                _memory[_ptr] = Console.ReadKey().KeyChar;
                break;
            case LoopOperation loopOperation:
                HandleLoop(loopOperation);
                break;
        }
    }

    private void HandleLoop(LoopOperation loopOperation)
    {
        while (CurrentCellValue != 0)
        {
            foreach (var operation in loopOperation.Operations)
            {
                HandleOperation(operation);
            }
        }
    }
}