using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Workouts.Create
{
    public interface ICurrentUserAccessor
    {
        Guid GetId();
    }
}
