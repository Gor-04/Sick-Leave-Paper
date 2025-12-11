//using System.Security.Cryptography;
//using System.Text;
//using UFAR.Classwork.Data.DAO;
//using UFAR.Classwork.Data.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace UFAR.Classwork.UI.Services
//{
//    public class AuthenticationService
//    {
//        private readonly ApplicationDBContext _db;

//        public AuthenticationService(ApplicationDBContext db)
//        {
//            _db = db;
//        }

//        public async Task<bool> LoginUserAsync(string email, string password)
//        {
//            var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.Email == email);
//            if (doctor != null && VerifyPassword(password, doctor.PasswordHash))
//                return true;

//            // You can also add PatientAccounts verification here
//            return false;
//        }

//        private bool VerifyPassword(string enteredPassword, string storedHash)
//        {
//            using (var sha256 = SHA256.Create())
//            {
//                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
//                string enteredHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
//                return enteredHash == storedHash.ToLower();
//            }
//        }
//    }
//}
