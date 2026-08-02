using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AraxGate.Core.Domain.Entities.Operation;

namespace AraxGate.Infra.Data.Sql.Configurations;

internal class GateEntranceConfig : IEntityTypeConfiguration<GateEntrance>
{
    public void Configure(EntityTypeBuilder<GateEntrance> builder)
    {
        builder.ToTable("GateEntrances", "Operation");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.GateInDate).IsRequired().HasColumnType("datetime2");
        builder.Property(x => x.GateOutDate).HasColumnType("datetime2");
        builder.Property(x => x.TruckNo).IsRequired().HasMaxLength(20);
        builder.Property(x => x.TruckNoletter).HasMaxLength(10);
        builder.Property(x => x.PlateType).HasMaxLength(10);
        builder.Property(x => x.GateEntranceNo).IsRequired().IsUnicode(false).HasMaxLength(15);
        builder.Property(x => x.Baskool).HasMaxLength(20).HasConversion<string>();
        builder.Property(x => x.BaskoolOut).HasMaxLength(20).HasConversion<string>();
        builder.Property(x => x.GateInWeight).IsRequired();
        builder.Property(x => x.CustomPermissionNo).IsRequired().HasMaxLength(20);
        builder.Property(x => x.DriverName).HasMaxLength(100);

        builder.HasIndex(x => x.GateInFrontPlateVehicleImageId)
       .IsUnique()
       .HasFilter("[GateInFrontPlateVehicleImageId] IS NOT NULL");

        builder.HasIndex(x => x.GateOutFrontPlateVehicleImageId)
        .IsUnique()
        .HasFilter("[GateOutFrontPlateVehicleImageId] IS NOT NULL");

        #region Navigation
        builder.HasOne(c => c.GateInFrontPlateVehicleImage).WithOne().HasForeignKey<GateEntrance>(d => d.GateInFrontPlateVehicleImageId);
        builder.HasOne(c => c.GateOutFrontPlateVehicleImage).WithOne().HasForeignKey<GateEntrance>(d => d.GateOutFrontPlateVehicleImageId);
        #endregion

    }
}
