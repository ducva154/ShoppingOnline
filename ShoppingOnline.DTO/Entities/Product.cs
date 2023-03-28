using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        [MaxLength(255)]
        public string Brand { get; set; }
        public int StockQuantity { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        [DefaultValue(false)]

        public virtual IEnumerable<Category> Categories { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }
        public virtual IEnumerable<CartItem> CartItems { get; set; } 
    }
}
