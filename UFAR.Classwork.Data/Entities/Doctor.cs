using System;
using System.ComponentModel.DataAnnotations;

namespace UFAR.Classwork.Data.Entities
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(64)] // SHA256 hash is exactly 64 characters
        public string PasswordHash { get; set; }
    }
}