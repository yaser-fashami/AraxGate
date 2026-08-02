using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AraxGate.Core.Domain.Entities.Basic;

namespace AraxGate.Infra.Data.Sql.Configurations;
internal class BaskoolSettingConfig : IEntityTypeConfiguration<BaskoolSetting>
{
    public void Configure(EntityTypeBuilder<BaskoolSetting> builder)
    {
        builder.ToTable("BaskoolSettings", "Basic");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BaskoolMACAddress).IsRequired().HasMaxLength(200);
    }
}
