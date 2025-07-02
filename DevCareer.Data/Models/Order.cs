using System.ComponentModel.DataAnnotations.Schema;

namespace DevCareer.Data.Models
{
    [Table("order")]
    public class Order
    {
        public int Id {get;set;}
        public decimal Amount {get;set;}
        public ICollection<Product> Products {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
        public DateTime UpdatedAt {get;set;} = DateTime.UtcNow;
    }
}