using Microsoft.EntityFrameworkCore;
using Thoujour.Models;

public class ThoughtsDb : DbContext
{
    public ThoughtsDb(DbContextOptions<ThoughtsDb> options)
        : base(options)
    {
    }

    public DbSet<Thoujour.Models.Thought> Thoughts { get; set; } = default!;
    public DbSet<Thoujour.Models.Block> Blocks { get; set; } = default!;
    public DbSet<Thoujour.Models.Comment> Comments { get; set; } = default!;
}
