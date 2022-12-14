﻿using System.ComponentModel.DataAnnotations;

namespace UniversityApi.Models
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdateAt { get; set; }
        public string DeleteBy { get; set; } = string.Empty;
        public DateTime? DeleteAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
