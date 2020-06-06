using CaseStudyBooksAPI.DAL.DomainClasses;
using CaseStudyBooksAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DAO
{
    public class OrderDAO
    {
        private AppDbContext _db;

        private readonly ProductDAO _productDAO;

        public OrderDAO(AppDbContext context)
        {
            _db = context;
            _productDAO = new ProductDAO(_db);
        }

        // Adds a new Order, updates both the Order and OrderLineItem table using transaction.
        // Returns the new Order entry Id.
        public AddOrderResultHelper AddOrder(int CustomerId, OrderSelectionHelper[] Selections)
        {
            int orderId = -1;

            bool isThereBackOrder = false;

            using (_db)
            {
                using (var _transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        decimal orderAmount = 0;
                        decimal hst = 0;

                        // Constructs a new Order object
                        Order order = new Order();
                        order.CustomerId = CustomerId;
                        order.OrderDate = System.DateTime.Now;

                        // Finds the total amount for the order.
                        foreach (OrderSelectionHelper selection in Selections)
                        {
                            orderAmount += (selection.Item.CostPrice) * (selection.Qty); // subtotal
                            hst = orderAmount * 0.13M; // hst
                            orderAmount += hst; //total for that selection.
                        }
                        order.OrderAmount = orderAmount;

                        // Persists the Order object to db
                        _db.Orders.Add(order);
                        _db.SaveChanges();

                        //Saves the order line items into the db.
                        foreach (OrderSelectionHelper selection in Selections)
                        {
                            OrderLineItem orderLineItem = new OrderLineItem();
                            orderLineItem.OrderId = order.Id; //oops
                            orderLineItem.ProductName = selection.ProductName;

                            Product product = _productDAO.GetByProductName(selection.ProductName);

                            if (selection.Qty <= product.QtyOnHand)
                            {
                                //We have enough stock
                                product.QtyOnHand -= selection.Qty;
                                product.QtyOnBackOrder = 0;

                                orderLineItem.QtySold = selection.Qty;
                                orderLineItem.QtyOrdered = selection.Qty;
                                orderLineItem.QtyBackOrdered = 0;
                            }
                            else
                            {
                                //We lack stock.
                                orderLineItem.QtySold = product.QtyOnHand;
                                orderLineItem.QtyOrdered = selection.Qty;
                                orderLineItem.QtyBackOrdered = (selection.Qty - product.QtyOnHand);

                                product.QtyOnBackOrder += (selection.Qty - product.QtyOnHand);
                                product.QtyOnHand = 0;

                                isThereBackOrder = true;
                            }

                            _productDAO.Update(product);

                            _db.OrderLineItems.Add(orderLineItem);
                            _db.SaveChanges();
                        }

                        // Commits
                        _transaction.Commit();
                        orderId = order.Id;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _transaction.Rollback();
                    }
                }
            }

            AddOrderResultHelper outcome = new AddOrderResultHelper();
            outcome.OrderId = orderId;
            outcome.IsThereBackOrder = isThereBackOrder;

            return outcome;
        }

        public List<Order> GetAllOrdersOfUser(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId).ToList<Order>();
        }

        public List<OrderDetailsHelper> GetOrderDetails(int orderId, string email)
        {
            List<OrderDetailsHelper> allDetails = new List<OrderDetailsHelper>();

            Customer customer = _db.Customers.FirstOrDefault(c => c.Email == email);

            var results = from pr in _db.Products
                          join ol in _db.OrderLineItems on pr.ProductName equals ol.ProductName
                          join o in _db.Orders on ol.OrderId equals o.Id
                          where (o.CustomerId == customer.Id && o.Id == orderId)
                          select new OrderDetailsHelper
                          {
                              OrderId = orderId,
                              CustomerId = customer.Id,
                              ProductName = pr.ProductName,
                              OrderDate = o.OrderDate.ToString("yyyy/MM/dd - hh:mm tt"),
                              MSRP = pr.MSRP,
                              QtyO = ol.QtyOrdered,
                              QtyS = ol.QtySold,
                              QtyB = ol.QtyBackOrdered
                          };

            allDetails = results.ToList<OrderDetailsHelper>();

            return allDetails;
        }
    }
}
