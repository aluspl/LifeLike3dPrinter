using Domain.Filament;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Configurations;

public class FilamentConfiguration : IEntityTypeConfiguration<Filament>
{
    public void Configure(EntityTypeBuilder<Filament> builder)
    {
        builder.ToTable("Filaments");
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

        builder.Property(x => x.Brand).IsRequired();
        builder.Property(x => x.Color).IsRequired();
        builder.Property(x => x.FilamentType).IsRequired();
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.FilamentId);
    }
}