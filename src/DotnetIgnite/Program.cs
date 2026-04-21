using System.CommandLine;
using DotnetIgnite.Cli.Commands;

string description = "The following commands are registered: entity, handler, query, command, repository, endpoint, feature - each accepts a <Name> argument";

var addCommand = new Command("add", description);
foreach (Command cmd in CliCommandFactory.CreateAddCommands())
    addCommand.Subcommands.Add(cmd);

var rootCommand = new RootCommand("dotnet-ignite - CLI scaffolding tool");

rootCommand.Subcommands.Add(new Command("init", "Initialize ignite in your project"));
rootCommand.Subcommands.Add(new Command("config", "Update project configuration"));
rootCommand.Subcommands.Add(addCommand);

return await rootCommand.Parse(args).InvokeAsync();
