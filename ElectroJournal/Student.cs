using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ElectroJournal
{
    public class Student // Студент
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string GroupNumber { get; set; }

        // Навигационные свойства
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

        public Student()
        {
            Grades = new List<Grade>();
            Attendances = new List<Attendance>();
        }
    }
}
