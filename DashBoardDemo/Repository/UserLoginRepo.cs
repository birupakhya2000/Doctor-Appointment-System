using DashBoardDemo.Models;

using DashBoardDemo.Interface;

namespace DashBoardDemo.Repository
{
    public class UserLoginRepo : IUserLoginRepo
    {
        private readonly AppDbContext appDbContext;
        public UserLoginRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
