using UFAR.Classwork.Data.Entities;
using UFAR.Classwork.Data.Models;

namespace UFAR.Classwork.UI.Services
{
    public class UserSessionService
    {
        public PatientAccount CurrentUser { get; set; } = new PatientAccount();
    }
}
