using TaskManager.ENUM;
using TaskManager.IRepository;
using TaskManager.IService;
using TaskManager.Model;

namespace TaskManager.Service;

public class ActivityService : IActivityService
{
    private readonly IActivityRepository _activityRepository;
    private Priority _priority;

    public ActivityService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }
    public void AddActivity()
    {
        var description = ChooseActivityType();
        var durationHrs = ChooseDuration();
        var priority = GetEnum();
        var startTime = GetStartTime();
        var activity = new Activity(description, durationHrs, priority, startTime);
        _activityRepository.AddActivity(activity);
        
        Console.WriteLine(description + "\n - Task added. \n");
    }
    
    private static string ChooseActivityType()
    {
        string description;

        while (true)
        {
            Console.WriteLine("Please enter an activity type: ");
            description = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(description))
            {
                
                if (int.TryParse(description, out _))
                {
                    Console.WriteLine("Numbers are not allowed. Please enter a valid word or sentence.");
                    continue;
                }
                
                var totalChar = CountCharacters(description);

                if (totalChar <= 20 && totalChar > 2)
                {
                    return description;
                }
                
                Console.WriteLine("Please enter a word or a sentence, with a maximum of 20 characters.");
            }
            else
            {
                Console.WriteLine("Null or whitespace is not allowed.");
            }
        }
    }
    
    private static int CountCharacters(string description) {
    char [] strArray = description.ToCharArray();
    var totalChar = 0;
    
        foreach (char c in strArray)
        {
        totalChar++;
        } 
        return totalChar;
    }
        
    private static int ChooseDuration()                                                     
    {                                                                                       
        var duration = 0; 
        var validInput = false;

        while (!validInput) 
        {
            Console.WriteLine("Please enter for how many hours the activity should last for (1-8 hours): ");

            var input = Console.ReadLine();
            
            if (Int32.TryParse(input, out duration) && duration > 0 && duration <= 8 ) 
            {
                validInput = true;  
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than 0 and less than or equal to 8.");
            }
        }
        
        return duration;  
    }
    
    public void GetAllActivities()
    {
        var activities = FetchActivities();
        
        if (activities.Count == 0) {
            Console.WriteLine("No activities found. Please add an activity if you want to explore this option. ");
            AddActivity();
        }
        foreach (var a in activities) 
        {
            Console.WriteLine(a);
        }
    }
    
    // A Helper function to reduce code repetition
    private IReadOnlyList<Activity> FetchActivities()
    {
        return _activityRepository.GetAllActivities();
    }

    public Priority GetEnum()
    {     
        Console.WriteLine("Please choose the priority of the activity: ");   
        
        var i = 1;
        foreach (Priority priority in Enum.GetValues(typeof(Priority)))
        {
                Console.WriteLine(i + ". " + priority);
                i++;
        }
        
        var value = Console.ReadLine();
        
        if (Int32.TryParse(value, out int number) && number >= 1 && number <= 3)
        {
            switch (number)                         
            {                                       
                case 1:                           
                    _priority = Priority.Low;     
                    break;                        
                case 2:                           
                    _priority = Priority.Medium;  
                    break;                        
                case 3:                           
                    _priority = Priority.High;    
                    break;                        
            }                                       
        } else
        {
            Console.WriteLine("Invalid input. Please enter a valid number between 1 and 3.");
            GetEnum();  
        }
        return _priority;
    }

    public DateTime GetStartTime()
    {
        Console.WriteLine("Please enter the date and time (e.g., YYYY-MM-DD HH:MM):");
        var input = Console.ReadLine();
        
        DateTime timestamp;

        if (!DateTime.TryParse(input, out timestamp) || timestamp < DateTime.Today)
        {
            Console.WriteLine("Invalid date/time format. Please try again!");
            GetStartTime();
        }
        return timestamp;
    }

    public void CalculateTimeUntilNextActivity()
    {
       var activities =  FetchActivities();
       if (activities.Count == 0) {
           Console.WriteLine("No activities found. Please add an activity if you want to explore this option. \n");
           AddActivity();
       }
       
       DateTime now = DateTime.Now;
       var closestActivity = activities
           .Where(a => a.StartTime > now)
           .OrderBy(a => a.StartTime)
           .FirstOrDefault();

       
       TimeSpan timeLeft = closestActivity.StartTime - now;
       
       Console.WriteLine($"The closest activity is '{closestActivity.Description}' " +
                         $"which starts at {closestActivity.StartTime}.");
       Console.WriteLine($"Time left: {timeLeft.Days} days, {timeLeft.Hours} hours, " +
                         $"{timeLeft.Minutes} minutes, and {timeLeft.Seconds} seconds.");
    }
}