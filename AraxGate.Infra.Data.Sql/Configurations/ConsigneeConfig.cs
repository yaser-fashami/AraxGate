using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AraxGate.Core.Domain.Entities.Basic;

namespace AraxGate.Infra.Data.Sql.Configurations;
internal class ConsigneeConfig : IEntityTypeConfiguration<Consignee>
{
    public void Configure(EntityTypeBuilder<Consignee> builder)
    {
        builder.ToTable("Consignees", "Basic");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ConsigneeName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.ConsigneeNameEng).IsRequired().IsUnicode(false).HasMaxLength(200);
        builder.Property(x => x.TelNo).HasMaxLength(200);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.Address).IsUnicode(true).HasMaxLength(500);
        builder.Property(x => x.City).IsUnicode(true).HasMaxLength(20);
        builder.Property(x => x.PostalCode).HasMaxLength(20);
        builder.Property(x => x.Description).IsUnicode(true).HasMaxLength(4000);
        builder.Property(x => x.ConsigneeType).IsRequired().IsUnicode(true).HasMaxLength(50);
        builder.Property(x => x.NationalCode).IsUnicode(false).HasMaxLength(20);
        builder.Property(x => x.EconomicCode).IsUnicode(false).HasMaxLength(20);

        #region Navigation
        builder.HasMany(c => c.GateEntrances).WithOne(d => d.Consignee).HasForeignKey("ConsigneeId");
        #endregion

    }
}
