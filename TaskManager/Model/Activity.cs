using TaskManager.ENUM;

namespace TaskManager.Model;
public class Activity
{
    public string Description { get; private set; }
    private int durationHrs { get; set; }
    private Priority priority { get; set; }
    public DateTime StartTime { get; private set; }

    public Activity(string description, int durationHrs, Priority priority, DateTime startTime)
    {
        Description = description;
        this.durationHrs = durationHrs;
        this.priority = priority;
        StartTime = startTime;
    }
    
    public override string ToString()
    {
        return 
            "\n-------------------\n" +
            "Description: " + Description + "\n" +
            "Duration: " + durationHrs + " hours\n" +
            "Priority level: " + priority + "\n" +
            "Start time: " + StartTime + "\n" +
            "-------------------";
    }

}                                       