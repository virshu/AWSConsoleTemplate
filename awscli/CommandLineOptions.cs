using CommandLine;

namespace $safeprojectname$;

internal class CommandLineOptions
{
    [Option('p', "profile", Required = false, 
        HelpText = "Profile name to use for credentials. Leave blank to use default or IAM Role credentials")]
    public string? Profile { get; set; }

    [Option('r', "region", Required = false, 
        HelpText = "Region to use for operations. Leave blank to use default or IAM Role credentials")]
    public string? Region { get; set; }
}