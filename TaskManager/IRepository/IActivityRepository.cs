using TaskManager.Model;

namespace TaskManager.IRepository;

public interface IActivityRepository
{
    void AddActivity(Activity activity);
    IReadOnlyList<Activity> GetAllActivities();
}