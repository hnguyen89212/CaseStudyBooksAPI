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
     * 
     * Why should we set the ProductName column to have max length of 450?
     * Because the migration w-3-c-2 specifies that 
     * the ProductName in Products table has max length of 450.
     */
    public class OrderLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [StringLength(450)]
        public string ProductName { get; set; }
        [ForeignKey("ProductName")]
        public Product Product { get; set; }

        public int QtyOrdered { get; set; }

        public int QtySold { get; set; }

        public int QtyBackOrdered { get; set; }
    }
}
