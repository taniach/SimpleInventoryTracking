using System;

namespace SimpleInventoryTracking.ViewModels
{
    public class PurchaseViewModel
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public DateTime DatePurchased { get; set; }
        public string Description { get; set; }
    }
}
