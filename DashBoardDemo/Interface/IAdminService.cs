using DashBoardDemo.ModelDb;

namespace DashBoardDemo.Interface
{
    public interface IAdminService
    {
        Task<IEnumerable<UserLogin>> getuser();
        Task<UserLogin> AuthenticateUser(string username, string passcode);
    }
}
