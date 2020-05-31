using CaseStudyBooksAPI.DAL.DomainClasses;
using CaseStudyBooksAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DAO
{
    public class OrderDAO
    {
        private AppDbContext _db;

        public OrderDAO(AppDbContext context)
        {
            _db = context;
        }

        // Adds a new Order, updates both the Order and OrderLineItem table using transaction.
        // Returns the new Order entry Id.
        public int AddOrder(int CustomerId, OrderSelectionHelper[] Selections)
        {
            int orderId = -1;
            
            using(_db)
            {
                using (var _transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        // Constructs a new Order object
                        Order order = new Order();
                        order.CustomerId = CustomerId;
                        order.OrderDate = System.DateTime.Now;

                        decimal orderAmount = 0;
                        foreach(OrderSelectionHelper selection in Selections)
                        {
                            orderAmount += selection.Item.CostPrice;
                            // todo: quantity handling
                        }
                        order.OrderAmount = orderAmount;

                        // Persists the Order object to db
                        _db.Orders.Add(order);
                        _db.SaveChanges();

                        // Persists the OrderLineItem objects to db
                        foreach(OrderSelectionHelper selection in Selections)
                        {
                            OrderLineItem orderLineItem = new OrderLineItem();
                            orderLineItem.OrderId = order.Id;
                            orderLineItem.ProductName = selection.ProductName;

                            // todo: quantity handling
                            orderLineItem.QtyOrdered = selection.Qty;
                            orderLineItem.QtySold = selection.Qty;
                            orderLineItem.QtyBackOrdered = selection.Qty;

                            _db.OrderLineItems.Add(orderLineItem);
                            _db.SaveChanges();
                        }

                        // Commits
                        _transaction.Commit();
                        orderId = order.Id;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _transaction.Rollback();
                    }
                }
            }

            return orderId;
        }

    }
}
