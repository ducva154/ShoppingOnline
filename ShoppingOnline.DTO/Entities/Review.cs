using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingOnline.DTO.Entities
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ReviewId { get; set; }

        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public DateTime Date { get; set; }

        public virtual CustomUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}
