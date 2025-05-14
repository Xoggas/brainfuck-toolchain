using BrainfuckToAsm;

public static class Program
{
    private static string s_basePath = @"C:\Users\raspisanie\Documents\GitHub\brainfuck-to-asm\BrainfuckToAsm";
    
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

        var transpiler = new Transpiler(operations);
        
        File.WriteAllText(Path.Combine(s_basePath, "Output.cs"), transpiler.Transpile());
    }
}