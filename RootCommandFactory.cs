namespace CliRegistration;

public static class CliRegistration
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCli(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var rootCommand = new RootCommand("A sample CLI application.");
                rootCommand.AddCommand(new GreetCommand());
                return rootCommand;
            });

            return services;
        }
    }
}
