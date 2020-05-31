using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseStudyBooksAPI.DAL;
using CaseStudyBooksAPI.DAL.DAO;
using CaseStudyBooksAPI.DAL.DomainClasses;
using CaseStudyBooksAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyBooksAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private AppDbContext _db;

        public OrderController(AppDbContext context)
        {
            _db = context;
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<string> Index(OrderHelper orderHelper)
        {
            string ret;

            try
            {
                CustomerDAO customerDAO = new CustomerDAO(_db);
                OrderDAO orderDAO = new OrderDAO(_db);

                Customer orderOwner = customerDAO.GetByEmail(orderHelper.Email);

                int orderId = orderDAO.AddOrder(orderOwner.Id, orderHelper.Selections);

                if (orderId > 0)
                {
                    ret = "Order " + orderId + " is successfully saved.";
                }
                else
                {
                    ret = "Order not saved.";
                }
            }
            catch(Exception ex)
            {
                ret = "We encounter a problem saving order: " + ex.Message;
            }

            return ret;
        }
    }
}