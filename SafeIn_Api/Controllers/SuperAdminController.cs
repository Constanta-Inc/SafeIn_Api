using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafeIn_Api.Models;

namespace SafeIn_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : ControllerBase
    {
        private readonly ILogger<SuperAdminController> _logger;
        private AppDbContext _context;
        private UserManager<Employee> _userManager;
        public SuperAdminController(ILogger<SuperAdminController> logger, AppDbContext context, UserManager<Employee> userManager)
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


        [HttpPost("register-new-company")]
        public async Task<IActionResult> RegisterCompany(String CompanyName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestErrorMessages();
            }
         
            if (!_context.Companies.Any(n => n.Name == CompanyName))
            {
                var company = new Company()
                {
                    CompanyId = Guid.NewGuid().ToString(),
                    Name = CompanyName
                };
                _context.Companies.Add(company);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return Conflict($"Company with name {CompanyName} is already registered.");
            }
        }
        
        [HttpPost("admin")]
        public async Task<IActionResult> RegisterEmployee(SafeInApiLocal.Models.AdminRegisterRequest registerRequest)
        {
            var id = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(id).Result;
            Company company = _context.Companies.Where(a => a.Name == registerRequest.CompanyName).Single();

            if (!ModelState.IsValid)
            {
                return BadRequestErrorMessages();
            }

            var isEmailAlreadyRegistered = await _userManager.FindByEmailAsync(registerRequest.Email) != null;

            if (isEmailAlreadyRegistered)
            {
                return Conflict($"Email Id {registerRequest.Email} is already registered.");
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
            await _userManager.AddToRoleAsync(newEmployee, "Admin");
            if (result.Succeeded)
            {
                return Ok($"New admin of {company.Name} company is  created successfully");
            }
            else
            {
                return StatusCode(500, result.Errors.Select(e => new { Msg = e.Code, Desc = e.Description }).ToList());
            }
        }


        //[HttpDelete("admin")]
        //public async Task<IActionResult> DeleteEmployee(string adminId)
        //{
        //    //var id = _userManager.GetUserId(User);
        //    var user = _userManager.FindByIdAsync(adminId).Result;
        //    var company = _context.Companies.Find(user.CompanyId);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequestErrorMessages();
        //    }
        //    var result = await _userManager.DeleteAsync(user);
        //    _context.SaveChanges();
        //    if (result.Succeeded)
        //    {
        //        return Ok($"Admin of {company.Name} company is deleted successfully");
        //    }
        //    else
        //    {
        //        return StatusCode(500, result.Errors.Select(e => new { Msg = e.Code, Desc = e.Description }).ToList());
        //    }
        //}

    }
}
