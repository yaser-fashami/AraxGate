using Microsoft.Extensions.Configuration;
using SinaOTOS.Core.Domain.Interfaces;
using System.Reflection;

namespace SinaOTOS.Services.ApplicationServices;

public class AppVersionService : IAppVersionService
{
    private readonly IConfiguration _configuration;

    public AppVersionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Version => Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version.Substring(0,5);

}
