using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SafeIn_Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SafeIn_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoorController : ControllerBase
    {
        private readonly ILogger<DoorController> _logger;
        private AppDbContext _context;
        private UserManager<Employee> _userManager;
        public IConfiguration _configuration;
        public DoorController(IConfiguration config, ILogger<DoorController> logger, AppDbContext context, UserManager<Employee> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _configuration = config;
            
    }


        [HttpGet("enter-the-door")]///{userAccessToken}/{DoorId}")]
        public async Task<IActionResult> Get(string DoorId, string userAccessToken)
        {

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true, //you might want to validate the audience and issuer depending on your use case
                ValidAudience = _configuration["token:audience"],
                ValidateIssuer = true,
                ValidIssuer= _configuration["token:issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["token:key"])),
                ValidateLifetime = true //we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(userAccessToken, tokenValidationParameters, out SecurityToken securityToken);


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
            catch
            {
                return BadRequest("INVALID QR CODE!!! SORRY FOR INCONVENIENCE!");
            }
        }

    }
}
