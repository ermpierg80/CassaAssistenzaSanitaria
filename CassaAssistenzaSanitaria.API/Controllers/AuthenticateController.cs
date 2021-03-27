using CassaAssistenzaSanitaria.API.Models;
using CassaAssistenzaSanitaria.API.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CassaAssistenzaSanitaria.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private log4net.ILog log;

        public AuthenticateController(UserManager<ApplicationUser> userManager)
        {
            log = Logger.GetLogger(typeof(AuthenticateController));
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model != null)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                    var roles = await userManager.GetRolesAsync(user);
                    foreach (var userRole in roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7S79jvOkEdwoRqHx"));
                    var token = new JwtSecurityToken(
                        issuer: "https://dotnetdetail.net",
                        audience: "https://dotnetdetail.net",
                        expires: DateTime.Now.AddDays(5),
                        claims: authClaims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }
            return Unauthorized();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<bool> Post([FromBody] UserMakeModel model)
        {
            bool esito = false;
            if (model != null)
            {
                try
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        Email = model.EMail,
                        UserName = model.Username,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    var identity = await userManager.CreateAsync(user, model.Password);
                    if (identity != null)
                    {
                        esito = identity.Succeeded;
                    }
                    if (!esito)
                    {
                        throw new Exception(identity.Errors.ToString());
                    }
                }
                catch (Exception e)
                {
                    log.Error(e.ToString());
                }
            }
            return esito;
        }

        [HttpPut]
        public async Task<bool> Put([FromBody] LoginChangeModel model)
        {
            bool esito = false;
            if (model != null)
            {
                try
                {
                    var user = await userManager.FindByNameAsync(model.Username);
                    var identity = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (identity != null)
                    {
                        esito = identity.Succeeded;
                    }
                    if (!esito)
                    {
                        throw new Exception(identity.Errors.ToString());
                    }
                }
                catch(Exception e)
                {
                    log.Error(e.ToString());
                }
            }
            return esito;
        }
    }
}
