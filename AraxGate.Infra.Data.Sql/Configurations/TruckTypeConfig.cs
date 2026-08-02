using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AraxGate.Core.Domain.Entities.Basic;

namespace AraxGate.Infra.Data.Sql.Configurations;
internal class TruckTypeConfig : IEntityTypeConfiguration<TruckType>
{
    public void Configure(EntityTypeBuilder<TruckType> builder)
    {
        builder.ToTable("TruckTypes", "Basic");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TruckTypeName).IsRequired().HasMaxLength(50).IsUnicode(true);
        builder.Property(x => x.Description).IsUnicode(true).HasMaxLength(4000);

        #region Navigation
        builder.HasMany(c => c.GateEntrances).WithOne(d => d.TruckType).HasForeignKey("TruckTypeId");
        #endregion

    }
}
