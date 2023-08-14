using E_Commerce_API.Helper;
using E_Commerce_Common;
using E_Commerce_DataAccess;
using E_Commerce_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _apiSettings;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> apiSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _apiSettings = apiSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromBody] RegisterRequestDTO registerDTO)
        {
            if (registerDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                PhoneNumber = registerDTO.PhoneNumber,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new RegisterResponseDTO()
                {
                    IsReqisterationSuccess = false,
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Keys.Role_ForCustomer);
            if (!roleResult.Succeeded)
            {
                return BadRequest(new RegisterResponseDTO()
                {
                    Errors = roleResult.Errors.Select(x => x.Description),
                    IsReqisterationSuccess = false

                });
            }
            return StatusCode(201);

        }


        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequestDTO loginDTO)
        {
            if (loginDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginDTO.UserName);
                if (user == null)
                {
                    return Unauthorized(new LoginResponseDTO()
                    {
                        IsAuthSuccess = false,
                        ErrorMessage = "Invalid Authentication",
                    });
                }
                var signInCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                        issuer: _apiSettings.ValidIssuer,
                        audience: _apiSettings.ValidAudience,
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: signInCredentials);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new LoginResponseDTO()
                {
                    IsAuthSuccess = true,
                    Token = token,
                    UserDTO = new UserDTO()
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    }
                });
            }
            else
            {
                return Unauthorized(new LoginResponseDTO()
                {
                    IsAuthSuccess = false,
                    ErrorMessage = "Invalid Auth Bro",
                });
            }
            return StatusCode(201);

        }




        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Id",user.Id),
                new Claim("PhoneNumber",user.PhoneNumber),
            };
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            return claims;
        }

    }
}
