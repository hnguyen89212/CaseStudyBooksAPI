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
                        ProductDAO productDAO = new ProductDAO(_db);
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

                            Product product = productDAO.GetByProductName(selection.ProductName);

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

                            productDAO.Update(product);

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
    }
}
