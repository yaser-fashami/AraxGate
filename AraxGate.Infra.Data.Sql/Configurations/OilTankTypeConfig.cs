using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AraxGate.Core.Domain.Entities.Basic;

namespace AraxGate.Infra.Data.Sql.Configurations;

internal class OilTankTypeConfig : IEntityTypeConfiguration<OilTankType>
{
    public void Configure(EntityTypeBuilder<OilTankType> builder)
    {
        builder.ToTable("OilTankTypes", "Basic");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TankName).IsRequired().HasMaxLength(10);
        builder.Property(x => x.TankGroup).IsRequired().HasMaxLength(3);
        builder.Property(x => x.Description).HasMaxLength(4000);

        #region Navigation
        builder.HasMany(c => c.GateEntrances).WithOne(d => d.OilTankType).HasForeignKey("OilTankTypeId");
        #endregion

    }
}
