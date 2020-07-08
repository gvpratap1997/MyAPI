using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyAPI.BL;
using MyAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace MyAPI.Controllers
{
    
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        SQL_Helper instance;

        public LoginController(IConfiguration config)
        {
            _config = config;
            instance = new SQL_Helper();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString , userdata = user.username});
            }

            return response;
        }
        private string GenerateJSONWebToken(Models.UserModel userInfo)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }

        private Models.UserModel AuthenticateUser(Models.UserModel login)
        {
            Models.UserModel user = null;

            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information 
            List<UserModel> result = instance.FindByEmail();
            foreach(var data  in result)
            {
                if(login.email == data.email && login.password == data.password){
                    user = new UserModel { email = data.email, password = data.password, username = data.username };
                }
            }
            return user;
        }
    }
}
