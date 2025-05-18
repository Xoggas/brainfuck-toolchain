namespace BrainfuckToCSharp;

public sealed class CpuEmulator
{
    private const byte IncrementDecrementOpCode = 0x0;
    private const byte MoveOpCode = 0x1;
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
                case IncrementDecrementOpCode:
                    ExecuteIncrementDecrementInstruction();
                    break;
                case MoveOpCode:
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
        _ = ReadByte();
        var sign = ReadByte();
        var value = ReadByte();

        switch (sign)
        {
            case 0x0:
                _memory[_ptr] += (char)value;
                break;
            case 0x1:
                _memory[_ptr] -= (char)value;
                break;
        }
    }
    
    private void ExecuteMoveOperation()
    {
        _ = ReadByte();
        var sign = ReadByte();
        var value = ReadByte();

        switch (sign)
        {
            case 0x0:
                _ptr += value;
                break;
            case 0x1:
                _ptr -= value;
                break;
        }
    }
}