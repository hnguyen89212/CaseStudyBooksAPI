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

        // Gets a product by its name.s
        public Product GetByProductName(string name)
        {
            return _db.Products.Where(each => each.ProductName == name).FirstOrDefault();
        }

        public void Update(Product product)
        {
            Product productFromDb = GetByProductName(product.ProductName);

            if (productFromDb != null)
            {
                productFromDb.BrandId = product.BrandId;
                productFromDb.GraphicName = product.GraphicName;
                productFromDb.CostPrice = product.CostPrice;
                productFromDb.MSRP = product.MSRP;
                productFromDb.QtyOnHand = product.QtyOnHand;
                productFromDb.QtyOnBackOrder = product.QtyOnBackOrder;
                productFromDb.Description = product.Description;
                productFromDb.ReleasedYear = product.ReleasedYear;

                //_db.Update(productFromDb);
                _db.Products.Update(productFromDb);
                _db.SaveChanges();
            }
        }
    }
}
