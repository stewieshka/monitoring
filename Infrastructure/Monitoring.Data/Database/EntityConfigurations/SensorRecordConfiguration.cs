using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monitoring.Domain;

namespace Monitoring.Data.Database.EntityConfigurations;

public class SensorRecordConfiguration : IEntityTypeConfiguration<SensorRecord>
{
    public void Configure(EntityTypeBuilder<SensorRecord> builder)
    {
        builder.ToTable("sensor_records");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id");
        
        builder.Property(x => x.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(x => x.Temperature)
            .HasColumnName("temperature")
            .IsRequired();
        
        builder.Property(x => x.Humidity)
            .HasColumnName("humidity")
            .IsRequired();

        builder.Property(x => x.SensorId)
            .HasColumnName("sensor_id")
            .IsRequired();

        builder.HasOne(x => x.Sensor)
            .WithMany(x => x.SensorRecords)
            .HasForeignKey(x => x.SensorId);

    }
}