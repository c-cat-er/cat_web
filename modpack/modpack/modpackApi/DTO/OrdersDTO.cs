using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Drawing;

namespace modpackApi.DTO
{
    public class OrdersDTO
    {
        public int orderId {  get; set; } 
        public int memberId { get; set; }

        public string memberName {  get; set; }
        public string shippingName {  get; set; }

        public decimal? shippingCost { get; set; }

        public string shippingStatusName {  get; set; }

        public string orderStatusName { get; set; }

        public string paymentName { get; set; } 
        public string paymentStatusName { get; set; }   

        public string recipientName { get; set; }  
        public string recipientAddress { get; set; }

        public string billRecipientName {  get; set; }

        public string billRecipientAddress { get; set; }
        public DateTime creationTime { get; set; }





    }
}
