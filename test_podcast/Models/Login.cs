using System.ComponentModel.DataAnnotations;


namespace test_podcast.Models
{
    public class Login
    {
        public int id { get; set; }

        public string email { get; set; }

        [StringLength(20, MinimumLength = 4)]
        public string username { get; set; }

        [StringLength(25, MinimumLength = 10)]
        public string password { get; set; }
    }
}
