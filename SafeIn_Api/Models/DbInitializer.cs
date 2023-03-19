using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SafeIn_Api.Models;
using System;
using System.Linq;

namespace SafeIn_Api.Models
{
    public static class DbInitializer
    {
        public static async void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                var _userManager =
                         serviceScope.ServiceProvider.GetService<UserManager<Employee>>();
                var _roleManager =
                         serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                //creating default company Constanta
                if (!context.Companies.Any(n => n.Name == "Constanta"))
                {
                    var company = new Company()
                    {
                        CompanyId = Guid.NewGuid().ToString(),
                        Name = "Constanta"
                    };
                    context.Companies.Add(company);
                    context.SaveChanges();
                }
                //add superadmin for that company
                //var companyConstanta = context.Companies.Find("Constanta");
                var companyConstanta = context.Companies
                    .Where(b => b.Name == "Constanta")
                    .FirstOrDefault();

                if (!context.Users.Any(usr => usr.Email == "khrystyna-yaryna.kolba@lnu.edu.ua"))
                {
                    var user = new Employee()
                    {
                        UserName = "KhrystynaKolba",
                        Email = "khrystyna-yaryna.kolba@lnu.edu.ua",
                        //CompanyId = companyConstanta.CompanyId,
                        Company = companyConstanta
                    };
                    var userResult = await _userManager.CreateAsync(user, "String-123");
                    context.SaveChanges();
                    //   companyConstanta.Employees.Add(user);
                    //}
                }


                if (!context.Users.Any(usr => usr.Email == "vitalii.seniuk@lnu.edu.ua"))
                {
                    var user = new Employee()
                    {
                        UserName = "VitaliiSeniuk",
                        Email = "vitalii.seniuk@lnu.edu.ua",
                        //CompanyId = companyConstanta.CompanyId,
                        Company = companyConstanta
                    };
                    var userResult = await _userManager.CreateAsync(user, "String-123");
                    context.SaveChanges();
                    //   companyConstanta.Employees.Add(user);
                    //}
                }
                if (!context.Users.Any(usr => usr.Email == "user@lnu.edu.ua"))
                {
                    var user = new Employee()
                    {
                        UserName = "user",
                        Email = "user@lnu.edu.ua",
                        //CompanyId = companyConstanta.CompanyId,
                        Company = companyConstanta
                    };
                    var userResult = await _userManager.CreateAsync(user, "String-123");
                    context.SaveChanges();
                    //   companyConstanta.Employees.Add(user);
                    //}
                }
                //add roles
                //foreach (IdentityRole role in _roleManager.Roles)
                //{
                //    _roleManager.DeleteAsync(role).GetAwaiter().GetResult();
                //}
                if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole("SuperAdmin")).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole("Employee")).GetAwaiter().GetResult();
                }

                //making SuperAdmin
                var superAdminUser = _userManager.FindByEmailAsync("khrystyna-yaryna.kolba@lnu.edu.ua").Result;
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                //making admin
                var adminUser = _userManager.FindByEmailAsync("vitalii.seniuk@lnu.edu.ua").Result;
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                //making employee
                var Employee = _userManager.FindByEmailAsync("user@lnu.edu.ua").Result;
                await _userManager.AddToRoleAsync(adminUser, "Employee");
                //await _userManager.RemoveFromRolesAsync(adminUser, new string[] {"Employee" , "Admin"});
                //add first test door 
                if (!context.Doors.Any(door => door.Company == companyConstanta))
                {
                    var door = new Door()
                    {
                        DoorId = Guid.NewGuid().ToString(),
                        Company = companyConstanta
                    };
                    //companyConstanta.Doors.Add(door);
                    context.Doors.Add(door);
                    context.SaveChanges();
                }
                context.SaveChanges();

            }
        }
    }
}
