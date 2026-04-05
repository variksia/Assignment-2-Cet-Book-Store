using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CetStudentBook.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 200 characters.")]
        [DisplayName("Book Title")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Author must be between 2 and 200 characters.")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Publish Date is required.")]
        [DataType(DataType.Date)]
        [DisplayName("Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Page Count is required.")]
        [Range(1, 10000, ErrorMessage = "Page Count must be between 1 and 10000.")]
        [DisplayName("Page Count")]
        public int PageCount { get; set; }

        [Required(ErrorMessage = "Is Second Hand is required.")]
        [DisplayName("Second Hand?")]
        public bool IsSecondHand { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100,000.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
