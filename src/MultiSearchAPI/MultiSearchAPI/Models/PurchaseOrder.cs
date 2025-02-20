namespace MultiSearchAPI.Models
{
    public class PurchaseOrder
    {
        public int PurchaseOrderID { get; set; }
        public string DeliveryDate { get; set; }
        public string Supplier { get; set; }
        public string MaterialID { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
    }
}
