namespace modpackApi.DTO
{
    public class OrderDetailDTO
    {
        public int detailsId {  get; set; }
        public int orderId { get; set; } 
        
        public string name { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }

        public string ImageFileName { get; set; }
        public decimal subtotal { get; set; }
        public string ImageFile { get;  set; }
    }
}