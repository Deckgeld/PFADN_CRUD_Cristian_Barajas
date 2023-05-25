using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Passengers.Config;
using Passengers.WebApi.Auth;
using Passengers.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Passengers.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtIssuerOptions _jwtOptions;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<JwtTokenValidationSettings> _jwtConfig;
        public AuthController(UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> signInManager,
                IJwtIssuerOptions jwtOptions,
                RoleManager<IdentityRole> roleManager,
                IOptions<JwtTokenValidationSettings> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtOptions = jwtOptions;
            _roleManager = roleManager;
            _jwtConfig = jwtConfig;
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            var resuslt = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (user == null || !(await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                return Unauthorized();

            var tokenString = await CreateJwtTokenAsync(user);

            var result = new ContentResult() { Content = tokenString, ContentType = "aplication/text" };

            return result;
        }


        private async Task<String> CreateJwtTokenAsync(IdentityUser user)
        {
            //Create JWT claims
            var claims = new List<Claim>(new[]
            {
                //Issuer
                new Claim (JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),

                //UserName
                new Claim (JwtRegisteredClaimNames.Sub, user.UserName),

                //Email is unique
                new Claim (JwtRegisteredClaimNames.Email, user.Email),

                //Unique Id for all Jwt tokes - Id jti random
                new Claim (JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),

                //Isseued at
                //The toUnixEpochDate method is inside InitDbExtensions
                new Claim (JwtRegisteredClaimNames.Jti, _jwtOptions.IssuedAt.toUnixEpochDate().ToString(), ClaimValueTypes.Integer64)
            });

            //Add userclaims from storage
            claims.AddRange(await _userManager.GetClaimsAsync(user));

            //Add user role, they are converted to claims
            var roleNames = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                //Find IdentityRole by name
                var role = await _roleManager.FindByIdAsync(roleName);
                if (role != null)
                {
                    //Convert Identity to claim and add
                    var roleClaim = new Claim(ClaimTypes.Role, role.Name, ClaimValueTypes.String, _jwtOptions.Issuer);
                    claims.Add(roleClaim);

                    //Add claims belonging to the role
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }
            }

            //Prepare Jwt Token
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expires,
                signingCredentials: _jwtOptions.SigningCredentials);

            //Serialize token
            var result = new JwtSecurityTokenHandler().WriteToken(jwt);

            return result;
        }
    }
}
