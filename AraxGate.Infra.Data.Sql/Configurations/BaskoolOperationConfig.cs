using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AraxGate.Core.Domain.Entities.Operation;

namespace AraxGate.Infra.Data.Sql.Configurations;

internal class BaskoolOperationConfig : IEntityTypeConfiguration<BaskoolOperation>
{
    public void Configure(EntityTypeBuilder<BaskoolOperation> builder)
    {
        builder.ToTable("BaskoolOperations", "Operation");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.BaskoolType).IsRequired();
        builder.Property(x => x.Weight).IsRequired().HasColumnType("float");
        builder.Property(x => x.CreateDate).IsRequired();
    }
}
