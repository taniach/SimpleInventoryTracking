﻿using System.ComponentModel.DataAnnotations;

namespace SimpleInventoryTracking.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string OwnerID { get; set; }

        public void Purchase(int quantityPurchased)
        {
            QuantityAvailable += quantityPurchased;
        }

        public bool Use(int quantityUsed)
        {
            if (quantityUsed > QuantityAvailable)
                return false;

            QuantityAvailable -= quantityUsed;
            return true;
        }

        public bool IsInStock()
        {
            return QuantityAvailable > 0;
        }
    }
}
