using BrainfuckToCSharp;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Provide some input file path");
        }

        var inputFilePath = args[0];

        if (Path.Exists(inputFilePath) is false)
        {
            Console.WriteLine("Input file does not exist");
        }

        var fileContent = File.ReadAllText(inputFilePath);

        var parser = new Parser(fileContent);

        var operations = parser.Parse().ToArray();

        Compile(operations);
        
        Interpret(operations);
        
        Transpile(operations);
    }

    private static void Compile(IEnumerable<IOperation> operations)
    {
        Console.WriteLine("Compiler result:");
        
        var compiler = new Compiler(operations);

        var instructions = compiler.Compile();

        File.WriteAllBytes("Runtime/Output.bf", instructions);
        
        var cpuEmulator = new CpuEmulator(instructions);

        cpuEmulator.Execute();
    }

    private static void Interpret(IEnumerable<IOperation> operations)
    {
        Console.WriteLine("Interpreter result:");
        
        var interpreter = new Interpreter(operations);

        interpreter.Interpret();
    }

    private static void Transpile(IEnumerable<IOperation> operations)
    {
        var transpiler = new Transpiler(operations);
        
        var sourceCode = transpiler.Transpile();
        
        File.WriteAllText("Runtime/Output._cs", sourceCode);
    }
}