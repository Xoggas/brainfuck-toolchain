using BrainfuckToAsm;

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

        var operations = parser.Parse();

        var transpiler = new Transpiler(operations);
        
        File.WriteAllText("Output.cs", transpiler.Transpile());
    }
}