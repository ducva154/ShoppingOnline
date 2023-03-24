using Microsoft.AspNetCore.Identity;
using ShoppingOnline.DTO.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }
        public double Amount { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [Phone]
        public string Contact { get; set; }
        public DateTime Date { get; set; }
        [DefaultValue(false)]
        public bool Paided { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }

        public virtual CustomUser User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
