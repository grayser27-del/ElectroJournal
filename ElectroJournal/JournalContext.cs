using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ElectroJournal
{
    public class JournalContext : DbContext
    {
        private static readonly string? ConnectionString;
        // Получение строки подключения из config.json
        static JournalContext() 
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .Build();

            ConnectionString = configuration.GetConnectionString("DefaultConnection");

            // Проверка на null
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new InvalidOperationException("Connection string not found in config.json");
            }
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Связи:

            // Связь Teacher - Subject (один-ко-многим)
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithOne(s => s.Teacher)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь Student - Grade (один-ко-многим)
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Student)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь Subject - Grade (один-ко-многим)
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.Subject)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь Student - Attendance (один-ко-многим)
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Attendances)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь Subject - Schedule (один-ко-многим)
            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Schedules)
                .WithOne(sc => sc.Subject)
                .HasForeignKey(sc => sc.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            modelBuilder.Entity<Grade>()
                .HasIndex(g => g.Date);

            modelBuilder.Entity<Attendance>()
                .HasIndex(a => a.Date);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.GroupNumber);
        }
    }
}
