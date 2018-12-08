using System;

namespace SimpleInventoryTracking.ViewModels
{
    public class PurchaseViewModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public DateTime DatePurchased { get; set; }
        public string Description { get; set; }
    }
}
