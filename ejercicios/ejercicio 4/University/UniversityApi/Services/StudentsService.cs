using UniversityApi.Models;

namespace UniversityApi.Services
{
    public class StudentsService : IStudentsService
    {
        // Obtener todos los alumnos con cursos asociados:

        public IEnumerable<Student> GetStudentsWithCourses()
        {
            throw new NotImplementedException();
        }

        // Obtener todos los alumnos que no tienen cursos asociados:

        public IEnumerable<Student> GetStudentsWithNoCourses()
        {
            throw new NotImplementedException();
        }

        // Obtener alumnos de un Curso concreto:

        public IEnumerable<Student> GetStudentsSpecificCourse(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
