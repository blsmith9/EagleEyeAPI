using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EagleEye2.Models
{
    public class Store
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
    }

    public class StoreDTO
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
    }

    public class StoreDetailDTO
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Location { get; set; }
    }
}