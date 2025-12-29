using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime.Credentials;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using $safeprojectname$;

AWSCredentials awsCredentials = DefaultAWSCredentialsIdentityResolver.GetCredentials();
RegionEndpoint regionEndpoint = FallbackRegionFactory.GetRegionEndpoint();

Parser.Default.ParseArguments<CommandLineOptions>(args)
    .WithParsed(co =>
    {
        if (co.Profile is not null)
        {
            SharedCredentialsFile sharedfile = new();
            if (!sharedfile.TryGetProfile(co.Profile, out CredentialProfile profile))
            {
                Console.WriteLine($"Could not find profile {co.Profile}");
                return;
            }

            AWSCredentialsFactory.TryGetAWSCredentials(profile, sharedfile, out awsCredentials);
            regionEndpoint = profile.Region;
        }

        if (string.IsNullOrEmpty(co.Region)) return;

        RegionEndpoint tmp = RegionEndpoint.GetBySystemName(co.Region);
        if (tmp.DisplayName == "Unknown")
        {
            Console.WriteLine($"Unknown region {co.Region}. Using default {regionEndpoint.DisplayName}");
        }
        else
        {
            regionEndpoint = tmp;
        }
    });

IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
	//.AddUserSecrets<$safeprojectname$Service>()
    .Build();
configuration["value"] = "something";

IServiceCollection services = new ServiceCollection();

services.AddSingleton<I$safeprojectname$Service, $safeprojectname$Service>();
// Use this as guidance for AWS service that you want to use
// And pass through Dependency Injection to $safeprojectname$Service:
// public class $safeprojectname$Service(IAmazonSecretsManager secret)
/*
services.AddSingleton<IAmazonSecretsManager>(_ => new AmazonSecretsManagerClient(awsCredentials, regionEndpoint));
*/

services.AddSingleton<IConfiguration>(configuration);

ServiceProvider serviceProvider = services.BuildServiceProvider();
IServiceScope scope = serviceProvider.CreateScope();

await scope.ServiceProvider.GetRequiredService<I$safeprojectname$Service>().Run();

await serviceProvider.DisposeAsync();