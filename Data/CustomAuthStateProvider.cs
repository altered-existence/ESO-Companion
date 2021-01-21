using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ESOCompanion.Data
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        string UserId = "";
        string Password = "";

        public void LoadUser(string _UserId, string _Password)
        {
            UserId = _UserId;
            Password = _Password;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, Password),
            }, "Fake authentication type");

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }
    }
}