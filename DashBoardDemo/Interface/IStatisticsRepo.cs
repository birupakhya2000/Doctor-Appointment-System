namespace DashBoardDemo.Interface
{
    public interface IStatisticsRepo
    {
        int GetTotalDoctors();
        int GetTotalPatients();
        int GetTodaysPatients();
        int ApprovedApp();
        int RejectApp();
    }
}