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
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ReviewId{ get; set; }
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public virtual IdentityUser User{ get; set; }
        public virtual Product Product { get; set; }
    }
}
