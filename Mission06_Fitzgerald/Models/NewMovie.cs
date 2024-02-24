using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mission06_Fitzgerald.Models
{
    public class NewMovie
    {
        [Key]
        [Required]
        public int MovieID { get; set; }
        [Required]
        public string Title { get; set; }

        [ForeignKey("CategoryId")]
        [Required(ErrorMessage = "Sorry, you need to select a category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        [Range(1888,2024)]
        public int Year { get; set; }
        public string? Director { get; set; }
        [Required]
        public string Rating { get; set; }
        public bool? Edited { get; set; }
        public string? LentTo { get; set; }
        [Required]
        public bool CopiedToPlex { get; set; }
        public string? Notes { get; set; }
    }
}