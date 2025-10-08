using System.ComponentModel.DataAnnotations;

namespace FCG.Domain.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public string Gender { get; set; }
        public bool IsOnPromotion { get; set; } = false;
        public double? OriginalPrice { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
