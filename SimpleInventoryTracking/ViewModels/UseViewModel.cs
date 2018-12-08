using System;

namespace SimpleInventoryTracking.ViewModels
{
    public class UseViewModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public DateTime DateUsed { get; set; }
        public string Description { get; set; }
    }
}
