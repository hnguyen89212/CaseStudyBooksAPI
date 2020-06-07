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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BranchController(AppDbContext context)
        {
            _db = context;
        }

        [HttpGet("{lat}/{lng}")]
        public ActionResult<List<Branch>> Index(float lat, float lng)
        {
            BranchDAO branchDAO = new BranchDAO(_db);
            return branchDAO.GetTopThreeClosestBranches(lat, lng);
        }
    }
}