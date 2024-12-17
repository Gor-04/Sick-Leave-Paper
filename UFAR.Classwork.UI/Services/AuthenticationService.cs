using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UFAR.Classwork.Data.Entities;
using UFAR.Classwork.Data.DAO;

namespace UFAR.Classwork.UI.Services
{
    public class AuthenticationService
    {
        private readonly ApplicationDBContext _context;

        public AuthenticationService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(PatientAccount patient)
        {
            try
            {
                // Check if email already exists
                if (await _context.PatientAccounts.AnyAsync(u => u.Email == patient.Email))
                    return false;

                // Set default values for missing fields
                patient.Departement ??= "Your Departement";
                patient.Position ??= "Your Position";
                patient.Illness ??= "Not Specified"; // Default value for Illness
                patient.IllnessDescription ??= "Not Specified";

                // Add the patient to the database
                await _context.PatientAccounts.AddAsync(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                return false;
            }
        }



        public async Task<bool> LoginUserAsync(string email, string password)
        {
            try
            {
                var patient = await _context.PatientAccounts
                    .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);

                if (patient != null)
                {
                    return true;
                }

                bool isDoctor = await _context.Doctors.AnyAsync(d => d.Email == email && d.PasswordHash == password);
                if (isDoctor)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging in user: {ex.Message}");
                return false;
            }
        }

        public async Task<PatientAccount?> GetAuthenticatedUserAsync(string email)
        {
            return await _context.PatientAccounts.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> UpdatePatientAccountAsync(PatientAccount patient)
        {
            try
            {
                var existingPatient = await _context.PatientAccounts.FirstOrDefaultAsync(p => p.Email == patient.Email);
                if (existingPatient == null) return false;

                // Update fields
                existingPatient.Position = patient.Position;
                existingPatient.Departement = patient.Departement;
                existingPatient.Illness = patient.Illness;
                existingPatient.IllnessDescription = patient.IllnessDescription;

                // Save changes
                _context.PatientAccounts.Update(existingPatient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}