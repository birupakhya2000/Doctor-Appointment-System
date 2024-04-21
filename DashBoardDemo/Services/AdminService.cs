using DashBoardDemo.Interface;
using DashBoardDemo.ModelDb;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;
using Microsoft.EntityFrameworkCore;

namespace DashBoardDemo.Services
{
    public class AdminService: IAdminService
    {
        private readonly AppDbContext appDbContext;

        public AdminService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
           
        }

        public async Task<UserLogin> AuthenticateUser(string username, string passcode)
        {
            var succeeded = await appDbContext.UserLogin.FirstOrDefaultAsync(authUser => authUser.UserName == username && authUser.passcode == passcode);
            return succeeded;
        }


        public async Task<IEnumerable<UserLogin>> getuser()
        {
            return await appDbContext.UserLogin.ToListAsync();
        }
    }
}
