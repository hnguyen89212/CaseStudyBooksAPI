using CaseStudyBooksAPI.DAL.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DAO
{
    /**
     * Product repository class to access DB through db context.
     */ 
    public class ProductDAO
    {
        private AppDbContext _db;

        public ProductDAO(AppDbContext context)
        {
            _db = context;
        }

        // Gets all products of a brand.
        public List<Product> GetAllByBrand(int brandId)
        {
            return _db.Products.Where(each => each.BrandId == brandId).ToList();
        }
    }
}
