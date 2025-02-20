namespace MultiSearchAPI.Models
{
    public class SalesOrder
    {
        public int SalesOrderID { get; set; }
        public string DeliveryDate { get; set; }
        public string Customer { get; set; }
        public string MaterialID { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
    }
}
