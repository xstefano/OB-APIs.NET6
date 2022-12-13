using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models
{
    public class Courses : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string LongDescription { get; set; } = string.Empty;

        [Required]
        public string TargetPublic { get; set; } = string.Empty;

        [Required]
        public string Objectives { get; set; } = string.Empty;

        [Required]
        public string Requirements { get; set; } = string.Empty;

        public enum Level { basic = 0, intermediate = 1, advanced = 2 }
    }
}
