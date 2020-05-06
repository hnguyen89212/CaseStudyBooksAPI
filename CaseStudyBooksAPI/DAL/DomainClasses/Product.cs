using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyBooksAPI.DAL.DomainClasses
{
    /**
     * This class models a product we sell.
     */ 
    public class Product
    {
        // Primary Key
        [Key]
        [Required]
        public string ProductName;

        // Brand [1] - Product [N]
        // Foreign Key
        [Required]
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Range(0, 9999.99)]
        public decimal CostPrice { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Range(0, 9999.99)]
        public decimal MSRP { get; set; }

        [Required]
        public int QtyOnHand { get; set; }

        [Required]
        public int QtyOnBackOrder { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public int ReleasedYear { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }
    }
}
