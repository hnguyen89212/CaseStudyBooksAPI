using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CaseStudyBooksAPI.DAL;
using CaseStudyBooksAPI.DAL.DAO;
using CaseStudyBooksAPI.DAL.DomainClasses;
using CaseStudyBooksAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private AppDbContext _db;

        public RegisterController(AppDbContext context)
        {
            _db = context;
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<CustomerHelper> Index(CustomerHelper userHelper)
        {
            CustomerDAO userDAO = new CustomerDAO(_db);
            Customer existingUser = userDAO.GetByEmail(userHelper.email);
            if (existingUser == null)
            {
                HashSalt hashSalt = GenerateSaltedHash(64, userHelper.password);
                userHelper.password = ""; // flush the string, no need for plain password.
                Customer user = new Customer();
                user.FirstName = userHelper.firstName;
                user.LastName = userHelper.lastName;
                user.Email = userHelper.email;
                user.Hash = hashSalt.Hash;
                user.Salt = hashSalt.Salt;
                user = userDAO.Register(user);
                if (user.Id > 0)
                {
                    userHelper.token = "User registration success.";
                }
                else
                {
                    userHelper.token = "User registration failed.";
                }
            }
            else
            {
                userHelper.token = "User registration failed: user is already existing.";
            }
            return userHelper;
        }

        private static HashSalt GenerateSaltedHash(int size, string plainPassword)
        {
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            // Fills an array of bytes with a cryptographically strong sequence of random nonzero values.
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);
            // A password, salt, and iteration count, then generates a binary key
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(plainPassword, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
            return hashSalt;
        }
    }

    public class HashSalt
    {
        public string Hash { get; set; }

        public string Salt { get; set; }
    }
}