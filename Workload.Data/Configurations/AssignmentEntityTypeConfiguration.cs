namespace Workload.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Workload.Model;

public class AssignmentEntityTypeConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        _ = builder.Property(p => p.Description).HasMaxLength(128);
    }
}