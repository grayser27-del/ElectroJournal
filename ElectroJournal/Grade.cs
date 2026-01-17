using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroJournal
{
    public class Grade // Оценки
    {
        [Key]
        public int GradeId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Value { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        // Внешние ключи
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        // Навигационные свойства
        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
