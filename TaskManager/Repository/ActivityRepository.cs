using TaskManager.IRepository;
using TaskManager.Model;

namespace TaskManager.Repository;

public class ActivityRepository : IActivityRepository
{
    private List<Activity> _activities = new List<Activity>();

    public void AddActivity(Activity activity)
    {
        _activities.Add(activity);
    }

    public IReadOnlyList<Activity> GetAllActivities()
    {
        return _activities.AsReadOnly();
    }
}