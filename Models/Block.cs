using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thoujour.Models
{
    public class Block
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_Thought_001")]
        public int ThoughtId { get; set; }
        public string? Title { get; set; }
        public string? B64Img { get; set; }
        public string? Text { get; set; }

        public Thought Thought { get; set; }
    }
}
