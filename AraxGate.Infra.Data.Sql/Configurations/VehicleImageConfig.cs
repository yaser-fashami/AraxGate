using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AraxGate.Core.Domain.Entities.Operation;

namespace AraxGate.Infra.Data.Sql.Configurations;

public class VehicleImageConfig : IEntityTypeConfiguration<VehicleImage>
{
    public void Configure(EntityTypeBuilder<VehicleImage> builder)
    {
        builder.ToTable("VehicleImages", "Operation");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ImageData).HasColumnType("varbinary(max)");
        builder.Property(x => x.ImagePath).HasColumnType("varchar(500)");
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");

        #region Navigation
        builder.HasOne(c => c.GateIn).WithOne(d => d.GateInFrontPlateVehicleImage).HasForeignKey<GateEntrance>(e => e.GateInFrontPlateVehicleImageId);
        builder.HasOne(c => c.GateOut).WithOne(d => d.GateOutFrontPlateVehicleImage).HasForeignKey<GateEntrance>(e => e.GateOutFrontPlateVehicleImageId);
        #endregion


    }
}
