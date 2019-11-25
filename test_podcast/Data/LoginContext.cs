
using System;
using test_podcast.Models;

namespace test_podcast.Data
{
    public class LoginContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public LoginContext (Microsoft.EntityFrameworkCore.DbContextOptions<LoginContext> options)
            : base(options)
        {}
        
        public Microsoft.EntityFrameworkCore.DbSet<test_podcast.Models.Login> User { get; set; }
        
        public Microsoft.EntityFrameworkCore.DbSet<test_podcast.Models.Quiz> Scores { get; set; }
    }
}
