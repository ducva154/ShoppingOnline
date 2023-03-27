using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingOnline.DTO.Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [DefaultValue(false)]

        public IEnumerable<Product> Products { get; set; }
    }
}
