using System.CommandLine;

namespace DotnetIgnite.Cli.Commands;

/// <summary>
/// Factory for creating "add" commands with predefined names and descriptions.
/// </summary>
/// <remarks>
/// Uses an internal dictionary of predefined descriptions to generate Command objects. Each
/// command receives a "name" argument and an action that outputs an informational message to the console.
/// </remarks>
public class CliCommandFactory
{
	private static readonly Dictionary<string, string> _descriptions = new()
    {
        { "entity", "Add a new entity" },
        { "handler", "Add a new handler" },
        { "query", "Add a new query" },
        { "command", "Add a new command" },
        { "repository", "Add a new repository" },
        { "endpoint", "Add a new endpoint" },
        { "feature", "Add a new feature" }
    };

    public static IEnumerable<Command> CreateAddCommands() =>
        _descriptions.Select(kvp => CreateCommand(kvp.Key, kvp.Value));

    private static Command CreateCommand(string name, string description)
    {
        var cmd = new Command(name, description);
        cmd.Arguments.Add(new Argument<string>("name"));
        cmd.SetAction(parseResult =>
        {
            string? name = parseResult.GetValue<string>("name");
            Console.WriteLine($"ignite: running '{cmd.Name} {name}'...");
        });
        return cmd;
    }
}
