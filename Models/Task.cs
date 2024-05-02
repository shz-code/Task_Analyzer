using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Task_Analyzer.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name ="Due Date")]
        [Required(ErrorMessage ="Due Date is required")]
        public DateTime DueDate { get; set; }

        [Range(1,5,ErrorMessage ="Priority must be between 1 and 5")]
        public int Priority {  get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
