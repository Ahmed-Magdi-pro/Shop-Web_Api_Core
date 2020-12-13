using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiCoreApp.Authentication;
using WebApiCoreApp.ViewModel;
using ZendeskApi_v2.Models.Constants;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebApiCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager ,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM model)
        {
            var emailExist = await userManager.FindByEmailAsync(model.Email);
            if (emailExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Respnse { Status = "Error", Message = "email already exist" });
            }
            var userExist = await userManager.FindByNameAsync(model.UserName);
            if (userExist!=null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Respnse { Status = "Error", Message = "user name already exist" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Respnse { Status = "Error", Message = "user creation faild" });
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.EndUser))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.EndUser));
            if(await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
              await userManager.AddToRoleAsync(user, UserRoles.EndUser);
            }
            return Ok(new Respnse { Status = "Success", Message = "User Created" });
        }



        [HttpPost]
        [Route("RegisterAsAdmin")]
        public async Task<IActionResult> RegisterAsAdmin([FromBody] RegisterVM modal)
        {
            var emailExist =await userManager.FindByEmailAsync(modal.Email);
            if (emailExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Respnse
                { Status = "Error", Message = "email already exist" });
            }
            var userExist =await userManager.FindByNameAsync(modal.UserName);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Respnse
                { Status = "Error", Message = "user name already exist" });
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = modal.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = modal.UserName
            };

            var result = await userManager.CreateAsync(user, modal.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Respnse
                 { Status = "Error", Message = "creation faild" });
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.EndUser))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.EndUser));
            if(await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            return Ok(new Respnse { Status = "Success", Message = " Admin Created" });

        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM modal)
        {
            var user = await userManager.FindByEmailAsync(modal.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, modal.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name,user.UserName),
                    new Claim (System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                   issuer: _configuration["JWT:ValidIssuer"],
                   audience: _configuration["JWT:ValidAudience"],
                   expires: DateTime.Now.AddDays(15),
                   claims: authClaims,
                   signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                   );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = user.UserName,
                });

                return Unauthorized();
        }




    }
}
