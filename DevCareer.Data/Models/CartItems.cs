using System.ComponentModel.DataAnnotations.Schema;

namespace DevCareer.Data.Models
{
    [Table("cart-items")]
    public class CartItems
    {
        public int CartId {get;set;}
        public Cart? Cart {get;set;}
        public int ProductId {get;set;}
        public Product? Product {get;set;}
        public int Quantity {get;set;}
    }
}