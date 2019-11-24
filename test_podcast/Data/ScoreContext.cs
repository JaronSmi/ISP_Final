
namespace test_podcast.Data
{
    public class ScoreContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ScoreContext(Microsoft.EntityFrameworkCore.DbContextOptions<ScoreContext> options)
            : base(options)
        { }

        public Microsoft.EntityFrameworkCore.DbSet<test_podcast.Models.Score> Score { get; set; }
    }
}
