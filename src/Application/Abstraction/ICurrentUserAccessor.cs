using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstraction
{
    public interface ICurrentUserAccessor
    {
        Guid GetId();
    }
}
