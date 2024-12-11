using Microsoft.EntityFrameworkCore;
using UFAR.Classwork.Data.Entities;

namespace UFAR.Classwork.Data.DAO
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<PatientAccount> PatientAccounts { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
