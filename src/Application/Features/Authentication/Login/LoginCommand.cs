using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Login
{
    public record LoginCommand(string Email, string Password);
}
