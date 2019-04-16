namespace learning_together_api.Services
{
    public interface IWorkshopAttendeeService
    {
        void Enroll(int workshopId, int userId);

        void Unenroll(int workshopId, int userId);
    }
}