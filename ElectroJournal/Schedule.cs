using System;
using System.ComponentModel.DataAnnotations;

namespace ElectroJournal
{
    public class Schedule // Расписание
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Classroom { get; set; }

        // Внешний ключ
        public int SubjectId { get; set; }

        // Навигационное свойство
        public Subject Subject { get; set; }
    }
}
