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

        private readonly OrderDAO _orderDAO;

        private readonly CustomerDAO _customerDAO;

        public OrderController(AppDbContext context)
        {
            _db = context;
            _orderDAO = new OrderDAO(_db);
            _customerDAO = new CustomerDAO(_db);
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

                AddOrderResultHelper result = orderDAO.AddOrder(orderOwner.Id, orderHelper.Selections);

                if (result.OrderId > 0)
                {
                    ret = "Order " + result.OrderId + " is successfully saved.";

                    if (result.IsThereBackOrder)
                    {
                        ret += " Goods are backordered.";
                    }
                }
                else
                {
                    ret = "Order is not saved.";
                }
            }
            catch (Exception ex)
            {
                ret = "We encounter a problem saving order: " + ex.Message;
            }

            return ret;
        }

        [HttpGet("{email}")]
        public ActionResult<List<Order>> List(string email)
        {
            List<Order> ordersOfUser = new List<Order>();
            Customer customer = _customerDAO.GetByEmail(email);
            ordersOfUser = _orderDAO.GetAllOrdersOfUser(customer.Id);
            return ordersOfUser;
        }

        [HttpGet("{orderId}/{email}")]
        public ActionResult<List<OrderDetailsHelper>> GetOrderDetails(int orderId, string email)
        {
            return _orderDAO.GetOrderDetails(orderId, email);
        }
    }
}