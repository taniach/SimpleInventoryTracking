using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleInventoryTracking.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public string TypeOfTransaction { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
