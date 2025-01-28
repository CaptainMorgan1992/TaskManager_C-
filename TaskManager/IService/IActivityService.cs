using TaskManager.ENUM;
using TaskManager.Model;

namespace TaskManager.IService;

public interface IActivityService
{
    void AddActivity();
    DateTime GetStartTime();
    Priority GetEnum();
    void CalculateTimeUntilNextActivity();
    void GetAllActivities();
}