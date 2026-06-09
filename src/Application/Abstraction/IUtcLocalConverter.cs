using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstraction
{
    public interface IUtcLocalConverter
    {
        DateTime ConvertUtcToLocal(Instant utc, string userZone);
    }

}
