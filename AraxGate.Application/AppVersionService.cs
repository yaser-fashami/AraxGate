using Microsoft.Extensions.Configuration;
using AraxGate.Core.Domain.Interfaces;
using System.Reflection;

namespace AraxGate.Application;

public class AppVersionService : IAppVersionService
{
    private readonly IConfiguration _configuration;

    public AppVersionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Version => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version.Substring(0, 5);

}
