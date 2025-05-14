## 🧠 Brainfuck to C# Transpiler

**BrainfuckToCSharp** is a lightweight console utility that transpiles [Brainfuck](https://en.wikipedia.org/wiki/Brainfuck) code into equivalent C# code. Each Brainfuck instruction is translated into readable and compilable C# logic that emulates the behavior of the original program.

---

## ✨ Features

* Full support for all Brainfuck commands: `+ - > < . , [ ]`
* Generates clean and understandable C# output
* Simple CLI usage — just pass the input file path

---

## 🛠 Usage

To transpile a Brainfuck file, run the program from the command line and pass the path to the `.bf` file as the only argument:

```bash
dotnet run -- "path/to/your/code.bf"
```

The transpiled C# code will be written to a file named `Output.cs` in the current directory.

---

## 📌 Example

Input Brainfuck code:

```
>+++++++++[<++++++++>-]<.>+++++++[<++++>-]<+.+++++++..+++.>>>++++++++[<++++>-]
<.>>>++++++++++[<+++++++++>-]<---.<<<<.+++.------.--------.>>+.>++++++++++.
```

Transpiled C# output:

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

## 📄 License

This project is licensed under the MIT License. Feel free to use, modify, and distribute it.
