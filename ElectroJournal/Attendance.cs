using System;
using System.ComponentModel.DataAnnotations;

namespace ElectroJournal
{
    public class Attendance // Посещаемость 
    {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        // Внешний ключ
        public int StudentId { get; set; }

        // Навигационное свойство
        public Student Student { get; set; }
    }
}
