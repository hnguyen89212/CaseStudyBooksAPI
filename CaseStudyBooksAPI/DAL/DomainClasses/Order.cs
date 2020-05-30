using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudyBooksAPI.DAL.DomainClasses
{
    /**
     * A class to represent an order and capture:
     * - Id of order
     * - date time of placement
     * - total amount of money
     * - customer identity
     * - collection of the ordered items.
     */ 
    public class Order
    {
        public Order()
        {
            OrderLineItems = new HashSet<OrderLineItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "money")]
        public decimal OrderAmount { get; set; }

        [Required]
        [StringLength(128)]
        public int CustomerId { get; set; }

        public virtual ICollection<OrderLineItem> OrderLineItems { get; set; }
    }
}
