using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithDB.DataAccess.EntityFramework.Entity
{
    [Index(nameof(Title))]
    internal class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(255)]
        [Required]
        public string PictureUrl { get; set; }

        [Range(1, double.MaxValue)]
        [Required]
        public double Price { get; set; }


        public int MarketId { get; set; }
        [Required]
        [ForeignKey(nameof(MarketId))]
        public Market Market { get; set; }

    }
}
