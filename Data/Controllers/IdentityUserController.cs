using ESOCompanion.Data.DBContexts;
using DataAccessLibrary.Models.UserModels;
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
    public class IdentityUserController : ControllerBase
    {
        private ApplicationDbContext appDbContext;

        public IdentityUserController(ApplicationDbContext context)
        {
            appDbContext = context;
        }

        [HttpGet]
        public IList<IdentityUser<string>> Get()
        {
            return (this.appDbContext.IdentityUsers.ToList());
        }
    }
}