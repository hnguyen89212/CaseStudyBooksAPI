using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.Helpers
{
    public class OrderDetailsHelper
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public string ProductName { get; set; }

        // Formats this to string.
        public string OrderDate { get; set; }

        public decimal MSRP { get; set; }

        public int QtyO { get; set; }

        public int QtyS { get; set; }

        public int QtyB { get; set; }
    }
}

/**
ProductName = Products.ProductName
OrderDate = Orders.OrderDate
MSRP = Products.MSRP
QtyO = OrderLineItems.QtyOrdered
QtyS = OrderLineItems.QtySold
QtyB = QtyO - QtyS = OrderLineItems.QtyBackOrdered
*/
