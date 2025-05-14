namespace BrainfuckToAsm;

public sealed class Interpreter
{
    private readonly IOperation[] _operations;
    private readonly char[] _memory = new char[30000];
    private int _ptr = 0;

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
            case NextCellOperation:
                _ptr++;

                if (_ptr >= _operations.Length)
                {
                    _ptr = 0;
                }

                break;
            case PreviousCellOperation:
                _ptr--;

                if (_ptr < 0)
                {
                    _ptr = _operations.Length - 1;
                }

                break;
            case AddOperation:
                _memory[_ptr]++;
                break;
            case SubtractOperation:
                _memory[_ptr]--;
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