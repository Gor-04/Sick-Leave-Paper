using System;
using System.Security.Cryptography;
using System.Text;
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

        // ---------------- Register Patient ----------------
        public async Task<bool> RegisterUserAsync(PatientAccount patient, string plainPassword)
        {
            try
            {
                // Check if email already exists
                if (await _context.PatientAccounts.AnyAsync(u => u.Email == patient.Email))
                    return false;
                // Hash the password
                patient.PasswordHash = HashPassword(plainPassword);
                // Set default values for missing fields
                patient.Departement ??= "Your Departement";
                patient.Position ??= "Your Position";
                patient.Illness ??= "Not Specified";
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

        // ---------------- Login ----------------
        public async Task<bool> LoginUserAsync(string email, string password)
        {
            // Check patient
            var patient = await _context.PatientAccounts
    .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == HashPassword(password));


            if (patient != null)
                return true;

            // Check doctor
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Email == email);
            if (doctor != null && VerifyPassword(password, doctor.PasswordHash))
                return true;

            return false;
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                string enteredHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return enteredHash == storedHash.ToLower();
            }
        }


        // ---------------- Helper: Hash Password ----------------
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        // ---------------- Get Authenticated Patient ----------------
        public async Task<PatientAccount?> GetAuthenticatedUserAsync(string email)
        {
            return await _context.PatientAccounts.FirstOrDefaultAsync(u => u.Email == email);
        }

        // ---------------- Update Patient Account ----------------
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

                _context.PatientAccounts.Update(existingPatient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Doctor GetDoctorByEmail(string email)
        {
            return _context.Doctors.FirstOrDefault(d => d.Email == email);
        }
    }
}
