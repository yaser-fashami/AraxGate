using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AraxGate.Core.Domain.Entities;
using AraxGate.Core.Domain.Entities.Basic;
using AraxGate.Core.Domain.Entities.Operation;
using AraxGate.Core.Domain.KeylessEntities;
using AraxGate.Utilities;
using AraxGate.Infra.Data.Sql.Configurations;

namespace AraxGate.Infrastructure;

public class AraxGateDbContext : IdentityDbContext<User>
{
	public AraxGateDbContext(DbContextOptions options) : base(options)
	{

	}

	#region Basic
	public DbSet<Currency> Currencies { get; set; }
    public DbSet<TruckType> TruckTypes { get; set; }
    public DbSet<OilTankType> OilTankTypes { get; set; }
    public DbSet<CommodityType> CommodityTypes { get; set; }
    public DbSet<Consignee> Consignees { get; set; }
    public DbSet<BaskoolSetting> BaskoolSettings { get; set; }

    #endregion

    #region Operation
    public DbSet<GateEntrance> GateEntrances { get; set; }
    public DbSet<BaskoolOperation> BaskoolOperations { get; set; }
    public DbSet<VehicleImage> VehicleImages { get; set; }
    #endregion


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
		optionsBuilder.UseExceptionProcessor();
		optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfiguration(new UserConfig());
		builder.ApplyConfiguration(new CurrencyConfig());
		builder.ApplyConfiguration(new TruckTypeConfig());
		builder.ApplyConfiguration(new GateEntranceConfig());
		builder.ApplyConfiguration(new CommodityTypeConfig());
		builder.ApplyConfiguration(new ConsigneeConfig());
		builder.ApplyConfiguration(new BaskoolSettingConfig());
		builder.ApplyConfiguration(new BaskoolOperationConfig());
		builder.ApplyConfiguration(new VehicleImageConfig());

	#region Conversions
	builder.Entity<GateEntrance>().Property(c => c.Baskool).HasConversion
		(
			v => v.ToString(),
			v => (BaskoolType)Enum.Parse(typeof(BaskoolType), v)
		);
	builder.Entity<GateEntrance>().Property(c => c.BaskoolOut).HasConversion
		(
			v => v.ToString(),
			v => (BaskoolType)Enum.Parse(typeof(BaskoolType), v)
		);
	builder.Entity<GateEntrance>().Property(c => c.PlateType).HasConversion
		(
			v => v.ToString(),
			v => (PlateType)Enum.Parse(typeof(PlateType), v)
		);
	builder.Entity<OilTankType>().Property(c => c.TankType).HasConversion
		(
			v => v.ToString(),
			v => (TankType)Enum.Parse(typeof(TankType), v)
		);
	builder.Entity<Consignee>().Property(c => c.ConsigneeType).HasConversion
		(
			v => v.ToString(),
			v => (ConsigneeType)Enum.Parse(typeof(ConsigneeType), v)
		);

	#endregion

		builder.Entity<DataForDashboardChart1_Proc>()
			.HasNoKey();


		var user = new User() { Id = Guid.NewGuid().ToString(), UserName="admin", PasswordHash = Util.GetHashString("123"), IsActived = true };

		builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "1", Name = "admin", NormalizedName = "admin" },
												new IdentityRole() { Id = "2", Name = "default", NormalizedName = "default" });

		builder.Entity<User>().HasData(user);
		builder.Entity<IdentityUserRole<string>>().HasData(new { RoleId = "1", UserId = user.Id });

	}

}
