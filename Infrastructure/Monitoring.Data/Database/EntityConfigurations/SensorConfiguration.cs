using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Domain;

namespace Monitoring.Data.Database.EntityConfigurations;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.ToTable("sensors");
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(x => x.LocationX)
            .HasColumnName("location_x")
            .IsRequired();
        
        builder.Property(x => x.LocationY)
            .HasColumnName("location_y")
            .IsRequired();
        
        builder.Property(x => x.PhotoUrl)
            .HasColumnName("photo_url")
            .IsRequired();

        builder.Property(x => x.BuildingId)
            .HasColumnName("building_id");

        builder.HasOne(x => x.Building)
            .WithMany(x => x.Sensors)
            .HasForeignKey(x => x.BuildingId);

        builder.Property(x => x.BatteryLevel)
            .HasColumnName("battery_level")
            .IsRequired();
    }
}