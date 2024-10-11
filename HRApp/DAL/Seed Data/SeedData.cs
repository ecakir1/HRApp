﻿using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace HRApp.DAL.Seed_Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdminUser(RoleManager<IdentityRole<Guid>> roleManager, UserManager<Employee> userManager)
        {
            // Admin rolünü ekleyin
            var adminRole = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid> { Name = adminRole });
            }

            // Admin kullanıcısını ekleyin
            var adminEmail = "admin@example.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new Employee
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}
