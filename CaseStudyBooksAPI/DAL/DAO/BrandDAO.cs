using CaseStudyBooksAPI.DAL.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DAO
{
    public class BrandDAO
    {
        private AppDbContext _db;

        public BrandDAO(AppDbContext context)
        {
            _db = context;
        }

        public List<Brand> getAll()
        {
            return _db.Brands.ToList<Brand>();
        }
    }
}
