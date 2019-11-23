
namespace test_podcast.Data
{
    public class UserContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UserContext (Microsoft.EntityFrameworkCore.DbContextOptions<UserContext> options)
            : base(options)
        {}
        
        public Microsoft.EntityFrameworkCore.DbSet<test_podcast.Models.User> User { get; set; }
    }
}
