using System.ComponentModel.DataAnnotations;

namespace test_podcast.Models
{
    public class Quiz
    {
        // Primary Key
        [Required]
        public int id { get; set; }

        [Required]
        public int score { get; set; }

        [Required]
        public System.DateTime date { get; set; }

        [Required]
        public string username { get; set; }
    }
}