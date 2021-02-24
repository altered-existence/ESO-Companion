using ESOCompanion.Data.DBContexts;
using DataAccessLibrary.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESOCompanion.Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApplicationDbContext appDbContext;

        public UserController(ApplicationDbContext context)
        {
            appDbContext = context;
        }

        [HttpGet]
        public IList<User> Get()
        {
            return (this.appDbContext.MyUsers.ToList());
        }
    }
}