using UniversityApi.Models;

namespace UniversityApi.Services
{
    public class CoursesService : ICoursesService
    {
        // Obtener todos los Cursos de una categoría concreta:

        public IEnumerable<Course> GetCoursesOfOneCategory(Category category)
        {
            throw new NotImplementedException();
        }

        // Obtener los Cursos de un Alumno:

        public IEnumerable<Course> GetCoursesOfOneStudent(Student student)
        {
            throw new NotImplementedException();
        }

        // Obtener Cursos sin temarios:

        public IEnumerable<Course> GetCourseWithoutSyllabus()
        {
            throw new NotImplementedException();
        }
    }
}
