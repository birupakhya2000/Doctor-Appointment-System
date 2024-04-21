using DashBoardDemo.ModelDb;

namespace DashBoardDemo.Interface
{
    public interface ILoginDetailsService
    {
        Task<int?> VerifyEmail(string email);
        Task<int?> VerifyOTP(int userId,int otp);
        Task<bool> UpdatePassword(int userId, string newPassword);




    }
}
