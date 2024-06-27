using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class CoverLetter
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Letter { get; set; }
        public string JobListing { get; set; }
    }
}
