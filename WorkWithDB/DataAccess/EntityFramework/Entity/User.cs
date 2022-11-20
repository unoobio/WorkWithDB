using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WorkWithDB.DataAccess.EntityFramework.Entity
{
    [Index(nameof(Email), IsUnique = true)]
    internal class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Required]
        public string LastName { get; set; }

        [StringLength(255)]
        [RegularExpression("^[a-zA-Z0-9.!#$%&''*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")]
        [Required]
        public string Email { get; set; }

        [StringLength(11)]
        [RegularExpression("^[0-9]{11}$")]
        [Required]
        public string PhoneNumber { get; set; }

    }
}
