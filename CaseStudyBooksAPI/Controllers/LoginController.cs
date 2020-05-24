using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CaseStudyBooksAPI.DAL;
using CaseStudyBooksAPI.DAL.DAO;
using CaseStudyBooksAPI.DAL.DomainClasses;
using CaseStudyBooksAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CaseStudyBooksAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private AppDbContext _db;

        IConfiguration configurations;

        public LoginController(AppDbContext context, IConfiguration config)
        {
            _db = context;
            configurations = config;
        }

        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public ActionResult<CustomerHelper> Index(CustomerHelper userHelper)
        {
            CustomerDAO dao = new CustomerDAO(_db);
            Customer user = dao.GetByEmail(userHelper.email);
            if (VerifyPassword(userHelper.password, user.Hash, user.Salt))
            {
                userHelper.password = "";
                var appSettings = configurations.GetSection("AppSettings").GetValue<string>("Secret");
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string returnToken = tokenHandler.WriteToken(token);
                userHelper.token = returnToken;
            }
            else
            {
                userHelper.token = "Credentials invalid - login failed";
            }
            return userHelper;
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
    }
}