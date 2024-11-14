using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Domain;

namespace Monitoring.Data.Database.EntityConfigurations;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("buildings");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.LocationX)
            .HasColumnName("locationX")
            .IsRequired();
        
        builder.Property(x => x.LocationY)
            .HasColumnName("locationY")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id");
        
        builder.HasMany(x => x.Sensors)
            .WithOne(x => x.Building)
            .HasForeignKey(x => x.BuildingId);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Buildings)
            .HasForeignKey(x => x.UserId);
    }
}