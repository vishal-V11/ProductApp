namespace BookingAPI.Models
{
    public class Bill
    {
        public string InvoiceNo { get; set; }
        public string UserName { get; set; }
        public int TotalAmount { get; set; }
        public int GstAmount { get; set; }
        public int GrandTotal { get; set; }
    }
}
