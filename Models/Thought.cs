using System.ComponentModel.DataAnnotations;

namespace Thoujour.Models
{
    public class Thought
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public List<Block>? Blocks { get; set; }
    }
}
