using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyBooksAPI.DAL;
using CaseStudyBooksAPI.DAL.DAO;
using CaseStudyBooksAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyBooksAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private AppDbContext _db;

        public ProductController(AppDbContext context)
        {
            _db = context;
        }

        // Gets all products from a brand.
        [Route("{brandId}")]
        public ActionResult<List<Product>> Index(int brandId)
        {
            ProductDAO productDAO = new ProductDAO(_db);
            List<Product> productsUnderTheBrand = productDAO.GetAllByBrand(brandId);
            return productsUnderTheBrand;
        }
    }
}