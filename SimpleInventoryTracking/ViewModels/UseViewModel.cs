using System;

namespace SimpleInventoryTracking.ViewModels
{
    public class UseViewModel
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public DateTime DateUsed { get; set; }
        public string Description { get; set; }
    }
}
