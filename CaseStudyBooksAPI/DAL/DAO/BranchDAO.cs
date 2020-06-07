using CaseStudyBooksAPI.DAL.DomainClasses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DAO
{
    public class BranchDAO
    {
        private AppDbContext _db;

        public BranchDAO(AppDbContext context)
        {
            _db = context;
        }

        public List<Branch> GetTopThreeClosestBranches(float? lat, float? lng)
        {
            List<Branch> branches = null;

            try
            {
                var latParam = new SqlParameter("@lat", lat);
                var lngParam = new SqlParameter("@lng", lng);

                var query = _db.Branches.FromSqlRaw("dbo.pTop3ClosestBranches @lat, @lng", latParam, lngParam);

                branches = query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return branches;
        }
    }
}
