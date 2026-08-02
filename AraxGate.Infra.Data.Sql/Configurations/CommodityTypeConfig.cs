using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AraxGate.Core.Domain.Entities.Basic;

namespace AraxGate.Infra.Data.Sql.Configurations;
internal class CommodityTypeConfig : IEntityTypeConfiguration<CommodityType>
{
    public void Configure(EntityTypeBuilder<CommodityType> builder)
    {
        builder.ToTable("CommodityTypes", "Basic");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CommodityTypeName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(4000);

        #region Navigation
        builder.HasMany(c => c.GateEntrances).WithOne(d => d.CommodityType).HasForeignKey("CommodityTypeId");
        #endregion
    }
}
