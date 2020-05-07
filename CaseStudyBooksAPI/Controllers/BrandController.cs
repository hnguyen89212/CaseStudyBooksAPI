using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyBooksAPI.DAL;
using CaseStudyBooksAPI.DAL.DAO;
using CaseStudyBooksAPI.DAL.DomainClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyBooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private AppDbContext _db;

        public BrandController(AppDbContext context)
        {
            _db = context;
        }

        public ActionResult<List<Brand>> Index()
        {
            BrandDAO dao = new BrandDAO(_db);
            return dao.getAll();
        }
    }
}