    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace UFAR.Classwork.Data.Entities
{
    public class PatientAccount
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(20)]
        public string IdNumber { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public string Position { get; set; }


        public string Departement { get; set; }

        public string Illness { get; set; }


        public string IllnessDescription { get; set; }

        public int DaysTime { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; } = "Pending"; 
    }
}
