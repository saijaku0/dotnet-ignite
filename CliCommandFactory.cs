using System;

namespace 

public class CliCommandFactory
{
	private static readonly Dictionary<string, string> _command = new()
    {
        { "entity", "Add a new entity" },
        { "handler", "Add a new handler" },
        { "query", "Add a new query" },
        { "command", "Add a new command" },
        { "repository", "Add a new repository" },
        { "endpoint", "Add a new endpoint" },
        { "feature", "Add a new feature" }
    };

    public static IEnumerable<Command> CreateAddCommands()
    {
        return _command.Select(kvp => CreateCommand(kvp.Key, kvp.Value));
    }

    private static Command CreateCommand(string name, string description)
    {
        var cmd = new Command(name, description);
        cmd.AddArgument(new Argument<string>("name"));
        return cmd;
    }
}
