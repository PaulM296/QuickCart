﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuickCart.Api.DTOs;
using QuickCart.Api.Extensions;
using QuickCart.Domain.Entities;

namespace QuickCart.Api.Controllers
{
    public class AccountController(SignInManager<AppUser> signInManager) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password); 

            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false)
                return NoContent();

            var user = await signInManager.UserManager.GetUserByEmail(User);

            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email
            });
        }

        [HttpGet]
        public ActionResult GetAuthState()
        {
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated ?? false
            });
        }
    }
}
