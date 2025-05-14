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