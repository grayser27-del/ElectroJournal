using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElectroJournal
{
    public class Teacher // Преподователь
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Position { get; set; }

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        // Навигационные свойства
        public ICollection<Subject> Subjects { get; set; }

        public Teacher()
        {
            Subjects = new List<Subject>();
        }
    }
}
