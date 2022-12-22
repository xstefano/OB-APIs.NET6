using UniversityApi.Models;

namespace UniversityApi.Services
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetCoursesOfOneCategory(Category category);
        IEnumerable<Course> GetCoursesOfOneStudent(Student student);
        IEnumerable<Course> GetCourseWithoutSyllabus();
    }
}
