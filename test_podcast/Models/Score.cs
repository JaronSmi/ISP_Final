using System.ComponentModel.DataAnnotations;

namespace test_podcast.Models
{
    public class Score
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int score { get; set; }

        [Required]
        public string user { get; set; }
    }
}
