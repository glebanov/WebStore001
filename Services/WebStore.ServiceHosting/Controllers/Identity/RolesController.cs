﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebAPI.Identity.Role)]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleStore<Role> _RoleStore;

        public RolesController(WebStoreDB db)
        {
            _RoleStore = new RoleStore<Role>(db);
        }
    }
}