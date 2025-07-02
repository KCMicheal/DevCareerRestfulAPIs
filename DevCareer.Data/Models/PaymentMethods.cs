namespace Ecommerce.Models
{
    public class PaymentRequest
    {
        public decimal Amount {get;set;}
        public string Currency {get;set;} = "NGN";
        public string Email {get;set;} = string.Empty;
        public string PhoneNumber {get;set;} = string.Empty;
        public string FullName {get;set;} = string.Empty;
    }
}