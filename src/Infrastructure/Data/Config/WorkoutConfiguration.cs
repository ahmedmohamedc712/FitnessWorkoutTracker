using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace Infrastructure.Data.Config
{
    internal class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Title).HasMaxLength(50);

            builder.Property(x => x.Description).HasMaxLength(250);

            var instantConverter = new ValueConverter<Instant, DateTime>(
                v => v.ToDateTimeUtc(),
                v => Instant.FromDateTimeUtc(DateTime.SpecifyKind(v, DateTimeKind.Utc))
            );

            builder.Property(x => x.CreatedAt)
                .HasConversion(instantConverter)
                .HasColumnType("datetime2");

            builder.HasOne(x => x.User)
                .WithMany(u => u.Workouts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
