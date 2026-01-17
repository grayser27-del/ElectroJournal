using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;


namespace ElectroJournal
{
    public class Subject // Дисциплина
    {
        [Key]
        public int SubjectId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int Hours { get; set; }

        // Внешний ключ
        public int TeacherId { get; set; }

        // Навигационные свойства
        public Teacher Teacher { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Schedule> Schedules { get; set; }

        public Subject()
        {
            Grades = new List<Grade>();
            Schedules = new List<Schedule>();
        }
    }
}
