using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CetStudentBook.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; } 
        [Length(3,200)]
        [DisplayName("Full Name")]
         public string Name { get; set; }  

        [DataType(DataType.EmailAddress)]
        [Length(5,200)]
        public string Email { get; set; } 
        
        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } 

    }
}
