using CaseStudyBooksAPI.DAL.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DAO
{
    /**
     * Brand repository to query brands through db context.
     */
    public class BrandDAO
    {
        private AppDbContext _db;

        public BrandDAO(AppDbContext context)
        {
            _db = context;
        }

        // Gets all brands.
        public List<Brand> GetAll()
        {
            return _db.Brands.ToList<Brand>();
        }
    }
}
