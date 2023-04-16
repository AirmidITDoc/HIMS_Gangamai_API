using HIMS.Data.Opd;
using HIMS.Model.Opd;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly I_LoginManager _loginManager;
        private readonly IConfiguration configuration;

        public LoginController(I_LoginManager loginManager, IConfiguration configuration)
        {
            _loginManager = loginManager;
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get(string userName)
        {
            var user = _loginManager.Get(userName);
            return Ok(user);
        }

        [HttpPost("token")]
        public ActionResult Token(Login userDetails)
        {
            var user = _loginManager.Get(userDetails.UserName);

            // return null if user not found
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var IsAuthenticated = (userDetails.Password == user.Password && userDetails.UserName == user.UserName);

            if (!IsAuthenticated) return BadRequest(new { message = "Username or password is incorrect" });

            var secret = configuration.GetValue<string>("AppSettings:SECRET");
            //var secret = Environment.GetEnvironmentVariable("SECRET");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)); //Secret
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var role = string.IsNullOrWhiteSpace(user.Role) ? string.Empty : user.Role;
            var authClaims = new[]
               {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, role)
                };
            //UserId, IsAdmin, UserName, UserEmail, UserType should be token
            //Invalid Username or Password
            var token = new JwtSecurityToken(
                null,
                null,
                authClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            return Ok(new
            {
                user = new
                {
                    id = user.UserId,
                    userName = user.UserName,
                    MailId = user.MailId,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    role = user.Role,
                    RoleId= user.RoleId ,
                    StoreId=user.StoreId ,
                    DoctorID=user.DoctorID,
                    MailDomain=user.MailDomain,
                    LoginStatus=user.LoginStatus
            },
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo
            });
        }
    }
}
