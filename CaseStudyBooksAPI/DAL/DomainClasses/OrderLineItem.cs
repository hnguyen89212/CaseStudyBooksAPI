using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DomainClasses
{
    /**
     * A class to model an item in an order.
     * It should be distinguished from a product.
     */ 
    public class OrderLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int QtyOrdered { get; set; }

        public int QtySold { get; set; }

        public int QtyBackOrdered { get; set; }
    }
}
