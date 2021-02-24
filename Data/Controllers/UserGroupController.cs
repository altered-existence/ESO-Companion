﻿using ESOCompanion.Data.DBContexts;
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
    public class UserGroupController : ControllerBase
    {
        private ApplicationDbContext appDbContext;

        public UserGroupController(ApplicationDbContext context)
        {
            appDbContext = context;
        }

        [HttpGet]
        public IList<UserGroup> Get()
        {
            return (this.appDbContext.UserGroups.ToList());
        }
    }
}