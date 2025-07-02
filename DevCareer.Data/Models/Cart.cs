using System.ComponentModel.DataAnnotations.Schema;

namespace DevCareer.Data.Models
{
    [Table("cart")]
    public class Cart
    {
        public int Id {get;set;}
        public string? UserId {get;set;}
        public ICollection<CartItems>? CartItems {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
        public DateTime UpdatedAt {get;set;} = DateTime.UtcNow;
    }
}