using SafeIn_Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafeInApiLocal.Models;
using SafeIn_Api.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SafeIn_Api.Controllers
{
    [Authorize(Roles ="Employee,Admin,SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private AppDbContext _context;
        private UserManager<Employee> _userManager;
        public EmployeeController(ILogger<EmployeeController> logger, AppDbContext context, UserManager<Employee> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("information")]
        public IActionResult Get()
        {
            InfoResponse response = new InfoResponse();
            var id = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(id).Result;
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.Company =  _context.Companies.FindAsync(user.CompanyId).Result.Name;
            response.Role = _userManager.GetRolesAsync(user).Result[0];

            return Ok(response);
        }


    }
}
