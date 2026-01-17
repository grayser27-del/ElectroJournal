using ElectroJournal;
using System;
using System.Linq;

namespace ElectroJournal
{
    class Program
    {
        static void Main(string[] args)
        {
            //Имитация загрузки АИС
            CatProcessingAnimation();

            // Создание и миграция базы данных
            using (var context = new JournalContext())
            {
                context.Database.EnsureCreated();
                Console.WriteLine("База данных создана успешно!");

                // Заполнение данными
                SeedDatabase(context);

                // Примеры запросов
                DisplayStatistics(context);
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        static void CatProcessingAnimation()
        {
            string[] catFrames = {
            "  /\\_/\\  \n ( o.o ) \n  > ^ <  ",
            "  /\\_/\\  \n ( -.- ) \n  > ^ <  ",
            "  /\\_/\\  \n ( ^.^ ) \n  > ^ <  "
        };

            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║    Загрузка АИС Электрон...        ║");
            Console.WriteLine("╚════════════════════════════════════╝\n");

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, 3);
                ClearConsoleLines(3, 5);
                Console.SetCursorPosition(0, 3);
                Console.WriteLine(catFrames[i % catFrames.Length]);
                Console.Write("\nПрогресс: [");
                for (int j = 0; j < 10; j++)
                {
                    if (j <= i) Console.Write("█");
                    else Console.Write("░");
                }
                Console.Write($"] {(i + 1) * 10}%\n");

                Thread.Sleep(500);
            }

            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║  АИС Электрон загружена успешно!   ║");
            Console.WriteLine("╚════════════════════════════════════╝");
        }

        static void ClearConsoleLines(int startLine, int lineCount)
        {
            for (int i = 0; i < lineCount; i++)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, startLine);
        }

        static void SeedDatabase(JournalContext context)
        {
            // Проверка на заполненость БД
            if (context.Students.Any())
            {
                Console.WriteLine("База данных уже содержит данные.");
                return;
            }

            Console.WriteLine("Заполнение базы данных тестовыми данными...");

            var teacher1 = new Teacher
            {
                FirstName = "Иван",
                LastName = "Петров",
                MiddleName = "Сергеевич",
                Position = "Старший преподаватель",
                Email = "i.petrov@example.ru"
            };

            var teacher2 = new Teacher
            {
                FirstName = "Мария",
                LastName = "Иванова",
                MiddleName = "Александровна",
                Position = "преподаватель",
                Email = "m.ivanova@example.ru"
            };

            context.Teachers.AddRange(teacher1, teacher2);
            context.SaveChanges();

            var student1 = new Student
            {
                FirstName = "Алексей",
                LastName = "Смирнов",
                MiddleName = "Дмитриевич",
                BirthDate = new DateTime(2012, 5, 15),
                GroupNumber = "ИСиП-301"
            };

            var student2 = new Student
            {
                FirstName = "Екатерина",
                LastName = "Кузнецова",
                MiddleName = "Владимировна",
                BirthDate = new DateTime(2013, 9, 22),
                GroupNumber = "ИСиП-301"
            };

            var student3 = new Student
            {
                FirstName = "Дмитрий",
                LastName = "Попов",
                MiddleName = "Игоревич",
                BirthDate = new DateTime(2015, 3, 10),
                GroupNumber = "ИСиП-301"
            };

            context.Students.AddRange(student1, student2, student3);
            context.SaveChanges();

            var subject1 = new Subject
            {
                Name = "Программирование на C#",
                Description = "Основы программирования на языке C#",
                Hours = 72,
                TeacherId = teacher1.TeacherId
            };

            var subject2 = new Subject
            {
                Name = "Базы данных",
                Description = "Основы проектирования и работы с базами данных",
                Hours = 54,
                TeacherId = teacher2.TeacherId
            };

            context.Subjects.AddRange(subject1, subject2);
            context.SaveChanges();

            var grade1 = new Grade
            {
                Value = 5,
                Date = DateTime.Now.AddDays(-10),
                Comment = "Практическая работа №1",
                StudentId = student1.StudentId,
                SubjectId = subject1.SubjectId
            };

            var grade2 = new Grade
            {
                Value = 4,
                Date = DateTime.Now.AddDays(-5),
                Comment = "Практическая работа №13",
                StudentId = student1.StudentId,
                SubjectId = subject2.SubjectId
            };

            var grade3 = new Grade
            {
                Value = 5,
                Date = DateTime.Now.AddDays(-3),
                Comment = "Практическая работа №12",
                StudentId = student2.StudentId,
                SubjectId = subject1.SubjectId
            };

            context.Grades.AddRange(grade1, grade2, grade3);
            context.SaveChanges();

            var attendance1 = new Attendance
            {
                Date = DateTime.Now.AddDays(-1),
                IsPresent = true,
                StudentId = student1.StudentId
            };

            var attendance2 = new Attendance
            {
                Date = DateTime.Now.AddDays(-1),
                IsPresent = false,
                StudentId = student2.StudentId
            };

            context.Attendances.AddRange(attendance1, attendance2);
            context.SaveChanges();

            var schedule1 = new Schedule
            {
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(10, 30, 0),
                Classroom = "Аудитория 101",
                SubjectId = subject1.SubjectId
            };

            var schedule2 = new Schedule
            {
                DayOfWeek = DayOfWeek.Wednesday,
                StartTime = new TimeSpan(11, 0, 0),
                EndTime = new TimeSpan(12, 30, 0),
                Classroom = "Аудитория 203",
                SubjectId = subject2.SubjectId
            };

            context.Schedules.AddRange(schedule1, schedule2);
            context.SaveChanges();

            Console.WriteLine("Тестовые данные добавлены успешно!");
        }

        static void DisplayStatistics(JournalContext context)
        {
            Console.WriteLine("\nСтатистика: ");

            var studentCount = context.Students.Count();
            Console.WriteLine($"Всего студентов: {studentCount}");

            var teacherCount = context.Teachers.Count();
            Console.WriteLine($"Всего преподавателей: {teacherCount}");

            Console.WriteLine("\nСредние оценки по предметам:");
            var averageGrades = context.Grades
                .GroupBy(g => g.Subject.Name)
                .Select(g => new
                {
                    Subject = g.Key,
                    Average = g.Average(x => x.Value)
                });

            foreach (var item in averageGrades)
            {
                Console.WriteLine($"  {item.Subject}: {item.Average:F2}");
            }

            Console.WriteLine("\nСтатистика посещаемости:");
            var attendanceStats = context.Attendances
                .GroupBy(a => a.IsPresent)
                .Select(g => new
                {
                    Status = g.Key ? "Присутствовал" : "Отсутствовал",
                    Count = g.Count()
                });

            foreach (var stat in attendanceStats)
            {
                Console.WriteLine($"  {stat.Status}: {stat.Count}");
            }

            Console.WriteLine("\nСтуденты и их оценки:");
            var studentsWithGrades = context.Students
                .Select(s => new
                {
                    s.FirstName,
                    s.LastName,
                    s.GroupNumber,
                    Grades = s.Grades.Select(g => g.Value).ToList()
                });

            foreach (var student in studentsWithGrades)
            {
                Console.Write($"  {student.LastName} {student.FirstName} ({student.GroupNumber}): ");
                if (student.Grades.Any())
                {
                    Console.WriteLine(string.Join(", ", student.Grades));
                }
                else
                {
                    Console.WriteLine("оценок нет");
                }
            }
        }
    }
}