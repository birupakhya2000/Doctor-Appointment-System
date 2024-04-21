namespace DashBoardDemo.Interface
{
    public interface IStatisticsService
    {
        int GetTotalDoctors();
        int GetTotalPatients();
        int GetTodaysPatients();
        int ApprovedApp();
        int RejectApp();
    }
}
