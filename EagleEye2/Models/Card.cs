using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EagleEye2.Models
{
    public class Card
    {
        [Required]
        public int Id { get; set; }
                
        // Foreign Key
        [Required]
        public int UserId { get; set; }

        // Navigation Property
        public User User { get; set; }
    }

    public class CardDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class CardDetailDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserDepartment { get; set; }
    }
}