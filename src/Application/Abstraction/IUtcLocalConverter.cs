using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstraction
{
    public interface IUtcLocalConverter
    {
        Instant ConvertLocalToUtc(DateTime localTime, string userZone);
        DateTime ConvertUtcToLocal(Instant utc, string userZone);
    }

}
