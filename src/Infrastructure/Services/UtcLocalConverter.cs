using Application.Abstraction;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class UtcLocalConverter : IUtcLocalConverter
    {
        public Instant ConvertLocalToUtc(DateTime localTime, string zone)
        {
            var localDateTime = LocalDateTime.FromDateTime(localTime);

            DateTimeZone dateTimeZone = DateTimeZoneProviders.Tzdb[zone];

            ZonedDateTime zoned = localDateTime.InZoneLeniently(dateTimeZone);

            return zoned.ToInstant();
        }

        public DateTime ConvertUtcToLocal(Instant utc, string userZone)
        {
            var zone = DateTimeZoneProviders.Tzdb[userZone];

            var zonedDateTime = utc.InZone(zone);

            return zonedDateTime.ToDateTimeUnspecified();
        }
    }
}
