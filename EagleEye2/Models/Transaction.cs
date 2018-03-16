using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EagleEye2.Models
{
    public class Transaction
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double Amount { get; set; }
        
        // Foreign Key
        [Required]
        public int StoreId { get; set; }
        [Required]
        public int CardId { get; set; }
        // Navigation Property
        public Store Store { get; set; }
        public Card Card { get; set; }
    }

    public class TransactionDTO
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int CardId { get; set; }
        public int StoreID { get; set; }
    }

    public class TransactionDetailDTO
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int CardId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }

    }
}