using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AraxGate.Infrastructure;

namespace AraxGate.Infra.Data.Sql;

public class AraxGateDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AraxGateDbContext>
{
    public AraxGateDbContext CreateDbContext(string[] args)
    {
        string cnn = @"Server=.;Initial Catalog=SinaOTOSDB;User ID=sa; Password=123qwe!@#; Encrypt=false;";
        var options = new DbContextOptionsBuilder().UseSqlServer(cnn).Options;
        return new AraxGateDbContext(options);
    }
}
