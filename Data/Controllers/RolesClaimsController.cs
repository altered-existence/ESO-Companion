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
    public class RolesClaimsController : ControllerBase
    {
        private ApplicationDbContext appDbContext;

        public RolesClaimsController(ApplicationDbContext context)
        {
            appDbContext = context;
        }

        [HttpGet]
        public IList<IdentityRoleClaim<string>> Get()
        {
            return (this.appDbContext.RoleClaims.ToList());
        }
    }
}