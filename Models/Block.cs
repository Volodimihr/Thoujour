using System.ComponentModel.DataAnnotations;

namespace Thoujour.Models
{
    public class Block
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ThoughtId { get; set; }
        public string? Title { get; set; }
        public string? B64Img { get; set; }
        public string? Text { get; set; }
    }
}
