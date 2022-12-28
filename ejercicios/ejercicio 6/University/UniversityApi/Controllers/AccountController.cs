﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Data;
using UniversityApi.Helpers;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
		private readonly ApplicationDbContext _context;
		private readonly JwtSettings _jwtSettings;
        public AccountController(ApplicationDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                EmailAddress = "xcloudsgls@gmail.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id = 2,
                EmailAddress = "pepe@gmail.com",
                Name = "User1",
                Password = "pepe"
            }
        };


        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();

                var users = _context.Users.ToList();

                var searchUser = users
                                     .FirstOrDefault(u => (u.Name == userLogin.UserName) && (u.Password == userLogin.Password));

                var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (searchUser is not null)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        Id = searchUser.Id,
                        UserName = searchUser.Name, 
                        EmailId = searchUser.EmailAddress,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                return Ok(Token);
            }
            catch (Exception exception)
            {

                throw new Exception("GetToken Error", exception);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}
