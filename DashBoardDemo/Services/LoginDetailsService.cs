using DashBoardDemo.Interface;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;

namespace DashBoardDemo.Services
{
    public class LoginDetailsService : ILoginDetailsService
    {

        private readonly LoginDetailsRepo loginDetailsRepo;
        private readonly AppDbContext appDbContext;
        private readonly LoginRepo loginRepo;

        public LoginDetailsService(AppDbContext appDbContext, LoginDetailsRepo loginDetailsRepo, LoginRepo loginRepo)
        {
            this.appDbContext = appDbContext;
            this.loginDetailsRepo = loginDetailsRepo;
            this.loginRepo = loginRepo;



        }

        public async Task<int?> VerifyEmail(string email)
        {
            return await loginDetailsRepo.VerifyEmail(email);
        }



        public async Task<int?> VerifyOTP(int userId,int otp)
        {
            // Return null if the OTP verification fails

            return await loginDetailsRepo.VerifyOTP(userId,otp);
        }


        public async Task<bool> UpdatePassword(int userId, string newPassword)
        {
            return await loginDetailsRepo.UpdatePassword(userId, newPassword);
        }

    }
}
