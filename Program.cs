using Spectre.Console;
using Spectre.Console.Cli;
using uMethodLib;

class Program
{
    private static int Main(string[] args)
    {
        var app = new CommandApp<TestSuite>();
        if (!AnsiConsole.Confirm("Run tests?"))
            Environment.Exit(0);
        return app.Run(args);
    }
}