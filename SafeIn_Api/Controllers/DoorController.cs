using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafeIn_Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SafeIn_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoorController : ControllerBase
    {
        private readonly ILogger<DoorController> _logger;
        private AppDbContext _context;
        private UserManager<Employee> _userManager;
        public DoorController(ILogger<DoorController> logger, AppDbContext context, UserManager<Employee> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        [HttpGet("enter-the-door")]///{userAccessToken}/{DoorId}")]
        public async Task<IActionResult> Get(string DoorId, string userAccessToken)
        {
            var token = new JwtSecurityToken(jwtEncodedString: userAccessToken);
            var userId = token.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            var door = _context.Doors.Find(DoorId);
            if (user != null && door != null && token.ValidTo >= DateTime.UtcNow && user.CompanyId == door.CompanyId)
            {
                return Ok("Employee entered the door successfully");
            }
            else
            {
                return BadRequest("THIS PERSON CANNOT ENTER THE DOOR!!! SORRY FOR INCONVENIENCE!");
            }
        }

    }
}
