﻿using LagoVista.IoT.Web.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using System;
using LagoVista.Core.PlatformSupport;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LagoVista.UserAdmin.Managers;
using LagoVista.Core.Models.UIMetaData;
using LagoVista.UserAdmin.Models.Account;
using LagoVista.UserAdmin.Interfaces.Managers;

namespace LagoVista.UserManagement.Rest
{
    /// <summary>
    /// User Services
    /// </summary>
    [Authorize]
    public class UserServices : LagoVistaBaseController
    {
        IAppUserManager _appUserManager;
        IOrganizationManager _orgManager;
        public UserServices(IAppUserManager appUserManager, IOrganizationManager orgManager, UserManager<AppUser> userManager, ILogger logger) : base(userManager, logger)
        {
            _appUserManager = appUserManager;
            _orgManager = orgManager;
        }

        /// <summary>
        /// User Service - Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/user/{id}")]
        public async Task<DetailResponse<AppUser>> GetUserAsync(String id)
        {
            var appUser = await _appUserManager.GetUserByIdAsync(id, UserEntityHeader);
            appUser.PasswordHash = null;
            return DetailResponse<AppUser>.Create(appUser);
        }

        /// <summary>
        /// User Service - Get by User Name (generally email)
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [HttpGet("/api/user")]
        public async Task<DetailResponse<AppUser>> GetCurrentUser()
        {
            var appUser = await _appUserManager.GetUserByIdAsync(UserEntityHeader.Id, UserEntityHeader);
            appUser.PasswordHash = null;
            return DetailResponse<AppUser>.Create(appUser);
        }

    }
}