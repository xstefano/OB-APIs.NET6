using Microsoft.EntityFrameworkCore;
using UniversityApi.Data;
using UniversityApi.Models;

namespace LinqSnippets
{
    public class Services
    {
        private readonly ApplicationDbContext _context;

        public Services(ApplicationDbContext context)
        {
            _context = context;
        }

        // Buscar usuarios por email:
        public async void FindUsersByEmail(string email)
        {
            var users = await _context.Users.ToListAsync();

            var findUsers = users
                                .Select(u => u)
                                .Where(u => u.EmailAddress == email)
                                .OrderBy(u => u.Name);
        }

        // Buscar alumnos mayores de edad:

        public async void FindStudentsLegalAge()
        {
            var students = await _context.Students.ToListAsync();

            var findStudentsLegalAge = students
                                              .Select(s => s)
                                              .Where(s => (DateTime.Now.Year - s.Dob.Year) >= 18)
                                              .OrderBy(s => s.FirstName);
        }

        // Buscar alumnos que tengan al menos un curso:

        public async void FindStudentsWithMoreThanOneCourse()
        {
            var students = await _context.Students.ToListAsync();

            var studentsWithMoreThanOneCourse = students
                                                       .Select(s => s)
                                                       .Where(s => s.Courses.Count > 0)
                                                       .OrderBy(s => s.FirstName);
        }

        // Buscar cursos de un nivel determinado que al menos tengan un alumno inscrito:

        public async void FindCoursesOfLevel(Level level)
        {
            var courses = await _context.Courses.ToListAsync();

            var coursesOfLevel = courses
                                       .Select(c => c)
                                       .Where(c => (c.Level == level) && (c.Students.Count > 0))
                                       .OrderBy(c => c.Name);
        }

        // Buscar cursos de un nivel determinado que sean de una categoría determinada:

        public async void FindCoursesOfLevelAndCategory(Level level, Category category)
        {
            var courses = await _context.Courses.ToListAsync();

            var coursesOfLevelAndCategory = courses
                                                  .Select(c => c)
                                                  .Where(c => (c.Level == level) && (c.Categories == category && c.Categories.Count > 0))
                                                  .OrderBy(c => c.Name);
        }

        // Buscars cursos sin alumnos:

        public async void FindCoursesWithoutStudents()
        {
            var courses = await _context.Courses.ToListAsync();

            var coursesWithoutStudents = courses
                                               .Select(c => c)
                                               .Where(c => c.Students.Count < 1)
                                               .OrderBy(c => c.Name);
        }
    }
}