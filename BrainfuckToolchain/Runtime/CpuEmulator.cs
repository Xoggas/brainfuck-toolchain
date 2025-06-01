namespace BrainfuckToCSharp;

public sealed class CpuEmulator
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

    private readonly byte[] _instructions;
    private int _instructionPtr;
    private readonly char[] _memory = new char[30000];
    private int _ptr;

    public CpuEmulator(byte[] instructions)
    {
        _instructions = instructions;
    }

    private byte CurrentByte => _instructions[_instructionPtr];

    private void Advance(int amount = 1)
    {
        _instructionPtr += amount;
    }

    private byte ReadByte()
    {
        var value = CurrentByte;
        Advance();
        return value;
    }

    private int ReadInt()
    {
        var b1 = ReadByte();
        var b2 = ReadByte();
        var b3 = ReadByte();
        var b4 = ReadByte();
        return (int)BitConverter.ToUInt32([b1, b2, b3, b4]);
    }

    public void Execute()
    {
        while (CurrentByte != ReturnOpCode)
        {
            switch (CurrentByte)
            {
                case IncrementOpCode or DecrementOpCode:
                    ExecuteIncrementDecrementInstruction();
                    break;
                case SelectNextCellOpCode or SelectPreviousCellOpCode:
                    ExecuteMoveOperation();
                    break;
                case InputOpCode:
                    _memory[_ptr] = Console.ReadKey().KeyChar;
                    Advance();
                    break;
                case OutputOpCode:
                    Console.Write(_memory[_ptr]);
                    Advance();
                    break;
                case GotoIfOpCode:
                    _ = ReadByte();
                    var nextIp = ReadInt();

                    if (_memory[_ptr] == 0)
                    {
                        _instructionPtr = nextIp;
                    }

                    break;
                case GotoOpCode:
                    _ = ReadByte();
                    var targetIp = ReadInt();
                    _instructionPtr = targetIp;
                    break;
                case ReturnOpCode:
                    return;
            }
        }
    }

    private void ExecuteIncrementDecrementInstruction()
    {
        var operation = ReadByte();
        var offset = ReadByte();

        switch (operation)
        {
            case IncrementOpCode:
                _memory[_ptr] += (char)offset;
                break;
            case DecrementOpCode:
                _memory[_ptr] -= (char)offset;
                break;
        }
    }

    private void ExecuteMoveOperation()
    {
        var operation = ReadByte();
        var offset = ReadByte();

        switch (operation)
        {
            case SelectNextCellOpCode:
                _ptr += offset;
                break;
            case SelectPreviousCellOpCode:
                _ptr -= offset;
                break;
        }
    }
}