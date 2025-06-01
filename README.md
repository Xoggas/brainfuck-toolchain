## 🧠 Brainfuck Toolchain (Transpiler, Interpreter, Compiler)

This project provides a full toolchain for working with the Brainfuck language. It includes:

* 🧾 **Transpiler** – Converts Brainfuck code into C# source code.
* 🖥 **Interpreter** – Executes Brainfuck using a custom virtual machine.
* 🛠 **Compiler** – Compiles Brainfuck into a custom compact instruction format.

---

## 🔧 Custom Instruction Set

The compiler translates Brainfuck code into a custom bytecode format, where each instruction is 1 to 5 bytes long. Below is the encoding:

| Brainfuck | Instruction         | Bytes   | Description                      |
| --------- |---------------------|---------| -------------------------------- |
| `+`       | `0x00 <value>`      | 2 bytes | Increment memory cell by `value` |
| `-`       | `0x08 <value>`      | 2 bytes | Decrement memory cell by `value` |
| `>`       | `0x01 <value>`      | 2 bytes | Move pointer right by `value`    |
| `<`       | `0x07 <value>`      | 2 bytes | Move pointer left by `value`     |
| `,`       | `0x02`              | 1 byte  | Read input into current cell     |
| `.`       | `0x03`              | 1 byte  | Write current cell to output     |
| `goto`    | `0x04 <u32 index>`  | 5 bytes | Jump to instruction at index     |
| `goto_if` | `0x05 <u32 index>`  | 5 bytes | If current cell == 0, jump       |
| `ret`     | `0x06`              | 1 byte  | Terminate execution              |

### 🔁 Loop Generation Strategy

Loops (`[ ... ]`) are compiled using a combination of `goto_if` and `goto`:

```text
goto_if L_end
  [ loop body ]
goto L_start
L_end:
```

This effectively creates a while-like loop: it checks the current cell, and jumps over the loop if it's zero, or re-enters it otherwise.

---

## 📥 Sample Input

```brainfuck
>+++++++++[<++++++++>-]<.>+++++++[<++++>-]<+.+++++++..+++.>>>++++++++[<++++>-]
<.>>>++++++++++[<+++++++++>-]<---.<<<<.+++.------.--------.>>+.>++++++++++.
```

---

## 🔄 Transpiler Output (C#)

```csharp
var memory = new char[30000];
var ptr = 0;
ptr += 1;
memory[ptr] += (char)9;
while (memory[ptr] != 0)
{
    ptr -= 1;
    memory[ptr] += (char)8;
    ptr += 1;
    memory[ptr] -= (char)1;
}
ptr -= 1;
Console.Write(memory[ptr]);
ptr += 1;
memory[ptr] += (char)7;
while (memory[ptr] != 0)
{
    ptr -= 1;
    memory[ptr] += (char)4;
    ptr += 1;
    memory[ptr] -= (char)1;
}
ptr -= 1;
memory[ptr] += (char)1;
Console.Write(memory[ptr]);
memory[ptr] += (char)7;
Console.Write(memory[ptr]);
Console.Write(memory[ptr]);
memory[ptr] += (char)3;
Console.Write(memory[ptr]);
ptr += 3;
memory[ptr] += (char)8;
while (memory[ptr] != 0)
{
    ptr -= 1;
    memory[ptr] += (char)4;
    ptr += 1;
    memory[ptr] -= (char)1;
}
ptr -= 1;
Console.Write(memory[ptr]);
ptr += 3;
memory[ptr] += (char)10;
while (memory[ptr] != 0)
{
    ptr -= 1;
    memory[ptr] += (char)9;
    ptr += 1;
    memory[ptr] -= (char)1;
}
ptr -= 1;
memory[ptr] -= (char)3;
Console.Write(memory[ptr]);
ptr -= 4;
Console.Write(memory[ptr]);
memory[ptr] += (char)3;
Console.Write(memory[ptr]);
memory[ptr] -= (char)6;
Console.Write(memory[ptr]);
memory[ptr] -= (char)8;
Console.Write(memory[ptr]);
ptr += 2;
memory[ptr] += (char)1;
Console.Write(memory[ptr]);
ptr += 1;
memory[ptr] += (char)10;
Console.Write(memory[ptr]);
```

---

## 🧱 Compiler Output 

```
0x01, 0x01, 0x00, 0x09, 0x05, 0x16, 0x00, 0x00, 0x00, 0x07, 0x01, 0x00,
0x08, 0x01, 0x01, 0x08, 0x01, 0x04, 0x04, 0x00, 0x00, 0x00, 0x07, 0x01,
0x03, 0x01, 0x01, 0x00, 0x07, 0x05, 0x2F, 0x00, 0x00, 0x00, 0x07, 0x01,
0x00, 0x04, 0x01, 0x01, 0x08, 0x01, 0x04, 0x1D, 0x00, 0x00, 0x00, 0x07,
0x01, 0x00, 0x01, 0x03, 0x00, 0x07, 0x03, 0x03, 0x00, 0x03, 0x03, 0x01,
0x03, 0x00, 0x08, 0x05, 0x51, 0x00, 0x00, 0x00, 0x07, 0x01, 0x00, 0x04,
0x01, 0x01, 0x08, 0x01, 0x04, 0x3F, 0x00, 0x00, 0x00, 0x07, 0x01, 0x03,
0x01, 0x03, 0x00, 0x0A, 0x05, 0x6A, 0x00, 0x00, 0x00, 0x07, 0x01, 0x00,
0x09, 0x01, 0x01, 0x08, 0x01, 0x04, 0x58, 0x00, 0x00, 0x00, 0x07, 0x01,
0x08, 0x03, 0x03, 0x07, 0x04, 0x03, 0x00, 0x03, 0x03, 0x08, 0x06, 0x03,
0x08, 0x08, 0x03, 0x01, 0x02, 0x00, 0x01, 0x03, 0x01, 0x01, 0x00, 0x0A,
0x03, 0x06
```

## 📄 License
This project is licensed under the MIT License — you can use, modify, and distribute it freely, with proper attribution. The software is provided as is, without any warranty.
