using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeIn_Api.Controllers;
using SafeIn_Api.Models;

namespace SafeIn_Api.Controllers
{
    [Authorize(Roles="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private AppDbContext _context;
        private UserManager<Employee> _userManager;
        public AdminController(ILogger<AdminController> logger, AppDbContext context, UserManager<Employee> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        private IActionResult BadRequestErrorMessages()
        {
            var errMsgs = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(errMsgs);
        }

        [HttpPost("doors")]
        public async Task<ActionResult<Door>> AddDoor()
        {
            var id = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(id).Result;

            var door = new Door()
            {
                DoorId = Guid.NewGuid().ToString(),
                Company = _context.Companies.FindAsync(user.CompanyId).Result

            };
            _context.Doors.Add(door);
            _context.SaveChanges();
            return StatusCode(201);
        }



        [HttpPost("employee")]
        public async Task<IActionResult> RegisterEmployee(RegisterRequest registerRequest)
        {
            var id = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(id).Result;
            var company = _context.Companies.FindAsync(user.CompanyId).Result;

            if (!ModelState.IsValid)
            {
                return BadRequestErrorMessages();
            }

            var isEmailAlreadyRegistered = await _userManager.FindByEmailAsync(registerRequest.Email) != null;

            if (isEmailAlreadyRegistered)
            {
                return Conflict($"Email {registerRequest.Email} is already registered.");
            }

            var newUser = new Employee
            {
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                Company = company
            };

            var result = await _userManager.CreateAsync(newUser, registerRequest.Password);
            _context.SaveChanges();
            var newEmployee = _userManager.FindByEmailAsync(registerRequest.Email).Result;
            await _userManager.AddToRoleAsync(newEmployee, "Employee");
            if (result.Succeeded)
            {
                return Ok($"New employee of {company.Name} company is  created successfully");
            }
            else
            {
                return StatusCode(500, result.Errors.Select(e => new { Msg = e.Code, Desc = e.Description }).ToList());
            }
        }
        //[HttpDelete("employee")]
        //public async Task<IActionResult> DeleteEmployee(string email)
        //{
        //    var id = _userManager.GetUserId(User);
        //    var admin = _userManager.FindByIdAsync(id).Result;
        //    var user = _userManager.FindByEmailAsync(email).Result;
        //    var company = _context.Companies.FindAsync(user.CompanyId).Result;

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequestErrorMessages();
        //    }

        //    var result = await _userManager.DeleteAsync(user);
        //    _context.SaveChanges();
        //    if (result.Succeeded)
        //    {
        //        return Ok($"Employee of {company.Name} company is deleted successfully");
        //    }
        //    else
        //    {
        //        return StatusCode(500, result.Errors.Select(e => new { Msg = e.Code, Desc = e.Description }).ToList());
        //    }
        //}

    }
}
