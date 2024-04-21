using DashBoardDemo.Models;

namespace DashBoardDemo.Repository
{
    public class UserLoginRepo
    {
        private readonly AppDbContext appDbContext;
        public UserLoginRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
    }
}
