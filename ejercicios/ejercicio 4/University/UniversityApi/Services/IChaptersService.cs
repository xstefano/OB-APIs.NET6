using UniversityApi.Models;

namespace UniversityApi.Services
{
    public interface IChaptersService
    {
        IEnumerable<Chapter> GetCourseSyllabusForOneCourse();
    }
}
