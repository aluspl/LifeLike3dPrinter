using Domain.Filament;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Configurations;

public class FilamentItemConfiguration : IEntityTypeConfiguration<FilamentItem>
{
    public void Configure(EntityTypeBuilder<FilamentItem> builder)
    {
        builder.ToTable("FilamentItems");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder
            .Property(entity => entity.Created)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()");

        builder
            .Property(entity => entity.Updated)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()")
            .IsConcurrencyToken();

    }
}