using System.Threading.Tasks;

namespace DashBoardDemo.Interface
{
    public interface ILoginDetailsRepo
    {
        Task<int?> VerifyEmail(string email);
        Task<int?> VerifyOTP(int userId, int otp);
        Task<bool> UpdatePassword(int userId, string newPassword);
    }
}