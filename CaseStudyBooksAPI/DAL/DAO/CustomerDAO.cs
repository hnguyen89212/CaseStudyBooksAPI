using CaseStudyBooksAPI.DAL.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DAO
{
    public class CustomerDAO
    {
        private AppDbContext _db;

        public CustomerDAO(AppDbContext context)
        {
            _db = context;
        }

        public Customer Register(Customer user)
        {
            _db.Customers.Add(user);
            _db.SaveChanges();
            return user;
        }

        public Customer GetByEmail(string email)
        {
            return _db.Customers.FirstOrDefault(each => each.Email == email);
        }
    }
}
