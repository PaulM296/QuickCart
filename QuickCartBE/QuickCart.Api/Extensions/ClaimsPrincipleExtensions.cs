﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickCart.Domain.Entities;
using System.Security.Authentication;
using System.Security.Claims;

namespace QuickCart.Api.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users.FirstOrDefaultAsync(x => 
            x.Email == user.GetEmail());

            if (userToReturn == null)
                throw new AuthenticationException("User not found");

            return userToReturn;
        }

        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Email == user.GetEmail());

            if (userToReturn == null)
                throw new AuthenticationException("User not found");

            return userToReturn;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                throw new AuthenticationException("Email claim not found");

            return email;
        }
    }
}
