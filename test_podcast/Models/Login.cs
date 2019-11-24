using System.ComponentModel.DataAnnotations;


namespace test_podcast.Models
{
    public class Login
    {
        public int id { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string username { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 10)]
        public string password { get; set; }
    }
}
