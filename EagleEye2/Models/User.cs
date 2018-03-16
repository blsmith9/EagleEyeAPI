using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EagleEye2.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public int AccessLevel { get; set; }
    }

    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int AccessLevel { get; set; }

    }
}