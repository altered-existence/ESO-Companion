using ESOCompanion.Data.DBContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESOCompanion.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginsController : ControllerBase
    {
        private ApplicationDbContext appDbContext;

        public UserLoginsController(ApplicationDbContext context)
        {
            appDbContext = context;
        }

        [HttpGet]
        public IList<IdentityUserLogin<string>> Get()
        {
            return (this.appDbContext.UserLogins.ToList());
        }
    }
}