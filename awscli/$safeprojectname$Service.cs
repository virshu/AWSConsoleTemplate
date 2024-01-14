using Microsoft.Extensions.Configuration;

namespace $safeprojectname$;

public interface I$safeprojectname$Service
{
    Task Run();
}

public class $safeprojectname$Service(IConfiguration configuration) : I$safeprojectname$Service
{
    public async Task Run()
    {
        await Console.Out.WriteAsync($"Completed {configuration["value"]}");
    }
}
