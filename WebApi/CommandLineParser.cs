using CommandLine;

namespace WebApi
{
    public record DatabaseOptions
    {
        [Option('c', "connectionString", Required = true, HelpText = "Connection string for the database")]
        public string ConnectionString { get; init; } = string.Empty;
    }

    public static class CommandLineParser
    {
        private static readonly Parser Parser;

        static CommandLineParser()
        {
            Parser = new Parser(config =>
            {
                config.IgnoreUnknownArguments = true;
                config.AutoHelp = true;
                config.HelpWriter = Console.Error;
            });
        }

        public static bool IsRunningMigrationMode(string[] args)
        {
            return args.Contains("--RunMigrations");
        }

        public static string GetConnectionString(string[] args)
        {
            var connectionString = Parser.ParseArguments<DatabaseOptions>(args)
                .MapResult(
                    options => options.ConnectionString,
                    _ => string.Empty);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Command line arguments are invalid");
            }

            return connectionString;
        }
    }
}
