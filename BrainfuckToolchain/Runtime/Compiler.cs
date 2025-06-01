namespace BrainfuckToCSharp;

public sealed class Compiler
{
    private const byte IncrementOpCode = 0x0;
    private const byte DecrementOpCode = 0x8;
    private const byte SelectNextCellOpCode = 0x1;
    private const byte SelectPreviousCellOpCode = 0x7;
    private const byte InputOpCode = 0x2;
    private const byte OutputOpCode = 0x3;
    private const byte GotoOpCode = 0x4;
    private const byte GotoIfOpCode = 0x5;
    private const byte ReturnOpCode = 0x6;

    private readonly IOperation[] _operations;
    private readonly List<byte> _instructions = [];

    public Compiler(IEnumerable<IOperation> operations)
    {
        _operations = operations.ToArray();
    }

    public byte[] Compile()
    {
        foreach (var operation in _operations)
        {
            HandleOperation(operation);
        }

        AddInstruction(ReturnOpCode);

        return _instructions.ToArray();
    }

    private static byte[] UintToBytes(uint value)
    {
        return BitConverter.GetBytes(value);
    }

    private int AddInstruction(params byte[] bytes)
    {
        var insertionPoint = _instructions.Count;

        _instructions.AddRange(bytes);

        return insertionPoint;
    }

    private void HandleOperation(IOperation operation)
    {
        switch (operation)
        {
            case NextCellOperation nextCellOperation:
                AddInstruction(SelectNextCellOpCode, 
                    (byte)nextCellOperation.Count);
                break;
            case PreviousCellOperation previousCellOperation:
                AddInstruction(SelectPreviousCellOpCode, 
                    (byte)previousCellOperation.Count);
                break;
            case AddOperation addOperation:
                AddInstruction(IncrementOpCode,
                    (byte)addOperation.Count);
                break;
            case SubtractOperation subtractOperation:
                AddInstruction(DecrementOpCode,
                    (byte)subtractOperation.Count);
                break;
            case InputOperation:
                AddInstruction(InputOpCode);
                break;
            case OutputOperation:
                AddInstruction(OutputOpCode);
                break;
            case LoopOperation loopOperation:
                HandleLoop(loopOperation);
                break;
        }
    }

    private void HandleLoop(LoopOperation loopOperation)
    {
        // Adding and recording the address of the goto_if operation
        // to which we will jump after the iteration ended to avoid double checks
        // in the beginning of the loop and in the end  
        var gotoIfInstruction = AddInstruction(GotoIfOpCode, 0x0, 0x0, 0x0, 0x0);

        // Iterating through the operations inside the loop
        foreach (var operation in loopOperation.Operations)
        {
            HandleOperation(operation);
        }

        // Converting the goto_if operation index into bytes
        var gotoIfInstructionBytes = UintToBytes((uint)gotoIfInstruction);

        // Adding a goto operation in the end of the loop that
        // will jump to goto_if operation
        AddInstruction(
            GotoOpCode,
            gotoIfInstructionBytes[0],
            gotoIfInstructionBytes[1],
            gotoIfInstructionBytes[2],
            gotoIfInstructionBytes[3]
        );

        // Recording the index of the operation after the goto
        var currentInstructionBytes = UintToBytes((uint)_instructions.Count);

        // Replacing zero bytes in the goto_if operation
        // to the bytes of the next operation after goto
        for (var i = 0; i < 4; i++)
        {
            _instructions[gotoIfInstruction + i + 1] = currentInstructionBytes[i];
        }
    }
}