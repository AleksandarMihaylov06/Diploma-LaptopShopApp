﻿using LaptopShopApp.Data;
using LaptopShopApp.Infrastructure.Data.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApp.Infrastructure.Data.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrands(dataBrand);

            return app;
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.UserName = "admin";
                user.Email = "admin@admin.com";
                user.Address = "admin address";
                user.PhoneNumber = "0888888888";

                var result = await userManager.CreateAsync
                    (user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category {CategoryName="Laptop"},
                new Category {CategoryName="Mouse"},
                new Category {CategoryName="Monitor"},
                new Category {CategoryName="Keyboard"},
                new Category {CategoryName="Web camera"},
                new Category {CategoryName="Headphone"},
                new Category {CategoryName="Microphone"},
                new Category {CategoryName="Flash dirve"},
                new Category {CategoryName="External HDD hard drives"},
            });
            dataCategory.SaveChanges();
        }
        private static void SeedBrands(ApplicationDbContext dataBrands)
        {
            if (dataBrands.Brands.Any())
            {
                return;
            }
            dataBrands.Brands.AddRange(new[]
            {
                new Brand {BrandName="Acer"},
                new Brand {BrandName="Asus"},
                new Brand {BrandName="Apple"},
                new Brand {BrandName="Dell"},
                new Brand {BrandName="HP"},
                new Brand {BrandName="Apple"},
                new Brand {BrandName="Logitech"},
                new Brand {BrandName="Cougar"},
                new Brand {BrandName="Razer"},
                new Brand {BrandName="Canyon"},
                new Brand {BrandName="Lenovo"},
            });
            dataBrands.SaveChanges();
        }
    }
}
