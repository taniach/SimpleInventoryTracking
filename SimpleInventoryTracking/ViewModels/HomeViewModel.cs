using SimpleInventoryTracking.Models;
using System.Collections.Generic;

namespace SimpleInventoryTracking.ViewModels
{
    public class HomeViewModel
    {
        public string Title { get; set; }
        public List<Product> Products { get; set; }
    }
}
