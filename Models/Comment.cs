using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thoujour.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_Thought_002")]
        public int ThoughtId { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string Text { get; set;}
        [Required]
        public DateTime Date { get; set; }

        public Thought? Thought { get; set; }
    }
}
