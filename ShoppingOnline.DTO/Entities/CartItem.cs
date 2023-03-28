using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Entities
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual CustomUser User { get; set; }
    }
}
